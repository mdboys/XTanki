using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000069 RID: 105
[NullableContext(1)]
[Nullable(0)]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Sonic Ether/SE Natural Bloom and Dirty Lens")]
public class SENaturalBloomAndDirtyLens : MonoBehaviour
{
	// Token: 0x060001F7 RID: 503 RVA: 0x00064D74 File Offset: 0x00062F74
	private void Start()
	{
		this.isSupported = true;
		if (!this.material)
		{
			this.material = new Material(this.shader);
		}
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures || !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
		{
			this.isSupported = false;
		}
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x000067F7 File Offset: 0x000049F7
	private void OnDisable()
	{
		if (this.material)
		{
			global::UnityEngine.Object.DestroyImmediate(this.material);
		}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00064DC4 File Offset: 0x00062FC4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.isSupported)
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (!this.material)
		{
			this.material = new Material(this.shader);
		}
		this.material.hideFlags = HideFlags.HideAndDontSave;
		this.material.SetFloat("_BloomIntensity", Mathf.Exp(this.bloomIntensity) - 1f);
		this.material.SetFloat("_LensDirtIntensity", Mathf.Exp(this.lensDirtIntensity) - 1f);
		source.filterMode = FilterMode.Bilinear;
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
		Graphics.Blit(source, temporary, this.material, 4);
		int num = source.width / 2;
		int num2 = source.height / 2;
		RenderTexture renderTexture = temporary;
		int num3 = 2;
		for (int i = 0; i < 6; i++)
		{
			RenderTexture renderTexture2 = RenderTexture.GetTemporary(num, num2, 0, source.format);
			renderTexture2.filterMode = FilterMode.Bilinear;
			Graphics.Blit(renderTexture, renderTexture2, this.material, 1);
			renderTexture = renderTexture2;
			float num4 = ((i <= 1) ? 0.5f : 1f);
			if (i == 2)
			{
				num4 = 0.75f;
			}
			for (int j = 0; j < num3; j++)
			{
				this.material.SetFloat("_BlurSize", (this.blurSize * 0.5f + (float)j) * num4);
				RenderTexture renderTexture3 = RenderTexture.GetTemporary(num, num2, 0, source.format);
				renderTexture3.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture2, renderTexture3, this.material, 2);
				RenderTexture.ReleaseTemporary(renderTexture2);
				renderTexture2 = renderTexture3;
				renderTexture3 = RenderTexture.GetTemporary(num, num2, 0, source.format);
				renderTexture3.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture2, renderTexture3, this.material, 3);
				RenderTexture.ReleaseTemporary(renderTexture2);
				renderTexture2 = renderTexture3;
			}
			switch (i)
			{
			case 0:
				this.material.SetTexture("_Bloom0", renderTexture2);
				break;
			case 1:
				this.material.SetTexture("_Bloom1", renderTexture2);
				break;
			case 2:
				this.material.SetTexture("_Bloom2", renderTexture2);
				break;
			case 3:
				this.material.SetTexture("_Bloom3", renderTexture2);
				break;
			case 4:
				this.material.SetTexture("_Bloom4", renderTexture2);
				break;
			case 5:
				this.material.SetTexture("_Bloom5", renderTexture2);
				break;
			}
			RenderTexture.ReleaseTemporary(renderTexture2);
			num /= 2;
			num2 /= 2;
		}
		this.material.SetTexture("_LensDirt", this.lensDirtTexture);
		Graphics.Blit(temporary, destination, this.material, 0);
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x04000166 RID: 358
	[Range(0f, 0.4f)]
	public float bloomIntensity = 0.05f;

	// Token: 0x04000167 RID: 359
	public Shader shader;

	// Token: 0x04000168 RID: 360
	public Texture2D lensDirtTexture;

	// Token: 0x04000169 RID: 361
	[Range(0f, 0.95f)]
	public float lensDirtIntensity = 0.05f;

	// Token: 0x0400016A RID: 362
	public bool inputIsHDR;

	// Token: 0x0400016B RID: 363
	private readonly float blurSize = 4f;

	// Token: 0x0400016C RID: 364
	private bool isSupported;

	// Token: 0x0400016D RID: 365
	private Material material;
}
