using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AF6 RID: 10998
	[NullableContext(1)]
	[Nullable(0)]
	public class DecalsMeshMinimizer
	{
		// Token: 0x060097D6 RID: 38870 RVA: 0x0014C794 File Offset: 0x0014A994
		public void MinimizeActiveProjectorOfDecalsMesh(DecalsMesh a_DecalsMesh)
		{
			if (a_DecalsMesh == null)
			{
				throw new ArgumentNullException("Decals mesh argument is not allowed to be null.");
			}
			float meshMinimizerMaximumAbsoluteError = a_DecalsMesh.Decals.MeshMinimizerMaximumAbsoluteError;
			float meshMinimizerMaximumRelativeError = a_DecalsMesh.Decals.MeshMinimizerMaximumRelativeError;
			this.MinimizeActiveProjectorOfDecalsMesh(a_DecalsMesh, meshMinimizerMaximumAbsoluteError, meshMinimizerMaximumRelativeError);
		}

		// Token: 0x060097D7 RID: 38871 RVA: 0x0014C7D0 File Offset: 0x0014A9D0
		public void MinimizeActiveProjectorOfDecalsMesh(DecalsMesh a_DecalsMesh, float a_MaximumAbsoluteError, float a_MaximumRelativeError)
		{
			if (a_DecalsMesh == null)
			{
				throw new ArgumentNullException("Decals mesh argument is not allowed to be null.");
			}
			if (a_DecalsMesh.ActiveDecalProjector == null)
			{
				throw new ArgumentNullException("Active decal projector of decals mesh is not allowed to be null.");
			}
			if (a_MaximumAbsoluteError < 0f)
			{
				throw new ArgumentOutOfRangeException("The maximum absolute error has to be greater than zero.");
			}
			bool flag = a_MaximumRelativeError < 0f || a_MaximumRelativeError > 1f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("The maximum relative error has to be within [0.0f, 1.0f].");
			}
			this.ClearAll();
			DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError = a_MaximumAbsoluteError;
			DecalsMeshMinimizer.s_CurrentMaximumRelativeError = a_MaximumRelativeError;
			this.ComputePotentialObsoleteVertices(a_DecalsMesh);
			this.ComputeObsoleteInternalVertices(a_DecalsMesh);
			this.ComputeObsoleteExternalVertices(a_DecalsMesh);
			this.RemoveObsoleteVertices(a_DecalsMesh);
			this.ClearAll();
		}

		// Token: 0x060097D8 RID: 38872 RVA: 0x0014C86C File Offset: 0x0014AA6C
		private void ComputePotentialObsoleteVertices(DecalsMesh a_DecalsMesh)
		{
			DecalProjectorBase activeDecalProjector = a_DecalsMesh.ActiveDecalProjector;
			this.ClearAll();
			for (int i = activeDecalProjector.DecalsMeshLowerTriangleIndex; i <= activeDecalProjector.DecalsMeshUpperTriangleIndex; i += 3)
			{
				int num = a_DecalsMesh.Triangles[i];
				int num2 = a_DecalsMesh.Triangles[i + 1];
				int num3 = a_DecalsMesh.Triangles[i + 2];
				OptimizeEdge optimizeEdge = new OptimizeEdge(num, num2, i);
				OptimizeEdge optimizeEdge2 = new OptimizeEdge(num2, num3, i);
				OptimizeEdge optimizeEdge3 = new OptimizeEdge(num3, num, i);
				this.AddEdge(optimizeEdge);
				this.AddEdge(optimizeEdge2);
				this.AddEdge(optimizeEdge3);
			}
			List<OptimizeEdge> list = this.m_RedundantExternalEdges.OptimizedEdgeList();
			foreach (OptimizeEdge optimizeEdge4 in list)
			{
				if (!this.m_NonInternalVertexIndices.ContainsKey(optimizeEdge4.vertex1Index))
				{
					this.m_NonInternalVertexIndices.Add(optimizeEdge4.vertex1Index, optimizeEdge4.vertex1Index);
				}
				if (!this.m_NonInternalVertexIndices.ContainsKey(optimizeEdge4.vertex2Index))
				{
					this.m_NonInternalVertexIndices.Add(optimizeEdge4.vertex2Index, optimizeEdge4.vertex2Index);
				}
			}
			foreach (OptimizeEdge optimizeEdge5 in this.m_OverusedInternalEdges.OptimizedEdgeList())
			{
				if (!this.m_NonInternalVertexIndices.ContainsKey(optimizeEdge5.vertex1Index))
				{
					this.m_NonInternalVertexIndices.Add(optimizeEdge5.vertex1Index, optimizeEdge5.vertex1Index);
				}
				if (!this.m_NonInternalVertexIndices.ContainsKey(optimizeEdge5.vertex2Index))
				{
					this.m_NonInternalVertexIndices.Add(optimizeEdge5.vertex2Index, optimizeEdge5.vertex2Index);
				}
			}
			for (int j = activeDecalProjector.DecalsMeshLowerVertexIndex; j <= activeDecalProjector.DecalsMeshUpperVertexIndex; j++)
			{
				if (!this.m_NonInternalVertexIndices.ContainsKey(j))
				{
					this.m_ObsoleteInternalVertexIndices.Add(j);
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				OptimizeEdge optimizeEdge6 = list[k];
				for (int l = k + 1; l < this.m_RedundantExternalEdges.Count; l++)
				{
					OptimizeEdge optimizeEdge7 = list[l];
					if (optimizeEdge6.vertex1Index == optimizeEdge7.vertex1Index || optimizeEdge6.vertex1Index == optimizeEdge7.vertex2Index)
					{
						if (!this.m_ObsoleteExternalVertexIndices.Contains(optimizeEdge6.vertex1Index) && DecalsMeshMinimizer.AreEdgesParallelOrIsAtLeastOneAPoint(a_DecalsMesh, optimizeEdge6, optimizeEdge7))
						{
							this.m_ObsoleteExternalVertexIndices.Add(optimizeEdge6.vertex1Index);
						}
					}
					else if ((optimizeEdge6.vertex2Index == optimizeEdge7.vertex1Index || optimizeEdge6.vertex2Index == optimizeEdge7.vertex2Index) && !this.m_ObsoleteExternalVertexIndices.Contains(optimizeEdge6.vertex2Index) && DecalsMeshMinimizer.AreEdgesParallelOrIsAtLeastOneAPoint(a_DecalsMesh, optimizeEdge6, optimizeEdge7))
					{
						this.m_ObsoleteExternalVertexIndices.Add(optimizeEdge6.vertex2Index);
					}
				}
			}
			this.ClearTemporaryCollections();
		}

		// Token: 0x060097D9 RID: 38873 RVA: 0x0014CB78 File Offset: 0x0014AD78
		private void AddEdge(OptimizeEdge a_Edge)
		{
			if (this.m_OverusedInternalEdges.HasEdge(a_Edge))
			{
				return;
			}
			if (this.m_RedundantInternalEdges.HasEdge(a_Edge))
			{
				this.m_RedundantInternalEdges.RemoveEdge(a_Edge);
				this.m_OverusedInternalEdges.AddEdge(a_Edge);
				return;
			}
			if (!this.m_RedundantExternalEdges.HasEdge(a_Edge))
			{
				this.m_RedundantExternalEdges.AddEdge(a_Edge);
				return;
			}
			OptimizeEdge optimizeEdge = this.m_RedundantExternalEdges[a_Edge];
			this.m_RedundantExternalEdges.RemoveEdge(a_Edge);
			optimizeEdge.triangle2Index = a_Edge.triangle1Index;
			this.m_RedundantInternalEdges.AddEdge(optimizeEdge);
		}

		// Token: 0x060097DA RID: 38874 RVA: 0x0014CC08 File Offset: 0x0014AE08
		private void ComputeObsoleteInternalVertices(DecalsMesh a_DecalsMesh)
		{
			List<OptimizeEdge> list = this.m_RedundantInternalEdges.OptimizedEdgeList();
			for (int i = this.m_ObsoleteInternalVertexIndices.Count - 1; i >= 0; i--)
			{
				int num = this.m_ObsoleteInternalVertexIndices[i];
				if (!this.m_ObsoleteVertexFinder.IsInternalVertexObsolete(a_DecalsMesh, num, list))
				{
					this.m_ObsoleteInternalVertexIndices.RemoveAt(i);
				}
			}
		}

		// Token: 0x060097DB RID: 38875 RVA: 0x0014CC64 File Offset: 0x0014AE64
		private void ComputeObsoleteExternalVertices(DecalsMesh a_DecalsMesh)
		{
			for (int i = this.m_ObsoleteExternalVertexIndices.Count - 1; i >= 0; i--)
			{
				int num = this.m_ObsoleteExternalVertexIndices[i];
				if (!this.m_ObsoleteVertexFinder.IsExternalVertexObsolete(a_DecalsMesh, num))
				{
					this.m_ObsoleteExternalVertexIndices.RemoveAt(i);
				}
			}
		}

		// Token: 0x060097DC RID: 38876 RVA: 0x0014CCB4 File Offset: 0x0014AEB4
		private void RemoveObsoleteVertices(DecalsMesh a_DecalsMesh)
		{
			DecalProjectorBase activeDecalProjector = a_DecalsMesh.ActiveDecalProjector;
			this.m_ObsoleteInternalVertexIndices.Sort();
			List<OptimizeEdge> list = this.m_RedundantInternalEdges.OptimizedEdgeList();
			for (int i = this.m_ObsoleteInternalVertexIndices.Count - 1; i >= 0; i--)
			{
				int num = this.m_ObsoleteInternalVertexIndices[i];
				bool flag;
				this.m_ObsoleteVertexRemover.RemoveObsoleteInternalVertex(a_DecalsMesh, num, list, out flag);
				if (!flag)
				{
					this.m_ObsoleteInternalVertexIndices.RemoveAt(i);
				}
			}
			for (int j = this.m_ObsoleteExternalVertexIndices.Count - 1; j >= 0; j--)
			{
				int num2 = this.m_ObsoleteExternalVertexIndices[j];
				bool flag2;
				this.m_ObsoleteVertexRemover.RemoveObsoleteExternalVertex(a_DecalsMesh, num2, out flag2);
				if (!flag2)
				{
					this.m_ObsoleteExternalVertexIndices.RemoveAt(j);
				}
			}
			this.m_ObsoleteInternalVertexIndices.AddRange(this.m_ObsoleteExternalVertexIndices);
			this.m_ObsoleteInternalVertexIndices.Sort();
			foreach (int num3 in this.m_ObsoleteInternalVertexIndices)
			{
				this.m_RemovedIndices.AddRemovedIndex(num3);
			}
			a_DecalsMesh.RemoveAndAdjustIndices(activeDecalProjector.DecalsMeshLowerTriangleIndex, this.m_RemovedIndices);
			activeDecalProjector.IsUV1ProjectionCalculated = false;
			activeDecalProjector.IsUV2ProjectionCalculated = false;
			activeDecalProjector.IsTangentProjectionCalculated = false;
		}

		// Token: 0x060097DD RID: 38877 RVA: 0x0014CE04 File Offset: 0x0014B004
		private static bool AreEdgesParallelOrIsAtLeastOneAPoint(DecalsMesh a_DecalsMesh, OptimizeEdge a_Edge1, OptimizeEdge a_Edge2)
		{
			bool flag = false;
			Vector3 vector = a_DecalsMesh.Vertices[a_Edge1.vertex1Index];
			Vector3 vector2 = a_DecalsMesh.Vertices[a_Edge1.vertex2Index];
			Vector3 vector3 = a_DecalsMesh.Vertices[a_Edge2.vertex1Index];
			Vector3 vector4 = a_DecalsMesh.Vertices[a_Edge2.vertex2Index];
			Vector3 vector5 = vector2 - vector;
			Vector3 vector6 = vector4 - vector3;
			vector5.Normalize();
			vector6.Normalize();
			if (Vector3Extension.Approximately(vector5, Vector3.zero, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError) || Vector3Extension.Approximately(vector6, Vector3.zero, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError))
			{
				flag = true;
			}
			else if (MathfExtension.Approximately(Mathf.Abs(Vector3.Dot(vector5, vector6)), 1f, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060097DE RID: 38878 RVA: 0x0005AA3F File Offset: 0x00058C3F
		private void ClearAll()
		{
			this.m_RedundantInternalEdges.Clear();
			this.m_ObsoleteInternalVertexIndices.Clear();
			this.m_ObsoleteExternalVertexIndices.Clear();
			this.m_RemovedIndices.Clear();
			this.ClearTemporaryCollections();
		}

		// Token: 0x060097DF RID: 38879 RVA: 0x0005AA73 File Offset: 0x00058C73
		private void ClearTemporaryCollections()
		{
			this.m_RedundantExternalEdges.Clear();
			this.m_OverusedInternalEdges.Clear();
			this.m_NonInternalVertexIndices.Clear();
		}

		// Token: 0x040063BF RID: 25535
		internal static float s_CurrentMaximumAbsoluteError = 0.0001f;

		// Token: 0x040063C0 RID: 25536
		internal static float s_CurrentMaximumRelativeError = 0.0001f;

		// Token: 0x040063C1 RID: 25537
		private readonly SortedDictionary<int, int> m_NonInternalVertexIndices = new SortedDictionary<int, int>();

		// Token: 0x040063C2 RID: 25538
		private readonly List<int> m_ObsoleteExternalVertexIndices = new List<int>();

		// Token: 0x040063C3 RID: 25539
		private readonly List<int> m_ObsoleteInternalVertexIndices = new List<int>();

		// Token: 0x040063C4 RID: 25540
		private readonly ObsoleteVertexFinder m_ObsoleteVertexFinder = new ObsoleteVertexFinder();

		// Token: 0x040063C5 RID: 25541
		private readonly ObsoleteVertexRemover m_ObsoleteVertexRemover = new ObsoleteVertexRemover();

		// Token: 0x040063C6 RID: 25542
		private readonly OptimizeEdges m_OverusedInternalEdges = new OptimizeEdges();

		// Token: 0x040063C7 RID: 25543
		private readonly OptimizeEdges m_RedundantExternalEdges = new OptimizeEdges();

		// Token: 0x040063C8 RID: 25544
		private readonly OptimizeEdges m_RedundantInternalEdges = new OptimizeEdges();

		// Token: 0x040063C9 RID: 25545
		private readonly RemovedIndices m_RemovedIndices = new RemovedIndices();
	}
}
