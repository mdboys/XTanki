using System;
using System.Runtime.CompilerServices;
using log4net.Repository;

namespace log4net.Plugin
{
	// Token: 0x02002A13 RID: 10771
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PluginSkeleton : IPlugin
	{
		// Token: 0x060092DC RID: 37596 RVA: 0x00057C59 File Offset: 0x00055E59
		protected PluginSkeleton(string name)
		{
			this.m_name = name;
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x060092DD RID: 37597 RVA: 0x00057C68 File Offset: 0x00055E68
		// (set) Token: 0x060092DE RID: 37598 RVA: 0x00057C70 File Offset: 0x00055E70
		protected virtual ILoggerRepository LoggerRepository
		{
			get
			{
				return this.m_repository;
			}
			set
			{
				this.m_repository = value;
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x060092DF RID: 37599 RVA: 0x00057C79 File Offset: 0x00055E79
		// (set) Token: 0x060092E0 RID: 37600 RVA: 0x00057C81 File Offset: 0x00055E81
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x060092E1 RID: 37601 RVA: 0x00057C70 File Offset: 0x00055E70
		public virtual void Attach(ILoggerRepository repository)
		{
			this.m_repository = repository;
		}

		// Token: 0x060092E2 RID: 37602 RVA: 0x0000568E File Offset: 0x0000388E
		public virtual void Shutdown()
		{
		}

		// Token: 0x04006190 RID: 24976
		private string m_name;

		// Token: 0x04006191 RID: 24977
		private ILoggerRepository m_repository;
	}
}
