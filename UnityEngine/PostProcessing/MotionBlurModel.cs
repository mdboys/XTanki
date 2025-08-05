using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	public class MotionBlurModel : PostProcessingModel
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0000B70A File Offset: 0x0000990A
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0000B712 File Offset: 0x00009912
		public MotionBlurModel.Settings settings
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

		// Token: 0x060009A3 RID: 2467 RVA: 0x0000B71B File Offset: 0x0000991B
		public override void Reset()
		{
			this.m_Settings = MotionBlurModel.Settings.defaultSettings;
		}

		// Token: 0x04000794 RID: 1940
		[SerializeField]
		private MotionBlurModel.Settings m_Settings = MotionBlurModel.Settings.defaultSettings;

		// Token: 0x0200021D RID: 541
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700018B RID: 395
			// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0008123C File Offset: 0x0007F43C
			public static MotionBlurModel.Settings defaultSettings
			{
				get
				{
					return new MotionBlurModel.Settings
					{
						shutterAngle = 270f,
						sampleCount = 10,
						frameBlending = 0f
					};
				}
			}

			// Token: 0x04000795 RID: 1941
			[Range(0f, 360f)]
			[Tooltip("The angle of rotary shutter. Larger values give longer exposure.")]
			public float shutterAngle;

			// Token: 0x04000796 RID: 1942
			[Range(4f, 32f)]
			[Tooltip("The amount of sample points, which affects quality and performances.")]
			public int sampleCount;

			// Token: 0x04000797 RID: 1943
			[Range(0f, 1f)]
			[Tooltip("The strength of multiple frame blending. The opacity of preceding frames are determined from this coefficient and time differences.")]
			public float frameBlending;
		}
	}
}
