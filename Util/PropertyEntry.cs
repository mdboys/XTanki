using System;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029D0 RID: 10704
	[NullableContext(1)]
	[Nullable(0)]
	public class PropertyEntry
	{
		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x060090FA RID: 37114 RVA: 0x00056CE8 File Offset: 0x00054EE8
		// (set) Token: 0x060090FB RID: 37115 RVA: 0x00056CF0 File Offset: 0x00054EF0
		public string Key { get; set; }

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x060090FC RID: 37116 RVA: 0x00056CF9 File Offset: 0x00054EF9
		// (set) Token: 0x060090FD RID: 37117 RVA: 0x00056D01 File Offset: 0x00054F01
		public object Value { get; set; }

		// Token: 0x060090FE RID: 37118 RVA: 0x00056D0A File Offset: 0x00054F0A
		public override string ToString()
		{
			return string.Format("PropertyEntry(Key={0}, Value={1})", this.Key, this.Value);
		}
	}
}
