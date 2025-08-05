using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x02002A1F RID: 10783
	[NullableContext(1)]
	[Nullable(0)]
	public class RawPropertyLayout : IRawLayout
	{
		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x0600931C RID: 37660 RVA: 0x00057EDB File Offset: 0x000560DB
		// (set) Token: 0x0600931D RID: 37661 RVA: 0x00057EE3 File Offset: 0x000560E3
		public string Key { get; set; }

		// Token: 0x0600931E RID: 37662 RVA: 0x00057EEC File Offset: 0x000560EC
		public virtual object Format(LoggingEvent loggingEvent)
		{
			return loggingEvent.LookupProperty(this.Key);
		}
	}
}
