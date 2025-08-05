using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02002A90 RID: 10896
	[NullableContext(1)]
	public interface IAppender
	{
		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x06009628 RID: 38440
		// (set) Token: 0x06009629 RID: 38441
		string Name { get; set; }

		// Token: 0x0600962A RID: 38442
		void Close();

		// Token: 0x0600962B RID: 38443
		void DoAppend(LoggingEvent loggingEvent);
	}
}
