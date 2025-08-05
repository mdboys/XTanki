using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Repository;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A4C RID: 10828
	[NullableContext(1)]
	[Nullable(0)]
	public class CompactRepositorySelector : IRepositorySelector
	{
		// Token: 0x060093B3 RID: 37811 RVA: 0x00142650 File Offset: 0x00140850
		public CompactRepositorySelector(Type defaultRepositoryType)
		{
			if (defaultRepositoryType == null)
			{
				throw new ArgumentNullException("defaultRepositoryType");
			}
			if (!typeof(ILoggerRepository).IsAssignableFrom(defaultRepositoryType))
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("defaultRepositoryType", defaultRepositoryType, string.Format("Parameter: defaultRepositoryType, Value: [{0}] out of range. Argument must implement the ILoggerRepository interface", defaultRepositoryType));
			}
			this.m_defaultRepositoryType = defaultRepositoryType;
			LogLog.Debug(CompactRepositorySelector.declaringType, string.Format("defaultRepositoryType [{0}]", this.m_defaultRepositoryType));
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060093B4 RID: 37812 RVA: 0x000583BE File Offset: 0x000565BE
		// (remove) Token: 0x060093B5 RID: 37813 RVA: 0x000583C7 File Offset: 0x000565C7
		public event LoggerRepositoryCreationEventHandler LoggerRepositoryCreatedEvent
		{
			add
			{
				this.m_loggerRepositoryCreatedEvent += value;
			}
			remove
			{
				this.m_loggerRepositoryCreatedEvent -= value;
			}
		}

		// Token: 0x060093B6 RID: 37814 RVA: 0x000583D0 File Offset: 0x000565D0
		public ILoggerRepository GetRepository(Assembly assembly)
		{
			return this.CreateRepository(assembly, this.m_defaultRepositoryType);
		}

		// Token: 0x060093B7 RID: 37815 RVA: 0x001426C8 File Offset: 0x001408C8
		public ILoggerRepository GetRepository(string repositoryName)
		{
			if (repositoryName == null)
			{
				throw new ArgumentNullException("repositoryName");
			}
			ILoggerRepository loggerRepository2;
			lock (this)
			{
				ILoggerRepository loggerRepository = this.m_name2repositoryMap[repositoryName] as ILoggerRepository;
				if (loggerRepository == null)
				{
					throw new LogException("Repository [" + repositoryName + "] is NOT defined.");
				}
				loggerRepository2 = loggerRepository;
			}
			return loggerRepository2;
		}

		// Token: 0x060093B8 RID: 37816 RVA: 0x00142730 File Offset: 0x00140930
		public ILoggerRepository CreateRepository(Assembly assembly, Type repositoryType)
		{
			if (repositoryType == null)
			{
				repositoryType = this.m_defaultRepositoryType;
			}
			ILoggerRepository loggerRepository2;
			lock (this)
			{
				ILoggerRepository loggerRepository = this.m_name2repositoryMap["log4net-default-repository"] as ILoggerRepository;
				if (loggerRepository == null)
				{
					loggerRepository = this.CreateRepository("log4net-default-repository", repositoryType);
				}
				loggerRepository2 = loggerRepository;
			}
			return loggerRepository2;
		}

		// Token: 0x060093B9 RID: 37817 RVA: 0x00142794 File Offset: 0x00140994
		public ILoggerRepository CreateRepository(string repositoryName, Type repositoryType)
		{
			if (repositoryName == null)
			{
				throw new ArgumentNullException("repositoryName");
			}
			if (repositoryType == null)
			{
				repositoryType = this.m_defaultRepositoryType;
			}
			ILoggerRepository loggerRepository2;
			lock (this)
			{
				if (this.m_name2repositoryMap[repositoryName] is ILoggerRepository)
				{
					throw new LogException("Repository [" + repositoryName + "] is already defined. Repositories cannot be redefined.");
				}
				LogLog.Debug(CompactRepositorySelector.declaringType, string.Format("Creating repository [{0}] using type [{1}]", repositoryName, repositoryType));
				ILoggerRepository loggerRepository = (ILoggerRepository)Activator.CreateInstance(repositoryType);
				loggerRepository.Name = repositoryName;
				this.m_name2repositoryMap[repositoryName] = loggerRepository;
				this.OnLoggerRepositoryCreatedEvent(loggerRepository);
				loggerRepository2 = loggerRepository;
			}
			return loggerRepository2;
		}

		// Token: 0x060093BA RID: 37818 RVA: 0x00142848 File Offset: 0x00140A48
		public bool ExistsRepository(string repositoryName)
		{
			bool flag;
			lock (this)
			{
				flag = this.m_name2repositoryMap.ContainsKey(repositoryName);
			}
			return flag;
		}

		// Token: 0x060093BB RID: 37819 RVA: 0x00142884 File Offset: 0x00140A84
		public ILoggerRepository[] GetAllRepositories()
		{
			ILoggerRepository[] array2;
			lock (this)
			{
				ICollection values = this.m_name2repositoryMap.Values;
				ILoggerRepository[] array = new ILoggerRepository[values.Count];
				values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060093BC RID: 37820 RVA: 0x001428D4 File Offset: 0x00140AD4
		// (remove) Token: 0x060093BD RID: 37821 RVA: 0x0014290C File Offset: 0x00140B0C
		private event LoggerRepositoryCreationEventHandler m_loggerRepositoryCreatedEvent;

		// Token: 0x060093BE RID: 37822 RVA: 0x000583DF File Offset: 0x000565DF
		protected virtual void OnLoggerRepositoryCreatedEvent(ILoggerRepository repository)
		{
			LoggerRepositoryCreationEventHandler loggerRepositoryCreatedEvent = this.m_loggerRepositoryCreatedEvent;
			if (loggerRepositoryCreatedEvent == null)
			{
				return;
			}
			loggerRepositoryCreatedEvent(this, new LoggerRepositoryCreationEventArgs(repository));
		}

		// Token: 0x040061E7 RID: 25063
		private const string DefaultRepositoryName = "log4net-default-repository";

		// Token: 0x040061E8 RID: 25064
		private static readonly Type declaringType = typeof(CompactRepositorySelector);

		// Token: 0x040061E9 RID: 25065
		private readonly Type m_defaultRepositoryType;

		// Token: 0x040061EA RID: 25066
		private readonly Hashtable m_name2repositoryMap = new Hashtable();
	}
}
