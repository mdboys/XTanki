using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02002A40 RID: 10816
	[NullableContext(1)]
	[Nullable(0)]
	public class LevelMatchFilter : FilterSkeleton
	{
		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06009385 RID: 37765 RVA: 0x000581F0 File Offset: 0x000563F0
		// (set) Token: 0x06009386 RID: 37766 RVA: 0x000581F8 File Offset: 0x000563F8
		public bool AcceptOnMatch { get; set; } = true;

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06009387 RID: 37767 RVA: 0x00058201 File Offset: 0x00056401
		// (set) Token: 0x06009388 RID: 37768 RVA: 0x00058209 File Offset: 0x00056409
		public Level LevelToMatch { get; set; }

		// Token: 0x06009389 RID: 37769 RVA: 0x00058212 File Offset: 0x00056412
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (!(this.LevelToMatch != null) || !(this.LevelToMatch == loggingEvent.Level))
			{
				return FilterDecision.Neutral;
			}
			if (!this.AcceptOnMatch)
			{
				return FilterDecision.Deny;
			}
			return FilterDecision.Accept;
		}
	}
}
