using System;

namespace log4net.Core
{
	// Token: 0x02002A4F RID: 10831
	[Flags]
	public enum FixFlags
	{
		// Token: 0x040061F7 RID: 25079
		[Obsolete("Replaced by composite Properties")]
		Mdc = 1,
		// Token: 0x040061F8 RID: 25080
		Ndc = 2,
		// Token: 0x040061F9 RID: 25081
		Message = 4,
		// Token: 0x040061FA RID: 25082
		ThreadName = 8,
		// Token: 0x040061FB RID: 25083
		LocationInfo = 16,
		// Token: 0x040061FC RID: 25084
		UserName = 32,
		// Token: 0x040061FD RID: 25085
		Domain = 64,
		// Token: 0x040061FE RID: 25086
		Identity = 128,
		// Token: 0x040061FF RID: 25087
		Exception = 256,
		// Token: 0x04006200 RID: 25088
		Properties = 512,
		// Token: 0x04006201 RID: 25089
		None = 0,
		// Token: 0x04006202 RID: 25090
		All = 268435455,
		// Token: 0x04006203 RID: 25091
		Partial = 844
	}
}
