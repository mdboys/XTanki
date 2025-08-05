using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B09 RID: 11017
	[NullableContext(1)]
	[Nullable(0)]
	internal class ObsoleteVertexFinder
	{
		// Token: 0x0600989A RID: 39066 RVA: 0x0005B12E File Offset: 0x0005932E
		public bool IsInternalVertexObsolete(DecalsMesh a_DecalsMesh, int a_VertexIndex, List<OptimizeEdge> a_RedundantInternalEdges)
		{
			this.ClearAll();
			this.ComputeNeighbors(a_DecalsMesh, a_VertexIndex, a_RedundantInternalEdges);
			return this.IsVertexObsolete(a_DecalsMesh, a_VertexIndex);
		}

		// Token: 0x0600989B RID: 39067 RVA: 0x0005B147 File Offset: 0x00059347
		public bool IsExternalVertexObsolete(DecalsMesh a_DecalsMesh, int a_VertexIndex)
		{
			this.ClearAll();
			this.ComputeNeighbors(a_DecalsMesh, a_VertexIndex);
			return this.IsVertexObsolete(a_DecalsMesh, a_VertexIndex);
		}

		// Token: 0x0600989C RID: 39068 RVA: 0x0014FA88 File Offset: 0x0014DC88
		private void ComputeNeighbors(DecalsMesh a_DecalsMesh, int a_VertexIndex, List<OptimizeEdge> a_RedundantInternalEdges)
		{
			this.ClearNeighbors();
			foreach (OptimizeEdge optimizeEdge in a_RedundantInternalEdges)
			{
				this.AddNeighborTriangleIfNeeded(a_DecalsMesh, a_VertexIndex, optimizeEdge.triangle1Index);
				this.AddNeighborTriangleIfNeeded(a_DecalsMesh, a_VertexIndex, optimizeEdge.triangle2Index);
			}
		}

		// Token: 0x0600989D RID: 39069 RVA: 0x0014FAF4 File Offset: 0x0014DCF4
		private void ComputeNeighbors(DecalsMesh a_DecalsMesh, int a_VertexIndex)
		{
			this.ClearNeighbors();
			for (int i = a_DecalsMesh.ActiveDecalProjector.DecalsMeshLowerTriangleIndex; i < a_DecalsMesh.Triangles.Count; i += 3)
			{
				this.AddNeighborTriangleIfNeeded(a_DecalsMesh, a_VertexIndex, i);
			}
		}

		// Token: 0x0600989E RID: 39070 RVA: 0x0014FB30 File Offset: 0x0014DD30
		private void AddNeighborTriangleIfNeeded(DecalsMesh a_DecalsMesh, int a_VertexIndex, int a_TriangleIndex)
		{
			int num = a_DecalsMesh.Triangles[a_TriangleIndex];
			int num2 = a_DecalsMesh.Triangles[a_TriangleIndex + 1];
			int num3 = a_DecalsMesh.Triangles[a_TriangleIndex + 2];
			int num4 = 0;
			int num5 = 0;
			bool flag = true;
			if (a_VertexIndex == num)
			{
				num4 = num2;
				num5 = num3;
			}
			else if (a_VertexIndex == num2)
			{
				num4 = num;
				num5 = num3;
			}
			else if (a_VertexIndex == num3)
			{
				num4 = num;
				num5 = num2;
			}
			else
			{
				flag = false;
			}
			if (flag && !this.m_NeighboringTriangles.Contains(a_TriangleIndex))
			{
				this.m_NeighboringTriangles.Add(a_TriangleIndex);
				if (!this.m_NeighboringVertexIndices.Contains(num4))
				{
					Vector3 vector = a_DecalsMesh.Vertices[a_VertexIndex];
					Vector3 vector2 = a_DecalsMesh.Vertices[num4];
					float num6 = Vector3.Distance(vector, vector2);
					this.m_NeighboringVertexIndices.Add(num4);
					this.m_NeighboringVertexDistances.Add(num6);
				}
				if (!this.m_NeighboringVertexIndices.Contains(num5))
				{
					Vector3 vector3 = a_DecalsMesh.Vertices[a_VertexIndex];
					Vector3 vector4 = a_DecalsMesh.Vertices[num5];
					float num7 = Vector3.Distance(vector3, vector4);
					this.m_NeighboringVertexIndices.Add(num5);
					this.m_NeighboringVertexDistances.Add(num7);
				}
				this.AddTriangleNormal(a_DecalsMesh, a_TriangleIndex);
			}
		}

		// Token: 0x0600989F RID: 39071 RVA: 0x0014FC58 File Offset: 0x0014DE58
		private void AddTriangleNormal(DecalsMesh a_DecalsMesh, int a_TriangleIndex)
		{
			if (!this.m_TriangleNormals.HasTriangleNormal(a_TriangleIndex))
			{
				int num = a_DecalsMesh.Triangles[a_TriangleIndex];
				int num2 = a_DecalsMesh.Triangles[a_TriangleIndex + 1];
				int num3 = a_DecalsMesh.Triangles[a_TriangleIndex + 2];
				Vector3 vector = a_DecalsMesh.Vertices[num];
				Vector3 vector2 = a_DecalsMesh.Vertices[num2];
				Vector3 vector3 = a_DecalsMesh.Vertices[num3];
				Vector3 vector4 = GeometryUtilities.TriangleNormal(vector, vector2, vector3);
				this.m_TriangleNormals.AddTriangleNormal(a_TriangleIndex, vector4);
			}
		}

		// Token: 0x060098A0 RID: 39072 RVA: 0x0014FCE0 File Offset: 0x0014DEE0
		private bool IsVertexObsolete(DecalsMesh a_DecalsMesh, int a_VertexIndex)
		{
			bool flag = false;
			if (this.m_NeighboringTriangles.Count > 0)
			{
				flag = true;
				bool flag2 = false;
				Vector3 vector = Vector3.zero;
				foreach (int num in this.m_NeighboringTriangles)
				{
					vector = this.m_TriangleNormals[num];
					if (vector != Vector3.zero)
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					foreach (int num2 in this.m_NeighboringTriangles)
					{
						Vector3 vector2 = this.m_TriangleNormals[num2];
						if (vector2 != Vector3.zero && !MathfExtension.Approximately(Vector3.Dot(vector, vector2), 1f, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError))
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					float num3 = 0f;
					for (int i = 0; i < this.m_NeighboringVertexIndices.Count; i++)
					{
						float num4 = this.m_NeighboringVertexDistances[i];
						num3 += num4;
					}
					float num5 = 0f;
					if (!MathfExtension.Approximately(num3, 0f, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError))
					{
						num5 = 1f / num3;
					}
					for (int j = 0; j < this.m_NeighboringVertexIndices.Count; j++)
					{
						float num6 = this.m_NeighboringVertexDistances[j];
						if (MathfExtension.Approximately(num6, 0f, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError))
						{
							this.m_NeighboringVertexWeight.Add(0f);
							int num7 = this.m_NeighboringVertexIndices[j];
							flag = this.AreVertexPropertiesIdentical(a_DecalsMesh, a_VertexIndex, num7);
							if (!flag)
							{
								break;
							}
						}
						else
						{
							float num8 = 1f - num6 / num5;
							this.m_NeighboringVertexWeight.Add(num8);
						}
					}
					if (flag)
					{
						flag = this.AreWeightedVertexPropertiesApproximatelyVertexProperties(a_DecalsMesh, a_VertexIndex);
					}
				}
			}
			return flag;
		}

		// Token: 0x060098A1 RID: 39073 RVA: 0x0014FEE8 File Offset: 0x0014E0E8
		private bool AreVertexPropertiesIdentical(DecalsMesh a_DecalsMesh, int a_VertexIndex1, int a_VertexIndex2)
		{
			Decals decals = a_DecalsMesh.Decals;
			Vector3 vector = a_DecalsMesh.Vertices[a_VertexIndex1];
			Vector3 vector2 = a_DecalsMesh.Vertices[a_VertexIndex2];
			bool flag = Vector3Extension.Approximately(vector, vector2, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			if (flag && decals.CurrentNormalsMode == NormalsMode.Target)
			{
				Vector3 vector3 = a_DecalsMesh.Normals[a_VertexIndex1];
				Vector3 vector4 = a_DecalsMesh.Normals[a_VertexIndex2];
				flag = Vector3Extension.Approximately(vector3, vector4, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			if (flag && decals.CurrentTangentsMode == TangentsMode.Target)
			{
				Vector4 vector5 = a_DecalsMesh.Tangents[a_VertexIndex1];
				Vector4 vector6 = a_DecalsMesh.Tangents[a_VertexIndex2];
				flag = Vector3Extension.Approximately(vector5, vector6, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			bool flag2 = flag;
			if (flag2)
			{
				UVMode currentUVMode = decals.CurrentUVMode;
				bool flag3 = currentUVMode - UVMode.TargetUV <= 1;
				flag2 = flag3;
			}
			if (flag2)
			{
				Vector2 vector7 = a_DecalsMesh.UVs[a_VertexIndex1];
				Vector2 vector8 = a_DecalsMesh.UVs[a_VertexIndex2];
				flag = Vector2Extension.Approximately(vector7, vector8, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			flag2 = flag;
			if (flag2)
			{
				UV2Mode currentUV2Mode = decals.CurrentUV2Mode;
				bool flag3 = currentUV2Mode - UV2Mode.TargetUV <= 1;
				flag2 = flag3;
			}
			if (flag2)
			{
				Vector2 vector9 = a_DecalsMesh.UV2s[a_VertexIndex1];
				Vector2 vector10 = a_DecalsMesh.UV2s[a_VertexIndex2];
				flag = Vector2Extension.Approximately(vector9, vector10, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			return flag;
		}

		// Token: 0x060098A2 RID: 39074 RVA: 0x00150044 File Offset: 0x0014E244
		private bool AreWeightedVertexPropertiesApproximatelyVertexProperties(DecalsMesh a_DecalsMesh, int a_VertexIndex)
		{
			bool flag = true;
			Decals decals = a_DecalsMesh.Decals;
			if (flag)
			{
				Vector3 vector = a_DecalsMesh.Vertices[a_VertexIndex];
				Vector3 vector2 = Vector3.zero;
				for (int i = 0; i < this.m_NeighboringVertexIndices.Count; i++)
				{
					int num = this.m_NeighboringVertexIndices[i];
					float num2 = this.m_NeighboringVertexWeight[i];
					Vector3 vector3 = a_DecalsMesh.Vertices[num];
					vector2 += num2 * vector3;
				}
				flag = flag && Vector3Extension.Approximately(vector, vector2, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			if (flag && decals.CurrentNormalsMode == NormalsMode.Target)
			{
				Vector3 vector4 = a_DecalsMesh.Normals[a_VertexIndex];
				Vector3 vector5 = Vector3.zero;
				for (int j = 0; j < this.m_NeighboringVertexIndices.Count; j++)
				{
					int num3 = this.m_NeighboringVertexIndices[j];
					float num4 = this.m_NeighboringVertexWeight[j];
					Vector3 vector6 = a_DecalsMesh.Normals[num3];
					vector5 += num4 * vector6;
					vector5.Normalize();
				}
				flag = flag && Vector3Extension.Approximately(vector4, vector5, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			if (flag && decals.CurrentTangentsMode == TangentsMode.Target)
			{
				Vector4 vector7 = a_DecalsMesh.Tangents[a_VertexIndex];
				Vector4 vector8 = Vector3.zero;
				for (int k = 0; k < this.m_NeighboringVertexIndices.Count; k++)
				{
					int num5 = this.m_NeighboringVertexIndices[k];
					float num6 = this.m_NeighboringVertexWeight[k];
					Vector4 vector9 = a_DecalsMesh.Tangents[num5];
					vector8 += num6 * vector9;
					vector8.Normalize();
				}
				flag = flag && Vector4Extension.Approximately(vector7, vector8, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			bool flag2 = flag;
			if (flag2)
			{
				UVMode currentUVMode = decals.CurrentUVMode;
				bool flag3 = currentUVMode - UVMode.TargetUV <= 1;
				flag2 = flag3;
			}
			if (flag2)
			{
				Vector2 vector10 = a_DecalsMesh.UVs[a_VertexIndex];
				Vector2 vector11 = Vector3.zero;
				for (int l = 0; l < this.m_NeighboringVertexIndices.Count; l++)
				{
					int num7 = this.m_NeighboringVertexIndices[l];
					float num8 = this.m_NeighboringVertexWeight[l];
					Vector2 vector12 = a_DecalsMesh.UVs[num7];
					vector11 += num8 * vector12;
				}
				flag = flag && Vector2Extension.Approximately(vector10, vector11, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			flag2 = flag;
			if (flag2)
			{
				UV2Mode currentUV2Mode = decals.CurrentUV2Mode;
				bool flag3 = currentUV2Mode - UV2Mode.TargetUV <= 1;
				flag2 = flag3;
			}
			if (flag2)
			{
				Vector2 vector13 = a_DecalsMesh.UV2s[a_VertexIndex];
				Vector2 vector14 = Vector3.zero;
				for (int m = 0; m < this.m_NeighboringVertexIndices.Count; m++)
				{
					int num9 = this.m_NeighboringVertexIndices[m];
					float num10 = this.m_NeighboringVertexWeight[m];
					Vector2 vector15 = a_DecalsMesh.UV2s[num9];
					vector14 += num10 * vector15;
				}
				bool flag4 = flag && Vector2Extension.Approximately(vector13, vector14, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError);
			}
			return true;
		}

		// Token: 0x060098A3 RID: 39075 RVA: 0x0005B15F File Offset: 0x0005935F
		public void ClearAll()
		{
			this.m_TriangleNormals.Clear();
			this.ClearNeighbors();
		}

		// Token: 0x060098A4 RID: 39076 RVA: 0x0005B172 File Offset: 0x00059372
		private void ClearNeighbors()
		{
			this.m_NeighboringTriangles.Clear();
			this.m_NeighboringVertexIndices.Clear();
			this.m_NeighboringVertexDistances.Clear();
			this.m_NeighboringVertexWeight.Clear();
		}

		// Token: 0x04006420 RID: 25632
		private readonly List<int> m_NeighboringTriangles = new List<int>();

		// Token: 0x04006421 RID: 25633
		private readonly List<float> m_NeighboringVertexDistances = new List<float>();

		// Token: 0x04006422 RID: 25634
		private readonly List<int> m_NeighboringVertexIndices = new List<int>();

		// Token: 0x04006423 RID: 25635
		private readonly List<float> m_NeighboringVertexWeight = new List<float>();

		// Token: 0x04006424 RID: 25636
		private readonly OptimizeTriangleNormals m_TriangleNormals = new OptimizeTriangleNormals();
	}
}
