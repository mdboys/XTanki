using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001D1 RID: 465
	[Serializable]
	public class AntialiasingModel : PostProcessingModel
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0000AEC3 File Offset: 0x000090C3
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0000AECB File Offset: 0x000090CB
		public AntialiasingModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0000AED4 File Offset: 0x000090D4
		public override void Reset()
		{
			this.m_Settings = AntialiasingModel.Settings.defaultSettings;
		}

		// Token: 0x04000656 RID: 1622
		[SerializeField]
		private AntialiasingModel.Settings m_Settings = AntialiasingModel.Settings.defaultSettings;

		// Token: 0x020001D2 RID: 466
		public enum FxaaPreset
		{
			// Token: 0x04000658 RID: 1624
			ExtremePerformance,
			// Token: 0x04000659 RID: 1625
			Performance,
			// Token: 0x0400065A RID: 1626
			Default,
			// Token: 0x0400065B RID: 1627
			Quality,
			// Token: 0x0400065C RID: 1628
			ExtremeQuality
		}

		// Token: 0x020001D3 RID: 467
		public enum Method
		{
			// Token: 0x0400065E RID: 1630
			Fxaa,
			// Token: 0x0400065F RID: 1631
			Taa
		}

		// Token: 0x020001D4 RID: 468
		[Serializable]
		public struct FxaaQualitySettings
		{
			// Token: 0x04000660 RID: 1632
			[Tooltip("The amount of desired sub-pixel aliasing removal. Effects the sharpeness of the output.")]
			[Range(0f, 1f)]
			public float subpixelAliasingRemovalAmount;

			// Token: 0x04000661 RID: 1633
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.063f, 0.333f)]
			public float edgeDetectionThreshold;

			// Token: 0x04000662 RID: 1634
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0f, 0.0833f)]
			public float minimumRequiredLuminance;

			// Token: 0x04000663 RID: 1635
			[Nullable(1)]
			public static AntialiasingModel.FxaaQualitySettings[] presets = new AntialiasingModel.FxaaQualitySettings[]
			{
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0f,
					edgeDetectionThreshold = 0.333f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.25f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.75f,
					edgeDetectionThreshold = 0.166f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.0625f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.063f,
					minimumRequiredLuminance = 0.0312f
				}
			};
		}

		// Token: 0x020001D5 RID: 469
		[Serializable]
		public struct FxaaConsoleSettings
		{
			// Token: 0x04000664 RID: 1636
			[Tooltip("The amount of spread applied to the sampling coordinates while sampling for subpixel information.")]
			[Range(0.33f, 0.5f)]
			public float subpixelSpreadAmount;

			// Token: 0x04000665 RID: 1637
			[Tooltip("This value dictates how sharp the edges in the image are kept; a higher value implies sharper edges.")]
			[Range(2f, 8f)]
			public float edgeSharpnessAmount;

			// Token: 0x04000666 RID: 1638
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.125f, 0.25f)]
			public float edgeDetectionThreshold;

			// Token: 0x04000667 RID: 1639
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0.04f, 0.06f)]
			public float minimumRequiredLuminance;

			// Token: 0x04000668 RID: 1640
			[Nullable(1)]
			public static AntialiasingModel.FxaaConsoleSettings[] presets = new AntialiasingModel.FxaaConsoleSettings[]
			{
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.05f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 4f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 2f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				}
			};
		}

		// Token: 0x020001D6 RID: 470
		[Serializable]
		public struct FxaaSettings
		{
			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0007D174 File Offset: 0x0007B374
			public static AntialiasingModel.FxaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.FxaaSettings
					{
						preset = AntialiasingModel.FxaaPreset.Default
					};
				}
			}

			// Token: 0x04000669 RID: 1641
			public AntialiasingModel.FxaaPreset preset;
		}

		// Token: 0x020001D7 RID: 471
		[Serializable]
		public struct TaaSettings
		{
			// Token: 0x17000153 RID: 339
			// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0007D194 File Offset: 0x0007B394
			public static AntialiasingModel.TaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.TaaSettings
					{
						jitterSpread = 0.75f,
						sharpen = 0.3f,
						stationaryBlending = 0.95f,
						motionBlending = 0.85f
					};
				}
			}

			// Token: 0x0400066A RID: 1642
			[Tooltip("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable but blurrier output.")]
			[Range(0.1f, 1f)]
			public float jitterSpread;

			// Token: 0x0400066B RID: 1643
			[Tooltip("Controls the amount of sharpening applied to the color buffer.")]
			[Range(0f, 3f)]
			public float sharpen;

			// Token: 0x0400066C RID: 1644
			[Tooltip("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float stationaryBlending;

			// Token: 0x0400066D RID: 1645
			[Tooltip("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float motionBlending;
		}

		// Token: 0x020001D8 RID: 472
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0007D1DC File Offset: 0x0007B3DC
			public static AntialiasingModel.Settings defaultSettings
			{
				get
				{
					return new AntialiasingModel.Settings
					{
						method = AntialiasingModel.Method.Fxaa,
						fxaaSettings = AntialiasingModel.FxaaSettings.defaultSettings,
						taaSettings = AntialiasingModel.TaaSettings.defaultSettings
					};
				}
			}

			// Token: 0x0400066E RID: 1646
			public AntialiasingModel.Method method;

			// Token: 0x0400066F RID: 1647
			public AntialiasingModel.FxaaSettings fxaaSettings;

			// Token: 0x04000670 RID: 1648
			public AntialiasingModel.TaaSettings taaSettings;
		}
	}
}
