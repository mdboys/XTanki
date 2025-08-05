using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02002A3C RID: 10812
	public sealed class DenyAllFilter : FilterSkeleton
	{
		// Token: 0x0600937B RID: 37755 RVA: 0x00009C62 File Offset: 0x00007E62
		[NullableContext(1)]
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			return FilterDecision.Deny;
		}
	}
}
