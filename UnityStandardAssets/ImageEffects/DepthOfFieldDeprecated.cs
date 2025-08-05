﻿using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200019C RID: 412
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Camera/Depth of Field (deprecated)")]
	public class DepthOfFieldDeprecated : PostEffectsBase
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x0000A789 File Offset: 0x00008989
		private void OnEnable()
		{
			this._camera = base.GetComponent<Camera>();
			this._camera.depthTextureMode |= DepthTextureMode.Depth;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0000A7AA File Offset: 0x000089AA
		private void OnDisable()
		{
			Quads.Cleanup();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00077B60 File Offset: 0x00075D60
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.smoothness < 0.1f)
			{
				this.smoothness = 0.1f;
			}
			this.bokeh = this.bokeh && this.bokehSupport;
			float num = ((!this.bokeh) ? 1f : DepthOfFieldDeprecated.BOKEH_EXTRA_BLUR);
			bool flag = this.quality > DepthOfFieldDeprecated.Dof34QualitySetting.OnlyBackground;
			float num2 = this.focalSize / (this._camera.farClipPlane - this._camera.nearClipPlane);
			if (this.simpleTweakMode)
			{
				this.focalDistance01 = ((!this.objectFocus) ? this.FocalDistance01(this.focalPoint) : (this._camera.WorldToViewportPoint(this.objectFocus.position).z / this._camera.farClipPlane));
				this.focalStartCurve = this.focalDistance01 * this.smoothness;
				this.focalEndCurve = this.focalStartCurve;
				flag = flag && this.focalPoint > this._camera.nearClipPlane + Mathf.Epsilon;
			}
			else
			{
				if (this.objectFocus)
				{
					Vector3 vector = this._camera.WorldToViewportPoint(this.objectFocus.position);
					vector.z /= this._camera.farClipPlane;
					this.focalDistance01 = vector.z;
				}
				else
				{
					this.focalDistance01 = this.FocalDistance01(this.focalZDistance);
				}
				this.focalStartCurve = this.focalZStartCurve;
				this.focalEndCurve = this.focalZEndCurve;
				flag = flag && this.focalPoint > this._camera.nearClipPlane + Mathf.Epsilon;
			}
			this.widthOverHeight = 1f * (float)source.width / (1f * (float)source.height);
			this.oneOverBaseSize = 0.001953125f;
			this.dofMaterial.SetFloat("_ForegroundBlurExtrude", this.foregroundBlurExtrude);
			this.dofMaterial.SetVector("_CurveParams", new Vector4((!this.simpleTweakMode) ? this.focalStartCurve : (1f / this.focalStartCurve), (!this.simpleTweakMode) ? this.focalEndCurve : (1f / this.focalEndCurve), num2 * 0.5f, this.focalDistance01));
			this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)source.width), 1f / (1f * (float)source.height), 0f, 0f));
			int dividerBasedOnQuality = this.GetDividerBasedOnQuality();
			int lowResolutionDividerBasedOnQuality = this.GetLowResolutionDividerBasedOnQuality(dividerBasedOnQuality);
			this.AllocateTextures(flag, source, dividerBasedOnQuality, lowResolutionDividerBasedOnQuality);
			Graphics.Blit(source, source, this.dofMaterial, 3);
			this.Downsample(source, this.mediumRezWorkTexture);
			this.Blur(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DepthOfFieldDeprecated.DofBlurriness.Low, 4, this.maxBlurSpread);
			if (this.bokeh && (DepthOfFieldDeprecated.BokehDestination.Foreground & this.bokehDestination) != (DepthOfFieldDeprecated.BokehDestination)0)
			{
				this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThresholdContrast, this.bokehThresholdLuminance, 0.95f, 0f));
				Graphics.Blit(this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
				Graphics.Blit(this.mediumRezWorkTexture, this.lowRezWorkTexture);
				this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread * num);
			}
			else
			{
				this.Downsample(this.mediumRezWorkTexture, this.lowRezWorkTexture);
				this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread);
			}
			this.dofBlurMaterial.SetTexture("_TapLow", this.lowRezWorkTexture);
			this.dofBlurMaterial.SetTexture("_TapMedium", this.mediumRezWorkTexture);
			Graphics.Blit(null, this.finalDefocus, this.dofBlurMaterial, 3);
			if (this.bokeh && (DepthOfFieldDeprecated.BokehDestination.Foreground & this.bokehDestination) != (DepthOfFieldDeprecated.BokehDestination)0)
			{
				this.AddBokeh(this.bokehSource2, this.bokehSource, this.finalDefocus);
			}
			this.dofMaterial.SetTexture("_TapLowBackground", this.finalDefocus);
			this.dofMaterial.SetTexture("_TapMedium", this.mediumRezWorkTexture);
			Graphics.Blit(source, (!flag) ? destination : this.foregroundTexture, this.dofMaterial, this.visualize ? 2 : 0);
			if (flag)
			{
				Graphics.Blit(this.foregroundTexture, source, this.dofMaterial, 5);
				this.Downsample(source, this.mediumRezWorkTexture);
				this.BlurFg(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DepthOfFieldDeprecated.DofBlurriness.Low, 2, this.maxBlurSpread);
				if (this.bokeh && (DepthOfFieldDeprecated.BokehDestination.Foreground & this.bokehDestination) != (DepthOfFieldDeprecated.BokehDestination)0)
				{
					this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThresholdContrast * 0.5f, this.bokehThresholdLuminance, 0f, 0f));
					Graphics.Blit(this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
					Graphics.Blit(this.mediumRezWorkTexture, this.lowRezWorkTexture);
					this.BlurFg(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread * num);
				}
				else
				{
					this.BlurFg(this.mediumRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread);
				}
				Graphics.Blit(this.lowRezWorkTexture, this.finalDefocus);
				this.dofMaterial.SetTexture("_TapLowForeground", this.finalDefocus);
				Graphics.Blit(source, destination, this.dofMaterial, this.visualize ? 1 : 4);
				if (this.bokeh && (DepthOfFieldDeprecated.BokehDestination.Foreground & this.bokehDestination) != (DepthOfFieldDeprecated.BokehDestination)0)
				{
					this.AddBokeh(this.bokehSource2, this.bokehSource, destination);
				}
			}
			this.ReleaseTextures();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0007811C File Offset: 0x0007631C
		private void CreateMaterials()
		{
			this.dofBlurMaterial = base.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
			this.dofMaterial = base.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
			this.bokehSupport = this.bokehShader.isSupported;
			if (this.bokeh && this.bokehSupport && this.bokehShader)
			{
				this.bokehMaterial = base.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000781A0 File Offset: 0x000763A0
		public override bool CheckResources()
		{
			base.CheckSupport(true);
			this.dofBlurMaterial = base.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
			this.dofMaterial = base.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
			this.bokehSupport = this.bokehShader.isSupported;
			if (this.bokeh && this.bokehSupport && this.bokehShader)
			{
				this.bokehMaterial = base.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
			}
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00078240 File Offset: 0x00076440
		private float FocalDistance01(float worldDist)
		{
			return this._camera.WorldToViewportPoint((worldDist - this._camera.nearClipPlane) * this._camera.transform.forward + this._camera.transform.position).z / (this._camera.farClipPlane - this._camera.nearClipPlane);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000782AC File Offset: 0x000764AC
		private int GetDividerBasedOnQuality()
		{
			int num = 1;
			if (this.resolution == DepthOfFieldDeprecated.DofResolution.Medium)
			{
				num = 2;
			}
			else if (this.resolution == DepthOfFieldDeprecated.DofResolution.Low)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000782D4 File Offset: 0x000764D4
		private int GetLowResolutionDividerBasedOnQuality(int baseDivider)
		{
			int num = baseDivider;
			if (this.resolution == DepthOfFieldDeprecated.DofResolution.High)
			{
				num *= 2;
			}
			if (this.resolution == DepthOfFieldDeprecated.DofResolution.Low)
			{
				num *= 2;
			}
			return num;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00078300 File Offset: 0x00076500
		private void Blur(RenderTexture from, RenderTexture to, DepthOfFieldDeprecated.DofBlurriness iterations, int blurPass, float spread)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(to.width, to.height);
			if (iterations > DepthOfFieldDeprecated.DofBlurriness.Low)
			{
				this.BlurHex(from, to, blurPass, spread, temporary);
				if (iterations > DepthOfFieldDeprecated.DofBlurriness.High)
				{
					this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
					Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
					this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
					Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
				}
			}
			else
			{
				this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
				Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
				this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
				Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			}
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00078440 File Offset: 0x00076640
		private void BlurFg(RenderTexture from, RenderTexture to, DepthOfFieldDeprecated.DofBlurriness iterations, int blurPass, float spread)
		{
			this.dofBlurMaterial.SetTexture("_TapHigh", from);
			RenderTexture temporary = RenderTexture.GetTemporary(to.width, to.height);
			if (iterations > DepthOfFieldDeprecated.DofBlurriness.Low)
			{
				this.BlurHex(from, to, blurPass, spread, temporary);
				if (iterations > DepthOfFieldDeprecated.DofBlurriness.High)
				{
					this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
					Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
					this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
					Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
				}
			}
			else
			{
				this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
				Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
				this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
				Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			}
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00078594 File Offset: 0x00076794
		private void BlurHex(RenderTexture from, RenderTexture to, int blurPass, float spread, RenderTexture tmp)
		{
			this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
			Graphics.Blit(from, tmp, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
			Graphics.Blit(tmp, to, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, spread * this.oneOverBaseSize, 0f, 0f));
			Graphics.Blit(to, tmp, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (0f - spread) * this.oneOverBaseSize, 0f, 0f));
			Graphics.Blit(tmp, to, this.dofBlurMaterial, blurPass);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000786B4 File Offset: 0x000768B4
		private void Downsample(RenderTexture from, RenderTexture to)
		{
			this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)to.width), 1f / (1f * (float)to.height), 0f, 0f));
			Graphics.Blit(from, to, this.dofMaterial, DepthOfFieldDeprecated.SMOOTH_DOWNSAMPLE_PASS);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00078718 File Offset: 0x00076918
		private void AddBokeh(RenderTexture bokehInfo, RenderTexture tempTex, RenderTexture finalTarget)
		{
			if (!this.bokehMaterial)
			{
				return;
			}
			Mesh[] meshes = Quads.GetMeshes(tempTex.width, tempTex.height);
			RenderTexture.active = tempTex;
			GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
			GL.PushMatrix();
			GL.LoadIdentity();
			bokehInfo.filterMode = FilterMode.Point;
			float num = (float)bokehInfo.width * 1f / ((float)bokehInfo.height * 1f);
			float num2 = 2f / (1f * (float)bokehInfo.width);
			num2 += this.bokehScale * this.maxBlurSpread * DepthOfFieldDeprecated.BOKEH_EXTRA_BLUR * this.oneOverBaseSize;
			this.bokehMaterial.SetTexture("_Source", bokehInfo);
			this.bokehMaterial.SetTexture("_MainTex", this.bokehTexture);
			this.bokehMaterial.SetVector("_ArScale", new Vector4(num2, num2 * num, 0.5f, 0.5f * num));
			this.bokehMaterial.SetFloat("_Intensity", this.bokehIntensity);
			this.bokehMaterial.SetPass(0);
			foreach (Mesh mesh in meshes)
			{
				if (mesh)
				{
					Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
				}
			}
			GL.PopMatrix();
			Graphics.Blit(tempTex, finalTarget, this.dofMaterial, 8);
			bokehInfo.filterMode = FilterMode.Bilinear;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0007887C File Offset: 0x00076A7C
		private void ReleaseTextures()
		{
			if (this.foregroundTexture)
			{
				RenderTexture.ReleaseTemporary(this.foregroundTexture);
			}
			if (this.finalDefocus)
			{
				RenderTexture.ReleaseTemporary(this.finalDefocus);
			}
			if (this.mediumRezWorkTexture)
			{
				RenderTexture.ReleaseTemporary(this.mediumRezWorkTexture);
			}
			if (this.lowRezWorkTexture)
			{
				RenderTexture.ReleaseTemporary(this.lowRezWorkTexture);
			}
			if (this.bokehSource)
			{
				RenderTexture.ReleaseTemporary(this.bokehSource);
			}
			if (this.bokehSource2)
			{
				RenderTexture.ReleaseTemporary(this.bokehSource2);
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0007891C File Offset: 0x00076B1C
		private void AllocateTextures(bool blurForeground, RenderTexture source, int divider, int lowTexDivider)
		{
			this.foregroundTexture = null;
			if (blurForeground)
			{
				this.foregroundTexture = RenderTexture.GetTemporary(source.width, source.height, 0);
			}
			this.mediumRezWorkTexture = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
			this.finalDefocus = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
			this.lowRezWorkTexture = RenderTexture.GetTemporary(source.width / lowTexDivider, source.height / lowTexDivider, 0);
			this.bokehSource = null;
			this.bokehSource2 = null;
			if (this.bokeh)
			{
				this.bokehSource = RenderTexture.GetTemporary(source.width / (lowTexDivider * this.bokehDownsample), source.height / (lowTexDivider * this.bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
				this.bokehSource2 = RenderTexture.GetTemporary(source.width / (lowTexDivider * this.bokehDownsample), source.height / (lowTexDivider * this.bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
				this.bokehSource.filterMode = FilterMode.Bilinear;
				this.bokehSource2.filterMode = FilterMode.Bilinear;
				RenderTexture.active = this.bokehSource2;
				GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
			}
			source.filterMode = FilterMode.Bilinear;
			this.finalDefocus.filterMode = FilterMode.Bilinear;
			this.mediumRezWorkTexture.filterMode = FilterMode.Bilinear;
			this.lowRezWorkTexture.filterMode = FilterMode.Bilinear;
			if (this.foregroundTexture)
			{
				this.foregroundTexture.filterMode = FilterMode.Bilinear;
			}
		}

		// Token: 0x04000510 RID: 1296
		private static readonly int SMOOTH_DOWNSAMPLE_PASS = 6;

		// Token: 0x04000511 RID: 1297
		private static readonly float BOKEH_EXTRA_BLUR = 2f;

		// Token: 0x04000512 RID: 1298
		public DepthOfFieldDeprecated.Dof34QualitySetting quality = DepthOfFieldDeprecated.Dof34QualitySetting.OnlyBackground;

		// Token: 0x04000513 RID: 1299
		public DepthOfFieldDeprecated.DofResolution resolution = DepthOfFieldDeprecated.DofResolution.Low;

		// Token: 0x04000514 RID: 1300
		public bool simpleTweakMode = true;

		// Token: 0x04000515 RID: 1301
		public float focalPoint = 1f;

		// Token: 0x04000516 RID: 1302
		public float smoothness = 0.5f;

		// Token: 0x04000517 RID: 1303
		public float focalZDistance;

		// Token: 0x04000518 RID: 1304
		public float focalZStartCurve = 1f;

		// Token: 0x04000519 RID: 1305
		public float focalZEndCurve = 1f;

		// Token: 0x0400051A RID: 1306
		public Transform objectFocus;

		// Token: 0x0400051B RID: 1307
		public float focalSize;

		// Token: 0x0400051C RID: 1308
		public DepthOfFieldDeprecated.DofBlurriness bluriness = DepthOfFieldDeprecated.DofBlurriness.High;

		// Token: 0x0400051D RID: 1309
		public float maxBlurSpread = 1.75f;

		// Token: 0x0400051E RID: 1310
		public float foregroundBlurExtrude = 1.15f;

		// Token: 0x0400051F RID: 1311
		public Shader dofBlurShader;

		// Token: 0x04000520 RID: 1312
		public Shader dofShader;

		// Token: 0x04000521 RID: 1313
		public bool visualize;

		// Token: 0x04000522 RID: 1314
		public DepthOfFieldDeprecated.BokehDestination bokehDestination = DepthOfFieldDeprecated.BokehDestination.Background;

		// Token: 0x04000523 RID: 1315
		public bool bokeh;

		// Token: 0x04000524 RID: 1316
		public bool bokehSupport = true;

		// Token: 0x04000525 RID: 1317
		public Shader bokehShader;

		// Token: 0x04000526 RID: 1318
		public Texture2D bokehTexture;

		// Token: 0x04000527 RID: 1319
		public float bokehScale = 2.4f;

		// Token: 0x04000528 RID: 1320
		public float bokehIntensity = 0.15f;

		// Token: 0x04000529 RID: 1321
		public float bokehThresholdContrast = 0.1f;

		// Token: 0x0400052A RID: 1322
		public float bokehThresholdLuminance = 0.55f;

		// Token: 0x0400052B RID: 1323
		public int bokehDownsample = 1;

		// Token: 0x0400052C RID: 1324
		private Camera _camera;

		// Token: 0x0400052D RID: 1325
		private Material bokehMaterial;

		// Token: 0x0400052E RID: 1326
		private RenderTexture bokehSource;

		// Token: 0x0400052F RID: 1327
		private RenderTexture bokehSource2;

		// Token: 0x04000530 RID: 1328
		private Material dofBlurMaterial;

		// Token: 0x04000531 RID: 1329
		private Material dofMaterial;

		// Token: 0x04000532 RID: 1330
		private RenderTexture finalDefocus;

		// Token: 0x04000533 RID: 1331
		private float focalDistance01 = 0.1f;

		// Token: 0x04000534 RID: 1332
		private float focalEndCurve = 2f;

		// Token: 0x04000535 RID: 1333
		private float focalStartCurve = 2f;

		// Token: 0x04000536 RID: 1334
		private RenderTexture foregroundTexture;

		// Token: 0x04000537 RID: 1335
		private RenderTexture lowRezWorkTexture;

		// Token: 0x04000538 RID: 1336
		private RenderTexture mediumRezWorkTexture;

		// Token: 0x04000539 RID: 1337
		private float oneOverBaseSize = 0.001953125f;

		// Token: 0x0400053A RID: 1338
		private float widthOverHeight = 1.25f;

		// Token: 0x0200019D RID: 413
		[NullableContext(0)]
		public enum BokehDestination
		{
			// Token: 0x0400053C RID: 1340
			Background = 1,
			// Token: 0x0400053D RID: 1341
			Foreground,
			// Token: 0x0400053E RID: 1342
			BackgroundAndForeground
		}

		// Token: 0x0200019E RID: 414
		[NullableContext(0)]
		public enum Dof34QualitySetting
		{
			// Token: 0x04000540 RID: 1344
			OnlyBackground = 1,
			// Token: 0x04000541 RID: 1345
			BackgroundAndForeground
		}

		// Token: 0x0200019F RID: 415
		[NullableContext(0)]
		public enum DofBlurriness
		{
			// Token: 0x04000543 RID: 1347
			Low = 1,
			// Token: 0x04000544 RID: 1348
			High,
			// Token: 0x04000545 RID: 1349
			VeryHigh = 4
		}

		// Token: 0x020001A0 RID: 416
		[NullableContext(0)]
		public enum DofResolution
		{
			// Token: 0x04000547 RID: 1351
			High = 2,
			// Token: 0x04000548 RID: 1352
			Medium,
			// Token: 0x04000549 RID: 1353
			Low
		}
	}
}
