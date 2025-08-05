using System;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AE9 RID: 10985
	internal struct BoneWeightElement : IComparable<BoneWeightElement>
	{
		// Token: 0x06009779 RID: 38777 RVA: 0x0005A7B7 File Offset: 0x000589B7
		public int CompareTo(BoneWeightElement a_Other)
		{
			return -this.weight.CompareTo(a_Other.weight);
		}

		// Token: 0x040063A1 RID: 25505
		public int index;

		// Token: 0x040063A2 RID: 25506
		public float weight;
	}
}
