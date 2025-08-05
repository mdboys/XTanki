using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200020C RID: 524
	[Nullable(new byte[] { 0, 1 })]
	public sealed class FxaaComponent : PostProcessingComponentRenderTexture<AntialiasingModel>
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0008003C File Offset: 0x0007E23C
		public override bool active
		{
			get
			{
				AntialiasingModel model = base.model;
				return model != null && model.enabled && model.settings.method == AntialiasingModel.Method.Fxaa && !this.context.interrupted;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00080078 File Offset: 0x0007E278
		[NullableContext(1)]
		public void Render(RenderTexture source, RenderTexture destination)
		{
			AntialiasingModel.FxaaSettings fxaaSettings = base.model.settings.fxaaSettings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/FXAA");
			AntialiasingModel.FxaaQualitySettings fxaaQualitySettings = AntialiasingModel.FxaaQualitySettings.presets[(int)fxaaSettings.preset];
			AntialiasingModel.FxaaConsoleSettings fxaaConsoleSettings = AntialiasingModel.FxaaConsoleSettings.presets[(int)fxaaSettings.preset];
			material.SetVector(FxaaComponent.Uniforms._QualitySettings, new Vector3(fxaaQualitySettings.subpixelAliasingRemovalAmount, fxaaQualitySettings.edgeDetectionThreshold, fxaaQualitySettings.minimumRequiredLuminance));
			material.SetVector(FxaaComponent.Uniforms._ConsoleSettings, new Vector4(fxaaConsoleSettings.subpixelSpreadAmount, fxaaConsoleSettings.edgeSharpnessAmount, fxaaConsoleSettings.edgeDetectionThreshold, fxaaConsoleSettings.minimumRequiredLuminance));
			Graphics.Blit(source, destination, material, 0);
		}

		// Token: 0x0200020D RID: 525
		private static class Uniforms
		{
			// Token: 0x04000751 RID: 1873
			internal static readonly int _QualitySettings = Shader.PropertyToID("_QualitySettings");

			// Token: 0x04000752 RID: 1874
			internal static readonly int _ConsoleSettings = Shader.PropertyToID("_ConsoleSettings");
		}
	}
}
