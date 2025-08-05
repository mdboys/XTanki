using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001CB RID: 459
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class AmbientOcclusionComponent : PostProcessingComponentCommandBuffer<AmbientOcclusionModel>
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0007CA58 File Offset: 0x0007AC58
		private AmbientOcclusionComponent.OcclusionSource occlusionSource
		{
			get
			{
				if (this.context.isGBufferAvailable && !base.model.settings.forceForwardCompatibility)
				{
					return AmbientOcclusionComponent.OcclusionSource.GBuffer;
				}
				if (base.model.settings.highPrecision && (!this.context.isGBufferAvailable || base.model.settings.forceForwardCompatibility))
				{
					return AmbientOcclusionComponent.OcclusionSource.DepthTexture;
				}
				return AmbientOcclusionComponent.OcclusionSource.DepthNormalsTexture;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0007CABC File Offset: 0x0007ACBC
		private bool ambientOnlySupported
		{
			get
			{
				return this.context.isHdr && base.model.settings.ambientOnly && this.context.isGBufferAvailable && !base.model.settings.forceForwardCompatibility;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0007CB0C File Offset: 0x0007AD0C
		public override bool active
		{
			get
			{
				AmbientOcclusionModel model = base.model;
				return model != null && model.enabled && model.settings.intensity > 0f && !this.context.interrupted;
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0007CB50 File Offset: 0x0007AD50
		public override DepthTextureMode GetCameraFlags()
		{
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			if (this.occlusionSource == AmbientOcclusionComponent.OcclusionSource.DepthTexture)
			{
				depthTextureMode |= DepthTextureMode.Depth;
			}
			if (this.occlusionSource != AmbientOcclusionComponent.OcclusionSource.GBuffer)
			{
				depthTextureMode |= DepthTextureMode.DepthNormals;
			}
			return depthTextureMode;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000AE35 File Offset: 0x00009035
		public override string GetName()
		{
			return "Ambient Occlusion";
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000AE3C File Offset: 0x0000903C
		public override CameraEvent GetCameraEvent()
		{
			if (this.ambientOnlySupported && !this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.AmbientOcclusion))
			{
				return CameraEvent.BeforeReflections;
			}
			return CameraEvent.BeforeImageEffectsOpaque;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0007CB7C File Offset: 0x0007AD7C
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			AmbientOcclusionModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Blit");
			Material material2 = this.context.materialFactory.Get("Hidden/Post FX/Ambient Occlusion");
			material2.shaderKeywords = null;
			material2.SetFloat(AmbientOcclusionComponent.Uniforms._Intensity, settings.intensity);
			material2.SetFloat(AmbientOcclusionComponent.Uniforms._Radius, settings.radius);
			material2.SetFloat(AmbientOcclusionComponent.Uniforms._Downsample, (!settings.downsampling) ? 1f : 0.5f);
			material2.SetInt(AmbientOcclusionComponent.Uniforms._SampleCount, (int)settings.sampleCount);
			int width = this.context.width;
			int height = this.context.height;
			int num = ((!settings.downsampling) ? 1 : 2);
			int num2 = AmbientOcclusionComponent.Uniforms._OcclusionTexture1;
			cb.GetTemporaryRT(num2, width / num, height / num, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.Blit(null, num2, material2, (int)this.occlusionSource);
			int occlusionTexture = AmbientOcclusionComponent.Uniforms._OcclusionTexture2;
			cb.GetTemporaryRT(occlusionTexture, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, num2);
			cb.Blit(num2, occlusionTexture, material2, (this.occlusionSource != AmbientOcclusionComponent.OcclusionSource.GBuffer) ? 3 : 4);
			cb.ReleaseTemporaryRT(num2);
			num2 = AmbientOcclusionComponent.Uniforms._OcclusionTexture;
			cb.GetTemporaryRT(num2, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, occlusionTexture);
			cb.Blit(occlusionTexture, num2, material2, 5);
			cb.ReleaseTemporaryRT(occlusionTexture);
			if (this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.AmbientOcclusion))
			{
				cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, num2);
				cb.Blit(num2, BuiltinRenderTextureType.CameraTarget, material2, 8);
				this.context.Interrupt();
			}
			else if (this.ambientOnlySupported)
			{
				cb.SetRenderTarget(this.m_MRT, BuiltinRenderTextureType.CameraTarget);
				cb.DrawMesh(GraphicsUtils.quad, Matrix4x4.identity, material2, 0, 7);
			}
			else
			{
				RenderTextureFormat renderTextureFormat = ((!this.context.isHdr) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR);
				int tempRT = AmbientOcclusionComponent.Uniforms._TempRT;
				cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Bilinear, renderTextureFormat);
				cb.Blit(BuiltinRenderTextureType.CameraTarget, tempRT, material, 0);
				cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, tempRT);
				cb.Blit(tempRT, BuiltinRenderTextureType.CameraTarget, material2, 6);
				cb.ReleaseTemporaryRT(tempRT);
			}
			cb.ReleaseTemporaryRT(num2);
		}

		// Token: 0x04000639 RID: 1593
		private const string k_BlitShaderString = "Hidden/Post FX/Blit";

		// Token: 0x0400063A RID: 1594
		private const string k_ShaderString = "Hidden/Post FX/Ambient Occlusion";

		// Token: 0x0400063B RID: 1595
		private readonly RenderTargetIdentifier[] m_MRT = new RenderTargetIdentifier[]
		{
			BuiltinRenderTextureType.GBuffer0,
			BuiltinRenderTextureType.CameraTarget
		};

		// Token: 0x020001CC RID: 460
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x0400063C RID: 1596
			internal static readonly int _Intensity = Shader.PropertyToID("_Intensity");

			// Token: 0x0400063D RID: 1597
			internal static readonly int _Radius = Shader.PropertyToID("_Radius");

			// Token: 0x0400063E RID: 1598
			internal static readonly int _Downsample = Shader.PropertyToID("_Downsample");

			// Token: 0x0400063F RID: 1599
			internal static readonly int _SampleCount = Shader.PropertyToID("_SampleCount");

			// Token: 0x04000640 RID: 1600
			internal static readonly int _OcclusionTexture1 = Shader.PropertyToID("_OcclusionTexture1");

			// Token: 0x04000641 RID: 1601
			internal static readonly int _OcclusionTexture2 = Shader.PropertyToID("_OcclusionTexture2");

			// Token: 0x04000642 RID: 1602
			internal static readonly int _OcclusionTexture = Shader.PropertyToID("_OcclusionTexture");

			// Token: 0x04000643 RID: 1603
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04000644 RID: 1604
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");
		}

		// Token: 0x020001CD RID: 461
		[NullableContext(0)]
		private enum OcclusionSource
		{
			// Token: 0x04000646 RID: 1606
			DepthTexture,
			// Token: 0x04000647 RID: 1607
			DepthNormalsTexture,
			// Token: 0x04000648 RID: 1608
			GBuffer
		}
	}
}
