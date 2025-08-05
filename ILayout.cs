using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x02002A19 RID: 10777
	[NullableContext(1)]
	public interface ILayout
	{
		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x060092FC RID: 37628
		string ContentType { get; }

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x060092FD RID: 37629
		string Header { get; }

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x060092FE RID: 37630
		string Footer { get; }

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x060092FF RID: 37631
		bool IgnoresException { get; }

		// Token: 0x06009300 RID: 37632
		void Format(TextWriter writer, LoggingEvent loggingEvent);
	}
}
