using System;
using System.Runtime.CompilerServices;

namespace log4net.Core
{
	// Token: 0x02002A6E RID: 10862
	public class TimeEvaluator : ITriggeringEventEvaluator
	{
		// Token: 0x060094E5 RID: 38117 RVA: 0x00059263 File Offset: 0x00057463
		public TimeEvaluator()
			: this(0)
		{
		}

		// Token: 0x060094E6 RID: 38118 RVA: 0x0005926C File Offset: 0x0005746C
		public TimeEvaluator(int interval)
		{
			this.Interval = interval;
			this.m_lasttime = DateTime.Now;
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x060094E7 RID: 38119 RVA: 0x00059286 File Offset: 0x00057486
		// (set) Token: 0x060094E8 RID: 38120 RVA: 0x0005928E File Offset: 0x0005748E
		public int Interval { get; set; }

		// Token: 0x060094E9 RID: 38121 RVA: 0x00144490 File Offset: 0x00142690
		[NullableContext(1)]
		public bool IsTriggeringEvent(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.Interval == 0)
			{
				return false;
			}
			bool flag;
			lock (this)
			{
				if (DateTime.Now.Subtract(this.m_lasttime).TotalSeconds > (double)this.Interval)
				{
					this.m_lasttime = DateTime.Now;
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0400625D RID: 25181
		private const int DEFAULT_INTERVAL = 0;

		// Token: 0x0400625E RID: 25182
		private DateTime m_lasttime;
	}
}
