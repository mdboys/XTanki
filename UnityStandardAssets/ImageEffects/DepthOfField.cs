using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000199 RID: 409
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Camera/Depth of Field (Lens Blur, Scatter, DX11)")]
	public class DepthOfField : PostEffectsBase
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0000A742 File Offset: 0x00008942
		private void OnEnable()
		{
			base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00076D68 File Offset: 0x00074F68
		private void OnDisable()
		{
			this.ReleaseComputeResources();
			if (this.dofHdrMaterial)
			{
				global::UnityEngine.Object.DestroyImmediate(this.dofHdrMaterial);
			}
			this.dofHdrMaterial = null;
			if (this.dx11bokehMaterial)
			{
				global::UnityEngine.Object.DestroyImmediate(this.dx11bokehMaterial);
			}
			this.dx11bokehMaterial = null;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00076DBC File Offset: 0x00074FBC
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.aperture < 0f)
			{
				this.aperture = 0f;
			}
			if (this.maxBlurSize < 0.1f)
			{
				this.maxBlurSize = 0.1f;
			}
			this.focalSize = Mathf.Clamp(this.focalSize, 0f, 2f);
			this.internalBlurWidth = Mathf.Max(this.maxBlurSize, 0f);
			this.focalDistance01 = ((!this.focalTransform) ? this.FocalDistance01(this.focalLength) : (base.GetComponent<Camera>().WorldToViewportPoint(this.focalTransform.position).z / base.GetComponent<Camera>().farClipPlane));
			this.dofHdrMaterial.SetVector("_CurveParams", new Vector4(1f, this.focalSize, this.aperture / 10f, this.focalDistance01));
			RenderTexture renderTexture = null;
			RenderTexture renderTexture2 = null;
			float num = this.internalBlurWidth * this.foregroundOverlap;
			if (this.visualizeFocus)
			{
				this.WriteCoc(source, true);
				Graphics.Blit(source, destination, this.dofHdrMaterial, 16);
			}
			else if (this.blurType == DepthOfField.BlurType.DX11 && this.dx11bokehMaterial)
			{
				if (this.highResolution)
				{
					this.internalBlurWidth = ((this.internalBlurWidth >= 0.1f) ? this.internalBlurWidth : 0.1f);
					num = this.internalBlurWidth * this.foregroundOverlap;
					renderTexture = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
					RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
					this.WriteCoc(source, false);
					RenderTexture renderTexture3 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					RenderTexture renderTexture4 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					Graphics.Blit(source, renderTexture3, this.dofHdrMaterial, 15);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, 1.5f, 0f, 1.5f));
					Graphics.Blit(renderTexture3, renderTexture4, this.dofHdrMaterial, 19);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(1.5f, 0f, 0f, 1.5f));
					Graphics.Blit(renderTexture4, renderTexture3, this.dofHdrMaterial, 19);
					if (this.nearBlur)
					{
						Graphics.Blit(source, renderTexture4, this.dofHdrMaterial, 4);
					}
					this.dx11bokehMaterial.SetTexture("_BlurredColor", renderTexture3);
					this.dx11bokehMaterial.SetFloat("_SpawnHeuristic", this.dx11SpawnHeuristic);
					this.dx11bokehMaterial.SetVector("_BokehParams", new Vector4(this.dx11BokehScale, this.dx11BokehIntensity, Mathf.Clamp(this.dx11BokehThreshold, 0.005f, 4f), this.internalBlurWidth));
					this.dx11bokehMaterial.SetTexture("_FgCocMask", (!this.nearBlur) ? null : renderTexture4);
					Graphics.SetRandomWriteTarget(1, this.cbPoints);
					Graphics.Blit(source, renderTexture, this.dx11bokehMaterial, 0);
					Graphics.ClearRandomWriteTargets();
					if (this.nearBlur)
					{
						this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, num, 0f, num));
						Graphics.Blit(renderTexture4, renderTexture3, this.dofHdrMaterial, 2);
						this.dofHdrMaterial.SetVector("_Offsets", new Vector4(num, 0f, 0f, num));
						Graphics.Blit(renderTexture3, renderTexture4, this.dofHdrMaterial, 2);
						Graphics.Blit(renderTexture4, renderTexture, this.dofHdrMaterial, 3);
					}
					Graphics.Blit(renderTexture, temporary, this.dofHdrMaterial, 20);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(this.internalBlurWidth, 0f, 0f, this.internalBlurWidth));
					Graphics.Blit(renderTexture, source, this.dofHdrMaterial, 5);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, this.internalBlurWidth, 0f, this.internalBlurWidth));
					Graphics.Blit(source, temporary, this.dofHdrMaterial, 21);
					Graphics.SetRenderTarget(temporary);
					ComputeBuffer.CopyCount(this.cbPoints, this.cbDrawArgs, 0);
					this.dx11bokehMaterial.SetBuffer("pointBuffer", this.cbPoints);
					this.dx11bokehMaterial.SetTexture("_MainTex", this.dx11BokehTexture);
					this.dx11bokehMaterial.SetVector("_Screen", new Vector3(1f / (1f * (float)source.width), 1f / (1f * (float)source.height), this.internalBlurWidth));
					this.dx11bokehMaterial.SetPass(2);
					Graphics.DrawProceduralIndirect(MeshTopology.Points, this.cbDrawArgs, 0);
					Graphics.Blit(temporary, destination);
					RenderTexture.ReleaseTemporary(temporary);
					RenderTexture.ReleaseTemporary(renderTexture3);
					RenderTexture.ReleaseTemporary(renderTexture4);
				}
				else
				{
					renderTexture = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					renderTexture2 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					num = this.internalBlurWidth * this.foregroundOverlap;
					this.WriteCoc(source, false);
					source.filterMode = FilterMode.Bilinear;
					Graphics.Blit(source, renderTexture, this.dofHdrMaterial, 6);
					RenderTexture renderTexture3 = RenderTexture.GetTemporary(renderTexture.width >> 1, renderTexture.height >> 1, 0, renderTexture.format);
					RenderTexture renderTexture4 = RenderTexture.GetTemporary(renderTexture.width >> 1, renderTexture.height >> 1, 0, renderTexture.format);
					Graphics.Blit(renderTexture, renderTexture3, this.dofHdrMaterial, 15);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, 1.5f, 0f, 1.5f));
					Graphics.Blit(renderTexture3, renderTexture4, this.dofHdrMaterial, 19);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(1.5f, 0f, 0f, 1.5f));
					Graphics.Blit(renderTexture4, renderTexture3, this.dofHdrMaterial, 19);
					RenderTexture renderTexture5 = null;
					if (this.nearBlur)
					{
						renderTexture5 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
						Graphics.Blit(source, renderTexture5, this.dofHdrMaterial, 4);
					}
					this.dx11bokehMaterial.SetTexture("_BlurredColor", renderTexture3);
					this.dx11bokehMaterial.SetFloat("_SpawnHeuristic", this.dx11SpawnHeuristic);
					this.dx11bokehMaterial.SetVector("_BokehParams", new Vector4(this.dx11BokehScale, this.dx11BokehIntensity, Mathf.Clamp(this.dx11BokehThreshold, 0.005f, 4f), this.internalBlurWidth));
					this.dx11bokehMaterial.SetTexture("_FgCocMask", renderTexture5);
					Graphics.SetRandomWriteTarget(1, this.cbPoints);
					Graphics.Blit(renderTexture, renderTexture2, this.dx11bokehMaterial, 0);
					Graphics.ClearRandomWriteTargets();
					RenderTexture.ReleaseTemporary(renderTexture3);
					RenderTexture.ReleaseTemporary(renderTexture4);
					if (this.nearBlur)
					{
						this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, num, 0f, num));
						Graphics.Blit(renderTexture5, renderTexture, this.dofHdrMaterial, 2);
						this.dofHdrMaterial.SetVector("_Offsets", new Vector4(num, 0f, 0f, num));
						Graphics.Blit(renderTexture, renderTexture5, this.dofHdrMaterial, 2);
						Graphics.Blit(renderTexture5, renderTexture2, this.dofHdrMaterial, 3);
					}
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(this.internalBlurWidth, 0f, 0f, this.internalBlurWidth));
					Graphics.Blit(renderTexture2, renderTexture, this.dofHdrMaterial, 5);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, this.internalBlurWidth, 0f, this.internalBlurWidth));
					Graphics.Blit(renderTexture, renderTexture2, this.dofHdrMaterial, 5);
					Graphics.SetRenderTarget(renderTexture2);
					ComputeBuffer.CopyCount(this.cbPoints, this.cbDrawArgs, 0);
					this.dx11bokehMaterial.SetBuffer("pointBuffer", this.cbPoints);
					this.dx11bokehMaterial.SetTexture("_MainTex", this.dx11BokehTexture);
					this.dx11bokehMaterial.SetVector("_Screen", new Vector3(1f / (1f * (float)renderTexture2.width), 1f / (1f * (float)renderTexture2.height), this.internalBlurWidth));
					this.dx11bokehMaterial.SetPass(1);
					Graphics.DrawProceduralIndirect(MeshTopology.Points, this.cbDrawArgs, 0);
					this.dofHdrMaterial.SetTexture("_LowRez", renderTexture2);
					this.dofHdrMaterial.SetTexture("_FgOverlap", renderTexture5);
					this.dofHdrMaterial.SetVector("_Offsets", 1f * (float)source.width / (1f * (float)renderTexture2.width) * this.internalBlurWidth * Vector4.one);
					Graphics.Blit(source, destination, this.dofHdrMaterial, 9);
					if (renderTexture5)
					{
						RenderTexture.ReleaseTemporary(renderTexture5);
					}
				}
			}
			else
			{
				source.filterMode = FilterMode.Bilinear;
				if (this.highResolution)
				{
					this.internalBlurWidth *= 2f;
				}
				this.WriteCoc(source, true);
				renderTexture = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				renderTexture2 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				int num2 = ((this.blurSampleCount != DepthOfField.BlurSampleCount.High && this.blurSampleCount != DepthOfField.BlurSampleCount.Medium) ? 11 : 17);
				if (this.highResolution)
				{
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, this.internalBlurWidth, 0.025f, this.internalBlurWidth));
					Graphics.Blit(source, destination, this.dofHdrMaterial, num2);
				}
				else
				{
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, this.internalBlurWidth, 0.1f, this.internalBlurWidth));
					Graphics.Blit(source, renderTexture, this.dofHdrMaterial, 6);
					Graphics.Blit(renderTexture, renderTexture2, this.dofHdrMaterial, num2);
					this.dofHdrMaterial.SetTexture("_LowRez", renderTexture2);
					this.dofHdrMaterial.SetTexture("_FgOverlap", null);
					this.dofHdrMaterial.SetVector("_Offsets", Vector4.one * (1f * (float)source.width / (1f * (float)renderTexture2.width)) * this.internalBlurWidth);
					Graphics.Blit(source, destination, this.dofHdrMaterial, (this.blurSampleCount != DepthOfField.BlurSampleCount.High) ? 12 : 18);
				}
			}
			if (renderTexture)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			if (renderTexture2)
			{
				RenderTexture.ReleaseTemporary(renderTexture2);
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00077864 File Offset: 0x00075A64
		public override bool CheckResources()
		{
			base.CheckSupport(true);
			this.dofHdrMaterial = base.CheckShaderAndCreateMaterial(this.dofHdrShader, this.dofHdrMaterial);
			if (this.supportDX11 && this.blurType == DepthOfField.BlurType.DX11)
			{
				this.dx11bokehMaterial = base.CheckShaderAndCreateMaterial(this.dx11BokehShader, this.dx11bokehMaterial);
				this.CreateComputeResources();
			}
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0000A757 File Offset: 0x00008957
		private void ReleaseComputeResources()
		{
			ComputeBuffer computeBuffer = this.cbDrawArgs;
			if (computeBuffer != null)
			{
				computeBuffer.Release();
			}
			this.cbDrawArgs = null;
			ComputeBuffer computeBuffer2 = this.cbPoints;
			if (computeBuffer2 != null)
			{
				computeBuffer2.Release();
			}
			this.cbPoints = null;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000778D4 File Offset: 0x00075AD4
		private void CreateComputeResources()
		{
			if (this.cbDrawArgs == null)
			{
				this.cbDrawArgs = new ComputeBuffer(1, 16, ComputeBufferType.DrawIndirect);
				int[] array = new int[4];
				array[1] = 1;
				int[] array2 = array;
				this.cbDrawArgs.SetData(array2);
			}
			if (this.cbPoints == null)
			{
				this.cbPoints = new ComputeBuffer(90000, 28, ComputeBufferType.Append);
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00077930 File Offset: 0x00075B30
		private float FocalDistance01(float worldDist)
		{
			return base.GetComponent<Camera>().WorldToViewportPoint((worldDist - base.GetComponent<Camera>().nearClipPlane) * base.GetComponent<Camera>().transform.forward + base.GetComponent<Camera>().transform.position).z / (base.GetComponent<Camera>().farClipPlane - base.GetComponent<Camera>().nearClipPlane);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0007799C File Offset: 0x00075B9C
		private void WriteCoc(RenderTexture fromTo, bool fgDilate)
		{
			this.dofHdrMaterial.SetTexture("_FgOverlap", null);
			if (this.nearBlur && fgDilate)
			{
				int num = fromTo.width / 2;
				int num2 = fromTo.height / 2;
				RenderTexture renderTexture = RenderTexture.GetTemporary(num, num2, 0, fromTo.format);
				Graphics.Blit(fromTo, renderTexture, this.dofHdrMaterial, 4);
				float num3 = this.internalBlurWidth * this.foregroundOverlap;
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, num3, 0f, num3));
				RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, fromTo.format);
				Graphics.Blit(renderTexture, temporary, this.dofHdrMaterial, 2);
				RenderTexture.ReleaseTemporary(renderTexture);
				this.dofHdrMaterial.SetVector("_Offsets", new Vector4(num3, 0f, 0f, num3));
				renderTexture = RenderTexture.GetTemporary(num, num2, 0, fromTo.format);
				Graphics.Blit(temporary, renderTexture, this.dofHdrMaterial, 2);
				RenderTexture.ReleaseTemporary(temporary);
				this.dofHdrMaterial.SetTexture("_FgOverlap", renderTexture);
				fromTo.MarkRestoreExpected();
				Graphics.Blit(fromTo, fromTo, this.dofHdrMaterial, 13);
				RenderTexture.ReleaseTemporary(renderTexture);
				return;
			}
			fromTo.MarkRestoreExpected();
			Graphics.Blit(fromTo, fromTo, this.dofHdrMaterial, 0);
		}

		// Token: 0x040004F1 RID: 1265
		public bool visualizeFocus;

		// Token: 0x040004F2 RID: 1266
		public float focalLength = 10f;

		// Token: 0x040004F3 RID: 1267
		public float focalSize = 0.05f;

		// Token: 0x040004F4 RID: 1268
		public float aperture = 11.5f;

		// Token: 0x040004F5 RID: 1269
		public Transform focalTransform;

		// Token: 0x040004F6 RID: 1270
		public float maxBlurSize = 2f;

		// Token: 0x040004F7 RID: 1271
		public bool highResolution;

		// Token: 0x040004F8 RID: 1272
		public DepthOfField.BlurType blurType;

		// Token: 0x040004F9 RID: 1273
		public DepthOfField.BlurSampleCount blurSampleCount = DepthOfField.BlurSampleCount.High;

		// Token: 0x040004FA RID: 1274
		public bool nearBlur;

		// Token: 0x040004FB RID: 1275
		public float foregroundOverlap = 1f;

		// Token: 0x040004FC RID: 1276
		public Shader dofHdrShader;

		// Token: 0x040004FD RID: 1277
		public Shader dx11BokehShader;

		// Token: 0x040004FE RID: 1278
		public float dx11BokehThreshold = 0.5f;

		// Token: 0x040004FF RID: 1279
		public float dx11SpawnHeuristic = 0.0875f;

		// Token: 0x04000500 RID: 1280
		public Texture2D dx11BokehTexture;

		// Token: 0x04000501 RID: 1281
		public float dx11BokehScale = 1.2f;

		// Token: 0x04000502 RID: 1282
		public float dx11BokehIntensity = 2.5f;

		// Token: 0x04000503 RID: 1283
		private ComputeBuffer cbDrawArgs;

		// Token: 0x04000504 RID: 1284
		private ComputeBuffer cbPoints;

		// Token: 0x04000505 RID: 1285
		private Material dofHdrMaterial;

		// Token: 0x04000506 RID: 1286
		private Material dx11bokehMaterial;

		// Token: 0x04000507 RID: 1287
		private float focalDistance01 = 10f;

		// Token: 0x04000508 RID: 1288
		private float internalBlurWidth = 1f;

		// Token: 0x0200019A RID: 410
		[NullableContext(0)]
		public enum BlurSampleCount
		{
			// Token: 0x0400050A RID: 1290
			Low,
			// Token: 0x0400050B RID: 1291
			Medium,
			// Token: 0x0400050C RID: 1292
			High
		}

		// Token: 0x0200019B RID: 411
		[NullableContext(0)]
		public enum BlurType
		{
			// Token: 0x0400050E RID: 1294
			DiscBlur,
			// Token: 0x0400050F RID: 1295
			DX11
		}
	}
}
