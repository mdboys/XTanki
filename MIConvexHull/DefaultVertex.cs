using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x0200299D RID: 10653
	[NullableContext(1)]
	[Nullable(0)]
	public class DefaultVertex : IVertex
	{
		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06008F3F RID: 36671 RVA: 0x00055D3D File Offset: 0x00053F3D
		// (set) Token: 0x06008F40 RID: 36672 RVA: 0x00055D45 File Offset: 0x00053F45
		public double[] Position { get; set; }
	}
}
