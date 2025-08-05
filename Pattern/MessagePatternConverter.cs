using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A2F RID: 10799
	internal sealed class MessagePatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009355 RID: 37717 RVA: 0x000580B9 File Offset: 0x000562B9
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			loggingEvent.WriteRenderedMessage(writer);
		}
	}
}
