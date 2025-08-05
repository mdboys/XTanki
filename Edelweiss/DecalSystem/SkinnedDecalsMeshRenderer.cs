using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B1A RID: 11034
	[NullableContext(1)]
	[Nullable(0)]
	[RequireComponent(typeof(SkinnedMeshRenderer))]
	public class SkinnedDecalsMeshRenderer : MonoBehaviour
	{
		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x06009916 RID: 39190 RVA: 0x0005B662 File Offset: 0x00059862
		public SkinnedMeshRenderer SkinnedMeshRenderer
		{
			get
			{
				if (this.m_SkinnedMeshRenderer == null)
				{
					this.m_SkinnedMeshRenderer = base.GetComponent<SkinnedMeshRenderer>();
				}
				return this.m_SkinnedMeshRenderer;
			}
		}

		// Token: 0x04006452 RID: 25682
		[HideInInspector]
		[SerializeField]
		private SkinnedMeshRenderer m_SkinnedMeshRenderer;
	}
}
