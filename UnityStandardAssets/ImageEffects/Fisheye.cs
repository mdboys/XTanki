using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x020001A3 RID: 419
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Displacement/Fisheye")]
	public class Fisheye : PostEffectsBase
	{
		// Token: 0x0600084B RID: 2123 RVA: 0x00078D94 File Offset: 0x00076F94
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			float num = 0.15625f;
			float num2 = (float)source.width * 1f / ((float)source.height * 1f);
			this.fisheyeMaterial.SetVector("intensity", new Vector4(this.strengthX * num2 * num, this.strengthY * num, this.strengthX * num2 * num, this.strengthY * num));
			Graphics.Blit(source, destination, this.fisheyeMaterial);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0000A7D9 File Offset: 0x000089D9
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.fisheyeMaterial = base.CheckShaderAndCreateMaterial(this.fishEyeShader, this.fisheyeMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x0400055B RID: 1371
		public float strengthX = 0.05f;

		// Token: 0x0400055C RID: 1372
		public float strengthY = 0.05f;

		// Token: 0x0400055D RID: 1373
		public Shader fishEyeShader;

		// Token: 0x0400055E RID: 1374
		private Material fisheyeMaterial;
	}
}
