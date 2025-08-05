using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02002A41 RID: 10817
	[NullableContext(1)]
	[Nullable(0)]
	public class LevelRangeFilter : FilterSkeleton
	{
		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x0600938B RID: 37771 RVA: 0x0005825F File Offset: 0x0005645F
		// (set) Token: 0x0600938C RID: 37772 RVA: 0x00058267 File Offset: 0x00056467
		public bool AcceptOnMatch { get; set; } = true;

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x0600938D RID: 37773 RVA: 0x00058270 File Offset: 0x00056470
		// (set) Token: 0x0600938E RID: 37774 RVA: 0x00058278 File Offset: 0x00056478
		public Level LevelMin { get; set; }

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x0600938F RID: 37775 RVA: 0x00058281 File Offset: 0x00056481
		// (set) Token: 0x06009390 RID: 37776 RVA: 0x00058289 File Offset: 0x00056489
		public Level LevelMax { get; set; }

		// Token: 0x06009391 RID: 37777 RVA: 0x001421CC File Offset: 0x001403CC
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.LevelMin != null && loggingEvent.Level < this.LevelMin)
			{
				return FilterDecision.Deny;
			}
			if (this.LevelMax != null && loggingEvent.Level > this.LevelMax)
			{
				return FilterDecision.Deny;
			}
			if (this.AcceptOnMatch)
			{
				return FilterDecision.Accept;
			}
			return FilterDecision.Neutral;
		}
	}
}
