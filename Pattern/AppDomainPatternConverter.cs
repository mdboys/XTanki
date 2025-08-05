using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A26 RID: 10790
	internal sealed class AppDomainPatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009341 RID: 37697 RVA: 0x00058021 File Offset: 0x00056221
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.Domain);
		}
	}
}
