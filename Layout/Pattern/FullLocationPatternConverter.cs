using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A2A RID: 10794
	internal sealed class FullLocationPatternConverter : PatternLayoutConverter
	{
		// Token: 0x0600934B RID: 37707 RVA: 0x00058062 File Offset: 0x00056262
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.LocationInformation.FullInfo);
		}
	}
}
