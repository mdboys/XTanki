using System;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AEA RID: 10986
	internal struct CutEdge : IComparable<CutEdge>
	{
		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x0600977A RID: 38778 RVA: 0x00148EC0 File Offset: 0x001470C0
		public int SmallerIndex
		{
			get
			{
				int num = this.vertex1Index;
				if (this.vertex2Index < num)
				{
					num = this.vertex2Index;
				}
				return num;
			}
		}

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x0600977B RID: 38779 RVA: 0x00148EE8 File Offset: 0x001470E8
		public int GreaterIndex
		{
			get
			{
				int num = this.vertex1Index;
				if (this.vertex2Index > num)
				{
					num = this.vertex2Index;
				}
				return num;
			}
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x0600977C RID: 38780 RVA: 0x0005A7CB File Offset: 0x000589CB
		public int ModifiedIndex
		{
			get
			{
				if (this.vertex1Index == this.newVertex1Index)
				{
					return this.newVertex2Index;
				}
				return this.newVertex1Index;
			}
		}

		// Token: 0x0600977D RID: 38781 RVA: 0x0005A7E8 File Offset: 0x000589E8
		public CutEdge(int a_Vertex1Index, int a_Vertex2Index)
		{
			this.vertex1Index = a_Vertex1Index;
			this.vertex2Index = a_Vertex2Index;
			this.newVertex1Index = this.vertex1Index;
			this.newVertex2Index = this.vertex2Index;
		}

		// Token: 0x0600977E RID: 38782 RVA: 0x00148F10 File Offset: 0x00147110
		public int CompareTo(CutEdge a_Other)
		{
			int num = this.SmallerIndex.CompareTo(a_Other.SmallerIndex);
			if (num == 0)
			{
				num = this.GreaterIndex.CompareTo(a_Other.GreaterIndex);
			}
			return num;
		}

		// Token: 0x040063A3 RID: 25507
		public int vertex1Index;

		// Token: 0x040063A4 RID: 25508
		public int vertex2Index;

		// Token: 0x040063A5 RID: 25509
		public int newVertex1Index;

		// Token: 0x040063A6 RID: 25510
		public int newVertex2Index;
	}
}
