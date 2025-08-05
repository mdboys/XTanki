using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Repository;

namespace log4net
{
	// Token: 0x020029B2 RID: 10674
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LogManager
	{
		// Token: 0x06008FD9 RID: 36825 RVA: 0x00005698 File Offset: 0x00003898
		private LogManager()
		{
		}

		// Token: 0x06008FDA RID: 36826 RVA: 0x00056149 File Offset: 0x00054349
		public static ILog Exists(string name)
		{
			return LogManager.Exists(Assembly.GetCallingAssembly(), name);
		}

		// Token: 0x06008FDB RID: 36827 RVA: 0x00056156 File Offset: 0x00054356
		public static ILog Exists(string repository, string name)
		{
			return LogManager.WrapLogger(LoggerManager.Exists(repository, name));
		}

		// Token: 0x06008FDC RID: 36828 RVA: 0x00056164 File Offset: 0x00054364
		public static ILog Exists(Assembly repositoryAssembly, string name)
		{
			return LogManager.WrapLogger(LoggerManager.Exists(repositoryAssembly, name));
		}

		// Token: 0x06008FDD RID: 36829 RVA: 0x00056172 File Offset: 0x00054372
		public static ILog[] GetCurrentLoggers()
		{
			return LogManager.GetCurrentLoggers(Assembly.GetCallingAssembly());
		}

		// Token: 0x06008FDE RID: 36830 RVA: 0x0005617E File Offset: 0x0005437E
		public static ILog[] GetCurrentLoggers(string repository)
		{
			return LogManager.WrapLoggers(LoggerManager.GetCurrentLoggers(repository));
		}

		// Token: 0x06008FDF RID: 36831 RVA: 0x0005618B File Offset: 0x0005438B
		public static ILog[] GetCurrentLoggers(Assembly repositoryAssembly)
		{
			return LogManager.WrapLoggers(LoggerManager.GetCurrentLoggers(repositoryAssembly));
		}

		// Token: 0x06008FE0 RID: 36832 RVA: 0x00056198 File Offset: 0x00054398
		public static ILog GetLogger(string name)
		{
			return LogManager.GetLogger(Assembly.GetCallingAssembly(), name);
		}

		// Token: 0x06008FE1 RID: 36833 RVA: 0x000561A5 File Offset: 0x000543A5
		public static ILog GetLogger(string repository, string name)
		{
			return LogManager.WrapLogger(LoggerManager.GetLogger(repository, name));
		}

		// Token: 0x06008FE2 RID: 36834 RVA: 0x000561B3 File Offset: 0x000543B3
		public static ILog GetLogger(Assembly repositoryAssembly, string name)
		{
			return LogManager.WrapLogger(LoggerManager.GetLogger(repositoryAssembly, name));
		}

		// Token: 0x06008FE3 RID: 36835 RVA: 0x000561C1 File Offset: 0x000543C1
		public static ILog GetLogger(Type type)
		{
			return LogManager.GetLogger(Assembly.GetCallingAssembly(), type.FullName);
		}

		// Token: 0x06008FE4 RID: 36836 RVA: 0x000561D3 File Offset: 0x000543D3
		public static ILog GetLogger(string repository, Type type)
		{
			return LogManager.WrapLogger(LoggerManager.GetLogger(repository, type));
		}

		// Token: 0x06008FE5 RID: 36837 RVA: 0x000561E1 File Offset: 0x000543E1
		public static ILog GetLogger(Assembly repositoryAssembly, Type type)
		{
			return LogManager.WrapLogger(LoggerManager.GetLogger(repositoryAssembly, type));
		}

		// Token: 0x06008FE6 RID: 36838 RVA: 0x000561EF File Offset: 0x000543EF
		public static void Shutdown()
		{
			LoggerManager.Shutdown();
		}

		// Token: 0x06008FE7 RID: 36839 RVA: 0x000561F6 File Offset: 0x000543F6
		public static void ShutdownRepository()
		{
			LogManager.ShutdownRepository(Assembly.GetCallingAssembly());
		}

		// Token: 0x06008FE8 RID: 36840 RVA: 0x00056202 File Offset: 0x00054402
		public static void ShutdownRepository(string repository)
		{
			LoggerManager.ShutdownRepository(repository);
		}

		// Token: 0x06008FE9 RID: 36841 RVA: 0x0005620A File Offset: 0x0005440A
		public static void ShutdownRepository(Assembly repositoryAssembly)
		{
			LoggerManager.ShutdownRepository(repositoryAssembly);
		}

		// Token: 0x06008FEA RID: 36842 RVA: 0x00056212 File Offset: 0x00054412
		public static void ResetConfiguration()
		{
			LogManager.ResetConfiguration(Assembly.GetCallingAssembly());
		}

		// Token: 0x06008FEB RID: 36843 RVA: 0x0005621E File Offset: 0x0005441E
		public static void ResetConfiguration(string repository)
		{
			LoggerManager.ResetConfiguration(repository);
		}

		// Token: 0x06008FEC RID: 36844 RVA: 0x00056226 File Offset: 0x00054426
		public static void ResetConfiguration(Assembly repositoryAssembly)
		{
			LoggerManager.ResetConfiguration(repositoryAssembly);
		}

		// Token: 0x06008FED RID: 36845 RVA: 0x0005622E File Offset: 0x0005442E
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository()
		{
			return LogManager.GetRepository(Assembly.GetCallingAssembly());
		}

		// Token: 0x06008FEE RID: 36846 RVA: 0x0005623A File Offset: 0x0005443A
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository(string repository)
		{
			return LogManager.GetRepository(repository);
		}

		// Token: 0x06008FEF RID: 36847 RVA: 0x00056242 File Offset: 0x00054442
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository(Assembly repositoryAssembly)
		{
			return LogManager.GetRepository(repositoryAssembly);
		}

		// Token: 0x06008FF0 RID: 36848 RVA: 0x0005622E File Offset: 0x0005442E
		public static ILoggerRepository GetRepository()
		{
			return LogManager.GetRepository(Assembly.GetCallingAssembly());
		}

		// Token: 0x06008FF1 RID: 36849 RVA: 0x0005624A File Offset: 0x0005444A
		public static ILoggerRepository GetRepository(string repository)
		{
			return LoggerManager.GetRepository(repository);
		}

		// Token: 0x06008FF2 RID: 36850 RVA: 0x00056252 File Offset: 0x00054452
		public static ILoggerRepository GetRepository(Assembly repositoryAssembly)
		{
			return LoggerManager.GetRepository(repositoryAssembly);
		}

		// Token: 0x06008FF3 RID: 36851 RVA: 0x0005625A File Offset: 0x0005445A
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(Type repositoryType)
		{
			return LogManager.CreateRepository(Assembly.GetCallingAssembly(), repositoryType);
		}

		// Token: 0x06008FF4 RID: 36852 RVA: 0x0005625A File Offset: 0x0005445A
		public static ILoggerRepository CreateRepository(Type repositoryType)
		{
			return LogManager.CreateRepository(Assembly.GetCallingAssembly(), repositoryType);
		}

		// Token: 0x06008FF5 RID: 36853 RVA: 0x00056267 File Offset: 0x00054467
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(string repository)
		{
			return LoggerManager.CreateRepository(repository);
		}

		// Token: 0x06008FF6 RID: 36854 RVA: 0x00056267 File Offset: 0x00054467
		public static ILoggerRepository CreateRepository(string repository)
		{
			return LoggerManager.CreateRepository(repository);
		}

		// Token: 0x06008FF7 RID: 36855 RVA: 0x0005626F File Offset: 0x0005446F
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(string repository, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repository, repositoryType);
		}

