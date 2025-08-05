using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B01 RID: 11009
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GenericDecalsMeshBase
	{
		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06009879 RID: 39033 RVA: 0x0005B068 File Offset: 0x00059268
		public List<Vector2> UVs
		{
			get
			{
				this.RecalculateUVs();
				return this.m_UVs;
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x0600987A RID: 39034 RVA: 0x0005B076 File Offset: 0x00059276
		public List<Vector2> UV2s
		{
			get
			{
				if (Application.isPlaying && this.HasUV2LightmappingMode())
				{
					throw new InvalidOperationException("The lightmap for the UV2s can not be recalculated if the application is playing!");
				}
				this.RecalculateUV2s();
				return this.m_UV2s;
			}
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x0600987B RID: 39035 RVA: 0x0005B09E File Offset: 0x0005929E
		public List<Vector3> Vertices
		{
			get
			{
				return this.m_Vertices;
			}
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x0600987C RID: 39036 RVA: 0x0005B0A6 File Offset: 0x000592A6
		public List<Vector3> Normals
		{
			get
			{
				return this.m_Normals;
			}
		}

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x0600987D RID: 39037 RVA: 0x0005B0AE File Offset: 0x000592AE
		public List<Vector4> Tangents
		{
			get
			{
				this.RecalculateTangents();
				return this.m_Tangents;
			}
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x0600987E RID: 39038 RVA: 0x0005B0BC File Offset: 0x000592BC
		public List<Color> TargetVertexColors
		{
			get
			{
				return this.m_TargetVertexColors;
			}
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x0600987F RID: 39039 RVA: 0x0005B0C4 File Offset: 0x000592C4
		public List<Color> VertexColors
		{
			get
			{
				return this.m_VertexColors;
			}
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06009880 RID: 39040 RVA: 0x0005B0CC File Offset: 0x000592CC
		public List<int> Triangles
		{
			get
			{
				return this.m_Triangles;
			}
		}

		// Token: 0x06009881 RID: 39041
		protected abstract void RecalculateUVs();

		// Token: 0x06009882 RID: 39042
		protected abstract bool HasUV2LightmappingMode();

		// Token: 0x06009883 RID: 39043
		protected abstract void RecalculateUV2s();

		// Token: 0x06009884 RID: 39044
		protected abstract void RecalculateTangents();

		// Token: 0x04006404 RID: 25604
		protected List<Vector3> m_Normals = new List<Vector3>();

		// Token: 0x04006405 RID: 25605
		internal RemovedIndices m_RemovedIndices = new RemovedIndices();

		// Token: 0x04006406 RID: 25606
		protected List<Vector4> m_Tangents = new List<Vector4>();

		// Token: 0x04006407 RID: 25607
		protected List<Color> m_TargetVertexColors = new List<Color>();

		// Token: 0x04006408 RID: 25608
		protected List<int> m_Triangles = new List<int>();

		// Token: 0x04006409 RID: 25609
		protected List<Vector2> m_UV2s = new List<Vector2>();

		// Token: 0x0400640A RID: 25610
		protected List<Vector2> m_UVs = new List<Vector2>();

		// Token: 0x0400640B RID: 25611
		protected List<Color> m_VertexColors = new List<Color>();

		// Token: 0x0400640C RID: 25612
		protected List<Vector3> m_Vertices = new List<Vector3>();
	}
}
