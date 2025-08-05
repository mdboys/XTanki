using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AF4 RID: 10996
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public class DecalsMesh : GenericDecalsMesh<Decals, DecalProjectorBase, DecalsMesh>
	{
		// Token: 0x060097C1 RID: 38849 RVA: 0x0005AA20 File Offset: 0x00058C20
		public DecalsMesh()
		{
		}

		// Token: 0x060097C2 RID: 38850 RVA: 0x0005AA28 File Offset: 0x00058C28
		public DecalsMesh(Decals a_Decals)
		{
			this.m_Decals = a_Decals;
		}

		// Token: 0x060097C3 RID: 38851 RVA: 0x00149C00 File Offset: 0x00147E00
		public void Add(Mesh a_Mesh, Matrix4x4 a_WorldToMeshMatrix, Matrix4x4 a_MeshToWorldMatrix)
		{
			DecalProjectorBase activeDecalProjector = base.ActiveDecalProjector;
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
				if (0 > activeDecalProjector.UV1RectangleIndex || this.m_Decals.CurrentUvRectangles == null || activeDecalProjector.UV1RectangleIndex >= this.m_Decals.CurrentUvRectangles.Length)
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
				if (0 > activeDecalProjector.UV2RectangleIndex || this.m_Decals.CurrentUv2Rectangles == null || activeDecalProjector.UV2RectangleIndex >= this.m_Decals.CurrentUv2Rectangles.Length)
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
				throw new ArgumentException("The mesh is not readable! Make sure the mesh can be read in the import settings and no static batching is used for it.");
			}
			Vector3[] vertices = a_Mesh.vertices;
			Vector3[] array = a_Mesh.normals;
			Vector4[] array2 = null;
			Color[] array3 = null;
			Vector2[] array4 = null;
			Vector2[] array5 = null;
			int[] triangles = a_Mesh.triangles;
			if (triangles == null)
			{
				throw new NullReferenceException("The triangles in the mesh are null!");
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
				array3 = a_Mesh.colors;
				if (array3 != null && array3.Length != 0 && vertices.Length != array3.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of colors!");
				}
			}
			if (this.m_Decals.CurrentUVMode == UVMode.TargetUV)
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
			else if (this.m_Decals.CurrentUVMode == UVMode.TargetUV2)
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
			if (this.m_Decals.CurrentUV2Mode == UV2Mode.TargetUV)
			{
				array5 = a_Mesh.uv;
				if (array5 == null)
				{
					throw new NullReferenceException("The uv's in the mesh are null!");
				}
				if (vertices.Length != array5.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of uv's!");
				}
			}
			else if (this.m_Decals.CurrentUV2Mode == UV2Mode.TargetUV2)
			{
				array5 = a_Mesh.uv2;
				if (array5 == null)
				{
					throw new NullReferenceException("The uv2's in the mesh are null!");
				}
				if (vertices.Length != array5.Length)
				{
					throw new FormatException("The number of vertices in the mesh does not match the number of uv2's!");
				}
			}
			this.Add(vertices, array, array2, array3, array4, array5, triangles, a_WorldToMeshMatrix, a_MeshToWorldMatrix);
			if (flag2)
			{
				a_Mesh.normals = null;
			}
		}

		// Token: 0x060097C4 RID: 38852 RVA: 0x00149F08 File Offset: 0x00148108
		public void Add(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Vector2[] a_UVs, Vector2[] a_UV2s, int[] a_Triangles, Matrix4x4 a_WorldToMeshMatrix, Matrix4x4 a_MeshToWorldMatrix)
		{
			this.Add(a_Vertices, a_Normals, a_Tangents, null, a_UVs, a_UV2s, a_Triangles, a_WorldToMeshMatrix, a_MeshToWorldMatrix);
		}

		// Token: 0x060097C5 RID: 38853 RVA: 0x00149F2C File Offset: 0x0014812C
		public void Add(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Color[] a_Colors, Vector2[] a_UVs, Vector2[] a_UV2s, int[] a_Triangles, Matrix4x4 a_WorldToMeshMatrix, Matrix4x4 a_MeshToWorldMatrix)
		{
			DecalProjectorBase activeDecalProjector = base.ActiveDecalProjector;
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
				if (0 > activeDecalProjector.UV1RectangleIndex || this.m_Decals.CurrentUvRectangles == null || activeDecalProjector.UV1RectangleIndex >= this.m_Decals.CurrentUvRectangles.Length)
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
				if (0 > activeDecalProjector.UV2RectangleIndex || this.m_Decals.CurrentUv2Rectangles == null || activeDecalProjector.UV2RectangleIndex >= this.m_Decals.CurrentUv2Rectangles.Length)
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
			this.InternalAdd(a_Vertices, a_Normals, a_Tangents, a_Colors, a_UVs, a_UV2s, normalized, num, matrix4x, transpose);
			if (a_WorldToMeshMatrix.Determinant() >= 0f)
			{
				for (int i = 0; i < a_Triangles.Length; i += 3)
				{
					int num2 = a_Triangles[i];
					int num3 = a_Triangles[i + 1];
					int num4 = a_Triangles[i + 2];
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
				for (int j = 0; j < a_Triangles.Length; j += 3)
				{
					int num5 = a_Triangles[j];
					int num6 = a_Triangles[j + 1];
					int num7 = a_Triangles[j + 2];
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
			activeDecalProjector.IsTangentProjectionCalculated = false;
			activeDecalProjector.IsUV1ProjectionCalculated = false;
			activeDecalProjector.IsUV2ProjectionCalculated = false;
		}

		// Token: 0x060097C6 RID: 38854 RVA: 0x0014A3F4 File Offset: 0x001485F4
		private void InternalAdd(Vector3[] a_Vertices, Vector3[] a_Normals, Vector4[] a_Tangents, Color[] a_Colors, Vector2[] a_UVs, Vector2[] a_UV2s, Vector3 a_ReversedProjectionNormal, float a_CullingDotProduct, Matrix4x4 a_MeshToDecalsMatrix, Matrix4x4 a_MeshToDecalsMatrixInvertedTransposed)
		{
			Color vertexColorTint = this.m_Decals.VertexColorTint;
			Color color = (1f - base.ActiveDecalProjector.VertexColorBlending) * base.ActiveDecalProjector.VertexColor;
			for (int i = 0; i < a_Vertices.Length; i++)
			{
				Vector3 normalized = a_MeshToDecalsMatrixInvertedTransposed.MultiplyVector(a_Normals[i]).normalized;
				if (a_CullingDotProduct > Vector3.Dot(a_ReversedProjectionNormal, normalized))
				{
					this.m_RemovedIndices.AddRemovedIndex(i);
				}
				else
				{
					Vector3 vector = a_MeshToDecalsMatrix.MultiplyPoint3x4(a_Vertices[i]);
					this.m_Vertices.Add(vector);
					this.m_Normals.Add(normalized);
					if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
					{
						Vector4 vector2 = a_MeshToDecalsMatrixInvertedTransposed.MultiplyVector(a_Tangents[i]).normalized;
						this.m_Tangents.Add(vector2);
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
				}
			}
		}

		// Token: 0x060097C7 RID: 38855 RVA: 0x0014A5A8 File Offset: 0x001487A8
		public void Add(Terrain a_Terrain, Matrix4x4 a_TerrainToDecalsMatrix)
		{
			DecalProjectorBase activeDecalProjector = base.ActiveDecalProjector;
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
				if (0 > activeDecalProjector.UV1RectangleIndex || this.m_Decals.CurrentUvRectangles == null || activeDecalProjector.UV1RectangleIndex >= this.m_Decals.CurrentUvRectangles.Length)
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
				if (0 > activeDecalProjector.UV2RectangleIndex || this.m_Decals.CurrentUv2Rectangles == null || activeDecalProjector.UV2RectangleIndex >= this.m_Decals.CurrentUv2Rectangles.Length)
				{
					throw new IndexOutOfRangeException("The uv2 rectangle index of the active projector is not a valid index within the decals uv2 rectangles array!");
				}
			}
			if (a_Terrain == null)
			{
				throw new ArgumentNullException("The terrain parameter is not allowed to be null!");
			}
			if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
			{
				throw new InvalidOperationException("Terrains don't have tangents!");
			}
			uvmode = this.m_Decals.CurrentUVMode;
			flag = uvmode - UVMode.TargetUV <= 1;
			if (flag)
			{
				throw new InvalidOperationException("Terrains don't have uv's!");
			}
			uv2Mode = this.m_Decals.CurrentUV2Mode;
			flag = uv2Mode - UV2Mode.TargetUV <= 1;
			if (flag)
			{
				throw new InvalidOperationException("Terrains don't have uv2's!");
			}
			Bounds bounds = activeDecalProjector.WorldBounds();
			bounds.center -= a_Terrain.transform.position;
			TerrainData terrainData = a_Terrain.terrainData;
			Matrix4x4 transpose = a_TerrainToDecalsMatrix.inverse.transpose;
			if (!(terrainData != null))
			{
				return;
			}
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			if (min.x > max.x)
			{
				float x = min.x;
				min.x = max.x;
				max.x = x;
			}
			if (min.z > max.z)
			{
				float z = min.z;
				min.z = max.z;
				max.z = z;
			}
			Vector3 size = terrainData.size;
			min.x = Mathf.Clamp(min.x, 0f, size.x);
			max.x = Mathf.Clamp(max.x, 0f, size.x);
			min.z = Mathf.Clamp(min.z, 0f, size.z);
			max.z = Mathf.Clamp(max.z, 0f, size.z);
			Vector3 heightmapScale = terrainData.heightmapScale;
			int num = Mathf.FloorToInt(min.x / heightmapScale.x);
			int num2 = Mathf.FloorToInt(min.z / heightmapScale.z);
			int num3 = Mathf.CeilToInt(max.x / heightmapScale.x);
			int num4 = Mathf.CeilToInt(max.z / heightmapScale.z);
			int count = base.Vertices.Count;
			int count2 = base.Triangles.Count;
			if (num < num3 && num2 < num4)
			{
				Color color = this.m_Decals.VertexColorTint * ((1f - base.ActiveDecalProjector.VertexColorBlending) * base.ActiveDecalProjector.VertexColor + base.ActiveDecalProjector.VertexColorBlending * Color.white);
				float num5 = 1f / (float)(terrainData.heightmapWidth - 1);
				float num6 = 1f / (float)(terrainData.heightmapHeight - 1);
				for (int i = num; i <= num3; i++)
				{
					float num7 = (float)i * heightmapScale.x;
					for (int j = num2; j <= num4; j++)
					{
						float height = terrainData.GetHeight(i, j);
						float num8 = (float)j * heightmapScale.z;
						Vector3 vector = a_TerrainToDecalsMatrix.MultiplyPoint3x4(new Vector3(num7, height, num8));
						Vector3 vector2 = terrainData.GetInterpolatedNormal((float)i * num5, (float)j * num6);
						vector2 = transpose.MultiplyVector(vector2);
						vector2.Normalize();
						this.m_Vertices.Add(vector);
						this.m_Normals.Add(vector2);
						if (this.m_Decals.UseVertexColors)
						{
							this.m_TargetVertexColors.Add(Color.white);
							this.m_VertexColors.Add(color);
						}
					}
				}
				int num9 = num3 - num + 1;
				int num10 = num4 - num2 + 1;
				int num11 = num9 - 1;
				int num12 = num10 - 1;
				for (int k = 0; k < num11; k++)
				{
					for (int l = 0; l < num12; l++)
					{
						int num13 = count + l + k * num10;
						int num14 = num13 + 1;
						int num15 = num13 + num10;
						int num16 = num15 + 1;
						this.m_Triangles.Add(num13);
						this.m_Triangles.Add(num14);
						this.m_Triangles.Add(num16);
						this.m_Triangles.Add(num13);
						this.m_Triangles.Add(num16);
						this.m_Triangles.Add(num15);
					}
				}
			}
			float num17 = Mathf.Cos(activeDecalProjector.CullingAngle * 0.017453292f);
			if (!Mathf.Approximately(num17, -1f))
			{
				Vector3 normalized = new Vector3(0f, 1f, 0f);
				normalized = (this.m_Decals.CachedTransform.worldToLocalMatrix * activeDecalProjector.ProjectorToWorldMatrix).inverse.transpose.MultiplyVector(normalized).normalized;
				for (int m = count; m < this.m_Vertices.Count; m++)
				{
					Vector3 vector3 = this.m_Normals[m];
					if (num17 > Vector3.Dot(normalized, vector3))
					{
						this.m_RemovedIndices.AddRemovedIndex(m);
					}
				}
				for (int n = this.m_Triangles.Count - 1; n >= count2; n -= 3)
				{
					int num18 = this.m_Triangles[n];
					int num19 = this.m_Triangles[n - 1];
					int num20 = this.m_Triangles[n - 2];
					if (this.m_RemovedIndices.IsRemovedIndex(num18) || this.m_RemovedIndices.IsRemovedIndex(num19) || this.m_RemovedIndices.IsRemovedIndex(num20))
					{
						this.m_Triangles.RemoveRange(n - 2, 3);
					}
					else
					{
						int num21 = this.m_RemovedIndices.AdjustedIndex(num18);
						int num22 = this.m_RemovedIndices.AdjustedIndex(num19);
						int num23 = this.m_RemovedIndices.AdjustedIndex(num20);
						this.m_Triangles[n] = num21;
						this.m_Triangles[n - 1] = num22;
						this.m_Triangles[n - 2] = num23;
					}
				}
				base.RemoveIndices(this.m_RemovedIndices);
				this.m_RemovedIndices.Clear();
			}
			activeDecalProjector.DecalsMeshUpperVertexIndex = this.m_Vertices.Count - 1;
			activeDecalProjector.DecalsMeshUpperTriangleIndex = this.m_Triangles.Count - 1;
			activeDecalProjector.IsUV1ProjectionCalculated = false;
			activeDecalProjector.IsUV2ProjectionCalculated = false;
			activeDecalProjector.IsTangentProjectionCalculated = false;
		}

		// Token: 0x060097C8 RID: 38856 RVA: 0x0014AD00 File Offset: 0x00148F00
		public void Add(Terrain a_Terrain, float a_Density, Matrix4x4 a_TerrainToDecalsMatrix)
		{
			DecalProjectorBase activeDecalProjector = base.ActiveDecalProjector;
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
				if (0 > activeDecalProjector.UV1RectangleIndex || this.m_Decals.CurrentUvRectangles == null || activeDecalProjector.UV1RectangleIndex >= this.m_Decals.CurrentUvRectangles.Length)
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
				if (0 > activeDecalProjector.UV2RectangleIndex || this.m_Decals.CurrentUv2Rectangles == null || activeDecalProjector.UV2RectangleIndex >= this.m_Decals.CurrentUv2Rectangles.Length)
				{
					throw new IndexOutOfRangeException("The uv2 rectangle index of the active projector is not a valid index within the decals uv2 rectangles array!");
				}
			}
			if (a_Terrain == null)
			{
				throw new ArgumentNullException("The terrain parameter is not allowed to be null!");
			}
			if (this.m_Decals.CurrentTangentsMode == TangentsMode.Target)
			{
				throw new InvalidOperationException("Terrains don't have tangents!");
			}
			uvmode = this.m_Decals.CurrentUVMode;
			flag = uvmode - UVMode.TargetUV <= 1;
			if (flag)
			{
				throw new InvalidOperationException("Terrains don't have uv's!");
			}
			uv2Mode = this.m_Decals.CurrentUV2Mode;
			flag = uv2Mode - UV2Mode.TargetUV <= 1;
			if (flag)
			{
				throw new InvalidOperationException("Terrains don't have uv2's!");
			}
			flag = a_Density < 0f || a_Density > 1f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("The density has to be in the range [0.0f, 1.0f].");
			}
			Bounds bounds = activeDecalProjector.WorldBounds();
			bounds.center -= a_Terrain.transform.position;
			TerrainData terrainData = a_Terrain.terrainData;
			Matrix4x4 transpose = a_TerrainToDecalsMatrix.inverse.transpose;
			if (!(terrainData != null))
			{
				return;
			}
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			if (min.x > max.x)
			{
				float x = min.x;
				min.x = max.x;
				max.x = x;
			}
			if (min.z > max.z)
			{
				float z = min.z;
				min.z = max.z;
				max.z = z;
			}
			Vector3 size = terrainData.size;
			min.x = Mathf.Clamp(min.x, 0f, size.x);
			max.x = Mathf.Clamp(max.x, 0f, size.x);
			min.z = Mathf.Clamp(min.z, 0f, size.z);
			max.z = Mathf.Clamp(max.z, 0f, size.z);
			Vector3 heightmapScale = terrainData.heightmapScale;
			int num = Mathf.FloorToInt(min.x / heightmapScale.x);
			int num2 = Mathf.FloorToInt(min.z / heightmapScale.z);
			int num3 = Mathf.CeilToInt(max.x / heightmapScale.x);
			int num4 = Mathf.CeilToInt(max.z / heightmapScale.z);
			int count = base.Vertices.Count;
			int count2 = base.Triangles.Count;
			if (num < num3 && num2 < num4)
			{
				Color color = this.m_Decals.VertexColorTint * ((1f - base.ActiveDecalProjector.VertexColorBlending) * base.ActiveDecalProjector.VertexColor + base.ActiveDecalProjector.VertexColorBlending * Color.white);
				float num5 = 7f;
				float num6 = 0f - Mathf.Pow(2f, (0f - num5) * a_Density) + 1f;
				int num7 = Mathf.RoundToInt(Mathf.Lerp((float)(num3 - num), 1f, num6));
				int num8 = Mathf.RoundToInt(Mathf.Lerp((float)(num4 - num2), 1f, num6));
				float num9 = 1f / (float)(terrainData.heightmapWidth - 1);
				float num10 = 1f / (float)(terrainData.heightmapHeight - 1);
				int num11 = 0;
				int num12 = 0;
				int i = num;
				for (;;)
				{
					bool flag2 = false;
					if (i >= num3)
					{
						i = num3;
						flag2 = true;
					}
					float num13 = (float)i * heightmapScale.x;
					int num14 = num2;
					for (;;)
					{
						bool flag3 = false;
						if (num14 >= num4)
						{
							num14 = num4;
							flag3 = true;
						}
						float height = terrainData.GetHeight(i, num14);
						float num15 = (float)num14 * heightmapScale.z;
						Vector3 vector = a_TerrainToDecalsMatrix.MultiplyPoint3x4(new Vector3(num13, height, num15));
						Vector3 vector2 = terrainData.GetInterpolatedNormal((float)i * num9, (float)num14 * num10);
						vector2 = transpose.MultiplyVector(vector2);
						vector2.Normalize();
						this.m_Vertices.Add(vector);
						this.m_Normals.Add(vector2);
						if (this.m_Decals.UseVertexColors)
						{
							this.m_TargetVertexColors.Add(Color.white);
							this.m_VertexColors.Add(color);
						}
						if (num11 == 0)
						{
							num12++;
						}
						if (flag3)
						{
							break;
						}
						num14 += num8;
					}
					num11++;
					if (flag2)
					{
						break;
					}
					i += num7;
				}
				int num16 = num11 - 1;
				int num17 = num12 - 1;
				for (i = 0; i < num16; i++)
				{
					for (int j = 0; j < num17; j++)
					{
						int num18 = count + j + i * num12;
						int num19 = num18 + 1;
						int num20 = num18 + num12;
						int num21 = num20 + 1;
						this.m_Triangles.Add(num18);
						this.m_Triangles.Add(num19);
						this.m_Triangles.Add(num21);
						this.m_Triangles.Add(num18);
						this.m_Triangles.Add(num21);
						this.m_Triangles.Add(num20);
					}
				}
			}
			float num22 = Mathf.Cos(activeDecalProjector.CullingAngle * 0.017453292f);
			if (!Mathf.Approximately(num22, -1f))
			{
				Vector3 normalized = new Vector3(0f, 1f, 0f);
				normalized = (this.m_Decals.CachedTransform.worldToLocalMatrix * activeDecalProjector.ProjectorToWorldMatrix).inverse.transpose.MultiplyVector(normalized).normalized;
				for (int k = count; k < this.m_Vertices.Count; k++)
				{
					Vector3 vector3 = this.m_Normals[k];
					if (num22 > Vector3.Dot(normalized, vector3))
					{
						this.m_RemovedIndices.AddRemovedIndex(k);
					}
				}
				for (int l = this.m_Triangles.Count - 1; l >= count2; l -= 3)
				{
					int num23 = this.m_Triangles[l];
					int num24 = this.m_Triangles[l - 1];
					int num25 = this.m_Triangles[l - 2];
					if (this.m_RemovedIndices.IsRemovedIndex(num23) || this.m_RemovedIndices.IsRemovedIndex(num24) || this.m_RemovedIndices.IsRemovedIndex(num25))
					{
						this.m_Triangles.RemoveRange(l - 2, 3);
					}
					else
					{
						int num26 = this.m_RemovedIndices.AdjustedIndex(num23);
						int num27 = this.m_RemovedIndices.AdjustedIndex(num24);
						int num28 = this.m_RemovedIndices.AdjustedIndex(num25);
						this.m_Triangles[l] = num26;
						this.m_Triangles[l - 1] = num27;
						this.m_Triangles[l - 2] = num28;
					}
				}
				base.RemoveIndices(this.m_RemovedIndices);
				this.m_RemovedIndices.Clear();
			}
			activeDecalProjector.DecalsMeshUpperVertexIndex = this.m_Vertices.Count - 1;
			activeDecalProjector.DecalsMeshUpperTriangleIndex = this.m_Triangles.Count - 1;
			activeDecalProjector.IsUV1ProjectionCalculated = false;
			activeDecalProjector.IsUV2ProjectionCalculated = false;
			activeDecalProjector.IsTangentProjectionCalculated = false;
		}

		// Token: 0x060097C9 RID: 38857 RVA: 0x0014B4F8 File Offset: 0x001496F8
		internal override void RemoveRange(int a_StartIndex, int a_Count)
		{
			this.m_Vertices.RemoveRange(a_StartIndex, a_Count);
			this.m_Normals.RemoveRange(a_StartIndex, a_Count);
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

		// Token: 0x060097CA RID: 38858 RVA: 0x0014B5B4 File Offset: 0x001497B4
		public override void OffsetActiveProjectorVertices()
		{
			DecalProjectorBase activeDecalProjector = base.ActiveDecalProjector;
			if (activeDecalProjector == null)
			{
				throw new InvalidOperationException("There is no active projector.");
			}
			float meshOffset = activeDecalProjector.MeshOffset;
			if (!Mathf.Approximately(meshOffset, 0f))
			{
				Matrix4x4 worldToLocalMatrix = this.m_Decals.CachedTransform.worldToLocalMatrix;
				Matrix4x4 transpose = worldToLocalMatrix.transpose;
				int decalsMeshLowerVertexIndex = activeDecalProjector.DecalsMeshLowerVertexIndex;
				int decalsMeshUpperVertexIndex = activeDecalProjector.DecalsMeshUpperVertexIndex;
				for (int i = decalsMeshLowerVertexIndex; i <= decalsMeshUpperVertexIndex; i++)
				{
					Vector3 vector = transpose.MultiplyVector(this.m_Normals[i]).normalized * meshOffset;
					vector = worldToLocalMatrix.MultiplyVector(vector);
					List<Vector3> vertices = this.m_Vertices;
					int num = i;
					vertices[num] += vector;
				}
			}
		}
	}
}