		// Token: 0x06008FF8 RID: 36856 RVA: 0x0005626F File Offset: 0x0005446F
		public static ILoggerRepository CreateRepository(string repository, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repository, repositoryType);
		}

		// Token: 0x06008FF9 RID: 36857 RVA: 0x00056278 File Offset: 0x00054478
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(Assembly repositoryAssembly, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repositoryAssembly, repositoryType);
		}

		// Token: 0x06008FFA RID: 36858 RVA: 0x00056278 File Offset: 0x00054478
		public static ILoggerRepository CreateRepository(Assembly repositoryAssembly, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repositoryAssembly, repositoryType);
		}

		// Token: 0x06008FFB RID: 36859 RVA: 0x00056281 File Offset: 0x00054481
		public static ILoggerRepository[] GetAllRepositories()
		{
			return LoggerManager.GetAllRepositories();
		}

		// Token: 0x06008FFC RID: 36860 RVA: 0x00056288 File Offset: 0x00054488
		private static ILog WrapLogger(ILogger logger)
		{
			return (ILog)LogManager.s_wrapperMap.GetWrapper(logger);
		}

		// Token: 0x06008FFD RID: 36861 RVA: 0x0013B154 File Offset: 0x00139354
		private static ILog[] WrapLoggers(ILogger[] loggers)
		{
			ILog[] array = new ILog[loggers.Length];
			for (int i = 0; i < loggers.Length; i++)
			{
				array[i] = LogManager.WrapLogger(loggers[i]);
			}
			return array;
		}

		// Token: 0x06008FFE RID: 36862 RVA: 0x0005629A File Offset: 0x0005449A
		private static ILoggerWrapper WrapperCreationHandler(ILogger logger)
		{
			return new LogImpl(logger);
		}

		// Token: 0x040060C8 RID: 24776
		private static readonly WrapperMap s_wrapperMap = new WrapperMap(new WrapperCreationHandler(LogManager.WrapperCreationHandler));
	}
}
