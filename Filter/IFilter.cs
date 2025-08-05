using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02002A3F RID: 10815
	[NullableContext(1)]
	public interface IFilter : IOptionHandler
	{
		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06009382 RID: 37762
		// (set) Token: 0x06009383 RID: 37763
		IFilter Next { get; set; }

		// Token: 0x06009384 RID: 37764
		FilterDecision Decide(LoggingEvent loggingEvent);
	}
}
