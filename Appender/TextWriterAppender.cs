using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002AA0 RID: 10912
	[NullableContext(1)]
	[Nullable(0)]
	public class TextWriterAppender : AppenderSkeleton
	{
		// Token: 0x06009670 RID: 38512 RVA: 0x0005A06A File Offset: 0x0005826A
		public TextWriterAppender()
		{
		}

		// Token: 0x06009671 RID: 38513 RVA: 0x0005A079 File Offset: 0x00058279
		[Obsolete("Instead use the default constructor and set the Layout & Writer properties")]
		public TextWriterAppender(ILayout layout, Stream os)
			: this(layout, new StreamWriter(os))
		{
		}

		// Token: 0x06009672 RID: 38514 RVA: 0x0005A088 File Offset: 0x00058288
		[Obsolete("Instead use the default constructor and set the Layout & Writer properties")]
		public TextWriterAppender(ILayout layout, TextWriter writer)
		{
			this.Layout = layout;
			this.Writer = writer;
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06009673 RID: 38515 RVA: 0x0005A0A5 File Offset: 0x000582A5
		// (set) Token: 0x06009674 RID: 38516 RVA: 0x0005A0AD File Offset: 0x000582AD
		public bool ImmediateFlush { get; set; } = true;

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06009675 RID: 38517 RVA: 0x0005A0B6 File Offset: 0x000582B6
		// (set) Token: 0x06009676 RID: 38518 RVA: 0x00147714 File Offset: 0x00145914
		public virtual TextWriter Writer
		{
			get
			{
				return this.QuietWriter;
			}
			set
			{
				lock (this)
				{
					this.Reset();
					if (value != null)
					{
						this.QuietWriter = new QuietTextWriter(value, this.ErrorHandler);
						this.WriteHeader();
					}
				}
			}
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06009677 RID: 38519 RVA: 0x0005A0BE File Offset: 0x000582BE
		// (set) Token: 0x06009678 RID: 38520 RVA: 0x00147764 File Offset: 0x00145964
		public override IErrorHandler ErrorHandler
		{
			get
			{
				return base.ErrorHandler;
			}
			set
			{
				lock (this)
				{
					if (value == null)
					{
						LogLog.Warn(TextWriterAppender.declaringType, "TextWriterAppender: You have tried to set a null error-handler.");
					}
					else
					{
						base.ErrorHandler = value;
						if (this.QuietWriter != null)
						{
							this.QuietWriter.ErrorHandler = value;
						}
					}
				}
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06009679 RID: 38521 RVA: 0x00005B7A File Offset: 0x00003D7A
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x0600967A RID: 38522 RVA: 0x0005A0C6 File Offset: 0x000582C6
		// (set) Token: 0x0600967B RID: 38523 RVA: 0x0005A0CE File Offset: 0x000582CE
		protected QuietTextWriter QuietWriter { get; set; }

		// Token: 0x0600967C RID: 38524 RVA: 0x001477C4 File Offset: 0x001459C4
		protected override bool PreAppendCheck()
		{
			if (!base.PreAppendCheck())
			{
				return false;
			}
			if (this.QuietWriter == null)
			{
				this.PrepareWriter();
				if (this.QuietWriter == null)
				{
					this.ErrorHandler.Error("No output stream or file set for the appender named [" + base.Name + "].");
					return false;
				}
			}
			if (this.QuietWriter.Closed)
			{
				this.ErrorHandler.Error("Output stream for appender named [" + base.Name + "] has been closed.");
				return false;
			}
			return true;
		}

		// Token: 0x0600967D RID: 38525 RVA: 0x0005A0D7 File Offset: 0x000582D7
		protected override void Append(LoggingEvent loggingEvent)
		{
			base.RenderLoggingEvent(this.QuietWriter, loggingEvent);
			if (this.ImmediateFlush)
			{
				this.QuietWriter.Flush();
			}
		}

		// Token: 0x0600967E RID: 38526 RVA: 0x00147844 File Offset: 0x00145A44
		protected override void Append(LoggingEvent[] loggingEvents)
		{
			foreach (LoggingEvent loggingEvent in loggingEvents)
			{
				base.RenderLoggingEvent(this.QuietWriter, loggingEvent);
			}
			if (this.ImmediateFlush)
			{
				this.QuietWriter.Flush();
			}
		}

		// Token: 0x0600967F RID: 38527 RVA: 0x00147888 File Offset: 0x00145A88
		protected override void OnClose()
		{
			lock (this)
			{
				this.Reset();
			}
		}

		// Token: 0x06009680 RID: 38528 RVA: 0x0005A0F9 File Offset: 0x000582F9
		protected virtual void WriteFooterAndCloseWriter()
		{
			this.WriteFooter();
			this.CloseWriter();
		}

		// Token: 0x06009681 RID: 38529 RVA: 0x001478BC File Offset: 0x00145ABC
		protected virtual void CloseWriter()
		{
			if (this.QuietWriter != null)
			{
				try
				{
					this.QuietWriter.Close();
				}
				catch (Exception ex)
				{
					this.ErrorHandler.Error(string.Format("Could not close writer [{0}]", this.QuietWriter), ex);
				}
			}
		}

		// Token: 0x06009682 RID: 38530 RVA: 0x0005A107 File Offset: 0x00058307
		protected virtual void Reset()
		{
			this.WriteFooterAndCloseWriter();
			this.QuietWriter = null;
		}

		// Token: 0x06009683 RID: 38531 RVA: 0x00147910 File Offset: 0x00145B10
		protected virtual void WriteFooter()
		{
			if (this.Layout != null)
			{
				QuietTextWriter quietWriter = this.QuietWriter;
				if (quietWriter != null && !quietWriter.Closed)
				{
					string footer = this.Layout.Footer;
					if (footer != null)
					{
						this.QuietWriter.Write(footer);
					}
				}
			}
		}

		// Token: 0x06009684 RID: 38532 RVA: 0x00147954 File Offset: 0x00145B54
		protected virtual void WriteHeader()
		{
			if (this.Layout != null)
			{
				QuietTextWriter quietWriter = this.QuietWriter;
				if (quietWriter != null && !quietWriter.Closed)
				{
					string header = this.Layout.Header;
					if (header != null)
					{
						this.QuietWriter.Write(header);
					}
				}
			}
		}

		// Token: 0x06009685 RID: 38533 RVA: 0x0000568E File Offset: 0x0000388E
		protected virtual void PrepareWriter()
		{
		}

		// Token: 0x04006323 RID: 25379
		private static readonly Type declaringType = typeof(TextWriterAppender);
	}
}
