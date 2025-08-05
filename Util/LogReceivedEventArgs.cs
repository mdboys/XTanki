using System;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029C4 RID: 10692
	[NullableContext(1)]
	[Nullable(0)]
	public class LogReceivedEventArgs : EventArgs
	{
		// Token: 0x0600908A RID: 37002 RVA: 0x0005688E File Offset: 0x00054A8E
		public LogReceivedEventArgs(LogLog loglog)
		{
			this.LogLog = loglog;
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x0600908B RID: 37003 RVA: 0x0005689D File Offset: 0x00054A9D
		public LogLog LogLog { get; }
	}
}
