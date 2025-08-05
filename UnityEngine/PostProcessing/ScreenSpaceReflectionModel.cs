using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200022A RID: 554
	[Serializable]
	public class ScreenSpaceReflectionModel : PostProcessingModel
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0000B901 File Offset: 0x00009B01
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0000B909 File Offset: 0x00009B09
		public ScreenSpaceReflectionModel.Settings settings
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

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000B912 File Offset: 0x00009B12
		public override void Reset()
		{
			this.m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;
		}

		// Token: 0x040007FF RID: 2047
		[SerializeField]
		private ScreenSpaceReflectionModel.Settings m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;

		// Token: 0x0200022B RID: 555
		public enum SSRReflectionBlendType
		{
			// Token: 0x04000801 RID: 2049
			PhysicallyBased,
			// Token: 0x04000802 RID: 2050
			Additive
		}

		// Token: 0x0200022C RID: 556
		public enum SSRResolution
		{
			// Token: 0x04000804 RID: 2052
			High,
			// Token: 0x04000805 RID: 2053
			Low = 2
		}

		// Token: 0x0200022D RID: 557
		[Serializable]
		public struct IntensitySettings
		{
			// Token: 0x04000806 RID: 2054
			[Tooltip("Nonphysical multiplier for the SSR reflections. 1.0 is physically based.")]
			[Range(0f, 2f)]
			public float reflectionMultiplier;

			// Token: 0x04000807 RID: 2055
			[Tooltip("How far away from the maxDistance to begin fading SSR.")]
			[Range(0f, 1000f)]
			public float fadeDistance;

			// Token: 0x04000808 RID: 2056
			[Tooltip("Amplify Fresnel fade out. Increase if floor reflections look good close to the surface and bad farther 'under' the floor.")]
			[Range(0f, 1f)]
			public float fresnelFade;

			// Token: 0x04000809 RID: 2057
			[Tooltip("Higher values correspond to a faster Fresnel fade as the reflection changes from the grazing angle.")]
			[Range(0.1f, 10f)]
			public float fresnelFadePower;
		}

		// Token: 0x0200022E RID: 558
		[Serializable]
		public struct ReflectionSettings
		{
			// Token: 0x0400080A RID: 2058
			[Tooltip("How the reflections are blended into the render.")]
			public ScreenSpaceReflectionModel.SSRReflectionBlendType blendType;

			// Token: 0x0400080B RID: 2059
			[Tooltip("Half resolution SSRR is much faster, but less accurate.")]
			public ScreenSpaceReflectionModel.SSRResolution reflectionQuality;

			// Token: 0x0400080C RID: 2060
			[Tooltip("Maximum reflection distance in world units.")]
			[Range(0.1f, 300f)]
			public float maxDistance;

			// Token: 0x0400080D RID: 2061
			[Tooltip("Max raytracing length.")]
			[Range(16f, 1024f)]
			public int iterationCount;

			// Token: 0x0400080E RID: 2062
			[Tooltip("Log base 2 of ray tracing coarse step size. Higher traces farther, lower gives better quality silhouettes.")]
			[Range(1f, 16f)]
			public int stepSize;

			// Token: 0x0400080F RID: 2063
			[Tooltip("Typical thickness of columns, walls, furniture, and other objects that reflection rays might pass behind.")]
			[Range(0.01f, 10f)]
			public float widthModifier;

			// Token: 0x04000810 RID: 2064
			[Tooltip("Blurriness of reflections.")]
			[Range(0.1f, 8f)]
			public float reflectionBlur;

			// Token: 0x04000811 RID: 2065
			[Tooltip("Disable for a performance gain in scenes where most glossy objects are horizontal, like floors, water, and tables. Leave on for scenes with glossy vertical objects.")]
			public bool reflectBackfaces;
		}

		// Token: 0x0200022F RID: 559
		[Serializable]
		public struct ScreenEdgeMask
		{
			// Token: 0x04000812 RID: 2066
			[Tooltip("Higher = fade out SSRR near the edge of the screen so that reflections don't pop under camera motion.")]
			[Range(0f, 1f)]
			public float intensity;
		}

		// Token: 0x02000230 RID: 560
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060009EA RID: 2538 RVA: 0x00082910 File Offset: 0x00080B10
			public static ScreenSpaceReflectionModel.Settings defaultSettings
			{
				get
				{
					return new ScreenSpaceReflectionModel.Settings
					{
						reflection = new ScreenSpaceReflectionModel.ReflectionSettings
						{
							blendType = ScreenSpaceReflectionModel.SSRReflectionBlendType.PhysicallyBased,
							reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.Low,
							maxDistance = 100f,
							iterationCount = 256,
							stepSize = 3,
							widthModifier = 0.5f,
							reflectionBlur = 1f,
							reflectBackfaces = false
						},
						intensity = new ScreenSpaceReflectionModel.IntensitySettings
						{
							reflectionMultiplier = 1f,
							fadeDistance = 100f,
							fresnelFade = 1f,
							fresnelFadePower = 1f
						},
						screenEdgeMask = new ScreenSpaceReflectionModel.ScreenEdgeMask
						{
							intensity = 0.03f
						}
					};
				}
			}

			// Token: 0x04000813 RID: 2067
			public ScreenSpaceReflectionModel.ReflectionSettings reflection;

			// Token: 0x04000814 RID: 2068
			public ScreenSpaceReflectionModel.IntensitySettings intensity;

			// Token: 0x04000815 RID: 2069
			public ScreenSpaceReflectionModel.ScreenEdgeMask screenEdgeMask;
		}
	}
}
