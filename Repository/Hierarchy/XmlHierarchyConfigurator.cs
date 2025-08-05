using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Xml;
using log4net.Appender;
using log4net.Core;
using log4net.ObjectRenderer;
using log4net.Util;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x02002A09 RID: 10761
	[NullableContext(1)]
	[Nullable(0)]
	public class XmlHierarchyConfigurator
	{
		// Token: 0x06009277 RID: 37495 RVA: 0x00057912 File Offset: 0x00055B12
		public XmlHierarchyConfigurator(Hierarchy hierarchy)
		{
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06009278 RID: 37496 RVA: 0x0013EF80 File Offset: 0x0013D180
		private bool HasCaseInsensitiveEnvironment
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform != PlatformID.Unix && platform != PlatformID.MacOSX;
			}
		}

		// Token: 0x06009279 RID: 37497 RVA: 0x0013EFA8 File Offset: 0x0013D1A8
		public void Configure(XmlElement element)
		{
			if (element == null || this.<hierarchy>P == null)
			{
				return;
			}
			if (element.LocalName != "log4net")
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, "Xml element is - not a <log4net> element.");
				return;
			}
			if (!LogLog.EmitInternalMessages)
			{
				string attribute = element.GetAttribute("emitDebug");
				LogLog.Debug(XmlHierarchyConfigurator.declaringType, "emitDebug attribute [" + attribute + "].");
				if (attribute.Length > 0 && attribute != "null")
				{
					LogLog.EmitInternalMessages = OptionConverter.ToBoolean(attribute, true);
				}
				else
				{
					LogLog.Debug(XmlHierarchyConfigurator.declaringType, "Ignoring emitDebug attribute.");
				}
			}
			if (!LogLog.InternalDebugging)
			{
				string attribute2 = element.GetAttribute("debug");
				LogLog.Debug(XmlHierarchyConfigurator.declaringType, "debug attribute [" + attribute2 + "].");
				if (attribute2.Length > 0 && attribute2 != "null")
				{
					LogLog.InternalDebugging = OptionConverter.ToBoolean(attribute2, true);
				}
				else
				{
					LogLog.Debug(XmlHierarchyConfigurator.declaringType, "Ignoring debug attribute.");
				}
				string attribute3 = element.GetAttribute("configDebug");
				if (attribute3.Length > 0 && attribute3 != "null")
				{
					LogLog.Warn(XmlHierarchyConfigurator.declaringType, "The \"configDebug\" attribute is deprecated.");
					LogLog.Warn(XmlHierarchyConfigurator.declaringType, "Use the \"debug\" attribute instead.");
					LogLog.InternalDebugging = OptionConverter.ToBoolean(attribute3, true);
				}
			}
			XmlHierarchyConfigurator.ConfigUpdateMode configUpdateMode = XmlHierarchyConfigurator.ConfigUpdateMode.Merge;
			string attribute4 = element.GetAttribute("update");
			if (attribute4 != null && attribute4.Length > 0)
			{
				try
				{
					configUpdateMode = (XmlHierarchyConfigurator.ConfigUpdateMode)OptionConverter.ConvertStringTo(typeof(XmlHierarchyConfigurator.ConfigUpdateMode), attribute4);
				}
				catch
				{
					LogLog.Error(XmlHierarchyConfigurator.declaringType, "Invalid update attribute value [" + attribute4 + "]");
				}
			}
			LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Format("Configuration update mode [{0}].", configUpdateMode));
			if (configUpdateMode == XmlHierarchyConfigurator.ConfigUpdateMode.Overwrite)
			{
				this.<hierarchy>P.ResetConfiguration();
				LogLog.Debug(XmlHierarchyConfigurator.declaringType, "Configuration reset before reading config.");
			}
			foreach (object obj in element.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					string localName = xmlElement.LocalName;
					if (!(localName == "logger") && !(localName == "category"))
					{
						if (!(localName == "root"))
						{
							if (!(localName == "renderer"))
							{
								if (xmlElement.LocalName != "appender")
								{
									this.SetParameter(xmlElement, this.<hierarchy>P);
								}
							}
							else
							{
								this.ParseRenderer(xmlElement);
							}
						}
						else
						{
							this.ParseRoot(xmlElement);
						}
					}
					else
					{
						this.ParseLogger(xmlElement);
					}
				}
			}
			string attribute5 = element.GetAttribute("threshold");
			LogLog.Debug(XmlHierarchyConfigurator.declaringType, "Hierarchy Threshold [" + attribute5 + "]");
			if (attribute5.Length <= 0 || attribute5 == "null")
			{
				return;
			}
			Level level = (Level)this.ConvertStringTo(typeof(Level), attribute5);
			if (level != null)
			{
				this.<hierarchy>P.Threshold = level;
				return;
			}
			LogLog.Warn(XmlHierarchyConfigurator.declaringType, "Unable to set hierarchy threshold using value [" + attribute5 + "] (with acceptable conversion types)");
		}

		// Token: 0x0600927A RID: 37498 RVA: 0x0013F304 File Offset: 0x0013D504
		protected IAppender FindAppenderByReference(XmlElement appenderRef)
		{
			string attribute = appenderRef.GetAttribute("ref");
			IAppender appender = (IAppender)this.m_appenderBag[attribute];
			if (appender != null)
			{
				return appender;
			}
			XmlElement xmlElement = null;
			if (attribute != null && attribute.Length > 0)
			{
				foreach (object obj in appenderRef.OwnerDocument.GetElementsByTagName("appender"))
				{
					XmlElement xmlElement2 = (XmlElement)obj;
					if (xmlElement2.GetAttribute("name") == attribute)
					{
						xmlElement = xmlElement2;
						break;
					}
				}
			}
			if (xmlElement == null)
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, "XmlHierarchyConfigurator: No appender named [" + attribute + "] could be found.");
				return null;
			}
			appender = this.ParseAppender(xmlElement);
			if (appender != null)
			{
				this.m_appenderBag[attribute] = appender;
			}
			return appender;
		}

		// Token: 0x0600927B RID: 37499 RVA: 0x0013F3E8 File Offset: 0x0013D5E8
		protected IAppender ParseAppender(XmlElement appenderElement)
		{
			string attribute = appenderElement.GetAttribute("name");
			string attribute2 = appenderElement.GetAttribute("type");
			LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Loading Appender [", attribute, "] type: [", attribute2, "]" }));
			IAppender appender3;
			try
			{
				IAppender appender = (IAppender)Activator.CreateInstance(SystemInfo.GetTypeFromString(attribute2, true, true));
				appender.Name = attribute;
				foreach (object obj in appenderElement.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.LocalName == "appender-ref")
						{
							string attribute3 = xmlElement.GetAttribute("ref");
							IAppenderAttachable appenderAttachable = appender as IAppenderAttachable;
							if (appenderAttachable != null)
							{
								LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Attaching appender named [", attribute3, "] to appender named [", appender.Name, "]." }));
								IAppender appender2 = this.FindAppenderByReference(xmlElement);
								if (appender2 != null)
								{
									appenderAttachable.AddAppender(appender2);
								}
							}
							else
							{
								LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Requesting attachment of appender named [", attribute3, "] to appender named [", appender.Name, "] which does not implement log4net.Core.IAppenderAttachable." }));
							}
						}
						else
						{
							this.SetParameter(xmlElement, appender);
						}
					}
				}
				IOptionHandler optionHandler = appender as IOptionHandler;
				if (optionHandler != null)
				{
					optionHandler.ActivateOptions();
				}
				LogLog.Debug(XmlHierarchyConfigurator.declaringType, "Created Appender [" + attribute + "]");
				appender3 = appender;
			}
			catch (Exception ex)
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Could not create Appender [", attribute, "] of type [", attribute2, "]. Reported error follows." }), ex);
				appender3 = null;
			}
			return appender3;
		}

		// Token: 0x0600927C RID: 37500 RVA: 0x0013F624 File Offset: 0x0013D824
		protected void ParseLogger(XmlElement loggerElement)
		{
			string attribute = loggerElement.GetAttribute("name");
			LogLog.Debug(XmlHierarchyConfigurator.declaringType, "Retrieving an instance of log4net.Repository.Logger for logger [" + attribute + "].");
			Logger logger = this.<hierarchy>P.GetLogger(attribute) as Logger;
			Logger logger2 = logger;
			lock (logger2)
			{
				bool flag = OptionConverter.ToBoolean(loggerElement.GetAttribute("additivity"), true);
				LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Format("Setting [{0}] additivity to [{1}].", logger.Name, flag));
				logger.Additivity = flag;
				this.ParseChildrenOfLoggerElement(loggerElement, logger, false);
			}
		}

		// Token: 0x0600927D RID: 37501 RVA: 0x0013F6CC File Offset: 0x0013D8CC
		protected void ParseRoot(XmlElement rootElement)
		{
			Logger root = this.<hierarchy>P.Root;
			Logger logger = root;
			lock (logger)
			{
				this.ParseChildrenOfLoggerElement(rootElement, root, true);
			}
		}

		// Token: 0x0600927E RID: 37502 RVA: 0x0013F710 File Offset: 0x0013D910
		protected void ParseChildrenOfLoggerElement(XmlElement catElement, Logger log, bool isRoot)
		{
			log.RemoveAllAppenders();
			foreach (object obj in catElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.LocalName == "appender-ref")
					{
						IAppender appender = this.FindAppenderByReference(xmlElement);
						string attribute = xmlElement.GetAttribute("ref");
						if (appender != null)
						{
							LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Adding appender named [", attribute, "] to logger [", log.Name, "]." }));
							log.AddAppender(appender);
						}
						else
						{
							LogLog.Error(XmlHierarchyConfigurator.declaringType, "Appender named [" + attribute + "] not found.");
						}
					}
					else
					{
						string localName = xmlElement.LocalName;
						bool flag = localName == "level" || localName == "priority";
						if (flag)
						{
							this.ParseLevel(xmlElement, log, isRoot);
						}
						else
						{
							this.SetParameter(xmlElement, log);
						}
					}
				}
			}
			IOptionHandler optionHandler = log as IOptionHandler;
			if (optionHandler != null)
			{
				optionHandler.ActivateOptions();
			}
		}

		// Token: 0x0600927F RID: 37503 RVA: 0x0013F874 File Offset: 0x0013DA74
		protected void ParseRenderer(XmlElement element)
		{
			string attribute = element.GetAttribute("renderingClass");
			string attribute2 = element.GetAttribute("renderedClass");
			LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Rendering class [", attribute, "], Rendered class [", attribute2, "]." }));
			IObjectRenderer objectRenderer = (IObjectRenderer)OptionConverter.InstantiateByClassName(attribute, typeof(IObjectRenderer), null);
			if (objectRenderer == null)
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, "Could not instantiate renderer [" + attribute + "].");
				return;
			}
			try
			{
				this.<hierarchy>P.RendererMap.Put(SystemInfo.GetTypeFromString(attribute2, true, true), objectRenderer);
			}
			catch (Exception ex)
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, "Could not find class [" + attribute2 + "].", ex);
			}
		}

		// Token: 0x06009280 RID: 37504 RVA: 0x0013F950 File Offset: 0x0013DB50
		protected void ParseLevel(XmlElement element, Logger log, bool isRoot)
		{
			string text = log.Name;
			if (isRoot)
			{
				text = "root";
			}
			string attribute = element.GetAttribute("value");
			LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Logger [", text, "] Level string is [", attribute, "]." }));
			if ("inherited" == attribute)
			{
				if (isRoot)
				{
					LogLog.Error(XmlHierarchyConfigurator.declaringType, "Root level cannot be inherited. Ignoring directive.");
					return;
				}
				LogLog.Debug(XmlHierarchyConfigurator.declaringType, "Logger [" + text + "] level set to inherit from parent.");
				log.Level = null;
				return;
			}
			else
			{
				log.Level = log.Hierarchy.LevelMap[attribute];
				if (log.Level == null)
				{
					LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Undefined level [", attribute, "] on Logger [", text, "]." }));
					return;
				}
				LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Format("Logger [{0}] level set to [name=\"{1}\",value={2}].", text, log.Level.Name, log.Level.Value));
				return;
			}
		}

		// Token: 0x06009281 RID: 37505 RVA: 0x0013FA7C File Offset: 0x0013DC7C
		protected void SetParameter(XmlElement element, object target)
		{
			string text = element.GetAttribute("name");
			if (element.LocalName != "param" || text == null || text.Length == 0)
			{
				text = element.LocalName;
			}
			Type type = target.GetType();
			Type type2 = null;
			PropertyInfo propertyInfo = null;
			MethodInfo methodInfo = null;
			propertyInfo = type.GetProperty(text, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (propertyInfo != null && propertyInfo.CanWrite)
			{
				type2 = propertyInfo.PropertyType;
			}
			else
			{
				propertyInfo = null;
				methodInfo = this.FindMethodInfo(type, text);
				if (methodInfo != null)
				{
					type2 = methodInfo.GetParameters()[0].ParameterType;
				}
			}
			if (type2 == null)
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Format("XmlHierarchyConfigurator: Cannot find Property [{0}] to set object on [{1}]", text, target));
				return;
			}
			string text2 = null;
			if (element.GetAttributeNode("value") != null)
			{
				text2 = element.GetAttribute("value");
			}
			else if (element.HasChildNodes)
			{
				foreach (object obj in element.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlNodeType nodeType = xmlNode.NodeType;
					bool flag = nodeType - XmlNodeType.Text <= 1;
					if (flag)
					{
						text2 = ((text2 != null) ? (text2 + xmlNode.InnerText) : xmlNode.InnerText);
					}
				}
			}
			if (text2 != null)
			{
				try
				{
					IDictionary dictionary = Environment.GetEnvironmentVariables();
					if (this.HasCaseInsensitiveEnvironment)
					{
						dictionary = this.CreateCaseInsensitiveWrapper(dictionary);
					}
					text2 = OptionConverter.SubstituteVariables(text2, dictionary);
				}
				catch (SecurityException)
				{
					LogLog.Debug(XmlHierarchyConfigurator.declaringType, "Security exception while trying to expand environment variables. Error Ignored. No Expansion.");
				}
				Type type3 = null;
				string attribute = element.GetAttribute("type");
				if (attribute != null && attribute.Length > 0)
				{
					try
					{
						Type typeFromString = SystemInfo.GetTypeFromString(attribute, true, true);
						LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Parameter [", text, "] specified subtype [", typeFromString.FullName, "]" }));
						if (!type2.IsAssignableFrom(typeFromString))
						{
							if (OptionConverter.CanConvertTypeTo(typeFromString, type2))
							{
								type3 = type2;
								type2 = typeFromString;
							}
							else
							{
								LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "subtype [", typeFromString.FullName, "] set on [", text, "] is not a subclass of property type [", type2.FullName, "] and there are no acceptable type conversions." }));
							}
						}
						else
						{
							type2 = typeFromString;
						}
					}
					catch (Exception ex)
					{
						LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Failed to find type [", attribute, "] set on [", text, "]" }), ex);
					}
				}
				object obj2 = this.ConvertStringTo(type2, text2);
				if (obj2 != null && type3 != null)
				{
					LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Concat(new string[]
					{
						"Performing additional conversion of value from [",
						obj2.GetType().Name,
						"] to [",
						type3.Name,
						"]"
					}));
					obj2 = OptionConverter.ConvertTypeTo(obj2, type3);
				}
				if (obj2 != null)
				{
					if (propertyInfo != null)
					{
						LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Format("Setting Property [{0}] to {1} value [{2}]", propertyInfo.Name, obj2.GetType().Name, obj2));
						try
						{
							propertyInfo.SetValue(target, obj2, BindingFlags.SetProperty, null, null, CultureInfo.InvariantCulture);
							return;
						}
						catch (TargetInvocationException ex2)
						{
							LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Format("Failed to set parameter [{0}] on object [{1}] using value [{2}]", propertyInfo.Name, target, obj2), ex2.InnerException);
							return;
						}
					}
					if (methodInfo == null)
					{
						return;
					}
					LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Format("Setting Collection Property [{0}] to {1} value [{2}]", methodInfo.Name, obj2.GetType().Name, obj2));
					try
					{
						methodInfo.Invoke(target, BindingFlags.InvokeMethod, null, new object[] { obj2 }, CultureInfo.InvariantCulture);
						return;
					}
					catch (TargetInvocationException ex3)
					{
						LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Format("Failed to set parameter [{0}] on object [{1}] using value [{2}]", text, target, obj2), ex3.InnerException);
						return;
					}
				}
				LogLog.Warn(XmlHierarchyConfigurator.declaringType, string.Format("Unable to set property [{0}] on object [{1}] using value [{2}] (with acceptable conversion types)", text, target, text2));
				return;
			}
			object obj3 = null;
			if (type2 == typeof(string) && !this.HasAttributesOrElements(element))
			{
				obj3 = string.Empty;
			}
			else
			{
				Type type4 = null;
				if (XmlHierarchyConfigurator.IsTypeConstructible(type2))
				{
					type4 = type2;
				}
				obj3 = this.CreateObjectFromXml(element, type4, type2);
			}
			if (obj3 == null)
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, "Failed to create object to set param: " + text);
				return;
			}
			if (propertyInfo != null)
			{
				LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Format("Setting Property [{0}] to object [{1}]", propertyInfo.Name, obj3));
				try
				{
					propertyInfo.SetValue(target, obj3, BindingFlags.SetProperty, null, null, CultureInfo.InvariantCulture);
					return;
				}
				catch (TargetInvocationException ex4)
				{
					LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Format("Failed to set parameter [{0}] on object [{1}] using value [{2}]", propertyInfo.Name, target, obj3), ex4.InnerException);
					return;
				}
			}
			if (methodInfo == null)
			{
				return;
			}
			LogLog.Debug(XmlHierarchyConfigurator.declaringType, string.Format("Setting Collection Property [{0}] to object [{1}]", methodInfo.Name, obj3));
			try
			{
				methodInfo.Invoke(target, BindingFlags.InvokeMethod, null, new object[] { obj3 }, CultureInfo.InvariantCulture);
			}
			catch (TargetInvocationException ex5)
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Format("Failed to set parameter [{0}] on object [{1}] using value [{2}]", methodInfo.Name, target, obj3), ex5.InnerException);
			}
		}

		// Token: 0x06009282 RID: 37506 RVA: 0x0013FFF8 File Offset: 0x0013E1F8
		private bool HasAttributesOrElements(XmlElement element)
		{
			foreach (object obj in element.ChildNodes)
			{
				XmlNodeType nodeType = ((XmlNode)obj).NodeType;
				bool flag = nodeType - XmlNodeType.Element <= 1;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009283 RID: 37507 RVA: 0x00140068 File Offset: 0x0013E268
		private static bool IsTypeConstructible(Type type)
		{
			if (type != null && type.IsClass && !type.IsAbstract)
			{
				ConstructorInfo constructor = type.GetConstructor(new Type[0]);
				if (constructor != null && !constructor.IsAbstract && !constructor.IsPrivate)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009284 RID: 37508 RVA: 0x001400AC File Offset: 0x0013E2AC
		private MethodInfo FindMethodInfo(Type targetType, string name)
		{
			string text = "Add" + name;
			foreach (MethodInfo methodInfo in targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!methodInfo.IsStatic && (string.Compare(methodInfo.Name, name, true, CultureInfo.InvariantCulture) == 0 || string.Compare(methodInfo.Name, text, true, CultureInfo.InvariantCulture) == 0) && methodInfo.GetParameters().Length == 1)
				{
					return methodInfo;
				}
			}
			return null;
		}

		// Token: 0x06009285 RID: 37509 RVA: 0x00140120 File Offset: 0x0013E320
		protected object ConvertStringTo(Type type, string value)
		{
			if (typeof(Level) == type)
			{
				Level level = this.<hierarchy>P.LevelMap[value];
				if (level == null)
				{
					LogLog.Error(XmlHierarchyConfigurator.declaringType, "XmlHierarchyConfigurator: Unknown Level Specified [" + value + "]");
				}
				return level;
			}
			return OptionConverter.ConvertStringTo(type, value);
		}

		// Token: 0x06009286 RID: 37510 RVA: 0x00140178 File Offset: 0x0013E378
		protected object CreateObjectFromXml(XmlElement element, Type defaultTargetType, Type typeConstraint)
		{
			Type type = null;
			string attribute = element.GetAttribute("type");
			if (attribute == null || attribute.Length == 0)
			{
				if (defaultTargetType == null)
				{
					LogLog.Error(XmlHierarchyConfigurator.declaringType, "Object type not specified. Cannot create object of type [" + typeConstraint.FullName + "]. Missing Value or Type.");
					return null;
				}
				type = defaultTargetType;
			}
			else
			{
				try
				{
					type = SystemInfo.GetTypeFromString(attribute, true, true);
				}
				catch (Exception ex)
				{
					LogLog.Error(XmlHierarchyConfigurator.declaringType, "Failed to find type [" + attribute + "]", ex);
					return null;
				}
			}
			bool flag = false;
			if (typeConstraint != null && !typeConstraint.IsAssignableFrom(type))
			{
				if (!OptionConverter.CanConvertTypeTo(type, typeConstraint))
				{
					LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Concat(new string[] { "Object type [", type.FullName, "] is not assignable to type [", typeConstraint.FullName, "]. There are no acceptable type conversions." }));
					return null;
				}
				flag = true;
			}
			object obj = null;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (Exception ex2)
			{
				LogLog.Error(XmlHierarchyConfigurator.declaringType, string.Format("XmlHierarchyConfigurator: Failed to construct object of type [{0}] Exception: {1}", type.FullName, ex2));
			}
			foreach (object obj2 in element.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj2;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					this.SetParameter((XmlElement)xmlNode, obj);
				}
			}
			IOptionHandler optionHandler = obj as IOptionHandler;
			if (optionHandler != null)
			{
				optionHandler.ActivateOptions();
			}
			if (flag)
			{
				return OptionConverter.ConvertTypeTo(obj, typeConstraint);
			}
			return obj;
		}

		// Token: 0x06009287 RID: 37511 RVA: 0x0014031C File Offset: 0x0013E51C
		private IDictionary CreateCaseInsensitiveWrapper(IDictionary dict)
		{
			if (dict == null)
			{
				return dict;
			}
			Hashtable hashtable = SystemInfo.CreateCaseInsensitiveHashtable();
			foreach (object obj in dict)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				hashtable[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return hashtable;
		}

		// Token: 0x04006167 RID: 24935
		[CompilerGenerated]
		private Hierarchy <hierarchy>P = hierarchy;

		// Token: 0x04006168 RID: 24936
		private const string CONFIGURATION_TAG = "log4net";

		// Token: 0x04006169 RID: 24937
		private const string RENDERER_TAG = "renderer";

		// Token: 0x0400616A RID: 24938
		private const string APPENDER_TAG = "appender";

		// Token: 0x0400616B RID: 24939
		private const string APPENDER_REF_TAG = "appender-ref";

		// Token: 0x0400616C RID: 24940
		private const string PARAM_TAG = "param";

		// Token: 0x0400616D RID: 24941
		private const string CATEGORY_TAG = "category";

		// Token: 0x0400616E RID: 24942
		private const string PRIORITY_TAG = "priority";

		// Token: 0x0400616F RID: 24943
		private const string LOGGER_TAG = "logger";

		// Token: 0x04006170 RID: 24944
		private const string NAME_ATTR = "name";

		// Token: 0x04006171 RID: 24945
		private const string TYPE_ATTR = "type";

		// Token: 0x04006172 RID: 24946
		private const string VALUE_ATTR = "value";

		// Token: 0x04006173 RID: 24947
		private const string ROOT_TAG = "root";

		// Token: 0x04006174 RID: 24948
		private const string LEVEL_TAG = "level";

		// Token: 0x04006175 RID: 24949
		private const string REF_ATTR = "ref";

		// Token: 0x04006176 RID: 24950
		private const string ADDITIVITY_ATTR = "additivity";

		// Token: 0x04006177 RID: 24951
		private const string THRESHOLD_ATTR = "threshold";

		// Token: 0x04006178 RID: 24952
		private const string CONFIG_DEBUG_ATTR = "configDebug";

		// Token: 0x04006179 RID: 24953
		private const string INTERNAL_DEBUG_ATTR = "debug";

		// Token: 0x0400617A RID: 24954
		private const string EMIT_INTERNAL_DEBUG_ATTR = "emitDebug";

		// Token: 0x0400617B RID: 24955
		private const string CONFIG_UPDATE_MODE_ATTR = "update";

		// Token: 0x0400617C RID: 24956
		private const string RENDERING_TYPE_ATTR = "renderingClass";

		// Token: 0x0400617D RID: 24957
		private const string RENDERED_TYPE_ATTR = "renderedClass";

		// Token: 0x0400617E RID: 24958
		private const string INHERITED = "inherited";

		// Token: 0x0400617F RID: 24959
		private static readonly Type declaringType = typeof(XmlHierarchyConfigurator);

		// Token: 0x04006180 RID: 24960
		private readonly Hashtable m_appenderBag = new Hashtable();

		// Token: 0x02002A0A RID: 10762
		[NullableContext(0)]
		private enum ConfigUpdateMode
		{
			// Token: 0x04006182 RID: 24962
			Merge,
			// Token: 0x04006183 RID: 24963
			Overwrite
		}
	}
}
