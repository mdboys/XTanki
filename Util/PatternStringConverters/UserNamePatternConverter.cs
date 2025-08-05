using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029F4 RID: 10740
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class UserNamePatternConverter : PatternConverter
	{
		// Token: 0x060091C5 RID: 37317 RVA: 0x000572F5 File Offset: 0x000554F5
		protected override void Convert(TextWriter writer, object state)
		{
			writer.Write(SystemInfo.NotAvailableText);
		}

		// Token: 0x04006143 RID: 24899
		private static readonly Type declaringType = typeof(UserNamePatternConverter);
	}
}
