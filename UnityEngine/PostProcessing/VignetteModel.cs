using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200023B RID: 571
	[Serializable]
	public class VignetteModel : PostProcessingModel
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0000BA2D File Offset: 0x00009C2D
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0000BA35 File Offset: 0x00009C35
		public VignetteModel.Settings settings
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

		// Token: 0x06000A0B RID: 2571 RVA: 0x0000BA3E File Offset: 0x00009C3E
		public override void Reset()
		{
			this.m_Settings = VignetteModel.Settings.defaultSettings;
		}

		// Token: 0x0400082D RID: 2093
		[SerializeField]
		private VignetteModel.Settings m_Settings = VignetteModel.Settings.defaultSettings;

		// Token: 0x0200023C RID: 572
		public enum Mode
		{
			// Token: 0x0400082F RID: 2095
			Classic,
			// Token: 0x04000830 RID: 2096
			Masked
		}

		// Token: 0x0200023D RID: 573
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00083408 File Offset: 0x00081608
			public static VignetteModel.Settings defaultSettings
			{
				get
				{
					return new VignetteModel.Settings
					{
						mode = VignetteModel.Mode.Classic,
						color = new Color(0f, 0f, 0f, 1f),
						center = new Vector2(0.5f, 0.5f),
						intensity = 0.45f,
						smoothness = 0.2f,
						roundness = 1f,
						mask = null,
						opacity = 1f,
						rounded = false
					};
				}
			}

			// Token: 0x04000831 RID: 2097
			[Tooltip("Use the \"Classic\" mode for parametric controls. Use the \"Masked\" mode to use your own texture mask.")]
			public VignetteModel.Mode mode;

			// Token: 0x04000832 RID: 2098
			[ColorUsage(false)]
			[Tooltip("Vignette color. Use the alpha channel for transparency.")]
			public Color color;

			// Token: 0x04000833 RID: 2099
			[Tooltip("Sets the vignette center point (screen center is [0.5,0.5]).")]
			public Vector2 center;

			// Token: 0x04000834 RID: 2100
			[Range(0f, 1f)]
			[Tooltip("Amount of vignetting on screen.")]
			public float intensity;

			// Token: 0x04000835 RID: 2101
			[Range(0.01f, 1f)]
			[Tooltip("Smoothness of the vignette borders.")]
			public float smoothness;

			// Token: 0x04000836 RID: 2102
			[Range(0f, 1f)]
			[Tooltip("Lower values will make a square-ish vignette.")]
			public float roundness;

			// Token: 0x04000837 RID: 2103
			[Nullable(1)]
			[Tooltip("A black and white mask to use as a vignette.")]
			public Texture mask;

			// Token: 0x04000838 RID: 2104
			[Range(0f, 1f)]
			[Tooltip("Mask opacity.")]
			public float opacity;

			// Token: 0x04000839 RID: 2105
			[Tooltip("Should the vignette be perfectly round or be dependent on the current aspect ratio?")]
			public bool rounded;
		}
	}
}
