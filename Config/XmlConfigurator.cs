using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using log4net.Repository;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x02002A79 RID: 10873
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class XmlConfigurator
	{
		// Token: 0x06009519 RID: 38169 RVA: 0x00005698 File Offset: 0x00003898
		private XmlConfigurator()
		{
		}

		// Token: 0x0600951A RID: 38170 RVA: 0x0005944B File Offset: 0x0005764B
		public static ICollection Configure()
		{
			return XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
		}

		// Token: 0x0600951B RID: 38171 RVA: 0x00144894 File Offset: 0x00142A94
		public static ICollection Configure(ILoggerRepository repository)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x0600951C RID: 38172 RVA: 0x001448D8 File Offset: 0x00142AD8
		private static void InternalConfigure(ILoggerRepository repository)
		{
			LogLog.Debug(XmlConfigurator.declaringType, "configuring repository [" + repository.Name + "] using .config file section");
			try
			{
				LogLog.Debug(XmlConfigurator.declaringType, "Application config file is [" + SystemInfo.ConfigurationFileLocation + "]");
			}
			catch
			{
				LogLog.Debug(XmlConfigurator.declaringType, "Application config file location unknown");
			}
			try
			{
				XmlElement xmlElement = ConfigurationSettings.GetConfig("log4net") as XmlElement;
				if (xmlElement == null)
				{
					LogLog.Error(XmlConfigurator.declaringType, "Failed to find configuration section 'log4net' in the application's .config file. Check your .config file for the <log4net> and <configSections> elements. The configuration section should look like: <section name=\"log4net\" type=\"log4net.Config.Log4NetConfigurationSectionHandler,log4net\" />");
				}
				else
				{
					XmlConfigurator.InternalConfigureFromXml(repository, xmlElement);
				}
			}
			catch (ConfigurationException ex)
			{
				if (ex.BareMessage.IndexOf("Unrecognized element") >= 0)
				{
					LogLog.Error(XmlConfigurator.declaringType, "Failed to parse config file. Check your .config file is well formed XML.", ex);
				}
				else
				{
					string text = "<section name=\"log4net\" type=\"log4net.Config.Log4NetConfigurationSectionHandler," + Assembly.GetExecutingAssembly().FullName + "\" />";
					LogLog.Error(XmlConfigurator.declaringType, "Failed to parse config file. Is the <configSections> specified as: " + text, ex);
				}
			}
		}

		// Token: 0x0600951D RID: 38173 RVA: 0x001449D8 File Offset: 0x00142BD8
		public static ICollection Configure(XmlElement element)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigureFromXml(repository, element);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x0600951E RID: 38174 RVA: 0x00144A28 File Offset: 0x00142C28
		public static ICollection Configure(ILoggerRepository repository, XmlElement element)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				LogLog.Debug(XmlConfigurator.declaringType, "configuring repository [" + repository.Name + "] using XML element");
				XmlConfigurator.InternalConfigureFromXml(repository, element);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x0600951F RID: 38175 RVA: 0x00144A8C File Offset: 0x00142C8C
		public static ICollection Configure(FileInfo configFile)
		{
			ArrayList arrayList = new ArrayList();
			ICollection collection;
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(LogManager.GetRepository(Assembly.GetCallingAssembly()), configFile);
				collection = arrayList;
			}
			return collection;
		}

		// Token: 0x06009520 RID: 38176 RVA: 0x00144AD8 File Offset: 0x00142CD8
		public static ICollection Configure(Uri configUri)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configUri);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x06009521 RID: 38177 RVA: 0x00144B28 File Offset: 0x00142D28
		public static ICollection Configure(Stream configStream)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configStream);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x06009522 RID: 38178 RVA: 0x00144B78 File Offset: 0x00142D78
		public static ICollection Configure(ILoggerRepository repository, FileInfo configFile)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configFile);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x06009523 RID: 38179 RVA: 0x00144BC0 File Offset: 0x00142DC0
		private static void InternalConfigure(ILoggerRepository repository, FileInfo configFile)
		{
			LogLog.Debug(XmlConfigurator.declaringType, string.Format("configuring repository [{0}] using file [{1}]", repository.Name, configFile));
			if (configFile == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Configure called with null 'configFile' parameter");
				return;
			}
			if (File.Exists(configFile.FullName))
			{
				FileStream fileStream = null;
				int num = 5;
				while (--num >= 0)
				{
					try
					{
						fileStream = configFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
						break;
					}
					catch (IOException ex)
					{
						if (num == 0)
						{
							LogLog.Error(XmlConfigurator.declaringType, "Failed to open XML config file [" + configFile.Name + "]", ex);
							fileStream = null;
						}
						Thread.Sleep(250);
					}
				}
				if (fileStream == null)
				{
					return;
				}
				try
				{
					XmlConfigurator.InternalConfigure(repository, fileStream);
					return;
				}
				finally
				{
					fileStream.Close();
				}
			}
			LogLog.Debug(XmlConfigurator.declaringType, "config file [" + configFile.FullName + "] not found. Configuration unchanged.");
		}

		// Token: 0x06009524 RID: 38180 RVA: 0x00144CA4 File Offset: 0x00142EA4
		public static ICollection Configure(ILoggerRepository repository, Uri configUri)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configUri);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x06009525 RID: 38181 RVA: 0x00144CEC File Offset: 0x00142EEC
		private static void InternalConfigure(ILoggerRepository repository, Uri configUri)
		{
			LogLog.Debug(XmlConfigurator.declaringType, string.Format("configuring repository [{0}] using URI [{1}]", repository.Name, configUri));
			if (configUri == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Configure called with null 'configUri' parameter");
				return;
			}
			if (configUri.IsFile)
			{
				XmlConfigurator.InternalConfigure(repository, new FileInfo(configUri.LocalPath));
				return;
			}
			WebRequest webRequest = null;
			try
			{
				webRequest = WebRequest.Create(configUri);
			}
			catch (Exception ex)
			{
				LogLog.Error(XmlConfigurator.declaringType, string.Format("Failed to create WebRequest for URI [{0}]", configUri), ex);
			}
			if (webRequest == null)
			{
				return;
			}
			try
			{
				webRequest.Credentials = CredentialCache.DefaultCredentials;
			}
			catch
			{
			}
			try
			{
				WebResponse response = webRequest.GetResponse();
				if (response != null)
				{
					try
					{
						using (Stream responseStream = response.GetResponseStream())
						{
							XmlConfigurator.InternalConfigure(repository, responseStream);
						}
					}
					finally
					{
						response.Close();
					}
				}
			}
			catch (Exception ex2)
			{
				LogLog.Error(XmlConfigurator.declaringType, string.Format("Failed to request config from URI [{0}]", configUri), ex2);
			}
		}

		// Token: 0x06009526 RID: 38182 RVA: 0x00144E10 File Offset: 0x00143010
		public static ICollection Configure(ILoggerRepository repository, Stream configStream)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigure(repository, configStream);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x06009527 RID: 38183 RVA: 0x00144E58 File Offset: 0x00143058
		private static void InternalConfigure(ILoggerRepository repository, Stream configStream)
		{
			LogLog.Debug(XmlConfigurator.declaringType, "configuring repository [" + repository.Name + "] using stream");
			if (configStream == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Configure called with null 'configStream' parameter");
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
				{
					ProhibitDtd = false
				};
				XmlReader xmlReader = XmlReader.Create(configStream, xmlReaderSettings);
				xmlDocument.Load(xmlReader);
			}
			catch (Exception ex)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Error while loading XML configuration", ex);
				xmlDocument = null;
			}
			if (xmlDocument != null)
			{
				LogLog.Debug(XmlConfigurator.declaringType, "loading XML configuration");
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("log4net");
				if (elementsByTagName.Count == 0)
				{
					LogLog.Debug(XmlConfigurator.declaringType, "XML configuration does not contain a <log4net> element. Configuration Aborted.");
					return;
				}
				if (elementsByTagName.Count > 1)
				{
					LogLog.Error(XmlConfigurator.declaringType, string.Format("XML configuration contains [{0}] <log4net> elements. Only one is allowed. Configuration Aborted.", elementsByTagName.Count));
					return;
				}
				XmlConfigurator.InternalConfigureFromXml(repository, elementsByTagName[0] as XmlElement);
			}
		}

		// Token: 0x06009528 RID: 38184 RVA: 0x00144F58 File Offset: 0x00143158
		public static ICollection ConfigureAndWatch(FileInfo configFile)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigureAndWatch(repository, configFile);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x06009529 RID: 38185 RVA: 0x00144FA8 File Offset: 0x001431A8
		public static ICollection ConfigureAndWatch(ILoggerRepository repository, FileInfo configFile)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				XmlConfigurator.InternalConfigureAndWatch(repository, configFile);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x0600952A RID: 38186 RVA: 0x00144FF0 File Offset: 0x001431F0
		private static void InternalConfigureAndWatch(ILoggerRepository repository, FileInfo configFile)
		{
			LogLog.Debug(XmlConfigurator.declaringType, string.Format("configuring repository [{0}] using file [{1}] watching for file updates", repository.Name, configFile));
			if (configFile == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "ConfigureAndWatch called with null 'configFile' parameter");
				return;
			}
			XmlConfigurator.InternalConfigure(repository, configFile);
			try
			{
				Hashtable repositoryName2ConfigAndWatchHandler = XmlConfigurator.m_repositoryName2ConfigAndWatchHandler;
				lock (repositoryName2ConfigAndWatchHandler)
				{
					XmlConfigurator.ConfigureAndWatchHandler configureAndWatchHandler = (XmlConfigurator.ConfigureAndWatchHandler)XmlConfigurator.m_repositoryName2ConfigAndWatchHandler[configFile.FullName];
					if (configureAndWatchHandler != null)
					{
						XmlConfigurator.m_repositoryName2ConfigAndWatchHandler.Remove(configFile.FullName);
						configureAndWatchHandler.Dispose();
					}
					configureAndWatchHandler = new XmlConfigurator.ConfigureAndWatchHandler(repository, configFile);
					XmlConfigurator.m_repositoryName2ConfigAndWatchHandler[configFile.FullName] = configureAndWatchHandler;
				}
			}
			catch (Exception ex)
			{
				LogLog.Error(XmlConfigurator.declaringType, "Failed to initialize configuration file watcher for file [" + configFile.FullName + "]", ex);
			}
		}

		// Token: 0x0600952B RID: 38187 RVA: 0x001450D0 File Offset: 0x001432D0
		private static void InternalConfigureFromXml(ILoggerRepository repository, XmlElement element)
		{
			if (element == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "ConfigureFromXml called with null 'element' parameter");
				return;
			}
			if (repository == null)
			{
				LogLog.Error(XmlConfigurator.declaringType, "ConfigureFromXml called with null 'repository' parameter");
				return;
			}
			LogLog.Debug(XmlConfigurator.declaringType, "Configuring Repository [" + repository.Name + "]");
			IXmlRepositoryConfigurator xmlRepositoryConfigurator = repository as IXmlRepositoryConfigurator;
			if (xmlRepositoryConfigurator == null)
			{
				LogLog.Warn(XmlConfigurator.declaringType, string.Format("Repository [{0}] does not support the XmlConfigurator", repository));
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = (XmlElement)xmlDocument.AppendChild(xmlDocument.ImportNode(element, true));
			xmlRepositoryConfigurator.Configure(xmlElement);
		}

		// Token: 0x0400626C RID: 25196
		private static readonly Hashtable m_repositoryName2ConfigAndWatchHandler = new Hashtable();

		// Token: 0x0400626D RID: 25197
		private static readonly Type declaringType = typeof(XmlConfigurator);

		// Token: 0x02002A7A RID: 10874
		[Nullable(0)]
		private sealed class ConfigureAndWatchHandler : IDisposable
		{
			// Token: 0x0600952D RID: 38189 RVA: 0x00145164 File Offset: 0x00143364
			public ConfigureAndWatchHandler(ILoggerRepository repository, FileInfo configFile)
			{
				this.m_repository = repository;
				this.m_configFile = configFile;
				this.m_watcher = new FileSystemWatcher();
				this.m_watcher.Path = this.m_configFile.DirectoryName;
				this.m_watcher.Filter = this.m_configFile.Name;
				this.m_watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite;
				this.m_watcher.Changed += this.ConfigureAndWatchHandler_OnChanged;
				this.m_watcher.Created += this.ConfigureAndWatchHandler_OnChanged;
				this.m_watcher.Deleted += this.ConfigureAndWatchHandler_OnChanged;
				this.m_watcher.Renamed += this.ConfigureAndWatchHandler_OnRenamed;
				this.m_watcher.EnableRaisingEvents = true;
				this.m_timer = new Timer(new TimerCallback(this.OnWatchedFileChange), null, -1, -1);
			}

			// Token: 0x0600952E RID: 38190 RVA: 0x00059477 File Offset: 0x00057677
			public void Dispose()
			{
				this.m_watcher.EnableRaisingEvents = false;
				this.m_watcher.Dispose();
				this.m_timer.Dispose();
			}

			// Token: 0x0600952F RID: 38191 RVA: 0x0005949B File Offset: 0x0005769B
			private void ConfigureAndWatchHandler_OnChanged(object source, FileSystemEventArgs e)
			{
				LogLog.Debug(XmlConfigurator.declaringType, string.Format("ConfigureAndWatchHandler: {0} [{1}]", e.ChangeType, this.m_configFile.FullName));
				this.m_timer.Change(500, -1);
			}

			// Token: 0x06009530 RID: 38192 RVA: 0x0005949B File Offset: 0x0005769B
			private void ConfigureAndWatchHandler_OnRenamed(object source, RenamedEventArgs e)
			{
				LogLog.Debug(XmlConfigurator.declaringType, string.Format("ConfigureAndWatchHandler: {0} [{1}]", e.ChangeType, this.m_configFile.FullName));
				this.m_timer.Change(500, -1);
			}

			// Token: 0x06009531 RID: 38193 RVA: 0x000594D9 File Offset: 0x000576D9
			private void OnWatchedFileChange(object state)
			{
				XmlConfigurator.InternalConfigure(this.m_repository, this.m_configFile);
			}

			// Token: 0x0400626E RID: 25198
			private const int TimeoutMillis = 500;

			// Token: 0x0400626F RID: 25199
			private readonly FileInfo m_configFile;

			// Token: 0x04006270 RID: 25200
			private readonly ILoggerRepository m_repository;

			// Token: 0x04006271 RID: 25201
			private readonly Timer m_timer;

			// Token: 0x04006272 RID: 25202
			private readonly FileSystemWatcher m_watcher;
		}
	}
}
