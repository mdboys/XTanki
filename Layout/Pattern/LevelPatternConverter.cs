using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A2C RID: 10796
	internal sealed class LevelPatternConverter : PatternLayoutConverter
	{
		// Token: 0x0600934F RID: 37711 RVA: 0x00058083 File Offset: 0x00056283
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			writer.Write(loggingEvent.Level.DisplayName);
		}
	}
}
