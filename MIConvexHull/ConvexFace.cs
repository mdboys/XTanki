using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x02002994 RID: 10644
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ConvexFace<[Nullable(0)] TVertex, [Nullable(0)] TFace> where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>
	{
		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06008F00 RID: 36608 RVA: 0x00055B1B File Offset: 0x00053D1B
		// (set) Token: 0x06008F01 RID: 36609 RVA: 0x00055B23 File Offset: 0x00053D23
		public TFace[] Adjacency { get; set; }

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06008F02 RID: 36610 RVA: 0x00055B2C File Offset: 0x00053D2C
		// (set) Token: 0x06008F03 RID: 36611 RVA: 0x00055B34 File Offset: 0x00053D34
		public TVertex[] Vertices { get; set; }

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06008F04 RID: 36612 RVA: 0x00055B3D File Offset: 0x00053D3D
		// (set) Token: 0x06008F05 RID: 36613 RVA: 0x00055B45 File Offset: 0x00053D45
		public double[] Normal { get; set; }
	}
}
