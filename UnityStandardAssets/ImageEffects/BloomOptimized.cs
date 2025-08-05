using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000189 RID: 393
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Bloom and Glow/Bloom (Optimized)")]
	public class BloomOptimized : PostEffectsBase
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x0000A323 File Offset: 0x00008523
		private void OnDisable()
		{
			if (this.fastBloomMaterial)
			{
				global::UnityEngine.Object.DestroyImmediate(this.fastBloomMaterial);
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00074D18 File Offset: 0x00072F18
		[NullableContext(1)]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			int num = ((this.resolution != BloomOptimized.Resolution.Low) ? 2 : 4);
			float num2 = ((this.resolution != BloomOptimized.Resolution.Low) ? 1f : 0.5f);
			this.fastBloomMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num2, 0f, this.threshold, this.intensity));
			source.filterMode = FilterMode.Bilinear;
			int num3 = source.width / num;
			int num4 = source.height / num;
			RenderTexture renderTexture = RenderTexture.GetTemporary(num3, num4, 0, source.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(source, renderTexture, this.fastBloomMaterial, 1);
			int num5 = ((this.blurType != BloomOptimized.BlurType.Standard) ? 2 : 0);
			for (int i = 0; i < this.blurIterations; i++)
			{
				this.fastBloomMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num2 + (float)i * 1f, 0f, this.threshold, this.intensity));
				RenderTexture renderTexture2 = RenderTexture.GetTemporary(num3, num4, 0, source.format);
				renderTexture2.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, renderTexture2, this.fastBloomMaterial, 2 + num5);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
				renderTexture2 = RenderTexture.GetTemporary(num3, num4, 0, source.format);
				renderTexture2.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, renderTexture2, this.fastBloomMaterial, 3 + num5);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
			}
			this.fastBloomMaterial.SetTexture("_Bloom", renderTexture);
			Graphics.Blit(source, destination, this.fastBloomMaterial, 0);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0000A33D File Offset: 0x0000853D
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.fastBloomMaterial = base.CheckShaderAndCreateMaterial(this.fastBloomShader, this.fastBloomMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x04000472 RID: 1138
		[Range(0f, 1.5f)]
		public float threshold = 0.25f;

		// Token: 0x04000473 RID: 1139
		[Range(0f, 2.5f)]
		public float intensity = 0.75f;

		// Token: 0x04000474 RID: 1140
		[Range(0.25f, 5.5f)]
		public float blurSize = 1f;

		// Token: 0x04000475 RID: 1141
		[Range(1f, 4f)]
		public int blurIterations = 1;

		// Token: 0x04000476 RID: 1142
		public BloomOptimized.BlurType blurType;

		// Token: 0x04000477 RID: 1143
		[Nullable(1)]
		public Shader fastBloomShader;

		// Token: 0x04000478 RID: 1144
		[Nullable(1)]
		private Material fastBloomMaterial;

		// Token: 0x04000479 RID: 1145
		private BloomOptimized.Resolution resolution;

		// Token: 0x0200018A RID: 394
		public enum BlurType
		{
			// Token: 0x0400047B RID: 1147
			Standard,
			// Token: 0x0400047C RID: 1148
			Sgx
		}

		// Token: 0x0200018B RID: 395
		public enum Resolution
		{
			// Token: 0x0400047E RID: 1150
			Low,
			// Token: 0x0400047F RID: 1151
			High
		}
	}
}
