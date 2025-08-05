using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000182 RID: 386
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Bloom and Glow/Bloom")]
	public class Bloom : PostEffectsBase
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x00073848 File Offset: 0x00071A48
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			this.doHdr = false;
			if (this.hdr == Bloom.HDRBloomMode.Auto)
			{
				this.doHdr = source.format == RenderTextureFormat.ARGBHalf && base.GetComponent<Camera>().hdr;
			}
			else
			{
				this.doHdr = this.hdr == Bloom.HDRBloomMode.On;
			}
			this.doHdr = this.doHdr && this.supportHDRTextures;
			Bloom.BloomScreenBlendMode bloomScreenBlendMode = this.screenBlendMode;
			if (this.doHdr)
			{
				bloomScreenBlendMode = Bloom.BloomScreenBlendMode.Add;
			}
			RenderTextureFormat renderTextureFormat = ((!this.doHdr) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf);
			int num = source.width / 2;
			int num2 = source.height / 2;
			int num3 = source.width / 4;
			int num4 = source.height / 4;
			float num5 = 1f * (float)source.width / (1f * (float)source.height);
			float num6 = 0.001953125f;
			RenderTexture temporary = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
			RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0, renderTextureFormat);
			if (this.quality > Bloom.BloomQuality.Cheap)
			{
				Graphics.Blit(source, temporary2, this.screenBlend, 2);
				RenderTexture temporary3 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
				Graphics.Blit(temporary2, temporary3, this.screenBlend, 2);
				Graphics.Blit(temporary3, temporary, this.screenBlend, 6);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				Graphics.Blit(source, temporary2);
				Graphics.Blit(temporary2, temporary, this.screenBlend, 6);
			}
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture renderTexture = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
			this.BrightFilter(this.bloomThreshold * this.bloomThresholdColor, temporary, renderTexture);
			if (this.bloomBlurIterations < 1)
			{
				this.bloomBlurIterations = 1;
			}
			else if (this.bloomBlurIterations > 10)
			{
				this.bloomBlurIterations = 10;
			}
			for (int i = 0; i < this.bloomBlurIterations; i++)
			{
				float num7 = (1f + (float)i * 0.25f) * this.sepBlurSpread;
				RenderTexture renderTexture2 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
				this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0f, num7 * num6, 0f, 0f));
				Graphics.Blit(renderTexture, renderTexture2, this.blurAndFlaresMaterial, 4);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
				renderTexture2 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
				this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num7 / num5 * num6, 0f, 0f, 0f));
				Graphics.Blit(renderTexture, renderTexture2, this.blurAndFlaresMaterial, 4);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
				if (this.quality > Bloom.BloomQuality.Cheap)
				{
					if (i == 0)
					{
						Graphics.SetRenderTarget(temporary);
						GL.Clear(false, true, Color.black);
						Graphics.Blit(renderTexture, temporary);
					}
					else
					{
						temporary.MarkRestoreExpected();
						Graphics.Blit(renderTexture, temporary, this.screenBlend, 10);
					}
				}
			}
			if (this.quality > Bloom.BloomQuality.Cheap)
			{
				Graphics.SetRenderTarget(renderTexture);
				GL.Clear(false, true, Color.black);
				Graphics.Blit(temporary, renderTexture, this.screenBlend, 6);
			}
			if (this.lensflareIntensity > Mathf.Epsilon)
			{
				RenderTexture temporary4 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
				if (this.lensflareMode == Bloom.LensFlareStyle.Ghosting)
				{
					this.BrightFilter(this.lensflareThreshold, renderTexture, temporary4);
					if (this.quality > Bloom.BloomQuality.Cheap)
					{
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0f, 1.5f / (1f * (float)temporary.height), 0f, 0f));
						Graphics.SetRenderTarget(temporary);
						GL.Clear(false, true, Color.black);
						Graphics.Blit(temporary4, temporary, this.blurAndFlaresMaterial, 4);
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(1.5f / (1f * (float)temporary.width), 0f, 0f, 0f));
						Graphics.SetRenderTarget(temporary4);
						GL.Clear(false, true, Color.black);
						Graphics.Blit(temporary, temporary4, this.blurAndFlaresMaterial, 4);
					}
					this.Vignette(0.975f, temporary4, temporary4);
					this.BlendFlares(temporary4, renderTexture);
				}
				else
				{
					float num8 = 1f * Mathf.Cos(this.flareRotation);
					float num9 = 1f * Mathf.Sin(this.flareRotation);
					float num10 = this.hollyStretchWidth * 1f / num5 * num6;
					this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num8, num9, 0f, 0f));
					this.blurAndFlaresMaterial.SetVector("_Threshhold", new Vector4(this.lensflareThreshold, 1f, 0f, 0f));
					this.blurAndFlaresMaterial.SetVector("_TintColor", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.flareColorA.a * this.lensflareIntensity);
					this.blurAndFlaresMaterial.SetFloat("_Saturation", this.lensFlareSaturation);
					temporary.DiscardContents();
					Graphics.Blit(temporary4, temporary, this.blurAndFlaresMaterial, 2);
					temporary4.DiscardContents();
					Graphics.Blit(temporary, temporary4, this.blurAndFlaresMaterial, 3);
					this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num8 * num10, num9 * num10, 0f, 0f));
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth);
					temporary.DiscardContents();
					Graphics.Blit(temporary4, temporary, this.blurAndFlaresMaterial, 1);
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 2f);
					temporary4.DiscardContents();
					Graphics.Blit(temporary, temporary4, this.blurAndFlaresMaterial, 1);
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 4f);
					temporary.DiscardContents();
					Graphics.Blit(temporary4, temporary, this.blurAndFlaresMaterial, 1);
					for (int j = 0; j < this.hollywoodFlareBlurIterations; j++)
					{
						num10 = this.hollyStretchWidth * 2f / num5 * num6;
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num10 * num8, num10 * num9, 0f, 0f));
						temporary4.DiscardContents();
						Graphics.Blit(temporary, temporary4, this.blurAndFlaresMaterial, 4);
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num10 * num8, num10 * num9, 0f, 0f));
						temporary.DiscardContents();
						Graphics.Blit(temporary4, temporary, this.blurAndFlaresMaterial, 4);
					}
					if (this.lensflareMode == Bloom.LensFlareStyle.Anamorphic)
					{
						this.AddTo(1f, temporary, renderTexture);
					}
					else
					{
						this.Vignette(1f, temporary, temporary4);
						this.BlendFlares(temporary4, temporary);
						this.AddTo(1f, temporary, renderTexture);
					}
				}
				RenderTexture.ReleaseTemporary(temporary4);
			}
			int num11 = (int)bloomScreenBlendMode;
			this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
			this.screenBlend.SetTexture("_ColorBuffer", source);
			if (this.quality > Bloom.BloomQuality.Cheap)
			{
				RenderTexture temporary5 = RenderTexture.GetTemporary(num, num2, 0, renderTextureFormat);
				Graphics.Blit(renderTexture, temporary5);
				Graphics.Blit(temporary5, destination, this.screenBlend, num11);
				RenderTexture.ReleaseTemporary(temporary5);
			}
			else
			{
				Graphics.Blit(renderTexture, destination, this.screenBlend, num11);
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00073FAC File Offset: 0x000721AC
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.screenBlend = base.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
			this.lensFlareMaterial = base.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
			this.blurAndFlaresMaterial = base.CheckShaderAndCreateMaterial(this.blurAndFlaresShader, this.blurAndFlaresMaterial);
			this.brightPassFilterMaterial = base.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000A28C File Offset: 0x0000848C
		private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
		{
			this.screenBlend.SetFloat("_Intensity", intensity_);
			to.MarkRestoreExpected();
			Graphics.Blit(from, to, this.screenBlend, 9);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00074038 File Offset: 0x00072238
		private void BlendFlares(RenderTexture from, RenderTexture to)
		{
			this.lensFlareMaterial.SetVector("colorA", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorB", new Vector4(this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorC", new Vector4(this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorD", new Vector4(this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) * this.lensflareIntensity);
			to.MarkRestoreExpected();
			Graphics.Blit(from, to, this.lensFlareMaterial);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0000A2B4 File Offset: 0x000084B4
		private void BrightFilter(float thresh, RenderTexture from, RenderTexture to)
		{
			this.brightPassFilterMaterial.SetVector("_Threshhold", new Vector4(thresh, thresh, thresh, thresh));
			Graphics.Blit(from, to, this.brightPassFilterMaterial, 0);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0000A2DD File Offset: 0x000084DD
		private void BrightFilter(Color threshColor, RenderTexture from, RenderTexture to)
		{
			this.brightPassFilterMaterial.SetVector("_Threshhold", threshColor);
			Graphics.Blit(from, to, this.brightPassFilterMaterial, 1);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00074188 File Offset: 0x00072388
		private void Vignette(float amount, RenderTexture from, RenderTexture to)
		{
			if (this.lensFlareVignetteMask)
			{
				this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
				to.MarkRestoreExpected();
				Graphics.Blit((!(from == to)) ? from : null, to, this.screenBlend, (!(from == to)) ? 3 : 7);
				return;
			}
			if (from != to)
			{
				Graphics.SetRenderTarget(to);
				GL.Clear(false, true, Color.black);
				Graphics.Blit(from, to);
			}
		}

		// Token: 0x04000421 RID: 1057
		public Bloom.TweakMode tweakMode;

		// Token: 0x04000422 RID: 1058
		public Bloom.BloomScreenBlendMode screenBlendMode = Bloom.BloomScreenBlendMode.Add;

		// Token: 0x04000423 RID: 1059
		public Bloom.HDRBloomMode hdr;

		// Token: 0x04000424 RID: 1060
		public float sepBlurSpread = 2.5f;

		// Token: 0x04000425 RID: 1061
		public Bloom.BloomQuality quality = Bloom.BloomQuality.High;

		// Token: 0x04000426 RID: 1062
		public float bloomIntensity = 0.5f;

		// Token: 0x04000427 RID: 1063
		public float bloomThreshold = 0.5f;

		// Token: 0x04000428 RID: 1064
		public Color bloomThresholdColor = Color.white;

		// Token: 0x04000429 RID: 1065
		public int bloomBlurIterations = 2;

		// Token: 0x0400042A RID: 1066
		public int hollywoodFlareBlurIterations = 2;

		// Token: 0x0400042B RID: 1067
		public float flareRotation;

		// Token: 0x0400042C RID: 1068
		public Bloom.LensFlareStyle lensflareMode = Bloom.LensFlareStyle.Anamorphic;

		// Token: 0x0400042D RID: 1069
		public float hollyStretchWidth = 2.5f;

		// Token: 0x0400042E RID: 1070
		public float lensflareIntensity;

		// Token: 0x0400042F RID: 1071
		public float lensflareThreshold = 0.3f;

		// Token: 0x04000430 RID: 1072
		public float lensFlareSaturation = 0.75f;

		// Token: 0x04000431 RID: 1073
		public Color flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);

		// Token: 0x04000432 RID: 1074
		public Color flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);

		// Token: 0x04000433 RID: 1075
		public Color flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);

		// Token: 0x04000434 RID: 1076
		public Color flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);

		// Token: 0x04000435 RID: 1077
		public Texture2D lensFlareVignetteMask;

		// Token: 0x04000436 RID: 1078
		public Shader lensFlareShader;

		// Token: 0x04000437 RID: 1079
		public Shader screenBlendShader;

		// Token: 0x04000438 RID: 1080
		public Shader blurAndFlaresShader;

		// Token: 0x04000439 RID: 1081
		public Shader brightPassFilterShader;

		// Token: 0x0400043A RID: 1082
		[HideInInspector]
		public Material brightPassFilterMaterial;

		// Token: 0x0400043B RID: 1083
		private Material blurAndFlaresMaterial;

		// Token: 0x0400043C RID: 1084
		private bool doHdr;

		// Token: 0x0400043D RID: 1085
		private Material lensFlareMaterial;

		// Token: 0x0400043E RID: 1086
		private Material screenBlend;

		// Token: 0x02000183 RID: 387
		[NullableContext(0)]
		public enum BloomQuality
		{
			// Token: 0x04000440 RID: 1088
			Cheap,
			// Token: 0x04000441 RID: 1089
			High
		}

		// Token: 0x02000184 RID: 388
		[NullableContext(0)]
		public enum BloomScreenBlendMode
		{
			// Token: 0x04000443 RID: 1091
			Screen,
			// Token: 0x04000444 RID: 1092
			Add
		}

		// Token: 0x02000185 RID: 389
		[NullableContext(0)]
		public enum HDRBloomMode
		{
			// Token: 0x04000446 RID: 1094
			Auto,
			// Token: 0x04000447 RID: 1095
			On,
			// Token: 0x04000448 RID: 1096
			Off
		}

		// Token: 0x02000186 RID: 390
		[NullableContext(0)]
		public enum LensFlareStyle
		{
			// Token: 0x0400044A RID: 1098
			Ghosting,
			// Token: 0x0400044B RID: 1099
			Anamorphic,
			// Token: 0x0400044C RID: 1100
			Combined
		}

		// Token: 0x02000187 RID: 391
		[NullableContext(0)]
		public enum TweakMode
		{
			// Token: 0x0400044E RID: 1102
			Basic,
			// Token: 0x0400044F RID: 1103
			Complex
		}
	}
}
