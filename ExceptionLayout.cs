using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x02002A18 RID: 10776
	public class ExceptionLayout : LayoutSkeleton
	{
		// Token: 0x060092F9 RID: 37625 RVA: 0x00057DEE File Offset: 0x00055FEE
		public ExceptionLayout()
		{
			this.IgnoresException = false;
		}

		// Token: 0x060092FA RID: 37626 RVA: 0x0000568E File Offset: 0x0000388E
		public override void ActivateOptions()
		{
		}

		// Token: 0x060092FB RID: 37627 RVA: 0x00057DFD File Offset: 0x00055FFD
		[NullableContext(1)]
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			writer.Write(loggingEvent.GetExceptionString());
		}
	}
}
