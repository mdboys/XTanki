using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B13 RID: 11027
	public class SkinnedDecalProjector : SkinnedDecalProjectorBase
	{
		// Token: 0x060098CC RID: 39116 RVA: 0x00150C68 File Offset: 0x0014EE68
		public SkinnedDecalProjector(Vector3 a_Position, Quaternion a_Rotation, Vector3 a_Scale, float a_CullingAngle, float a_meshOffset, int a_UV1RectangleIndex, int a_UV2RectangleIndex)
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

		// Token: 0x060098CD RID: 39117 RVA: 0x00150CC8 File Offset: 0x0014EEC8
		public SkinnedDecalProjector(Vector3 a_Position, Quaternion a_Rotation, Vector3 a_Scale, float a_CullingAngle, float a_meshOffset, int a_UV1RectangleIndex, int a_UV2RectangleIndex, Color a_VertexColor, float a_VertexColorBlending)
		{
			this.position = a_Position;
			this.rotation = a_Rotation;
			this.scale = a_Scale;
			this.cullingAngle = a_CullingAngle;
			this.meshOffset = a_meshOffset;
			this.uv1RectangleIndex = a_UV1RectangleIndex;
			this.uv2RectangleIndex = a_UV2RectangleIndex;
			this.vertexColor = a_VertexColor;
			this.SetVertexColorBlending(a_VertexColorBlending);
		}

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x060098CE RID: 39118 RVA: 0x0005B3E8 File Offset: 0x000595E8
		public override Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x060098CF RID: 39119 RVA: 0x0005B3F0 File Offset: 0x000595F0
		public override Quaternion Rotation
		{
			get
			{
				return this.rotation;
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x060098D0 RID: 39120 RVA: 0x0005B3F8 File Offset: 0x000595F8
		public override Vector3 Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x060098D1 RID: 39121 RVA: 0x0005B400 File Offset: 0x00059600
		public override float CullingAngle
		{
			get
			{
				return this.cullingAngle;
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x060098D2 RID: 39122 RVA: 0x0005B408 File Offset: 0x00059608
		public override float MeshOffset
		{
			get
			{
				return this.meshOffset;
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x060098D3 RID: 39123 RVA: 0x0005B410 File Offset: 0x00059610
		public override int UV1RectangleIndex
		{
			get
			{
				return this.uv1RectangleIndex;
			}
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x060098D4 RID: 39124 RVA: 0x0005B418 File Offset: 0x00059618
		public override int UV2RectangleIndex
		{
			get
			{
				return this.uv2RectangleIndex;
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x060098D5 RID: 39125 RVA: 0x0005B420 File Offset: 0x00059620
		public override Color VertexColor
		{
			get
			{
				return this.vertexColor;
			}
		}

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x060098D6 RID: 39126 RVA: 0x0005B428 File Offset: 0x00059628
		public override float VertexColorBlending
		{
			get
			{
				return this.m_VertexColorBlending;
			}
		}

		// Token: 0x060098D7 RID: 39127 RVA: 0x00150D20 File Offset: 0x0014EF20
		public void SetVertexColorBlending(float a_VertexColorBlending)
		{
			bool flag = a_VertexColorBlending < 0f || a_VertexColorBlending > 1f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("The blend value has to be in [0.0f, 1.0f].");
			}
			this.m_VertexColorBlending = a_VertexColorBlending;
		}

		// Token: 0x04006441 RID: 25665
		public float cullingAngle;

		// Token: 0x04006442 RID: 25666
		private float m_VertexColorBlending;

		// Token: 0x04006443 RID: 25667
		public float meshOffset;

		// Token: 0x04006444 RID: 25668
		public Vector3 position;

		// Token: 0x04006445 RID: 25669
		public Quaternion rotation;

		// Token: 0x04006446 RID: 25670
		public Vector3 scale;

		// Token: 0x04006447 RID: 25671
		public int uv1RectangleIndex;

		// Token: 0x04006448 RID: 25672
		public int uv2RectangleIndex;

		// Token: 0x04006449 RID: 25673
		public Color vertexColor;
	}
}
