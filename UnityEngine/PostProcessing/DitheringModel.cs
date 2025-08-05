using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000201 RID: 513
	[Serializable]
	public class DitheringModel : PostProcessingModel
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0000B3CC File Offset: 0x000095CC
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0000B3D4 File Offset: 0x000095D4
		public DitheringModel.Settings settings
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

		// Token: 0x0600094E RID: 2382 RVA: 0x0000B3DD File Offset: 0x000095DD
		public override void Reset()
		{
			this.m_Settings = DitheringModel.Settings.defaultSettings;
		}

		// Token: 0x04000728 RID: 1832
		[SerializeField]
		private DitheringModel.Settings m_Settings = DitheringModel.Settings.defaultSettings;

		// Token: 0x02000202 RID: 514
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000950 RID: 2384 RVA: 0x0007F878 File Offset: 0x0007DA78
			public static DitheringModel.Settings defaultSettings
			{
				get
				{
					return default(DitheringModel.Settings);
				}
			}
		}
	}
}
