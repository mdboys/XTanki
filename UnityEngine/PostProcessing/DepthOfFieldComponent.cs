using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001FA RID: 506
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class DepthOfFieldComponent : PostProcessingComponentRenderTexture<DepthOfFieldModel>
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0000B2A8 File Offset: 0x000094A8
		public override bool active
		{
			get
			{
				return base.model.enabled && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf) && !this.context.interrupted;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00005B7A File Offset: 0x00003D7A
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0007F2EC File Offset: 0x0007D4EC
		private float CalculateFocalLength()
		{
			DepthOfFieldModel.Settings settings = base.model.settings;
			if (!settings.useCameraFov)
			{
				return settings.focalLength / 1000f;
			}
			float num = this.context.camera.fieldOfView * 0.017453292f;
			return 0.012f / Mathf.Tan(0.5f * num);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0007F344 File Offset: 0x0007D544
		private float CalculateMaxCoCRadius(int screenHeight)
		{
			float num = (float)base.model.settings.kernelSize * 4f + 6f;
			return Mathf.Min(0.05f, num / (float)screenHeight);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0000B2CF File Offset: 0x000094CF
		private bool CheckHistory(int width, int height)
		{
			return this.m_CoCHistory != null && this.m_CoCHistory.IsCreated() && this.m_CoCHistory.width == width && this.m_CoCHistory.height == height;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0000B30A File Offset: 0x0000950A
		private RenderTextureFormat SelectFormat(RenderTextureFormat primary, RenderTextureFormat secondary)
		{
			if (SystemInfo.SupportsRenderTextureFormat(primary))
			{
				return primary;
			}
			if (SystemInfo.SupportsRenderTextureFormat(secondary))
			{
				return secondary;
			}
			return RenderTextureFormat.Default;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0007F380 File Offset: 0x0007D580
		public void Prepare(RenderTexture source, Material uberMaterial, bool antialiasCoC, Vector2 taaJitter, float taaBlending)
		{
			DepthOfFieldModel.Settings settings = base.model.settings;
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGBHalf;
			RenderTextureFormat renderTextureFormat2 = this.SelectFormat(RenderTextureFormat.R8, RenderTextureFormat.RHalf);
			float num = this.CalculateFocalLength();
			float num2 = Mathf.Max(settings.focusDistance, num);
			float num3 = (float)source.width / (float)source.height;
			float num4 = num * num / (settings.aperture * (num2 - num) * 0.024f * 2f);
			float num5 = this.CalculateMaxCoCRadius(source.height);
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Depth Of Field");
			material.SetFloat(DepthOfFieldComponent.Uniforms._Distance, num2);
			material.SetFloat(DepthOfFieldComponent.Uniforms._LensCoeff, num4);
			material.SetFloat(DepthOfFieldComponent.Uniforms._MaxCoC, num5);
			material.SetFloat(DepthOfFieldComponent.Uniforms._RcpMaxCoC, 1f / num5);
			material.SetFloat(DepthOfFieldComponent.Uniforms._RcpAspect, 1f / num3);
			RenderTexture renderTexture = this.context.renderTextureFactory.Get(this.context.width, this.context.height, 0, renderTextureFormat2, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			Graphics.Blit(null, renderTexture, material, 0);
			if (antialiasCoC)
			{
				material.SetTexture(DepthOfFieldComponent.Uniforms._CoCTex, renderTexture);
				float num6 = ((!this.CheckHistory(this.context.width, this.context.height)) ? 0f : taaBlending);
				material.SetVector(DepthOfFieldComponent.Uniforms._TaaParams, new Vector3(taaJitter.x, taaJitter.y, num6));
				RenderTexture temporary = RenderTexture.GetTemporary(this.context.width, this.context.height, 0, renderTextureFormat2);
				Graphics.Blit(this.m_CoCHistory, temporary, material, 1);
				this.context.renderTextureFactory.Release(renderTexture);
				if (this.m_CoCHistory != null)
				{
					RenderTexture.ReleaseTemporary(this.m_CoCHistory);
				}
				renderTexture = (this.m_CoCHistory = temporary);
			}
			RenderTexture renderTexture2 = this.context.renderTextureFactory.Get(this.context.width / 2, this.context.height / 2, 0, renderTextureFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			material.SetTexture(DepthOfFieldComponent.Uniforms._CoCTex, renderTexture);
			Graphics.Blit(source, renderTexture2, material, 2);
			RenderTexture renderTexture3 = this.context.renderTextureFactory.Get(this.context.width / 2, this.context.height / 2, 0, renderTextureFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			Graphics.Blit(renderTexture2, renderTexture3, material, (int)(3 + settings.kernelSize));
			Graphics.Blit(renderTexture3, renderTexture2, material, 7);
			uberMaterial.SetVector(DepthOfFieldComponent.Uniforms._DepthOfFieldParams, new Vector3(num2, num4, num5));
			if (this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.FocusPlane))
			{
				uberMaterial.EnableKeyword("DEPTH_OF_FIELD_COC_VIEW");
				this.context.Interrupt();
			}
			else
			{
				uberMaterial.SetTexture(DepthOfFieldComponent.Uniforms._DepthOfFieldTex, renderTexture2);
				uberMaterial.SetTexture(DepthOfFieldComponent.Uniforms._DepthOfFieldCoCTex, renderTexture);
				uberMaterial.EnableKeyword("DEPTH_OF_FIELD");
			}
			this.context.renderTextureFactory.Release(renderTexture3);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0000B321 File Offset: 0x00009521
		public override void OnDisable()
		{
			if (this.m_CoCHistory != null)
			{
				RenderTexture.ReleaseTemporary(this.m_CoCHistory);
			}
			this.m_CoCHistory = null;
		}

		// Token: 0x0400070A RID: 1802
		private const string k_ShaderString = "Hidden/Post FX/Depth Of Field";

		// Token: 0x0400070B RID: 1803
		private const float k_FilmHeight = 0.024f;

		// Token: 0x0400070C RID: 1804
		private RenderTexture m_CoCHistory;

		// Token: 0x020001FB RID: 507
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x0400070D RID: 1805
			internal static readonly int _DepthOfFieldTex = Shader.PropertyToID("_DepthOfFieldTex");

			// Token: 0x0400070E RID: 1806
			internal static readonly int _DepthOfFieldCoCTex = Shader.PropertyToID("_DepthOfFieldCoCTex");

			// Token: 0x0400070F RID: 1807
			internal static readonly int _Distance = Shader.PropertyToID("_Distance");

			// Token: 0x04000710 RID: 1808
			internal static readonly int _LensCoeff = Shader.PropertyToID("_LensCoeff");

			// Token: 0x04000711 RID: 1809
			internal static readonly int _MaxCoC = Shader.PropertyToID("_MaxCoC");

			// Token: 0x04000712 RID: 1810
			internal static readonly int _RcpMaxCoC = Shader.PropertyToID("_RcpMaxCoC");

			// Token: 0x04000713 RID: 1811
			internal static readonly int _RcpAspect = Shader.PropertyToID("_RcpAspect");

			// Token: 0x04000714 RID: 1812
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04000715 RID: 1813
			internal static readonly int _CoCTex = Shader.PropertyToID("_CoCTex");

			// Token: 0x04000716 RID: 1814
			internal static readonly int _TaaParams = Shader.PropertyToID("_TaaParams");

			// Token: 0x04000717 RID: 1815
			internal static readonly int _DepthOfFieldParams = Shader.PropertyToID("_DepthOfFieldParams");
		}
	}
}
