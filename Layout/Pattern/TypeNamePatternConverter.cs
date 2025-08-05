using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A39 RID: 10809
	internal sealed class TypeNamePatternConverter : NamedPatternConverter
	{
		// Token: 0x06009374 RID: 37748 RVA: 0x000581A3 File Offset: 0x000563A3
		[NullableContext(1)]
		protected override string GetFullyQualifiedName(LoggingEvent loggingEvent)
		{
			return loggingEvent.LocationInformation.ClassName;
		}
	}
}
