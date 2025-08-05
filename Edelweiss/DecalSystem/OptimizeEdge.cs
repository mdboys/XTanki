using System;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B0B RID: 11019
	internal struct OptimizeEdge : IComparable<OptimizeEdge>
	{
		// Token: 0x060098B1 RID: 39089 RVA: 0x0005B22B File Offset: 0x0005942B
		public OptimizeEdge(int a_Vertex1Index, int a_Vertex2Index, int a_Triangle1Index)
		{
			if (a_Vertex1Index < a_Vertex2Index)
			{
				this.vertex1Index = a_Vertex1Index;
				this.vertex2Index = a_Vertex2Index;
			}
			else
			{
				this.vertex1Index = a_Vertex2Index;
				this.vertex2Index = a_Vertex1Index;
			}
			this.triangle1Index = a_Triangle1Index;
			this.triangle2Index = -1;
		}

		// Token: 0x060098B2 RID: 39090 RVA: 0x00150B2C File Offset: 0x0014ED2C
		public int CompareTo(OptimizeEdge a_Other)
		{
			int num = this.vertex1Index.CompareTo(a_Other.vertex1Index);
			if (num == 0)
			{
				num = this.vertex2Index.CompareTo(a_Other.vertex2Index);
			}
			return num;
		}

		// Token: 0x0400642A RID: 25642
		public int vertex1Index;

		// Token: 0x0400642B RID: 25643
		public int vertex2Index;

		// Token: 0x0400642C RID: 25644
		public int triangle1Index;

		// Token: 0x0400642D RID: 25645
		public int triangle2Index;
	}
}
