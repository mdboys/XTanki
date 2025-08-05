using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000225 RID: 549
	[NullableContext(1)]
	[Nullable(0)]
	public class PostProcessingProfile : ScriptableObject
	{
		// Token: 0x040007BD RID: 1981
		public BuiltinDebugViewsModel debugViews = new BuiltinDebugViewsModel();

		// Token: 0x040007BE RID: 1982
		public FogModel fog = new FogModel();

		// Token: 0x040007BF RID: 1983
		public AntialiasingModel antialiasing = new AntialiasingModel();

		// Token: 0x040007C0 RID: 1984
		public AmbientOcclusionModel ambientOcclusion = new AmbientOcclusionModel();

		// Token: 0x040007C1 RID: 1985
		public ScreenSpaceReflectionModel screenSpaceReflection = new ScreenSpaceReflectionModel();

		// Token: 0x040007C2 RID: 1986
		public DepthOfFieldModel depthOfField = new DepthOfFieldModel();

		// Token: 0x040007C3 RID: 1987
		public MotionBlurModel motionBlur = new MotionBlurModel();

		// Token: 0x040007C4 RID: 1988
		public EyeAdaptationModel eyeAdaptation = new EyeAdaptationModel();

		// Token: 0x040007C5 RID: 1989
		public BloomModel bloom = new BloomModel();

		// Token: 0x040007C6 RID: 1990
		public ColorGradingModel colorGrading = new ColorGradingModel();

		// Token: 0x040007C7 RID: 1991
		public UserLutModel userLut = new UserLutModel();

		// Token: 0x040007C8 RID: 1992
		public ChromaticAberrationModel chromaticAberration = new ChromaticAberrationModel();

		// Token: 0x040007C9 RID: 1993
		public GrainModel grain = new GrainModel();

		// Token: 0x040007CA RID: 1994
		public VignetteModel vignette = new VignetteModel();

		// Token: 0x040007CB RID: 1995
		public DitheringModel dithering = new DitheringModel();
	}
}
