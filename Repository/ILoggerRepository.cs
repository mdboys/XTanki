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
	// Token: 0x020029F8 RID: 10744
	[NullableContext(1)]
	public interface ILoggerRepository
	{
		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x060091CF RID: 37327
		// (set) Token: 0x060091D0 RID: 37328
		string Name { get; set; }

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x060091D1 RID: 37329
		RendererMap RendererMap { get; }

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x060091D2 RID: 37330
		PluginMap PluginMap { get; }

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x060091D3 RID: 37331
		LevelMap LevelMap { get; }

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x060091D4 RID: 37332
		// (set) Token: 0x060091D5 RID: 37333
		Level Threshold { get; set; }

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x060091D6 RID: 37334
		// (set) Token: 0x060091D7 RID: 37335
		bool Configured { get; set; }

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x060091D8 RID: 37336
		// (set) Token: 0x060091D9 RID: 37337
		ICollection ConfigurationMessages { get; set; }

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x060091DA RID: 37338
		PropertiesDictionary Properties { get; }

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060091DB RID: 37339
		// (remove) Token: 0x060091DC RID: 37340
		event LoggerRepositoryShutdownEventHandler ShutdownEvent;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060091DD RID: 37341
		// (remove) Token: 0x060091DE RID: 37342
		event LoggerRepositoryConfigurationResetEventHandler ConfigurationReset;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060091DF RID: 37343
		// (remove) Token: 0x060091E0 RID: 37344
		event LoggerRepositoryConfigurationChangedEventHandler ConfigurationChanged;

		// Token: 0x060091E1 RID: 37345
		ILogger Exists(string name);

		// Token: 0x060091E2 RID: 37346
		ILogger[] GetCurrentLoggers();

		// Token: 0x060091E3 RID: 37347
		ILogger GetLogger(string name);

		// Token: 0x060091E4 RID: 37348
		void Shutdown();

		// Token: 0x060091E5 RID: 37349
		void ResetConfiguration();

		// Token: 0x060091E6 RID: 37350
		void Log(LoggingEvent logEvent);

		// Token: 0x060091E7 RID: 37351
		IAppender[] GetAppenders();
	}
}
