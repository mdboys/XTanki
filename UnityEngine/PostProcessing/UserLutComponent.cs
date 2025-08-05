using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000235 RID: 565
	[Nullable(new byte[] { 0, 1 })]
	public sealed class UserLutComponent : PostProcessingComponentRenderTexture<UserLutModel>
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0008312C File Offset: 0x0008132C
		public override bool active
		{
			get
			{
				UserLutModel.Settings settings = base.model.settings;
				return base.model.enabled && settings.lut != null && settings.contribution > 0f && settings.lut.height == (int)Mathf.Sqrt((float)settings.lut.width) && !this.context.interrupted;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0008319C File Offset: 0x0008139C
		[NullableContext(1)]
		public override void Prepare(Material uberMaterial)
		{
			UserLutModel.Settings settings = base.model.settings;
			uberMaterial.EnableKeyword("USER_LUT");
			uberMaterial.SetTexture(UserLutComponent.Uniforms._UserLut, settings.lut);
			uberMaterial.SetVector(UserLutComponent.Uniforms._UserLut_Params, new Vector4(1f / (float)settings.lut.width, 1f / (float)settings.lut.height, (float)settings.lut.height - 1f, settings.contribution));
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00083220 File Offset: 0x00081420
		public void OnGUI()
		{
			UserLutModel.Settings settings = base.model.settings;
			GUI.DrawTexture(new Rect(this.context.viewport.x * (float)Screen.width + 8f, 8f, (float)settings.lut.width, (float)settings.lut.height), settings.lut);
		}

		// Token: 0x02000236 RID: 566
		private static class Uniforms
		{
			// Token: 0x04000823 RID: 2083
			internal static readonly int _UserLut = Shader.PropertyToID("_UserLut");

			// Token: 0x04000824 RID: 2084
			internal static readonly int _UserLut_Params = Shader.PropertyToID("_UserLut_Params");
		}
	}
}
