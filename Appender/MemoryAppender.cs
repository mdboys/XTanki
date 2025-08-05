using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02002A96 RID: 10902
	[NullableContext(1)]
	[Nullable(0)]
	public class MemoryAppender : AppenderSkeleton
	{
		// Token: 0x0600963F RID: 38463 RVA: 0x00059E82 File Offset: 0x00058082
		public MemoryAppender()
		{
			this.m_eventsList = new ArrayList();
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06009640 RID: 38464 RVA: 0x00059EA0 File Offset: 0x000580A0
		// (set) Token: 0x06009641 RID: 38465 RVA: 0x00059EAF File Offset: 0x000580AF
		[Obsolete("Use Fix property")]
		public virtual bool OnlyFixPartialEventData
		{
			get
			{
				return this.Fix == FixFlags.Partial;
			}
			set
			{
				if (value)
				{
					this.Fix = FixFlags.Partial;
					return;
				}
				this.Fix = FixFlags.All;
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06009642 RID: 38466 RVA: 0x00059ECB File Offset: 0x000580CB
		// (set) Token: 0x06009643 RID: 38467 RVA: 0x00059ED3 File Offset: 0x000580D3
		public virtual FixFlags Fix
		{
			get
			{
				return this.m_fixFlags;
			}
			set
			{
				this.m_fixFlags = value;
			}
		}

		// Token: 0x06009644 RID: 38468 RVA: 0x00146DBC File Offset: 0x00144FBC
		public virtual LoggingEvent[] GetEvents()
		{
			object syncRoot = this.m_eventsList.SyncRoot;
			LoggingEvent[] array;
			lock (syncRoot)
			{
				array = (LoggingEvent[])this.m_eventsList.ToArray(typeof(LoggingEvent));
			}
			return array;
		}

		// Token: 0x06009645 RID: 38469 RVA: 0x00146E10 File Offset: 0x00145010
		protected override void Append(LoggingEvent loggingEvent)
		{
			loggingEvent.Fix = this.Fix;
			object syncRoot = this.m_eventsList.SyncRoot;
			lock (syncRoot)
			{
				this.m_eventsList.Add(loggingEvent);
			}
		}

		// Token: 0x06009646 RID: 38470 RVA: 0x00146E64 File Offset: 0x00145064
		public virtual void Clear()
		{
			object syncRoot = this.m_eventsList.SyncRoot;
			lock (syncRoot)
			{
				this.m_eventsList.Clear();
			}
		}

		// Token: 0x040062EC RID: 25324
		protected ArrayList m_eventsList;

		// Token: 0x040062ED RID: 25325
		protected FixFlags m_fixFlags = FixFlags.All;
	}
}
