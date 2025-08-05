using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B0A RID: 11018
	[NullableContext(1)]
	[Nullable(0)]
	internal class ObsoleteVertexRemover
	{
		// Token: 0x060098A6 RID: 39078 RVA: 0x00150398 File Offset: 0x0014E598
		internal void RemoveObsoleteInternalVertex(DecalsMesh a_DecalsMesh, int a_VertexIndex, List<OptimizeEdge> a_RedundantInternalEdges, out bool a_SuccessfulRemoval)
		{
			this.InitializeNeighboringTriangles(a_DecalsMesh, a_VertexIndex);
			this.InitializeSortedNeighboringInternalVertices(a_DecalsMesh, a_VertexIndex);
			if (this.m_SortedNeighboringVerticesInitialized)
			{
				this.RemoveUnneededTriangles(a_DecalsMesh);
				this.AddNewTriangles(a_DecalsMesh);
				a_DecalsMesh.ActiveDecalProjector.DecalsMeshUpperTriangleIndex = a_DecalsMesh.Triangles.Count - 1;
				a_SuccessfulRemoval = true;
			}
			else
			{
				a_SuccessfulRemoval = false;
			}
			this.Clear();
		}

		// Token: 0x060098A7 RID: 39079 RVA: 0x001503F4 File Offset: 0x0014E5F4
		internal void RemoveObsoleteExternalVertex(DecalsMesh a_DecalsMesh, int a_VertexIndex, out bool a_SuccessfulRemoval)
		{
			this.InitializeNeighboringTriangles(a_DecalsMesh, a_VertexIndex);
			this.InitializeSortedNeighboringExternalVertices(a_DecalsMesh, a_VertexIndex);
			if (this.m_SortedNeighboringVerticesInitialized)
			{
				this.RemoveUnneededTriangles(a_DecalsMesh);
				this.AddNewTriangles(a_DecalsMesh);
				a_DecalsMesh.ActiveDecalProjector.DecalsMeshUpperTriangleIndex = a_DecalsMesh.Triangles.Count - 1;
				a_SuccessfulRemoval = true;
			}
			else
			{
				a_SuccessfulRemoval = false;
			}
			this.Clear();
		}

		// Token: 0x060098A8 RID: 39080 RVA: 0x00150450 File Offset: 0x0014E650
		private void InitializeNeighboringTriangles(DecalsMesh a_DecalsMesh, int a_VertexIndex)
		{
			GenericDecalProjectorBase activeDecalProjector = a_DecalsMesh.ActiveDecalProjector;
			bool flag = false;
			for (int i = activeDecalProjector.DecalsMeshLowerTriangleIndex; i < a_DecalsMesh.Triangles.Count; i += 3)
			{
				int num = a_DecalsMesh.Triangles[i];
				int num2 = a_DecalsMesh.Triangles[i + 1];
				int num3 = a_DecalsMesh.Triangles[i + 2];
				if (num == a_VertexIndex || num2 == a_VertexIndex || num3 == a_VertexIndex)
				{
					this.m_NeighboringTriangles.Add(i);
					if (!flag)
					{
						Vector3 vector = a_DecalsMesh.Vertices[num];
						Vector3 vector2 = a_DecalsMesh.Vertices[num2];
						Vector3 vector3 = a_DecalsMesh.Vertices[num3];
						this.m_ReferenceTriangleNormal = GeometryUtilities.TriangleNormal(vector, vector2, vector3);
						if (!Vector3Extension.Approximately(this.m_ReferenceTriangleNormal, Vector3.zero, DecalsMeshMinimizer.s_CurrentMaximumAbsoluteError, DecalsMeshMinimizer.s_CurrentMaximumRelativeError))
						{
							flag = true;
						}
					}
				}
			}
		}

		// Token: 0x060098A9 RID: 39081 RVA: 0x00150528 File Offset: 0x0014E728
		private void InitializeSortedNeighboringInternalVertices(DecalsMesh a_DecalsMesh, int a_VertexIndex)
		{
			this.m_SortedNeighboringVerticesInitialized = true;
			this.m_UnusedNeighboringTriangles.AddRange(this.m_NeighboringTriangles);
			int num = this.m_UnusedNeighboringTriangles[this.m_UnusedNeighboringTriangles.Count - 1];
			int num2 = this.InnerTriangleIndexOfVertexIndex(a_DecalsMesh, num, a_VertexIndex);
			num2 = this.SuccessorInnerTriangleVertexIndex(num2);
			this.m_SortedNeighboringVertices.Add(a_DecalsMesh.Triangles[num + num2]);
			num2 = this.SuccessorInnerTriangleVertexIndex(num2);
			this.m_SortedNeighboringVertices.Add(a_DecalsMesh.Triangles[num + num2]);
			this.m_UnusedNeighboringTriangles.RemoveAt(this.m_UnusedNeighboringTriangles.Count - 1);
			while (this.m_UnusedNeighboringTriangles.Count > 1)
			{
				bool flag = false;
				int num3 = this.m_SortedNeighboringVertices[this.m_SortedNeighboringVertices.Count - 1];
				for (int i = 0; i < this.m_UnusedNeighboringTriangles.Count; i++)
				{
					int num4 = this.m_UnusedNeighboringTriangles[i];
					int num5 = this.InnerTriangleIndexOfVertexIndex(a_DecalsMesh, num4, a_VertexIndex);
					num5 = this.SuccessorInnerTriangleVertexIndex(num5);
					if (a_DecalsMesh.Triangles[num4 + num5] == num3)
					{
						num5 = this.SuccessorInnerTriangleVertexIndex(num5);
						int num6 = a_DecalsMesh.Triangles[num4 + num5];
						this.m_SortedNeighboringVertices.Add(num6);
						this.m_UnusedNeighboringTriangles.RemoveAt(i);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.m_SortedNeighboringVerticesInitialized = false;
					break;
				}
			}
			if (this.m_SortedNeighboringVerticesInitialized)
			{
				int num7 = this.m_SortedNeighboringVertices[this.m_SortedNeighboringVertices.Count - 1];
				int num8 = this.m_UnusedNeighboringTriangles[0];
				int num9 = this.InnerTriangleIndexOfVertexIndex(a_DecalsMesh, num8, a_VertexIndex);
				num9 = this.SuccessorInnerTriangleVertexIndex(num9);
				if (num7 != a_DecalsMesh.Triangles[num8 + num9])
				{
					throw new InvalidOperationException("The last triangle doesn't match the previous ones.");
				}
				num9 = this.SuccessorInnerTriangleVertexIndex(num9);
				if (this.m_SortedNeighboringVertices[0] != a_DecalsMesh.Triangles[num8 + num9])
				{
					throw new InvalidOperationException("The last triangle doesn't match to the first one.");
				}
			}
		}

		// Token: 0x060098AA RID: 39082 RVA: 0x00150730 File Offset: 0x0014E930
		private void InitializeSortedNeighboringExternalVertices(DecalsMesh a_DecalsMesh, int a_VertexIndex)
		{
			this.m_SortedNeighboringVerticesInitialized = true;
			this.m_UnusedNeighboringTriangles.AddRange(this.m_NeighboringTriangles);
			if (this.m_UnusedNeighboringTriangles.Count <= 0)
			{
				this.m_SortedNeighboringVerticesInitialized = false;
				return;
			}
			int num = this.m_UnusedNeighboringTriangles[this.m_UnusedNeighboringTriangles.Count - 1];
			int num2 = this.InnerTriangleIndexOfVertexIndex(a_DecalsMesh, num, a_VertexIndex);
			num2 = this.SuccessorInnerTriangleVertexIndex(num2);
			this.m_SortedNeighboringVertices.Add(a_DecalsMesh.Triangles[num + num2]);
			num2 = this.SuccessorInnerTriangleVertexIndex(num2);
			this.m_SortedNeighboringVertices.Add(a_DecalsMesh.Triangles[num + num2]);
			this.m_UnusedNeighboringTriangles.RemoveAt(this.m_UnusedNeighboringTriangles.Count - 1);
			while (this.m_UnusedNeighboringTriangles.Count != 0)
			{
				bool flag = false;
				int num3 = this.m_SortedNeighboringVertices[0];
				int num4 = this.m_SortedNeighboringVertices[this.m_SortedNeighboringVertices.Count - 1];
				for (int i = 0; i < this.m_UnusedNeighboringTriangles.Count; i++)
				{
					int num5 = this.m_UnusedNeighboringTriangles[i];
					int num6 = this.InnerTriangleIndexOfVertexIndex(a_DecalsMesh, num5, a_VertexIndex);
					num6 = this.SuccessorInnerTriangleVertexIndex(num6);
					if (a_DecalsMesh.Triangles[num5 + num6] == num4)
					{
						num6 = this.SuccessorInnerTriangleVertexIndex(num6);
						int num7 = a_DecalsMesh.Triangles[num5 + num6];
						this.m_SortedNeighboringVertices.Add(num7);
						this.m_UnusedNeighboringTriangles.RemoveAt(i);
						flag = true;
						break;
					}
					int num8 = num6;
					num6 = this.SuccessorInnerTriangleVertexIndex(num6);
					if (a_DecalsMesh.Triangles[num5 + num6] == num3)
					{
						int num9 = a_DecalsMesh.Triangles[num5 + num8];
						this.m_SortedNeighboringVertices.Insert(0, num9);
						this.m_UnusedNeighboringTriangles.RemoveAt(i);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.m_SortedNeighboringVerticesInitialized = false;
					return;
				}
			}
		}

		// Token: 0x060098AB RID: 39083 RVA: 0x00150918 File Offset: 0x0014EB18
		private int InnerTriangleIndexOfVertexIndex(DecalsMesh a_DecalsMesh, int a_TriangleIndex, int a_VertexIndex)
		{
			if (a_DecalsMesh.Triangles[a_TriangleIndex] == a_VertexIndex)
			{
				return 0;
			}
			if (a_DecalsMesh.Triangles[a_TriangleIndex + 1] == a_VertexIndex)
			{
				return 1;
			}
			if (a_DecalsMesh.Triangles[a_TriangleIndex + 2] == a_VertexIndex)
			{
				return 2;
			}
			throw new InvalidOperationException("The vertex index argument is not in the provided triangle.");
		}

		// Token: 0x060098AC RID: 39084 RVA: 0x00150968 File Offset: 0x0014EB68
		private int SuccessorInnerTriangleVertexIndex(int a_InnerTriangleVertexIndex)
		{
			int num = a_InnerTriangleVertexIndex + 1;
			if (num == 3)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060098AD RID: 39085 RVA: 0x00150980 File Offset: 0x0014EB80
		private void RemoveUnneededTriangles(DecalsMesh a_DecalsMesh)
		{
			this.m_NeighboringTriangles.Sort();
			for (int i = this.m_NeighboringTriangles.Count - 1; i >= 0; i--)
			{
				int num = this.m_NeighboringTriangles[i];
				int num2 = 1;
				while (i > 0 && num - 3 == this.m_NeighboringTriangles[i - 1])
				{
					num -= 3;
					num2++;
					i--;
				}
				a_DecalsMesh.RemoveTrianglesAt(num, num2);
			}
		}

		// Token: 0x060098AE RID: 39086 RVA: 0x001509EC File Offset: 0x0014EBEC
		private void AddNewTriangles(DecalsMesh a_DecalsMesh)
		{
			while (this.m_SortedNeighboringVertices.Count >= 3)
			{
				bool flag = false;
				for (int i = 0; i < this.m_SortedNeighboringVertices.Count; i++)
				{
					int num = i + 1;
					int num2 = i + 2;
					if (num >= this.m_SortedNeighboringVertices.Count)
					{
						num -= this.m_SortedNeighboringVertices.Count;
						num2 -= this.m_SortedNeighboringVertices.Count;
					}
					else if (num2 >= this.m_SortedNeighboringVertices.Count)
					{
						num2 -= this.m_SortedNeighboringVertices.Count;
					}
					int num3 = this.m_SortedNeighboringVertices[i];
					int num4 = this.m_SortedNeighboringVertices[num];
					int num5 = this.m_SortedNeighboringVertices[num2];
					Vector3 vector = a_DecalsMesh.Vertices[num3];
					Vector3 vector2 = a_DecalsMesh.Vertices[num4];
					Vector3 vector3 = a_DecalsMesh.Vertices[num5];
					Vector3 vector4 = GeometryUtilities.TriangleNormal(vector, vector2, vector3);
					if (Vector3.Dot(this.m_ReferenceTriangleNormal, vector4) >= 0f)
					{
						a_DecalsMesh.Triangles.Add(num3);
						a_DecalsMesh.Triangles.Add(num4);
						a_DecalsMesh.Triangles.Add(num5);
						this.m_SortedNeighboringVertices.RemoveAt(num);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
			}
		}

		// Token: 0x060098AF RID: 39087 RVA: 0x0005B1DF File Offset: 0x000593DF
		private void Clear()
		{
			this.m_NeighboringTriangles.Clear();
			this.m_SortedNeighboringVertices.Clear();
			this.m_UnusedNeighboringTriangles.Clear();
		}

		// Token: 0x04006425 RID: 25637
		private readonly List<int> m_NeighboringTriangles = new List<int>();

		// Token: 0x04006426 RID: 25638
		private Vector3 m_ReferenceTriangleNormal;

		// Token: 0x04006427 RID: 25639
		private readonly List<int> m_SortedNeighboringVertices = new List<int>();

		// Token: 0x04006428 RID: 25640
		private bool m_SortedNeighboringVerticesInitialized;

		// Token: 0x04006429 RID: 25641
		private readonly List<int> m_UnusedNeighboringTriangles = new List<int>();
	}
}
