using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000198 RID: 408
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Edge Detection/Crease Shading")]
	internal class CreaseShading : PostEffectsBase
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x00076B68 File Offset: 0x00074D68
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			int width = source.width;
			int height = source.height;
			float num = 1f * (float)width / (1f * (float)height);
			float num2 = 0.001953125f;
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			RenderTexture renderTexture = RenderTexture.GetTemporary(width / 2, height / 2, 0);
			Graphics.Blit(source, temporary, this.depthFetchMaterial);
			Graphics.Blit(temporary, renderTexture);
			for (int i = 0; i < this.softness; i++)
			{
				RenderTexture renderTexture2 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
				this.blurMaterial.SetVector("offsets", new Vector4(0f, this.spread * num2, 0f, 0f));
				Graphics.Blit(renderTexture, renderTexture2, this.blurMaterial);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
				renderTexture2 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
				this.blurMaterial.SetVector("offsets", new Vector4(this.spread * num2 / num, 0f, 0f, 0f));
				Graphics.Blit(renderTexture, renderTexture2, this.blurMaterial);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = renderTexture2;
			}
			this.creaseApplyMaterial.SetTexture("_HrDepthTex", temporary);
			this.creaseApplyMaterial.SetTexture("_LrDepthTex", renderTexture);
			this.creaseApplyMaterial.SetFloat("intensity", this.intensity);
			Graphics.Blit(source, destination, this.creaseApplyMaterial);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00076CF4 File Offset: 0x00074EF4
		public override bool CheckResources()
		{
			base.CheckSupport(true);
			this.blurMaterial = base.CheckShaderAndCreateMaterial(this.blurShader, this.blurMaterial);
			this.depthFetchMaterial = base.CheckShaderAndCreateMaterial(this.depthFetchShader, this.depthFetchMaterial);
			this.creaseApplyMaterial = base.CheckShaderAndCreateMaterial(this.creaseApplyShader, this.creaseApplyMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x040004E8 RID: 1256
		public float intensity = 0.5f;

		// Token: 0x040004E9 RID: 1257
		public int softness = 1;

		// Token: 0x040004EA RID: 1258
		public float spread = 1f;

		// Token: 0x040004EB RID: 1259
		public Shader blurShader;

		// Token: 0x040004EC RID: 1260
		public Shader depthFetchShader;

		// Token: 0x040004ED RID: 1261
		public Shader creaseApplyShader;

		// Token: 0x040004EE RID: 1262
		private Material blurMaterial;

		// Token: 0x040004EF RID: 1263
		private Material creaseApplyMaterial;

		// Token: 0x040004F0 RID: 1264
		private Material depthFetchMaterial;
	}
}
