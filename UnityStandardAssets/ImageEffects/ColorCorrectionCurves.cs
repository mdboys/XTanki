using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000192 RID: 402
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (Curves, Saturation)")]
	public class ColorCorrectionCurves : PostEffectsBase
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x0000568E File Offset: 0x0000388E
		private void Awake()
		{
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0000A510 File Offset: 0x00008710
		private new void Start()
		{
			base.Start();
			this.updateTexturesOnStartup = true;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00075DB8 File Offset: 0x00073FB8
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.updateTexturesOnStartup)
			{
				this.UpdateParameters();
				this.updateTexturesOnStartup = false;
			}
			if (this.useDepthCorrection)
			{
				base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
			}
			RenderTexture renderTexture = destination;
			if (this.selectiveCc)
			{
				renderTexture = RenderTexture.GetTemporary(source.width, source.height);
			}
			if (this.useDepthCorrection)
			{
				this.ccDepthMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
				this.ccDepthMaterial.SetTexture("_ZCurve", this.zCurveTex);
				this.ccDepthMaterial.SetTexture("_RgbDepthTex", this.rgbDepthChannelTex);
				this.ccDepthMaterial.SetFloat("_Saturation", this.saturation);
				Graphics.Blit(source, renderTexture, this.ccDepthMaterial);
			}
			else
			{
				this.ccMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
				this.ccMaterial.SetFloat("_Saturation", this.saturation);
				Graphics.Blit(source, renderTexture, this.ccMaterial);
			}
			if (this.selectiveCc)
			{
				this.selectiveCcMaterial.SetColor("selColor", this.selectiveFromColor);
				this.selectiveCcMaterial.SetColor("targetColor", this.selectiveToColor);
				Graphics.Blit(renderTexture, destination, this.selectiveCcMaterial);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00075F10 File Offset: 0x00074110
		public override bool CheckResources()
		{
			base.CheckSupport(this.mode == ColorCorrectionCurves.ColorCorrectionMode.Advanced);
			this.ccMaterial = base.CheckShaderAndCreateMaterial(this.simpleColorCorrectionCurvesShader, this.ccMaterial);
			this.ccDepthMaterial = base.CheckShaderAndCreateMaterial(this.colorCorrectionCurvesShader, this.ccDepthMaterial);
			this.selectiveCcMaterial = base.CheckShaderAndCreateMaterial(this.colorCorrectionSelectiveShader, this.selectiveCcMaterial);
			if (!this.rgbChannelTex)
			{
				this.rgbChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
			}
			if (!this.rgbDepthChannelTex)
			{
				this.rgbDepthChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
			}
			if (!this.zCurveTex)
			{
				this.zCurveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
			}
			this.rgbChannelTex.hideFlags = HideFlags.DontSave;
			this.rgbDepthChannelTex.hideFlags = HideFlags.DontSave;
			this.zCurveTex.hideFlags = HideFlags.DontSave;
			this.rgbChannelTex.wrapMode = TextureWrapMode.Clamp;
			this.rgbDepthChannelTex.wrapMode = TextureWrapMode.Clamp;
			this.zCurveTex.wrapMode = TextureWrapMode.Clamp;
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00076038 File Offset: 0x00074238
		public void UpdateParameters()
		{
			this.CheckResources();
			if (this.redChannel != null && this.greenChannel != null && this.blueChannel != null)
			{
				for (float num = 0f; num <= 1f; num += 0.003921569f)
				{
					float num2 = Mathf.Clamp(this.redChannel.Evaluate(num), 0f, 1f);
					float num3 = Mathf.Clamp(this.greenChannel.Evaluate(num), 0f, 1f);
					float num4 = Mathf.Clamp(this.blueChannel.Evaluate(num), 0f, 1f);
					this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
					this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
					this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
					float num5 = Mathf.Clamp(this.zCurve.Evaluate(num), 0f, 1f);
					this.zCurveTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num5, num5, num5));
					num2 = Mathf.Clamp(this.depthRedChannel.Evaluate(num), 0f, 1f);
					num3 = Mathf.Clamp(this.depthGreenChannel.Evaluate(num), 0f, 1f);
					num4 = Mathf.Clamp(this.depthBlueChannel.Evaluate(num), 0f, 1f);
					this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
					this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
					this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
				}
				this.rgbChannelTex.Apply();
				this.rgbDepthChannelTex.Apply();
				this.zCurveTex.Apply();
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0000A51F File Offset: 0x0000871F
		private void UpdateTextures()
		{
			this.UpdateParameters();
		}

		// Token: 0x040004B4 RID: 1204
		public AnimationCurve redChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040004B5 RID: 1205
		public AnimationCurve greenChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040004B6 RID: 1206
		public AnimationCurve blueChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040004B7 RID: 1207
		public bool useDepthCorrection;

		// Token: 0x040004B8 RID: 1208
		public AnimationCurve zCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040004B9 RID: 1209
		public AnimationCurve depthRedChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040004BA RID: 1210
		public AnimationCurve depthGreenChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040004BB RID: 1211
		public AnimationCurve depthBlueChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040004BC RID: 1212
		public float saturation = 1f;

		// Token: 0x040004BD RID: 1213
		public bool selectiveCc;

		// Token: 0x040004BE RID: 1214
		public Color selectiveFromColor = Color.white;

		// Token: 0x040004BF RID: 1215
		public Color selectiveToColor = Color.white;

		// Token: 0x040004C0 RID: 1216
		public ColorCorrectionCurves.ColorCorrectionMode mode;

		// Token: 0x040004C1 RID: 1217
		public bool updateTextures = true;

		// Token: 0x040004C2 RID: 1218
		public Shader colorCorrectionCurvesShader;

		// Token: 0x040004C3 RID: 1219
		public Shader simpleColorCorrectionCurvesShader;

		// Token: 0x040004C4 RID: 1220
		public Shader colorCorrectionSelectiveShader;

		// Token: 0x040004C5 RID: 1221
		private Material ccDepthMaterial;

		// Token: 0x040004C6 RID: 1222
		private Material ccMaterial;

		// Token: 0x040004C7 RID: 1223
		private Texture2D rgbChannelTex;

		// Token: 0x040004C8 RID: 1224
		private Texture2D rgbDepthChannelTex;

		// Token: 0x040004C9 RID: 1225
		private Material selectiveCcMaterial;

		// Token: 0x040004CA RID: 1226
		private bool updateTexturesOnStartup = true;

		// Token: 0x040004CB RID: 1227
		private Texture2D zCurveTex;

		// Token: 0x02000193 RID: 403
		[NullableContext(0)]
		public enum ColorCorrectionMode
		{
			// Token: 0x040004CD RID: 1229
			Simple,
			// Token: 0x040004CE RID: 1230
			Advanced
		}
	}
}
