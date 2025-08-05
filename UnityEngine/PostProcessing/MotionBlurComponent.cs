using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000216 RID: 534
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class MotionBlurComponent : PostProcessingComponentCommandBuffer<MotionBlurModel>
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0000B60C File Offset: 0x0000980C
		public MotionBlurComponent.ReconstructionFilter reconstructionFilter
		{
			get
			{
				if (this.m_ReconstructionFilter == null)
				{
					this.m_ReconstructionFilter = new MotionBlurComponent.ReconstructionFilter();
				}
				return this.m_ReconstructionFilter;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0000B627 File Offset: 0x00009827
		public MotionBlurComponent.FrameBlendingFilter frameBlendingFilter
		{
			get
			{
				if (this.m_FrameBlendingFilter == null)
				{
					this.m_FrameBlendingFilter = new MotionBlurComponent.FrameBlendingFilter();
				}
				return this.m_FrameBlendingFilter;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0008072C File Offset: 0x0007E92C
		public override bool active
		{
			get
			{
				MotionBlurModel.Settings settings = base.model.settings;
				return base.model.enabled && ((settings.shutterAngle > 0f && this.reconstructionFilter.IsSupported()) || settings.frameBlending > 0f) && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2 && !this.context.interrupted;
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000B642 File Offset: 0x00009842
		public override string GetName()
		{
			return "Motion Blur";
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0000B649 File Offset: 0x00009849
		public void ResetHistory()
		{
			MotionBlurComponent.FrameBlendingFilter frameBlendingFilter = this.m_FrameBlendingFilter;
			if (frameBlendingFilter != null)
			{
				frameBlendingFilter.Dispose();
			}
			this.m_FrameBlendingFilter = null;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00009BAE File Offset: 0x00007DAE
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000B663 File Offset: 0x00009863
		public override CameraEvent GetCameraEvent()
		{
			return CameraEvent.BeforeImageEffects;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0000B667 File Offset: 0x00009867
		public override void OnEnable()
		{
			this.m_FirstFrame = true;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00080794 File Offset: 0x0007E994
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			if (this.m_FirstFrame)
			{
				this.m_FirstFrame = false;
				return;
			}
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Motion Blur");
			Material material2 = this.context.materialFactory.Get("Hidden/Post FX/Blit");
			MotionBlurModel.Settings settings = base.model.settings;
			RenderTextureFormat renderTextureFormat = ((!this.context.isHdr) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR);
			int tempRT = MotionBlurComponent.Uniforms._TempRT;
			cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Point, renderTextureFormat);
			if (settings.shutterAngle > 0f && settings.frameBlending > 0f)
			{
				this.reconstructionFilter.ProcessImage(this.context, cb, ref settings, BuiltinRenderTextureType.CameraTarget, tempRT, material);
				this.frameBlendingFilter.BlendFrames(cb, settings.frameBlending, tempRT, BuiltinRenderTextureType.CameraTarget, material);
				this.frameBlendingFilter.PushFrame(cb, tempRT, this.context.width, this.context.height, material);
			}
			else if (settings.shutterAngle > 0f)
			{
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
				cb.Blit(BuiltinRenderTextureType.CameraTarget, tempRT, material2, 0);
				this.reconstructionFilter.ProcessImage(this.context, cb, ref settings, tempRT, BuiltinRenderTextureType.CameraTarget, material);
			}
			else if (settings.frameBlending > 0f)
			{
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
				cb.Blit(BuiltinRenderTextureType.CameraTarget, tempRT, material2, 0);
				this.frameBlendingFilter.BlendFrames(cb, settings.frameBlending, tempRT, BuiltinRenderTextureType.CameraTarget, material);
				this.frameBlendingFilter.PushFrame(cb, tempRT, this.context.width, this.context.height, material);
			}
			cb.ReleaseTemporaryRT(tempRT);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000B670 File Offset: 0x00009870
		public override void OnDisable()
		{
			MotionBlurComponent.FrameBlendingFilter frameBlendingFilter = this.m_FrameBlendingFilter;
			if (frameBlendingFilter == null)
			{
				return;
			}
			frameBlendingFilter.Dispose();
		}

		// Token: 0x04000763 RID: 1891
		private bool m_FirstFrame = true;

		// Token: 0x04000764 RID: 1892
		private MotionBlurComponent.FrameBlendingFilter m_FrameBlendingFilter;

		// Token: 0x04000765 RID: 1893
		private MotionBlurComponent.ReconstructionFilter m_ReconstructionFilter;

		// Token: 0x02000217 RID: 535
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x04000766 RID: 1894
			internal static readonly int _VelocityScale = Shader.PropertyToID("_VelocityScale");

			// Token: 0x04000767 RID: 1895
			internal static readonly int _MaxBlurRadius = Shader.PropertyToID("_MaxBlurRadius");

			// Token: 0x04000768 RID: 1896
			internal static readonly int _RcpMaxBlurRadius = Shader.PropertyToID("_RcpMaxBlurRadius");

			// Token: 0x04000769 RID: 1897
			internal static readonly int _VelocityTex = Shader.PropertyToID("_VelocityTex");

			// Token: 0x0400076A RID: 1898
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x0400076B RID: 1899
			internal static readonly int _Tile2RT = Shader.PropertyToID("_Tile2RT");

			// Token: 0x0400076C RID: 1900
			internal static readonly int _Tile4RT = Shader.PropertyToID("_Tile4RT");

			// Token: 0x0400076D RID: 1901
			internal static readonly int _Tile8RT = Shader.PropertyToID("_Tile8RT");

			// Token: 0x0400076E RID: 1902
			internal static readonly int _TileMaxOffs = Shader.PropertyToID("_TileMaxOffs");

			// Token: 0x0400076F RID: 1903
			internal static readonly int _TileMaxLoop = Shader.PropertyToID("_TileMaxLoop");

			// Token: 0x04000770 RID: 1904
			internal static readonly int _TileVRT = Shader.PropertyToID("_TileVRT");

			// Token: 0x04000771 RID: 1905
			internal static readonly int _NeighborMaxTex = Shader.PropertyToID("_NeighborMaxTex");

			// Token: 0x04000772 RID: 1906
			internal static readonly int _LoopCount = Shader.PropertyToID("_LoopCount");

			// Token: 0x04000773 RID: 1907
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");

			// Token: 0x04000774 RID: 1908
			internal static readonly int _History1LumaTex = Shader.PropertyToID("_History1LumaTex");

			// Token: 0x04000775 RID: 1909
			internal static readonly int _History2LumaTex = Shader.PropertyToID("_History2LumaTex");

			// Token: 0x04000776 RID: 1910
			internal static readonly int _History3LumaTex = Shader.PropertyToID("_History3LumaTex");

			// Token: 0x04000777 RID: 1911
			internal static readonly int _History4LumaTex = Shader.PropertyToID("_History4LumaTex");

			// Token: 0x04000778 RID: 1912
			internal static readonly int _History1ChromaTex = Shader.PropertyToID("_History1ChromaTex");

			// Token: 0x04000779 RID: 1913
			internal static readonly int _History2ChromaTex = Shader.PropertyToID("_History2ChromaTex");

			// Token: 0x0400077A RID: 1914
			internal static readonly int _History3ChromaTex = Shader.PropertyToID("_History3ChromaTex");

			// Token: 0x0400077B RID: 1915
			internal static readonly int _History4ChromaTex = Shader.PropertyToID("_History4ChromaTex");

			// Token: 0x0400077C RID: 1916
			internal static readonly int _History1Weight = Shader.PropertyToID("_History1Weight");

			// Token: 0x0400077D RID: 1917
			internal static readonly int _History2Weight = Shader.PropertyToID("_History2Weight");

			// Token: 0x0400077E RID: 1918
			internal static readonly int _History3Weight = Shader.PropertyToID("_History3Weight");

			// Token: 0x0400077F RID: 1919
			internal static readonly int _History4Weight = Shader.PropertyToID("_History4Weight");
		}

		// Token: 0x02000218 RID: 536
		[NullableContext(0)]
		private enum Pass
		{
			// Token: 0x04000781 RID: 1921
			VelocitySetup,
			// Token: 0x04000782 RID: 1922
			TileMax1,
			// Token: 0x04000783 RID: 1923
			TileMax2,
			// Token: 0x04000784 RID: 1924
			TileMaxV,
			// Token: 0x04000785 RID: 1925
			NeighborMax,
			// Token: 0x04000786 RID: 1926
			Reconstruction,
			// Token: 0x04000787 RID: 1927
			FrameCompression,
			// Token: 0x04000788 RID: 1928
			FrameBlendingChroma,
			// Token: 0x04000789 RID: 1929
			FrameBlendingRaw
		}

		// Token: 0x02000219 RID: 537
		[NullableContext(0)]
		public class ReconstructionFilter
		{
			// Token: 0x06000992 RID: 2450 RVA: 0x0000B691 File Offset: 0x00009891
			public ReconstructionFilter()
			{
				this.CheckTextureFormatSupport();
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x0000B6AE File Offset: 0x000098AE
			private void CheckTextureFormatSupport()
			{
				if (!SystemInfo.SupportsRenderTextureFormat(this.m_PackedRTFormat))
				{
					this.m_PackedRTFormat = RenderTextureFormat.ARGB32;
				}
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x0000B6C4 File Offset: 0x000098C4
			public bool IsSupported()
			{
				return SystemInfo.supportsMotionVectors;
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x00080B20 File Offset: 0x0007ED20
			[NullableContext(1)]
			public void ProcessImage(PostProcessingContext context, CommandBuffer cb, ref MotionBlurModel.Settings settings, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material)
			{
				int num = (int)(5f * (float)context.height / 100f);
				int num2 = ((num - 1) / 8 + 1) * 8;
				float num3 = settings.shutterAngle / 360f;
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._VelocityScale, num3);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._MaxBlurRadius, (float)num);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._RcpMaxBlurRadius, 1f / (float)num);
				int velocityTex = MotionBlurComponent.Uniforms._VelocityTex;
				cb.GetTemporaryRT(velocityTex, context.width, context.height, 0, FilterMode.Point, this.m_PackedRTFormat, RenderTextureReadWrite.Linear);
				cb.Blit(null, velocityTex, material, 0);
				int tile2RT = MotionBlurComponent.Uniforms._Tile2RT;
				cb.GetTemporaryRT(tile2RT, context.width / 2, context.height / 2, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, velocityTex);
				cb.Blit(velocityTex, tile2RT, material, 1);
				int tile4RT = MotionBlurComponent.Uniforms._Tile4RT;
				cb.GetTemporaryRT(tile4RT, context.width / 4, context.height / 4, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, tile2RT);
				cb.Blit(tile2RT, tile4RT, material, 2);
				cb.ReleaseTemporaryRT(tile2RT);
				int tile8RT = MotionBlurComponent.Uniforms._Tile8RT;
				cb.GetTemporaryRT(tile8RT, context.width / 8, context.height / 8, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, tile4RT);
				cb.Blit(tile4RT, tile8RT, material, 2);
				cb.ReleaseTemporaryRT(tile4RT);
				Vector2 vector = Vector2.one * ((float)num2 / 8f - 1f) * -0.5f;
				cb.SetGlobalVector(MotionBlurComponent.Uniforms._TileMaxOffs, vector);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._TileMaxLoop, (float)((int)((float)num2 / 8f)));
				int tileVRT = MotionBlurComponent.Uniforms._TileVRT;
				cb.GetTemporaryRT(tileVRT, context.width / num2, context.height / num2, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, tile8RT);
				cb.Blit(tile8RT, tileVRT, material, 3);
				cb.ReleaseTemporaryRT(tile8RT);
				int neighborMaxTex = MotionBlurComponent.Uniforms._NeighborMaxTex;
				int num4 = context.width / num2;
				int num5 = context.height / num2;
				cb.GetTemporaryRT(neighborMaxTex, num4, num5, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, tileVRT);
				cb.Blit(tileVRT, neighborMaxTex, material, 4);
				cb.ReleaseTemporaryRT(tileVRT);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._LoopCount, (float)Mathf.Clamp(settings.sampleCount / 2, 1, 64));
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, source);
				cb.Blit(source, destination, material, 5);
				cb.ReleaseTemporaryRT(velocityTex);
				cb.ReleaseTemporaryRT(neighborMaxTex);
			}

			// Token: 0x0400078A RID: 1930
			private RenderTextureFormat m_PackedRTFormat = RenderTextureFormat.ARGB2101010;

			// Token: 0x0400078B RID: 1931
			private readonly RenderTextureFormat m_VectorRTFormat = RenderTextureFormat.RGHalf;
		}

		// Token: 0x0200021A RID: 538
		[Nullable(0)]
		public class FrameBlendingFilter
		{
			// Token: 0x06000996 RID: 2454 RVA: 0x0000B6CB File Offset: 0x000098CB
			public FrameBlendingFilter()
			{
				this.m_UseCompression = MotionBlurComponent.FrameBlendingFilter.CheckSupportCompression();
				this.m_RawTextureFormat = MotionBlurComponent.FrameBlendingFilter.GetPreferredRenderTextureFormat();
				this.m_FrameList = new MotionBlurComponent.FrameBlendingFilter.Frame[4];
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x00080E04 File Offset: 0x0007F004
			public void Dispose()
			{
				foreach (MotionBlurComponent.FrameBlendingFilter.Frame frame in this.m_FrameList)
				{
					frame.Release();
				}
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x00080E38 File Offset: 0x0007F038
			public void PushFrame(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, Material material)
			{
				int frameCount = Time.frameCount;
				if (frameCount != this.m_LastFrameCount)
				{
					int num = frameCount % this.m_FrameList.Length;
					if (this.m_UseCompression)
					{
						this.m_FrameList[num].MakeRecord(cb, source, width, height, material);
					}
					else
					{
						this.m_FrameList[num].MakeRecordRaw(cb, source, width, height, this.m_RawTextureFormat);
					}
					this.m_LastFrameCount = frameCount;
				}
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x00080EA4 File Offset: 0x0007F0A4
			public void BlendFrames(CommandBuffer cb, float strength, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material)
			{
				float time = Time.time;
				MotionBlurComponent.FrameBlendingFilter.Frame frameRelative = this.GetFrameRelative(-1);
				MotionBlurComponent.FrameBlendingFilter.Frame frameRelative2 = this.GetFrameRelative(-2);
				MotionBlurComponent.FrameBlendingFilter.Frame frameRelative3 = this.GetFrameRelative(-3);
				MotionBlurComponent.FrameBlendingFilter.Frame frameRelative4 = this.GetFrameRelative(-4);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History1LumaTex, frameRelative.lumaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History2LumaTex, frameRelative2.lumaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History3LumaTex, frameRelative3.lumaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History4LumaTex, frameRelative4.lumaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History1ChromaTex, frameRelative.chromaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History2ChromaTex, frameRelative2.chromaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History3ChromaTex, frameRelative3.chromaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History4ChromaTex, frameRelative4.chromaTexture);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._History1Weight, frameRelative.CalculateWeight(strength, time));
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._History2Weight, frameRelative2.CalculateWeight(strength, time));
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._History3Weight, frameRelative3.CalculateWeight(strength, time));
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._History4Weight, frameRelative4.CalculateWeight(strength, time));
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, source);
				cb.Blit(source, destination, material, (!this.m_UseCompression) ? 8 : 7);
			}

			// Token: 0x0600099A RID: 2458 RVA: 0x0000B6F5 File Offset: 0x000098F5
			private static bool CheckSupportCompression()
			{
				return SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8) && SystemInfo.supportedRenderTargetCount > 1;
			}

			// Token: 0x0600099B RID: 2459 RVA: 0x00081000 File Offset: 0x0007F200
			private static RenderTextureFormat GetPreferredRenderTextureFormat()
			{
				foreach (RenderTextureFormat renderTextureFormat in new RenderTextureFormat[]
				{
					RenderTextureFormat.RGB565,
					RenderTextureFormat.ARGB1555,
					RenderTextureFormat.ARGB4444
				})
				{
					if (SystemInfo.SupportsRenderTextureFormat(renderTextureFormat))
					{
						return renderTextureFormat;
					}
				}
				return RenderTextureFormat.Default;
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x00081040 File Offset: 0x0007F240
			private MotionBlurComponent.FrameBlendingFilter.Frame GetFrameRelative(int offset)
			{
				int num = (Time.frameCount + this.m_FrameList.Length + offset) % this.m_FrameList.Length;
				return this.m_FrameList[num];
			}

			// Token: 0x0400078C RID: 1932
			private readonly MotionBlurComponent.FrameBlendingFilter.Frame[] m_FrameList;

			// Token: 0x0400078D RID: 1933
			private int m_LastFrameCount;

			// Token: 0x0400078E RID: 1934
			private readonly RenderTextureFormat m_RawTextureFormat;

			// Token: 0x0400078F RID: 1935
			private readonly bool m_UseCompression;

			// Token: 0x0200021B RID: 539
			[Nullable(0)]
			private struct Frame
			{
				// Token: 0x0600099D RID: 2461 RVA: 0x00081074 File Offset: 0x0007F274
				public float CalculateWeight(float strength, float currentTime)
				{
					if (Mathf.Approximately(this.m_Time, 0f))
					{
						return 0f;
					}
					float num = Mathf.Lerp(80f, 16f, strength);
					return Mathf.Exp((this.m_Time - currentTime) * num);
				}

				// Token: 0x0600099E RID: 2462 RVA: 0x000810BC File Offset: 0x0007F2BC
				public void Release()
				{
					if (this.lumaTexture != null)
					{
						RenderTexture.ReleaseTemporary(this.lumaTexture);
					}
					if (this.chromaTexture != null)
					{
						RenderTexture.ReleaseTemporary(this.chromaTexture);
					}
					this.lumaTexture = null;
					this.chromaTexture = null;
				}

				// Token: 0x0600099F RID: 2463 RVA: 0x0008110C File Offset: 0x0007F30C
				public void MakeRecord(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, Material material)
				{
					this.Release();
					this.lumaTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear);
					this.chromaTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear);
					this.lumaTexture.filterMode = FilterMode.Point;
					this.chromaTexture.filterMode = FilterMode.Point;
					if (this.m_MRT == null)
					{
						this.m_MRT = new RenderTargetIdentifier[2];
					}
					this.m_MRT[0] = this.lumaTexture;
					this.m_MRT[1] = this.chromaTexture;
					cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, source);
					cb.SetRenderTarget(this.m_MRT, this.lumaTexture);
					cb.DrawMesh(GraphicsUtils.quad, Matrix4x4.identity, material, 0, 6);
					this.m_Time = Time.time;
				}

				// Token: 0x060009A0 RID: 2464 RVA: 0x000811E0 File Offset: 0x0007F3E0
				public void MakeRecordRaw(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, RenderTextureFormat format)
				{
					this.Release();
					this.lumaTexture = RenderTexture.GetTemporary(width, height, 0, format);
					this.lumaTexture.filterMode = FilterMode.Point;
					cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, source);
					cb.Blit(source, this.lumaTexture);
					this.m_Time = Time.time;
				}

				// Token: 0x04000790 RID: 1936
				public RenderTexture lumaTexture;

				// Token: 0x04000791 RID: 1937
				public RenderTexture chromaTexture;

				// Token: 0x04000792 RID: 1938
				private float m_Time;

				// Token: 0x04000793 RID: 1939
				private RenderTargetIdentifier[] m_MRT;
			}
		}
	}
}
