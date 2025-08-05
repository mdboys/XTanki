using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001EF RID: 495
	[Serializable]
	public class ColorGradingModel : PostProcessingModel
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0000B240 File Offset: 0x00009440
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0000B248 File Offset: 0x00009448
		public ColorGradingModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
				this.OnValidate();
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0000B257 File Offset: 0x00009457
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0000B25F File Offset: 0x0000945F
		public bool isDirty { get; internal set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0000B268 File Offset: 0x00009468
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0000B270 File Offset: 0x00009470
		[Nullable(1)]
		public RenderTexture bakedLut
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			internal set;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0000B279 File Offset: 0x00009479
		public override void Reset()
		{
			this.m_Settings = ColorGradingModel.Settings.defaultSettings;
			this.OnValidate();
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0000B28C File Offset: 0x0000948C
		public override void OnValidate()
		{
			this.isDirty = true;
		}

		// Token: 0x040006D4 RID: 1748
		[SerializeField]
		private ColorGradingModel.Settings m_Settings = ColorGradingModel.Settings.defaultSettings;

		// Token: 0x020001F0 RID: 496
		public enum ColorWheelMode
		{
			// Token: 0x040006D8 RID: 1752
			Linear,
			// Token: 0x040006D9 RID: 1753
			Log
		}

		// Token: 0x020001F1 RID: 497
		public enum Tonemapper
		{
			// Token: 0x040006DB RID: 1755
			None,
			// Token: 0x040006DC RID: 1756
			ACES,
			// Token: 0x040006DD RID: 1757
			Neutral
		}

		// Token: 0x020001F2 RID: 498
		[Serializable]
		public struct TonemappingSettings
		{
			// Token: 0x1700016B RID: 363
			// (get) Token: 0x0600092F RID: 2351 RVA: 0x0007EE28 File Offset: 0x0007D028
			public static ColorGradingModel.TonemappingSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.TonemappingSettings
					{
						tonemapper = ColorGradingModel.Tonemapper.Neutral,
						neutralBlackIn = 0.02f,
						neutralWhiteIn = 10f,
						neutralBlackOut = 0f,
						neutralWhiteOut = 10f,
						neutralWhiteLevel = 5.3f,
						neutralWhiteClip = 10f
					};
				}
			}

			// Token: 0x040006DE RID: 1758
			[Tooltip("Tonemapping algorithm to use at the end of the color grading process. Use \"Neutral\" if you need a customizable tonemapper or \"Filmic\" to give a standard filmic look to your scenes.")]
			public ColorGradingModel.Tonemapper tonemapper;

			// Token: 0x040006DF RID: 1759
			[Range(-0.1f, 0.1f)]
			public float neutralBlackIn;

			// Token: 0x040006E0 RID: 1760
			[Range(1f, 20f)]
			public float neutralWhiteIn;

			// Token: 0x040006E1 RID: 1761
			[Range(-0.09f, 0.1f)]
			public float neutralBlackOut;

			// Token: 0x040006E2 RID: 1762
			[Range(1f, 19f)]
			public float neutralWhiteOut;

			// Token: 0x040006E3 RID: 1763
			[Range(0.1f, 20f)]
			public float neutralWhiteLevel;

			// Token: 0x040006E4 RID: 1764
			[Range(1f, 10f)]
			public float neutralWhiteClip;
		}

		// Token: 0x020001F3 RID: 499
		[Serializable]
		public struct BasicSettings
		{
			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000930 RID: 2352 RVA: 0x0007EE90 File Offset: 0x0007D090
			public static ColorGradingModel.BasicSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.BasicSettings
					{
						postExposure = 0f,
						temperature = 0f,
						tint = 0f,
						hueShift = 0f,
						saturation = 1f,
						contrast = 1f
					};
				}
			}

			// Token: 0x040006E5 RID: 1765
			[Tooltip("Adjusts the overall exposure of the scene in EV units. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
			public float postExposure;

			// Token: 0x040006E6 RID: 1766
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to a custom color temperature.")]
			public float temperature;

			// Token: 0x040006E7 RID: 1767
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
			public float tint;

			// Token: 0x040006E8 RID: 1768
			[Range(-180f, 180f)]
			[Tooltip("Shift the hue of all colors.")]
			public float hueShift;

			// Token: 0x040006E9 RID: 1769
			[Range(0f, 2f)]
			[Tooltip("Pushes the intensity of all colors.")]
			public float saturation;

			// Token: 0x040006EA RID: 1770
			[Range(0f, 2f)]
			[Tooltip("Expands or shrinks the overall range of tonal values.")]
			public float contrast;
		}

		// Token: 0x020001F4 RID: 500
		[Serializable]
		public struct ChannelMixerSettings
		{
			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000931 RID: 2353 RVA: 0x0007EEF0 File Offset: 0x0007D0F0
			public static ColorGradingModel.ChannelMixerSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ChannelMixerSettings
					{
						red = new Vector3(1f, 0f, 0f),
						green = new Vector3(0f, 1f, 0f),
						blue = new Vector3(0f, 0f, 1f),
						currentEditingChannel = 0
					};
				}
			}

			// Token: 0x040006EB RID: 1771
			public Vector3 red;

			// Token: 0x040006EC RID: 1772
			public Vector3 green;

			// Token: 0x040006ED RID: 1773
			public Vector3 blue;

			// Token: 0x040006EE RID: 1774
			[HideInInspector]
			public int currentEditingChannel;
		}

		// Token: 0x020001F5 RID: 501
		[Serializable]
		public struct LogWheelsSettings
		{
			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000932 RID: 2354 RVA: 0x0007EF60 File Offset: 0x0007D160
			public static ColorGradingModel.LogWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LogWheelsSettings
					{
						slope = Color.clear,
						power = Color.clear,
						offset = Color.clear
					};
				}
			}

			// Token: 0x040006EF RID: 1775
			[Trackball("GetSlopeValue")]
			public Color slope;

			// Token: 0x040006F0 RID: 1776
			[Trackball("GetPowerValue")]
			public Color power;

			// Token: 0x040006F1 RID: 1777
			[Trackball("GetOffsetValue")]
			public Color offset;
		}

		// Token: 0x020001F6 RID: 502
		[Serializable]
		public struct LinearWheelsSettings
		{
			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000933 RID: 2355 RVA: 0x0007EF9C File Offset: 0x0007D19C
			public static ColorGradingModel.LinearWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LinearWheelsSettings
					{
						lift = Color.clear,
						gamma = Color.clear,
						gain = Color.clear
					};
				}
			}

			// Token: 0x040006F2 RID: 1778
			[Trackball("GetLiftValue")]
			public Color lift;

			// Token: 0x040006F3 RID: 1779
			[Trackball("GetGammaValue")]
			public Color gamma;

			// Token: 0x040006F4 RID: 1780
			[Trackball("GetGainValue")]
			public Color gain;
		}

		// Token: 0x020001F7 RID: 503
		[Serializable]
		public struct ColorWheelsSettings
		{
			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000934 RID: 2356 RVA: 0x0007EFD8 File Offset: 0x0007D1D8
			public static ColorGradingModel.ColorWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ColorWheelsSettings
					{
						mode = ColorGradingModel.ColorWheelMode.Log,
						log = ColorGradingModel.LogWheelsSettings.defaultSettings,
						linear = ColorGradingModel.LinearWheelsSettings.defaultSettings
					};
				}
			}

			// Token: 0x040006F5 RID: 1781
			public ColorGradingModel.ColorWheelMode mode;

			// Token: 0x040006F6 RID: 1782
			[TrackballGroup]
			public ColorGradingModel.LogWheelsSettings log;

			// Token: 0x040006F7 RID: 1783
			[TrackballGroup]
			public ColorGradingModel.LinearWheelsSettings linear;
		}

		// Token: 0x020001F8 RID: 504
		[NullableContext(1)]
		[Nullable(0)]
		[Serializable]
		public struct CurvesSettings
		{
			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000935 RID: 2357 RVA: 0x0007F010 File Offset: 0x0007D210
			public static ColorGradingModel.CurvesSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.CurvesSettings
					{
						master = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						red = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						green = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						blue = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						hueVShue = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						hueVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						satVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						lumVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						e_CurrentEditingCurve = 0,
						e_CurveY = true,
						e_CurveR = false,
						e_CurveG = false,
						e_CurveB = false
					};
				}
			}

			// Token: 0x040006F8 RID: 1784
			public ColorGradingCurve master;

			// Token: 0x040006F9 RID: 1785
			public ColorGradingCurve red;

			// Token: 0x040006FA RID: 1786
			public ColorGradingCurve green;

			// Token: 0x040006FB RID: 1787
			public ColorGradingCurve blue;

			// Token: 0x040006FC RID: 1788
			public ColorGradingCurve hueVShue;

			// Token: 0x040006FD RID: 1789
			public ColorGradingCurve hueVSsat;

			// Token: 0x040006FE RID: 1790
			public ColorGradingCurve satVSsat;

			// Token: 0x040006FF RID: 1791
			public ColorGradingCurve lumVSsat;

			// Token: 0x04000700 RID: 1792
			[HideInInspector]
			public int e_CurrentEditingCurve;

			// Token: 0x04000701 RID: 1793
			[HideInInspector]
			public bool e_CurveY;

			// Token: 0x04000702 RID: 1794
			[HideInInspector]
			public bool e_CurveR;

			// Token: 0x04000703 RID: 1795
			[HideInInspector]
			public bool e_CurveG;

			// Token: 0x04000704 RID: 1796
			[HideInInspector]
			public bool e_CurveB;
		}

		// Token: 0x020001F9 RID: 505
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000936 RID: 2358 RVA: 0x0007F298 File Offset: 0x0007D498
			public static ColorGradingModel.Settings defaultSettings
			{
				get
				{
					return new ColorGradingModel.Settings
					{
						tonemapping = ColorGradingModel.TonemappingSettings.defaultSettings,
						basic = ColorGradingModel.BasicSettings.defaultSettings,
						channelMixer = ColorGradingModel.ChannelMixerSettings.defaultSettings,
						colorWheels = ColorGradingModel.ColorWheelsSettings.defaultSettings,
						curves = ColorGradingModel.CurvesSettings.defaultSettings
					};
				}
			}

			// Token: 0x04000705 RID: 1797
			public ColorGradingModel.TonemappingSettings tonemapping;

			// Token: 0x04000706 RID: 1798
			public ColorGradingModel.BasicSettings basic;

			// Token: 0x04000707 RID: 1799
			public ColorGradingModel.ChannelMixerSettings channelMixer;

			// Token: 0x04000708 RID: 1800
			public ColorGradingModel.ColorWheelsSettings colorWheels;

			// Token: 0x04000709 RID: 1801
			public ColorGradingModel.CurvesSettings curves;
		}
	}
}
