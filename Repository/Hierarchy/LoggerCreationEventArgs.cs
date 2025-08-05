using System;
using System.Runtime.CompilerServices;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x02002A04 RID: 10756
	[NullableContext(1)]
	[Nullable(0)]
	public class LoggerCreationEventArgs : EventArgs
	{
		// Token: 0x06009268 RID: 37480 RVA: 0x0005786F File Offset: 0x00055A6F
		public LoggerCreationEventArgs(Logger log)
		{
			this.Logger = log;
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06009269 RID: 37481 RVA: 0x0005787E File Offset: 0x00055A7E
		public Logger Logger { get; }
	}
}
