using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001E8 RID: 488
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class ChromaticAberrationComponent : PostProcessingComponentRenderTexture<ChromaticAberrationModel>
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0007DDE4 File Offset: 0x0007BFE4
		public override bool active
		{
			get
			{
				ChromaticAberrationModel model = base.model;
				return model != null && model.enabled && model.settings.intensity > 0f && !this.context.interrupted;
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000B08C File Offset: 0x0000928C
		public override void OnDisable()
		{
			GraphicsUtils.Destroy(this.m_SpectrumLut);
			this.m_SpectrumLut = null;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0007DE28 File Offset: 0x0007C028
		public override void Prepare(Material uberMaterial)
		{
			ChromaticAberrationModel.Settings settings = base.model.settings;
			Texture2D texture2D = settings.spectralTexture;
			if (texture2D == null)
			{
				if (this.m_SpectrumLut == null)
				{
					this.m_SpectrumLut = new Texture2D(3, 1, TextureFormat.RGB24, false)
					{
						name = "Chromatic Aberration Spectrum Lookup",
						filterMode = FilterMode.Bilinear,
						wrapMode = TextureWrapMode.Clamp,
						anisoLevel = 0,
						hideFlags = HideFlags.DontSave
					};
					Color[] array = new Color[]
					{
						new Color(1f, 0f, 0f),
						new Color(0f, 1f, 0f),
						new Color(0f, 0f, 1f)
					};
					this.m_SpectrumLut.SetPixels(array);
					this.m_SpectrumLut.Apply();
				}
				texture2D = this.m_SpectrumLut;
			}
			uberMaterial.EnableKeyword("CHROMATIC_ABERRATION");
			uberMaterial.SetFloat(ChromaticAberrationComponent.Uniforms._ChromaticAberration_Amount, settings.intensity * 0.03f);
			uberMaterial.SetTexture(ChromaticAberrationComponent.Uniforms._ChromaticAberration_Spectrum, texture2D);
		}

		// Token: 0x040006B0 RID: 1712
		private Texture2D m_SpectrumLut;

		// Token: 0x020001E9 RID: 489
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x040006B1 RID: 1713
			internal static readonly int _ChromaticAberration_Amount = Shader.PropertyToID("_ChromaticAberration_Amount");

			// Token: 0x040006B2 RID: 1714
			internal static readonly int _ChromaticAberration_Spectrum = Shader.PropertyToID("_ChromaticAberration_Spectrum");
		}
	}
}
