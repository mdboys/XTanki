using System;
using System.Runtime.CompilerServices;
using Edelweiss.TextureAtlas;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AFF RID: 11007
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GenericDecalsBase : MonoBehaviour
	{
		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x0600981A RID: 38938 RVA: 0x0005AC7A File Offset: 0x00058E7A
		public Transform CachedTransform
		{
			get
			{
				if (this.m_CachedTransform == null)
				{
					this.m_CachedTransform = base.transform;
				}
				return this.m_CachedTransform;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x0600981B RID: 38939 RVA: 0x0005AC9C File Offset: 0x00058E9C
		// (set) Token: 0x0600981C RID: 38940 RVA: 0x0005ACA4 File Offset: 0x00058EA4
		public GenericDecalsMeshBase LinkedDecalsMesh { get; internal set; }

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x0600981D RID: 38941 RVA: 0x0005ACAD File Offset: 0x00058EAD
		public bool IsLinkedWithADecalsMesh
		{
			get
			{
				return this.LinkedDecalsMesh != null;
			}
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x0600981E RID: 38942
		// (set) Token: 0x0600981F RID: 38943
		public abstract bool CastShadows { get; set; }

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06009820 RID: 38944
		// (set) Token: 0x06009821 RID: 38945
		public abstract bool ReceiveShadows { get; set; }

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06009822 RID: 38946
		// (set) Token: 0x06009823 RID: 38947
		public abstract bool UseLightProbes { get; set; }

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06009824 RID: 38948
		// (set) Token: 0x06009825 RID: 38949
		public abstract Transform LightProbeAnchor { get; set; }

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06009826 RID: 38950 RVA: 0x0005ACB8 File Offset: 0x00058EB8
		// (set) Token: 0x06009827 RID: 38951 RVA: 0x0014D7BC File Offset: 0x0014B9BC
		public ProjectionMode CurrentProjectionMode
		{
			get
			{
				return this.m_ProjectionMode;
			}
			set
			{
				if (this.IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The projection mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				this.m_ProjectionMode = value;
				if (this.m_ProjectionMode == ProjectionMode.Diffuse)
				{
					this.m_UVMode = UVMode.Project;
					this.m_UV2Mode = UV2Mode.None;
					this.m_NormalsMode = NormalsMode.Target;
					this.m_TangentsMode = TangentsMode.None;
					return;
				}
				if (this.m_ProjectionMode == ProjectionMode.BumpedDiffuse)
				{
					this.m_UVMode = UVMode.Project;
					this.m_UV2Mode = UV2Mode.None;
					this.m_NormalsMode = NormalsMode.Target;
					this.m_TangentsMode = TangentsMode.Project;
					return;
				}
				if (this.m_ProjectionMode == ProjectionMode.LightmappedDiffuse)
				{
					this.m_UVMode = UVMode.Project;
					this.m_UV2Mode = UV2Mode.Lightmapping;
					this.m_NormalsMode = NormalsMode.Target;
					this.m_TangentsMode = TangentsMode.None;
					return;
				}
				if (this.m_ProjectionMode == ProjectionMode.LightmappedBumpedDiffuse)
				{
					this.m_UVMode = UVMode.Project;
					this.m_UV2Mode = UV2Mode.Lightmapping;
					this.m_NormalsMode = NormalsMode.Target;
					this.m_TangentsMode = TangentsMode.Project;
					return;
				}
				if (this.m_ProjectionMode == ProjectionMode.BumpOfTarget)
				{
					this.m_UVMode = UVMode.Project;
					this.m_UV2Mode = UV2Mode.TargetUV;
					this.m_NormalsMode = NormalsMode.Target;
					this.m_TangentsMode = TangentsMode.Target;
				}
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06009828 RID: 38952 RVA: 0x0005ACC0 File Offset: 0x00058EC0
		// (set) Token: 0x06009829 RID: 38953 RVA: 0x0005ACC8 File Offset: 0x00058EC8
		public UVMode CurrentUVMode
		{
			get
			{
				return this.m_UVMode;
			}
			set
			{
				if (this.IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The uv mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				if (this.CurrentProjectionMode == ProjectionMode.Advanced)
				{
					this.m_UVMode = value;
					return;
				}
				throw new InvalidOperationException("Setting a new uv mode is only possible in the advanced projection mode!");
			}
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x0600982A RID: 38954 RVA: 0x0005ACF8 File Offset: 0x00058EF8
		// (set) Token: 0x0600982B RID: 38955 RVA: 0x0005AD00 File Offset: 0x00058F00
		public UV2Mode CurrentUV2Mode
		{
			get
			{
				return this.m_UV2Mode;
			}
			set
			{
				if (this.IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The uv2 mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				if (this.CurrentProjectionMode == ProjectionMode.Advanced)
				{
					this.m_UV2Mode = value;
					return;
				}
				throw new InvalidOperationException("Setting a new uv2 mode is only possible in the advanced projection mode!");
			}
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x0600982C RID: 38956 RVA: 0x0005AD30 File Offset: 0x00058F30
		// (set) Token: 0x0600982D RID: 38957 RVA: 0x0005AD38 File Offset: 0x00058F38
		public NormalsMode CurrentNormalsMode
		{
			get
			{
				return this.m_NormalsMode;
			}
			set
			{
				if (this.IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The normals mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				if (this.CurrentProjectionMode == ProjectionMode.Advanced)
				{
					this.m_NormalsMode = value;
					return;
				}
				throw new InvalidOperationException("Setting a new normals mode is only possible in the advanced projection mode!");
			}
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x0600982E RID: 38958 RVA: 0x0005AD68 File Offset: 0x00058F68
		// (set) Token: 0x0600982F RID: 38959 RVA: 0x0005AD70 File Offset: 0x00058F70
		public TangentsMode CurrentTangentsMode
		{
			get
			{
				return this.m_TangentsMode;
			}
			set
			{
				if (this.IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The tangents mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				if (this.CurrentProjectionMode == ProjectionMode.Advanced)
				{
					this.m_TangentsMode = value;
					return;
				}
				throw new InvalidOperationException("Setting a new tangents mode is only possible in the advanced projection mode!");
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x06009830 RID: 38960 RVA: 0x0005ADA0 File Offset: 0x00058FA0
		// (set) Token: 0x06009831 RID: 38961 RVA: 0x0005ADA8 File Offset: 0x00058FA8
		public bool UseVertexColors
		{
			get
			{
				return this.m_UseVertexColors;
			}
			set
			{
				if (this.IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The vertex color mode can't be changed as long as the instance is linked with a decals mesh.'");
				}
				if (value && Edition.IsDecalSystemFree)
				{
					throw new InvalidOperationException("Vertex colors can only be used in Decal System Pro.");
				}
				this.m_UseVertexColors = value;
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x06009832 RID: 38962 RVA: 0x0005ADD9 File Offset: 0x00058FD9
		// (set) Token: 0x06009833 RID: 38963 RVA: 0x0005ADE1 File Offset: 0x00058FE1
		public Color VertexColorTint
		{
			get
			{
				return this.m_VertexColorTint;
			}
			set
			{
				this.m_VertexColorTint = value;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06009834 RID: 38964 RVA: 0x0005ADEA File Offset: 0x00058FEA
		// (set) Token: 0x06009835 RID: 38965 RVA: 0x0005ADF2 File Offset: 0x00058FF2
		public bool AffectSameLODOnly
		{
			get
			{
				return this.m_AffectSameLODOnly;
			}
			set
			{
				if (Application.isPlaying)
				{
					throw new InvalidOperationException("This operation can only be executed in the editor, meaning while the application is not playing.");
				}
				this.m_AffectSameLODOnly = value;
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06009836 RID: 38966 RVA: 0x0005AE0D File Offset: 0x0005900D
		// (set) Token: 0x06009837 RID: 38967 RVA: 0x0005AE15 File Offset: 0x00059015
		public LightmapUpdateMode LightmapUpdateMode
		{
			get
			{
				return this.m_LightmapUpdateMode;
			}
			set
			{
				if (Application.isPlaying)
				{
					throw new InvalidOperationException("This operation can only be executed in the editor, meaning while the application is not playing.");
				}
				this.m_LightmapUpdateMode = value;
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06009838 RID: 38968 RVA: 0x0005AE30 File Offset: 0x00059030
		// (set) Token: 0x06009839 RID: 38969 RVA: 0x0005AE38 File Offset: 0x00059038
		public bool AreRenderersEditable
		{
			get
			{
				return this.m_AreRenderersEditable;
			}
			set
			{
				if (Edition.IsDecalSystemFree && value)
				{
					throw new InvalidOperationException("The renderer editability can only be used in Decal System Pro.");
				}
				this.m_AreRenderersEditable = value;
				this.ApplyRenderersEditability();
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x0600983A RID: 38970 RVA: 0x0005AE5B File Offset: 0x0005905B
		// (set) Token: 0x0600983B RID: 38971 RVA: 0x0005AE63 File Offset: 0x00059063
		public TextureAtlasType CurrentTextureAtlasType
		{
			get
			{
				return this.m_TextureAtlasType;
			}
			set
			{
				if (Edition.IsDecalSystemFree && value == TextureAtlasType.Reference)
				{
					throw new InvalidOperationException("Texture atlas assets can only be used in Decal System Pro.");
				}
				this.m_TextureAtlasType = value;
				this.ApplyMaterialToMeshRenderers();
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x0600983C RID: 38972 RVA: 0x0005AE88 File Offset: 0x00059088
		// (set) Token: 0x0600983D RID: 38973 RVA: 0x0005AE90 File Offset: 0x00059090
		public TextureAtlasAsset CurrentTextureAtlasAsset
		{
			get
			{
				return this.m_TextureAtlasAsset;
			}
			set
			{
				this.m_TextureAtlasAsset = value;
				if (!Edition.IsDecalSystemFree && this.CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					this.ApplyMaterialToMeshRenderers();
				}
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x0600983E RID: 38974 RVA: 0x0005AEAF File Offset: 0x000590AF
		// (set) Token: 0x0600983F RID: 38975 RVA: 0x0005AEB7 File Offset: 0x000590B7
		public Material CurrentMaterial
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
				if (this.CurrentTextureAtlasType == TextureAtlasType.Builtin)
				{
					this.ApplyMaterialToMeshRenderers();
				}
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06009840 RID: 38976 RVA: 0x0014D8A0 File Offset: 0x0014BAA0
		public UVRectangle[] CurrentUvRectangles
		{
			get
			{
				UVRectangle[] array = null;
				if (this.CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					if (this.CurrentTextureAtlasAsset != null)
					{
						array = this.CurrentTextureAtlasAsset.uvRectangles;
					}
				}
				else if (this.CurrentTextureAtlasType == TextureAtlasType.Builtin)
				{
					array = this.uvRectangles;
				}
				return array;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06009841 RID: 38977 RVA: 0x0014D8E4 File Offset: 0x0014BAE4
		public UVRectangle[] CurrentUv2Rectangles
		{
			get
			{
				UVRectangle[] array = null;
				if (this.CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					if (this.CurrentTextureAtlasAsset != null)
					{
						array = this.CurrentTextureAtlasAsset.uv2Rectangles;
					}
				}
				else if (this.CurrentTextureAtlasType == TextureAtlasType.Builtin)
				{
					array = this.uv2Rectangles;
				}
				return array;
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06009842 RID: 38978 RVA: 0x0005AECE File Offset: 0x000590CE
		public bool AreDecalsMeshesOptimized
		{
			get
			{
				return this.m_AreDecalsMeshesOptimized;
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06009843 RID: 38979 RVA: 0x0005AED6 File Offset: 0x000590D6
		// (set) Token: 0x06009844 RID: 38980 RVA: 0x0005AEDE File Offset: 0x000590DE
		public string MeshAssetFolder
		{
			get
			{
				return this.m_MeshAssetFolder;
			}
			set
			{
				this.m_MeshAssetFolder = value;
			}
		}

		// Token: 0x06009845 RID: 38981
		public abstract void OnEnable();

		// Token: 0x06009846 RID: 38982
		public abstract void InitializeDecalsMeshRenderers();

		// Token: 0x06009847 RID: 38983 RVA: 0x0005AEE7 File Offset: 0x000590E7
		public virtual void OptimizeDecalsMeshes()
		{
			this.m_AreDecalsMeshesOptimized = true;
		}

		// Token: 0x06009848 RID: 38984 RVA: 0x0005AEF0 File Offset: 0x000590F0
		public void SetDecalsMeshesAreNotOptimized()
		{
			this.m_AreDecalsMeshesOptimized = false;
		}

		// Token: 0x06009849 RID: 38985 RVA: 0x0005AEF9 File Offset: 0x000590F9
		public virtual void ApplyMaterialToMeshRenderers()
		{
			if (Edition.IsDecalSystemFree && this.CurrentTextureAtlasType == TextureAtlasType.Reference)
			{
				throw new InvalidOperationException("Texture atlas assets are only supported in Decal System Pro.");
			}
		}

		// Token: 0x0600984A RID: 38986 RVA: 0x0005AF16 File Offset: 0x00059116
		public virtual void ApplyRenderersEditability()
		{
			if (Edition.IsDecalSystemFree && this.m_AreRenderersEditable)
			{
				this.m_AreRenderersEditable = false;
			}
		}

		// Token: 0x040063E8 RID: 25576
		public const int c_MaximumVertexCount = 65535;

		// Token: 0x040063E9 RID: 25577
		[SerializeField]
		private ProjectionMode m_ProjectionMode;

		// Token: 0x040063EA RID: 25578
		[SerializeField]
		private UVMode m_UVMode;

		// Token: 0x040063EB RID: 25579
		[SerializeField]
		private UV2Mode m_UV2Mode;

		// Token: 0x040063EC RID: 25580
		[SerializeField]
		private NormalsMode m_NormalsMode = NormalsMode.Target;

		// Token: 0x040063ED RID: 25581
		[SerializeField]
		private TangentsMode m_TangentsMode;

		// Token: 0x040063EE RID: 25582
		[SerializeField]
		private bool m_UseVertexColors;

		// Token: 0x040063EF RID: 25583
		[SerializeField]
		private Color m_VertexColorTint = Color.white;

		// Token: 0x040063F0 RID: 25584
		[SerializeField]
		private bool m_AffectSameLODOnly;

		// Token: 0x040063F1 RID: 25585
		[SerializeField]
		private LightmapUpdateMode m_LightmapUpdateMode;

		// Token: 0x040063F2 RID: 25586
		[SerializeField]
		private bool m_AreRenderersEditable;

		// Token: 0x040063F3 RID: 25587
		[SerializeField]
		private TextureAtlasType m_TextureAtlasType;

		// Token: 0x040063F4 RID: 25588
		[SerializeField]
		private TextureAtlasAsset m_TextureAtlasAsset;

		// Token: 0x040063F5 RID: 25589
		[SerializeField]
		private Material m_Material;

		// Token: 0x040063F6 RID: 25590
		public UVRectangle[] uvRectangles = new UVRectangle[0];

		// Token: 0x040063F7 RID: 25591
		public UVRectangle[] uv2Rectangles = new UVRectangle[0];

		// Token: 0x040063F8 RID: 25592
		[SerializeField]
		private bool m_AreDecalsMeshesOptimized;

		// Token: 0x040063F9 RID: 25593
		[SerializeField]
		private string m_MeshAssetFolder = "Assets";

		// Token: 0x040063FA RID: 25594
		private Transform m_CachedTransform;
	}
}
