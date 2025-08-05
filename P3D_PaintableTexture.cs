using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200005A RID: 90
[NullableContext(1)]
[Nullable(0)]
[Serializable]
public class P3D_PaintableTexture
{
	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000194 RID: 404 RVA: 0x000062C0 File Offset: 0x000044C0
	public P3D_Painter Painter
	{
		get
		{
			return this.painter;
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x000062C8 File Offset: 0x000044C8
	public void Paint(P3D_Brush brush, Vector2 uv)
	{
		P3D_Painter p3D_Painter = this.painter;
		if (p3D_Painter == null)
		{
			return;
		}
		p3D_Painter.Paint(brush, uv);
	}

	// Token: 0x06000196 RID: 406 RVA: 0x000062DD File Offset: 0x000044DD
	public void UpdateTexture(GameObject gameObject)
	{
		if (this.painter == null)
		{
			this.painter = new P3D_Painter();
		}
		this.painter.SetCanvas(gameObject, this.TextureName, this.MaterialIndex);
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00063944 File Offset: 0x00061B44
	public void Awake(GameObject gameObject)
	{
		if (this.DuplicateOnAwake)
		{
			Material material = P3D_Helper.CloneMaterial(gameObject, this.MaterialIndex);
			if (material != null)
			{
				Texture texture = material.GetTexture(this.TextureName);
				if (texture != null)
				{
					texture = P3D_Helper.Clone<Texture>(texture, true);
					material.SetTexture(this.TextureName, texture);
				}
			}
		}
		if (this.CreateOnAwake && this.CreateWidth > 0 && this.CreateHeight > 0)
		{
			Material material2 = P3D_Helper.GetMaterial(gameObject, this.MaterialIndex);
			if (material2 != null)
			{
				Texture texture2 = material2.GetTexture(this.TextureName);
				TextureFormat textureFormat = P3D_Helper.GetTextureFormat(this.CreateFormat);
				Texture2D texture2D = P3D_Helper.CreateTexture(this.CreateWidth, this.CreateHeight, textureFormat, this.CreateMipMaps);
				if (texture2 != null)
				{
					Debug.LogWarning("There is already a texture in this texture slot, maybe set it to null to save memory?", gameObject);
				}
				texture2 = texture2D;
				P3D_Helper.ClearTexture(texture2D, this.CreateColor, true);
				material2.SetTexture(this.TextureName, texture2);
				if (!string.IsNullOrEmpty(this.CreateKeyword))
				{
					material2.EnableKeyword(this.CreateKeyword);
				}
			}
		}
		this.UpdateTexture(gameObject);
	}

	// Token: 0x04000117 RID: 279
	[Tooltip("If your paintable has more than one texture then you can specify a group to select just one")]
	public P3D_Group Group;

	// Token: 0x04000118 RID: 280
	[Tooltip("The material index we want to paint")]
	public int MaterialIndex;

	// Token: 0x04000119 RID: 281
	[Tooltip("The texture we want to paint")]
	public string TextureName = "_MainTex";

	// Token: 0x0400011A RID: 282
	[Tooltip("The UV set used when painting this texture")]
	public P3D_CoordType Coord;

	// Token: 0x0400011B RID: 283
	[Tooltip("Should the material and texture get duplicated on awake? (useful for prefab clones)")]
	public bool DuplicateOnAwake;

	// Token: 0x0400011C RID: 284
	[Tooltip("Should the texture get created on awake? (useful for saving scene file size)")]
	public bool CreateOnAwake;

	// Token: 0x0400011D RID: 285
	[Tooltip("The width of the created texture")]
	public int CreateWidth = 512;

	// Token: 0x0400011E RID: 286
	[Tooltip("The height of the created texture")]
	public int CreateHeight = 512;

	// Token: 0x0400011F RID: 287
	[Tooltip("The pixel format of the created texture")]
	public P3D_Format CreateFormat;

	// Token: 0x04000120 RID: 288
	[Tooltip("The color of the created texture")]
	public Color CreateColor = Color.white;

	// Token: 0x04000121 RID: 289
	[Tooltip("Should the created etxture have mip maps?")]
	public bool CreateMipMaps = true;

	// Token: 0x04000122 RID: 290
	[Tooltip("Some shaders (e.g. Standard Shader) require you to enable keywords when adding new textures, you can specify that keyword here")]
	public string CreateKeyword;

	// Token: 0x04000123 RID: 291
	[SerializeField]
	private P3D_Painter painter;
}
