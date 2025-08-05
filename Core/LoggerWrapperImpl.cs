using System;
using System.Runtime.CompilerServices;

namespace log4net.Core
{
	// Token: 0x02002A66 RID: 10854
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LoggerWrapperImpl : ILoggerWrapper
	{
		// Token: 0x0600947C RID: 38012 RVA: 0x00058BAE File Offset: 0x00056DAE
		protected LoggerWrapperImpl(ILogger logger)
		{
			this.m_logger = logger;
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x0600947D RID: 38013 RVA: 0x00058BBD File Offset: 0x00056DBD
		public virtual ILogger Logger
		{
			get
			{
				return this.m_logger;
			}
		}

		// Token: 0x04006232 RID: 25138
		private readonly ILogger m_logger;
	}
}
