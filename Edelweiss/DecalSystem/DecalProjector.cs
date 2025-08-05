using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AED RID: 10989
	public class DecalProjector : DecalProjectorBase
	{
		// Token: 0x0600978A RID: 38794 RVA: 0x00148F50 File Offset: 0x00147150
		public DecalProjector(Vector3 a_Position, Quaternion a_Rotation, Vector3 a_Scale, float a_CullingAngle, float a_meshOffset, int a_UV1RectangleIndex, int a_UV2RectangleIndex)
		{
			this.position = a_Position;
			this.rotation = a_Rotation;
			this.scale = a_Scale;
			this.cullingAngle = a_CullingAngle;
			this.meshOffset = a_meshOffset;
			this.uv1RectangleIndex = a_UV1RectangleIndex;
			this.uv2RectangleIndex = a_UV2RectangleIndex;
			this.vertexColor = Color.white;
			this.SetVertexColorBlending(0f);
		}

		// Token: 0x0600978B RID: 38795 RVA: 0x00148FB0 File Offset: 0x001471B0
		public DecalProjector(Vector3 a_Position, Quaternion a_Rotation, Vector3 a_Scale, float a_CullingAngle, float a_MeshOffset, int a_UV1RectangleIndex, int a_UV2RectangleIndex, Color a_VertexColor, float a_VertexColorBlending)
		{
			bool flag = a_VertexColorBlending < 0f || a_VertexColorBlending > 1f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("The blend value has to be in [0.0f, 1.0f].");
			}
			this.position = a_Position;
			this.rotation = a_Rotation;
			this.scale = a_Scale;
			this.cullingAngle = a_CullingAngle;
			this.meshOffset = a_MeshOffset;
			this.uv1RectangleIndex = a_UV1RectangleIndex;
			this.uv2RectangleIndex = a_UV2RectangleIndex;
			this.vertexColor = a_VertexColor;
			this.SetVertexColorBlending(a_VertexColorBlending);
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x0600978C RID: 38796 RVA: 0x0005A877 File Offset: 0x00058A77
		public override Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x0600978D RID: 38797 RVA: 0x0005A87F File Offset: 0x00058A7F
		public override Quaternion Rotation
		{
			get
			{
				return this.rotation;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x0600978E RID: 38798 RVA: 0x0005A887 File Offset: 0x00058A87
		public override Vector3 Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x0600978F RID: 38799 RVA: 0x0005A88F File Offset: 0x00058A8F
		public override float CullingAngle
		{
			get
			{
				return this.cullingAngle;
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06009790 RID: 38800 RVA: 0x0005A897 File Offset: 0x00058A97
		public override float MeshOffset
		{
			get
			{
				return this.meshOffset;
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06009791 RID: 38801 RVA: 0x0005A89F File Offset: 0x00058A9F
		public override int UV1RectangleIndex
		{
			get
			{
				return this.uv1RectangleIndex;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06009792 RID: 38802 RVA: 0x0005A8A7 File Offset: 0x00058AA7
		public override int UV2RectangleIndex
		{
			get
			{
				return this.uv2RectangleIndex;
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x06009793 RID: 38803 RVA: 0x0005A8AF File Offset: 0x00058AAF
		public override Color VertexColor
		{
			get
			{
				return this.vertexColor;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06009794 RID: 38804 RVA: 0x0005A8B7 File Offset: 0x00058AB7
		public override float VertexColorBlending
		{
			get
			{
				return this.m_VertexColorBlending;
			}
		}

		// Token: 0x06009795 RID: 38805 RVA: 0x00149030 File Offset: 0x00147230
		public void SetVertexColorBlending(float a_VertexColorBlending)
		{
			bool flag = a_VertexColorBlending < 0f || a_VertexColorBlending > 1f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("The blend value has to be in [0.0f, 1.0f].");
			}
			this.m_VertexColorBlending = a_VertexColorBlending;
		}

		// Token: 0x040063A8 RID: 25512
		public float cullingAngle;

		// Token: 0x040063A9 RID: 25513
		private float m_VertexColorBlending;

		// Token: 0x040063AA RID: 25514
		public float meshOffset;

		// Token: 0x040063AB RID: 25515
		public Vector3 position;

		// Token: 0x040063AC RID: 25516
		public Quaternion rotation;

		// Token: 0x040063AD RID: 25517
		public Vector3 scale;

		// Token: 0x040063AE RID: 25518
		public int uv1RectangleIndex;

		// Token: 0x040063AF RID: 25519
		public int uv2RectangleIndex;

		// Token: 0x040063B0 RID: 25520
		public Color vertexColor;
	}
}
