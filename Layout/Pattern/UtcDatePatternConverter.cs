using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A3B RID: 10811
	[NullableContext(1)]
	[Nullable(0)]
	internal class UtcDatePatternConverter : DatePatternConverter
	{
		// Token: 0x06009378 RID: 37752 RVA: 0x00142180 File Offset: 0x00140380
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			try
			{
				this.m_dateFormatter.FormatDate(loggingEvent.TimeStamp.ToUniversalTime(), writer);
			}
			catch (Exception ex)
			{
				LogLog.Error(UtcDatePatternConverter.declaringType, "Error occurred while converting date.", ex);
			}
		}

		// Token: 0x040061CD RID: 25037
		private static readonly Type declaringType = typeof(UtcDatePatternConverter);
	}
}
