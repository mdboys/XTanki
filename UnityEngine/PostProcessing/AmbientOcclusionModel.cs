using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001CE RID: 462
	[Serializable]
	public class AmbientOcclusionModel : PostProcessingModel
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0000AE92 File Offset: 0x00009092
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0000AE9A File Offset: 0x0000909A
		public AmbientOcclusionModel.Settings settings
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

		// Token: 0x060008CC RID: 2252 RVA: 0x0000AEA3 File Offset: 0x000090A3
		public override void Reset()
		{
			this.m_Settings = AmbientOcclusionModel.Settings.defaultSettings;
		}

		// Token: 0x04000649 RID: 1609
		[SerializeField]
		private AmbientOcclusionModel.Settings m_Settings = AmbientOcclusionModel.Settings.defaultSettings;

		// Token: 0x020001CF RID: 463
		public enum SampleCount
		{
			// Token: 0x0400064B RID: 1611
			Lowest = 3,
			// Token: 0x0400064C RID: 1612
			Low = 6,
			// Token: 0x0400064D RID: 1613
			Medium = 10,
			// Token: 0x0400064E RID: 1614
			High = 16
		}

		// Token: 0x020001D0 RID: 464
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060008CE RID: 2254 RVA: 0x0007CEA8 File Offset: 0x0007B0A8
			public static AmbientOcclusionModel.Settings defaultSettings
			{
				get
				{
					return new AmbientOcclusionModel.Settings
					{
						intensity = 1f,
						radius = 0.3f,
						sampleCount = AmbientOcclusionModel.SampleCount.Medium,
						downsampling = true,
						forceForwardCompatibility = false,
						ambientOnly = false,
						highPrecision = false
					};
				}
			}

			// Token: 0x0400064F RID: 1615
			[Range(0f, 4f)]
			[Tooltip("Degree of darkness produced by the effect.")]
			public float intensity;

			// Token: 0x04000650 RID: 1616
			[Min(0.0001f)]
			[Tooltip("Radius of sample points, which affects extent of darkened areas.")]
			public float radius;

			// Token: 0x04000651 RID: 1617
			[Tooltip("Number of sample points, which affects quality and performance.")]
			public AmbientOcclusionModel.SampleCount sampleCount;

			// Token: 0x04000652 RID: 1618
			[Tooltip("Halves the resolution of the effect to increase performance at the cost of visual quality.")]
			public bool downsampling;

			// Token: 0x04000653 RID: 1619
			[Tooltip("Forces compatibility with Forward rendered objects when working with the Deferred rendering path.")]
			public bool forceForwardCompatibility;

			// Token: 0x04000654 RID: 1620
			[Tooltip("Enables the ambient-only mode in that the effect only affects ambient lighting. This mode is only available with the Deferred rendering path and HDR rendering.")]
			public bool ambientOnly;

			// Token: 0x04000655 RID: 1621
			[Tooltip("Toggles the use of a higher precision depth texture with the forward rendering path (may impact performances). Has no effect with the deferred rendering path.")]
			public bool highPrecision;
		}
	}
}
