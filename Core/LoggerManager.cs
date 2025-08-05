using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A62 RID: 10850
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LoggerManager
	{
		// Token: 0x06009456 RID: 37974 RVA: 0x0014350C File Offset: 0x0014170C
		static LoggerManager()
		{
			try
			{
				LoggerManager.RegisterAppDomainEvents();
			}
			catch (SecurityException)
			{
				LogLog.Debug(LoggerManager.declaringType, "Security Exception (ControlAppDomain LinkDemand) while trying to register Shutdown handler with the AppDomain. LoggerManager.Shutdown() will not be called automatically when the AppDomain exits. It must be called programmatically.");
			}
			LogLog.Debug(LoggerManager.declaringType, LoggerManager.GetVersionInfo());
			LoggerManager.RepositorySelector = new CompactRepositorySelector(typeof(Hierarchy));
		}

		// Token: 0x06009457 RID: 37975 RVA: 0x00005698 File Offset: 0x00003898
		private LoggerManager()
		{
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06009458 RID: 37976 RVA: 0x000588F2 File Offset: 0x00056AF2
		// (set) Token: 0x06009459 RID: 37977 RVA: 0x000588F9 File Offset: 0x00056AF9
		public static IRepositorySelector RepositorySelector { get; set; }

		// Token: 0x0600945A RID: 37978 RVA: 0x00143574 File Offset: 0x00141774
		private static void RegisterAppDomainEvents()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			EventHandler eventHandler;
			if ((eventHandler = LoggerManager.<>O.<0>__OnProcessExit) == null)
			{
				eventHandler = (LoggerManager.<>O.<0>__OnProcessExit = new EventHandler(LoggerManager.OnProcessExit));
			}
			currentDomain.ProcessExit += eventHandler;
			AppDomain currentDomain2 = AppDomain.CurrentDomain;
			EventHandler eventHandler2;
			if ((eventHandler2 = LoggerManager.<>O.<1>__OnDomainUnload) == null)
			{
				eventHandler2 = (LoggerManager.<>O.<1>__OnDomainUnload = new EventHandler(LoggerManager.OnDomainUnload));
			}
			currentDomain2.DomainUnload += eventHandler2;
		}

		// Token: 0x0600945B RID: 37979 RVA: 0x0005624A File Offset: 0x0005444A
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository(string repository)
		{
			return LoggerManager.GetRepository(repository);
		}

		// Token: 0x0600945C RID: 37980 RVA: 0x00056252 File Offset: 0x00054452
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository(Assembly repositoryAssembly)
		{
			return LoggerManager.GetRepository(repositoryAssembly);
		}

		// Token: 0x0600945D RID: 37981 RVA: 0x00058901 File Offset: 0x00056B01
		public static ILoggerRepository GetRepository(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository);
		}

		// Token: 0x0600945E RID: 37982 RVA: 0x0005891C File Offset: 0x00056B1C
		public static ILoggerRepository GetRepository(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly);
		}

		// Token: 0x0600945F RID: 37983 RVA: 0x00058937 File Offset: 0x00056B37
		public static ILogger Exists(string repository, string name)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository).Exists(name);
		}

		// Token: 0x06009460 RID: 37984 RVA: 0x00058966 File Offset: 0x00056B66
		public static ILogger Exists(Assembly repositoryAssembly, string name)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).Exists(name);
		}

		// Token: 0x06009461 RID: 37985 RVA: 0x00058995 File Offset: 0x00056B95
		public static ILogger[] GetCurrentLoggers(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository).GetCurrentLoggers();
		}

		// Token: 0x06009462 RID: 37986 RVA: 0x000589B5 File Offset: 0x00056BB5
		public static ILogger[] GetCurrentLoggers(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).GetCurrentLoggers();
		}

		// Token: 0x06009463 RID: 37987 RVA: 0x000589D5 File Offset: 0x00056BD5
		public static ILogger GetLogger(string repository, string name)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository).GetLogger(name);
		}

		// Token: 0x06009464 RID: 37988 RVA: 0x00058A04 File Offset: 0x00056C04
		public static ILogger GetLogger(Assembly repositoryAssembly, string name)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).GetLogger(name);
		}

		// Token: 0x06009465 RID: 37989 RVA: 0x00058A33 File Offset: 0x00056C33
		public static ILogger GetLogger(string repository, Type type)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository).GetLogger(type.FullName);
		}

		// Token: 0x06009466 RID: 37990 RVA: 0x00058A67 File Offset: 0x00056C67
		public static ILogger GetLogger(Assembly repositoryAssembly, Type type)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).GetLogger(type.FullName);
		}

		// Token: 0x06009467 RID: 37991 RVA: 0x001435CC File Offset: 0x001417CC
		public static void Shutdown()
		{
			ILoggerRepository[] allRepositories = LoggerManager.GetAllRepositories();
			for (int i = 0; i < allRepositories.Length; i++)
			{
				allRepositories[i].Shutdown();
			}
		}

		// Token: 0x06009468 RID: 37992 RVA: 0x00058A9B File Offset: 0x00056C9B
		public static void ShutdownRepository(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			LoggerManager.RepositorySelector.GetRepository(repository).Shutdown();
		}

		// Token: 0x06009469 RID: 37993 RVA: 0x00058ABB File Offset: 0x00056CBB
		public static void ShutdownRepository(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).Shutdown();
		}

		// Token: 0x0600946A RID: 37994 RVA: 0x00058ADB File Offset: 0x00056CDB
		public static void ResetConfiguration(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			LoggerManager.RepositorySelector.GetRepository(repository).ResetConfiguration();
		}

		// Token: 0x0600946B RID: 37995 RVA: 0x00058AFB File Offset: 0x00056CFB
		public static void ResetConfiguration(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).ResetConfiguration();
		}

		// Token: 0x0600946C RID: 37996 RVA: 0x00056267 File Offset: 0x00054467
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(string repository)
		{
			return LoggerManager.CreateRepository(repository);
		}

		// Token: 0x0600946D RID: 37997 RVA: 0x00058B1B File Offset: 0x00056D1B
		public static ILoggerRepository CreateRepository(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			return LoggerManager.RepositorySelector.CreateRepository(repository, null);
		}

		// Token: 0x0600946E RID: 37998 RVA: 0x0005626F File Offset: 0x0005446F
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(string repository, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repository, repositoryType);
		}

		// Token: 0x0600946F RID: 37999 RVA: 0x00058B37 File Offset: 0x00056D37
		public static ILoggerRepository CreateRepository(string repository, Type repositoryType)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (repositoryType == null)
			{
				throw new ArgumentNullException("repositoryType");
			}
			return LoggerManager.RepositorySelector.CreateRepository(repository, repositoryType);
		}

		// Token: 0x06009470 RID: 38000 RVA: 0x00056278 File Offset: 0x00054478
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(Assembly repositoryAssembly, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repositoryAssembly, repositoryType);
		}

		// Token: 0x06009471 RID: 38001 RVA: 0x00058B61 File Offset: 0x00056D61
		public static ILoggerRepository CreateRepository(Assembly repositoryAssembly, Type repositoryType)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (repositoryType == null)
			{
				throw new ArgumentNullException("repositoryType");
			}
			return LoggerManager.RepositorySelector.CreateRepository(repositoryAssembly, repositoryType);
		}

		// Token: 0x06009472 RID: 38002 RVA: 0x00058B8B File Offset: 0x00056D8B
		public static ILoggerRepository[] GetAllRepositories()
		{
			return LoggerManager.RepositorySelector.GetAllRepositories();
		}

		// Token: 0x06009473 RID: 38003 RVA: 0x001435F8 File Offset: 0x001417F8
		private static string GetVersionInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			stringBuilder.Append("log4net assembly [").Append(executingAssembly.FullName).Append("]. ");
			stringBuilder.Append("Loaded from [").Append(SystemInfo.AssemblyLocationInfo(executingAssembly)).Append("]. ");
			stringBuilder.Append("(.NET Runtime [").Append(Environment.Version).Append("]");
			stringBuilder.Append(" on ").Append(Environment.OSVersion);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06009474 RID: 38004 RVA: 0x000561EF File Offset: 0x000543EF
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			LoggerManager.Shutdown();
		}

		// Token: 0x06009475 RID: 38005 RVA: 0x000561EF File Offset: 0x000543EF
		private static void OnProcessExit(object sender, EventArgs e)
		{
			LoggerManager.Shutdown();
		}

		// Token: 0x0400622D RID: 25133
		private static readonly Type declaringType = typeof(LoggerManager);

		// Token: 0x02002A63 RID: 10851
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400622F RID: 25135
			[Nullable(0)]
			public static EventHandler <0>__OnProcessExit;

			// Token: 0x04006230 RID: 25136
			[Nullable(0)]
			public static EventHandler <1>__OnDomainUnload;
		}
	}
}
