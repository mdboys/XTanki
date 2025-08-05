using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x02002A21 RID: 10785
	public class RawUtcTimeStampLayout : IRawLayout
	{
		// Token: 0x06009322 RID: 37666 RVA: 0x001413D8 File Offset: 0x0013F5D8
		[NullableContext(1)]
		public virtual object Format(LoggingEvent loggingEvent)
		{
			return loggingEvent.TimeStamp.ToUniversalTime();
		}
	}
}
