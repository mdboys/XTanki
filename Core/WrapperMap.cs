using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Repository;

namespace log4net.Core
{
	// Token: 0x02002A70 RID: 10864
	[NullableContext(1)]
	[Nullable(0)]
	public class WrapperMap
	{
		// Token: 0x060094EE RID: 38126 RVA: 0x00059297 File Offset: 0x00057497
		public WrapperMap(WrapperCreationHandler createWrapperHandler)
		{
			this.m_createWrapperHandler = createWrapperHandler;
			this.m_shutdownHandler = new LoggerRepositoryShutdownEventHandler(this.ILoggerRepository_Shutdown);
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x060094EF RID: 38127 RVA: 0x000592C3 File Offset: 0x000574C3
		protected Hashtable Repositories { get; } = new Hashtable();

		// Token: 0x060094F0 RID: 38128 RVA: 0x0014450C File Offset: 0x0014270C
		public virtual ILoggerWrapper GetWrapper(ILogger logger)
		{
			if (logger == null)
			{
				return null;
			}
			ILoggerWrapper loggerWrapper2;
			lock (this)
			{
				Hashtable hashtable = (Hashtable)this.Repositories[logger.Repository];
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					this.Repositories[logger.Repository] = hashtable;
					logger.Repository.ShutdownEvent += this.m_shutdownHandler;
				}
				ILoggerWrapper loggerWrapper = hashtable[logger] as ILoggerWrapper;
				if (loggerWrapper == null)
				{
					loggerWrapper = (ILoggerWrapper)(hashtable[logger] = this.CreateNewWrapperObject(logger));
				}
				loggerWrapper2 = loggerWrapper;
			}
			return loggerWrapper2;
		}

		// Token: 0x060094F1 RID: 38129 RVA: 0x000592CB File Offset: 0x000574CB
		protected virtual ILoggerWrapper CreateNewWrapperObject(ILogger logger)
		{
			WrapperCreationHandler createWrapperHandler = this.m_createWrapperHandler;
			if (createWrapperHandler == null)
			{
				return null;
			}
			return createWrapperHandler(logger);
		}

		// Token: 0x060094F2 RID: 38130 RVA: 0x001445B0 File Offset: 0x001427B0
		protected virtual void RepositoryShutdown(ILoggerRepository repository)
		{
			lock (this)
			{
				this.Repositories.Remove(repository);
				repository.ShutdownEvent -= this.m_shutdownHandler;
			}
		}

		// Token: 0x060094F3 RID: 38131 RVA: 0x001445F8 File Offset: 0x001427F8
		private void ILoggerRepository_Shutdown(object sender, EventArgs e)
		{
			ILoggerRepository loggerRepository = sender as ILoggerRepository;
			if (loggerRepository != null)
			{
				this.RepositoryShutdown(loggerRepository);
			}
		}

		// Token: 0x04006260 RID: 25184
		private readonly WrapperCreationHandler m_createWrapperHandler;

		// Token: 0x04006261 RID: 25185
		private readonly LoggerRepositoryShutdownEventHandler m_shutdownHandler;
	}
}
