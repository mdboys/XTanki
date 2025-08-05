using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B19 RID: 11033
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public class SkinnedDecalsMeshCutter : GenericDecalsMeshCutter<SkinnedDecals, SkinnedDecalProjectorBase, SkinnedDecalsMesh>
	{
		// Token: 0x06009912 RID: 39186 RVA: 0x00152E58 File Offset: 0x00151058
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
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeUnoptimized);
				return;
			}
			if (!flag && !flag3 && flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeUnoptimized);
				return;
			}
			if (!flag && flag3 && !flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeUnoptimized);
				return;
			}
			if (!flag && flag3 && flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeUnoptimized);
				return;
			}
			if (flag && !flag3 && !flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeUnoptimized);
				return;
			}
			if (flag && !flag3 && flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeUnoptimized);
				return;
			}
			if (flag && flag3 && !flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeUnoptimized);
				return;
			}
			if (flag && flag3 && flag4)
			{
				this.m_CreateCutEdgeDelegate = new CutEdgeDelegate(this.CutEdgeUnoptimized);
			}
		}

		// Token: 0x06009913 RID: 39187 RVA: 0x00152FBC File Offset: 0x001511BC
		private int CutEdgeUnoptimized(int a_RelativeVertexLocationsOffset, int a_IndexA, int a_IndexB, Vector3 a_VertexA, Vector3 a_VertexB, bool a_IsVertexAInside, float a_IntersectionFactorAB)
		{
			CutEdge cutEdge = new CutEdge(a_IndexA, a_IndexB);
			Vector3 vector = this.m_DecalsMesh.OriginalVertices[a_IndexA];
			Vector3 vector2 = this.m_DecalsMesh.OriginalVertices[a_IndexB];
			Vector3 vector3 = this.m_DecalsMesh.Normals[a_IndexA];
			Vector3 vector4 = this.m_DecalsMesh.Normals[a_IndexB];
			BoneWeight boneWeight = this.m_DecalsMesh.BoneWeights[a_IndexA];
			BoneWeight boneWeight2 = this.m_DecalsMesh.BoneWeights[a_IndexB];
			int count = this.m_DecalsMesh.Vertices.Count;
			this.m_DecalsMesh.OriginalVertices.Add(Vector3.Lerp(vector, vector2, a_IntersectionFactorAB));
			this.m_DecalsMesh.Vertices.Add(Vector3.Lerp(a_VertexA, a_VertexB, a_IntersectionFactorAB));
			this.m_DecalsMesh.Normals.Add(Vector3.Lerp(vector3, vector4, a_IntersectionFactorAB));
			this.m_DecalsMesh.BoneWeights.Add(this.LerpBoneWeights(boneWeight, boneWeight2, a_IntersectionFactorAB));
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

		// Token: 0x06009914 RID: 39188 RVA: 0x001532A0 File Offset: 0x001514A0
		private BoneWeight LerpBoneWeights(BoneWeight a_BoneWeight1, BoneWeight a_BoneWeight2, float a_Factor)
		{
			BoneWeightElement boneWeightElement = default(BoneWeightElement);
			float num = 1f - a_Factor;
			boneWeightElement.index = a_BoneWeight1.boneIndex0;
			boneWeightElement.weight = a_BoneWeight1.weight0 * num;
			this.m_BoneWeightElements[0] = boneWeightElement;
			boneWeightElement.index = a_BoneWeight1.boneIndex1;
			boneWeightElement.weight = a_BoneWeight1.weight1 * num;
			this.m_BoneWeightElements[1] = boneWeightElement;
			boneWeightElement.index = a_BoneWeight1.boneIndex2;
			boneWeightElement.weight = a_BoneWeight1.weight2 * num;
			this.m_BoneWeightElements[2] = boneWeightElement;
			boneWeightElement.index = a_BoneWeight1.boneIndex3;
			boneWeightElement.weight = a_BoneWeight1.weight3 * num;
			this.m_BoneWeightElements[3] = boneWeightElement;
			boneWeightElement.index = a_BoneWeight2.boneIndex0;
			boneWeightElement.weight = a_BoneWeight2.weight0 * a_Factor;
			this.m_BoneWeightElements[4] = boneWeightElement;
			boneWeightElement.index = a_BoneWeight2.boneIndex1;
			boneWeightElement.weight = a_BoneWeight2.weight1 * a_Factor;
			this.m_BoneWeightElements[5] = boneWeightElement;
			boneWeightElement.index = a_BoneWeight2.boneIndex2;
			boneWeightElement.weight = a_BoneWeight2.weight2 * a_Factor;
			this.m_BoneWeightElements[6] = boneWeightElement;
			boneWeightElement.index = a_BoneWeight2.boneIndex3;
			boneWeightElement.weight = a_BoneWeight2.weight3 * a_Factor;
			this.m_BoneWeightElements[7] = boneWeightElement;
			for (int i = 0; i < 4; i++)
			{
				int index = this.m_BoneWeightElements[i].index;
				for (int j = 4; j < 8; j++)
				{
					int index2 = this.m_BoneWeightElements[j].index;
					if (index == index2)
					{
						BoneWeightElement[] boneWeightElements = this.m_BoneWeightElements;
						int num2 = i;
						boneWeightElements[num2].weight = boneWeightElements[num2].weight + this.m_BoneWeightElements[j].weight;
						this.m_BoneWeightElements[j].weight = 0f;
						this.m_BoneWeightElements[j].index = 0;
					}
				}
			}
			Array.Sort<BoneWeightElement>(this.m_BoneWeightElements);
			float num3 = 1f / (this.m_BoneWeightElements[0].weight + this.m_BoneWeightElements[1].weight + this.m_BoneWeightElements[2].weight + this.m_BoneWeightElements[3].weight);
			return new BoneWeight
			{
				boneIndex0 = this.m_BoneWeightElements[0].index,
				weight0 = this.m_BoneWeightElements[0].weight * num3,
				boneIndex1 = this.m_BoneWeightElements[1].index,
				weight1 = this.m_BoneWeightElements[1].weight * num3,
				boneIndex2 = this.m_BoneWeightElements[2].index,
				weight2 = this.m_BoneWeightElements[2].weight * num3,
				boneIndex3 = this.m_BoneWeightElements[3].index,
				weight3 = this.m_BoneWeightElements[3].weight * num3
			};
		}

		// Token: 0x04006451 RID: 25681
		private readonly BoneWeightElement[] m_BoneWeightElements = new BoneWeightElement[8];
	}
}
