using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02002A91 RID: 10897
	public interface IBulkAppender : IAppender
	{
		// Token: 0x0600962C RID: 38444
		[NullableContext(1)]
		void DoAppend(LoggingEvent[] loggingEvents);
	}
}
