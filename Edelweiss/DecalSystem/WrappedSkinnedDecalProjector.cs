using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B24 RID: 11044
	[NullableContext(1)]
	[Nullable(0)]
	public class WrappedSkinnedDecalProjector : SkinnedDecalProjectorBase
	{
		// Token: 0x0600992E RID: 39214 RVA: 0x0005B7CB File Offset: 0x000599CB
		public WrappedSkinnedDecalProjector(SkinnedDecalProjectorComponent a_DecalProjector)
		{
			this.WrappedSkinnedDecalProjectorComponent = a_DecalProjector;
			this.m_Transform = this.WrappedSkinnedDecalProjectorComponent.transform;
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x0600992F RID: 39215 RVA: 0x0005B7EB File Offset: 0x000599EB
		public SkinnedDecalProjectorComponent WrappedSkinnedDecalProjectorComponent { get; }

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06009930 RID: 39216 RVA: 0x0005B7F3 File Offset: 0x000599F3
		public override Vector3 Position
		{
			get
			{
				return this.m_Transform.position;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x06009931 RID: 39217 RVA: 0x0005B800 File Offset: 0x00059A00
		public override Quaternion Rotation
		{
			get
			{
				return this.m_Transform.rotation;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x06009932 RID: 39218 RVA: 0x0005B80D File Offset: 0x00059A0D
		public override Vector3 Scale
		{
			get
			{
				return this.m_Transform.localScale;
			}
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06009933 RID: 39219 RVA: 0x0005B81A File Offset: 0x00059A1A
		public override float CullingAngle
		{
			get
			{
				return this.WrappedSkinnedDecalProjectorComponent.cullingAngle;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x06009934 RID: 39220 RVA: 0x0005B827 File Offset: 0x00059A27
		public override float MeshOffset
		{
			get
			{
				return this.WrappedSkinnedDecalProjectorComponent.meshOffset;
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06009935 RID: 39221 RVA: 0x0005B834 File Offset: 0x00059A34
		public override int UV1RectangleIndex
		{
			get
			{
				return this.WrappedSkinnedDecalProjectorComponent.uv1RectangleIndex;
			}
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x06009936 RID: 39222 RVA: 0x0005B841 File Offset: 0x00059A41
		public override int UV2RectangleIndex
		{
			get
			{
				return this.WrappedSkinnedDecalProjectorComponent.uv2RectangleIndex;
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x06009937 RID: 39223 RVA: 0x0005B84E File Offset: 0x00059A4E
		public override Color VertexColor
		{
			get
			{
				return this.WrappedSkinnedDecalProjectorComponent.vertexColor;
			}
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x06009938 RID: 39224 RVA: 0x0005B85B File Offset: 0x00059A5B
		public override float VertexColorBlending
		{
			get
			{
				return this.WrappedSkinnedDecalProjectorComponent.VertexColorBlending;
			}
		}

		// Token: 0x0400646B RID: 25707
		private readonly Transform m_Transform;
	}
}
