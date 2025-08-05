using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B18 RID: 11032
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public class SkinnedDecalsMesh : GenericDecalsMesh<SkinnedDecals, SkinnedDecalProjectorBase, SkinnedDecalsMesh>
	{
		// Token: 0x060098FC RID: 39164 RVA: 0x0005B507 File Offset: 0x00059707
		public SkinnedDecalsMesh()
		{
		}

		// Token: 0x060098FD RID: 39165 RVA: 0x0005B53B File Offset: 0x0005973B
		public SkinnedDecalsMesh(SkinnedDecals a_Decals)
		{
			this.m_Decals = a_Decals;
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x060098FE RID: 39166 RVA: 0x0005B576 File Offset: 0x00059776
		public List<Vector3> OriginalVertices { get; } = new List<Vector3>();

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x060098FF RID: 39167 RVA: 0x0005B57E File Offset: 0x0005977E
		public List<BoneWeight> BoneWeights { get; } = new List<BoneWeight>();

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06009900 RID: 39168 RVA: 0x0005B586 File Offset: 0x00059786
		public List<Transform> Bones { get; } = new List<Transform>();

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06009901 RID: 39169 RVA: 0x0005B58E File Offset: 0x0005978E
		public List<Matrix4x4> BindPoses { get; } = new List<Matrix4x4>();

		// Token: 0x06009902 RID: 39170 RVA: 0x0005B596 File Offset: 0x00059796
		internal override void SetRangesForAddedProjector(SkinnedDecalProjectorBase a_Projector)
		{
			base.SetRangesForAddedProjector(a_Projector);
			a_Projector.DecalsMeshLowerBonesIndex = this.Bones.Count;
			a_Projector.DecalsMeshUpperBonesIndex = this.Bones.Count - 1;
		}

		// Token: 0x06009903 RID: 39171 RVA: 0x001518D8 File Offset: 0x0014FAD8
		public void Add(Mesh a_Mesh, Transform[] a_Bones, SkinQuality a_SkinQuality, Matrix4x4 a_WorldToMeshMatrix, Matrix4x4 a_MeshToWorldMatrix)
		{
			SkinnedDecalProjectorBase activeDecalProjector = base.ActiveDecalProjector;
			if (activeDecalProjector == null)
			{
				throw new NullReferenceException("The active decal projector is not allowed to be null as mesh data should be added!");
			}
			UVMode currentUVMode = this.m_Decals.CurrentUVMode;
			bool flag = currentUVMode == UVMode.Project || currentUVMode == UVMode.ProjectWrapped;
			if (flag)
			{
				if (Edition.IsDecalSystemFree && this.m_Decals.CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					throw new InvalidOperationException("Texture atlas references can only be used with Decal System Pro.");
				}
				if (0 > activeDecalProjector.UV1RectangleIndex || activeDecalProjector.UV1RectangleIndex >= this.m_Decals.uvRectangles.Length)
				{
					throw new IndexOutOfRangeException("The uv rectangle index of the active projector is not a valid index within the decals uv rectangles array!");
				}
			}
			UV2Mode currentUV2Mode = this.m_Decals.CurrentUV2Mode;
			flag = currentUV2Mode == UV2Mode.Project || currentUV2Mode == UV2Mode.ProjectWrapped;
			if (flag)
			{
				if (Edition.IsDecalSystemFree && this.m_Decals.CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					throw new InvalidOperationException("Texture atlas references can only be used with Decal System Pro.");
				}
				if (0 > activeDecalProjector.UV2RectangleIndex || activeDecalProjector.UV2RectangleIndex >= this.m_Decals.uv2Rectangles.Length)
				{
					throw new IndexOutOfRangeException("The uv2 rectangle index of the active projector is not a valid index within the decals uv2 rectangles array!");
				}
			}
			if (a_Mesh == null)
			{
				throw new ArgumentNullException("The mesh parameter is not allowed to be null!");
			}
			if (!a_Mesh.isReadable)
			{
				throw new ArgumentException("The mesh is not readable!");
			}
			Vector3[] vertices = a_Mesh.vertices;
			Vector3[] array = a_Mesh.normals;
			Vector4[] array2 = null;
			Vector2[] array3 = null;
			Vector2[] array4 = null;
			BoneWeight[] boneWeights = a_Mesh.boneWeights;
			int[] triangles = a_Mesh.triangles;
			Matrix4x4[] bindposes = a_Mesh.bindposes;
			if (triangles == null)
			{
				throw new NullReferenceException("The triangles in the mesh are null!");
			}
			if (a_Bones == null)
			{
				throw new NullReferenceException("The bones are null!");
			}
			if (bindposes == null)
			{
				throw new NullReferenceException("The bind poses in the mesh are null!");
			}
			if (a_Bones.Length != bindposes.Length)
			{
				throw new FormatException("The number of bind poses in the mesh does not match the number of bones!");
			}
			if (vertices == null)
			{
				return;
			}
			bool flag2 = false;
			if (array == null || array.Length == 0)
			{
				flag2 = true;
				a_Mesh.RecalculateNormals();
				array = a_Mesh.normals;
			}
			else if (vertices.Length != array.Length)
			{
				throw new FormatException("The number of vertices in the mesh does not match the number of normals!");
			}
			if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
			{
				array2 = a_Mesh.tangents;
				if (array2 == null)
				{
					throw new NullReferenceException("The tangents in the mesh are null!");
				}
				if (vertices.Length != array2.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of tangents!");
				}
			}
			if (this.m_Decals.UseVertexColors)
			{
				Color[] colors = a_Mesh.colors;
				if (colors != null && colors.Length != 0 && vertices.Length != colors.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of colors!");
				}
			}
			if (this.m_Decals.CurrentUVMode == UVMode.TargetUV)
			{
				array3 = a_Mesh.uv;
				if (array3 == null)
				{
					throw new NullReferenceException("The uv's in the mesh are null!");
				}
				if (vertices.Length != array3.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of uv's!");
				}
			}
			else if (this.m_Decals.CurrentUVMode == UVMode.TargetUV2)
			{
				array3 = a_Mesh.uv2;
				if (array3 == null)
				{
					throw new NullReferenceException("The uv2's in the mesh are null!");
				}
				if (vertices.Length != array3.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of uv2's!");
				}
			}
			if (this.m_Decals.CurrentUV2Mode == UV2Mode.TargetUV)
			{
				array4 = a_Mesh.uv;
				if (array4 == null)
				{
					throw new NullReferenceException("The uv's in the mesh are null!");
				}
				if (vertices.Length != array4.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of uv's!");
				}
			}
			else if (this.m_Decals.CurrentUV2Mode == UV2Mode.TargetUV2)
			{
				array4 = a_Mesh.uv2;
				if (array4 == null)
				{
					throw new NullReferenceException("The uv2's in the mesh are null!");
				}
				if (vertices.Length != array4.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of uv2's!");
				}
			}
			this.Add(vertices, array, array2, array3, array4, boneWeights, triangles, a_Bones, bindposes, a_SkinQuality, a_WorldToMeshMatrix, a_MeshToWorldMatrix);
			if (flag2)
			{
				a_Mesh.normals = null;
			}
		}

		// Token: 0x06009904 RID: 39172 RVA: 0x00151C0C File Offset: 0x0014FE0C
		public void Add(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Vector2[] a_UVs, Vector2[] a_UV2s, BoneWeight[] a_BoneWeights, int[] a_Triangles, Transform[] a_Bones, Matrix4x4[] a_BindPoses, SkinQuality a_SkinQuality, Matrix4x4 a_WorldToMeshMatrix, Matrix4x4 a_MeshToWorldMatrix)
		{
			this.Add(a_Vertices, a_Normals, a_Tangents, null, a_UVs, a_UV2s, a_BoneWeights, a_Triangles, a_Bones, a_BindPoses, a_SkinQuality, a_WorldToMeshMatrix, a_MeshToWorldMatrix);
		}

		// Token: 0x06009905 RID: 39173 RVA: 0x00151C38 File Offset: 0x0014FE38
		public void Add(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Color[] a_Colors, Vector2[] a_UVs, Vector2[] a_UV2s, BoneWeight[] a_BoneWeights, int[] a_Triangles, Transform[] a_Bones, Matrix4x4[] a_BindPoses, SkinQuality a_SkinQuality, Matrix4x4 a_WorldToMeshMatrix, Matrix4x4 a_MeshToWorldMatrix)
		{
			SkinnedDecalProjectorBase activeDecalProjector = base.ActiveDecalProjector;
			if (activeDecalProjector == null)
			{
				throw new NullReferenceException("The active decal projector is not allowed to be null as mesh data should be added!");
			}
			UVMode uvmode = this.m_Decals.CurrentUVMode;
			bool flag = uvmode == UVMode.Project || uvmode == UVMode.ProjectWrapped;
			if (flag)
			{
				if (Edition.IsDecalSystemFree && this.m_Decals.CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					throw new InvalidOperationException("Texture atlas references can only be used with Decal System Pro.");
				}
				if (0 > activeDecalProjector.UV1RectangleIndex || activeDecalProjector.UV1RectangleIndex >= this.m_Decals.uvRectangles.Length)
				{
					throw new IndexOutOfRangeException("The uv rectangle index of the active projector is not a valid index within the decals uv rectangles array!");
				}
			}
			UV2Mode uv2Mode = this.m_Decals.CurrentUV2Mode;
			flag = uv2Mode == UV2Mode.Project || uv2Mode == UV2Mode.ProjectWrapped;
			if (flag)
			{
				if (Edition.IsDecalSystemFree && this.m_Decals.CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					throw new InvalidOperationException("Texture atlas references can only be used with Decal System Pro.");
				}
				if (0 > activeDecalProjector.UV2RectangleIndex || activeDecalProjector.UV2RectangleIndex >= this.m_Decals.uv2Rectangles.Length)
				{
					throw new IndexOutOfRangeException("The uv2 rectangle index of the active projector is not a valid index within the decals uv2 rectangles array!");
				}
			}
			if (this.m_Decals.UseVertexColors && Edition.IsDecalSystemFree)
			{
				throw new InvalidOperationException("Vertex colors are only supported in Decal System Pro.");
			}
			if (a_Vertices == null)
			{
				throw new ArgumentNullException("The vertices parameter is not allowed to be null!");
			}
			if (a_Normals == null)
			{
				throw new ArgumentNullException("The normals parameter is not allowed to be null!");
			}
			if (a_Triangles == null)
			{
				throw new ArgumentNullException("The triangles parameter is not allowed to be null!");
			}
			if (a_Vertices.Length != a_Normals.Length)
			{
				throw new FormatException("The number of vertices does not match the number of normals!");
			}
			if (a_Bones == null)
			{
				throw new NullReferenceException("The bones are null!");
			}
			if (a_BindPoses == null)
			{
				throw new NullReferenceException("The bind poses are null!");
			}
			if (a_Bones.Length != a_BindPoses.Length)
			{
				throw new FormatException("The number of bind poses does not match the number of bones!");
			}
			if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
			{
				if (a_Tangents == null)
				{
					throw new ArgumentNullException("The tangents parameter is not allowed to be null!");
				}
				if (a_Vertices.Length != a_Tangents.Length)
				{
					throw new FormatException("The number of vertices does not match the number of tangents!");
				}
			}
			uvmode = this.m_Decals.CurrentUVMode;
			flag = uvmode - UVMode.TargetUV <= 1;
			if (flag)
			{
				if (a_UVs == null)
				{
					throw new ArgumentNullException("The uv parameter is not allowed to be null!");
				}
				if (a_Vertices.Length != a_UVs.Length)
				{
					throw new FormatException("The number of vertices does not match the number of uv's!");
				}
			}
			else
			{
				uv2Mode = this.m_Decals.CurrentUV2Mode;
				flag = uv2Mode - UV2Mode.TargetUV <= 1;
				if (flag)
				{
					if (a_UV2s == null)
					{
						throw new NullReferenceException("The uv2 parameter is not allowed to be null!");
					}
					if (a_Vertices.Length != a_UV2s.Length)
					{
						throw new FormatException("The number of vertices does not match the number of uv2's!");
					}
				}
			}
			if (this.m_Decals.UseVertexColors && a_Colors != null && a_Colors.Length != 0 && a_Vertices.Length != a_Colors.Length)
			{
				throw new FormatException("The number of vertices does not macht the number of colors!");
			}
			Vector3 normalized = new Vector3(0f, 1f, 0f);
			Matrix4x4 worldToLocalMatrix = this.m_Decals.CachedTransform.worldToLocalMatrix;
			normalized = (worldToLocalMatrix * activeDecalProjector.ProjectorToWorldMatrix).inverse.transpose.MultiplyVector(normalized).normalized;
			Matrix4x4 matrix4x = worldToLocalMatrix * a_MeshToWorldMatrix;
			Matrix4x4 transpose = matrix4x.inverse.transpose;
			float num = Mathf.Cos(activeDecalProjector.CullingAngle * 0.017453292f);
			int count = this.m_Vertices.Count;
			int count2 = this.Bones.Count;
			for (int i = 0; i < a_Bones.Length; i++)
			{
				this.Bones.Add(a_Bones[i]);
				this.BindPoses.Add(a_BindPoses[i] * matrix4x.inverse);
			}
			this.InternalAdd(a_Vertices, a_Normals, a_Tangents, a_Colors, a_UVs, a_UV2s, a_BoneWeights, a_Triangles, count2, a_BindPoses, a_SkinQuality, normalized, num, worldToLocalMatrix, matrix4x, transpose);
			if (a_WorldToMeshMatrix.Determinant() >= 0f)
			{
				for (int j = 0; j < a_Triangles.Length; j += 3)
				{
					int num2 = a_Triangles[j];
					int num3 = a_Triangles[j + 1];
					int num4 = a_Triangles[j + 2];
					if (!this.m_RemovedIndices.IsRemovedIndex(num2) && !this.m_RemovedIndices.IsRemovedIndex(num3) && !this.m_RemovedIndices.IsRemovedIndex(num4))
					{
						num2 = count + this.m_RemovedIndices.AdjustedIndex(num2);
						num3 = count + this.m_RemovedIndices.AdjustedIndex(num3);
						num4 = count + this.m_RemovedIndices.AdjustedIndex(num4);
						this.m_Triangles.Add(num2);
						this.m_Triangles.Add(num3);
						this.m_Triangles.Add(num4);
					}
				}
			}
			else
			{
				for (int k = 0; k < a_Triangles.Length; k += 3)
				{
					int num5 = a_Triangles[k];
					int num6 = a_Triangles[k + 1];
					int num7 = a_Triangles[k + 2];
					if (!this.m_RemovedIndices.IsRemovedIndex(num5) && !this.m_RemovedIndices.IsRemovedIndex(num6) && !this.m_RemovedIndices.IsRemovedIndex(num7))
					{
						num5 = count + this.m_RemovedIndices.AdjustedIndex(num5);
						num6 = count + this.m_RemovedIndices.AdjustedIndex(num6);
						num7 = count + this.m_RemovedIndices.AdjustedIndex(num7);
						this.m_Triangles.Add(num6);
						this.m_Triangles.Add(num5);
						this.m_Triangles.Add(num7);
					}
				}
			}
			this.m_RemovedIndices.Clear();
			activeDecalProjector.DecalsMeshUpperVertexIndex = this.m_Vertices.Count - 1;
			activeDecalProjector.DecalsMeshUpperTriangleIndex = this.m_Triangles.Count - 1;
			activeDecalProjector.DecalsMeshUpperBonesIndex = this.Bones.Count - 1;
			activeDecalProjector.IsTangentProjectionCalculated = false;
			activeDecalProjector.IsUV1ProjectionCalculated = false;
			activeDecalProjector.IsUV2ProjectionCalculated = false;
		}

		// Token: 0x06009906 RID: 39174 RVA: 0x0005B5C3 File Offset: 0x000597C3
		internal override void RemoveRangesOfVertexAlignedLists(int a_LowerIndex, int a_Count)
		{
			base.RemoveRangesOfVertexAlignedLists(a_LowerIndex, a_Count);
			this.OriginalVertices.RemoveRange(a_LowerIndex, a_Count);
			this.BoneWeights.RemoveRange(a_LowerIndex, a_Count);
		}

		// Token: 0x06009907 RID: 39175 RVA: 0x0005B5E7 File Offset: 0x000597E7
		internal override int BoneIndexOffset(SkinnedDecalProjectorBase a_Projector)
		{
			return a_Projector.DecalsMeshBonesCount;
		}

		// Token: 0x06009908 RID: 39176 RVA: 0x0015218C File Offset: 0x0015038C
		internal override void RemoveBonesAndAdjustBoneWeightIndices(SkinnedDecalProjectorBase a_Projector)
		{
			int decalsMeshLowerBonesIndex = a_Projector.DecalsMeshLowerBonesIndex;
			int decalsMeshUpperBonesIndex = a_Projector.DecalsMeshUpperBonesIndex;
			int decalsMeshBonesCount = a_Projector.DecalsMeshBonesCount;
			if (decalsMeshBonesCount > 0)
			{
				this.Bones.RemoveRange(decalsMeshLowerBonesIndex, decalsMeshBonesCount);
				this.BindPoses.RemoveRange(decalsMeshLowerBonesIndex, decalsMeshBonesCount);
			}
			for (int i = a_Projector.DecalsMeshLowerVertexIndex; i < this.BoneWeights.Count; i++)
			{
				BoneWeight boneWeight = this.BoneWeights[i];
				boneWeight.boneIndex0 = this.AdjustedIndex(boneWeight.boneIndex0, decalsMeshUpperBonesIndex, decalsMeshBonesCount);
				boneWeight.boneIndex1 = this.AdjustedIndex(boneWeight.boneIndex1, decalsMeshUpperBonesIndex, decalsMeshBonesCount);
				boneWeight.boneIndex2 = this.AdjustedIndex(boneWeight.boneIndex2, decalsMeshUpperBonesIndex, decalsMeshBonesCount);
				boneWeight.boneIndex3 = this.AdjustedIndex(boneWeight.boneIndex3, decalsMeshUpperBonesIndex, decalsMeshBonesCount);
				this.BoneWeights[i] = boneWeight;
			}
		}

		// Token: 0x06009909 RID: 39177 RVA: 0x00152260 File Offset: 0x00150460
		private int AdjustedIndex(int a_Index, int a_UpperIndex, int a_Offset)
		{
			int num = a_Index;
			if (num > a_UpperIndex)
			{
				num -= a_Offset;
			}
			return num;
		}

		// Token: 0x0600990A RID: 39178 RVA: 0x0005B5EF File Offset: 0x000597EF
		internal override void AdjustProjectorIndices(SkinnedDecalProjectorBase a_Projector, int a_VertexIndexOffset, int a_TriangleIndexOffset, int a_BoneIndexOffset)
		{
			base.AdjustProjectorIndices(a_Projector, a_VertexIndexOffset, a_TriangleIndexOffset, a_BoneIndexOffset);
			a_Projector.DecalsMeshLowerBonesIndex -= a_BoneIndexOffset;
			a_Projector.DecalsMeshUpperBonesIndex -= a_BoneIndexOffset;
		}

		// Token: 0x0600990B RID: 39179 RVA: 0x0005B61A File Offset: 0x0005981A
		public override void ClearAll()
		{
			base.ClearAll();
			this.OriginalVertices.Clear();
			this.BoneWeights.Clear();
			this.Bones.Clear();
			this.BindPoses.Clear();
		}

		// Token: 0x0600990C RID: 39180 RVA: 0x00152278 File Offset: 0x00150478
		private void InternalAdd(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Color[] a_Colors, Vector2[] a_UVs, Vector2[] a_UV2s, BoneWeight[] a_BoneWeights, int[] a_Triangles, int a_BoneIndexOffset, Matrix4x4[] a_BindPoses, SkinQuality a_SkinQuality, Vector3 a_ReversedProjectionNormal, float a_CullingDotProduct, Matrix4x4 a_WorldToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrixInvertedTransposed)
		{
			if (a_SkinQuality == SkinQuality.Bone1 || (a_SkinQuality == SkinQuality.Auto && QualitySettings.blendWeights == BlendWeights.OneBone))
			{
				this.AddSkinQuality1(a_Vertices, a_Normals, a_Tangents, a_Colors, a_UVs, a_UV2s, a_BoneWeights, a_Triangles, a_BoneIndexOffset, a_BindPoses, a_ReversedProjectionNormal, a_CullingDotProduct, a_WorldToDecalsMatrix, a_MeshToDecalsMatrix, a_MeshToDecalsMatrixInvertedTransposed);
				return;
			}
			if (a_SkinQuality == SkinQuality.Bone2 || (a_SkinQuality == SkinQuality.Auto && QualitySettings.blendWeights == BlendWeights.TwoBones))
			{
				this.AddSkinQuality2(a_Vertices, a_Normals, a_Tangents, a_Colors, a_UVs, a_UV2s, a_BoneWeights, a_Triangles, a_BoneIndexOffset, a_BindPoses, a_ReversedProjectionNormal, a_CullingDotProduct, a_WorldToDecalsMatrix, a_MeshToDecalsMatrix, a_MeshToDecalsMatrixInvertedTransposed);
				return;
			}
			if (a_SkinQuality != SkinQuality.Auto)
			{
				if (a_SkinQuality != SkinQuality.Bone4)
				{
					return;
				}
			}
			else if (QualitySettings.blendWeights != BlendWeights.FourBones)
			{
				return;
			}
			this.AddSkinQuality4(a_Vertices, a_Normals, a_Tangents, a_Colors, a_UVs, a_UV2s, a_BoneWeights, a_Triangles, a_BoneIndexOffset, a_BindPoses, a_ReversedProjectionNormal, a_CullingDotProduct, a_WorldToDecalsMatrix, a_MeshToDecalsMatrix, a_MeshToDecalsMatrixInvertedTransposed);
		}

		// Token: 0x0600990D RID: 39181 RVA: 0x00152320 File Offset: 0x00150520
		private void AddSkinQuality1(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Color[] a_Colors, Vector2[] a_UVs, Vector2[] a_UV2s, BoneWeight[] a_BoneWeights, int[] a_Triangles, int a_BoneIndexOffset, Matrix4x4[] a_BindPoses, Vector3 a_ReversedProjectionNormal, float a_CullingDotProduct, Matrix4x4 a_WorldToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrixInvertedTransposed)
		{
			Color vertexColorTint = this.m_Decals.VertexColorTint;
			Color color = (1f - base.ActiveDecalProjector.VertexColorBlending) * base.ActiveDecalProjector.VertexColor;
			for (int i = 0; i < a_Vertices.Length; i++)
			{
				BoneWeight boneWeight = a_BoneWeights[i];
				boneWeight.boneIndex0 += a_BoneIndexOffset;
				boneWeight.weight0 = 1f;
				boneWeight.weight1 = 0f;
				boneWeight.weight2 = 0f;
				boneWeight.weight3 = 0f;
				Matrix4x4 matrix4x = this.Bones[boneWeight.boneIndex0].localToWorldMatrix * this.BindPoses[boneWeight.boneIndex0];
				Matrix4x4 matrix4x2 = default(Matrix4x4);
				for (int j = 0; j < 16; j++)
				{
					matrix4x2[j] = matrix4x[j] * boneWeight.weight0;
				}
				matrix4x2 = a_WorldToDecalsMatrix * matrix4x2 * a_MeshToDecalsMatrix;
				Matrix4x4 transpose = matrix4x2.inverse.transpose;
				Vector3 normalized = a_MeshToDecalsMatrixInvertedTransposed.MultiplyVector(a_Normals[i]).normalized;
				Vector3 normalized2 = transpose.MultiplyVector(a_Normals[i]).normalized;
				if (a_CullingDotProduct > Vector3.Dot(a_ReversedProjectionNormal, normalized2))
				{
					this.m_RemovedIndices.AddRemovedIndex(i);
				}
				else
				{
					Vector3 vector = a_MeshToDecalsMatrix.MultiplyPoint3x4(a_Vertices[i]);
					Vector3 vector2 = matrix4x2.MultiplyPoint3x4(a_Vertices[i]);
					this.m_Vertices.Add(vector2);
					this.OriginalVertices.Add(vector);
					this.m_Normals.Add(normalized);
					if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
					{
						Vector4 vector3 = a_MeshToDecalsMatrixInvertedTransposed.MultiplyVector(a_Tangents[i]).normalized;
						this.m_Tangents.Add(vector3);
					}
					if (this.m_Decals.UseVertexColors)
					{
						Color color2 = Color.white;
						if (a_Colors != null && a_Colors.Length != 0)
						{
							color2 = a_Colors[i];
						}
						this.m_TargetVertexColors.Add(color2);
						Color color3 = vertexColorTint * (color + base.ActiveDecalProjector.VertexColorBlending * color2);
						this.m_VertexColors.Add(color3);
					}
					UVMode currentUVMode = this.m_Decals.CurrentUVMode;
					bool flag = currentUVMode - UVMode.TargetUV <= 1;
					if (flag)
					{
						this.m_UVs.Add(a_UVs[i]);
					}
					UV2Mode currentUV2Mode = this.m_Decals.CurrentUV2Mode;
					flag = currentUV2Mode - UV2Mode.TargetUV <= 1;
					if (flag)
					{
						this.m_UV2s.Add(a_UV2s[i]);
					}
					this.BoneWeights.Add(boneWeight);
				}
			}
		}

		// Token: 0x0600990E RID: 39182 RVA: 0x001525E8 File Offset: 0x001507E8
		private void AddSkinQuality2(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Color[] a_Colors, Vector2[] a_UVs, Vector2[] a_UV2s, BoneWeight[] a_BoneWeights, int[] a_Triangles, int a_BoneIndexOffset, Matrix4x4[] a_BindPoses, Vector3 a_ReversedProjectionNormal, float a_CullingDotProduct, Matrix4x4 a_WorldToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrixInvertedTransposed)
		{
			Color vertexColorTint = this.m_Decals.VertexColorTint;
			Color color = (1f - base.ActiveDecalProjector.VertexColorBlending) * base.ActiveDecalProjector.VertexColor;
			for (int i = 0; i < a_Vertices.Length; i++)
			{
				BoneWeight boneWeight = a_BoneWeights[i];
				boneWeight.boneIndex0 += a_BoneIndexOffset;
				boneWeight.boneIndex1 += a_BoneIndexOffset;
				float num = 1f / (boneWeight.weight0 + boneWeight.weight1);
				boneWeight.weight0 *= num;
				boneWeight.weight1 *= num;
				boneWeight.weight2 = 0f;
				boneWeight.weight3 = 0f;
				Matrix4x4 matrix4x = this.Bones[boneWeight.boneIndex0].localToWorldMatrix * this.BindPoses[boneWeight.boneIndex0];
				Matrix4x4 matrix4x2 = this.Bones[boneWeight.boneIndex1].localToWorldMatrix * this.BindPoses[boneWeight.boneIndex1];
				Matrix4x4 matrix4x3 = default(Matrix4x4);
				for (int j = 0; j < 16; j++)
				{
					matrix4x3[j] = matrix4x[j] * boneWeight.weight0 + matrix4x2[j] * boneWeight.weight1;
				}
				matrix4x3 = a_WorldToDecalsMatrix * matrix4x3 * a_MeshToDecalsMatrix;
				Matrix4x4 transpose = matrix4x3.inverse.transpose;
				Vector3 normalized = a_MeshToDecalsMatrixInvertedTransposed.MultiplyVector(a_Normals[i]).normalized;
				Vector3 normalized2 = transpose.MultiplyVector(a_Normals[i]).normalized;
				if (a_CullingDotProduct > Vector3.Dot(a_ReversedProjectionNormal, normalized2))
				{
					this.m_RemovedIndices.AddRemovedIndex(i);
				}
				else
				{
					Vector3 vector = a_MeshToDecalsMatrix.MultiplyPoint3x4(a_Vertices[i]);
					Vector3 vector2 = matrix4x3.MultiplyPoint3x4(a_Vertices[i]);
					this.m_Vertices.Add(vector2);
					this.OriginalVertices.Add(vector);
					this.m_Normals.Add(normalized);
					if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
					{
						Vector4 vector3 = a_MeshToDecalsMatrixInvertedTransposed.MultiplyVector(a_Tangents[i]).normalized;
						this.m_Tangents.Add(vector3);
					}
					if (this.m_Decals.UseVertexColors)
					{
						Color color2 = Color.white;
						if (a_Colors != null && a_Colors.Length != 0)
						{
							color2 = a_Colors[i];
						}
						this.m_TargetVertexColors.Add(color2);
						Color color3 = vertexColorTint * (color + base.ActiveDecalProjector.VertexColorBlending * color2);
						this.m_VertexColors.Add(color3);
					}
					UVMode currentUVMode = this.m_Decals.CurrentUVMode;
					bool flag = currentUVMode - UVMode.TargetUV <= 1;
					if (flag)
					{
						this.m_UVs.Add(a_UVs[i]);
					}
					UV2Mode currentUV2Mode = this.m_Decals.CurrentUV2Mode;
					flag = currentUV2Mode - UV2Mode.TargetUV <= 1;
					if (flag)
					{
						this.m_UV2s.Add(a_UV2s[i]);
					}
					this.BoneWeights.Add(boneWeight);
				}
			}
		}

		// Token: 0x0600990F RID: 39183 RVA: 0x00152920 File Offset: 0x00150B20
		private void AddSkinQuality4(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Color[] a_Colors, Vector2[] a_UVs, Vector2[] a_UV2s, BoneWeight[] a_BoneWeights, int[] a_Triangles, int a_BoneIndexOffset, Matrix4x4[] a_BindPoses, Vector3 a_ReversedProjectionNormal, float a_CullingDotProduct, Matrix4x4 a_WorldToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrixInvertedTransposed)
		{
			Color vertexColorTint = this.m_Decals.VertexColorTint;
			Color color = (1f - base.ActiveDecalProjector.VertexColorBlending) * base.ActiveDecalProjector.VertexColor;
			for (int i = 0; i < a_Vertices.Length; i++)
			{
				BoneWeight boneWeight = a_BoneWeights[i];
				boneWeight.boneIndex0 += a_BoneIndexOffset;
				boneWeight.boneIndex1 += a_BoneIndexOffset;
				boneWeight.boneIndex2 += a_BoneIndexOffset;
				boneWeight.boneIndex3 += a_BoneIndexOffset;
				Matrix4x4 matrix4x = this.Bones[boneWeight.boneIndex0].localToWorldMatrix * this.BindPoses[boneWeight.boneIndex0];
				Matrix4x4 matrix4x2 = this.Bones[boneWeight.boneIndex1].localToWorldMatrix * this.BindPoses[boneWeight.boneIndex1];
				Matrix4x4 matrix4x3 = this.Bones[boneWeight.boneIndex2].localToWorldMatrix * this.BindPoses[boneWeight.boneIndex2];
				Matrix4x4 matrix4x4 = this.Bones[boneWeight.boneIndex3].localToWorldMatrix * this.BindPoses[boneWeight.boneIndex3];
				Matrix4x4 matrix4x5 = default(Matrix4x4);
				for (int j = 0; j < 16; j++)
				{
					matrix4x5[j] = matrix4x[j] * boneWeight.weight0 + matrix4x2[j] * boneWeight.weight1 + matrix4x3[j] * boneWeight.weight2 + matrix4x4[j] * boneWeight.weight3;
				}
				matrix4x5 = a_WorldToDecalsMatrix * matrix4x5 * a_MeshToDecalsMatrix;
				Matrix4x4 transpose = matrix4x5.inverse.transpose;
				Vector3 normalized = a_MeshToDecalsMatrixInvertedTransposed.MultiplyVector(a_Normals[i]).normalized;
				Vector3 normalized2 = transpose.MultiplyVector(a_Normals[i]).normalized;
				if (a_CullingDotProduct > Vector3.Dot(a_ReversedProjectionNormal, normalized2))
				{
					this.m_RemovedIndices.AddRemovedIndex(i);
				}
				else
				{
					Vector3 vector = a_MeshToDecalsMatrix.MultiplyPoint3x4(a_Vertices[i]);
					Vector3 vector2 = matrix4x5.MultiplyPoint3x4(a_Vertices[i]);
					this.m_Vertices.Add(vector2);
					this.OriginalVertices.Add(vector);
					this.m_Normals.Add(normalized);
					if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
					{
						Vector4 vector3 = a_MeshToDecalsMatrixInvertedTransposed.MultiplyVector(a_Tangents[i]).normalized;
						this.m_Tangents.Add(vector3);
					}
					if (this.m_Decals.UseVertexColors)
					{
						Color color2 = Color.white;
						if (a_Colors != null && a_Colors.Length != 0)
						{
							color2 = a_Colors[i];
						}
						this.m_TargetVertexColors.Add(color2);
						Color color3 = vertexColorTint * (color + base.ActiveDecalProjector.VertexColorBlending * color2);
						this.m_VertexColors.Add(color3);
					}
					UVMode currentUVMode = this.m_Decals.CurrentUVMode;
					bool flag = currentUVMode - UVMode.TargetUV <= 1;
					if (flag)
					{
						this.m_UVs.Add(a_UVs[i]);
					}
					UV2Mode currentUV2Mode = this.m_Decals.CurrentUV2Mode;
					flag = currentUV2Mode - UV2Mode.TargetUV <= 1;
					if (flag)
					{
						this.m_UV2s.Add(a_UV2s[i]);
					}
					this.BoneWeights.Add(boneWeight);
				}
			}
		}

		// Token: 0x06009910 RID: 39184 RVA: 0x00152CB0 File Offset: 0x00150EB0
		internal override void RemoveRange(int a_StartIndex, int a_Count)
		{
			this.m_Vertices.RemoveRange(a_StartIndex, a_Count);
			this.m_Normals.RemoveRange(a_StartIndex, a_Count);
			this.OriginalVertices.RemoveRange(a_StartIndex, a_Count);
			this.BoneWeights.RemoveRange(a_StartIndex, a_Count);
			if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
			{
				this.m_Tangents.RemoveRange(a_StartIndex, a_Count);
			}
			if (this.m_Decals.UseVertexColors)
			{
				this.m_TargetVertexColors.RemoveRange(a_StartIndex, a_Count);
				this.m_VertexColors.RemoveRange(a_StartIndex, a_Count);
			}
			UVMode currentUVMode = this.m_Decals.CurrentUVMode;
			bool flag = currentUVMode - UVMode.TargetUV <= 1;
			if (flag)
			{
				this.m_UVs.RemoveRange(a_StartIndex, a_Count);
			}
			UV2Mode currentUV2Mode = this.m_Decals.CurrentUV2Mode;
			flag = currentUV2Mode - UV2Mode.TargetUV <= 1;
			if (flag)
			{
				this.m_UV2s.RemoveRange(a_StartIndex, a_Count);
			}
		}

		// Token: 0x06009911 RID: 39185 RVA: 0x00152D84 File Offset: 0x00150F84
		public override void OffsetActiveProjectorVertices()
		{
			SkinnedDecalProjectorBase activeDecalProjector = base.ActiveDecalProjector;
			if (activeDecalProjector == null)
			{
				throw new InvalidOperationException("There is no active projector.");
			}
			float meshOffset = activeDecalProjector.MeshOffset;
			if (!Mathf.Approximately(meshOffset, 0f))
			{
				Matrix4x4 worldToLocalMatrix = this.m_Decals.CachedTransform.worldToLocalMatrix;
				Matrix4x4 transpose = worldToLocalMatrix.inverse.transpose;
				int decalsMeshLowerVertexIndex = activeDecalProjector.DecalsMeshLowerVertexIndex;
				int decalsMeshUpperVertexIndex = activeDecalProjector.DecalsMeshUpperVertexIndex;
				for (int i = decalsMeshLowerVertexIndex; i <= decalsMeshUpperVertexIndex; i++)
				{
					Vector3 vector = transpose.MultiplyVector(this.m_Normals[i]).normalized * meshOffset;
					vector = worldToLocalMatrix.MultiplyVector(vector);
					List<Vector3> originalVertices = this.OriginalVertices;
					int num = i;
					originalVertices[num] += vector;
				}
			}
		}
	}
}
