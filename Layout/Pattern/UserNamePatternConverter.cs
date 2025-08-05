using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A3A RID: 10810
	internal sealed class UserNamePatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009376 RID: 37750 RVA: 0x000581B0 File Offset: 0x000563B0
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.UserName);
		}
	}
}
