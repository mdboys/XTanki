using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001E3 RID: 483
	[Serializable]
	public class BuiltinDebugViewsModel : PostProcessingModel
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0000B018 File Offset: 0x00009218
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x0000B020 File Offset: 0x00009220
		public BuiltinDebugViewsModel.Settings settings
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0000B029 File Offset: 0x00009229
		public bool willInterrupt
		{
			get
			{
				return !this.IsModeActive(BuiltinDebugViewsModel.Mode.None) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.EyeAdaptation) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.PreGradingLog) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.LogLut) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.UserLut);
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000B05C File Offset: 0x0000925C
		public override void Reset()
		{
			this.settings = BuiltinDebugViewsModel.Settings.defaultSettings;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000B069 File Offset: 0x00009269
		public bool IsModeActive(BuiltinDebugViewsModel.Mode mode)
		{
			return this.m_Settings.mode == mode;
		}

		// Token: 0x0400069A RID: 1690
		[SerializeField]
		private BuiltinDebugViewsModel.Settings m_Settings = BuiltinDebugViewsModel.Settings.defaultSettings;

		// Token: 0x020001E4 RID: 484
		public enum Mode
		{
			// Token: 0x0400069C RID: 1692
			None,
			// Token: 0x0400069D RID: 1693
			Depth,
			// Token: 0x0400069E RID: 1694
			Normals,
			// Token: 0x0400069F RID: 1695
			MotionVectors,
			// Token: 0x040006A0 RID: 1696
			AmbientOcclusion,
			// Token: 0x040006A1 RID: 1697
			EyeAdaptation,
			// Token: 0x040006A2 RID: 1698
			FocusPlane,
			// Token: 0x040006A3 RID: 1699
			PreGradingLog,
			// Token: 0x040006A4 RID: 1700
			LogLut,
			// Token: 0x040006A5 RID: 1701
			UserLut
		}

		// Token: 0x020001E5 RID: 485
		[Serializable]
		public struct DepthSettings
		{
			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000900 RID: 2304 RVA: 0x0007DD2C File Offset: 0x0007BF2C
			public static BuiltinDebugViewsModel.DepthSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.DepthSettings
					{
						scale = 1f
					};
				}
			}

			// Token: 0x040006A6 RID: 1702
			[Range(0f, 1f)]
			[Tooltip("Scales the camera far plane before displaying the depth map.")]
			public float scale;
		}

		// Token: 0x020001E6 RID: 486
		[Serializable]
		public struct MotionVectorsSettings
		{
			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000901 RID: 2305 RVA: 0x0007DD50 File Offset: 0x0007BF50
			public static BuiltinDebugViewsModel.MotionVectorsSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.MotionVectorsSettings
					{
						sourceOpacity = 1f,
						motionImageOpacity = 0f,
						motionImageAmplitude = 16f,
						motionVectorsOpacity = 1f,
						motionVectorsResolution = 24,
						motionVectorsAmplitude = 64f
					};
				}
			}

			// Token: 0x040006A7 RID: 1703
			[Range(0f, 1f)]
			[Tooltip("Opacity of the source render.")]
			public float sourceOpacity;

			// Token: 0x040006A8 RID: 1704
			[Range(0f, 1f)]
			[Tooltip("Opacity of the per-pixel motion vector colors.")]
			public float motionImageOpacity;

			// Token: 0x040006A9 RID: 1705
			[Min(0f)]
			[Tooltip("Because motion vectors are mainly very small vectors, you can use this setting to make them more visible.")]
			public float motionImageAmplitude;

			// Token: 0x040006AA RID: 1706
			[Range(0f, 1f)]
			[Tooltip("Opacity for the motion vector arrows.")]
			public float motionVectorsOpacity;

			// Token: 0x040006AB RID: 1707
			[Range(8f, 64f)]
			[Tooltip("The arrow density on screen.")]
			public int motionVectorsResolution;

			// Token: 0x040006AC RID: 1708
			[Min(0f)]
			[Tooltip("Tweaks the arrows length.")]
			public float motionVectorsAmplitude;
		}

		// Token: 0x020001E7 RID: 487
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000902 RID: 2306 RVA: 0x0007DDAC File Offset: 0x0007BFAC
			public static BuiltinDebugViewsModel.Settings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.Settings
					{
						mode = BuiltinDebugViewsModel.Mode.None,
						depth = BuiltinDebugViewsModel.DepthSettings.defaultSettings,
						motionVectors = BuiltinDebugViewsModel.MotionVectorsSettings.defaultSettings
					};
				}
			}

			// Token: 0x040006AD RID: 1709
			public BuiltinDebugViewsModel.Mode mode;

			// Token: 0x040006AE RID: 1710
			public BuiltinDebugViewsModel.DepthSettings depth;

			// Token: 0x040006AF RID: 1711
			public BuiltinDebugViewsModel.MotionVectorsSettings motionVectors;
		}
	}
}
