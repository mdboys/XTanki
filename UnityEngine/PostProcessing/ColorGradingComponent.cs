using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001EC RID: 492
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class ColorGradingComponent : PostProcessingComponentRenderTexture<ColorGradingModel>
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0000B0F9 File Offset: 0x000092F9
		public override bool active
		{
			get
			{
				return base.model.enabled && !this.context.interrupted;
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000B118 File Offset: 0x00009318
		private float StandardIlluminantY(float x)
		{
			return 2.87f * x - 3f * x * x - 0.27509508f;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0007DF6C File Offset: 0x0007C16C
		private Vector3 CIExyToLMS(float x, float y)
		{
			float num = 1f;
			float num2 = num * x / y;
			float num3 = num * (1f - x - y) / y;
			float num4 = 0.7328f * num2 + 0.4296f * num - 0.1624f * num3;
			float num5 = -0.7036f * num2 + 1.6975f * num + 0.0061f * num3;
			float num6 = 0.003f * num2 + 0.0136f * num + 0.9834f * num3;
			return new Vector3(num4, num5, num6);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0007DFE4 File Offset: 0x0007C1E4
		private Vector3 CalculateColorBalance(float temperature, float tint)
		{
			float num = temperature / 55f;
			float num2 = tint / 55f;
			float num3 = 0.31271f - num * ((num >= 0f) ? 0.05f : 0.1f);
			float num4 = this.StandardIlluminantY(num3) + num2 * 0.05f;
			Vector3 vector = new Vector3(0.949237f, 1.03542f, 1.08728f);
			Vector3 vector2 = this.CIExyToLMS(num3, num4);
			return new Vector3(vector.x / vector2.x, vector.y / vector2.y, vector.z / vector2.z);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0007E080 File Offset: 0x0007C280
		private static Color NormalizeColor(Color c)
		{
			float num = (c.r + c.g + c.b) / 3f;
			if (Mathf.Approximately(num, 0f))
			{
				return new Color(1f, 1f, 1f, c.a);
			}
			return new Color
			{
				r = c.r / num,
				g = c.g / num,
				b = c.b / num,
				a = c.a
			};
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0000B131 File Offset: 0x00009331
		private static Vector3 ClampVector(Vector3 v, float min, float max)
		{
			return new Vector3(Mathf.Clamp(v.x, min, max), Mathf.Clamp(v.y, min, max), Mathf.Clamp(v.z, min, max));
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0007E114 File Offset: 0x0007C314
		public static Vector3 GetLiftValue(Color lift)
		{
			Color color = ColorGradingComponent.NormalizeColor(lift);
			float num = (color.r + color.g + color.b) / 3f;
			float num2 = (color.r - num) * 0.1f + lift.a;
			float num3 = (color.g - num) * 0.1f + lift.a;
			float num4 = (color.b - num) * 0.1f + lift.a;
			return ColorGradingComponent.ClampVector(new Vector3(num2, num3, num4), -1f, 1f);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0007E19C File Offset: 0x0007C39C
		public static Vector3 GetGammaValue(Color gamma)
		{
			Color color = ColorGradingComponent.NormalizeColor(gamma);
			float num = (color.r + color.g + color.b) / 3f;
			gamma.a *= ((gamma.a >= 0f) ? 5f : 0.8f);
			float num2 = Mathf.Pow(2f, (color.r - num) * 0.5f) + gamma.a;
			float num3 = Mathf.Pow(2f, (color.g - num) * 0.5f) + gamma.a;
			float num4 = Mathf.Pow(2f, (color.b - num) * 0.5f) + gamma.a;
			float num5 = 1f / Mathf.Max(0.01f, num2);
			float num6 = 1f / Mathf.Max(0.01f, num3);
			float num7 = 1f / Mathf.Max(0.01f, num4);
			return ColorGradingComponent.ClampVector(new Vector3(num5, num6, num7), 0f, 5f);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0007E2A0 File Offset: 0x0007C4A0
		public static Vector3 GetGainValue(Color gain)
		{
			Color color = ColorGradingComponent.NormalizeColor(gain);
			float num = (color.r + color.g + color.b) / 3f;
			gain.a *= ((gain.a <= 0f) ? 1f : 3f);
			float num2 = Mathf.Pow(2f, (color.r - num) * 0.5f) + gain.a;
			float num3 = Mathf.Pow(2f, (color.g - num) * 0.5f) + gain.a;
			float num4 = Mathf.Pow(2f, (color.b - num) * 0.5f) + gain.a;
			return ColorGradingComponent.ClampVector(new Vector3(num2, num3, num4), 0f, 4f);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000B15F File Offset: 0x0000935F
		public static void CalculateLiftGammaGain(Color lift, Color gamma, Color gain, out Vector3 outLift, out Vector3 outGamma, out Vector3 outGain)
		{
			outLift = ColorGradingComponent.GetLiftValue(lift);
			outGamma = ColorGradingComponent.GetGammaValue(gamma);
			outGain = ColorGradingComponent.GetGainValue(gain);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0007E368 File Offset: 0x0007C568
		public static Vector3 GetSlopeValue(Color slope)
		{
			Color color = ColorGradingComponent.NormalizeColor(slope);
			float num = (color.r + color.g + color.b) / 3f;
			slope.a *= 0.5f;
			float num2 = (color.r - num) * 0.1f + slope.a + 1f;
			float num3 = (color.g - num) * 0.1f + slope.a + 1f;
			float num4 = (color.b - num) * 0.1f + slope.a + 1f;
			return ColorGradingComponent.ClampVector(new Vector3(num2, num3, num4), 0f, 2f);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0007E410 File Offset: 0x0007C610
		public static Vector3 GetPowerValue(Color power)
		{
			Color color = ColorGradingComponent.NormalizeColor(power);
			float num = (color.r + color.g + color.b) / 3f;
			power.a *= 0.5f;
			float num2 = (color.r - num) * 0.1f + power.a + 1f;
			float num3 = (color.g - num) * 0.1f + power.a + 1f;
			float num4 = (color.b - num) * 0.1f + power.a + 1f;
			float num5 = 1f / Mathf.Max(0.01f, num2);
			float num6 = 1f / Mathf.Max(0.01f, num3);
			float num7 = 1f / Mathf.Max(0.01f, num4);
			return ColorGradingComponent.ClampVector(new Vector3(num5, num6, num7), 0.5f, 2.5f);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0007E4F4 File Offset: 0x0007C6F4
		public static Vector3 GetOffsetValue(Color offset)
		{
			Color color = ColorGradingComponent.NormalizeColor(offset);
			float num = (color.r + color.g + color.b) / 3f;
			offset.a *= 0.5f;
			float num2 = (color.r - num) * 0.05f + offset.a;
			float num3 = (color.g - num) * 0.05f + offset.a;
			float num4 = (color.b - num) * 0.05f + offset.a;
			return ColorGradingComponent.ClampVector(new Vector3(num2, num3, num4), -0.8f, 0.8f);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000B187 File Offset: 0x00009387
		public static void CalculateSlopePowerOffset(Color slope, Color power, Color offset, out Vector3 outSlope, out Vector3 outPower, out Vector3 outOffset)
		{
			outSlope = ColorGradingComponent.GetSlopeValue(slope);
			outPower = ColorGradingComponent.GetPowerValue(power);
			outOffset = ColorGradingComponent.GetOffsetValue(offset);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0007E58C File Offset: 0x0007C78C
		private Texture2D GetCurveTexture()
		{
			if (this.m_GradingCurves == null)
			{
				this.m_GradingCurves = new Texture2D(128, 2, TextureFormat.RGBAHalf, false, true)
				{
					name = "Internal Curves Texture",
					hideFlags = HideFlags.DontSave,
					anisoLevel = 0,
					wrapMode = TextureWrapMode.Clamp,
					filterMode = FilterMode.Bilinear
				};
			}
			ColorGradingModel.CurvesSettings curves = base.model.settings.curves;
			curves.hueVShue.Cache();
			curves.hueVSsat.Cache();
			for (int i = 0; i < 128; i++)
			{
				float num = (float)i * 0.0078125f;
				float num2 = curves.hueVShue.Evaluate(num);
				float num3 = curves.hueVSsat.Evaluate(num);
				float num4 = curves.satVSsat.Evaluate(num);
				float num5 = curves.lumVSsat.Evaluate(num);
				this.m_pixels[i] = new Color(num2, num3, num4, num5);
				float num6 = curves.master.Evaluate(num);
				float num7 = curves.red.Evaluate(num);
				float num8 = curves.green.Evaluate(num);
				float num9 = curves.blue.Evaluate(num);
				this.m_pixels[i + 128] = new Color(num7, num8, num9, num6);
			}
			this.m_GradingCurves.SetPixels(this.m_pixels);
			this.m_GradingCurves.Apply(false, false);
			return this.m_GradingCurves;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000B1AF File Offset: 0x000093AF
		private bool IsLogLutValid(RenderTexture lut)
		{
			return lut != null && lut.IsCreated() && lut.height == 32;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0007E700 File Offset: 0x0007C900
		private void GenerateLut()
		{
			ColorGradingModel.Settings settings = base.model.settings;
			if (!this.IsLogLutValid(base.model.bakedLut))
			{
				GraphicsUtils.Destroy(base.model.bakedLut);
				base.model.bakedLut = new RenderTexture(1024, 32, 0, RenderTextureFormat.ARGBHalf)
				{
					name = "Color Grading Log LUT",
					hideFlags = HideFlags.DontSave,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0
				};
			}
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Lut Generator");
			material.SetVector(ColorGradingComponent.Uniforms._LutParams, new Vector4(32f, 0.00048828125f, 0.015625f, 1.032258f));
			material.shaderKeywords = null;
			ColorGradingModel.TonemappingSettings tonemapping = settings.tonemapping;
			ColorGradingModel.Tonemapper tonemapper = tonemapping.tonemapper;
			if (tonemapper != ColorGradingModel.Tonemapper.ACES)
			{
				if (tonemapper == ColorGradingModel.Tonemapper.Neutral)
				{
					material.EnableKeyword("TONEMAPPING_NEUTRAL");
					float num = tonemapping.neutralBlackIn * 20f + 1f;
					float num2 = tonemapping.neutralBlackOut * 10f + 1f;
					float num3 = tonemapping.neutralWhiteIn / 20f;
					float num4 = 1f - tonemapping.neutralWhiteOut / 20f;
					float num5 = num / num2;
					float num6 = num3 / num4;
					float num7 = Mathf.Max(0f, Mathf.LerpUnclamped(0.57f, 0.37f, num5));
					float num8 = Mathf.LerpUnclamped(0.01f, 0.24f, num6);
					float num9 = Mathf.Max(0f, Mathf.LerpUnclamped(0.02f, 0.2f, num5));
					material.SetVector(ColorGradingComponent.Uniforms._NeutralTonemapperParams1, new Vector4(0.2f, num7, num8, num9));
					material.SetVector(ColorGradingComponent.Uniforms._NeutralTonemapperParams2, new Vector4(0.02f, 0.3f, tonemapping.neutralWhiteLevel, tonemapping.neutralWhiteClip / 10f));
				}
			}
			else
			{
				material.EnableKeyword("TONEMAPPING_FILMIC");
			}
			material.SetFloat(ColorGradingComponent.Uniforms._HueShift, settings.basic.hueShift / 360f);
			material.SetFloat(ColorGradingComponent.Uniforms._Saturation, settings.basic.saturation);
			material.SetFloat(ColorGradingComponent.Uniforms._Contrast, settings.basic.contrast);
			material.SetVector(ColorGradingComponent.Uniforms._Balance, this.CalculateColorBalance(settings.basic.temperature, settings.basic.tint));
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			ColorGradingComponent.CalculateLiftGammaGain(settings.colorWheels.linear.lift, settings.colorWheels.linear.gamma, settings.colorWheels.linear.gain, out vector, out vector2, out vector3);
			material.SetVector(ColorGradingComponent.Uniforms._Lift, vector);
			material.SetVector(ColorGradingComponent.Uniforms._InvGamma, vector2);
			material.SetVector(ColorGradingComponent.Uniforms._Gain, vector3);
			Vector3 vector4;
			Vector3 vector5;
			Vector3 vector6;
			ColorGradingComponent.CalculateSlopePowerOffset(settings.colorWheels.log.slope, settings.colorWheels.log.power, settings.colorWheels.log.offset, out vector4, out vector5, out vector6);
			material.SetVector(ColorGradingComponent.Uniforms._Slope, vector4);
			material.SetVector(ColorGradingComponent.Uniforms._Power, vector5);
			material.SetVector(ColorGradingComponent.Uniforms._Offset, vector6);
			material.SetVector(ColorGradingComponent.Uniforms._ChannelMixerRed, settings.channelMixer.red);
			material.SetVector(ColorGradingComponent.Uniforms._ChannelMixerGreen, settings.channelMixer.green);
			material.SetVector(ColorGradingComponent.Uniforms._ChannelMixerBlue, settings.channelMixer.blue);
			material.SetTexture(ColorGradingComponent.Uniforms._Curves, this.GetCurveTexture());
			Graphics.Blit(null, base.model.bakedLut, material, 0);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0007EAA8 File Offset: 0x0007CCA8
		public override void Prepare(Material uberMaterial)
		{
			if (base.model.isDirty || !this.IsLogLutValid(base.model.bakedLut))
			{
				this.GenerateLut();
				base.model.isDirty = false;
			}
			uberMaterial.EnableKeyword((!this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.PreGradingLog)) ? "COLOR_GRADING" : "COLOR_GRADING_LOG_VIEW");
			RenderTexture bakedLut = base.model.bakedLut;
			uberMaterial.SetTexture(ColorGradingComponent.Uniforms._LogLut, bakedLut);
			uberMaterial.SetVector(ColorGradingComponent.Uniforms._LogLut_Params, new Vector3(1f / (float)bakedLut.width, 1f / (float)bakedLut.height, (float)bakedLut.height - 1f));
			float num = Mathf.Exp(base.model.settings.basic.postExposure * 0.6931472f);
			uberMaterial.SetFloat(ColorGradingComponent.Uniforms._ExposureEV, num);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0007EB94 File Offset: 0x0007CD94
		public void OnGUI()
		{
			RenderTexture bakedLut = base.model.bakedLut;
			GUI.DrawTexture(new Rect(this.context.viewport.x * (float)Screen.width + 8f, 8f, (float)bakedLut.width, (float)bakedLut.height), bakedLut);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0000B1CE File Offset: 0x000093CE
		public override void OnDisable()
		{
			GraphicsUtils.Destroy(this.m_GradingCurves);
			GraphicsUtils.Destroy(base.model.bakedLut);
			this.m_GradingCurves = null;
			base.model.bakedLut = null;
		}

		// Token: 0x040006B6 RID: 1718
		private const int k_InternalLogLutSize = 32;

		// Token: 0x040006B7 RID: 1719
		private const int k_CurvePrecision = 128;

		// Token: 0x040006B8 RID: 1720
		private const float k_CurveStep = 0.0078125f;

		// Token: 0x040006B9 RID: 1721
		private Texture2D m_GradingCurves;

		// Token: 0x040006BA RID: 1722
		private readonly Color[] m_pixels = new Color[256];

		// Token: 0x020001ED RID: 493
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x040006BB RID: 1723
			internal static readonly int _LutParams = Shader.PropertyToID("_LutParams");

			// Token: 0x040006BC RID: 1724
			internal static readonly int _NeutralTonemapperParams1 = Shader.PropertyToID("_NeutralTonemapperParams1");

			// Token: 0x040006BD RID: 1725
			internal static readonly int _NeutralTonemapperParams2 = Shader.PropertyToID("_NeutralTonemapperParams2");

			// Token: 0x040006BE RID: 1726
			internal static readonly int _HueShift = Shader.PropertyToID("_HueShift");

			// Token: 0x040006BF RID: 1727
			internal static readonly int _Saturation = Shader.PropertyToID("_Saturation");

			// Token: 0x040006C0 RID: 1728
			internal static readonly int _Contrast = Shader.PropertyToID("_Contrast");

			// Token: 0x040006C1 RID: 1729
			internal static readonly int _Balance = Shader.PropertyToID("_Balance");

			// Token: 0x040006C2 RID: 1730
			internal static readonly int _Lift = Shader.PropertyToID("_Lift");

			// Token: 0x040006C3 RID: 1731
			internal static readonly int _InvGamma = Shader.PropertyToID("_InvGamma");

			// Token: 0x040006C4 RID: 1732
			internal static readonly int _Gain = Shader.PropertyToID("_Gain");

			// Token: 0x040006C5 RID: 1733
			internal static readonly int _Slope = Shader.PropertyToID("_Slope");

			// Token: 0x040006C6 RID: 1734
			internal static readonly int _Power = Shader.PropertyToID("_Power");

			// Token: 0x040006C7 RID: 1735
			internal static readonly int _Offset = Shader.PropertyToID("_Offset");

			// Token: 0x040006C8 RID: 1736
			internal static readonly int _ChannelMixerRed = Shader.PropertyToID("_ChannelMixerRed");

			// Token: 0x040006C9 RID: 1737
			internal static readonly int _ChannelMixerGreen = Shader.PropertyToID("_ChannelMixerGreen");

			// Token: 0x040006CA RID: 1738
			internal static readonly int _ChannelMixerBlue = Shader.PropertyToID("_ChannelMixerBlue");

			// Token: 0x040006CB RID: 1739
			internal static readonly int _Curves = Shader.PropertyToID("_Curves");

			// Token: 0x040006CC RID: 1740
			internal static readonly int _LogLut = Shader.PropertyToID("_LogLut");

			// Token: 0x040006CD RID: 1741
			internal static readonly int _LogLut_Params = Shader.PropertyToID("_LogLut_Params");

			// Token: 0x040006CE RID: 1742
			internal static readonly int _ExposureEV = Shader.PropertyToID("_ExposureEV");
		}
	}
}
