using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001FC RID: 508
	[Serializable]
	public class DepthOfFieldModel : PostProcessingModel
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0000B34B File Offset: 0x0000954B
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0000B353 File Offset: 0x00009553
		public DepthOfFieldModel.Settings settings
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

		// Token: 0x06000943 RID: 2371 RVA: 0x0000B35C File Offset: 0x0000955C
		public override void Reset()
		{
			this.m_Settings = DepthOfFieldModel.Settings.defaultSettings;
		}

		// Token: 0x04000718 RID: 1816
		[SerializeField]
		private DepthOfFieldModel.Settings m_Settings = DepthOfFieldModel.Settings.defaultSettings;

		// Token: 0x020001FD RID: 509
		public enum KernelSize
		{
			// Token: 0x0400071A RID: 1818
			Small,
			// Token: 0x0400071B RID: 1819
			Medium,
			// Token: 0x0400071C RID: 1820
			Large,
			// Token: 0x0400071D RID: 1821
			VeryLarge
		}

		// Token: 0x020001FE RID: 510
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000945 RID: 2373 RVA: 0x0007F744 File Offset: 0x0007D944
			public static DepthOfFieldModel.Settings defaultSettings
			{
				get
				{
					return new DepthOfFieldModel.Settings
					{
						focusDistance = 10f,
						aperture = 5.6f,
						focalLength = 50f,
						useCameraFov = false,
						kernelSize = DepthOfFieldModel.KernelSize.Medium
					};
				}
			}

			// Token: 0x0400071E RID: 1822
			[Min(0.1f)]
			[Tooltip("Distance to the point of focus.")]
			public float focusDistance;

			// Token: 0x0400071F RID: 1823
			[Range(0.05f, 32f)]
			[Tooltip("Ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
			public float aperture;

			// Token: 0x04000720 RID: 1824
			[Range(1f, 300f)]
			[Tooltip("Distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
			public float focalLength;

			// Token: 0x04000721 RID: 1825
			[Tooltip("Calculate the focal length automatically from the field-of-view value set on the camera. Using this setting isn't recommended.")]
			public bool useCameraFov;

			// Token: 0x04000722 RID: 1826
			[Tooltip("Convolution kernel size of the bokeh filter, which determines the maximum radius of bokeh. It also affects the performance (the larger the kernel is, the longer the GPU time is required).")]
			public DepthOfFieldModel.KernelSize kernelSize;
		}
	}
}
