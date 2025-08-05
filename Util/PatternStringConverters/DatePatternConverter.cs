using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.DateFormatter;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029EB RID: 10731
	[NullableContext(1)]
	[Nullable(0)]
	internal class DatePatternConverter : PatternConverter, IOptionHandler
	{
		// Token: 0x060091A9 RID: 37289 RVA: 0x0013DB1C File Offset: 0x0013BD1C
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

		// Token: 0x060091AA RID: 37290 RVA: 0x0013DBE0 File Offset: 0x0013BDE0
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				this.m_dateFormatter.FormatDate(DateTime.Now, writer);
			}
			catch (Exception ex)
			{
				LogLog.Error(DatePatternConverter.declaringType, "Error occurred while converting date.", ex);
			}
		}

		// Token: 0x0400613A RID: 24890
		private static readonly Type declaringType = typeof(DatePatternConverter);

		// Token: 0x0400613B RID: 24891
		protected IDateFormatter m_dateFormatter;
	}
}
