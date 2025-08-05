using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AEF RID: 10991
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public class DecalProjectorComponent : GenericDecalProjectorComponent<Decals, DecalProjectorBase, DecalsMesh>
	{
		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x06009797 RID: 38807 RVA: 0x0005A8C7 File Offset: 0x00058AC7
		// (set) Token: 0x06009798 RID: 38808 RVA: 0x0005A8CF File Offset: 0x00058ACF
		public float MeshMinimizerMaximumAbsoluteError
		{
			get
			{
				return this.m_MeshMinimizerMaximumAbsoluteError;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("The maximum absolute error has to be greater than zero.");
				}
				this.m_MeshMinimizerMaximumAbsoluteError = value;
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x06009799 RID: 38809 RVA: 0x0005A8EB File Offset: 0x00058AEB
		// (set) Token: 0x0600979A RID: 38810 RVA: 0x00149068 File Offset: 0x00147268
		public float MeshMinimizerMaximumRelativeError
		{
			get
			{
				return this.m_MeshMinimizerMaximumRelativeError;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("The maximum relative error has to be within [0.0f, 1.0f].");
				}
				this.m_MeshMinimizerMaximumRelativeError = value;
			}
		}

		// Token: 0x040063B1 RID: 25521
		public bool ignoreMeshMinimizer;

		// Token: 0x040063B2 RID: 25522
		public bool useCustomMeshMinimizerMaximumErrors;

		// Token: 0x040063B3 RID: 25523
		[SerializeField]
		private float m_MeshMinimizerMaximumAbsoluteError = 0.0001f;

		// Token: 0x040063B4 RID: 25524
		[SerializeField]
		private float m_MeshMinimizerMaximumRelativeError = 0.0001f;

		// Token: 0x040063B5 RID: 25525
		public bool affectMeshes = true;

		// Token: 0x040063B6 RID: 25526
		public bool affectTerrains = true;

		// Token: 0x040063B7 RID: 25527
		public bool useTerrainDensity;

		// Token: 0x040063B8 RID: 25528
		public float terrainDensity = 0.5f;
	}
}
