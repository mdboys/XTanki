using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000205 RID: 517
	[Serializable]
	public class EyeAdaptationModel : PostProcessingModel
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0000B447 File Offset: 0x00009647
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0000B44F File Offset: 0x0000964F
		public EyeAdaptationModel.Settings settings
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

		// Token: 0x0600095C RID: 2396 RVA: 0x0000B458 File Offset: 0x00009658
		public override void Reset()
		{
			this.m_Settings = EyeAdaptationModel.Settings.defaultSettings;
		}

		// Token: 0x0400073A RID: 1850
		[SerializeField]
		private EyeAdaptationModel.Settings m_Settings = EyeAdaptationModel.Settings.defaultSettings;

		// Token: 0x02000206 RID: 518
		public enum EyeAdaptationType
		{
			// Token: 0x0400073C RID: 1852
			Progressive,
			// Token: 0x0400073D RID: 1853
			Fixed
		}

		// Token: 0x02000207 RID: 519
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700017B RID: 379
			// (get) Token: 0x0600095E RID: 2398 RVA: 0x0007FE08 File Offset: 0x0007E008
			public static EyeAdaptationModel.Settings defaultSettings
			{
				get
				{
					return new EyeAdaptationModel.Settings
					{
						lowPercent = 45f,
						highPercent = 95f,
						minLuminance = -5f,
						maxLuminance = 1f,
						keyValue = 0.25f,
						dynamicKeyValue = true,
						adaptationType = EyeAdaptationModel.EyeAdaptationType.Progressive,
						speedUp = 2f,
						speedDown = 1f,
						logMin = -8,
						logMax = 4
					};
				}
			}

			// Token: 0x0400073E RID: 1854
			[Range(1f, 99f)]
			[Tooltip("Filters the dark part of the histogram when computing the average luminance to avoid very dark pixels from contributing to the auto exposure. Unit is in percent.")]
			public float lowPercent;

			// Token: 0x0400073F RID: 1855
			[Range(1f, 99f)]
			[Tooltip("Filters the bright part of the histogram when computing the average luminance to avoid very dark pixels from contributing to the auto exposure. Unit is in percent.")]
			public float highPercent;

			// Token: 0x04000740 RID: 1856
			[Tooltip("Minimum average luminance to consider for auto exposure (in EV).")]
			public float minLuminance;

			// Token: 0x04000741 RID: 1857
			[Tooltip("Maximum average luminance to consider for auto exposure (in EV).")]
			public float maxLuminance;

			// Token: 0x04000742 RID: 1858
			[Min(0f)]
			[Tooltip("Exposure bias. Use this to offset the global exposure of the scene.")]
			public float keyValue;

			// Token: 0x04000743 RID: 1859
			[Tooltip("Set this to true to let Unity handle the key value automatically based on average luminance.")]
			public bool dynamicKeyValue;

			// Token: 0x04000744 RID: 1860
			[Tooltip("Use \"Progressive\" if you want the auto exposure to be animated. Use \"Fixed\" otherwise.")]
			public EyeAdaptationModel.EyeAdaptationType adaptationType;

			// Token: 0x04000745 RID: 1861
			[Min(0f)]
			[Tooltip("Adaptation speed from a dark to a light environment.")]
			public float speedUp;

			// Token: 0x04000746 RID: 1862
			[Min(0f)]
			[Tooltip("Adaptation speed from a light to a dark environment.")]
			public float speedDown;

			// Token: 0x04000747 RID: 1863
			[Range(-16f, -1f)]
			[Tooltip("Lower bound for the brightness range of the generated histogram (in EV). The bigger the spread between min & max, the lower the precision will be.")]
			public int logMin;

			// Token: 0x04000748 RID: 1864
			[Range(1f, 16f)]
			[Tooltip("Upper bound for the brightness range of the generated histogram (in EV). The bigger the spread between min & max, the lower the precision will be.")]
			public int logMax;
		}
	}
}
