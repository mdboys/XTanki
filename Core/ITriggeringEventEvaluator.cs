using System;
using System.Runtime.CompilerServices;

namespace log4net.Core
{
	// Token: 0x02002A57 RID: 10839
	[NullableContext(1)]
	public interface ITriggeringEventEvaluator
	{
		// Token: 0x060093E0 RID: 37856
		bool IsTriggeringEvent(LoggingEvent loggingEvent);
	}
}
