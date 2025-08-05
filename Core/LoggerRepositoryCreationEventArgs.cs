using System;
using System.Runtime.CompilerServices;
using log4net.Repository;

namespace log4net.Core
{
	// Token: 0x02002A64 RID: 10852
	[NullableContext(1)]
	[Nullable(0)]
	public class LoggerRepositoryCreationEventArgs : EventArgs
	{
		// Token: 0x06009476 RID: 38006 RVA: 0x00058B97 File Offset: 0x00056D97
		public LoggerRepositoryCreationEventArgs(ILoggerRepository repository)
		{
			this.LoggerRepository = repository;
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06009477 RID: 38007 RVA: 0x00058BA6 File Offset: 0x00056DA6
		public ILoggerRepository LoggerRepository { get; }
	}
}
