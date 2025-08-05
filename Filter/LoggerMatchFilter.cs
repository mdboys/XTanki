using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02002A42 RID: 10818
	[NullableContext(1)]
	[Nullable(0)]
	public class LoggerMatchFilter : FilterSkeleton
	{
		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06009393 RID: 37779 RVA: 0x000582A1 File Offset: 0x000564A1
		// (set) Token: 0x06009394 RID: 37780 RVA: 0x000582A9 File Offset: 0x000564A9
		public bool AcceptOnMatch { get; set; } = true;

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06009395 RID: 37781 RVA: 0x000582B2 File Offset: 0x000564B2
		// (set) Token: 0x06009396 RID: 37782 RVA: 0x000582BA File Offset: 0x000564BA
		public string LoggerToMatch { get; set; }

		// Token: 0x06009397 RID: 37783 RVA: 0x00142238 File Offset: 0x00140438
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.LoggerToMatch == null || this.LoggerToMatch.Length == 0 || !loggingEvent.LoggerName.StartsWith(this.LoggerToMatch))
			{
				return FilterDecision.Neutral;
			}
			if (this.AcceptOnMatch)
			{
				return FilterDecision.Accept;
			}
			return FilterDecision.Deny;
		}
	}
}
