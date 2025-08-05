using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A30 RID: 10800
	internal sealed class MethodLocationPatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009357 RID: 37719 RVA: 0x000580C2 File Offset: 0x000562C2
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.LocationInformation.MethodName);
		}
	}
}
