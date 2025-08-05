using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000181 RID: 385
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Other/Antialiasing")]
	public class Antialiasing : PostEffectsBase
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x000734B0 File Offset: 0x000716B0
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.mode == AAMode.FXAA3Console && this.materialFXAAIII != null)
			{
				this.materialFXAAIII.SetFloat("_EdgeThresholdMin", this.edgeThresholdMin);
				this.materialFXAAIII.SetFloat("_EdgeThreshold", this.edgeThreshold);
				this.materialFXAAIII.SetFloat("_EdgeSharpness", this.edgeSharpness);
				Graphics.Blit(source, destination, this.materialFXAAIII);
				return;
			}
			if (this.mode == AAMode.FXAA1PresetB && this.materialFXAAPreset3 != null)
			{
				Graphics.Blit(source, destination, this.materialFXAAPreset3);
				return;
			}
			if (this.mode == AAMode.FXAA1PresetA && this.materialFXAAPreset2 != null)
			{
				source.anisoLevel = 4;
				Graphics.Blit(source, destination, this.materialFXAAPreset2);
				source.anisoLevel = 0;
				return;
			}
			if (this.mode == AAMode.FXAA2 && this.materialFXAAII != null)
			{
				Graphics.Blit(source, destination, this.materialFXAAII);
				return;
			}
			if (this.mode == AAMode.SSAA && this.ssaa != null)
			{
				Graphics.Blit(source, destination, this.ssaa);
				return;
			}
			if (this.mode == AAMode.DLAA && this.dlaa != null)
			{
				source.anisoLevel = 0;
				RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
				Graphics.Blit(source, temporary, this.dlaa, 0);
				Graphics.Blit(temporary, destination, this.dlaa, (!this.dlaaSharp) ? 1 : 2);
				RenderTexture.ReleaseTemporary(temporary);
				return;
			}
			if (this.mode == AAMode.NFAA && this.nfaa != null)
			{
				source.anisoLevel = 0;
				this.nfaa.SetFloat("_OffsetScale", this.offsetScale);
				this.nfaa.SetFloat("_BlurRadius", this.blurRadius);
				Graphics.Blit(source, destination, this.nfaa, (this.showGeneratedNormals > false) ? 1 : 0);
				return;
			}
			Graphics.Blit(source, destination);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0007369C File Offset: 0x0007189C
		public Material CurrentAAMaterial()
		{
			Material material;
			switch (this.mode)
			{
			case AAMode.FXAA2:
				material = this.materialFXAAII;
				break;
			case AAMode.FXAA3Console:
				material = this.materialFXAAIII;
				break;
			case AAMode.FXAA1PresetA:
				material = this.materialFXAAPreset2;
				break;
			case AAMode.FXAA1PresetB:
				material = this.materialFXAAPreset3;
				break;
			case AAMode.NFAA:
				material = this.nfaa;
				break;
			case AAMode.SSAA:
				material = this.ssaa;
				break;
			case AAMode.DLAA:
				material = this.dlaa;
				break;
			default:
				material = null;
				break;
			}
			return material;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00073718 File Offset: 0x00071918
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.materialFXAAPreset2 = base.CreateMaterial(this.shaderFXAAPreset2, this.materialFXAAPreset2);
			this.materialFXAAPreset3 = base.CreateMaterial(this.shaderFXAAPreset3, this.materialFXAAPreset3);
			this.materialFXAAII = base.CreateMaterial(this.shaderFXAAII, this.materialFXAAII);
			this.materialFXAAIII = base.CreateMaterial(this.shaderFXAAIII, this.materialFXAAIII);
			this.nfaa = base.CreateMaterial(this.nfaaShader, this.nfaa);
			this.ssaa = base.CreateMaterial(this.ssaaShader, this.ssaa);
			this.dlaa = base.CreateMaterial(this.dlaaShader, this.dlaa);
			if (!this.ssaaShader.isSupported)
			{
				base.NotSupported();
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x0400040B RID: 1035
		public AAMode mode = AAMode.FXAA3Console;

		// Token: 0x0400040C RID: 1036
		public bool showGeneratedNormals;

		// Token: 0x0400040D RID: 1037
		public float offsetScale = 0.2f;

		// Token: 0x0400040E RID: 1038
		public float blurRadius = 18f;

		// Token: 0x0400040F RID: 1039
		public float edgeThresholdMin = 0.05f;

		// Token: 0x04000410 RID: 1040
		public float edgeThreshold = 0.2f;

		// Token: 0x04000411 RID: 1041
		public float edgeSharpness = 4f;

		// Token: 0x04000412 RID: 1042
		public bool dlaaSharp;

		// Token: 0x04000413 RID: 1043
		public Shader ssaaShader;

		// Token: 0x04000414 RID: 1044
		public Shader dlaaShader;

		// Token: 0x04000415 RID: 1045
		public Shader nfaaShader;

		// Token: 0x04000416 RID: 1046
		public Shader shaderFXAAPreset2;

		// Token: 0x04000417 RID: 1047
		public Shader shaderFXAAPreset3;

		// Token: 0x04000418 RID: 1048
		public Shader shaderFXAAII;

		// Token: 0x04000419 RID: 1049
		public Shader shaderFXAAIII;

		// Token: 0x0400041A RID: 1050
		private Material dlaa;

		// Token: 0x0400041B RID: 1051
		private Material materialFXAAII;

		// Token: 0x0400041C RID: 1052
		private Material materialFXAAIII;

		// Token: 0x0400041D RID: 1053
		private Material materialFXAAPreset2;

		// Token: 0x0400041E RID: 1054
		private Material materialFXAAPreset3;

		// Token: 0x0400041F RID: 1055
		private Material nfaa;

		// Token: 0x04000420 RID: 1056
		private Material ssaa;
	}
}
