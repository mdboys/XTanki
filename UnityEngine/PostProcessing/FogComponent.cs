using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000208 RID: 520
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class FogComponent : PostProcessingComponentCommandBuffer<FogModel>
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0000B478 File Offset: 0x00009678
		public override bool active
		{
			get
			{
				return base.model.enabled && this.context.isGBufferAvailable && RenderSettings.fog && !this.context.interrupted;
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0000B4AB File Offset: 0x000096AB
		public override string GetName()
		{
			return "Fog";
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00005B7A File Offset: 0x00003D7A
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0000B4B2 File Offset: 0x000096B2
		public override CameraEvent GetCameraEvent()
		{
			return CameraEvent.BeforeImageEffectsOpaque;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0007FE94 File Offset: 0x0007E094
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			FogModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Fog");
			material.shaderKeywords = null;
			material.SetColor(FogComponent.Uniforms._FogColor, RenderSettings.fogColor);
			material.SetFloat(FogComponent.Uniforms._Density, RenderSettings.fogDensity);
			material.SetFloat(FogComponent.Uniforms._Start, RenderSettings.fogStartDistance);
			material.SetFloat(FogComponent.Uniforms._End, RenderSettings.fogEndDistance);
			switch (RenderSettings.fogMode)
			{
			case FogMode.Linear:
				material.EnableKeyword("FOG_LINEAR");
				break;
			case FogMode.Exponential:
				material.EnableKeyword("FOG_EXP");
				break;
			case FogMode.ExponentialSquared:
				material.EnableKeyword("FOG_EXP2");
				break;
			}
			RenderTextureFormat renderTextureFormat = ((!this.context.isHdr) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR);
			cb.GetTemporaryRT(FogComponent.Uniforms._TempRT, this.context.width, this.context.height, 24, FilterMode.Bilinear, renderTextureFormat);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, FogComponent.Uniforms._TempRT);
			cb.Blit(FogComponent.Uniforms._TempRT, BuiltinRenderTextureType.CameraTarget, material, (settings.excludeSkybox > false) ? 1 : 0);
			cb.ReleaseTemporaryRT(FogComponent.Uniforms._TempRT);
		}

		// Token: 0x04000749 RID: 1865
		private const string k_ShaderString = "Hidden/Post FX/Fog";

		// Token: 0x02000209 RID: 521
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x0400074A RID: 1866
			internal static readonly int _FogColor = Shader.PropertyToID("_FogColor");

			// Token: 0x0400074B RID: 1867
			internal static readonly int _Density = Shader.PropertyToID("_Density");

			// Token: 0x0400074C RID: 1868
			internal static readonly int _Start = Shader.PropertyToID("_Start");

			// Token: 0x0400074D RID: 1869
			internal static readonly int _End = Shader.PropertyToID("_End");

			// Token: 0x0400074E RID: 1870
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");
		}
	}
}
