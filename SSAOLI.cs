using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000084 RID: 132
[NullableContext(1)]
[Nullable(0)]
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(BlurOptimized))]
public class SSAOLI : MonoBehaviour
{
	// Token: 0x06000280 RID: 640 RVA: 0x0006700C File Offset: 0x0006520C
	private void Awake()
	{
		this.ssaoMaterial = new Material(this.ssaoShader);
		this.blur = base.GetComponent<BlurOptimized>();
		this.blur.enabled = false;
		this.uvRadiusDepth1 = this.CalculateUVRadiusDepth1();
		this.samples = this.CalculateSamples();
		this.rotationMap = this.CreateRotationMap();
	}

	// Token: 0x06000281 RID: 641 RVA: 0x00067068 File Offset: 0x00065268
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.ssaoMaterial.SetTexture("_RotationMap", this.rotationMap);
		this.ssaoMaterial.SetFloat("centerVolume", this.samples[0].w);
		this.ssaoMaterial.SetFloat("uvRadiusDepth1", this.uvRadiusDepth1);
		this.ssaoMaterial.SetFloat("radius", this.radius);
		this.ssaoMaterial.SetFloat("maxDistance", this.maxDistance);
		this.ssaoMaterial.SetFloat("intensity", this.intensity);
		for (int i = 1; i < this.samples.Count; i++)
		{
			this.ssaoMaterial.SetVector(string.Format("sample{0}", i), this.samples[i]);
		}
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
		Graphics.Blit(source, temporary, this.ssaoMaterial, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
		this.blur.OnRenderImage(temporary, temporary2);
		RenderTexture.ReleaseTemporary(temporary);
		this.ssaoMaterial.SetTexture("_SSAO", temporary2);
		Graphics.Blit(source, destination, this.ssaoMaterial, 1);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x06000282 RID: 642 RVA: 0x000671B0 File Offset: 0x000653B0
	private float CalculateUVRadiusDepth1()
	{
		Camera component = base.GetComponent<Camera>();
		Vector4 vector = component.transform.localToWorldMatrix.MultiplyPoint(new Vector3(this.radius, 0f, 1f));
		return component.WorldToViewportPoint(vector).x - 0.5f;
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00067208 File Offset: 0x00065408
	private Texture2D CreateRotationMap()
	{
		Texture2D texture2D = new Texture2D(4, 4, TextureFormat.RGBA32, false)
		{
			wrapMode = TextureWrapMode.Repeat
		};
		float num = 0f;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				texture2D.SetPixel(i, j, new Color(Mathf.Sin(num), Mathf.Cos(num), 0f));
				num += 0.19634955f;
			}
		}
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00067274 File Offset: 0x00065474
	private List<Vector4> CalculateSamples()
	{
		List<Vector4> list = this.CreateSamples();
		this.CalculateSampleVolumes(list);
		this.RemovePairSamples(list);
		return list;
	}

	// Token: 0x06000285 RID: 645 RVA: 0x00067298 File Offset: 0x00065498
	private void RemovePairSamples(List<Vector4> samples)
	{
		for (int i = samples.Count - 1; i > 1; i -= 2)
		{
			samples.RemoveAt(i);
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x000672C0 File Offset: 0x000654C0
	private void CalculateSampleVolumes(List<Vector4> samples)
	{
		float num = 0f;
		for (int i = 0; i < 256; i++)
		{
			for (int j = 0; j < 256; j++)
			{
				if (SSAOLI.IsPointInsideCircle(i, j, 128))
				{
					int num2 = SSAOLI.FindClosestSample(i, j, 128, samples);
					Vector4 vector = samples[num2];
					float num3 = this.CalculateLengthInSphere((float)(128 - i) / 128f, (float)(128 - j) / 128f);
					vector.w += num3;
					samples[num2] = vector;
					num += num3;
				}
			}
		}
		for (int k = 0; k < samples.Count; k++)
		{
			Vector4 vector2 = samples[k];
			vector2.w /= num;
			samples[k] = vector2;
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00006D72 File Offset: 0x00004F72
	private float CalculateLengthInSphere(float x, float y)
	{
		return 2f * Mathf.Sqrt(1f - (x * x + y * y));
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0006738C File Offset: 0x0006558C
	private static int FindClosestSample(int x, int y, int radius, List<Vector4> samples)
	{
		float num = float.MaxValue;
		int num2 = -1;
		for (int i = 0; i < samples.Count; i++)
		{
			float num3 = samples[i].x * (float)radius + (float)radius;
			float num4 = samples[i].y * (float)radius + (float)radius;
			float num5 = ((float)x - num3) * ((float)x - num3) + ((float)y - num4) * ((float)y - num4);
			if (num5 < num)
			{
				num = num5;
				num2 = i;
			}
		}
		return num2;
	}

	// Token: 0x06000289 RID: 649 RVA: 0x000673FC File Offset: 0x000655FC
	private static bool IsPointInsideCircle(int i, int j, int radius)
	{
		int num = radius - i;
		int num2 = radius - j;
		return (double)(num * num + num2 * num2) < (double)(radius * radius);
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00067420 File Offset: 0x00065620
	private List<Vector4> CreateSamples()
	{
		List<Vector4> list = new List<Vector4>();
		Vector4 vector = new Vector4
		{
			x = 0f,
			y = 0f,
			z = this.CalculateLengthInSphere(0f, 0f)
		};
		list.Add(vector);
		float num = 1.0471976f;
		float num2 = 0f;
		for (int i = 1; i <= 3; i++)
		{
			float num3 = (float)i / 3.5f;
			Vector4 vector2 = default(Vector4);
			vector2.x = (0f - num3) * Mathf.Sin(0f - num2);
			vector2.y = num3 * Mathf.Cos(0f - num2);
			vector2.z = this.CalculateLengthInSphere(vector2.x, vector2.y);
			Vector4 vector3 = new Vector4
			{
				x = 0f - vector2.x,
				y = 0f - vector2.y,
				z = vector2.z
			};
			list.Add(vector2);
			list.Add(vector3);
			num2 += num;
		}
		return list;
	}

	// Token: 0x040001E2 RID: 482
	public float radius = 2f;

	// Token: 0x040001E3 RID: 483
	public float maxDistance = 2f;

	// Token: 0x040001E4 RID: 484
	public float intensity = 2f;

	// Token: 0x040001E5 RID: 485
	public Shader ssaoShader;

	// Token: 0x040001E6 RID: 486
	private BlurOptimized blur;

	// Token: 0x040001E7 RID: 487
	private Texture2D rotationMap;

	// Token: 0x040001E8 RID: 488
	private List<Vector4> samples;

	// Token: 0x040001E9 RID: 489
	private Material ssaoMaterial;

	// Token: 0x040001EA RID: 490
	private float uvRadiusDepth1;
}
