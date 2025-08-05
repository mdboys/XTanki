using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B23 RID: 11043
	[NullableContext(1)]
	[Nullable(0)]
	public class WrappedDecalProjector : DecalProjectorBase
	{
		// Token: 0x06009923 RID: 39203 RVA: 0x0005B72E File Offset: 0x0005992E
		public WrappedDecalProjector(DecalProjectorComponent a_DecalProjector)
		{
			this.WrappedDecalProjectorComponent = a_DecalProjector;
			this.m_Transform = this.WrappedDecalProjectorComponent.transform;
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06009924 RID: 39204 RVA: 0x0005B74E File Offset: 0x0005994E
		public DecalProjectorComponent WrappedDecalProjectorComponent { get; }

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06009925 RID: 39205 RVA: 0x0005B756 File Offset: 0x00059956
		public override Vector3 Position
		{
			get
			{
				return this.m_Transform.position;
			}
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06009926 RID: 39206 RVA: 0x0005B763 File Offset: 0x00059963
		public override Quaternion Rotation
		{
			get
			{
				return this.m_Transform.rotation;
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06009927 RID: 39207 RVA: 0x0005B770 File Offset: 0x00059970
		public override Vector3 Scale
		{
			get
			{
				return this.m_Transform.localScale;
			}
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x06009928 RID: 39208 RVA: 0x0005B77D File Offset: 0x0005997D
		public override float CullingAngle
		{
			get
			{
				return this.WrappedDecalProjectorComponent.cullingAngle;
			}
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x06009929 RID: 39209 RVA: 0x0005B78A File Offset: 0x0005998A
		public override float MeshOffset
		{
			get
			{
				return this.WrappedDecalProjectorComponent.meshOffset;
			}
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x0600992A RID: 39210 RVA: 0x0005B797 File Offset: 0x00059997
		public override int UV1RectangleIndex
		{
			get
			{
				return this.WrappedDecalProjectorComponent.uv1RectangleIndex;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x0600992B RID: 39211 RVA: 0x0005B7A4 File Offset: 0x000599A4
		public override int UV2RectangleIndex
		{
			get
			{
				return this.WrappedDecalProjectorComponent.uv2RectangleIndex;
			}
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x0600992C RID: 39212 RVA: 0x0005B7B1 File Offset: 0x000599B1
		public override Color VertexColor
		{
			get
			{
				return this.WrappedDecalProjectorComponent.vertexColor;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x0600992D RID: 39213 RVA: 0x0005B7BE File Offset: 0x000599BE
		public override float VertexColorBlending
		{
			get
			{
				return this.WrappedDecalProjectorComponent.VertexColorBlending;
			}
		}

		// Token: 0x04006469 RID: 25705
		private readonly Transform m_Transform;
	}
}
