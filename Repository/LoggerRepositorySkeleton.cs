using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Appender;
using log4net.Core;
using log4net.ObjectRenderer;
using log4net.Plugin;
using log4net.Util;

namespace log4net.Repository
{
	// Token: 0x020029FD RID: 10749
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LoggerRepositorySkeleton : ILoggerRepository
	{
		// Token: 0x060091F5 RID: 37365 RVA: 0x000573B1 File Offset: 0x000555B1
		protected LoggerRepositorySkeleton()
			: this(new PropertiesDictionary())
		{
		}

		// Token: 0x060091F6 RID: 37366 RVA: 0x0013DFE0 File Offset: 0x0013C1E0
		protected LoggerRepositorySkeleton(PropertiesDictionary properties)
		{
			this.Properties = properties;
			this.m_rendererMap = new RendererMap();
			this.m_pluginMap = new PluginMap(this);
			this.m_levelMap = new LevelMap();
			this.m_configurationMessages = EmptyCollection.Instance;
			this.m_configured = false;
			this.AddBuiltinLevels();
			this.m_threshold = Level.All;
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x060091F7 RID: 37367 RVA: 0x000573BE File Offset: 0x000555BE
		// (set) Token: 0x060091F8 RID: 37368 RVA: 0x000573C6 File Offset: 0x000555C6
		public virtual string Name { get; set; }

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x060091F9 RID: 37369 RVA: 0x000573CF File Offset: 0x000555CF
		// (set) Token: 0x060091FA RID: 37370 RVA: 0x000573D7 File Offset: 0x000555D7
		public virtual Level Threshold
		{
			get
			{
				return this.m_threshold;
			}
			set
			{
				if (value != null)
				{
					this.m_threshold = value;
					return;
				}
				LogLog.Warn(LoggerRepositorySkeleton.declaringType, "LoggerRepositorySkeleton: Threshold cannot be set to null. Setting to ALL");
				this.m_threshold = Level.All;
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x060091FB RID: 37371 RVA: 0x00057404 File Offset: 0x00055604
		public virtual RendererMap RendererMap
		{
			get
			{
				return this.m_rendererMap;
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x060091FC RID: 37372 RVA: 0x0005740C File Offset: 0x0005560C
		public virtual PluginMap PluginMap
		{
			get
			{
				return this.m_pluginMap;
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x060091FD RID: 37373 RVA: 0x00057414 File Offset: 0x00055614
		public virtual LevelMap LevelMap
		{
			get
			{
				return this.m_levelMap;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x060091FE RID: 37374 RVA: 0x0005741C File Offset: 0x0005561C
		// (set) Token: 0x060091FF RID: 37375 RVA: 0x00057424 File Offset: 0x00055624
		public virtual bool Configured
		{
			get
			{
				return this.m_configured;
			}
			set
			{
				this.m_configured = value;
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06009200 RID: 37376 RVA: 0x0005742D File Offset: 0x0005562D
		// (set) Token: 0x06009201 RID: 37377 RVA: 0x00057435 File Offset: 0x00055635
		public virtual ICollection ConfigurationMessages
		{
			get
			{
				return this.m_configurationMessages;
			}
			set
			{
				this.m_configurationMessages = value;
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06009202 RID: 37378 RVA: 0x0005743E File Offset: 0x0005563E
		public PropertiesDictionary Properties { get; }

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06009203 RID: 37379 RVA: 0x00057446 File Offset: 0x00055646
		// (remove) Token: 0x06009204 RID: 37380 RVA: 0x0005744F File Offset: 0x0005564F
		public event LoggerRepositoryShutdownEventHandler ShutdownEvent
		{
			add
			{
				this.m_shutdownEvent += value;
			}
			remove
			{
				this.m_shutdownEvent -= value;
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06009205 RID: 37381 RVA: 0x00057458 File Offset: 0x00055658
		// (remove) Token: 0x06009206 RID: 37382 RVA: 0x00057461 File Offset: 0x00055661
		public event LoggerRepositoryConfigurationResetEventHandler ConfigurationReset
		{
			add
			{
				this.m_configurationResetEvent += value;
			}
			remove
			{
				this.m_configurationResetEvent -= value;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06009207 RID: 37383 RVA: 0x0005746A File Offset: 0x0005566A
		// (remove) Token: 0x06009208 RID: 37384 RVA: 0x00057473 File Offset: 0x00055673
		public event LoggerRepositoryConfigurationChangedEventHandler ConfigurationChanged
		{
			add
			{
				this.m_configurationChangedEvent += value;
			}
			remove
			{
				this.m_configurationChangedEvent -= value;
			}
		}

		// Token: 0x06009209 RID: 37385
		public abstract ILogger Exists(string name);

		// Token: 0x0600920A RID: 37386
		public abstract ILogger[] GetCurrentLoggers();

		// Token: 0x0600920B RID: 37387
		public abstract ILogger GetLogger(string name);

		// Token: 0x0600920C RID: 37388 RVA: 0x0013E040 File Offset: 0x0013C240
		public virtual void Shutdown()
		{
			foreach (IPlugin plugin in this.PluginMap.AllPlugins)
			{
				plugin.Shutdown();
			}
			this.OnShutdown(null);
		}

		// Token: 0x0600920D RID: 37389 RVA: 0x0005747C File Offset: 0x0005567C
		public virtual void ResetConfiguration()
		{
			this.m_rendererMap.Clear();
			this.m_levelMap.Clear();
			this.m_configurationMessages = EmptyCollection.Instance;
			this.AddBuiltinLevels();
			this.Configured = false;
			this.OnConfigurationReset(null);
		}

		// Token: 0x0600920E RID: 37390
		public abstract void Log(LoggingEvent logEvent);

		// Token: 0x0600920F RID: 37391
		public abstract IAppender[] GetAppenders();

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06009210 RID: 37392 RVA: 0x0013E0A0 File Offset: 0x0013C2A0
		// (remove) Token: 0x06009211 RID: 37393 RVA: 0x0013E0D8 File Offset: 0x0013C2D8
		private event LoggerRepositoryShutdownEventHandler m_shutdownEvent;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06009212 RID: 37394 RVA: 0x0013E110 File Offset: 0x0013C310
		// (remove) Token: 0x06009213 RID: 37395 RVA: 0x0013E148 File Offset: 0x0013C348
		private event LoggerRepositoryConfigurationResetEventHandler m_configurationResetEvent;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06009214 RID: 37396 RVA: 0x0013E180 File Offset: 0x0013C380
		// (remove) Token: 0x06009215 RID: 37397 RVA: 0x0013E1B8 File Offset: 0x0013C3B8
		private event LoggerRepositoryConfigurationChangedEventHandler m_configurationChangedEvent;

		// Token: 0x06009216 RID: 37398 RVA: 0x0013E1F0 File Offset: 0x0013C3F0
		private void AddBuiltinLevels()
		{
			this.m_levelMap.Add(Level.Off);
			this.m_levelMap.Add(Level.Emergency);
			this.m_levelMap.Add(Level.Fatal);
			this.m_levelMap.Add(Level.Alert);
			this.m_levelMap.Add(Level.Critical);
			this.m_levelMap.Add(Level.Severe);
			this.m_levelMap.Add(Level.Error);
			this.m_levelMap.Add(Level.Warn);
			this.m_levelMap.Add(Level.Notice);
			this.m_levelMap.Add(Level.Info);
			this.m_levelMap.Add(Level.Debug);
			this.m_levelMap.Add(Level.Fine);
			this.m_levelMap.Add(Level.Trace);
			this.m_levelMap.Add(Level.Finer);
			this.m_levelMap.Add(Level.Verbose);
			this.m_levelMap.Add(Level.Finest);
			this.m_levelMap.Add(Level.All);
		}

		// Token: 0x06009217 RID: 37399 RVA: 0x000574B3 File Offset: 0x000556B3
		public virtual void AddRenderer(Type typeToRender, IObjectRenderer rendererInstance)
		{
			if (typeToRender == null)
			{
				throw new ArgumentNullException("typeToRender");
			}
			if (rendererInstance == null)
			{
				throw new ArgumentNullException("rendererInstance");
			}
			this.m_rendererMap.Put(typeToRender, rendererInstance);
		}

		// Token: 0x06009218 RID: 37400 RVA: 0x000574DE File Offset: 0x000556DE
		protected virtual void OnShutdown(EventArgs e)
		{
			if (e == null)
			{
				e = EventArgs.Empty;
			}
			LoggerRepositoryShutdownEventHandler shutdownEvent = this.m_shutdownEvent;
			if (shutdownEvent == null)
			{
				return;
			}
			shutdownEvent(this, e);
		}

		// Token: 0x06009219 RID: 37401 RVA: 0x000574FC File Offset: 0x000556FC
		protected virtual void OnConfigurationReset(EventArgs e)
		{
			if (e == null)
			{
				e = EventArgs.Empty;
			}
			LoggerRepositoryConfigurationResetEventHandler configurationResetEvent = this.m_configurationResetEvent;
			if (configurationResetEvent == null)
			{
				return;
			}
			configurationResetEvent(this, e);
		}

		// Token: 0x0600921A RID: 37402 RVA: 0x0005751A File Offset: 0x0005571A
		protected virtual void OnConfigurationChanged(EventArgs e)
		{
			if (e == null)
			{
				e = EventArgs.Empty;
			}
			LoggerRepositoryConfigurationChangedEventHandler configurationChangedEvent = this.m_configurationChangedEvent;
			if (configurationChangedEvent == null)
			{
				return;
			}
			configurationChangedEvent(this, e);
		}

		// Token: 0x0600921B RID: 37403 RVA: 0x00057538 File Offset: 0x00055738
		public void RaiseConfigurationChanged(EventArgs e)
		{
			this.OnConfigurationChanged(e);
		}

		// Token: 0x04006146 RID: 24902
		private static readonly Type declaringType = typeof(LoggerRepositorySkeleton);

		// Token: 0x04006147 RID: 24903
		private ICollection m_configurationMessages;

		// Token: 0x04006148 RID: 24904
		private bool m_configured;

		// Token: 0x04006149 RID: 24905
		private readonly LevelMap m_levelMap;

		// Token: 0x0400614A RID: 24906
		private readonly PluginMap m_pluginMap;

		// Token: 0x0400614B RID: 24907
		private readonly RendererMap m_rendererMap;

		// Token: 0x0400614C RID: 24908
		private Level m_threshold;
	}
}
