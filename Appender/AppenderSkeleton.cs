using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A84 RID: 10884
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AppenderSkeleton : IBulkAppender, IAppender, IOptionHandler
	{
		// Token: 0x06009592 RID: 38290 RVA: 0x000598B9 File Offset: 0x00057AB9
		protected AppenderSkeleton()
		{
			this.m_errorHandler = new OnlyOnceErrorHandler(base.GetType().Name);
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06009593 RID: 38291 RVA: 0x000598D7 File Offset: 0x00057AD7
		// (set) Token: 0x06009594 RID: 38292 RVA: 0x000598DF File Offset: 0x00057ADF
		public Level Threshold { get; set; }

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06009595 RID: 38293 RVA: 0x000598E8 File Offset: 0x00057AE8
		// (set) Token: 0x06009596 RID: 38294 RVA: 0x001459AC File Offset: 0x00143BAC
		public virtual IErrorHandler ErrorHandler
		{
			get
			{
				return this.m_errorHandler;
			}
			set
			{
				lock (this)
				{
					if (value == null)
					{
						LogLog.Warn(AppenderSkeleton.declaringType, "You have tried to set a null error-handler.");
					}
					else
					{
						this.m_errorHandler = value;
					}
				}
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x06009597 RID: 38295 RVA: 0x000598F0 File Offset: 0x00057AF0
		public virtual IFilter FilterHead
		{
			get
			{
				return this.m_headFilter;
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x06009598 RID: 38296 RVA: 0x000598F8 File Offset: 0x00057AF8
		// (set) Token: 0x06009599 RID: 38297 RVA: 0x00059900 File Offset: 0x00057B00
		public virtual ILayout Layout
		{
			get
			{
				return this.m_layout;
			}
			set
			{
				this.m_layout = value;
			}
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x0600959A RID: 38298 RVA: 0x00007F86 File Offset: 0x00006186
		protected virtual bool RequiresLayout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x0600959B RID: 38299 RVA: 0x00059909 File Offset: 0x00057B09
		// (set) Token: 0x0600959C RID: 38300 RVA: 0x00059911 File Offset: 0x00057B11
		public string Name { get; set; }

		// Token: 0x0600959D RID: 38301 RVA: 0x001459F8 File Offset: 0x00143BF8
		public void Close()
		{
			lock (this)
			{
				if (!this.m_closed)
				{
					this.OnClose();
					this.m_closed = true;
				}
			}
		}

		// Token: 0x0600959E RID: 38302 RVA: 0x00145A3C File Offset: 0x00143C3C
		public void DoAppend(LoggingEvent loggingEvent)
		{
			lock (this)
			{
				if (this.m_closed)
				{
					this.ErrorHandler.Error("Attempted to append to closed appender named [" + this.Name + "].");
				}
				else if (!this.m_recursiveGuard)
				{
					try
					{
						this.m_recursiveGuard = true;
						if (this.FilterEvent(loggingEvent) && this.PreAppendCheck())
						{
							this.Append(loggingEvent);
						}
					}
					catch (Exception ex)
					{
						this.ErrorHandler.Error("Failed in DoAppend", ex);
					}
					finally
					{
						this.m_recursiveGuard = false;
					}
				}
			}
		}

		// Token: 0x0600959F RID: 38303 RVA: 0x00145AF4 File Offset: 0x00143CF4
		public void DoAppend(LoggingEvent[] loggingEvents)
		{
			lock (this)
			{
				if (this.m_closed)
				{
					this.ErrorHandler.Error("Attempted to append to closed appender named [" + this.Name + "].");
				}
				else if (!this.m_recursiveGuard)
				{
					try
					{
						this.m_recursiveGuard = true;
						ArrayList arrayList = new ArrayList(loggingEvents.Length);
						foreach (LoggingEvent loggingEvent in loggingEvents)
						{
							if (this.FilterEvent(loggingEvent))
							{
								arrayList.Add(loggingEvent);
							}
						}
						if (arrayList.Count > 0 && this.PreAppendCheck())
						{
							this.Append((LoggingEvent[])arrayList.ToArray(typeof(LoggingEvent)));
						}
					}
					catch (Exception ex)
					{
						this.ErrorHandler.Error("Failed in Bulk DoAppend", ex);
					}
					finally
					{
						this.m_recursiveGuard = false;
					}
				}
			}
		}

		// Token: 0x060095A0 RID: 38304 RVA: 0x0000568E File Offset: 0x0000388E
		public virtual void ActivateOptions()
		{
		}

		// Token: 0x060095A1 RID: 38305 RVA: 0x00145BF8 File Offset: 0x00143DF8
		~AppenderSkeleton()
		{
			if (!this.m_closed)
			{
				LogLog.Debug(AppenderSkeleton.declaringType, "Finalizing appender named [" + this.Name + "].");
				this.Close();
			}
		}

		// Token: 0x060095A2 RID: 38306 RVA: 0x00145C4C File Offset: 0x00143E4C
		protected virtual bool FilterEvent(LoggingEvent loggingEvent)
		{
			if (!this.IsAsSevereAsThreshold(loggingEvent.Level))
			{
				return false;
			}
			IFilter filter = this.FilterHead;
			while (filter != null)
			{
				switch (filter.Decide(loggingEvent))
				{
				case FilterDecision.Deny:
					return false;
				case FilterDecision.Neutral:
					filter = filter.Next;
					break;
				case FilterDecision.Accept:
					filter = null;
					break;
				}
			}
			return true;
		}

		// Token: 0x060095A3 RID: 38307 RVA: 0x00145CA4 File Offset: 0x00143EA4
		public virtual void AddFilter(IFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter param must not be null");
			}
			if (this.m_headFilter == null)
			{
				this.m_tailFilter = filter;
				this.m_headFilter = filter;
				return;
			}
			this.m_tailFilter.Next = filter;
			this.m_tailFilter = filter;
		}

		// Token: 0x060095A4 RID: 38308 RVA: 0x00145CEC File Offset: 0x00143EEC
		public virtual void ClearFilters()
		{
			this.m_headFilter = (this.m_tailFilter = null);
		}

		// Token: 0x060095A5 RID: 38309 RVA: 0x0005991A File Offset: 0x00057B1A
		protected virtual bool IsAsSevereAsThreshold(Level level)
		{
			return this.Threshold == null || level >= this.Threshold;
		}

		// Token: 0x060095A6 RID: 38310 RVA: 0x0000568E File Offset: 0x0000388E
		protected virtual void OnClose()
		{
		}

		// Token: 0x060095A7 RID: 38311
		protected abstract void Append(LoggingEvent loggingEvent);

		// Token: 0x060095A8 RID: 38312 RVA: 0x00145D0C File Offset: 0x00143F0C
		protected virtual void Append(LoggingEvent[] loggingEvents)
		{
			foreach (LoggingEvent loggingEvent in loggingEvents)
			{
				this.Append(loggingEvent);
			}
		}

		// Token: 0x060095A9 RID: 38313 RVA: 0x00059938 File Offset: 0x00057B38
		protected virtual bool PreAppendCheck()
		{
			if (this.m_layout == null && this.RequiresLayout)
			{
				this.ErrorHandler.Error("AppenderSkeleton: No layout set for the appender named [" + this.Name + "].");
				return false;
			}
			return true;
		}

		// Token: 0x060095AA RID: 38314 RVA: 0x00145D34 File Offset: 0x00143F34
		protected string RenderLoggingEvent(LoggingEvent loggingEvent)
		{
			if (this.m_renderWriter == null)
			{
				this.m_renderWriter = new ReusableStringWriter(CultureInfo.InvariantCulture);
			}
			ReusableStringWriter renderWriter = this.m_renderWriter;
			string text;
			lock (renderWriter)
			{
				this.m_renderWriter.Reset(1024, 256);
				this.RenderLoggingEvent(this.m_renderWriter, loggingEvent);
				text = this.m_renderWriter.ToString();
			}
			return text;
		}

		// Token: 0x060095AB RID: 38315 RVA: 0x00145DB0 File Offset: 0x00143FB0
		protected void RenderLoggingEvent(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (this.m_layout == null)
			{
				throw new InvalidOperationException("A layout must be set");
			}
			if (!this.m_layout.IgnoresException)
			{
				this.m_layout.Format(writer, loggingEvent);
				return;
			}
			string exceptionString = loggingEvent.GetExceptionString();
			if (exceptionString != null && exceptionString.Length > 0)
			{
				this.m_layout.Format(writer, loggingEvent);
				writer.WriteLine(exceptionString);
				return;
			}
			this.m_layout.Format(writer, loggingEvent);
		}

		// Token: 0x04006299 RID: 25241
		private const int c_renderBufferSize = 256;

		// Token: 0x0400629A RID: 25242
		private const int c_renderBufferMaxCapacity = 1024;

		// Token: 0x0400629B RID: 25243
		private static readonly Type declaringType = typeof(AppenderSkeleton);

		// Token: 0x0400629C RID: 25244
		private bool m_closed;

		// Token: 0x0400629D RID: 25245
		private IErrorHandler m_errorHandler;

		// Token: 0x0400629E RID: 25246
		private IFilter m_headFilter;

		// Token: 0x0400629F RID: 25247
		private ILayout m_layout;

		// Token: 0x040062A0 RID: 25248
		private bool m_recursiveGuard;

		// Token: 0x040062A1 RID: 25249
		private ReusableStringWriter m_renderWriter;

		// Token: 0x040062A2 RID: 25250
		private IFilter m_tailFilter;
	}
}
