using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Layout;

namespace log4net.Appender
{
	// Token: 0x02002A87 RID: 10887
	[NullableContext(1)]
	[Nullable(0)]
	public class ConsoleAppender : AppenderSkeleton
	{
		// Token: 0x060095CB RID: 38347 RVA: 0x0004CF23 File Offset: 0x0004B123
		public ConsoleAppender()
		{
		}

		// Token: 0x060095CC RID: 38348 RVA: 0x00059A5A File Offset: 0x00057C5A
		[Obsolete("Instead use the default constructor and set the Layout property")]
		public ConsoleAppender(ILayout layout)
			: this(layout, false)
		{
		}

		// Token: 0x060095CD RID: 38349 RVA: 0x00059A64 File Offset: 0x00057C64
		[Obsolete("Instead use the default constructor and set the Layout & Target properties")]
		public ConsoleAppender(ILayout layout, bool writeToErrorStream)
		{
			this.Layout = layout;
			this.m_writeToErrorStream = writeToErrorStream;
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x060095CE RID: 38350 RVA: 0x00059A7A File Offset: 0x00057C7A
		// (set) Token: 0x060095CF RID: 38351 RVA: 0x001462FC File Offset: 0x001444FC
		public virtual string Target
		{
			get
			{
				if (this.m_writeToErrorStream)
				{
					return "Console.Error";
				}
				return "Console.Out";
			}
			set
			{
				string text = value.Trim();
				if (string.Compare("Console.Error", text, true, CultureInfo.InvariantCulture) == 0)
				{
					this.m_writeToErrorStream = true;
					return;
				}
				this.m_writeToErrorStream = false;
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x060095D0 RID: 38352 RVA: 0x00005B7A File Offset: 0x00003D7A
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060095D1 RID: 38353 RVA: 0x00059A8F File Offset: 0x00057C8F
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_writeToErrorStream)
			{
				Console.Error.Write(base.RenderLoggingEvent(loggingEvent));
				return;
			}
			Console.Write(base.RenderLoggingEvent(loggingEvent));
		}

		// Token: 0x040062AE RID: 25262
		public const string ConsoleOut = "Console.Out";

		// Token: 0x040062AF RID: 25263
		public const string ConsoleError = "Console.Error";

		// Token: 0x040062B0 RID: 25264
		private bool m_writeToErrorStream;
	}
}
