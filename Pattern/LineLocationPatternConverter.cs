using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A2D RID: 10797
	internal sealed class LineLocationPatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009351 RID: 37713 RVA: 0x00058096 File Offset: 0x00056296
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.LocationInformation.LineNumber);
		}
	}
}
