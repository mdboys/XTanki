using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029EE RID: 10734
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class IdentityPatternConverter : PatternConverter
	{
		// Token: 0x060091B3 RID: 37299 RVA: 0x000572F5 File Offset: 0x000554F5
		protected override void Convert(TextWriter writer, object state)
		{
			writer.Write(SystemInfo.NotAvailableText);
		}

		// Token: 0x0400613E RID: 24894
		private static readonly Type declaringType = typeof(IdentityPatternConverter);
	}
}
