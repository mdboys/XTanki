using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A2E RID: 10798
	internal sealed class LoggerPatternConverter : NamedPatternConverter
	{
		// Token: 0x06009353 RID: 37715 RVA: 0x000580A9 File Offset: 0x000562A9
		[NullableContext(1)]
		protected override string GetFullyQualifiedName(LoggingEvent loggingEvent)
		{
			return loggingEvent.LoggerName;
		}
	}
}
