using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A34 RID: 10804
	internal sealed class PropertyPatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009365 RID: 37733 RVA: 0x0005811F File Offset: 0x0005631F
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.LookupProperty(this.Option));
				return;
			}
			PatternConverter.WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
		}
	}
}
