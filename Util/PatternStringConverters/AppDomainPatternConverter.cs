using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029EA RID: 10730
	internal sealed class AppDomainPatternConverter : PatternConverter
	{
		// Token: 0x060091A7 RID: 37287 RVA: 0x000572B5 File Offset: 0x000554B5
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, object state)
		{
			writer.Write(SystemInfo.ApplicationFriendlyName);
		}
	}
}
