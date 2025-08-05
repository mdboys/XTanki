using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AF7 RID: 10999
	[NullableContext(1)]
	[Nullable(0)]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class DecalsMeshRenderer : MonoBehaviour
	{
		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x060097E2 RID: 38882 RVA: 0x0005AAAC File Offset: 0x00058CAC
		public MeshFilter MeshFilter
		{
			get
			{
				if (this.m_MeshFilter == null)
				{
					this.m_MeshFilter = base.GetComponent<MeshFilter>();
				}
				return this.m_MeshFilter;
			}
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x060097E3 RID: 38883 RVA: 0x0005AACE File Offset: 0x00058CCE
		public MeshRenderer MeshRenderer
		{
			get
			{
				if (this.m_MeshRenderer == null)
				{
					this.m_MeshRenderer = base.GetComponent<MeshRenderer>();
				}
				return this.m_MeshRenderer;
			}
		}

		// Token: 0x040063CA RID: 25546
		[HideInInspector]
		[SerializeField]
		private MeshFilter m_MeshFilter;

		// Token: 0x040063CB RID: 25547
		[HideInInspector]
		[SerializeField]
		private MeshRenderer m_MeshRenderer;
	}
}
