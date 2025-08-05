using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A85 RID: 10885
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BufferingAppenderSkeleton : AppenderSkeleton
	{
		// Token: 0x060095AD RID: 38317 RVA: 0x0005997E File Offset: 0x00057B7E
		protected BufferingAppenderSkeleton()
			: this(true)
		{
		}

		// Token: 0x060095AE RID: 38318 RVA: 0x00059987 File Offset: 0x00057B87
		protected BufferingAppenderSkeleton(bool eventMustBeFixed)
		{
			this.m_eventMustBeFixed = eventMustBeFixed;
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x060095AF RID: 38319 RVA: 0x000599AC File Offset: 0x00057BAC
		// (set) Token: 0x060095B0 RID: 38320 RVA: 0x000599B4 File Offset: 0x00057BB4
		public bool Lossy { get; set; }

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x060095B1 RID: 38321 RVA: 0x000599BD File Offset: 0x00057BBD
		// (set) Token: 0x060095B2 RID: 38322 RVA: 0x000599C5 File Offset: 0x00057BC5
		public int BufferSize { get; set; } = 512;

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x060095B3 RID: 38323 RVA: 0x000599CE File Offset: 0x00057BCE
		// (set) Token: 0x060095B4 RID: 38324 RVA: 0x000599D6 File Offset: 0x00057BD6
		public ITriggeringEventEvaluator Evaluator { get; set; }

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x060095B5 RID: 38325 RVA: 0x000599DF File Offset: 0x00057BDF
		// (set) Token: 0x060095B6 RID: 38326 RVA: 0x000599E7 File Offset: 0x00057BE7
		public ITriggeringEventEvaluator LossyEvaluator { get; set; }

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x060095B7 RID: 38327 RVA: 0x000599F0 File Offset: 0x00057BF0
		// (set) Token: 0x060095B8 RID: 38328 RVA: 0x000599FF File Offset: 0x00057BFF
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

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x060095B9 RID: 38329 RVA: 0x00059A1B File Offset: 0x00057C1B
		// (set) Token: 0x060095BA RID: 38330 RVA: 0x00059A23 File Offset: 0x00057C23
		public virtual FixFlags Fix { get; set; } = FixFlags.All;

		// Token: 0x060095BB RID: 38331 RVA: 0x00059A2C File Offset: 0x00057C2C
		public virtual void Flush()
		{
			this.Flush(false);
		}

		// Token: 0x060095BC RID: 38332 RVA: 0x00145E20 File Offset: 0x00144020
		public virtual void Flush(bool flushLossyBuffer)
		{
			lock (this)
			{
				if (this.m_cb != null && this.m_cb.Length > 0)
				{
					if (this.Lossy)
					{
						if (flushLossyBuffer)
						{
							if (this.LossyEvaluator != null)
							{
								LoggingEvent[] array = this.m_cb.PopAll();
								ArrayList arrayList = new ArrayList(array.Length);
								foreach (LoggingEvent loggingEvent in array)
								{
									if (this.LossyEvaluator.IsTriggeringEvent(loggingEvent))
									{
										arrayList.Add(loggingEvent);
									}
								}
								if (arrayList.Count > 0)
								{
									this.SendBuffer((LoggingEvent[])arrayList.ToArray(typeof(LoggingEvent)));
								}
							}
							else
							{
								this.m_cb.Clear();
							}
						}
					}
					else
					{
						this.SendFromBuffer(null, this.m_cb);
					}
				}
			}
		}

		// Token: 0x060095BD RID: 38333 RVA: 0x00145F04 File Offset: 0x00144104
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.Lossy && this.Evaluator == null)
			{
				this.ErrorHandler.Error("Appender [" + base.Name + "] is Lossy but has no Evaluator. The buffer will never be sent!");
			}
			if (this.BufferSize > 1)
			{
				this.m_cb = new CyclicBuffer(this.BufferSize);
				return;
			}
			this.m_cb = null;
		}

		// Token: 0x060095BE RID: 38334 RVA: 0x00059A35 File Offset: 0x00057C35
		protected override void OnClose()
		{
			this.Flush(true);
		}

		// Token: 0x060095BF RID: 38335 RVA: 0x00145F6C File Offset: 0x0014416C
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_cb == null || this.BufferSize <= 1)
			{
				if (!this.Lossy || (this.Evaluator != null && this.Evaluator.IsTriggeringEvent(loggingEvent)) || (this.LossyEvaluator != null && this.LossyEvaluator.IsTriggeringEvent(loggingEvent)))
				{
					if (this.m_eventMustBeFixed)
					{
						loggingEvent.Fix = this.Fix;
					}
					this.SendBuffer(new LoggingEvent[] { loggingEvent });
				}
				return;
			}
			loggingEvent.Fix = this.Fix;
			LoggingEvent loggingEvent2 = this.m_cb.Append(loggingEvent);
			if (loggingEvent2 != null)
			{
				if (!this.Lossy)
				{
					this.SendFromBuffer(loggingEvent2, this.m_cb);
					return;
				}
				if (this.LossyEvaluator == null || !this.LossyEvaluator.IsTriggeringEvent(loggingEvent2))
				{
					loggingEvent2 = null;
				}
				if (this.Evaluator != null && this.Evaluator.IsTriggeringEvent(loggingEvent))
				{
					this.SendFromBuffer(loggingEvent2, this.m_cb);
					return;
				}
				if (loggingEvent2 != null)
				{
					this.SendBuffer(new LoggingEvent[] { loggingEvent2 });
					return;
				}
			}
			else if (this.Evaluator != null && this.Evaluator.IsTriggeringEvent(loggingEvent))
			{
				this.SendFromBuffer(null, this.m_cb);
			}
		}

		// Token: 0x060095C0 RID: 38336 RVA: 0x00146088 File Offset: 0x00144288
		protected virtual void SendFromBuffer(LoggingEvent firstLoggingEvent, CyclicBuffer buffer)
		{
			LoggingEvent[] array = buffer.PopAll();
			if (firstLoggingEvent == null)
			{
				this.SendBuffer(array);
				return;
			}
			if (array.Length == 0)
			{
				this.SendBuffer(new LoggingEvent[] { firstLoggingEvent });
				return;
			}
			LoggingEvent[] array2 = new LoggingEvent[array.Length + 1];
			Array.Copy(array, 0, array2, 1, array.Length);
			array2[0] = firstLoggingEvent;
			this.SendBuffer(array2);
		}

		// Token: 0x060095C1 RID: 38337
		protected abstract void SendBuffer(LoggingEvent[] events);

		// Token: 0x040062A5 RID: 25253
		private const int DEFAULT_BUFFER_SIZE = 512;

		// Token: 0x040062A6 RID: 25254
		private readonly bool m_eventMustBeFixed;

		// Token: 0x040062A7 RID: 25255
		private CyclicBuffer m_cb;
	}
}
