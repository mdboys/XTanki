using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200006A RID: 106
[NullableContext(1)]
[Nullable(0)]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Sonic Ether/SESSAO")]
public class SESSAO : MonoBehaviour
{
	// Token: 0x060001FB RID: 507 RVA: 0x0000683A File Offset: 0x00004A3A
	private void Start()
	{
		this.CheckInit();
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0006506C File Offset: 0x0006326C
	private void Update()
	{
		this.drawDistance = Mathf.Max(0f, this.drawDistance);
		this.drawDistanceFadeSize = Mathf.Max(0.001f, this.drawDistanceFadeSize);
		this.bilateralDepthTolerance = Mathf.Max(1E-06f, this.bilateralDepthTolerance);
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000683A File Offset: 0x00004A3A
	private void OnEnable()
	{
		this.CheckInit();
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00006842 File Offset: 0x00004A42
	private void OnDisable()
	{
		this.Cleanup();
	}

	// Token: 0x060001FF RID: 511 RVA: 0x000650BC File Offset: 0x000632BC
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.CheckInit();
		if (this.skipThisFrame)
		{
			Graphics.Blit(source, destination);
			return;
		}
		this.material.hideFlags = HideFlags.HideAndDontSave;
		this.material.SetTexture("_DitherTexture", (!this.preserveDetails) ? this.ditherTexture : this.ditherTextureSmall);
		this.material.SetInt("PreserveDetails", (this.preserveDetails > false) ? 1 : 0);
		this.material.SetMatrix("ProjectionMatrixInverse", base.GetComponent<Camera>().projectionMatrix.inverse);
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBHalf);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBHalf);
		RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, source.format);
		temporary3.wrapMode = TextureWrapMode.Clamp;
		temporary3.filterMode = FilterMode.Bilinear;
		Graphics.Blit(source, temporary3);
		this.material.SetTexture("_ColorDownsampled", temporary3);
		RenderTexture renderTexture = null;
		this.material.SetFloat("Radius", this.radius);
		this.material.SetFloat("Bias", this.bias);
		this.material.SetFloat("DepthTolerance", this.bilateralDepthTolerance);
		this.material.SetFloat("ZThickness", this.zThickness);
		this.material.SetFloat("Intensity", this.occlusionIntensity);
		this.material.SetFloat("SampleDistributionCurve", this.sampleDistributionCurve);
		this.material.SetFloat("ColorBleedAmount", this.colorBleedAmount);
		this.material.SetFloat("DrawDistance", this.drawDistance);
		this.material.SetFloat("DrawDistanceFadeSize", this.drawDistanceFadeSize);
		this.material.SetFloat("SelfBleedReduction", (!this.reduceSelfBleeding) ? 0f : 1f);
		this.material.SetFloat("BrightnessThreshold", this.brightnessThreshold);
		this.material.SetInt("HalfSampling", (this.halfSampling > false) ? 1 : 0);
		this.material.SetInt("Orthographic", (this.attachedCamera.orthographic > false) ? 1 : 0);
		if (this.useDownsampling)
		{
			renderTexture = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, RenderTextureFormat.ARGBHalf);
			renderTexture.filterMode = FilterMode.Bilinear;
			this.material.SetInt("Downsamp", 1);
			Graphics.Blit(source, renderTexture, this.material, (this.colorBleedAmount <= 0.0001f) ? 1 : 0);
		}
		else
		{
			this.material.SetInt("Downsamp", 0);
			Graphics.Blit(source, temporary, this.material, (this.colorBleedAmount <= 0.0001f) ? 1 : 0);
		}
		RenderTexture.ReleaseTemporary(temporary3);
		this.material.SetFloat("BlurDepthTolerance", 0.1f);
		int num = ((!this.attachedCamera.orthographic) ? 2 : 6);
		if (this.attachedCamera.orthographic)
		{
			this.material.SetFloat("Near", this.attachedCamera.nearClipPlane);
			this.material.SetFloat("Far", this.attachedCamera.farClipPlane);
		}
		if (this.useDownsampling)
		{
			this.material.SetVector("Kernel", new Vector2(2f, 0f));
			Graphics.Blit(renderTexture, temporary2, this.material, num);
			RenderTexture.ReleaseTemporary(renderTexture);
			this.material.SetVector("Kernel", new Vector2(0f, 2f));
			Graphics.Blit(temporary2, temporary, this.material, num);
			this.material.SetVector("Kernel", new Vector2(2f, 0f));
			Graphics.Blit(temporary, temporary2, this.material, num);
			this.material.SetVector("Kernel", new Vector2(0f, 2f));
			Graphics.Blit(temporary2, temporary, this.material, num);
		}
		else
		{
			this.material.SetVector("Kernel", new Vector2(1f, 0f));
			Graphics.Blit(temporary, temporary2, this.material, num);
			this.material.SetVector("Kernel", new Vector2(0f, 1f));
			Graphics.Blit(temporary2, temporary, this.material, num);
			this.material.SetVector("Kernel", new Vector2(1f, 0f));
			Graphics.Blit(temporary, temporary2, this.material, num);
			this.material.SetVector("Kernel", new Vector2(0f, 1f));
			Graphics.Blit(temporary2, temporary, this.material, num);
		}
		RenderTexture.ReleaseTemporary(temporary2);
		this.material.SetTexture("_SSAO", temporary);
		if (!this.visualizeSSAO)
		{
			Graphics.Blit(source, destination, this.material, 3);
		}
		else
		{
			Graphics.Blit(source, destination, this.material, 5);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000684A File Offset: 0x00004A4A
	private void CheckInit()
	{
		if (this.initChecker == null)
		{
			this.Init();
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x000655DC File Offset: 0x000637DC
	private void Init()
	{
		this.skipThisFrame = false;
		Shader shader = Shader.Find("Hidden/SESSAO");
		if (!shader)
		{
			this.skipThisFrame = true;
			return;
		}
		this.material = new Material(shader);
		this.attachedCamera = base.GetComponent<Camera>();
		this.attachedCamera.depthTextureMode |= DepthTextureMode.Depth;
		this.attachedCamera.depthTextureMode |= DepthTextureMode.DepthNormals;
		this.SetupDitherTexture();
		this.SetupDitherTextureSmall();
		this.initChecker = new object();
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000685A File Offset: 0x00004A5A
	private void Cleanup()
	{
		global::UnityEngine.Object.DestroyImmediate(this.material);
		this.initChecker = null;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00065660 File Offset: 0x00063860
	private void SetupDitherTextureSmall()
	{
		this.ditherTextureSmall = new Texture2D(3, 3, TextureFormat.Alpha8, false)
		{
			filterMode = FilterMode.Point
		};
		float[] array = new float[] { 8f, 1f, 6f, 3f, 0f, 4f, 7f, 2f, 5f };
		for (int i = 0; i < 9; i++)
		{
			Color color = new Color(0f, 0f, 0f, array[i] / 9f);
			int num = i % 3;
			int num2 = Mathf.FloorToInt((float)i / 3f);
			this.ditherTextureSmall.SetPixel(num, num2, color);
		}
		this.ditherTextureSmall.Apply();
		this.ditherTextureSmall.hideFlags = HideFlags.HideAndDontSave;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x000656FC File Offset: 0x000638FC
	private void SetupDitherTexture()
	{
		this.ditherTexture = new Texture2D(5, 5, TextureFormat.Alpha8, false)
		{
			filterMode = FilterMode.Point
		};
		float[] array = new float[]
		{
			12f, 1f, 10f, 3f, 20f, 5f, 18f, 7f, 16f, 9f,
			24f, 2f, 11f, 6f, 22f, 15f, 8f, 0f, 13f, 19f,
			4f, 21f, 14f, 23f, 17f
		};
		for (int i = 0; i < 25; i++)
		{
			Color color = new Color(0f, 0f, 0f, array[i] / 25f);
			int num = i % 5;
			int num2 = Mathf.FloorToInt((float)i / 5f);
			this.ditherTexture.SetPixel(num, num2, color);
		}
		this.ditherTexture.Apply();
		this.ditherTexture.hideFlags = HideFlags.HideAndDontSave;
	}

	// Token: 0x0400016E RID: 366
	public bool visualizeSSAO;

	// Token: 0x0400016F RID: 367
	[Range(0.02f, 5f)]
	public float radius = 1f;

	// Token: 0x04000170 RID: 368
	[Range(-0.2f, 0.5f)]
	public float bias = 0.1f;

	// Token: 0x04000171 RID: 369
	[Range(0.1f, 3f)]
	public float bilateralDepthTolerance = 0.2f;

	// Token: 0x04000172 RID: 370
	[Range(1f, 5f)]
	public float zThickness = 2.35f;

	// Token: 0x04000173 RID: 371
	[Range(0.5f, 5f)]
	public float occlusionIntensity = 1.3f;

	// Token: 0x04000174 RID: 372
	[Range(1f, 6f)]
	public float sampleDistributionCurve = 1.15f;

	// Token: 0x04000175 RID: 373
	[Range(0f, 1f)]
	public float colorBleedAmount = 1f;

	// Token: 0x04000176 RID: 374
	[Range(0.1f, 3f)]
	public float brightnessThreshold;

	// Token: 0x04000177 RID: 375
	public float drawDistance = 500f;

	// Token: 0x04000178 RID: 376
	public float drawDistanceFadeSize = 1f;

	// Token: 0x04000179 RID: 377
	public bool reduceSelfBleeding = true;

	// Token: 0x0400017A RID: 378
	public bool useDownsampling;

	// Token: 0x0400017B RID: 379
	public bool halfSampling;

	// Token: 0x0400017C RID: 380
	public bool preserveDetails;

	// Token: 0x0400017D RID: 381
	[HideInInspector]
	public Camera attachedCamera;

	// Token: 0x0400017E RID: 382
	private Texture2D ditherTexture;

	// Token: 0x0400017F RID: 383
	private Texture2D ditherTextureSmall;

	// Token: 0x04000180 RID: 384
	private object initChecker;

	// Token: 0x04000181 RID: 385
	private Material material;

	// Token: 0x04000182 RID: 386
	private bool skipThisFrame;
}
