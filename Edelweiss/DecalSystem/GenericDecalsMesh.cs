using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B00 RID: 11008
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GenericDecalsMesh<[Nullable(0)] D, [Nullable(0)] P, [Nullable(0)] DM> : GenericDecalsMeshBase where D : GenericDecals<D, P, DM> where P : GenericDecalProjector<D, P, DM> where DM : GenericDecalsMesh<D, P, DM>
	{
		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x0600984C RID: 38988 RVA: 0x0005AF6B File Offset: 0x0005916B
		public D Decals
		{
			get
			{
				return this.m_Decals;
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x0600984D RID: 38989 RVA: 0x0005AF73 File Offset: 0x00059173
		internal List<P> Projectors { get; } = new List<P>();

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x0600984E RID: 38990 RVA: 0x0014D928 File Offset: 0x0014BB28
		public P ActiveDecalProjector
		{
			get
			{
				P p = default(P);
				if (this.Projectors.Count != 0)
				{
					p = this.Projectors[this.Projectors.Count - 1];
				}
				return p;
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x0600984F RID: 38991 RVA: 0x0005AF7B File Offset: 0x0005917B
		internal List<Color[]> PreservedVertexColorArrays { get; } = new List<Color[]>();

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06009850 RID: 38992 RVA: 0x0005AF83 File Offset: 0x00059183
		// (set) Token: 0x06009851 RID: 38993 RVA: 0x0014D964 File Offset: 0x0014BB64
		public bool PreserveVertexColorArrays
		{
			get
			{
				return this.m_PreserveVertexColorArrays;
			}
			set
			{
				if (!value)
				{
					this.m_PreserveVertexColorArrays = value;
					this.PreservedVertexColorArrays.Clear();
					return;
				}
				if (!(this.m_Decals != null))
				{
					throw new InvalidOperationException("Unable to set preserving value while no valid decals instance is set.");
				}
				if (Edition.IsDecalSystemFree)
				{
					throw new InvalidOperationException("Preserving vertex color arrays is only supported in Decal System Pro.");
				}
				if (!this.m_Decals.UseVertexColors)
				{
					throw new InvalidOperationException("Unable to preserve vertex color arrays for a decals instance that does not use them.");
				}
				this.m_PreserveVertexColorArrays = value;
			}
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06009852 RID: 38994 RVA: 0x0005AF8B File Offset: 0x0005918B
		internal List<Vector2[]> PreservedProjectedUVArrays { get; } = new List<Vector2[]>();

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06009853 RID: 38995 RVA: 0x0005AF93 File Offset: 0x00059193
		// (set) Token: 0x06009854 RID: 38996 RVA: 0x0014D9DC File Offset: 0x0014BBDC
		public bool PreserveProjectedUVArrays
		{
			get
			{
				return this.m_PreserveProjectedUVArrays;
			}
			set
			{
				if (!value)
				{
					this.m_PreserveProjectedUVArrays = value;
					this.PreservedProjectedUVArrays.Clear();
					return;
				}
				if (!(this.m_Decals != null))
				{
					throw new InvalidOperationException("Unable to set preserving value while no valid decals instance is set.");
				}
				if (Edition.IsDecalSystemFree)
				{
					throw new InvalidOperationException("Preserving uv arrays is only supported in Decal System Pro.");
				}
				if (this.m_Decals.CurrentUVMode != UVMode.Project && this.m_Decals.CurrentUVMode != UVMode.ProjectWrapped)
				{
					throw new InvalidOperationException("Unalble to preserve uv arrays for a decals instance that does not use them.");
				}
				this.m_PreserveProjectedUVArrays = value;
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06009855 RID: 38997 RVA: 0x0005AF9B File Offset: 0x0005919B
		internal List<Vector2[]> PreservedProjectedUV2Arrays { get; } = new List<Vector2[]>();

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06009856 RID: 38998 RVA: 0x0005AFA3 File Offset: 0x000591A3
		// (set) Token: 0x06009857 RID: 38999 RVA: 0x0014DA68 File Offset: 0x0014BC68
		public bool PreserveProjectedUV2Arrays
		{
			get
			{
				return this.m_PreserveProjectedUV2Arrays;
			}
			set
			{
				if (!value)
				{
					this.m_PreserveProjectedUV2Arrays = value;
					this.PreservedProjectedUV2Arrays.Clear();
					return;
				}
				if (!(this.m_Decals != null))
				{
					throw new InvalidOperationException("Unable to set preserving value while no valid decals instance is set.");
				}
				if (Edition.IsDecalSystemFree)
				{
					throw new InvalidOperationException("Preserving uv arrays is only supported in Decal System Pro.");
				}
				if (this.m_Decals.CurrentUV2Mode != UV2Mode.Project && this.m_Decals.CurrentUV2Mode != UV2Mode.ProjectWrapped)
				{
					throw new InvalidOperationException("Unable to preserve uv arrays for a decals instance that does not use them.");
				}
				this.m_PreserveProjectedUV2Arrays = value;
			}
		}

		// Token: 0x06009858 RID: 39000 RVA: 0x0005AFAB File Offset: 0x000591AB
		public void Initialize(D a_Decals)
		{
			this.m_Decals = a_Decals;
			this.PreserveVertexColorArrays = false;
			this.PreserveProjectedUVArrays = false;
			this.PreserveProjectedUV2Arrays = false;
			this.ClearAll();
		}

		// Token: 0x06009859 RID: 39001 RVA: 0x0014DAF4 File Offset: 0x0014BCF4
		protected override void RecalculateUVs()
		{
			if (!(this.m_Decals != null) || (this.m_Decals.CurrentUVMode != UVMode.Project && this.m_Decals.CurrentUVMode != UVMode.ProjectWrapped))
			{
				return;
			}
			while (this.m_UVs.Count > this.m_Vertices.Count)
			{
				this.m_UVs.RemoveAt(this.m_UVs.Count - 1);
			}
			foreach (P p in this.Projectors)
			{
				if (!p.IsUV1ProjectionCalculated)
				{
					this.CalculateProjectedUV(p);
				}
			}
		}

		// Token: 0x0600985A RID: 39002 RVA: 0x0014DBC4 File Offset: 0x0014BDC4
		protected override bool HasUV2LightmappingMode()
		{
			bool flag = false;
			if (this.m_Decals != null && this.m_Decals.CurrentUV2Mode == UV2Mode.Lightmapping)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600985B RID: 39003 RVA: 0x0014DBFC File Offset: 0x0014BDFC
		protected override void RecalculateUV2s()
		{
			if (!(this.m_Decals != null) || (this.m_Decals.CurrentUV2Mode != UV2Mode.Project && this.m_Decals.CurrentUV2Mode != UV2Mode.ProjectWrapped))
			{
				return;
			}
			while (this.m_UV2s.Count > this.m_Vertices.Count)
			{
				this.m_UV2s.RemoveAt(this.m_UV2s.Count - 1);
			}
			foreach (P p in this.Projectors)
			{
				if (!p.IsUV2ProjectionCalculated)
				{
					this.CalculateProjectedUV2(p);
				}
			}
		}

		// Token: 0x0600985C RID: 39004 RVA: 0x0014DCCC File Offset: 0x0014BECC
		protected override void RecalculateTangents()
		{
			if (!(this.m_Decals != null) || this.m_Decals.CurrentTangentsMode != TangentsMode.Project)
			{
				return;
			}
			while (this.m_Tangents.Count > this.m_Vertices.Count)
			{
				this.m_Tangents.RemoveAt(this.m_Tangents.Count - 1);
			}
			foreach (P p in this.Projectors)
			{
				if (!p.IsTangentProjectionCalculated)
				{
					this.CalculateProjectedTangents(p);
				}
			}
		}

		// Token: 0x0600985D RID: 39005 RVA: 0x0014DD88 File Offset: 0x0014BF88
		public void UpdateVertexColors(P a_Projector)
		{
			if (this.m_Decals == null)
			{
				throw new InvalidOperationException("Decals mesh is not initialized with a decals instance.");
			}
			if (!this.Projectors.Contains(a_Projector))
			{
				throw new ArgumentException("Projector is not part of the decals mesh.");
			}
			Color vertexColorTint = this.m_Decals.VertexColorTint;
			Color color = (1f - a_Projector.VertexColorBlending) * a_Projector.VertexColor;
			for (int i = a_Projector.DecalsMeshLowerVertexIndex; i <= a_Projector.DecalsMeshUpperVertexIndex; i++)
			{
				this.m_VertexColors[i] = vertexColorTint * (color + a_Projector.VertexColorBlending * this.m_TargetVertexColors[i]);
			}
		}

		// Token: 0x0600985E RID: 39006 RVA: 0x0005AFCF File Offset: 0x000591CF
		public void CalculateProjectedUV1ForActiveProjector()
		{
			this.CalculateProjectedUV(this.ActiveDecalProjector);
		}

		// Token: 0x0600985F RID: 39007 RVA: 0x0005AFE2 File Offset: 0x000591E2
		public void CalculateProjectedUV2ForActiveProjector()
		{
			this.CalculateProjectedUV2(this.ActiveDecalProjector);
		}

		// Token: 0x06009860 RID: 39008 RVA: 0x0014DE54 File Offset: 0x0014C054
		public void UpdateProjectedUV(P a_Projector)
		{
			if (this.m_Decals == null)
			{
				throw new InvalidOperationException("Decals mesh is not initialized with a decals instance.");
			}
			if (this.m_Decals.CurrentUvRectangles == null)
			{
				throw new InvalidOperationException("Empty uv rectangles.");
			}
			if (this.m_Decals.CurrentUVMode != UVMode.Project && this.m_Decals.CurrentUVMode != UVMode.ProjectWrapped)
			{
				throw new InvalidOperationException("UV mode of the decals is not projected!");
			}
			if (!this.Projectors.Contains(a_Projector))
			{
				throw new ArgumentException("Projector is not part of the decals mesh.");
			}
			this.CalculateProjectedUV(a_Projector);
		}

		// Token: 0x06009861 RID: 39009 RVA: 0x0014DEF4 File Offset: 0x0014C0F4
		public void UpdateProjectedUV2(P a_Projector)
		{
			if (this.m_Decals == null)
			{
				throw new InvalidOperationException("Decals mesh is not initialized with a decals instance.");
			}
			if (this.m_Decals.CurrentUvRectangles == null)
			{
				throw new InvalidOperationException("Empty uv rectangles.");
			}
			if (this.m_Decals.CurrentUV2Mode != UV2Mode.Project && this.m_Decals.CurrentUV2Mode != UV2Mode.ProjectWrapped)
			{
				throw new InvalidOperationException("UV2 mode of the decals is not projected!");
			}
			if (!this.Projectors.Contains(a_Projector))
			{
				throw new ArgumentException("Projector is not part of the decals mesh.");
			}
			this.CalculateProjectedUV2(a_Projector);
		}

		// Token: 0x06009862 RID: 39010 RVA: 0x0014DF94 File Offset: 0x0014C194
		private void CalculateProjectedUV(GenericDecalProjectorBase a_Projector)
		{
			Matrix4x4 matrix4x = a_Projector.WorldToProjectorMatrix * this.m_Decals.CachedTransform.localToWorldMatrix;
			UVRectangle uvrectangle = this.m_Decals.CurrentUvRectangles[a_Projector.UV1RectangleIndex];
			List<Vector2> uvs = this.m_UVs;
			int decalsMeshLowerVertexIndex = a_Projector.DecalsMeshLowerVertexIndex;
			int decalsMeshUpperVertexIndex = a_Projector.DecalsMeshUpperVertexIndex;
			if (this.m_Decals.CurrentUVMode == UVMode.Project)
			{
				this.CalculateProjectedUV(matrix4x, uvrectangle, uvs, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			}
			else
			{
				this.CalculateWrappedProjectionUV(matrix4x, uvrectangle, uvs, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			}
			a_Projector.IsUV1ProjectionCalculated = true;
		}

		// Token: 0x06009863 RID: 39011 RVA: 0x0014E024 File Offset: 0x0014C224
		private void CalculateProjectedUV2(GenericDecalProjectorBase a_Projector)
		{
			Matrix4x4 matrix4x = a_Projector.WorldToProjectorMatrix * this.m_Decals.CachedTransform.localToWorldMatrix;
			UVRectangle uvrectangle = this.m_Decals.CurrentUv2Rectangles[a_Projector.UV2RectangleIndex];
			List<Vector2> uv2s = this.m_UV2s;
			int decalsMeshLowerVertexIndex = a_Projector.DecalsMeshLowerVertexIndex;
			int decalsMeshUpperVertexIndex = a_Projector.DecalsMeshUpperVertexIndex;
			if (this.m_Decals.CurrentUVMode == UVMode.Project)
			{
				this.CalculateProjectedUV(matrix4x, uvrectangle, uv2s, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			}
			else
			{
				this.CalculateWrappedProjectionUV(matrix4x, uvrectangle, uv2s, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			}
			a_Projector.IsUV2ProjectionCalculated = true;
		}

		// Token: 0x06009864 RID: 39012 RVA: 0x0014E0B4 File Offset: 0x0014C2B4
		private void CalculateProjectedUV(Matrix4x4 a_DecalsToProjectorMatrix, UVRectangle a_UVRectangle, List<Vector2> a_UVs, int a_LowerIndex, int a_UpperIndex)
		{
			Vector2 lowerLeftUV = a_UVRectangle.lowerLeftUV;
			Vector2 size = a_UVRectangle.Size;
			while (a_UVs.Count < a_LowerIndex)
			{
				a_UVs.Add(Vector2.zero);
			}
			for (int i = a_LowerIndex; i <= a_UpperIndex; i++)
			{
				Vector3 vector = base.Vertices[i];
				vector = a_DecalsToProjectorMatrix.MultiplyPoint3x4(vector);
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				vector2.x = lowerLeftUV.x + (vector2.x + 0.5f) * size.x;
				vector2.y = lowerLeftUV.y + (vector2.y + 0.5f) * size.y;
				if (i < a_UVs.Count)
				{
					a_UVs[i] = vector2;
				}
				else
				{
					a_UVs.Add(vector2);
				}
			}
		}

		// Token: 0x06009865 RID: 39013 RVA: 0x0014E184 File Offset: 0x0014C384
		private void CalculateWrappedProjectionUV(Matrix4x4 a_DecalsToProjectorMatrix, UVRectangle a_UVRectangle, List<Vector2> a_UVs, int a_LowerIndex, int a_UpperIndex)
		{
			Vector2 lowerLeftUV = a_UVRectangle.lowerLeftUV;
			Vector2 size = a_UVRectangle.Size;
			while (a_UVs.Count < a_LowerIndex)
			{
				a_UVs.Add(Vector2.zero);
			}
			for (int i = a_LowerIndex; i <= a_UpperIndex; i++)
			{
				Vector3 vector = a_DecalsToProjectorMatrix.MultiplyPoint3x4(base.Vertices[i]);
				Vector3 vector2 = a_DecalsToProjectorMatrix.MultiplyVector(base.Normals[i]);
				Vector2 vector3 = new Vector2(vector.x, vector.z);
				vector3 -= vector.y * new Vector2(vector2.x, vector2.z);
				vector3.x = Mathf.Clamp(vector3.x, -0.5f, 0.5f);
				vector3.y = Mathf.Clamp(vector3.y, -0.5f, 0.5f);
				vector3.x = lowerLeftUV.x + (vector3.x + 0.5f) * size.x;
				vector3.y = lowerLeftUV.y + (vector3.y + 0.5f) * size.y;
				if (i < a_UVs.Count)
				{
					a_UVs[i] = vector3;
				}
				else
				{
					a_UVs.Add(vector3);
				}
			}
		}

		// Token: 0x06009866 RID: 39014 RVA: 0x0014E2C8 File Offset: 0x0014C4C8
		private void CalculateProjectedTangents(GenericDecalProjectorBase a_Projector)
		{
			while (this.m_Tangents.Count < a_Projector.DecalsMeshLowerVertexIndex)
			{
				this.m_Tangents.Add(Vector4.zero);
			}
			Matrix4x4 transpose = (a_Projector.WorldToProjectorMatrix * this.m_Decals.CachedTransform.localToWorldMatrix).inverse.transpose;
			Matrix4x4 transpose2 = (this.m_Decals.CachedTransform.worldToLocalMatrix * a_Projector.ProjectorToWorldMatrix).inverse.transpose;
			for (int i = a_Projector.DecalsMeshLowerVertexIndex; i <= a_Projector.DecalsMeshUpperVertexIndex; i++)
			{
				Vector3 vector = base.Normals[i];
				vector = transpose.MultiplyVector(vector);
				vector.z = 0f;
				if (Mathf.Approximately(vector.x, 0f) && Mathf.Approximately(vector.y, 0f))
				{
					vector = new Vector3(0f, 1f, 0f);
				}
				vector = new Vector3(vector.y, 0f - vector.x, vector.z);
				vector = transpose2.MultiplyVector(vector);
				vector.Normalize();
				Vector4 vector2 = new Vector4(vector.x, vector.y, vector.z, -1f);
				if (i < this.m_Tangents.Count)
				{
					this.m_Tangents[i] = vector2;
				}
				else
				{
					this.m_Tangents.Add(vector2);
				}
			}
		}

		// Token: 0x06009867 RID: 39015 RVA: 0x0014E45C File Offset: 0x0014C65C
		public virtual void ClearAll()
		{
			foreach (P p in this.Projectors)
			{
				p.ResetDecalsMesh();
			}
			this.Projectors.Clear();
			base.Vertices.Clear();
			base.Normals.Clear();
			base.Tangents.Clear();
			base.TargetVertexColors.Clear();
			base.VertexColors.Clear();
			base.UVs.Clear();
			base.UV2s.Clear();
			base.Triangles.Clear();
			if (this.m_Decals != null)
			{
				this.m_Decals.LinkedDecalsMesh = null;
			}
		}

		// Token: 0x06009868 RID: 39016 RVA: 0x0005AFF5 File Offset: 0x000591F5
		public bool ContainsProjector(P a_Projector)
		{
			return this.Projectors.Contains(a_Projector);
		}

		// Token: 0x06009869 RID: 39017 RVA: 0x0014E538 File Offset: 0x0014C738
		public void AddProjector(P a_Projector)
		{
			if (a_Projector == null)
			{
				throw new ArgumentNullException("Projector parameter is not allowed to be null!");
			}
			if (a_Projector.DecalsMesh != null)
			{
				throw new InvalidOperationException("Projector is already used in this or another instance!");
			}
			if (this.m_Decals == null)
			{
				throw new NullReferenceException("Projectors can only be added if the decals is not null!");
			}
			if (this.m_Decals.LinkedDecalsMesh != null && this.m_Decals.LinkedDecalsMesh != this)
			{
				throw new InvalidOperationException("The decals instance is already linked to another decals mesh.");
			}
			P activeDecalProjector = this.ActiveDecalProjector;
			if (activeDecalProjector != null)
			{
				activeDecalProjector.IsActiveProjector = false;
			}
			this.Projectors.Add(a_Projector);
			a_Projector.DecalsMesh = this as DM;
			a_Projector.IsActiveProjector = true;
			this.SetRangesForAddedProjector(a_Projector);
			this.m_Decals.LinkedDecalsMesh = this;
		}

		// Token: 0x0600986A RID: 39018 RVA: 0x0014E624 File Offset: 0x0014C824
		internal virtual void SetRangesForAddedProjector(P a_Projector)
		{
			a_Projector.DecalsMeshLowerVertexIndex = this.m_Vertices.Count;
			a_Projector.DecalsMeshUpperVertexIndex = this.m_Vertices.Count - 1;
			a_Projector.DecalsMeshLowerTriangleIndex = this.m_Triangles.Count;
			a_Projector.DecalsMeshUpperTriangleIndex = this.m_Triangles.Count - 1;
		}

		// Token: 0x0600986B RID: 39019 RVA: 0x0014E690 File Offset: 0x0014C890
		public void RemoveProjector(P a_Projector)
		{
			if (a_Projector.DecalsMesh != this)
			{
				throw new InvalidOperationException("Unable to remove a projector that is not part of this instance.");
			}
			int decalsMeshLowerVertexIndex = a_Projector.DecalsMeshLowerVertexIndex;
			int decalsMeshUpperVertexIndex = a_Projector.DecalsMeshUpperVertexIndex;
			int decalsMeshVertexCount = a_Projector.DecalsMeshVertexCount;
			int decalsMeshLowerTriangleIndex = a_Projector.DecalsMeshLowerTriangleIndex;
			int decalsMeshTriangleCount = a_Projector.DecalsMeshTriangleCount;
			if (decalsMeshTriangleCount > 0)
			{
				this.m_Triangles.RemoveRange(decalsMeshLowerTriangleIndex, decalsMeshTriangleCount);
			}
			for (int i = decalsMeshLowerTriangleIndex; i < this.m_Triangles.Count; i++)
			{
				int num = this.m_Triangles[i];
				if (num > decalsMeshUpperVertexIndex)
				{
					this.m_Triangles[i] = num - decalsMeshVertexCount;
				}
			}
			this.RemoveRangesOfVertexAlignedLists(decalsMeshLowerVertexIndex, decalsMeshVertexCount);
			int num2 = this.BoneIndexOffset(a_Projector);
			this.RemoveBonesAndAdjustBoneWeightIndices(a_Projector);
			if (a_Projector.IsActiveProjector)
			{
				this.Projectors.RemoveAt(this.Projectors.Count - 1);
			}
			else
			{
				int num3 = this.Projectors.IndexOf(a_Projector);
				for (int j = num3 + 1; j < this.Projectors.Count; j++)
				{
					P p = this.Projectors[j];
					this.AdjustProjectorIndices(p, decalsMeshVertexCount, decalsMeshTriangleCount, num2);
				}
				this.Projectors.RemoveAt(num3);
			}
			a_Projector.ResetDecalsMesh();
			if (this.Projectors.Count == 0)
			{
				this.m_Decals.LinkedDecalsMesh = null;
			}
		}

		// Token: 0x0600986C RID: 39020 RVA: 0x0014E808 File Offset: 0x0014CA08
		internal virtual void RemoveRangesOfVertexAlignedLists(int a_LowerIndex, int a_Count)
		{
			this.m_Vertices.RemoveRange(a_LowerIndex, a_Count);
			if (a_LowerIndex < this.m_Normals.Count)
			{
				this.m_Normals.RemoveRange(a_LowerIndex, a_Count);
			}
			if (a_LowerIndex < this.m_UVs.Count)
			{
				this.m_UVs.RemoveRange(a_LowerIndex, a_Count);
			}
			if (a_LowerIndex < this.m_UV2s.Count)
			{
				this.m_UV2s.RemoveRange(a_LowerIndex, a_Count);
			}
			if (a_LowerIndex < this.m_Tangents.Count)
			{
				this.m_Tangents.RemoveRange(a_LowerIndex, a_Count);
			}
			if (a_LowerIndex < this.m_TargetVertexColors.Count)
			{
				this.m_TargetVertexColors.RemoveRange(a_LowerIndex, a_Count);
			}
			if (a_LowerIndex < this.m_VertexColors.Count)
			{
				this.m_VertexColors.RemoveRange(a_LowerIndex, a_Count);
			}
		}

		// Token: 0x0600986D RID: 39021 RVA: 0x00007F86 File Offset: 0x00006186
		internal virtual int BoneIndexOffset(P a_Projector)
		{
			return 0;
		}

		// Token: 0x0600986E RID: 39022 RVA: 0x0000568E File Offset: 0x0000388E
		internal virtual void RemoveBonesAndAdjustBoneWeightIndices(P a_Projector)
		{
		}

		// Token: 0x0600986F RID: 39023 RVA: 0x0014E8C4 File Offset: 0x0014CAC4
		internal virtual void AdjustProjectorIndices(P a_Projector, int a_VertexIndexOffset, int a_TriangleIndexOffset, int a_BoneIndexOffset)
		{
			ref P ptr = ref a_Projector;
			ptr.DecalsMeshLowerVertexIndex -= a_VertexIndexOffset;
			ptr = ref a_Projector;
			ptr.DecalsMeshUpperVertexIndex -= a_VertexIndexOffset;
			ptr = ref a_Projector;
			ptr.DecalsMeshLowerTriangleIndex -= a_TriangleIndexOffset;
			ptr = ref a_Projector;
			ptr.DecalsMeshUpperTriangleIndex -= a_TriangleIndexOffset;
		}

		// Token: 0x06009870 RID: 39024
		public abstract void OffsetActiveProjectorVertices();

		// Token: 0x06009871 RID: 39025 RVA: 0x0014E958 File Offset: 0x0014CB58
		public void SmoothActiveProjectorNormals(float a_SmoothFactor)
		{
			P activeDecalProjector = this.ActiveDecalProjector;
			if (activeDecalProjector == null)
			{
				throw new InvalidOperationException("There is no active projector.");
			}
			bool flag = a_SmoothFactor < 0f || a_SmoothFactor > 1f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("The smooth factor has to be in [0.0f, 1.0f].");
			}
			int decalsMeshLowerVertexIndex = activeDecalProjector.DecalsMeshLowerVertexIndex;
			int decalsMeshUpperVertexIndex = activeDecalProjector.DecalsMeshUpperVertexIndex;
			Vector3 vector = activeDecalProjector.Rotation * Vector3.up;
			for (int i = decalsMeshLowerVertexIndex; i <= decalsMeshUpperVertexIndex; i++)
			{
				Vector3 vector2 = this.m_Normals[i];
				vector2 = Vector3.Lerp(vector2, vector, a_SmoothFactor);
				vector2.Normalize();
				this.m_Normals[i] = vector2;
			}
		}

		// Token: 0x06009872 RID: 39026 RVA: 0x0005B003 File Offset: 0x00059203
		internal void RemoveTriangleAt(int a_Index)
		{
			this.m_Triangles.RemoveRange(a_Index, 3);
		}

		// Token: 0x06009873 RID: 39027 RVA: 0x0005B012 File Offset: 0x00059212
		internal void RemoveTrianglesAt(int a_Index, int a_Count)
		{
			this.m_Triangles.RemoveRange(a_Index, 3 * a_Count);
		}

		// Token: 0x06009874 RID: 39028 RVA: 0x0005B023 File Offset: 0x00059223
		internal void RemoveAndAdjustIndices(int a_LowerTriangleIndex, RemovedIndices a_RemovedIndices)
		{
			this.AdjustTriangleIndices(a_LowerTriangleIndex, a_RemovedIndices);
			this.RemoveIndices(a_RemovedIndices);
		}

		// Token: 0x06009875 RID: 39029 RVA: 0x0014EA10 File Offset: 0x0014CC10
		private void AdjustTriangleIndices(int a_LowerTriangleIndex, RemovedIndices a_RemovedIndices)
		{
			for (int i = a_LowerTriangleIndex; i < this.m_Triangles.Count; i++)
			{
				this.m_Triangles[i] = a_RemovedIndices.AdjustedIndex(this.m_Triangles[i]);
			}
			this.ActiveDecalProjector.DecalsMeshUpperTriangleIndex = this.m_Triangles.Count - 1;
		}

		// Token: 0x06009876 RID: 39030 RVA: 0x0014EA70 File Offset: 0x0014CC70
		internal void RemoveIndices(RemovedIndices a_RemovedIndices)
		{
			int num = -1;
			int num2 = 0;
			for (int i = this.m_Vertices.Count - 1; i >= 0; i--)
			{
				bool flag = a_RemovedIndices.IsRemovedIndex(i);
				if (flag)
				{
					if (num == -1)
					{
						num = i;
						num2 = 1;
					}
					else
					{
						num = i;
						num2++;
					}
				}
				if ((!flag || i == 0) && num != -1)
				{
					this.RemoveRange(num, num2);
					num = -1;
					num2 = 0;
				}
			}
			this.ActiveDecalProjector.DecalsMeshUpperVertexIndex = this.m_Vertices.Count - 1;
		}

		// Token: 0x06009877 RID: 39031
		internal abstract void RemoveRange(int a_StartIndex, int a_Count);

		// Token: 0x040063FC RID: 25596
		protected D m_Decals;

		// Token: 0x040063FD RID: 25597
		private bool m_PreserveProjectedUV2Arrays;

		// Token: 0x040063FE RID: 25598
		private bool m_PreserveProjectedUVArrays;

		// Token: 0x040063FF RID: 25599
		private bool m_PreserveVertexColorArrays;
	}
}
