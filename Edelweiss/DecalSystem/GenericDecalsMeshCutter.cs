using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B02 RID: 11010
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GenericDecalsMeshCutter<[Nullable(0)] D, [Nullable(0)] P, [Nullable(0)] DM> where D : GenericDecals<D, P, DM> where P : GenericDecalProjector<D, P, DM> where DM : GenericDecalsMesh<D, P, DM>
	{
		// Token: 0x06009886 RID: 39046
		internal abstract void InitializeDelegates();

		// Token: 0x06009887 RID: 39047 RVA: 0x0014EB60 File Offset: 0x0014CD60
		public void CutDecalsPlanes(DM a_DecalsMesh)
		{
			if (a_DecalsMesh == null)
			{
				throw new ArgumentNullException("The decals mesh argument is null!");
			}
			if (a_DecalsMesh.ActiveDecalProjector == null)
			{
				throw new NullReferenceException("The active decal projector of the decals mesh is null!");
			}
			this.m_DecalsMesh = a_DecalsMesh;
			this.m_ActiveProjector = a_DecalsMesh.ActiveDecalProjector;
			this.InitializeDelegates();
			Matrix4x4 matrix4x = a_DecalsMesh.Decals.CachedTransform.worldToLocalMatrix * this.m_ActiveProjector.ProjectorToWorldMatrix;
			Matrix4x4 transpose = matrix4x.inverse.transpose;
			Vector3 vector = matrix4x.MultiplyPoint3x4(new Vector3(0.5f, 0f, 0f));
			Vector3 vector2 = transpose.MultiplyVector(new Vector3(1f, 0f, 0f)).normalized;
			Plane plane = new Plane(vector2, -vector);
			this.PlaneDecalsMeshCutter(a_DecalsMesh, plane);
			vector = matrix4x.MultiplyPoint3x4(new Vector3(-0.5f, 0f, 0f));
			vector2 = transpose.MultiplyVector(new Vector3(-1f, 0f, 0f)).normalized;
			plane = new Plane(vector2, -vector);
			this.PlaneDecalsMeshCutter(a_DecalsMesh, plane);
			vector = matrix4x.MultiplyPoint3x4(new Vector3(0f, 0f, 0.5f));
			vector2 = transpose.MultiplyVector(new Vector3(0f, 0f, 1f)).normalized;
			plane = new Plane(vector2, -vector);
			this.PlaneDecalsMeshCutter(a_DecalsMesh, plane);
			vector = matrix4x.MultiplyPoint3x4(new Vector3(0f, 0f, -0.5f));
			vector2 = transpose.MultiplyVector(new Vector3(0f, 0f, -1f)).normalized;
			plane = new Plane(vector2, -vector);
			this.PlaneDecalsMeshCutter(a_DecalsMesh, plane);
			vector = matrix4x.MultiplyPoint3x4(new Vector3(0f, 0f, 0f));
			vector2 = transpose.MultiplyVector(new Vector3(0f, 1f, 0f)).normalized;
			plane = new Plane(vector2, -vector);
			this.PlaneDecalsMeshCutter(a_DecalsMesh, plane);
			vector = matrix4x.MultiplyPoint3x4(new Vector3(0f, -1f, 0f));
			vector2 = transpose.MultiplyVector(new Vector3(0f, -1f, 0f)).normalized;
			plane = new Plane(vector2, -vector);
			this.PlaneDecalsMeshCutter(a_DecalsMesh, plane);
		}

		// Token: 0x06009888 RID: 39048 RVA: 0x0014EE0C File Offset: 0x0014D00C
		internal void PlaneDecalsMeshCutter(DM a_DecalsMesh, Plane a_Plane)
		{
			this.m_RelativeVertexLocations.Clear();
			this.m_ObsoleteTriangleIndices.Clear();
			this.m_CutEdges.Clear();
			this.m_RemovedIndices.Clear();
			int decalsMeshLowerVertexIndex = this.m_ActiveProjector.DecalsMeshLowerVertexIndex;
			int decalsMeshUpperVertexIndex = this.m_ActiveProjector.DecalsMeshUpperVertexIndex;
			Vector3 vector = this.PlaneOrigin(a_Plane);
			Vector3 normal = a_Plane.normal;
			for (int i = decalsMeshLowerVertexIndex; i <= decalsMeshUpperVertexIndex; i++)
			{
				Vector3 vector2 = a_DecalsMesh.Vertices[i];
				RelativeVertexLocation relativeVertexLocation = this.VertexLocationRelativeToPlane(vector, normal, vector2);
				this.m_RelativeVertexLocations.Add(relativeVertexLocation);
			}
			int count = a_DecalsMesh.Triangles.Count;
			int decalsMeshLowerTriangleIndex = this.m_ActiveProjector.DecalsMeshLowerTriangleIndex;
			for (int j = decalsMeshLowerTriangleIndex; j < count; j += 3)
			{
				int num = a_DecalsMesh.Triangles[j];
				int num2 = a_DecalsMesh.Triangles[j + 1];
				int num3 = a_DecalsMesh.Triangles[j + 2];
				RelativeVertexLocation relativeVertexLocation2 = this.m_RelativeVertexLocations[num - decalsMeshLowerVertexIndex];
				RelativeVertexLocation relativeVertexLocation3 = this.m_RelativeVertexLocations[num2 - decalsMeshLowerVertexIndex];
				RelativeVertexLocation relativeVertexLocation4 = this.m_RelativeVertexLocations[num3 - decalsMeshLowerVertexIndex];
				if (relativeVertexLocation2 != RelativeVertexLocation.Inside && relativeVertexLocation3 != RelativeVertexLocation.Inside && relativeVertexLocation4 != RelativeVertexLocation.Inside)
				{
					this.m_ObsoleteTriangleIndices.Add(j);
				}
				else
				{
					Vector3 vector3 = a_DecalsMesh.Vertices[num];
					Vector3 vector4 = a_DecalsMesh.Vertices[num2];
					Vector3 vector5 = a_DecalsMesh.Vertices[num3];
					bool flag = this.Intersect(relativeVertexLocation2, relativeVertexLocation3);
					bool flag2 = this.Intersect(relativeVertexLocation2, relativeVertexLocation4);
					bool flag3 = this.Intersect(relativeVertexLocation3, relativeVertexLocation4);
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					if (flag)
					{
						DecalRay decalRay = new DecalRay(vector3, vector4 - vector3);
						float num7 = this.IntersectionFactor(decalRay, a_Plane);
						num4 = this.m_GetCutEdgeDelegate(decalsMeshLowerVertexIndex, num, num2, vector3, vector4, relativeVertexLocation2 == RelativeVertexLocation.Inside, num7);
					}
					if (flag2)
					{
						DecalRay decalRay2 = new DecalRay(vector3, vector5 - vector3);
						float num8 = this.IntersectionFactor(decalRay2, a_Plane);
						num5 = this.m_GetCutEdgeDelegate(decalsMeshLowerVertexIndex, num, num3, vector3, vector5, relativeVertexLocation2 == RelativeVertexLocation.Inside, num8);
					}
					if (flag3)
					{
						DecalRay decalRay3 = new DecalRay(vector4, vector5 - vector4);
						float num9 = this.IntersectionFactor(decalRay3, a_Plane);
						num6 = this.m_GetCutEdgeDelegate(decalsMeshLowerVertexIndex, num2, num3, vector4, vector5, relativeVertexLocation3 == RelativeVertexLocation.Inside, num9);
					}
					if (flag && flag2)
					{
						if (relativeVertexLocation2 == RelativeVertexLocation.Inside)
						{
							a_DecalsMesh.Triangles[j + 1] = num4;
							a_DecalsMesh.Triangles[j + 2] = num5;
						}
						else
						{
							a_DecalsMesh.Triangles[j] = num4;
							a_DecalsMesh.Triangles.Add(num3);
							a_DecalsMesh.Triangles.Add(num5);
							a_DecalsMesh.Triangles.Add(num4);
						}
					}
					else if (flag && flag3)
					{
						if (relativeVertexLocation3 == RelativeVertexLocation.Inside)
						{
							a_DecalsMesh.Triangles[j] = num4;
							a_DecalsMesh.Triangles[j + 2] = num6;
						}
						else
						{
							a_DecalsMesh.Triangles[j + 1] = num4;
							a_DecalsMesh.Triangles.Add(num3);
							a_DecalsMesh.Triangles.Add(num4);
							a_DecalsMesh.Triangles.Add(num6);
						}
					}
					else if (flag2 && flag3)
					{
						if (relativeVertexLocation4 == RelativeVertexLocation.Inside)
						{
							a_DecalsMesh.Triangles[j] = num5;
							a_DecalsMesh.Triangles[j + 1] = num6;
						}
						else
						{
							a_DecalsMesh.Triangles[j + 2] = num5;
							a_DecalsMesh.Triangles.Add(num2);
							a_DecalsMesh.Triangles.Add(num6);
							a_DecalsMesh.Triangles.Add(num5);
						}
					}
					else if (flag && relativeVertexLocation4 == RelativeVertexLocation.OnPlane)
					{
						if (relativeVertexLocation2 == RelativeVertexLocation.Inside)
						{
							a_DecalsMesh.Triangles[j + 1] = num4;
						}
						else
						{
							a_DecalsMesh.Triangles[j] = num4;
						}
					}
					else if (flag2 && relativeVertexLocation3 == RelativeVertexLocation.OnPlane)
					{
						if (relativeVertexLocation2 == RelativeVertexLocation.Inside)
						{
							a_DecalsMesh.Triangles[j + 2] = num5;
						}
						else
						{
							a_DecalsMesh.Triangles[j] = num5;
						}
					}
					else if (flag3 && relativeVertexLocation2 == RelativeVertexLocation.OnPlane)
					{
						if (relativeVertexLocation3 == RelativeVertexLocation.Inside)
						{
							a_DecalsMesh.Triangles[j + 2] = num6;
						}
						else
						{
							a_DecalsMesh.Triangles[j + 1] = num6;
						}
					}
				}
			}
			for (int k = this.m_ObsoleteTriangleIndices.Count - 1; k >= 0; k--)
			{
				int num10 = this.m_ObsoleteTriangleIndices[k];
				int num11 = 1;
				while (k > 0 && num10 - 3 == this.m_ObsoleteTriangleIndices[k - 1])
				{
					num10 -= 3;
					num11++;
					k--;
				}
				a_DecalsMesh.RemoveTrianglesAt(num10, num11);
			}
			for (int l = 0; l < this.m_RelativeVertexLocations.Count; l++)
			{
				if (this.m_RelativeVertexLocations[l] == RelativeVertexLocation.Outside)
				{
					this.m_RemovedIndices.AddRemovedIndex(l + decalsMeshLowerVertexIndex);
				}
			}
			a_DecalsMesh.RemoveAndAdjustIndices(decalsMeshLowerTriangleIndex, this.m_RemovedIndices);
			this.m_ActiveProjector.IsUV1ProjectionCalculated = false;
			this.m_ActiveProjector.IsUV2ProjectionCalculated = false;
			this.m_ActiveProjector.IsTangentProjectionCalculated = false;
		}

		// Token: 0x06009889 RID: 39049 RVA: 0x0005B0D4 File Offset: 0x000592D4
		private bool Intersect(RelativeVertexLocation a_VertexLocation1, RelativeVertexLocation a_VertexLocation2)
		{
			return a_VertexLocation1 != RelativeVertexLocation.OnPlane && a_VertexLocation2 != RelativeVertexLocation.OnPlane && a_VertexLocation1 != a_VertexLocation2;
		}

		// Token: 0x0600988A RID: 39050 RVA: 0x0014F3F4 File Offset: 0x0014D5F4
		private float IntersectionFactor(DecalRay a_Ray, Plane a_Plane)
		{
			float num = Vector3.Dot(a_Plane.normal, a_Ray.direction);
			return (a_Plane.distance - Vector3.Dot(a_Plane.normal, a_Ray.origin)) / num;
		}

		// Token: 0x0600988B RID: 39051 RVA: 0x0005B0E5 File Offset: 0x000592E5
		private Vector3 PlaneOrigin(Plane a_Plane)
		{
			return a_Plane.distance * a_Plane.normal;
		}

		// Token: 0x0600988C RID: 39052 RVA: 0x0014F430 File Offset: 0x0014D630
		private RelativeVertexLocation VertexLocationRelativeToPlane(Vector3 a_PlaneOrigin, Vector3 a_PlaneNormal, Vector3 a_Vertex)
		{
			float num = Vector3.Dot(a_Vertex - a_PlaneOrigin, a_PlaneNormal);
			if (Mathf.Approximately(num, 0f))
			{
				return RelativeVertexLocation.OnPlane;
			}
			if (num < 0f)
			{
				return RelativeVertexLocation.Inside;
			}
			return RelativeVertexLocation.Outside;
		}

		// Token: 0x0600988D RID: 39053 RVA: 0x0014F468 File Offset: 0x0014D668
		internal int CutEdge(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			if (this.m_CutEdges.HasEdge(cutEdge))
			{
				return this.m_CutEdges[cutEdge].ModifiedIndex;
			}
			return this.m_CreateCutEdgeDelegate(a_RelativeVertexLocationsOffset, a_IndexA, a_IndexB, a_VertexA, a_VertexB, a_IsVertexAInside, a_IntersectionFactorAB);
		}

		// Token: 0x0400640D RID: 25613
		internal P m_ActiveProjector;

		// Token: 0x0400640E RID: 25614
		internal CutEdgeDelegate m_CreateCutEdgeDelegate;

		// Token: 0x0400640F RID: 25615
		internal CutEdges m_CutEdges = new CutEdges();

		// Token: 0x04006410 RID: 25616
		internal DM m_DecalsMesh;

		// Token: 0x04006411 RID: 25617
		internal CutEdgeDelegate m_GetCutEdgeDelegate;

		// Token: 0x04006412 RID: 25618
		internal List<int> m_ObsoleteTriangleIndices = new List<int>();

		// Token: 0x04006413 RID: 25619
		internal List<RelativeVertexLocation> m_RelativeVertexLocations = new List<RelativeVertexLocation>();

		// Token: 0x04006414 RID: 25620
		internal RemovedIndices m_RemovedIndices = new RemovedIndices();
	}
}
