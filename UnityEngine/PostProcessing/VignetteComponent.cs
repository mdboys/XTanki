using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000239 RID: 569
	[Nullable(new byte[] { 0, 1 })]
	public sealed class VignetteComponent : PostProcessingComponentRenderTexture<VignetteModel>
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0000BA06 File Offset: 0x00009C06
		public override bool active
		{
			get
			{
				return base.model.enabled && !this.context.interrupted;
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000832B4 File Offset: 0x000814B4
		[NullableContext(1)]
		public override void Prepare(Material uberMaterial)
		{
			VignetteModel.Settings settings = base.model.settings;
			uberMaterial.SetColor(VignetteComponent.Uniforms._Vignette_Color, settings.color);
			if (settings.mode == VignetteModel.Mode.Classic)
			{
				uberMaterial.SetVector(VignetteComponent.Uniforms._Vignette_Center, settings.center);
				uberMaterial.EnableKeyword("VIGNETTE_CLASSIC");
				float num = (1f - settings.roundness) * 6f + settings.roundness;
				uberMaterial.SetVector(VignetteComponent.Uniforms._Vignette_Settings, new Vector4(settings.intensity * 3f, settings.smoothness * 5f, num, (!settings.rounded) ? 0f : 1f));
				return;
			}
			if (settings.mode == VignetteModel.Mode.Masked && settings.mask != null && settings.opacity > 0f)
			{
				uberMaterial.EnableKeyword("VIGNETTE_MASKED");
				uberMaterial.SetTexture(VignetteComponent.Uniforms._Vignette_Mask, settings.mask);
				uberMaterial.SetFloat(VignetteComponent.Uniforms._Vignette_Opacity, settings.opacity);
			}
		}

		// Token: 0x0200023A RID: 570
		private static class Uniforms
		{
			// Token: 0x04000828 RID: 2088
			internal static readonly int _Vignette_Color = Shader.PropertyToID("_Vignette_Color");

			// Token: 0x04000829 RID: 2089
			internal static readonly int _Vignette_Center = Shader.PropertyToID("_Vignette_Center");

			// Token: 0x0400082A RID: 2090
			internal static readonly int _Vignette_Settings = Shader.PropertyToID("_Vignette_Settings");

			// Token: 0x0400082B RID: 2091
			internal static readonly int _Vignette_Mask = Shader.PropertyToID("_Vignette_Mask");

			// Token: 0x0400082C RID: 2092
			internal static readonly int _Vignette_Opacity = Shader.PropertyToID("_Vignette_Opacity");
		}
	}
}
