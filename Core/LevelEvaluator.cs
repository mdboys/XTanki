using System;
using System.Runtime.CompilerServices;

namespace log4net.Core
{
	// Token: 0x02002A5E RID: 10846
	[NullableContext(1)]
	[Nullable(0)]
	public class LevelEvaluator : ITriggeringEventEvaluator
	{
		// Token: 0x0600943C RID: 37948 RVA: 0x00058824 File Offset: 0x00056A24
		public LevelEvaluator()
			: this(Level.Off)
		{
		}

		// Token: 0x0600943D RID: 37949 RVA: 0x00058831 File Offset: 0x00056A31
		public LevelEvaluator(Level threshold)
		{
			if (threshold == null)
			{
				throw new ArgumentNullException("threshold");
			}
			this.Threshold = threshold;
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x0600943E RID: 37950 RVA: 0x00058854 File Offset: 0x00056A54
		// (set) Token: 0x0600943F RID: 37951 RVA: 0x0005885C File Offset: 0x00056A5C
		public Level Threshold { get; set; }

		// Token: 0x06009440 RID: 37952 RVA: 0x00058865 File Offset: 0x00056A65
		public bool IsTriggeringEvent(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			return loggingEvent.Level >= this.Threshold;
		}
	}
}
