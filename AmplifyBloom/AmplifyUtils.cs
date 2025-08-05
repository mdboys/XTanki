using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CDE RID: 11486
	[NullableContext(1)]
	[Nullable(0)]
	public class AmplifyUtils
	{
		// Token: 0x0600A0B8 RID: 41144 RVA: 0x0015FF64 File Offset: 0x0015E164
		public static void InitializeIds()
		{
			AmplifyUtils.IsInitialized = true;
			AmplifyUtils.MaskTextureId = Shader.PropertyToID("_MaskTex");
			AmplifyUtils.MipResultsRTS = new int[]
			{
				Shader.PropertyToID("_MipResultsRTS0"),
				Shader.PropertyToID("_MipResultsRTS1"),
				Shader.PropertyToID("_MipResultsRTS2"),
				Shader.PropertyToID("_MipResultsRTS3"),
				Shader.PropertyToID("_MipResultsRTS4"),
				Shader.PropertyToID("_MipResultsRTS5")
			};
			AmplifyUtils.AnamorphicRTS = new int[]
			{
				Shader.PropertyToID("_AnamorphicRTS0"),
				Shader.PropertyToID("_AnamorphicRTS1"),
				Shader.PropertyToID("_AnamorphicRTS2"),
				Shader.PropertyToID("_AnamorphicRTS3"),
				Shader.PropertyToID("_AnamorphicRTS4"),
				Shader.PropertyToID("_AnamorphicRTS5"),
				Shader.PropertyToID("_AnamorphicRTS6"),
				Shader.PropertyToID("_AnamorphicRTS7")
			};
			AmplifyUtils.AnamorphicGlareWeightsMatStr = new int[]
			{
				Shader.PropertyToID("_AnamorphicGlareWeightsMat0"),
				Shader.PropertyToID("_AnamorphicGlareWeightsMat1"),
				Shader.PropertyToID("_AnamorphicGlareWeightsMat2"),
				Shader.PropertyToID("_AnamorphicGlareWeightsMat3")
			};
			AmplifyUtils.AnamorphicGlareOffsetsMatStr = new int[]
			{
				Shader.PropertyToID("_AnamorphicGlareOffsetsMat0"),
				Shader.PropertyToID("_AnamorphicGlareOffsetsMat1"),
				Shader.PropertyToID("_AnamorphicGlareOffsetsMat2"),
				Shader.PropertyToID("_AnamorphicGlareOffsetsMat3")
			};
			AmplifyUtils.AnamorphicGlareWeightsStr = new int[]
			{
				Shader.PropertyToID("_AnamorphicGlareWeights0"),
				Shader.PropertyToID("_AnamorphicGlareWeights1"),
				Shader.PropertyToID("_AnamorphicGlareWeights2"),
				Shader.PropertyToID("_AnamorphicGlareWeights3"),
				Shader.PropertyToID("_AnamorphicGlareWeights4"),
				Shader.PropertyToID("_AnamorphicGlareWeights5"),
				Shader.PropertyToID("_AnamorphicGlareWeights6"),
				Shader.PropertyToID("_AnamorphicGlareWeights7"),
				Shader.PropertyToID("_AnamorphicGlareWeights8"),
				Shader.PropertyToID("_AnamorphicGlareWeights9"),
				Shader.PropertyToID("_AnamorphicGlareWeights10"),
				Shader.PropertyToID("_AnamorphicGlareWeights11"),
				Shader.PropertyToID("_AnamorphicGlareWeights12"),
				Shader.PropertyToID("_AnamorphicGlareWeights13"),
				Shader.PropertyToID("_AnamorphicGlareWeights14"),
				Shader.PropertyToID("_AnamorphicGlareWeights15")
			};
			AmplifyUtils.UpscaleWeightsStr = new int[]
			{
				Shader.PropertyToID("_UpscaleWeights0"),
				Shader.PropertyToID("_UpscaleWeights1"),
				Shader.PropertyToID("_UpscaleWeights2"),
				Shader.PropertyToID("_UpscaleWeights3"),
				Shader.PropertyToID("_UpscaleWeights4"),
				Shader.PropertyToID("_UpscaleWeights5"),
				Shader.PropertyToID("_UpscaleWeights6"),
				Shader.PropertyToID("_UpscaleWeights7")
			};
			AmplifyUtils.LensDirtWeightsStr = new int[]
			{
				Shader.PropertyToID("_LensDirtWeights0"),
				Shader.PropertyToID("_LensDirtWeights1"),
				Shader.PropertyToID("_LensDirtWeights2"),
				Shader.PropertyToID("_LensDirtWeights3"),
				Shader.PropertyToID("_LensDirtWeights4"),
				Shader.PropertyToID("_LensDirtWeights5"),
				Shader.PropertyToID("_LensDirtWeights6"),
				Shader.PropertyToID("_LensDirtWeights7")
			};
			AmplifyUtils.LensStarburstWeightsStr = new int[]
			{
				Shader.PropertyToID("_LensStarburstWeights0"),
				Shader.PropertyToID("_LensStarburstWeights1"),
				Shader.PropertyToID("_LensStarburstWeights2"),
				Shader.PropertyToID("_LensStarburstWeights3"),
				Shader.PropertyToID("_LensStarburstWeights4"),
				Shader.PropertyToID("_LensStarburstWeights5"),
				Shader.PropertyToID("_LensStarburstWeights6"),
				Shader.PropertyToID("_LensStarburstWeights7")
			};
			AmplifyUtils.BloomRangeId = Shader.PropertyToID("_BloomRange");
			AmplifyUtils.LensDirtStrengthId = Shader.PropertyToID("_LensDirtStrength");
			AmplifyUtils.BloomParamsId = Shader.PropertyToID("_BloomParams");
			AmplifyUtils.TempFilterValueId = Shader.PropertyToID("_TempFilterValue");
			AmplifyUtils.LensFlareStarMatrixId = Shader.PropertyToID("_LensFlareStarMatrix");
			AmplifyUtils.LensFlareStarburstStrengthId = Shader.PropertyToID("_LensFlareStarburstStrength");
			AmplifyUtils.LensFlareGhostsParamsId = Shader.PropertyToID("_LensFlareGhostsParams");
			AmplifyUtils.LensFlareLUTId = Shader.PropertyToID("_LensFlareLUT");
			AmplifyUtils.LensFlareHaloParamsId = Shader.PropertyToID("_LensFlareHaloParams");
			AmplifyUtils.LensFlareGhostChrDistortionId = Shader.PropertyToID("_LensFlareGhostChrDistortion");
			AmplifyUtils.LensFlareHaloChrDistortionId = Shader.PropertyToID("_LensFlareHaloChrDistortion");
			AmplifyUtils.BokehParamsId = Shader.PropertyToID("_BokehParams");
			AmplifyUtils.BlurRadiusId = Shader.PropertyToID("_BlurRadius");
			AmplifyUtils.LensStarburstRTId = Shader.PropertyToID("_LensStarburst");
			AmplifyUtils.LensDirtRTId = Shader.PropertyToID("_LensDirt");
			AmplifyUtils.LensFlareRTId = Shader.PropertyToID("_LensFlare");
			AmplifyUtils.LensGlareRTId = Shader.PropertyToID("_LensGlare");
			AmplifyUtils.SourceContributionId = Shader.PropertyToID("_SourceContribution");
			AmplifyUtils.UpscaleContributionId = Shader.PropertyToID("_UpscaleContribution");
		}

		// Token: 0x0600A0B9 RID: 41145 RVA: 0x0016042C File Offset: 0x0015E62C
		public static void DebugLog(string value, LogType type)
		{
			switch (type)
			{
			case LogType.Normal:
				Debug.Log(AmplifyUtils.DebugStr + value);
				return;
			case LogType.Warning:
				Debug.LogWarning(AmplifyUtils.DebugStr + value);
				return;
			case LogType.Error:
				Debug.LogError(AmplifyUtils.DebugStr + value);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A0BA RID: 41146 RVA: 0x00160480 File Offset: 0x0015E680
		public static RenderTexture GetTempRenderTarget(int width, int height)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, AmplifyUtils.CurrentRTFormat, AmplifyUtils.CurrentReadWriteMode);
			temporary.filterMode = AmplifyUtils.CurrentFilterMode;
			temporary.wrapMode = AmplifyUtils.CurrentWrapMode;
			AmplifyUtils._allocatedRT.Add(temporary);
			return temporary;
		}

		// Token: 0x0600A0BB RID: 41147 RVA: 0x0005D687 File Offset: 0x0005B887
		public static void ReleaseTempRenderTarget(RenderTexture renderTarget)
		{
			if (renderTarget != null && AmplifyUtils._allocatedRT.Remove(renderTarget))
			{
				renderTarget.DiscardContents();
				RenderTexture.ReleaseTemporary(renderTarget);
			}
		}

		// Token: 0x0600A0BC RID: 41148 RVA: 0x001604C4 File Offset: 0x0015E6C4
		public static void ReleaseAllRT()
		{
			for (int i = 0; i < AmplifyUtils._allocatedRT.Count; i++)
			{
				AmplifyUtils._allocatedRT[i].DiscardContents();
				RenderTexture.ReleaseTemporary(AmplifyUtils._allocatedRT[i]);
			}
			AmplifyUtils._allocatedRT.Clear();
		}

		// Token: 0x0600A0BD RID: 41149 RVA: 0x0005D6AB File Offset: 0x0005B8AB
		public static void EnsureKeywordEnabled(Material mat, string keyword, bool state)
		{
			if (mat != null)
			{
				if (state && !mat.IsKeywordEnabled(keyword))
				{
					mat.EnableKeyword(keyword);
					return;
				}
				if (!state && mat.IsKeywordEnabled(keyword))
				{
					mat.DisableKeyword(keyword);
				}
			}
		}

		// Token: 0x040067E2 RID: 26594
		public static int MaskTextureId;

		// Token: 0x040067E3 RID: 26595
		public static int BlurRadiusId;

		// Token: 0x040067E4 RID: 26596
		public static string HighPrecisionKeyword = "AB_HIGH_PRECISION";

		// Token: 0x040067E5 RID: 26597
		public static string ShaderModeTag = "Mode";

		// Token: 0x040067E6 RID: 26598
		public static string ShaderModeValue = "Full";

		// Token: 0x040067E7 RID: 26599
		public static string DebugStr = "[AmplifyBloom] ";

		// Token: 0x040067E8 RID: 26600
		public static int UpscaleContributionId;

		// Token: 0x040067E9 RID: 26601
		public static int SourceContributionId;

		// Token: 0x040067EA RID: 26602
		public static int LensStarburstRTId;

		// Token: 0x040067EB RID: 26603
		public static int LensDirtRTId;

		// Token: 0x040067EC RID: 26604
		public static int LensFlareRTId;

		// Token: 0x040067ED RID: 26605
		public static int LensGlareRTId;

		// Token: 0x040067EE RID: 26606
		public static int[] MipResultsRTS;

		// Token: 0x040067EF RID: 26607
		public static int[] AnamorphicRTS;

		// Token: 0x040067F0 RID: 26608
		public static int[] AnamorphicGlareWeightsMatStr;

		// Token: 0x040067F1 RID: 26609
		public static int[] AnamorphicGlareOffsetsMatStr;

		// Token: 0x040067F2 RID: 26610
		public static int[] AnamorphicGlareWeightsStr;

		// Token: 0x040067F3 RID: 26611
		public static int[] UpscaleWeightsStr;

		// Token: 0x040067F4 RID: 26612
		public static int[] LensDirtWeightsStr;

		// Token: 0x040067F5 RID: 26613
		public static int[] LensStarburstWeightsStr;

		// Token: 0x040067F6 RID: 26614
		public static int BloomRangeId;

		// Token: 0x040067F7 RID: 26615
		public static int LensDirtStrengthId;

		// Token: 0x040067F8 RID: 26616
		public static int BloomParamsId;

		// Token: 0x040067F9 RID: 26617
		public static int TempFilterValueId;

		// Token: 0x040067FA RID: 26618
		public static int LensFlareStarMatrixId;

		// Token: 0x040067FB RID: 26619
		public static int LensFlareStarburstStrengthId;

		// Token: 0x040067FC RID: 26620
		public static int LensFlareGhostsParamsId;

		// Token: 0x040067FD RID: 26621
		public static int LensFlareLUTId;

		// Token: 0x040067FE RID: 26622
		public static int LensFlareHaloParamsId;

		// Token: 0x040067FF RID: 26623
		public static int LensFlareGhostChrDistortionId;

		// Token: 0x04006800 RID: 26624
		public static int LensFlareHaloChrDistortionId;

		// Token: 0x04006801 RID: 26625
		public static int BokehParamsId = -1;

		// Token: 0x04006802 RID: 26626
		public static RenderTextureFormat CurrentRTFormat = RenderTextureFormat.DefaultHDR;

		// Token: 0x04006803 RID: 26627
		public static FilterMode CurrentFilterMode = FilterMode.Bilinear;

		// Token: 0x04006804 RID: 26628
		public static TextureWrapMode CurrentWrapMode = TextureWrapMode.Clamp;

		// Token: 0x04006805 RID: 26629
		public static RenderTextureReadWrite CurrentReadWriteMode = RenderTextureReadWrite.sRGB;

		// Token: 0x04006806 RID: 26630
		public static bool IsInitialized;

		// Token: 0x04006807 RID: 26631
		private static readonly List<RenderTexture> _allocatedRT = new List<RenderTexture>();
	}
}
