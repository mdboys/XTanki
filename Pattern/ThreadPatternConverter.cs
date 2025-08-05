using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A38 RID: 10808
	internal sealed class ThreadPatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009372 RID: 37746 RVA: 0x00058195 File Offset: 0x00056395
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.ThreadName);
		}
	}
}
