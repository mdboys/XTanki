using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AFB RID: 11003
	public abstract class GenericDecalProjectorBase
	{
		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060097EC RID: 38892 RVA: 0x0005AB17 File Offset: 0x00058D17
		// (set) Token: 0x060097ED RID: 38893 RVA: 0x0005AB1F File Offset: 0x00058D1F
		public bool IsActiveProjector { get; internal set; }

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060097EE RID: 38894 RVA: 0x0005AB28 File Offset: 0x00058D28
		// (set) Token: 0x060097EF RID: 38895 RVA: 0x0005AB30 File Offset: 0x00058D30
		public int DecalsMeshLowerVertexIndex { get; internal set; }

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x060097F0 RID: 38896 RVA: 0x0005AB39 File Offset: 0x00058D39
		// (set) Token: 0x060097F1 RID: 38897 RVA: 0x0005AB41 File Offset: 0x00058D41
		public int DecalsMeshUpperVertexIndex { get; internal set; }

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x060097F2 RID: 38898 RVA: 0x0005AB4A File Offset: 0x00058D4A
		public int DecalsMeshVertexCount
		{
			get
			{
				return this.DecalsMeshUpperVertexIndex - this.DecalsMeshLowerVertexIndex + 1;
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x060097F3 RID: 38899 RVA: 0x0005AB5B File Offset: 0x00058D5B
		// (set) Token: 0x060097F4 RID: 38900 RVA: 0x0005AB63 File Offset: 0x00058D63
		public int DecalsMeshLowerTriangleIndex { get; internal set; }

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x060097F5 RID: 38901 RVA: 0x0005AB6C File Offset: 0x00058D6C
		// (set) Token: 0x060097F6 RID: 38902 RVA: 0x0005AB74 File Offset: 0x00058D74
		public int DecalsMeshUpperTriangleIndex { get; internal set; }

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x060097F7 RID: 38903 RVA: 0x0005AB7D File Offset: 0x00058D7D
		public int DecalsMeshTriangleCount
		{
			get
			{
				return this.DecalsMeshUpperTriangleIndex - this.DecalsMeshLowerTriangleIndex + 1;
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x060097F8 RID: 38904 RVA: 0x0005AB8E File Offset: 0x00058D8E
		// (set) Token: 0x060097F9 RID: 38905 RVA: 0x0005AB96 File Offset: 0x00058D96
		public bool IsUV1ProjectionCalculated { get; internal set; }

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x060097FA RID: 38906 RVA: 0x0005AB9F File Offset: 0x00058D9F
		// (set) Token: 0x060097FB RID: 38907 RVA: 0x0005ABA7 File Offset: 0x00058DA7
		public bool IsUV2ProjectionCalculated { get; internal set; }

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x060097FC RID: 38908 RVA: 0x0005ABB0 File Offset: 0x00058DB0
		// (set) Token: 0x060097FD RID: 38909 RVA: 0x0005ABB8 File Offset: 0x00058DB8
		public bool IsTangentProjectionCalculated { get; internal set; }

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x060097FE RID: 38910
		public abstract Vector3 Position { get; }

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x060097FF RID: 38911
		public abstract Quaternion Rotation { get; }

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06009800 RID: 38912
		public abstract Vector3 Scale { get; }

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06009801 RID: 38913
		public abstract float CullingAngle { get; }

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06009802 RID: 38914
		public abstract float MeshOffset { get; }

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06009803 RID: 38915
		public abstract int UV1RectangleIndex { get; }

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06009804 RID: 38916
		public abstract int UV2RectangleIndex { get; }

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06009805 RID: 38917
		public abstract Color VertexColor { get; }

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06009806 RID: 38918
		public abstract float VertexColorBlending { get; }

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06009807 RID: 38919 RVA: 0x0005ABC1 File Offset: 0x00058DC1
		public Matrix4x4 ProjectorToWorldMatrix
		{
			get
			{
				return Matrix4x4.TRS(this.Position, this.Rotation, this.Scale);
			}
		}

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06009808 RID: 38920 RVA: 0x0014CF94 File Offset: 0x0014B194
		public Matrix4x4 WorldToProjectorMatrix
		{
			get
			{
				return this.ProjectorToWorldMatrix.inverse;
			}
		}

		// Token: 0x06009809 RID: 38921 RVA: 0x0014CFB0 File Offset: 0x0014B1B0
		public Bounds WorldBounds()
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(this.Position, this.Rotation, Vector3.one);
			Vector3 vector = 0.5f * this.Scale;
			Vector3 vector2 = new Vector3(0f, 0f - Mathf.Abs(vector.y), 0f);
			Vector3 vector3 = matrix4x.MultiplyPoint3x4(Vector3.zero);
			Bounds bounds = new Bounds(vector3, Vector3.zero);
			vector3 = vector2 + new Vector3(vector.x, vector.y, vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(vector.x, vector.y, 0f - vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(vector.x, 0f - vector.y, vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(vector.x, 0f - vector.y, 0f - vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(0f - vector.x, vector.y, vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(0f - vector.x, vector.y, 0f - vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(0f - vector.x, 0f - vector.y, vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(0f - vector.x, 0f - vector.y, 0f - vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			return bounds;
		}
	}
}
