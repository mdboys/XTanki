using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020029AC RID: 10668
	[NullableContext(1)]
	[Nullable(0)]
	public class MeshBrushParent : MonoBehaviour
	{
		// Token: 0x06008F7F RID: 36735 RVA: 0x0004F05F File Offset: 0x0004D25F
		private void Start()
		{
			global::UnityEngine.Object.Destroy(this);
		}

		// Token: 0x0400609C RID: 24732
		private Renderer curRenderer;

		// Token: 0x0400609D RID: 24733
		private ArrayList elements;

		// Token: 0x0400609E RID: 24734
		private MeshFilter filter;

		// Token: 0x0400609F RID: 24735
		private CombineUtility.MeshInstance instance;

		// Token: 0x040060A0 RID: 24736
		private CombineUtility.MeshInstance[] instances;

		// Token: 0x040060A1 RID: 24737
		private Material[] materials;

		// Token: 0x040060A2 RID: 24738
		private Hashtable materialToMesh;

		// Token: 0x040060A3 RID: 24739
		private Transform[] meshes;

		// Token: 0x040060A4 RID: 24740
		private Component[] meshFilters;

		// Token: 0x040060A5 RID: 24741
		private Matrix4x4 myTransform;

		// Token: 0x040060A6 RID: 24742
		private ArrayList objects;
	}
}
