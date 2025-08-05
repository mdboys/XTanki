using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000231 RID: 561
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class TaaComponent : PostProcessingComponentRenderTexture<AntialiasingModel>
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x000829E4 File Offset: 0x00080BE4
		public override bool active
		{
			get
			{
				AntialiasingModel model = base.model;
				return model != null && model.enabled && model.settings.method == AntialiasingModel.Method.Taa && SystemInfo.supportsMotionVectors && SystemInfo.supportedRenderTargetCount >= 2 && !this.context.interrupted;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0000B932 File Offset: 0x00009B32
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0000B93A File Offset: 0x00009B3A
		public Vector2 jitterVector { get; private set; }

		// Token: 0x060009EE RID: 2542 RVA: 0x00009BAE File Offset: 0x00007DAE
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000B943 File Offset: 0x00009B43
		public void ResetHistory()
		{
			this.m_ResetHistory = true;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00082A30 File Offset: 0x00080C30
		public void SetProjectionMatrix(Func<Vector2, Matrix4x4> jitteredFunc)
		{
			AntialiasingModel.TaaSettings taaSettings = base.model.settings.taaSettings;
			Vector2 vector = this.GenerateRandomOffset();
			vector *= taaSettings.jitterSpread;
			this.context.camera.nonJitteredProjectionMatrix = this.context.camera.projectionMatrix;
			if (jitteredFunc != null)
			{
				this.context.camera.projectionMatrix = jitteredFunc(vector);
			}
			else
			{
				this.context.camera.projectionMatrix = ((!this.context.camera.orthographic) ? this.GetPerspectiveProjectionMatrix(vector) : this.GetOrthographicProjectionMatrix(vector));
			}
			this.context.camera.useJitteredProjectionMatrixForTransparentRendering = false;
			vector.x /= (float)this.context.width;
			vector.y /= (float)this.context.height;
			this.context.materialFactory.Get("Hidden/Post FX/Temporal Anti-aliasing").SetVector(TaaComponent.Uniforms._Jitter, vector);
			this.jitterVector = vector;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00082B3C File Offset: 0x00080D3C
		public void Render(RenderTexture source, RenderTexture destination)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Temporal Anti-aliasing");
			material.shaderKeywords = null;
			AntialiasingModel.TaaSettings taaSettings = base.model.settings.taaSettings;
			if (this.m_ResetHistory || this.m_HistoryTexture == null || this.m_HistoryTexture.width != source.width || this.m_HistoryTexture.height != source.height)
			{
				if (this.m_HistoryTexture)
				{
					RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
				}
				this.m_HistoryTexture = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
				this.m_HistoryTexture.name = "TAA History";
				Graphics.Blit(source, this.m_HistoryTexture, material, 2);
			}
			material.SetVector(TaaComponent.Uniforms._SharpenParameters, new Vector4(taaSettings.sharpen, 0f, 0f, 0f));
			material.SetVector(TaaComponent.Uniforms._FinalBlendParameters, new Vector4(taaSettings.stationaryBlending, taaSettings.motionBlending, 6000f, 0f));
			material.SetTexture(TaaComponent.Uniforms._MainTex, source);
			material.SetTexture(TaaComponent.Uniforms._HistoryTex, this.m_HistoryTexture);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			temporary.name = "TAA History";
			this.m_MRT[0] = destination.colorBuffer;
			this.m_MRT[1] = temporary.colorBuffer;
			Graphics.SetRenderTarget(this.m_MRT, source.depthBuffer);
			GraphicsUtils.Blit(material, (this.context.camera.orthographic > false) ? 1 : 0);
			RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
			this.m_HistoryTexture = temporary;
			this.m_ResetHistory = false;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00082CFC File Offset: 0x00080EFC
		private float GetHaltonValue(int index, int radix)
		{
			float num = 0f;
			float num2 = 1f / (float)radix;
			while (index > 0)
			{
				num += (float)(index % radix) * num2;
				index /= radix;
				num2 /= (float)radix;
			}
			return num;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00082D34 File Offset: 0x00080F34
		private Vector2 GenerateRandomOffset()
		{
			Vector2 vector = new Vector2(this.GetHaltonValue(this.m_SampleIndex & 1023, 2), this.GetHaltonValue(this.m_SampleIndex & 1023, 3));
			int num = this.m_SampleIndex + 1;
			this.m_SampleIndex = num;
			if (num >= 8)
			{
				this.m_SampleIndex = 0;
			}
			return vector;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00082D88 File Offset: 0x00080F88
		private Matrix4x4 GetPerspectiveProjectionMatrix(Vector2 offset)
		{
			float num = Mathf.Tan(0.008726646f * this.context.camera.fieldOfView);
			float num2 = num * this.context.camera.aspect;
			offset.x *= num2 / (0.5f * (float)this.context.width);
			offset.y *= num / (0.5f * (float)this.context.height);
			float num3 = (offset.x - num2) * this.context.camera.nearClipPlane;
			float num4 = (offset.x + num2) * this.context.camera.nearClipPlane;
			float num5 = (offset.y + num) * this.context.camera.nearClipPlane;
			float num6 = (offset.y - num) * this.context.camera.nearClipPlane;
			Matrix4x4 matrix4x = default(Matrix4x4);
			matrix4x[0, 0] = 2f * this.context.camera.nearClipPlane / (num4 - num3);
			matrix4x[0, 1] = 0f;
			matrix4x[0, 2] = (num4 + num3) / (num4 - num3);
			matrix4x[0, 3] = 0f;
			matrix4x[1, 0] = 0f;
			matrix4x[1, 1] = 2f * this.context.camera.nearClipPlane / (num5 - num6);
			matrix4x[1, 2] = (num5 + num6) / (num5 - num6);
			matrix4x[1, 3] = 0f;
			matrix4x[2, 0] = 0f;
			matrix4x[2, 1] = 0f;
			matrix4x[2, 2] = (0f - (this.context.camera.farClipPlane + this.context.camera.nearClipPlane)) / (this.context.camera.farClipPlane - this.context.camera.nearClipPlane);
			matrix4x[2, 3] = (0f - 2f * this.context.camera.farClipPlane * this.context.camera.nearClipPlane) / (this.context.camera.farClipPlane - this.context.camera.nearClipPlane);
			matrix4x[3, 0] = 0f;
			matrix4x[3, 1] = 0f;
			matrix4x[3, 2] = -1f;
			matrix4x[3, 3] = 0f;
			return matrix4x;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00083018 File Offset: 0x00081218
		private Matrix4x4 GetOrthographicProjectionMatrix(Vector2 offset)
		{
			float orthographicSize = this.context.camera.orthographicSize;
			float num = orthographicSize * this.context.camera.aspect;
			offset.x *= num / (0.5f * (float)this.context.width);
			offset.y *= orthographicSize / (0.5f * (float)this.context.height);
			float num2 = offset.x - num;
			float num3 = offset.x + num;
			float num4 = offset.y + orthographicSize;
			float num5 = offset.y - orthographicSize;
			return Matrix4x4.Ortho(num2, num3, num5, num4, this.context.camera.nearClipPlane, this.context.camera.farClipPlane);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0000B94C File Offset: 0x00009B4C
		public override void OnDisable()
		{
			if (this.m_HistoryTexture != null)
			{
				RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
			}
			this.m_HistoryTexture = null;
			this.m_SampleIndex = 0;
			this.ResetHistory();
		}

		// Token: 0x04000816 RID: 2070
		private const string k_ShaderString = "Hidden/Post FX/Temporal Anti-aliasing";

		// Token: 0x04000817 RID: 2071
		private const int k_SampleCount = 8;

		// Token: 0x04000818 RID: 2072
		private readonly RenderBuffer[] m_MRT = new RenderBuffer[2];

		// Token: 0x04000819 RID: 2073
		private RenderTexture m_HistoryTexture;

		// Token: 0x0400081A RID: 2074
		private bool m_ResetHistory = true;

		// Token: 0x0400081B RID: 2075
		private int m_SampleIndex;

		// Token: 0x02000232 RID: 562
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x0400081D RID: 2077
			internal static readonly int _Jitter = Shader.PropertyToID("_Jitter");

			// Token: 0x0400081E RID: 2078
			internal static readonly int _SharpenParameters = Shader.PropertyToID("_SharpenParameters");

			// Token: 0x0400081F RID: 2079
			internal static readonly int _FinalBlendParameters = Shader.PropertyToID("_FinalBlendParameters");

			// Token: 0x04000820 RID: 2080
			internal static readonly int _HistoryTex = Shader.PropertyToID("_HistoryTex");

			// Token: 0x04000821 RID: 2081
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");
		}
	}
}
