using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A29 RID: 10793
	internal sealed class FileLocationPatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009349 RID: 37705 RVA: 0x0005804F File Offset: 0x0005624F
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.LocationInformation.FileName);
		}
	}
}
