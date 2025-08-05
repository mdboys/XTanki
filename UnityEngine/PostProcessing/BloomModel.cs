using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001DB RID: 475
	[Serializable]
	public class BloomModel : PostProcessingModel
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0000AF16 File Offset: 0x00009116
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0000AF1E File Offset: 0x0000911E
		public BloomModel.Settings settings
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

		// Token: 0x060008DE RID: 2270 RVA: 0x0000AF27 File Offset: 0x00009127
		public override void Reset()
		{
			this.m_Settings = BloomModel.Settings.defaultSettings;
		}

		// Token: 0x0400067E RID: 1662
		[SerializeField]
		private BloomModel.Settings m_Settings = BloomModel.Settings.defaultSettings;

		// Token: 0x020001DC RID: 476
		[Serializable]
		public struct BloomSettings
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0000AF47 File Offset: 0x00009147
			// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0000AF54 File Offset: 0x00009154
			public float thresholdLinear
			{
				get
				{
					return Mathf.GammaToLinearSpace(this.threshold);
				}
				set
				{
					this.threshold = Mathf.LinearToGammaSpace(value);
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0007D690 File Offset: 0x0007B890
			public static BloomModel.BloomSettings defaultSettings
			{
				get
				{
					return new BloomModel.BloomSettings
					{
						intensity = 0.5f,
						threshold = 1.1f,
						softKnee = 0.5f,
						radius = 4f,
						antiFlicker = false
					};
				}
			}

			// Token: 0x0400067F RID: 1663
			[Min(0f)]
			[Tooltip("Strength of the bloom filter.")]
			public float intensity;

			// Token: 0x04000680 RID: 1664
			[Min(0f)]
			[Tooltip("Filters out pixels under this level of brightness.")]
			public float threshold;

			// Token: 0x04000681 RID: 1665
			[Range(0f, 1f)]
			[Tooltip("Makes transition between under/over-threshold gradual (0 = hard threshold, 1 = soft threshold).")]
			public float softKnee;

			// Token: 0x04000682 RID: 1666
			[Range(1f, 7f)]
			[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
			public float radius;

			// Token: 0x04000683 RID: 1667
			[Tooltip("Reduces flashing noise with an additional filter.")]
			public bool antiFlicker;
		}

		// Token: 0x020001DD RID: 477
		[Serializable]
		public struct LensDirtSettings
		{
			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0007D6E0 File Offset: 0x0007B8E0
			public static BloomModel.LensDirtSettings defaultSettings
			{
				get
				{
					return new BloomModel.LensDirtSettings
					{
						texture = null,
						intensity = 3f
					};
				}
			}

			// Token: 0x04000684 RID: 1668
			[Nullable(1)]
			[Tooltip("Dirtiness texture to add smudges or dust to the lens.")]
			public Texture texture;

			// Token: 0x04000685 RID: 1669
			[Min(0f)]
			[Tooltip("Amount of lens dirtiness.")]
			public float intensity;
		}

		// Token: 0x020001DE RID: 478
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0007D70C File Offset: 0x0007B90C
			public static BloomModel.Settings defaultSettings
			{
				get
				{
					return new BloomModel.Settings
					{
						bloom = BloomModel.BloomSettings.defaultSettings,
						lensDirt = BloomModel.LensDirtSettings.defaultSettings
					};
				}
			}

			// Token: 0x04000686 RID: 1670
			public BloomModel.BloomSettings bloom;

			// Token: 0x04000687 RID: 1671
			public BloomModel.LensDirtSettings lensDirt;
		}
	}
}
