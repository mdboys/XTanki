using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x02002A22 RID: 10786
	public class SimpleLayout : LayoutSkeleton
	{
		// Token: 0x06009324 RID: 37668 RVA: 0x00057F07 File Offset: 0x00056107
		public SimpleLayout()
		{
			this.IgnoresException = true;
		}

		// Token: 0x06009325 RID: 37669 RVA: 0x0000568E File Offset: 0x0000388E
		public override void ActivateOptions()
		{
		}

		// Token: 0x06009326 RID: 37670 RVA: 0x00057F16 File Offset: 0x00056116
		[NullableContext(1)]
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			writer.Write(loggingEvent.Level.DisplayName);
			writer.Write(" - ");
			loggingEvent.WriteRenderedMessage(writer);
			writer.WriteLine();
		}
	}
}
