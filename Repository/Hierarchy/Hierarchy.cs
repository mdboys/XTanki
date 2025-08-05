using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Xml;
using log4net.Appender;
using log4net.Core;
using log4net.Util;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x02002A00 RID: 10752
	[NullableContext(1)]
	[Nullable(0)]
	public class Hierarchy : LoggerRepositorySkeleton, IBasicRepositoryConfigurator, IXmlRepositoryConfigurator
	{
		// Token: 0x06009220 RID: 37408 RVA: 0x0005757C File Offset: 0x0005577C
		public Hierarchy()
			: this(new DefaultLoggerFactory())
		{
		}

		// Token: 0x06009221 RID: 37409 RVA: 0x00057589 File Offset: 0x00055789
		public Hierarchy(PropertiesDictionary properties)
			: this(properties, new DefaultLoggerFactory())
		{
		}

		// Token: 0x06009222 RID: 37410 RVA: 0x00057597 File Offset: 0x00055797
		public Hierarchy(ILoggerFactory loggerFactory)
			: this(new PropertiesDictionary(), loggerFactory)
		{
		}

		// Token: 0x06009223 RID: 37411 RVA: 0x000575A5 File Offset: 0x000557A5
		public Hierarchy(PropertiesDictionary properties, ILoggerFactory loggerFactory)
			: base(properties)
		{
			if (loggerFactory == null)
			{
				throw new ArgumentNullException("loggerFactory");
			}
			this.m_defaultFactory = loggerFactory;
			this.m_ht = Hashtable.Synchronized(new Hashtable());
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06009224 RID: 37412 RVA: 0x000575D3 File Offset: 0x000557D3
		// (set) Token: 0x06009225 RID: 37413 RVA: 0x000575DB File Offset: 0x000557DB
		public bool EmittedNoAppenderWarning { get; set; }

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06009226 RID: 37414 RVA: 0x0013E310 File Offset: 0x0013C510
		public Logger Root
		{
			get
			{
				if (this.m_root == null)
				{
					lock (this)
					{
						if (this.m_root == null)
						{
							Logger logger = this.m_defaultFactory.CreateLogger(this, null);
							logger.Hierarchy = this;
							this.m_root = logger;
						}
					}
				}
				return this.m_root;
			}
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06009227 RID: 37415 RVA: 0x000575E4 File Offset: 0x000557E4
		// (set) Token: 0x06009228 RID: 37416 RVA: 0x000575EC File Offset: 0x000557EC
		public ILoggerFactory LoggerFactory
		{
			get
			{
				return this.m_defaultFactory;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_defaultFactory = value;
			}
		}

		// Token: 0x06009229 RID: 37417 RVA: 0x00057603 File Offset: 0x00055803
		void IBasicRepositoryConfigurator.Configure(IAppender appender)
		{
			this.BasicRepositoryConfigure(new IAppender[] { appender });
		}

		// Token: 0x0600922A RID: 37418 RVA: 0x00057615 File Offset: 0x00055815
		void IBasicRepositoryConfigurator.Configure(params IAppender[] appenders)
		{
			this.BasicRepositoryConfigure(appenders);
		}

		// Token: 0x0600922B RID: 37419 RVA: 0x0005761E File Offset: 0x0005581E
		void IXmlRepositoryConfigurator.Configure(XmlElement element)
		{
			this.XmlRepositoryConfigure(element);
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600922C RID: 37420 RVA: 0x00057627 File Offset: 0x00055827
		// (remove) Token: 0x0600922D RID: 37421 RVA: 0x00057630 File Offset: 0x00055830
		public event LoggerCreationEventHandler LoggerCreatedEvent
		{
			add
			{
				this.m_loggerCreatedEvent += value;
			}
			remove
			{
				this.m_loggerCreatedEvent -= value;
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600922E RID: 37422 RVA: 0x0013E370 File Offset: 0x0013C570
		// (remove) Token: 0x0600922F RID: 37423 RVA: 0x0013E3A8 File Offset: 0x0013C5A8
		private event LoggerCreationEventHandler m_loggerCreatedEvent;

		// Token: 0x06009230 RID: 37424 RVA: 0x00057639 File Offset: 0x00055839
		public override ILogger Exists(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.m_ht[new LoggerKey(name)] as Logger;
		}

		// Token: 0x06009231 RID: 37425 RVA: 0x0013E3E0 File Offset: 0x0013C5E0
		public override ILogger[] GetCurrentLoggers()
		{
			ArrayList arrayList = new ArrayList(this.m_ht.Count);
			foreach (object obj in this.m_ht.Values)
			{
				if (obj is Logger)
				{
					arrayList.Add(obj);
				}
			}
			return (Logger[])arrayList.ToArray(typeof(Logger));
		}

		// Token: 0x06009232 RID: 37426 RVA: 0x0005765F File Offset: 0x0005585F
		public override ILogger GetLogger(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetLogger(name, this.m_defaultFactory);
		}

		// Token: 0x06009233 RID: 37427 RVA: 0x0013E46C File Offset: 0x0013C66C
		public override void Shutdown()
		{
			LogLog.Debug(Hierarchy.declaringType, "Shutdown called on Hierarchy [" + this.Name + "]");
			this.Root.CloseNestedAppenders();
			Hashtable ht = this.m_ht;
			lock (ht)
			{
				ILogger[] currentLoggers = this.GetCurrentLoggers();
				for (int i = 0; i < currentLoggers.Length; i++)
				{
					((Logger)currentLoggers[i]).CloseNestedAppenders();
				}
				this.Root.RemoveAllAppenders();
				for (int j = 0; j < currentLoggers.Length; j++)
				{
					((Logger)currentLoggers[j]).RemoveAllAppenders();
				}
			}
			base.Shutdown();
		}

		// Token: 0x06009234 RID: 37428 RVA: 0x0013E518 File Offset: 0x0013C718
		public override void ResetConfiguration()
		{
			this.Root.Level = this.LevelMap.LookupWithDefault(Level.Debug);
			this.Threshold = this.LevelMap.LookupWithDefault(Level.All);
			Hashtable ht = this.m_ht;
			lock (ht)
			{
				this.Shutdown();
				foreach (Logger logger in this.GetCurrentLoggers())
				{
					logger.Level = null;
					logger.Additivity = true;
				}
			}
			base.ResetConfiguration();
			this.OnConfigurationChanged(null);
		}

		// Token: 0x06009235 RID: 37429 RVA: 0x0005767C File Offset: 0x0005587C
		public override void Log(LoggingEvent logEvent)
		{
			if (logEvent == null)
			{
				throw new ArgumentNullException("logEvent");
			}
			this.GetLogger(logEvent.LoggerName, this.m_defaultFactory).Log(logEvent);
		}

		// Token: 0x06009236 RID: 37430 RVA: 0x0013E5BC File Offset: 0x0013C7BC
		public override IAppender[] GetAppenders()
		{
			ArrayList arrayList = new ArrayList();
			Hierarchy.CollectAppenders(arrayList, this.Root);
			foreach (Logger logger in this.GetCurrentLoggers())
			{
				Hierarchy.CollectAppenders(arrayList, logger);
			}
			return (IAppender[])arrayList.ToArray(typeof(IAppender));
		}

		// Token: 0x06009237 RID: 37431 RVA: 0x0013E618 File Offset: 0x0013C818
		private static void CollectAppender(ArrayList appenderList, IAppender appender)
		{
			if (!appenderList.Contains(appender))
			{
				appenderList.Add(appender);
				IAppenderAttachable appenderAttachable = appender as IAppenderAttachable;
				if (appenderAttachable != null)
				{
					Hierarchy.CollectAppenders(appenderList, appenderAttachable);
				}
			}
		}

		// Token: 0x06009238 RID: 37432 RVA: 0x0013E648 File Offset: 0x0013C848
		private static void CollectAppenders(ArrayList appenderList, IAppenderAttachable container)
		{
			foreach (IAppender appender in container.Appenders)
			{
				Hierarchy.CollectAppender(appenderList, appender);
			}
		}

		// Token: 0x06009239 RID: 37433 RVA: 0x0013E69C File Offset: 0x0013C89C
		protected void BasicRepositoryConfigure(params IAppender[] appenders)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				foreach (IAppender appender in appenders)
				{
					this.Root.AddAppender(appender);
				}
			}
			this.Configured = true;
			this.ConfigurationMessages = arrayList;
			this.OnConfigurationChanged(new ConfigurationChangedEventArgs(arrayList));
		}

		// Token: 0x0600923A RID: 37434 RVA: 0x0013E710 File Offset: 0x0013C910
		protected void XmlRepositoryConfigure(XmlElement element)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				new XmlHierarchyConfigurator(this).Configure(element);
			}
			this.Configured = true;
			this.ConfigurationMessages = arrayList;
			this.OnConfigurationChanged(new ConfigurationChangedEventArgs(arrayList));
		}

		// Token: 0x0600923B RID: 37435 RVA: 0x000576A4 File Offset: 0x000558A4
		public bool IsDisabled(Level level)
		{
			if (level == null)
			{
				throw new ArgumentNullException("level");
			}
			return !this.Configured || this.Threshold > level;
		}

		// Token: 0x0600923C RID: 37436 RVA: 0x000576CA File Offset: 0x000558CA
		public void Clear()
		{
			this.m_ht.Clear();
		}

		// Token: 0x0600923D RID: 37437 RVA: 0x0013E76C File Offset: 0x0013C96C
		public Logger GetLogger(string name, ILoggerFactory factory)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			LoggerKey loggerKey = new LoggerKey(name);
			Hashtable ht = this.m_ht;
			Logger logger2;
			lock (ht)
			{
				object obj = this.m_ht[loggerKey];
				if (obj == null)
				{
					Logger logger = factory.CreateLogger(this, name);
					logger.Hierarchy = this;
					this.m_ht[loggerKey] = logger;
					this.UpdateParents(logger);
					this.OnLoggerCreationEvent(logger);
					logger2 = logger;
				}
				else
				{
					Logger logger3 = obj as Logger;
					if (logger3 != null)
					{
						logger2 = logger3;
					}
					else
					{
						ProvisionNode provisionNode = obj as ProvisionNode;
						if (provisionNode != null)
						{
							Logger logger = factory.CreateLogger(this, name);
							logger.Hierarchy = this;
							this.m_ht[loggerKey] = logger;
							Hierarchy.UpdateChildren(provisionNode, logger);
							this.UpdateParents(logger);
							this.OnLoggerCreationEvent(logger);
							logger2 = logger;
						}
						else
						{
							logger2 = null;
						}
					}
				}
			}
			return logger2;
		}

		// Token: 0x0600923E RID: 37438 RVA: 0x000576D7 File Offset: 0x000558D7
		protected virtual void OnLoggerCreationEvent(Logger logger)
		{
			LoggerCreationEventHandler loggerCreatedEvent = this.m_loggerCreatedEvent;
			if (loggerCreatedEvent == null)
			{
				return;
			}
			loggerCreatedEvent(this, new LoggerCreationEventArgs(logger));
		}

		// Token: 0x0600923F RID: 37439 RVA: 0x0013E860 File Offset: 0x0013CA60
		private void UpdateParents(Logger log)
		{
			string name = log.Name;
			int length = name.Length;
			bool flag = false;
			for (int i = name.LastIndexOf('.', length - 1); i >= 0; i = name.LastIndexOf('.', i - 1))
			{
				LoggerKey loggerKey = new LoggerKey(name.Substring(0, i));
				object obj = this.m_ht[loggerKey];
				if (obj == null)
				{
					ProvisionNode provisionNode = new ProvisionNode(log);
					this.m_ht[loggerKey] = provisionNode;
				}
				else
				{
					Logger logger = obj as Logger;
					if (logger != null)
					{
						flag = true;
						log.Parent = logger;
						break;
					}
					ProvisionNode provisionNode2 = obj as ProvisionNode;
					if (provisionNode2 != null)
					{
						provisionNode2.Add(log);
					}
					else
					{
						LogLog.Error(Hierarchy.declaringType, string.Format("Unexpected object type [{0}] in ht.", obj.GetType()), new LogException());
					}
				}
				if (i == 0)
				{
					break;
				}
			}
			if (!flag)
			{
				log.Parent = this.Root;
			}
		}

		// Token: 0x06009240 RID: 37440 RVA: 0x0013E940 File Offset: 0x0013CB40
		private static void UpdateChildren(ProvisionNode pn, Logger log)
		{
			for (int i = 0; i < pn.Count; i++)
			{
				Logger logger = (Logger)pn[i];
				if (!logger.Parent.Name.StartsWith(log.Name))
				{
					log.Parent = logger.Parent;
					logger.Parent = log;
				}
			}
		}

		// Token: 0x06009241 RID: 37441 RVA: 0x0013E998 File Offset: 0x0013CB98
		internal void AddLevel(Hierarchy.LevelEntry levelEntry)
		{
			if (levelEntry == null)
			{
				throw new ArgumentNullException("levelEntry");
			}
			if (levelEntry.Name == null)
			{
				throw new ArgumentNullException("levelEntry.Name");
			}
			if (levelEntry.Value == -1)
			{
				Level level = this.LevelMap[levelEntry.Name];
				if (level == null)
				{
					throw new InvalidOperationException("Cannot redefine level [" + levelEntry.Name + "] because it is not defined in the LevelMap. To define the level supply the level value.");
				}
				levelEntry.Value = level.Value;
			}
			this.LevelMap.Add(levelEntry.Name, levelEntry.Value, levelEntry.DisplayName);
		}

		// Token: 0x06009242 RID: 37442 RVA: 0x000576F0 File Offset: 0x000558F0
		internal void AddProperty(PropertyEntry propertyEntry)
		{
			if (propertyEntry == null)
			{
				throw new ArgumentNullException("propertyEntry");
			}
			if (propertyEntry.Key == null)
			{
				throw new ArgumentNullException("propertyEntry.Key");
			}
			base.Properties[propertyEntry.Key] = propertyEntry.Value;
		}

		// Token: 0x04006152 RID: 24914
		private static readonly Type declaringType = typeof(Hierarchy);

		// Token: 0x04006153 RID: 24915
		private ILoggerFactory m_defaultFactory;

		// Token: 0x04006154 RID: 24916
		private readonly Hashtable m_ht;

		// Token: 0x04006155 RID: 24917
		private Logger m_root;

		// Token: 0x02002A01 RID: 10753
		[Nullable(0)]
		internal class LevelEntry
		{
			// Token: 0x170016C8 RID: 5832
			// (get) Token: 0x06009244 RID: 37444 RVA: 0x0005773B File Offset: 0x0005593B
			// (set) Token: 0x06009245 RID: 37445 RVA: 0x00057743 File Offset: 0x00055943
			public int Value { get; set; } = -1;

			// Token: 0x170016C9 RID: 5833
			// (get) Token: 0x06009246 RID: 37446 RVA: 0x0005774C File Offset: 0x0005594C
			// (set) Token: 0x06009247 RID: 37447 RVA: 0x00057754 File Offset: 0x00055954
			public string Name { get; set; }

			// Token: 0x170016CA RID: 5834
			// (get) Token: 0x06009248 RID: 37448 RVA: 0x0005775D File Offset: 0x0005595D
			// (set) Token: 0x06009249 RID: 37449 RVA: 0x00057765 File Offset: 0x00055965
			public string DisplayName { get; set; }

			// Token: 0x0600924A RID: 37450 RVA: 0x0005776E File Offset: 0x0005596E
			public override string ToString()
			{
				return string.Format("LevelEntry(Value={0}, Name={1}, DisplayName={2})", this.Value, this.Name, this.DisplayName);
			}
		}
	}
}
