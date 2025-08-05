using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.DateFormatter;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A27 RID: 10791
	[NullableContext(1)]
	[Nullable(0)]
	internal class DatePatternConverter : PatternLayoutConverter, IOptionHandler
	{
		// Token: 0x06009343 RID: 37699 RVA: 0x00141BCC File Offset: 0x0013FDCC
		public void ActivateOptions()
		{
			string text = this.Option;
			if (text == null)
			{
				text = "ISO8601";
			}
			if (string.Compare(text, "ISO8601", true, CultureInfo.InvariantCulture) == 0)
			{
				this.m_dateFormatter = new Iso8601DateFormatter();
				return;
			}
			if (string.Compare(text, "ABSOLUTE", true, CultureInfo.InvariantCulture) == 0)
			{
				this.m_dateFormatter = new AbsoluteTimeDateFormatter();
				return;
			}
			if (string.Compare(text, "DATE", true, CultureInfo.InvariantCulture) == 0)
			{
				this.m_dateFormatter = new DateTimeDateFormatter();
				return;
			}
			try
			{
				this.m_dateFormatter = new SimpleDateFormatter(text);
			}
			catch (Exception ex)
			{
				LogLog.Error(DatePatternConverter.declaringType, "Could not instantiate SimpleDateFormatter with [" + text + "]", ex);
				this.m_dateFormatter = new Iso8601DateFormatter();
			}
		}

		// Token: 0x06009344 RID: 37700 RVA: 0x00141C90 File Offset: 0x0013FE90
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			try
			{
				this.m_dateFormatter.FormatDate(loggingEvent.TimeStamp, writer);
			}
			catch (Exception ex)
			{
				LogLog.Error(DatePatternConverter.declaringType, "Error occurred while converting date.", ex);
			}
		}

		// Token: 0x040061C4 RID: 25028
		private static readonly Type declaringType = typeof(DatePatternConverter);

		// Token: 0x040061C5 RID: 25029
		protected IDateFormatter m_dateFormatter;
	}
}
