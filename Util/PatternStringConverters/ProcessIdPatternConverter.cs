using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029F1 RID: 10737
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ProcessIdPatternConverter : PatternConverter
	{
		// Token: 0x060091BC RID: 37308 RVA: 0x0013DDF8 File Offset: 0x0013BFF8
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				writer.Write(Process.GetCurrentProcess().Id);
			}
			catch (SecurityException)
			{
				LogLog.Debug(ProcessIdPatternConverter.declaringType, "Security exception while trying to get current process id. Error Ignored.");
				writer.Write(SystemInfo.NotAvailableText);
			}
		}

		// Token: 0x0400613F RID: 24895
		private static readonly Type declaringType = typeof(ProcessIdPatternConverter);
	}
}
