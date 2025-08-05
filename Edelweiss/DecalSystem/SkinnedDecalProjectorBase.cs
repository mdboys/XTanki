using System;
using System.Runtime.CompilerServices;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B14 RID: 11028
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public abstract class SkinnedDecalProjectorBase : GenericDecalProjector<SkinnedDecals, SkinnedDecalProjectorBase, SkinnedDecalsMesh>
	{
		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x060098D8 RID: 39128 RVA: 0x0005B430 File Offset: 0x00059630
		// (set) Token: 0x060098D9 RID: 39129 RVA: 0x0005B438 File Offset: 0x00059638
		public int DecalsMeshLowerBonesIndex { get; internal set; }

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x060098DA RID: 39130 RVA: 0x0005B441 File Offset: 0x00059641
		// (set) Token: 0x060098DB RID: 39131 RVA: 0x0005B449 File Offset: 0x00059649
		public int DecalsMeshUpperBonesIndex { get; internal set; }

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x060098DC RID: 39132 RVA: 0x0005B452 File Offset: 0x00059652
		public int DecalsMeshBonesCount
		{
			get
			{
				return this.DecalsMeshUpperBonesIndex - this.DecalsMeshLowerBonesIndex + 1;
			}
		}
	}
}
