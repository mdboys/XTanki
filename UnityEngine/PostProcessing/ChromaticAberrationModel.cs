using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001EA RID: 490
	[Serializable]
	public class ChromaticAberrationModel : PostProcessingModel
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0000B0C8 File Offset: 0x000092C8
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0000B0D0 File Offset: 0x000092D0
		public ChromaticAberrationModel.Settings settings
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

		// Token: 0x0600090A RID: 2314 RVA: 0x0000B0D9 File Offset: 0x000092D9
		public override void Reset()
		{
			this.m_Settings = ChromaticAberrationModel.Settings.defaultSettings;
		}

		// Token: 0x040006B3 RID: 1715
		[SerializeField]
		private ChromaticAberrationModel.Settings m_Settings = ChromaticAberrationModel.Settings.defaultSettings;

		// Token: 0x020001EB RID: 491
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000166 RID: 358
			// (get) Token: 0x0600090C RID: 2316 RVA: 0x0007DF40 File Offset: 0x0007C140
			public static ChromaticAberrationModel.Settings defaultSettings
			{
				get
				{
					return new ChromaticAberrationModel.Settings
					{
						spectralTexture = null,
						intensity = 0.1f
					};
				}
			}

			// Token: 0x040006B4 RID: 1716
			[Nullable(1)]
			[Tooltip("Shift the hue of chromatic aberrations.")]
			public Texture2D spectralTexture;

			// Token: 0x040006B5 RID: 1717
			[Range(0f, 1f)]
			[Tooltip("Amount of tangential distortion.")]
			public float intensity;
		}
	}
}
