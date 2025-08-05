using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000194 RID: 404
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (3D Lookup Texture)")]
	public class ColorCorrectionLookup : PostEffectsBase
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x0000A527 File Offset: 0x00008727
		private void OnDisable()
		{
			if (this.material)
			{
				global::UnityEngine.Object.DestroyImmediate(this.material);
				this.material = null;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000A548 File Offset: 0x00008748
		private void OnDestroy()
		{
			if (this.converted3DLut)
			{
				global::UnityEngine.Object.DestroyImmediate(this.converted3DLut);
			}
			this.converted3DLut = null;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0007644C File Offset: 0x0007464C
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources() || !SystemInfo.supports3DTextures)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.converted3DLut == null)
			{
				this.SetIdentityLut();
			}
			int width = this.converted3DLut.width;
			this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
			this.material.SetFloat("_Scale", (float)(width - 1) / (1f * (float)width));
			this.material.SetFloat("_Offset", 1f / (2f * (float)width));
			this.material.SetTexture("_ClutTex", this.converted3DLut);
			Graphics.Blit(source, destination, this.material, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0000A569 File Offset: 0x00008769
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.material = base.CheckShaderAndCreateMaterial(this.shader, this.material);
			if (!this.isSupported || !SystemInfo.supports3DTextures)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00076504 File Offset: 0x00074704
		public void SetIdentityLut()
		{
			int num = 16;
			Color[] array = new Color[num * num * num];
			float num2 = 1f / (1f * (float)num - 1f);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
					}
				}
			}
			if (this.converted3DLut)
			{
				global::UnityEngine.Object.DestroyImmediate(this.converted3DLut);
			}
			this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
			this.converted3DLut.SetPixels(array);
			this.converted3DLut.Apply();
			this.basedOnTempTex = string.Empty;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000A5A6 File Offset: 0x000087A6
		public bool ValidDimensions(Texture2D tex2d)
		{
			return tex2d && tex2d.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000765EC File Offset: 0x000747EC
		public void Convert(Texture2D temp2DTex, string path)
		{
			if (!temp2DTex)
			{
				Debug.LogError("Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
				return;
			}
			int num = temp2DTex.width * temp2DTex.height;
			num = temp2DTex.height;
			if (!this.ValidDimensions(temp2DTex))
			{
				Debug.LogWarning("The given 2D texture " + temp2DTex.name + " cannot be used as a 3D LUT.");
				this.basedOnTempTex = string.Empty;
				return;
			}
			Color[] pixels = temp2DTex.GetPixels();
			Color[] array = new Color[pixels.Length];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						int num2 = num - j - 1;
						array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
					}
				}
			}
			if (this.converted3DLut)
			{
				global::UnityEngine.Object.DestroyImmediate(this.converted3DLut);
			}
			this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
			this.converted3DLut.SetPixels(array);
			this.converted3DLut.Apply();
			this.basedOnTempTex = path;
		}

		// Token: 0x040004CF RID: 1231
		public Shader shader;

		// Token: 0x040004D0 RID: 1232
		public Texture3D converted3DLut;

		// Token: 0x040004D1 RID: 1233
		public string basedOnTempTex = string.Empty;

		// Token: 0x040004D2 RID: 1234
		private Material material;
	}
}
