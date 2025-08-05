using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000188 RID: 392
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Bloom and Glow/BloomAndFlares (3.5, Deprecated)")]
	public class BloomAndFlares : PostEffectsBase
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00074308 File Offset: 0x00072508
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			this.doHdr = false;
			if (this.hdr == HDRBloomMode.Auto)
			{
				this.doHdr = source.format == RenderTextureFormat.ARGBHalf && base.GetComponent<Camera>().hdr;
			}
			else
			{
				this.doHdr = this.hdr == HDRBloomMode.On;
			}
			this.doHdr = this.doHdr && this.supportHDRTextures;
			BloomScreenBlendMode bloomScreenBlendMode = this.screenBlendMode;
			if (this.doHdr)
			{
				bloomScreenBlendMode = BloomScreenBlendMode.Add;
			}
			RenderTextureFormat renderTextureFormat = ((!this.doHdr) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, renderTextureFormat);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			Graphics.Blit(source, temporary, this.screenBlend, 2);
			Graphics.Blit(temporary, temporary2, this.screenBlend, 2);
			RenderTexture.ReleaseTemporary(temporary);
			this.BrightFilter(this.bloomThreshold, this.useSrcAlphaAsMask, temporary2, temporary3);
			temporary2.DiscardContents();
			if (this.bloomBlurIterations < 1)
			{
				this.bloomBlurIterations = 1;
			}
			for (int i = 0; i < this.bloomBlurIterations; i++)
			{
				float num3 = (1f + (float)i * 0.5f) * this.sepBlurSpread;
				this.separableBlurMaterial.SetVector("offsets", new Vector4(0f, num3 * num2, 0f, 0f));
				RenderTexture renderTexture = ((i != 0) ? temporary2 : temporary3);
				Graphics.Blit(renderTexture, temporary4, this.separableBlurMaterial);
				renderTexture.DiscardContents();
				this.separableBlurMaterial.SetVector("offsets", new Vector4(num3 / num * num2, 0f, 0f, 0f));
				Graphics.Blit(temporary4, temporary2, this.separableBlurMaterial);
				temporary4.DiscardContents();
			}
			if (this.lensflares)
			{
				if (this.lensflareMode == LensflareStyle34.Ghosting)
				{
					this.BrightFilter(this.lensflareThreshold, 0f, temporary2, temporary4);
					temporary2.DiscardContents();
					this.Vignette(0.975f, temporary4, temporary3);
					temporary4.DiscardContents();
					this.BlendFlares(temporary3, temporary2);
					temporary3.DiscardContents();
				}
				else
				{
					this.hollywoodFlaresMaterial.SetVector("_threshold", new Vector4(this.lensflareThreshold, 1f / (1f - this.lensflareThreshold), 0f, 0f));
					this.hollywoodFlaresMaterial.SetVector("tintColor", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.flareColorA.a * this.lensflareIntensity);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 2);
					temporary4.DiscardContents();
					Graphics.Blit(temporary3, temporary4, this.hollywoodFlaresMaterial, 3);
					temporary3.DiscardContents();
					this.hollywoodFlaresMaterial.SetVector("offsets", new Vector4(this.sepBlurSpread * 1f / num * num2, 0f, 0f, 0f));
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 1);
					temporary4.DiscardContents();
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 2f);
					Graphics.Blit(temporary3, temporary4, this.hollywoodFlaresMaterial, 1);
					temporary3.DiscardContents();
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 4f);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 1);
					temporary4.DiscardContents();
					if (this.lensflareMode == LensflareStyle34.Anamorphic)
					{
						for (int j = 0; j < this.hollywoodFlareBlurIterations; j++)
						{
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, 0f, 0f, 0f));
							Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
							temporary3.DiscardContents();
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, 0f, 0f, 0f));
							Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
							temporary4.DiscardContents();
						}
						this.AddTo(1f, temporary3, temporary2);
						temporary3.DiscardContents();
					}
					else
					{
						for (int k = 0; k < this.hollywoodFlareBlurIterations; k++)
						{
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, 0f, 0f, 0f));
							Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
							temporary3.DiscardContents();
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, 0f, 0f, 0f));
							Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
							temporary4.DiscardContents();
						}
						this.Vignette(1f, temporary3, temporary4);
						temporary3.DiscardContents();
						this.BlendFlares(temporary4, temporary3);
						temporary4.DiscardContents();
						this.AddTo(1f, temporary3, temporary2);
						temporary3.DiscardContents();
					}
				}
			}
			this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
			this.screenBlend.SetTexture("_ColorBuffer", source);
			Graphics.Blit(temporary2, destination, this.screenBlend, (int)bloomScreenBlendMode);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
			RenderTexture.ReleaseTemporary(temporary4);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00074918 File Offset: 0x00072B18
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.screenBlend = base.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
			this.lensFlareMaterial = base.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
			this.vignetteMaterial = base.CheckShaderAndCreateMaterial(this.vignetteShader, this.vignetteMaterial);
			this.separableBlurMaterial = base.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
			this.addBrightStuffBlendOneOneMaterial = base.CheckShaderAndCreateMaterial(this.addBrightStuffOneOneShader, this.addBrightStuffBlendOneOneMaterial);
			this.hollywoodFlaresMaterial = base.CheckShaderAndCreateMaterial(this.hollywoodFlaresShader, this.hollywoodFlaresMaterial);
			this.brightPassFilterMaterial = base.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0000A303 File Offset: 0x00008503
		private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
		{
			this.addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity_);
			Graphics.Blit(from, to, this.addBrightStuffBlendOneOneMaterial);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000749EC File Offset: 0x00072BEC
		private void BlendFlares(RenderTexture from, RenderTexture to)
		{
			this.lensFlareMaterial.SetVector("colorA", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorB", new Vector4(this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorC", new Vector4(this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorD", new Vector4(this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) * this.lensflareIntensity);
			Graphics.Blit(from, to, this.lensFlareMaterial);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00074B38 File Offset: 0x00072D38
		private void BrightFilter(float thresh, float useAlphaAsMask, RenderTexture from, RenderTexture to)
		{
			if (this.doHdr)
			{
				this.brightPassFilterMaterial.SetVector("threshold", new Vector4(thresh, 1f, 0f, 0f));
			}
			else
			{
				this.brightPassFilterMaterial.SetVector("threshold", new Vector4(thresh, 1f / (1f - thresh), 0f, 0f));
			}
			this.brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useAlphaAsMask);
			Graphics.Blit(from, to, this.brightPassFilterMaterial);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00074BC0 File Offset: 0x00072DC0
		private void Vignette(float amount, RenderTexture from, RenderTexture to)
		{
			if (this.lensFlareVignetteMask)
			{
				this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
				Graphics.Blit(from, to, this.screenBlend, 3);
				return;
			}
			this.vignetteMaterial.SetFloat("vignetteIntensity", amount);
			Graphics.Blit(from, to, this.vignetteMaterial);
		}

		// Token: 0x04000450 RID: 1104
		public TweakMode34 tweakMode;

		// Token: 0x04000451 RID: 1105
		public BloomScreenBlendMode screenBlendMode = BloomScreenBlendMode.Add;

		// Token: 0x04000452 RID: 1106
		public HDRBloomMode hdr;

		// Token: 0x04000453 RID: 1107
		public float sepBlurSpread = 1.5f;

		// Token: 0x04000454 RID: 1108
		public float useSrcAlphaAsMask = 0.5f;

		// Token: 0x04000455 RID: 1109
		public float bloomIntensity = 1f;

		// Token: 0x04000456 RID: 1110
		public float bloomThreshold = 0.5f;

		// Token: 0x04000457 RID: 1111
		public int bloomBlurIterations = 2;

		// Token: 0x04000458 RID: 1112
		public bool lensflares;

		// Token: 0x04000459 RID: 1113
		public int hollywoodFlareBlurIterations = 2;

		// Token: 0x0400045A RID: 1114
		public LensflareStyle34 lensflareMode = LensflareStyle34.Anamorphic;

		// Token: 0x0400045B RID: 1115
		public float hollyStretchWidth = 3.5f;

		// Token: 0x0400045C RID: 1116
		public float lensflareIntensity = 1f;

		// Token: 0x0400045D RID: 1117
		public float lensflareThreshold = 0.3f;

		// Token: 0x0400045E RID: 1118
		public Color flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);

		// Token: 0x0400045F RID: 1119
		public Color flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);

		// Token: 0x04000460 RID: 1120
		public Color flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);

		// Token: 0x04000461 RID: 1121
		public Color flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);

		// Token: 0x04000462 RID: 1122
		public Texture2D lensFlareVignetteMask;

		// Token: 0x04000463 RID: 1123
		public Shader lensFlareShader;

		// Token: 0x04000464 RID: 1124
		public Shader vignetteShader;

		// Token: 0x04000465 RID: 1125
		public Shader separableBlurShader;

		// Token: 0x04000466 RID: 1126
		public Shader addBrightStuffOneOneShader;

		// Token: 0x04000467 RID: 1127
		public Shader screenBlendShader;

		// Token: 0x04000468 RID: 1128
		public Shader hollywoodFlaresShader;

		// Token: 0x04000469 RID: 1129
		public Shader brightPassFilterShader;

		// Token: 0x0400046A RID: 1130
		private Material addBrightStuffBlendOneOneMaterial;

		// Token: 0x0400046B RID: 1131
		private Material brightPassFilterMaterial;

		// Token: 0x0400046C RID: 1132
		private bool doHdr;

		// Token: 0x0400046D RID: 1133
		private Material hollywoodFlaresMaterial;

		// Token: 0x0400046E RID: 1134
		private Material lensFlareMaterial;

		// Token: 0x0400046F RID: 1135
		private Material screenBlend;

		// Token: 0x04000470 RID: 1136
		private Material separableBlurMaterial;

		// Token: 0x04000471 RID: 1137
		private Material vignetteMaterial;
	}
}
