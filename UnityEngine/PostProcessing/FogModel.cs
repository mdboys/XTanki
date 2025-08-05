using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class FogModel : PostProcessingModel
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0000B4BE File Offset: 0x000096BE
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0000B4C6 File Offset: 0x000096C6
		public FogModel.Settings settings
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

		// Token: 0x06000968 RID: 2408 RVA: 0x0000B4CF File Offset: 0x000096CF
		public override void Reset()
		{
			this.m_Settings = FogModel.Settings.defaultSettings;
		}

		// Token: 0x0400074F RID: 1871
		[SerializeField]
		private FogModel.Settings m_Settings = FogModel.Settings.defaultSettings;

		// Token: 0x0200020B RID: 523
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700017E RID: 382
			// (get) Token: 0x0600096A RID: 2410 RVA: 0x0008001C File Offset: 0x0007E21C
			public static FogModel.Settings defaultSettings
			{
				get
				{
					return new FogModel.Settings
					{
						excludeSkybox = true
					};
				}
			}

			// Token: 0x04000750 RID: 1872
			[Tooltip("Should the fog affect the skybox?")]
			public bool excludeSkybox;
		}
	}
}
