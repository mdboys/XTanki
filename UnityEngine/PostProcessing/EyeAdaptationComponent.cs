using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000203 RID: 515
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class EyeAdaptationComponent : PostProcessingComponentRenderTexture<EyeAdaptationModel>
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0000B3FD File Offset: 0x000095FD
		public override bool active
		{
			get
			{
				return base.model.enabled && SystemInfo.supportsComputeShaders && !this.context.interrupted;
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000B423 File Offset: 0x00009623
		public void ResetHistory()
		{
			this.m_FirstFrame = true;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0000B423 File Offset: 0x00009623
		public override void OnEnable()
		{
			this.m_FirstFrame = true;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0007F890 File Offset: 0x0007DA90
		public override void OnDisable()
		{
			RenderTexture[] autoExposurePool = this.m_AutoExposurePool;
			for (int i = 0; i < autoExposurePool.Length; i++)
			{
				GraphicsUtils.Destroy(autoExposurePool[i]);
			}
			ComputeBuffer histogramBuffer = this.m_HistogramBuffer;
			if (histogramBuffer != null)
			{
				histogramBuffer.Release();
			}
			this.m_HistogramBuffer = null;
			if (this.m_DebugHistogram != null)
			{
				this.m_DebugHistogram.Release();
			}
			this.m_DebugHistogram = null;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0007F8F4 File Offset: 0x0007DAF4
		private Vector4 GetHistogramScaleOffsetRes()
		{
			EyeAdaptationModel.Settings settings = base.model.settings;
			float num = (float)(settings.logMax - settings.logMin);
			float num2 = 1f / num;
			float num3 = (float)(-(float)settings.logMin) * num2;
			return new Vector4(num2, num3, Mathf.Floor((float)this.context.width / 2f), Mathf.Floor((float)this.context.height / 2f));
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0007F964 File Offset: 0x0007DB64
		public Texture Prepare(RenderTexture source, Material uberMaterial)
		{
			EyeAdaptationModel.Settings settings = base.model.settings;
			if (this.m_EyeCompute == null)
			{
				this.m_EyeCompute = Resources.Load<ComputeShader>("Shaders/EyeHistogram");
			}
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Eye Adaptation");
			material.shaderKeywords = null;
			if (this.m_HistogramBuffer == null)
			{
				this.m_HistogramBuffer = new ComputeBuffer(64, 4);
			}
			if (EyeAdaptationComponent.s_EmptyHistogramBuffer == null)
			{
				EyeAdaptationComponent.s_EmptyHistogramBuffer = new uint[64];
			}
			Vector4 histogramScaleOffsetRes = this.GetHistogramScaleOffsetRes();
			RenderTexture renderTexture = this.context.renderTextureFactory.Get((int)histogramScaleOffsetRes.z, (int)histogramScaleOffsetRes.w, 0, source.format, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			Graphics.Blit(source, renderTexture);
			if (this.m_AutoExposurePool[0] == null || !this.m_AutoExposurePool[0].IsCreated())
			{
				this.m_AutoExposurePool[0] = new RenderTexture(1, 1, 0, RenderTextureFormat.RFloat);
			}
			if (this.m_AutoExposurePool[1] == null || !this.m_AutoExposurePool[1].IsCreated())
			{
				this.m_AutoExposurePool[1] = new RenderTexture(1, 1, 0, RenderTextureFormat.RFloat);
			}
			this.m_HistogramBuffer.SetData(EyeAdaptationComponent.s_EmptyHistogramBuffer);
			int num = this.m_EyeCompute.FindKernel("KEyeHistogram");
			this.m_EyeCompute.SetBuffer(num, "_Histogram", this.m_HistogramBuffer);
			this.m_EyeCompute.SetTexture(num, "_Source", renderTexture);
			this.m_EyeCompute.SetVector("_ScaleOffsetRes", histogramScaleOffsetRes);
			this.m_EyeCompute.Dispatch(num, Mathf.CeilToInt((float)renderTexture.width / 16f), Mathf.CeilToInt((float)renderTexture.height / 16f), 1);
			this.context.renderTextureFactory.Release(renderTexture);
			settings.highPercent = Mathf.Clamp(settings.highPercent, 1.01f, 99f);
			settings.lowPercent = Mathf.Clamp(settings.lowPercent, 1f, settings.highPercent - 0.01f);
			material.SetBuffer("_Histogram", this.m_HistogramBuffer);
			material.SetVector(EyeAdaptationComponent.Uniforms._Params, new Vector4(settings.lowPercent * 0.01f, settings.highPercent * 0.01f, Mathf.Exp(settings.minLuminance * 0.6931472f), Mathf.Exp(settings.maxLuminance * 0.6931472f)));
			material.SetVector(EyeAdaptationComponent.Uniforms._Speed, new Vector2(settings.speedDown, settings.speedUp));
			material.SetVector(EyeAdaptationComponent.Uniforms._ScaleOffsetRes, histogramScaleOffsetRes);
			material.SetFloat(EyeAdaptationComponent.Uniforms._ExposureCompensation, settings.keyValue);
			if (settings.dynamicKeyValue)
			{
				material.EnableKeyword("AUTO_KEY_VALUE");
			}
			if (this.m_FirstFrame || !Application.isPlaying)
			{
				this.m_CurrentAutoExposure = this.m_AutoExposurePool[0];
				Graphics.Blit(null, this.m_CurrentAutoExposure, material, 1);
				Graphics.Blit(this.m_AutoExposurePool[0], this.m_AutoExposurePool[1]);
			}
			else
			{
				int num2 = this.m_AutoExposurePingPing;
				Texture texture = this.m_AutoExposurePool[++num2 % 2];
				RenderTexture renderTexture2 = this.m_AutoExposurePool[++num2 % 2];
				Graphics.Blit(texture, renderTexture2, material, (int)settings.adaptationType);
				this.m_AutoExposurePingPing = (num2 + 1) % 2;
				this.m_CurrentAutoExposure = renderTexture2;
			}
			if (this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.EyeAdaptation))
			{
				if (this.m_DebugHistogram == null || !this.m_DebugHistogram.IsCreated())
				{
					this.m_DebugHistogram = new RenderTexture(256, 128, 0, RenderTextureFormat.ARGB32)
					{
						filterMode = FilterMode.Point,
						wrapMode = TextureWrapMode.Clamp
					};
				}
				material.SetFloat(EyeAdaptationComponent.Uniforms._DebugWidth, (float)this.m_DebugHistogram.width);
				Graphics.Blit(null, this.m_DebugHistogram, material, 2);
			}
			this.m_FirstFrame = false;
			return this.m_CurrentAutoExposure;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0007FD28 File Offset: 0x0007DF28
		public void OnGUI()
		{
			if (!(this.m_DebugHistogram == null) && this.m_DebugHistogram.IsCreated())
			{
				GUI.DrawTexture(new Rect(this.context.viewport.x * (float)Screen.width + 8f, 8f, (float)this.m_DebugHistogram.width, (float)this.m_DebugHistogram.height), this.m_DebugHistogram);
			}
		}

		// Token: 0x04000729 RID: 1833
		private const int k_HistogramBins = 64;

		// Token: 0x0400072A RID: 1834
		private const int k_HistogramThreadX = 16;

		// Token: 0x0400072B RID: 1835
		private const int k_HistogramThreadY = 16;

		// Token: 0x0400072C RID: 1836
		private static uint[] s_EmptyHistogramBuffer;

		// Token: 0x0400072D RID: 1837
		private readonly RenderTexture[] m_AutoExposurePool = new RenderTexture[2];

		// Token: 0x0400072E RID: 1838
		private int m_AutoExposurePingPing;

		// Token: 0x0400072F RID: 1839
		private RenderTexture m_CurrentAutoExposure;

		// Token: 0x04000730 RID: 1840
		private RenderTexture m_DebugHistogram;

		// Token: 0x04000731 RID: 1841
		private ComputeShader m_EyeCompute;

		// Token: 0x04000732 RID: 1842
		private bool m_FirstFrame = true;

		// Token: 0x04000733 RID: 1843
		private ComputeBuffer m_HistogramBuffer;

		// Token: 0x02000204 RID: 516
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x04000734 RID: 1844
			internal static readonly int _Params = Shader.PropertyToID("_Params");

			// Token: 0x04000735 RID: 1845
			internal static readonly int _Speed = Shader.PropertyToID("_Speed");

			// Token: 0x04000736 RID: 1846
			internal static readonly int _ScaleOffsetRes = Shader.PropertyToID("_ScaleOffsetRes");

			// Token: 0x04000737 RID: 1847
			internal static readonly int _ExposureCompensation = Shader.PropertyToID("_ExposureCompensation");

			// Token: 0x04000738 RID: 1848
			internal static readonly int _AutoExposure = Shader.PropertyToID("_AutoExposure");

			// Token: 0x04000739 RID: 1849
			internal static readonly int _DebugWidth = Shader.PropertyToID("_DebugWidth");
		}
	}
}
