using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x02002A20 RID: 10784
	public class RawTimeStampLayout : IRawLayout
	{
		// Token: 0x06009320 RID: 37664 RVA: 0x00057EFA File Offset: 0x000560FA
		[NullableContext(1)]
		public virtual object Format(LoggingEvent loggingEvent)
		{
			return loggingEvent.TimeStamp;
		}
	}
}
