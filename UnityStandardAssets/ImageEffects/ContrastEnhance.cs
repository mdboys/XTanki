﻿using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000196 RID: 406
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Color Adjustments/Contrast Enhance (Unsharp Mask)")]
	internal class ContrastEnhance : PostEffectsBase
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x00076708 File Offset: 0x00074908
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			int width = source.width;
			int height = source.height;
			RenderTexture temporary = RenderTexture.GetTemporary(width / 2, height / 2, 0);
			Graphics.Blit(source, temporary);
			RenderTexture renderTexture = RenderTexture.GetTemporary(width / 4, height / 4, 0);
			Graphics.Blit(temporary, renderTexture);
			RenderTexture.ReleaseTemporary(temporary);
			this.separableBlurMaterial.SetVector("offsets", new Vector4(0f, this.blurSpread * 1f / (float)renderTexture.height, 0f, 0f));
			RenderTexture temporary2 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
			Graphics.Blit(renderTexture, temporary2, this.separableBlurMaterial);
			RenderTexture.ReleaseTemporary(renderTexture);
			this.separableBlurMaterial.SetVector("offsets", new Vector4(this.blurSpread * 1f / (float)renderTexture.width, 0f, 0f, 0f));
			renderTexture = RenderTexture.GetTemporary(width / 4, height / 4, 0);
			Graphics.Blit(temporary2, renderTexture, this.separableBlurMaterial);
			RenderTexture.ReleaseTemporary(temporary2);
			this.contrastCompositeMaterial.SetTexture("_MainTexBlurred", renderTexture);
			this.contrastCompositeMaterial.SetFloat("intensity", this.intensity);
			this.contrastCompositeMaterial.SetFloat("threshhold", this.threshold);
			Graphics.Blit(source, destination, this.contrastCompositeMaterial);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00076860 File Offset: 0x00074A60
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.contrastCompositeMaterial = base.CheckShaderAndCreateMaterial(this.contrastCompositeShader, this.contrastCompositeMaterial);
			this.separableBlurMaterial = base.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x040004D4 RID: 1236
		public float intensity = 0.5f;

		// Token: 0x040004D5 RID: 1237
		public float threshold;

		// Token: 0x040004D6 RID: 1238
		public float blurSpread = 1f;

		// Token: 0x040004D7 RID: 1239
		public Shader separableBlurShader;

		// Token: 0x040004D8 RID: 1240
		public Shader contrastCompositeShader;

		// Token: 0x040004D9 RID: 1241
		private Material contrastCompositeMaterial;

		// Token: 0x040004DA RID: 1242
		private Material separableBlurMaterial;
	}
}
