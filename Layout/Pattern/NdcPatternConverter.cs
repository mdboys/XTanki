using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A32 RID: 10802
	internal sealed class NdcPatternConverter : PatternLayoutConverter
	{
		// Token: 0x0600935E RID: 37726 RVA: 0x000580E6 File Offset: 0x000562E6
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.LookupProperty("NDC"));
		}
	}
}
