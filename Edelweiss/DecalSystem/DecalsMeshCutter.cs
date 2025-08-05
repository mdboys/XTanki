using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AF5 RID: 10997
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public class DecalsMeshCutter : GenericDecalsMeshCutter<Decals, DecalProjectorBase, DecalsMesh>
	{
		// Token: 0x060097CB RID: 38859 RVA: 0x0014B680 File Offset: 0x00149880
		internal override void InitializeDelegates()
		{
			this.m_GetCutEdgeDelegate = new CutEdgeDelegate(base.CutEdge);
			bool flag = this.m_DecalsMesh.Decals.CurrentTangentsMode == TangentsMode.Target;
			UVMode currentUVMode = this.m_DecalsMesh.Decals.CurrentUVMode;
			bool flag2 = currentUVMode - UVMode.TargetUV <= 1;
			bool flag3 = flag2;
			UV2Mode currentUV2Mode = this.m_DecalsMesh.Decals.CurrentUV2Mode;
			flag2 = currentUV2Mode - UV2Mode.TargetUV <= 1;
			bool flag4 = flag2;
			if (!flag && !flag3 && !flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeOptimizedVerticesNormals);
				return;
			}
			if (!flag && !flag3 && flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeOptimizedVerticesNormalsUV2s);
				return;
			}
			if (!flag && flag3 && !flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeOptimizedVerticesNormalsUVs);
				return;
			}
			if (!flag && flag3 && flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeOptimizedVerticesNormalsUVsUV2s);
				return;
			}
			if (flag && !flag3 && !flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeOptimizedVerticesNormalsTangents);
				return;
			}
			if (flag && !flag3 && flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeOptimizedVerticesNormalsTangentsUV2s);
				return;
			}
			if (flag && flag3 && !flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeOptimizedVerticesNormalsTangentsUVs);
				return;
			}
			if (flag && flag3 && flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeOptimizedVerticesNormalsTangentsUVsUV2s);
			}
		}

		// Token: 0x060097CC RID: 38860 RVA: 0x0014B7E4 File Offset: 0x001499E4
		private int CutEdgeOptimizedVerticesNormals(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(this.m_DecalsMesh.Normals[a_IndexA], this.m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}

		// Token: 0x060097CD RID: 38861 RVA: 0x0014B938 File Offset: 0x00149B38
		private int CutEdgeOptimizedVerticesNormalsTangents(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(this.m_DecalsMesh.Normals[a_IndexA], this.m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.Tangents.Add(Vector4.Lerp(this.m_DecalsMesh.Tangents[a_IndexA], this.m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}

		// Token: 0x060097CE RID: 38862 RVA: 0x0014BAC4 File Offset: 0x00149CC4
		private int CutEdgeOptimizedVerticesNormalsUVs(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(this.m_DecalsMesh.Normals[a_IndexA], this.m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.UVs.Add(Vector2.Lerp(this.m_DecalsMesh.UVs[a_IndexA], this.m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}

		// Token: 0x060097CF RID: 38863 RVA: 0x0014BC50 File Offset: 0x00149E50
		private int CutEdgeOptimizedVerticesNormalsTangentsUVs(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(this.m_DecalsMesh.Normals[a_IndexA], this.m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.Tangents.Add(Vector4.Lerp(this.m_DecalsMesh.Tangents[a_IndexA], this.m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.UVs.Add(Vector2.Lerp(this.m_DecalsMesh.UVs[a_IndexA], this.m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}

		// Token: 0x060097D0 RID: 38864 RVA: 0x0014BE18 File Offset: 0x0014A018
		private int CutEdgeOptimizedVerticesNormalsUV2s(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(this.m_DecalsMesh.Normals[a_IndexA], this.m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.UV2s.Add(Vector2.Lerp(this.m_DecalsMesh.UV2s[a_IndexA], this.m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}

		// Token: 0x060097D1 RID: 38865 RVA: 0x0014BFA4 File Offset: 0x0014A1A4
		private int CutEdgeOptimizedVerticesNormalsTangentsUV2s(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(this.m_DecalsMesh.Normals[a_IndexA], this.m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.Tangents.Add(Vector4.Lerp(this.m_DecalsMesh.Tangents[a_IndexA], this.m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.UV2s.Add(Vector2.Lerp(this.m_DecalsMesh.UV2s[a_IndexA], this.m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}

		// Token: 0x060097D2 RID: 38866 RVA: 0x0014C16C File Offset: 0x0014A36C
		private int CutEdgeOptimizedVerticesNormalsUVsUV2s(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(this.m_DecalsMesh.Normals[a_IndexA], this.m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.UVs.Add(Vector2.Lerp(this.m_DecalsMesh.UVs[a_IndexA], this.m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.UV2s.Add(Vector2.Lerp(this.m_DecalsMesh.UV2s[a_IndexA], this.m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}

		// Token: 0x060097D3 RID: 38867 RVA: 0x0014C334 File Offset: 0x0014A534
		private int CutEdgeOptimizedVerticesNormalsTangentsUVsUV2s(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(this.m_DecalsMesh.Normals[a_IndexA], this.m_DecalsMesh.Normals[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.Tangents.Add(Vector4.Lerp(this.m_DecalsMesh.Tangents[a_IndexA], this.m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.UVs.Add(Vector2.Lerp(this.m_DecalsMesh.UVs[a_IndexA], this.m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			this.m_DecalsMesh.UV2s.Add(Vector2.Lerp(this.m_DecalsMesh.UV2s[a_IndexA], this.m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}

		// Token: 0x060097D4 RID: 38868 RVA: 0x0014C534 File Offset: 0x0014A734
		private int CutEdgeUnoptimized(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			Vector3 vector = this.m_DecalsMesh.Normals[a_IndexA];
			Vector3 vector2 = this.m_DecalsMesh.Normals[a_IndexB];
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(vector, vector2, a_IntersectionFactorAB));
			this.m_ActiveProjector.DecalsMeshUpperVertexIndex++;
			if (this.m_DecalsMesh.Decals.CurrentTangentsMode == TangentsMode.Target)
			{
				this.m_DecalsMesh.Tangents.Add(Vector4.Lerp(this.m_DecalsMesh.Tangents[a_IndexA], this.m_DecalsMesh.Tangents[a_IndexB], a_IntersectionFactorAB));
			}
			if (this.m_DecalsMesh.Decals.UseVertexColors)
			{
				this.m_DecalsMesh.TargetVertexColors.Add(Color.Lerp(this.m_DecalsMesh.TargetVertexColors[a_IndexA], this.m_DecalsMesh.TargetVertexColors[a_IndexB], a_IntersectionFactorAB));
				this.m_DecalsMesh.VertexColors.Add(Color.Lerp(this.m_DecalsMesh.VertexColors[a_IndexA], this.m_DecalsMesh.VertexColors[a_IndexB], a_IntersectionFactorAB));
			}
			UVMode currentUVMode = this.m_DecalsMesh.Decals.CurrentUVMode;
			bool flag = currentUVMode - UVMode.TargetUV <= 1;
			if (flag)
			{
				this.m_DecalsMesh.UVs.Add(Vector2.Lerp(this.m_DecalsMesh.UVs[a_IndexA], this.m_DecalsMesh.UVs[a_IndexB], a_IntersectionFactorAB));
			}
			UV2Mode currentUV2Mode = this.m_DecalsMesh.Decals.CurrentUV2Mode;
			flag = currentUV2Mode - UV2Mode.TargetUV <= 1;
			if (flag)
			{
				this.m_DecalsMesh.UV2s.Add(Vector2.Lerp(this.m_DecalsMesh.UV2s[a_IndexA], this.m_DecalsMesh.UV2s[a_IndexB], a_IntersectionFactorAB));
			}
			if (a_IsVertexAInside)
			{
				cutEdge.newVertex2Index = count;
				this.m_RelativeVertexLocations[a_IndexB - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			else
			{
				cutEdge.newVertex1Index = count;
				this.m_RelativeVertexLocations[a_IndexA - a_RelativeVertexLocationsOffset] = RelativeVertexLocation.Outside;
			}
			this.m_CutEdges.AddEdge(cutEdge);
			return count;
		}
	}
}
