using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200018E RID: 398
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Blur/Blur (Optimized)")]
	public class BlurOptimized : PostEffectsBase
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x0000A438 File Offset: 0x00008638
		public void OnDisable()
		{
			if (this.blurMaterial)
			{
				global::UnityEngine.Object.DestroyImmediate(this.blurMaterial);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00075020 File Offset: 0x00073220
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			float num = 1f / (1f * (float)(1 << this.downsample));
			this.blurMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num, (0f - this.blurSize) * num, 0f, 0f));
			source.filterMode = FilterMode.Bilinear;
			int num2 = source.width >> this.downsample;
			int num3 = source.height >> this.downsample;
			RenderTexture renderTexture = RenderTexture.GetTemporary(num2, num3, 0, source.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(source, renderTexture, this.blurMaterial, 0);
			int num4 = ((this.blurType != BlurOptimized.BlurType.StandardGauss) ? 2 : 0);
			for (int i = 0; i < this.blurIterations; i++)
			{
				float num5 = (float)i * 1f;
				this.blurMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num + num5, (0f - this.blurSize) * num - num5, 0f, 0f));
				RenderTexture renderTexture2 = RenderTexture.GetTemporary(num2, num3, 0, source.format);
				renderTexture2.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, renderTexture2, this.blurMaterial, 1 + num4);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
				renderTexture2 = RenderTexture.GetTemporary(num2, num3, 0, source.format);
				renderTexture2.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, renderTexture2, this.blurMaterial, 2 + num4);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
			}
			Graphics.Blit(renderTexture, destination);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0000A452 File Offset: 0x00008652
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.blurMaterial = base.CheckShaderAndCreateMaterial(this.blurShader, this.blurMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x04000487 RID: 1159
		[Range(0f, 2f)]
		public int downsample = 1;

		// Token: 0x04000488 RID: 1160
		[Range(0f, 10f)]
		public float blurSize = 3f;

		// Token: 0x04000489 RID: 1161
		[Range(1f, 4f)]
		public int blurIterations = 2;

		// Token: 0x0400048A RID: 1162
		public BlurOptimized.BlurType blurType;

		// Token: 0x0400048B RID: 1163
		public Shader blurShader;

		// Token: 0x0400048C RID: 1164
		private Material blurMaterial;

		// Token: 0x0200018F RID: 399
		[NullableContext(0)]
		public enum BlurType
		{
			// Token: 0x0400048E RID: 1166
			StandardGauss,
			// Token: 0x0400048F RID: 1167
			SgxGauss
		}
	}
}
