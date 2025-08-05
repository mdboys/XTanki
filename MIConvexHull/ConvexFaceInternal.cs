using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x02002995 RID: 10645
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ConvexFaceInternal
	{
		// Token: 0x06008F07 RID: 36615 RVA: 0x00055B4E File Offset: 0x00053D4E
		public ConvexFaceInternal(int dimension, int index, IndexBuffer beyondList)
		{
			this.Index = index;
			this.AdjacentFaces = new int[dimension];
			this.VerticesBeyond = beyondList;
			this.Normal = new double[dimension];
			this.Vertices = new int[dimension];
		}

		// Token: 0x04005FF9 RID: 24569
		public int[] AdjacentFaces;

		// Token: 0x04005FFA RID: 24570
		public int FurthestVertex;

		// Token: 0x04005FFB RID: 24571
		public int Index;

		// Token: 0x04005FFC RID: 24572
		public bool InList;

		// Token: 0x04005FFD RID: 24573
		public bool IsNormalFlipped;

		// Token: 0x04005FFE RID: 24574
		public ConvexFaceInternal Next;

		// Token: 0x04005FFF RID: 24575
		public double[] Normal;

		// Token: 0x04006000 RID: 24576
		public double Offset;

		// Token: 0x04006001 RID: 24577
		public ConvexFaceInternal Previous;

		// Token: 0x04006002 RID: 24578
		public int Tag;

		// Token: 0x04006003 RID: 24579
		public int[] Vertices;

		// Token: 0x04006004 RID: 24580
		public IndexBuffer VerticesBeyond;
	}
}
