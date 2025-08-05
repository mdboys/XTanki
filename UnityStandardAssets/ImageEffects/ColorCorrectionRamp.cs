using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000195 RID: 405
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (Ramp)")]
	public class ColorCorrectionRamp : ImageEffectBase
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x0000A5E1 File Offset: 0x000087E1
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.material.SetTexture("_RampTex", this.textureRamp);
			Graphics.Blit(source, destination, base.material);
		}

		// Token: 0x040004D3 RID: 1235
		public Texture textureRamp;
	}
}
