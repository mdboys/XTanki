using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029F5 RID: 10741
	[NullableContext(1)]
	[Nullable(0)]
	internal class UtcDatePatternConverter : DatePatternConverter
	{
		// Token: 0x060091C8 RID: 37320 RVA: 0x0013DF9C File Offset: 0x0013C19C
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				this.m_dateFormatter.FormatDate(DateTime.UtcNow, writer);
			}
			catch (Exception ex)
			{
				LogLog.Error(UtcDatePatternConverter.declaringType, "Error occurred while converting date.", ex);
			}
		}

		// Token: 0x04006144 RID: 24900
		private static readonly Type declaringType = typeof(UtcDatePatternConverter);
	}
}
