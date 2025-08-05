using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B17 RID: 11031
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public abstract class SkinnedDecals : GenericDecals<SkinnedDecals, SkinnedDecalProjectorBase, SkinnedDecalsMesh>
	{
		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x060098E0 RID: 39136 RVA: 0x0005B473 File Offset: 0x00059673
		// (set) Token: 0x060098E1 RID: 39137 RVA: 0x00150D58 File Offset: 0x0014EF58
		public override bool CastShadows
		{
			get
			{
				return this.SkinnedDecalsMeshRenderers[0].SkinnedMeshRenderer.castShadows;
			}
			set
			{
				SkinnedDecalsMeshRenderer[] skinnedDecalsMeshRenderers = this.SkinnedDecalsMeshRenderers;
				for (int i = 0; i < skinnedDecalsMeshRenderers.Length; i++)
				{
					skinnedDecalsMeshRenderers[i].SkinnedMeshRenderer.castShadows = value;
				}
			}
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x060098E2 RID: 39138 RVA: 0x0005B487 File Offset: 0x00059687
		// (set) Token: 0x060098E3 RID: 39139 RVA: 0x00150D88 File Offset: 0x0014EF88
		public override bool ReceiveShadows
		{
			get
			{
				return this.SkinnedDecalsMeshRenderers[0].SkinnedMeshRenderer.receiveShadows;
			}
			set
			{
				SkinnedDecalsMeshRenderer[] skinnedDecalsMeshRenderers = this.SkinnedDecalsMeshRenderers;
				for (int i = 0; i < skinnedDecalsMeshRenderers.Length; i++)
				{
					skinnedDecalsMeshRenderers[i].SkinnedMeshRenderer.receiveShadows = value;
				}
			}
		}

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x060098E4 RID: 39140 RVA: 0x0005B49B File Offset: 0x0005969B
		// (set) Token: 0x060098E5 RID: 39141 RVA: 0x00150DB8 File Offset: 0x0014EFB8
		public override bool UseLightProbes
		{
			get
			{
				return this.SkinnedDecalsMeshRenderers[0].SkinnedMeshRenderer.useLightProbes;
			}
			set
			{
				SkinnedDecalsMeshRenderer[] skinnedDecalsMeshRenderers = this.SkinnedDecalsMeshRenderers;
				for (int i = 0; i < skinnedDecalsMeshRenderers.Length; i++)
				{
					skinnedDecalsMeshRenderers[i].SkinnedMeshRenderer.useLightProbes = value;
				}
			}
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x060098E6 RID: 39142 RVA: 0x0005B4AF File Offset: 0x000596AF
		// (set) Token: 0x060098E7 RID: 39143 RVA: 0x00150DE8 File Offset: 0x0014EFE8
		public override Transform LightProbeAnchor
		{
			get
			{
				return this.SkinnedDecalsMeshRenderers[0].SkinnedMeshRenderer.probeAnchor;
			}
			set
			{
				SkinnedDecalsMeshRenderer[] skinnedDecalsMeshRenderers = this.SkinnedDecalsMeshRenderers;
				for (int i = 0; i < skinnedDecalsMeshRenderers.Length; i++)
				{
					skinnedDecalsMeshRenderers[i].SkinnedMeshRenderer.probeAnchor = value;
				}
			}
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x060098E8 RID: 39144 RVA: 0x0005B4C3 File Offset: 0x000596C3
		public SkinnedDecalsMeshRenderer[] SkinnedDecalsMeshRenderers
		{
			get
			{
				return this.m_SkinnedDecalsMeshRenderers.ToArray();
			}
		}

		// Token: 0x060098E9 RID: 39145 RVA: 0x0005B4D0 File Offset: 0x000596D0
		public override void OnEnable()
		{
			this.InitializeDecalsMeshRenderers();
			if (this.m_SkinnedDecalsMeshRenderers.Count == 0)
			{
				this.PushSkinnedDecalsMeshRenderer();
			}
		}

		// Token: 0x060098EA RID: 39146 RVA: 0x00150E18 File Offset: 0x0014F018
		public bool IsSkinnedDecalsMeshRenderer(SkinnedMeshRenderer a_SkinnedMeshRenderer)
		{
			bool flag = false;
			foreach (SkinnedDecalsMeshRenderer skinnedDecalsMeshRenderer in this.m_SkinnedDecalsMeshRenderers)
			{
				if (a_SkinnedMeshRenderer == skinnedDecalsMeshRenderer.SkinnedMeshRenderer)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x060098EB RID: 39147 RVA: 0x00150E7C File Offset: 0x0014F07C
		public override void ApplyMaterialToMeshRenderers()
		{
			base.ApplyMaterialToMeshRenderers();
			Material material = null;
			if (base.CurrentTextureAtlasType == TextureAtlasType.Builtin)
			{
				material = base.CurrentMaterial;
			}
			else if (base.CurrentTextureAtlasType == TextureAtlasType.Reference && base.CurrentTextureAtlasAsset != null)
			{
				material = base.CurrentTextureAtlasAsset.material;
			}
			foreach (SkinnedDecalsMeshRenderer skinnedDecalsMeshRenderer in this.m_SkinnedDecalsMeshRenderers)
			{
				if (Application.isPlaying)
				{
					skinnedDecalsMeshRenderer.SkinnedMeshRenderer.material = material;
				}
				else
				{
					skinnedDecalsMeshRenderer.SkinnedMeshRenderer.sharedMaterial = material;
				}
			}
		}

		// Token: 0x060098EC RID: 39148 RVA: 0x00150F28 File Offset: 0x0014F128
		public override void ApplyRenderersEditability()
		{
			base.ApplyRenderersEditability();
			HideFlags hideFlags = HideFlags.None;
			if (!base.AreRenderersEditable)
			{
				hideFlags = HideFlags.NotEditable;
			}
			foreach (SkinnedDecalsMeshRenderer skinnedDecalsMeshRenderer in this.m_SkinnedDecalsMeshRenderers)
			{
				skinnedDecalsMeshRenderer.gameObject.hideFlags = hideFlags;
			}
		}

		// Token: 0x060098ED RID: 39149 RVA: 0x00150F90 File Offset: 0x0014F190
		public override void InitializeDecalsMeshRenderers()
		{
			this.m_SkinnedDecalsMeshRenderers.Clear();
			Transform cachedTransform = base.CachedTransform;
			for (int i = 0; i < cachedTransform.childCount; i++)
			{
				SkinnedDecalsMeshRenderer component = cachedTransform.GetChild(i).GetComponent<SkinnedDecalsMeshRenderer>();
				if (component != null)
				{
					this.m_SkinnedDecalsMeshRenderers.Add(component);
				}
			}
		}

		// Token: 0x060098EE RID: 39150 RVA: 0x00150FE4 File Offset: 0x0014F1E4
		public override void UpdateDecalsMeshes(SkinnedDecalsMesh a_DecalsMesh)
		{
			base.UpdateDecalsMeshes(a_DecalsMesh);
			if (a_DecalsMesh.Vertices.Count <= 65535)
			{
				if (this.m_SkinnedDecalsMeshRenderers.Count == 0)
				{
					this.PushSkinnedDecalsMeshRenderer();
				}
				else if (this.m_SkinnedDecalsMeshRenderers.Count > 1)
				{
					while (this.m_SkinnedDecalsMeshRenderers.Count > 1)
					{
						this.PopSkinnedDecalsMeshRenderer();
					}
				}
				SkinnedDecalsMeshRenderer skinnedDecalsMeshRenderer = this.m_SkinnedDecalsMeshRenderers[0];
				this.ApplyToSkinnedDecalsMeshRenderer(skinnedDecalsMeshRenderer, a_DecalsMesh);
			}
			else
			{
				int num = 0;
				for (int i = 0; i < a_DecalsMesh.Projectors.Count; i++)
				{
					GenericDecalProjectorBase genericDecalProjectorBase = a_DecalsMesh.Projectors[i];
					GenericDecalProjectorBase genericDecalProjectorBase2 = a_DecalsMesh.Projectors[i];
					if (num >= this.m_SkinnedDecalsMeshRenderers.Count)
					{
						this.PushSkinnedDecalsMeshRenderer();
					}
					SkinnedDecalsMeshRenderer skinnedDecalsMeshRenderer2 = this.m_SkinnedDecalsMeshRenderers[num];
					int num2 = 0;
					int num3 = i;
					GenericDecalProjectorBase genericDecalProjectorBase3 = a_DecalsMesh.Projectors[i];
					while (i < a_DecalsMesh.Projectors.Count && num2 + genericDecalProjectorBase3.DecalsMeshVertexCount <= 65535)
					{
						genericDecalProjectorBase2 = genericDecalProjectorBase3;
						num2 += genericDecalProjectorBase3.DecalsMeshVertexCount;
						i++;
						if (i < a_DecalsMesh.Projectors.Count)
						{
							genericDecalProjectorBase3 = a_DecalsMesh.Projectors[i];
						}
					}
					if (num3 != i)
					{
						this.ApplyToSkinnedDecalsMeshRenderer(skinnedDecalsMeshRenderer2, a_DecalsMesh, genericDecalProjectorBase, genericDecalProjectorBase2);
						num++;
					}
				}
				while (num + 1 < this.m_SkinnedDecalsMeshRenderers.Count)
				{
					this.PopSkinnedDecalsMeshRenderer();
				}
			}
			base.SetDecalsMeshesAreNotOptimized();
		}

		// Token: 0x060098EF RID: 39151 RVA: 0x0005B4EB File Offset: 0x000596EB
		[Obsolete("UpdateSkinnedDecalsMeshes is deprecated, please use UpdateDecalsMeshes instead.")]
		public void UpdateSkinnedDecalsMeshes(SkinnedDecalsMesh a_SkinnedDecalsMesh)
		{
			this.UpdateDecalsMeshes(a_SkinnedDecalsMesh);
		}

		// Token: 0x060098F0 RID: 39152 RVA: 0x00151150 File Offset: 0x0014F350
		public override void UpdateVertexColors(SkinnedDecalsMesh a_DecalsMesh)
		{
			base.UpdateVertexColors(a_DecalsMesh);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_SkinnedDecalsMeshRenderers.Count; i++)
			{
				Mesh sharedMesh = this.m_SkinnedDecalsMeshRenderers[i].SkinnedMeshRenderer.sharedMesh;
				num2 = num2 + sharedMesh.vertexCount - 1;
				Color[] array = a_DecalsMesh.PreservedVertexColorArrays[i];
				SkinnedDecals.CopyListRangeToArray<Color>(ref array, a_DecalsMesh.VertexColors, num, num2);
				sharedMesh.colors = array;
				num = num2;
			}
		}

		// Token: 0x060098F1 RID: 39153 RVA: 0x001511C8 File Offset: 0x0014F3C8
		public override void UpdateProjectedUVs(SkinnedDecalsMesh a_DecalsMesh)
		{
			base.UpdateProjectedUVs(a_DecalsMesh);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_SkinnedDecalsMeshRenderers.Count; i++)
			{
				Mesh sharedMesh = this.m_SkinnedDecalsMeshRenderers[i].SkinnedMeshRenderer.sharedMesh;
				num2 = num2 + sharedMesh.vertexCount - 1;
				Vector2[] array = a_DecalsMesh.PreservedProjectedUVArrays[i];
				SkinnedDecals.CopyListRangeToArray<Vector2>(ref array, a_DecalsMesh.UVs, num, num2);
				sharedMesh.uv = array;
				num = num2;
			}
		}

		// Token: 0x060098F2 RID: 39154 RVA: 0x00151240 File Offset: 0x0014F440
		public override void UpdateProjectedUV2s(SkinnedDecalsMesh a_DecalsMesh)
		{
			base.UpdateProjectedUV2s(a_DecalsMesh);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_SkinnedDecalsMeshRenderers.Count; i++)
			{
				Mesh sharedMesh = this.m_SkinnedDecalsMeshRenderers[i].SkinnedMeshRenderer.sharedMesh;
				num2 = num2 + sharedMesh.vertexCount - 1;
				Vector2[] array = a_DecalsMesh.PreservedProjectedUV2Arrays[i];
				SkinnedDecals.CopyListRangeToArray<Vector2>(ref array, a_DecalsMesh.UV2s, num, num2);
				sharedMesh.uv2 = array;
				num = num2;
			}
		}

		// Token: 0x060098F3 RID: 39155 RVA: 0x001512B8 File Offset: 0x0014F4B8
		private void PushSkinnedDecalsMeshRenderer()
		{
			GameObject gameObject = new GameObject("Decals Mesh Renderer");
			Transform transform = gameObject.transform;
			transform.parent = base.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			SkinnedDecalsMeshRenderer skinnedDecalsMeshRenderer = this.AddSkinnedDecalsMeshRendererComponentToGameObject(gameObject);
			skinnedDecalsMeshRenderer.SkinnedMeshRenderer.material = base.CurrentMaterial;
			this.m_SkinnedDecalsMeshRenderers.Add(skinnedDecalsMeshRenderer);
		}

		// Token: 0x060098F4 RID: 39156 RVA: 0x00151328 File Offset: 0x0014F528
		private void PopSkinnedDecalsMeshRenderer()
		{
			SkinnedDecalsMeshRenderer skinnedDecalsMeshRenderer = this.m_SkinnedDecalsMeshRenderers[this.m_SkinnedDecalsMeshRenderers.Count - 1];
			if (Application.isPlaying)
			{
				global::UnityEngine.Object.Destroy(skinnedDecalsMeshRenderer.SkinnedMeshRenderer.sharedMesh);
				global::UnityEngine.Object.Destroy(skinnedDecalsMeshRenderer.gameObject);
			}
			this.m_SkinnedDecalsMeshRenderers.RemoveAt(this.m_SkinnedDecalsMeshRenderers.Count - 1);
		}

		// Token: 0x060098F5 RID: 39157 RVA: 0x00151388 File Offset: 0x0014F588
		private void ApplyToSkinnedDecalsMeshRenderer(SkinnedDecalsMeshRenderer a_SkinnedDecalsMeshRenderer, SkinnedDecalsMesh a_SkinnedDecalsMesh)
		{
			Mesh mesh = this.MeshOfSkinnedDecalsMeshRenderer(a_SkinnedDecalsMeshRenderer);
			mesh.Clear();
			if (!Edition.IsDX11)
			{
				mesh.MarkDynamic();
			}
			if (a_SkinnedDecalsMesh.OriginalVertices.Count == 0)
			{
				mesh.vertices = new Vector3[1];
				if (base.CurrentNormalsMode != NormalsMode.None)
				{
					mesh.normals = new Vector3[1];
				}
				if (base.CurrentTangentsMode != TangentsMode.None)
				{
					mesh.tangents = new Vector4[1];
				}
				if (base.UseVertexColors)
				{
					mesh.colors = new Color[1];
				}
				mesh.uv = new Vector2[1];
				if (base.CurrentUV2Mode != UV2Mode.None)
				{
					mesh.uv2 = new Vector2[1];
				}
				mesh.boneWeights = new BoneWeight[1];
				mesh.bindposes = new Matrix4x4[1];
				a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.bones = new Transform[] { a_SkinnedDecalsMeshRenderer.transform };
				a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.localBounds = mesh.bounds;
				a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.updateWhenOffscreen = false;
				return;
			}
			mesh.vertices = a_SkinnedDecalsMesh.OriginalVertices.ToArray();
			if (base.CurrentNormalsMode != NormalsMode.None)
			{
				mesh.normals = a_SkinnedDecalsMesh.Normals.ToArray();
			}
			if (base.CurrentTangentsMode != TangentsMode.None)
			{
				mesh.tangents = a_SkinnedDecalsMesh.Tangents.ToArray();
			}
			if (base.UseVertexColors)
			{
				Color[] array = a_SkinnedDecalsMesh.VertexColors.ToArray();
				if (a_SkinnedDecalsMesh.PreserveVertexColorArrays)
				{
					a_SkinnedDecalsMesh.PreservedVertexColorArrays.Add(array);
				}
				mesh.colors = array;
			}
			Vector2[] array2 = a_SkinnedDecalsMesh.UVs.ToArray();
			if (a_SkinnedDecalsMesh.PreserveProjectedUVArrays)
			{
				a_SkinnedDecalsMesh.PreservedProjectedUVArrays.Add(array2);
			}
			mesh.uv = array2;
			if (base.CurrentUV2Mode != UV2Mode.None)
			{
				Vector2[] array3 = a_SkinnedDecalsMesh.UV2s.ToArray();
				if (a_SkinnedDecalsMesh.PreserveProjectedUV2Arrays)
				{
					a_SkinnedDecalsMesh.PreservedProjectedUV2Arrays.Add(array3);
				}
				mesh.uv2 = array3;
			}
			mesh.boneWeights = a_SkinnedDecalsMesh.BoneWeights.ToArray();
			mesh.triangles = a_SkinnedDecalsMesh.Triangles.ToArray();
			mesh.bindposes = a_SkinnedDecalsMesh.BindPoses.ToArray();
			a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.bones = a_SkinnedDecalsMesh.Bones.ToArray();
			a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.localBounds = mesh.bounds;
			a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.updateWhenOffscreen = true;
		}

		// Token: 0x060098F6 RID: 39158 RVA: 0x001515A8 File Offset: 0x0014F7A8
		private void ApplyToSkinnedDecalsMeshRenderer(SkinnedDecalsMeshRenderer a_SkinnedDecalsMeshRenderer, SkinnedDecalsMesh a_SkinnedDecalsMesh, GenericDecalProjectorBase a_FirstProjector, GenericDecalProjectorBase a_LastProjector)
		{
			int decalsMeshLowerVertexIndex = a_FirstProjector.DecalsMeshLowerVertexIndex;
			int decalsMeshUpperVertexIndex = a_LastProjector.DecalsMeshUpperVertexIndex;
			int decalsMeshLowerTriangleIndex = a_FirstProjector.DecalsMeshLowerTriangleIndex;
			int decalsMeshUpperTriangleIndex = a_LastProjector.DecalsMeshUpperTriangleIndex;
			Mesh mesh = this.MeshOfSkinnedDecalsMeshRenderer(a_SkinnedDecalsMeshRenderer);
			mesh.Clear();
			if (!Edition.IsDX11)
			{
				mesh.MarkDynamic();
			}
			Vector3[] array = new Vector3[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
			SkinnedDecals.CopyListRangeToArray<Vector3>(ref array, a_SkinnedDecalsMesh.OriginalVertices, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			mesh.vertices = array;
			BoneWeight[] array2 = new BoneWeight[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
			SkinnedDecals.CopyListRangeToArray<BoneWeight>(ref array2, a_SkinnedDecalsMesh.BoneWeights, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			mesh.boneWeights = array2;
			int[] array3 = new int[decalsMeshUpperTriangleIndex - decalsMeshLowerTriangleIndex + 1];
			SkinnedDecals.CopyListRangeToArray<int>(ref array3, a_SkinnedDecalsMesh.Triangles, decalsMeshLowerTriangleIndex, decalsMeshUpperTriangleIndex);
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] -= decalsMeshLowerVertexIndex;
			}
			mesh.triangles = array3;
			Vector2[] array4 = new Vector2[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
			SkinnedDecals.CopyListRangeToArray<Vector2>(ref array4, a_SkinnedDecalsMesh.UVs, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			if (a_SkinnedDecalsMesh.PreserveProjectedUVArrays)
			{
				a_SkinnedDecalsMesh.PreservedProjectedUVArrays.Add(array4);
			}
			mesh.uv = array4;
			if (base.CurrentUV2Mode != UV2Mode.None && base.CurrentUV2Mode != UV2Mode.Lightmapping)
			{
				Vector2[] array5 = new Vector2[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
				SkinnedDecals.CopyListRangeToArray<Vector2>(ref array5, a_SkinnedDecalsMesh.UV2s, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
				if (a_SkinnedDecalsMesh.PreserveProjectedUV2Arrays)
				{
					a_SkinnedDecalsMesh.PreservedProjectedUV2Arrays.Add(array5);
				}
				mesh.uv2 = array5;
			}
			else
			{
				mesh.uv2 = null;
			}
			if (base.CurrentNormalsMode != NormalsMode.None)
			{
				Vector3[] array6 = new Vector3[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
				SkinnedDecals.CopyListRangeToArray<Vector3>(ref array6, a_SkinnedDecalsMesh.Normals, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
				mesh.normals = array6;
			}
			else
			{
				mesh.normals = null;
			}
			if (base.CurrentTangentsMode != TangentsMode.None)
			{
				Vector4[] array7 = new Vector4[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
				SkinnedDecals.CopyListRangeToArray<Vector4>(ref array7, a_SkinnedDecalsMesh.Tangents, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
				mesh.tangents = array7;
			}
			else
			{
				mesh.tangents = null;
			}
			if (base.UseVertexColors)
			{
				Color[] array8 = new Color[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
				SkinnedDecals.CopyListRangeToArray<Color>(ref array8, a_SkinnedDecalsMesh.VertexColors, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
				if (a_SkinnedDecalsMesh.PreserveVertexColorArrays)
				{
					a_SkinnedDecalsMesh.PreservedVertexColorArrays.Add(array8);
				}
				mesh.colors = array8;
			}
			else
			{
				mesh.colors = null;
			}
			Matrix4x4[] array9 = a_SkinnedDecalsMesh.BindPoses.ToArray();
			mesh.bindposes = array9;
			Transform[] array10 = a_SkinnedDecalsMesh.Bones.ToArray();
			a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.bones = array10;
			a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.localBounds = mesh.bounds;
			a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.updateWhenOffscreen = true;
		}

		// Token: 0x060098F7 RID: 39159 RVA: 0x00149A94 File Offset: 0x00147C94
		private static void CopyListRangeToArray<[Nullable(2)] T>(ref T[] a_Array, List<T> a_List, int a_LowerListIndex, int a_UpperListIndex)
		{
			int num = 0;
			for (int i = a_LowerListIndex; i <= a_UpperListIndex; i++)
			{
				a_Array[num] = a_List[i];
				num++;
			}
		}

		// Token: 0x060098F8 RID: 39160 RVA: 0x00151810 File Offset: 0x0014FA10
		private Mesh MeshOfSkinnedDecalsMeshRenderer(SkinnedDecalsMeshRenderer a_SkinnedDecalsMeshRenderer)
		{
			Mesh mesh;
			if (a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.sharedMesh == null)
			{
				mesh = new Mesh
				{
					name = "Skinned Decal Mesh"
				};
				a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.sharedMesh = mesh;
			}
			else
			{
				mesh = a_SkinnedDecalsMeshRenderer.SkinnedMeshRenderer.sharedMesh;
				mesh.Clear();
			}
			return mesh;
		}

		// Token: 0x060098F9 RID: 39161 RVA: 0x00151864 File Offset: 0x0014FA64
		public override void OptimizeDecalsMeshes()
		{
			base.OptimizeDecalsMeshes();
			foreach (SkinnedDecalsMeshRenderer skinnedDecalsMeshRenderer in this.m_SkinnedDecalsMeshRenderers)
			{
				if (skinnedDecalsMeshRenderer.SkinnedMeshRenderer != null)
				{
					skinnedDecalsMeshRenderer.SkinnedMeshRenderer.sharedMesh != null;
				}
			}
		}

		// Token: 0x060098FA RID: 39162
		protected abstract SkinnedDecalsMeshRenderer AddSkinnedDecalsMeshRendererComponentToGameObject(GameObject a_GameObject);

		// Token: 0x0400644C RID: 25676
		private readonly List<SkinnedDecalsMeshRenderer> m_SkinnedDecalsMeshRenderers = new List<SkinnedDecalsMeshRenderer>();
	}
}
