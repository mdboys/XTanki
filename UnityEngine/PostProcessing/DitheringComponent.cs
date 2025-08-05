using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001FF RID: 511
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class DitheringComponent : PostProcessingComponentRenderTexture<DitheringModel>
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0000B37C File Offset: 0x0000957C
		public override bool active
		{
			get
			{
				return base.model.enabled && !this.context.interrupted;
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0000B39B File Offset: 0x0000959B
		public override void OnDisable()
		{
			this.noiseTextures = null;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0007F790 File Offset: 0x0007D990
		private void LoadNoiseTextures()
		{
			this.noiseTextures = new Texture2D[64];
			for (int i = 0; i < 64; i++)
			{
				this.noiseTextures[i] = Resources.Load<Texture2D>(string.Format("Bluenoise64/LDR_LLL1_{0}", i));
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0007F7D4 File Offset: 0x0007D9D4
		public override void Prepare(Material uberMaterial)
		{
			int num = this.textureIndex + 1;
			this.textureIndex = num;
			if (num >= 64)
			{
				this.textureIndex = 0;
			}
			float value = Random.value;
			float value2 = Random.value;
			if (this.noiseTextures == null)
			{
				this.LoadNoiseTextures();
			}
			Texture2D texture2D = this.noiseTextures[this.textureIndex];
			uberMaterial.EnableKeyword("DITHERING");
			uberMaterial.SetTexture(DitheringComponent.Uniforms._DitheringTex, texture2D);
			uberMaterial.SetVector(DitheringComponent.Uniforms._DitheringCoords, new Vector4((float)this.context.width / (float)texture2D.width, (float)this.context.height / (float)texture2D.height, value, value2));
		}

		// Token: 0x04000723 RID: 1827
		private const int k_TextureCount = 64;

		// Token: 0x04000724 RID: 1828
		private Texture2D[] noiseTextures;

		// Token: 0x04000725 RID: 1829
		private int textureIndex;

		// Token: 0x02000200 RID: 512
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x04000726 RID: 1830
			internal static readonly int _DitheringTex = Shader.PropertyToID("_DitheringTex");

			// Token: 0x04000727 RID: 1831
			internal static readonly int _DitheringCoords = Shader.PropertyToID("_DitheringCoords");
		}
	}
}
