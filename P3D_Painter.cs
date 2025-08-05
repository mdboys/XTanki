using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200005B RID: 91
[NullableContext(1)]
[Nullable(0)]
[Serializable]
public class P3D_Painter
{
	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000199 RID: 409 RVA: 0x00006345 File Offset: 0x00004545
	public bool IsReady
	{
		get
		{
			return this.Canvas != null;
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00063A58 File Offset: 0x00061C58
	public void SetCanvas(GameObject gameObject, string textureName = "_MainTex", int newMaterialIndex = 0)
	{
		Material material = P3D_Helper.GetMaterial(gameObject, newMaterialIndex);
		if (material != null)
		{
			this.SetCanvas(material.GetTexture(textureName), material.GetTextureScale(textureName), material.GetTextureOffset(textureName));
			return;
		}
		this.SetCanvas(null, Vector2.zero, Vector2.zero);
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00006353 File Offset: 0x00004553
	public void SetCanvas(Texture newTexture)
	{
		this.SetCanvas(newTexture, Vector2.one, Vector2.zero);
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00063AA4 File Offset: 0x00061CA4
	public void SetCanvas(Texture newTexture, Vector2 newTiling, Vector2 newOffset)
	{
		Texture2D texture2D = newTexture as Texture2D;
		if (!(texture2D != null) || newTiling.x == 0f || newTiling.y == 0f)
		{
			this.Canvas = null;
			return;
		}
		if (!P3D_Helper.IsWritableFormat(texture2D.format))
		{
			throw new Exception("Trying to paint a non-writable texture");
		}
		this.Canvas = texture2D;
		this.Tiling = newTiling;
		this.Offset = newOffset;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00063B10 File Offset: 0x00061D10
	public bool Paint(P3D_Brush brush, List<P3D_Result> results, P3D_CoordType coord = P3D_CoordType.UV1)
	{
		bool flag = false;
		if (results != null)
		{
			for (int i = 0; i < results.Count; i++)
			{
				flag |= this.Paint(brush, results[i], coord);
			}
		}
		return flag;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00006366 File Offset: 0x00004566
	public bool Paint(P3D_Brush brush, P3D_Result result, P3D_CoordType coord = P3D_CoordType.UV1)
	{
		return result != null && this.Paint(brush, result.GetUV(coord));
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00063B48 File Offset: 0x00061D48
	public bool Paint(P3D_Brush brush, Vector2 uv)
	{
		if (this.Canvas != null)
		{
			Vector2 vector = P3D_Helper.CalculatePixelFromCoord(uv, this.Tiling, this.Offset, this.Canvas.width, this.Canvas.height);
			return this.Paint(brush, vector.x, vector.y);
		}
		return false;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00063BA4 File Offset: 0x00061DA4
	public bool Paint(P3D_Brush brush, float x, float y)
	{
		if (brush != null)
		{
			P3D_Matrix p3D_Matrix = P3D_Helper.CreateMatrix(new Vector2(x, y) + brush.Offset, brush.Size, brush.Angle);
			return this.Paint(brush, p3D_Matrix);
		}
		return false;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000637B File Offset: 0x0000457B
	public bool Paint(P3D_Brush brush, P3D_Matrix matrix)
	{
		if (this.Canvas != null && brush != null)
		{
			brush.Paint(this.Canvas, matrix);
			this.Dirty = true;
			return true;
		}
		return false;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000063A5 File Offset: 0x000045A5
	public void Apply()
	{
		if (this.Canvas != null && this.Dirty)
		{
			this.Dirty = false;
			this.Canvas.Apply();
		}
	}

	// Token: 0x04000124 RID: 292
	public bool Dirty;

	// Token: 0x04000125 RID: 293
	public Texture2D Canvas;

	// Token: 0x04000126 RID: 294
	public Vector2 Tiling = Vector2.one;

	// Token: 0x04000127 RID: 295
	public Vector2 Offset;
}
