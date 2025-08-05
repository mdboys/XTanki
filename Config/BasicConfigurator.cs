using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x02002A73 RID: 10867
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BasicConfigurator
	{
		// Token: 0x060094F8 RID: 38136 RVA: 0x00005698 File Offset: 0x00003898
		private BasicConfigurator()
		{
		}

		// Token: 0x060094F9 RID: 38137 RVA: 0x00059308 File Offset: 0x00057508
		public static ICollection Configure()
		{
			return BasicConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
		}

		// Token: 0x060094FA RID: 38138 RVA: 0x00059319 File Offset: 0x00057519
		public static ICollection Configure(IAppender appender)
		{
			return BasicConfigurator.Configure(new IAppender[] { appender });
		}

		// Token: 0x060094FB RID: 38139 RVA: 0x00144618 File Offset: 0x00142818
		public static ICollection Configure(params IAppender[] appenders)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				BasicConfigurator.InternalConfigure(repository, appenders);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060094FC RID: 38140 RVA: 0x00144668 File Offset: 0x00142868
		public static ICollection Configure(ILoggerRepository repository)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				PatternLayout patternLayout = new PatternLayout
				{
					ConversionPattern = "%timestamp [%thread] %level %logger %ndc - %message%newline"
				};
				patternLayout.ActivateOptions();
				ConsoleAppender consoleAppender = new ConsoleAppender
				{
					Layout = patternLayout
				};
				consoleAppender.ActivateOptions();
				BasicConfigurator.InternalConfigure(repository, new IAppender[] { consoleAppender });
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060094FD RID: 38141 RVA: 0x0005932A File Offset: 0x0005752A
		public static ICollection Configure(ILoggerRepository repository, IAppender appender)
		{
			return BasicConfigurator.Configure(repository, new IAppender[] { appender });
		}

		// Token: 0x060094FE RID: 38142 RVA: 0x001446E0 File Offset: 0x001428E0
		public static ICollection Configure(ILoggerRepository repository, params IAppender[] appenders)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				BasicConfigurator.InternalConfigure(repository, appenders);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x060094FF RID: 38143 RVA: 0x00144728 File Offset: 0x00142928
		private static void InternalConfigure(ILoggerRepository repository, params IAppender[] appenders)
		{
			IBasicRepositoryConfigurator basicRepositoryConfigurator = repository as IBasicRepositoryConfigurator;
			if (basicRepositoryConfigurator != null)
			{
				basicRepositoryConfigurator.Configure(appenders);
				return;
			}
			LogLog.Warn(BasicConfigurator.declaringType, string.Format("BasicConfigurator: Repository [{0}] does not support the BasicConfigurator", repository));
		}

		// Token: 0x04006264 RID: 25188
		private static readonly Type declaringType = typeof(BasicConfigurator);
	}
}
