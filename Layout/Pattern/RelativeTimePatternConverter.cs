using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A35 RID: 10805
	internal sealed class RelativeTimePatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009367 RID: 37735 RVA: 0x00141F70 File Offset: 0x00140170
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(RelativeTimePatternConverter.TimeDifferenceInMillis(LoggingEvent.StartTime, loggingEvent.TimeStamp).ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06009368 RID: 37736 RVA: 0x00141FA0 File Offset: 0x001401A0
		private static long TimeDifferenceInMillis(DateTime start, DateTime end)
		{
			return (long)(end.ToUniversalTime() - start.ToUniversalTime()).TotalMilliseconds;
		}
	}
}
