using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000211 RID: 529
	[Serializable]
	public class GrainModel : PostProcessingModel
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0000B580 File Offset: 0x00009780
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0000B588 File Offset: 0x00009788
		public GrainModel.Settings settings
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

		// Token: 0x06000977 RID: 2423 RVA: 0x0000B591 File Offset: 0x00009791
		public override void Reset()
		{
			this.m_Settings = GrainModel.Settings.defaultSettings;
		}

		// Token: 0x0400075A RID: 1882
		[SerializeField]
		private GrainModel.Settings m_Settings = GrainModel.Settings.defaultSettings;

		// Token: 0x02000212 RID: 530
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000979 RID: 2425 RVA: 0x000802E0 File Offset: 0x0007E4E0
			public static GrainModel.Settings defaultSettings
			{
				get
				{
					return new GrainModel.Settings
					{
						colored = true,
						intensity = 0.5f,
						size = 1f,
						luminanceContribution = 0.8f
					};
				}
			}

			// Token: 0x0400075B RID: 1883
			[Tooltip("Enable the use of colored grain.")]
			public bool colored;

			// Token: 0x0400075C RID: 1884
			[Range(0f, 1f)]
			[Tooltip("Grain strength. Higher means more visible grain.")]
			public float intensity;

			// Token: 0x0400075D RID: 1885
			[Range(0.3f, 3f)]
			[Tooltip("Grain particle size.")]
			public float size;

			// Token: 0x0400075E RID: 1886
			[Range(0f, 1f)]
			[Tooltip("Controls the noisiness response curve based on scene luminance. Lower values mean less noise in dark areas.")]
			public float luminanceContribution;
		}
	}
}
