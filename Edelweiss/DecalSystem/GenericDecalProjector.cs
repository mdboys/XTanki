using System;
using System.Runtime.CompilerServices;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AFA RID: 11002
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GenericDecalProjector<[Nullable(0)] D, [Nullable(0)] P, [Nullable(0)] DM> : GenericDecalProjectorBase where D : GenericDecals<D, P, DM> where P : GenericDecalProjector<D, P, DM> where DM : GenericDecalsMesh<D, P, DM>
	{
		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060097E8 RID: 38888 RVA: 0x0005AAFE File Offset: 0x00058CFE
		// (set) Token: 0x060097E9 RID: 38889 RVA: 0x0005AB06 File Offset: 0x00058D06
		public DM DecalsMesh { get; internal set; }

		// Token: 0x060097EA RID: 38890 RVA: 0x0014CF4C File Offset: 0x0014B14C
		internal void ResetDecalsMesh()
		{
			this.DecalsMesh = default(DM);
			base.IsActiveProjector = false;
			base.DecalsMeshLowerVertexIndex = 0;
			base.DecalsMeshUpperVertexIndex = 0;
			base.IsUV1ProjectionCalculated = false;
			base.IsUV2ProjectionCalculated = false;
			base.IsTangentProjectionCalculated = false;
		}
	}
}
