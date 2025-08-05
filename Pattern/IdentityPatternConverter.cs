using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A2B RID: 10795
	internal sealed class IdentityPatternConverter : PatternLayoutConverter
	{
		// Token: 0x0600934D RID: 37709 RVA: 0x00058075 File Offset: 0x00056275
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.Identity);
		}
	}
}
