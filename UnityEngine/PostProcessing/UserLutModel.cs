using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000237 RID: 567
	[Serializable]
	public class UserLutModel : PostProcessingModel
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0000B9D5 File Offset: 0x00009BD5
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0000B9DD File Offset: 0x00009BDD
		public UserLutModel.Settings settings
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

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000B9E6 File Offset: 0x00009BE6
		public override void Reset()
		{
			this.m_Settings = UserLutModel.Settings.defaultSettings;
		}

		// Token: 0x04000825 RID: 2085
		[SerializeField]
		private UserLutModel.Settings m_Settings = UserLutModel.Settings.defaultSettings;

		// Token: 0x02000238 RID: 568
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00083288 File Offset: 0x00081488
			public static UserLutModel.Settings defaultSettings
			{
				get
				{
					return new UserLutModel.Settings
					{
						lut = null,
						contribution = 1f
					};
				}
			}

			// Token: 0x04000826 RID: 2086
			[Nullable(1)]
			[Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
			public Texture2D lut;

			// Token: 0x04000827 RID: 2087
			[Range(0f, 1f)]
			[Tooltip("Blending factor.")]
			public float contribution;
		}
	}
}
