using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AF3 RID: 10995
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public abstract class Decals : GenericDecals<Decals, DecalProjectorBase, DecalsMesh>
	{
		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060097A0 RID: 38816 RVA: 0x0005A942 File Offset: 0x00058B42
		// (set) Token: 0x060097A1 RID: 38817 RVA: 0x0005A94A File Offset: 0x00058B4A
		public MeshMinimizerMode CurrentMeshMinimizerMode
		{
			get
			{
				return this.m_MeshMinimizerMode;
			}
			set
			{
				this.m_MeshMinimizerMode = value;
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060097A2 RID: 38818 RVA: 0x0005A953 File Offset: 0x00058B53
		// (set) Token: 0x060097A3 RID: 38819 RVA: 0x0005A95B File Offset: 0x00058B5B
		public float MeshMinimizerMaximumAbsoluteError
		{
			get
			{
				return this.m_MeshMinimizerMaximumAbsoluteError;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("The maximum absolute error has to be greater than zero.");
				}
				this.m_MeshMinimizerMaximumAbsoluteError = value;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060097A4 RID: 38820 RVA: 0x0005A977 File Offset: 0x00058B77
		// (set) Token: 0x060097A5 RID: 38821 RVA: 0x001490DC File Offset: 0x001472DC
		public float MeshMinimizerMaximumRelativeError
		{
			get
			{
				return this.m_MeshMinimizerMaximumRelativeError;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("The maximum relative error has to be within [0.0f, 1.0f].");
				}
				this.m_MeshMinimizerMaximumRelativeError = value;
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x060097A6 RID: 38822 RVA: 0x0005A97F File Offset: 0x00058B7F
		// (set) Token: 0x060097A7 RID: 38823 RVA: 0x00149114 File Offset: 0x00147314
		public override bool CastShadows
		{
			get
			{
				return this.DecalsMeshRenderers[0].MeshRenderer.castShadows;
			}
			set
			{
				DecalsMeshRenderer[] decalsMeshRenderers = this.DecalsMeshRenderers;
				for (int i = 0; i < decalsMeshRenderers.Length; i++)
				{
					decalsMeshRenderers[i].MeshRenderer.castShadows = value;
				}
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x060097A8 RID: 38824 RVA: 0x0005A993 File Offset: 0x00058B93
		// (set) Token: 0x060097A9 RID: 38825 RVA: 0x00149144 File Offset: 0x00147344
		public override bool ReceiveShadows
		{
			get
			{
				return this.DecalsMeshRenderers[0].MeshRenderer.receiveShadows;
			}
			set
			{
				DecalsMeshRenderer[] decalsMeshRenderers = this.DecalsMeshRenderers;
				for (int i = 0; i < decalsMeshRenderers.Length; i++)
				{
					decalsMeshRenderers[i].MeshRenderer.receiveShadows = value;
				}
			}
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060097AA RID: 38826 RVA: 0x0005A9A7 File Offset: 0x00058BA7
		// (set) Token: 0x060097AB RID: 38827 RVA: 0x00149174 File Offset: 0x00147374
		public override bool UseLightProbes
		{
			get
			{
				return this.DecalsMeshRenderers[0].MeshRenderer.useLightProbes;
			}
			set
			{
				DecalsMeshRenderer[] decalsMeshRenderers = this.DecalsMeshRenderers;
				for (int i = 0; i < decalsMeshRenderers.Length; i++)
				{
					decalsMeshRenderers[i].MeshRenderer.useLightProbes = value;
				}
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x060097AC RID: 38828 RVA: 0x0005A9BB File Offset: 0x00058BBB
		// (set) Token: 0x060097AD RID: 38829 RVA: 0x001491A4 File Offset: 0x001473A4
		public override Transform LightProbeAnchor
		{
			get
			{
				return this.DecalsMeshRenderers[0].MeshRenderer.probeAnchor;
			}
			set
			{
				DecalsMeshRenderer[] decalsMeshRenderers = this.DecalsMeshRenderers;
				for (int i = 0; i < decalsMeshRenderers.Length; i++)
				{
					decalsMeshRenderers[i].MeshRenderer.probeAnchor = value;
				}
			}
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x060097AE RID: 38830 RVA: 0x0005A9CF File Offset: 0x00058BCF
		public DecalsMeshRenderer[] DecalsMeshRenderers
		{
			get
			{
				return this.m_DecalsMeshRenderers.ToArray();
			}
		}

		// Token: 0x060097AF RID: 38831 RVA: 0x0005A9DC File Offset: 0x00058BDC
		public override void OnEnable()
		{
			this.InitializeDecalsMeshRenderers();
			if (this.m_DecalsMeshRenderers.Count == 0)
			{
				this.PushDecalsMeshRenderer();
			}
		}

		// Token: 0x060097B0 RID: 38832 RVA: 0x001491D4 File Offset: 0x001473D4
		public bool IsDecalsMeshRenderer(MeshRenderer a_MeshRenderer)
		{
			bool flag = false;
			foreach (DecalsMeshRenderer decalsMeshRenderer in this.m_DecalsMeshRenderers)
			{
				if (a_MeshRenderer == decalsMeshRenderer.MeshRenderer)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x060097B1 RID: 38833 RVA: 0x00149238 File Offset: 0x00147438
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
			foreach (DecalsMeshRenderer decalsMeshRenderer in this.m_DecalsMeshRenderers)
			{
				decalsMeshRenderer.MeshRenderer.material = material;
			}
		}

		// Token: 0x060097B2 RID: 38834 RVA: 0x001492CC File Offset: 0x001474CC
		public override void ApplyRenderersEditability()
		{
			base.ApplyRenderersEditability();
			HideFlags hideFlags = HideFlags.None;
			if (!base.AreRenderersEditable)
			{
				hideFlags = HideFlags.NotEditable;
			}
			foreach (DecalsMeshRenderer decalsMeshRenderer in this.m_DecalsMeshRenderers)
			{
				decalsMeshRenderer.gameObject.hideFlags = hideFlags;
			}
		}

		// Token: 0x060097B3 RID: 38835 RVA: 0x00149334 File Offset: 0x00147534
		public override void InitializeDecalsMeshRenderers()
		{
			this.m_DecalsMeshRenderers.Clear();
			Transform cachedTransform = base.CachedTransform;
			for (int i = 0; i < cachedTransform.childCount; i++)
			{
				DecalsMeshRenderer component = cachedTransform.GetChild(i).GetComponent<DecalsMeshRenderer>();
				if (component != null)
				{
					this.m_DecalsMeshRenderers.Add(component);
				}
			}
		}

		// Token: 0x060097B4 RID: 38836 RVA: 0x00149388 File Offset: 0x00147588
		public override void UpdateDecalsMeshes(DecalsMesh a_DecalsMesh)
		{
			base.UpdateDecalsMeshes(a_DecalsMesh);
			if (a_DecalsMesh.Vertices.Count <= 65535)
			{
				if (this.m_DecalsMeshRenderers.Count == 0)
				{
					this.PushDecalsMeshRenderer();
				}
				else if (this.m_DecalsMeshRenderers.Count > 1)
				{
					while (this.m_DecalsMeshRenderers.Count > 1)
					{
						this.PopDecalsMeshRenderer();
					}
				}
				DecalsMeshRenderer decalsMeshRenderer = this.m_DecalsMeshRenderers[0];
				this.ApplyToDecalsMeshRenderer(decalsMeshRenderer, a_DecalsMesh);
			}
			else
			{
				int num = 0;
				for (int i = 0; i < a_DecalsMesh.Projectors.Count; i++)
				{
					GenericDecalProjectorBase genericDecalProjectorBase = a_DecalsMesh.Projectors[i];
					GenericDecalProjectorBase genericDecalProjectorBase2 = a_DecalsMesh.Projectors[i];
					if (num >= this.m_DecalsMeshRenderers.Count)
					{
						this.PushDecalsMeshRenderer();
					}
					DecalsMeshRenderer decalsMeshRenderer2 = this.m_DecalsMeshRenderers[num];
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
						this.ApplyToDecalsMeshRenderer(decalsMeshRenderer2, a_DecalsMesh, genericDecalProjectorBase, genericDecalProjectorBase2);
						num++;
					}
				}
				while (num + 1 < this.m_DecalsMeshRenderers.Count)
				{
					this.PopDecalsMeshRenderer();
				}
			}
			base.SetDecalsMeshesAreNotOptimized();
		}

		// Token: 0x060097B5 RID: 38837 RVA: 0x001494F4 File Offset: 0x001476F4
		public override void UpdateVertexColors(DecalsMesh a_DecalsMesh)
		{
			base.UpdateVertexColors(a_DecalsMesh);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_DecalsMeshRenderers.Count; i++)
			{
				DecalsMeshRenderer decalsMeshRenderer = this.m_DecalsMeshRenderers[i];
				Mesh mesh = ((!Application.isPlaying) ? decalsMeshRenderer.MeshFilter.sharedMesh : decalsMeshRenderer.MeshFilter.mesh);
				num2 = num2 + mesh.vertexCount - 1;
				Color[] array = a_DecalsMesh.PreservedVertexColorArrays[i];
				Decals.CopyListRangeToArray<Color>(ref array, a_DecalsMesh.VertexColors, num, num2);
				mesh.colors = array;
				num = num2;
			}
		}

		// Token: 0x060097B6 RID: 38838 RVA: 0x00149584 File Offset: 0x00147784
		public override void UpdateProjectedUVs(DecalsMesh a_DecalsMesh)
		{
			base.UpdateProjectedUVs(a_DecalsMesh);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_DecalsMeshRenderers.Count; i++)
			{
				DecalsMeshRenderer decalsMeshRenderer = this.m_DecalsMeshRenderers[i];
				Mesh mesh = ((!Application.isPlaying) ? decalsMeshRenderer.MeshFilter.sharedMesh : decalsMeshRenderer.MeshFilter.mesh);
				num2 = num2 + mesh.vertexCount - 1;
				Vector2[] array = a_DecalsMesh.PreservedProjectedUVArrays[i];
				Decals.CopyListRangeToArray<Vector2>(ref array, a_DecalsMesh.UVs, num, num2);
				mesh.uv = array;
				num = num2;
			}
		}

		// Token: 0x060097B7 RID: 38839 RVA: 0x00149614 File Offset: 0x00147814
		public override void UpdateProjectedUV2s(DecalsMesh a_DecalsMesh)
		{
			base.UpdateProjectedUV2s(a_DecalsMesh);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_DecalsMeshRenderers.Count; i++)
			{
				DecalsMeshRenderer decalsMeshRenderer = this.m_DecalsMeshRenderers[i];
				Mesh mesh = ((!Application.isPlaying) ? decalsMeshRenderer.MeshFilter.sharedMesh : decalsMeshRenderer.MeshFilter.mesh);
				num2 = num2 + mesh.vertexCount - 1;
				Vector2[] array = a_DecalsMesh.PreservedProjectedUV2Arrays[i];
				Decals.CopyListRangeToArray<Vector2>(ref array, a_DecalsMesh.UV2s, num, num2);
				mesh.uv2 = array;
				num = num2;
			}
		}

		// Token: 0x060097B8 RID: 38840 RVA: 0x001496A4 File Offset: 0x001478A4
		private void PushDecalsMeshRenderer()
		{
			GameObject gameObject = new GameObject("Decals Mesh Renderer");
			Transform transform = gameObject.transform;
			transform.parent = base.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			DecalsMeshRenderer decalsMeshRenderer = this.AddDecalsMeshRendererComponentToGameObject(gameObject);
			decalsMeshRenderer.MeshRenderer.material = base.CurrentMaterial;
			this.m_DecalsMeshRenderers.Add(decalsMeshRenderer);
		}

		// Token: 0x060097B9 RID: 38841 RVA: 0x00149714 File Offset: 0x00147914
		private void PopDecalsMeshRenderer()
		{
			DecalsMeshRenderer decalsMeshRenderer = this.m_DecalsMeshRenderers[this.m_DecalsMeshRenderers.Count - 1];
			if (Application.isPlaying)
			{
				global::UnityEngine.Object.Destroy(decalsMeshRenderer.MeshFilter.mesh);
				global::UnityEngine.Object.Destroy(decalsMeshRenderer.gameObject);
			}
			this.m_DecalsMeshRenderers.RemoveAt(this.m_DecalsMeshRenderers.Count - 1);
		}

		// Token: 0x060097BA RID: 38842 RVA: 0x00149774 File Offset: 0x00147974
		private void ApplyToDecalsMeshRenderer(DecalsMeshRenderer a_DecalsMeshRenderer, DecalsMesh a_DecalsMesh)
		{
			Mesh mesh = this.MeshOfDecalsMeshRenderer(a_DecalsMeshRenderer);
			mesh.Clear();
			if (!Edition.IsDX11)
			{
				mesh.MarkDynamic();
			}
			mesh.vertices = a_DecalsMesh.Vertices.ToArray();
			if (base.CurrentNormalsMode != NormalsMode.None)
			{
				mesh.normals = a_DecalsMesh.Normals.ToArray();
			}
			else
			{
				mesh.normals = null;
			}
			if (base.CurrentTangentsMode != TangentsMode.None)
			{
				mesh.tangents = a_DecalsMesh.Tangents.ToArray();
			}
			else
			{
				mesh.tangents = null;
			}
			if (base.UseVertexColors)
			{
				Color[] array = a_DecalsMesh.VertexColors.ToArray();
				if (a_DecalsMesh.PreserveVertexColorArrays)
				{
					a_DecalsMesh.PreservedVertexColorArrays.Add(array);
				}
				mesh.colors = array;
			}
			else
			{
				mesh.colors = null;
			}
			Vector2[] array2 = a_DecalsMesh.UVs.ToArray();
			if (a_DecalsMesh.PreserveProjectedUVArrays)
			{
				a_DecalsMesh.PreservedProjectedUVArrays.Add(array2);
			}
			mesh.uv = array2;
			if (base.CurrentUV2Mode != UV2Mode.None)
			{
				Vector2[] array3 = a_DecalsMesh.UV2s.ToArray();
				if (a_DecalsMesh.PreserveProjectedUV2Arrays)
				{
					a_DecalsMesh.PreservedProjectedUV2Arrays.Add(array3);
				}
				mesh.uv2 = array3;
			}
			else
			{
				mesh.uv2 = null;
			}
			mesh.triangles = a_DecalsMesh.Triangles.ToArray();
		}

		// Token: 0x060097BB RID: 38843 RVA: 0x0014989C File Offset: 0x00147A9C
		private void ApplyToDecalsMeshRenderer(DecalsMeshRenderer a_DecalsMeshRenderer, DecalsMesh a_DecalsMesh, GenericDecalProjectorBase a_FirstProjector, GenericDecalProjectorBase a_LastProjector)
		{
			int decalsMeshLowerVertexIndex = a_FirstProjector.DecalsMeshLowerVertexIndex;
			int decalsMeshUpperVertexIndex = a_LastProjector.DecalsMeshUpperVertexIndex;
			int decalsMeshLowerTriangleIndex = a_FirstProjector.DecalsMeshLowerTriangleIndex;
			int decalsMeshUpperTriangleIndex = a_LastProjector.DecalsMeshUpperTriangleIndex;
			Mesh mesh = this.MeshOfDecalsMeshRenderer(a_DecalsMeshRenderer);
			mesh.Clear();
			if (!Edition.IsDX11)
			{
				mesh.MarkDynamic();
			}
			Vector3[] array = new Vector3[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
			Decals.CopyListRangeToArray<Vector3>(ref array, a_DecalsMesh.Vertices, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			mesh.vertices = array;
			int[] array2 = new int[decalsMeshUpperTriangleIndex - decalsMeshLowerTriangleIndex + 1];
			Decals.CopyListRangeToArray<int>(ref array2, a_DecalsMesh.Triangles, decalsMeshLowerTriangleIndex, decalsMeshUpperTriangleIndex);
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] -= decalsMeshLowerVertexIndex;
			}
			mesh.triangles = array2;
			Vector2[] array3 = new Vector2[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
			Decals.CopyListRangeToArray<Vector2>(ref array3, a_DecalsMesh.UVs, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
			if (a_DecalsMesh.PreserveProjectedUVArrays)
			{
				a_DecalsMesh.PreservedProjectedUVArrays.Add(array3);
			}
			mesh.uv = array3;
			if (base.CurrentUV2Mode != UV2Mode.None && base.CurrentUV2Mode != UV2Mode.Lightmapping)
			{
				Vector2[] array4 = new Vector2[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
				Decals.CopyListRangeToArray<Vector2>(ref array4, a_DecalsMesh.UV2s, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
				if (a_DecalsMesh.PreserveProjectedUV2Arrays)
				{
					a_DecalsMesh.PreservedProjectedUV2Arrays.Add(array4);
				}
				mesh.uv2 = array4;
			}
			else
			{
				mesh.uv2 = null;
			}
			if (base.CurrentNormalsMode != NormalsMode.None)
			{
				Vector3[] array5 = new Vector3[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
				Decals.CopyListRangeToArray<Vector3>(ref array5, a_DecalsMesh.Normals, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
				mesh.normals = array5;
			}
			else
			{
				mesh.normals = null;
			}
			if (base.CurrentTangentsMode != TangentsMode.None)
			{
				Vector4[] array6 = new Vector4[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
				Decals.CopyListRangeToArray<Vector4>(ref array6, a_DecalsMesh.Tangents, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
				mesh.tangents = array6;
			}
			else
			{
				mesh.tangents = null;
			}
			if (base.UseVertexColors)
			{
				Color[] array7 = new Color[decalsMeshUpperVertexIndex - decalsMeshLowerVertexIndex + 1];
				Decals.CopyListRangeToArray<Color>(ref array7, a_DecalsMesh.VertexColors, decalsMeshLowerVertexIndex, decalsMeshUpperVertexIndex);
				if (a_DecalsMesh.PreserveVertexColorArrays)
				{
					a_DecalsMesh.PreservedVertexColorArrays.Add(array7);
				}
				mesh.colors = array7;
				return;
			}
			mesh.colors = null;
		}

		// Token: 0x060097BC RID: 38844 RVA: 0x00149A94 File Offset: 0x00147C94
		private static void CopyListRangeToArray<[Nullable(2)] T>(ref T[] a_Array, List<T> a_List, int a_LowerListIndex, int a_UpperListIndex)
		{
			int num = 0;
			for (int i = a_LowerListIndex; i <= a_UpperListIndex; i++)
			{
				a_Array[num] = a_List[i];
				num++;
			}
		}

		// Token: 0x060097BD RID: 38845 RVA: 0x00149AC4 File Offset: 0x00147CC4
		private Mesh MeshOfDecalsMeshRenderer(DecalsMeshRenderer a_DecalsMeshRenderer)
		{
			Mesh mesh;
			if (Application.isPlaying)
			{
				if (a_DecalsMeshRenderer.MeshFilter.mesh == null)
				{
					mesh = new Mesh
					{
						name = "Decal Mesh"
					};
					a_DecalsMeshRenderer.MeshFilter.mesh = mesh;
				}
				else
				{
					mesh = a_DecalsMeshRenderer.MeshFilter.mesh;
					mesh.Clear();
				}
			}
			else if (a_DecalsMeshRenderer.MeshFilter.sharedMesh == null)
			{
				mesh = new Mesh
				{
					name = "Decal Mesh"
				};
				a_DecalsMeshRenderer.MeshFilter.sharedMesh = mesh;
			}
			else
			{
				mesh = a_DecalsMeshRenderer.MeshFilter.sharedMesh;
				mesh.Clear();
			}
			return mesh;
		}

		// Token: 0x060097BE RID: 38846 RVA: 0x00149B64 File Offset: 0x00147D64
		public override void OptimizeDecalsMeshes()
		{
			base.OptimizeDecalsMeshes();
			foreach (DecalsMeshRenderer decalsMeshRenderer in this.m_DecalsMeshRenderers)
			{
				if (Application.isPlaying)
				{
					if (decalsMeshRenderer.MeshFilter != null && !(decalsMeshRenderer.MeshFilter.mesh != null))
					{
					}
				}
				else if (decalsMeshRenderer.MeshFilter != null)
				{
					decalsMeshRenderer.MeshFilter.sharedMesh != null;
				}
			}
		}

		// Token: 0x060097BF RID: 38847
		protected abstract DecalsMeshRenderer AddDecalsMeshRendererComponentToGameObject(GameObject a_GameObject);

		// Token: 0x040063BB RID: 25531
		[SerializeField]
		private MeshMinimizerMode m_MeshMinimizerMode;

		// Token: 0x040063BC RID: 25532
		[SerializeField]
		private float m_MeshMinimizerMaximumAbsoluteError = 0.0001f;

		// Token: 0x040063BD RID: 25533
		[SerializeField]
		private float m_MeshMinimizerMaximumRelativeError = 0.0001f;

		// Token: 0x040063BE RID: 25534
		private readonly List<DecalsMeshRenderer> m_DecalsMeshRenderers = new List<DecalsMeshRenderer>();
	}
}
