using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000061 RID: 97
[NullableContext(1)]
[Nullable(0)]
[Serializable]
public class P3D_UndoState
{
	// Token: 0x060001E4 RID: 484 RVA: 0x000066D5 File Offset: 0x000048D5
	public P3D_UndoState(Texture2D newTexture)
	{
		if (newTexture != null)
		{
			this.Texture = newTexture;
			this.Width = newTexture.width;
			this.Height = newTexture.height;
			this.Pixels = newTexture.GetPixels32();
		}
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00064BC4 File Offset: 0x00062DC4
	public void Perform()
	{
		if (this.Texture != null)
		{
			if (this.Texture.width != this.Width || this.Texture.height != this.Height)
			{
				this.Texture.Resize(this.Width, this.Height);
			}
			this.Texture.SetPixels32(this.Pixels);
			this.Texture.Apply();
		}
	}

	// Token: 0x04000148 RID: 328
	public Texture2D Texture;

	// Token: 0x04000149 RID: 329
	public int Width;

	// Token: 0x0400014A RID: 330
	public int Height;

	// Token: 0x0400014B RID: 331
	public Color32[] Pixels;
}
