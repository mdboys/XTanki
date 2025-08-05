using System;

// Token: 0x0200005C RID: 92
[Serializable]
public struct P3D_Rect
{
	// Token: 0x060001A4 RID: 420 RVA: 0x000063E2 File Offset: 0x000045E2
	public P3D_Rect(int newXMin, int newXMax, int newYMin, int newYMax)
	{
		this.XMin = newXMin;
		this.XMax = newXMax;
		this.YMin = newYMin;
		this.YMax = newYMax;
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006401 File Offset: 0x00004601
	public bool IsSet
	{
		get
		{
			return this.XMin != this.XMax && this.YMin != this.YMax;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006424 File Offset: 0x00004624
	public int Width
	{
		get
		{
			return this.XMax - this.XMin;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x060001A7 RID: 423 RVA: 0x00006433 File Offset: 0x00004633
	public int Height
	{
		get
		{
			return this.YMax - this.YMin;
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x000063E2 File Offset: 0x000045E2
	public void Set(int newXMin, int newXMax, int newYMin, int newYMax)
	{
		this.XMin = newXMin;
		this.XMax = newXMax;
		this.YMin = newYMin;
		this.YMax = newYMax;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00006442 File Offset: 0x00004642
	public void Add(int newX, int newY)
	{
		this.Add(newX, newX + 1, newY, newY + 1);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00006452 File Offset: 0x00004652
	public void Add(P3D_Rect rect)
	{
		this.Add(rect.XMin, rect.XMax, rect.YMin, rect.YMax);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00063BE4 File Offset: 0x00061DE4
	public void Add(int newXMin, int newXMax, int newYMin, int newYMax)
	{
		if (this.Width == 0)
		{
			this.XMin = newXMin;
			this.XMax = newXMax;
		}
		else
		{
			if (newXMin < this.XMin)
			{
				this.XMin = newXMin;
			}
			if (newXMax > this.XMax)
			{
				this.XMax = newXMax;
			}
		}
		if (this.Height == 0)
		{
			this.YMin = newYMin;
			this.YMax = newYMax;
			return;
		}
		if (newYMin < this.YMin)
		{
			this.YMin = newYMin;
		}
		if (newYMax > this.YMax)
		{
			this.YMax = newYMax;
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00063C64 File Offset: 0x00061E64
	public bool Overlaps(P3D_Rect other)
	{
		return this.IsSet && other.IsSet && this.XMax > other.XMin && this.XMin < other.XMax && this.YMax > other.YMin && this.YMin < other.YMax;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00006472 File Offset: 0x00004672
	public void Clear()
	{
		this.XMin = 0;
		this.XMax = 0;
		this.YMin = 0;
		this.YMax = 0;
	}

	// Token: 0x04000128 RID: 296
	public int XMin;

	// Token: 0x04000129 RID: 297
	public int XMax;

	// Token: 0x0400012A RID: 298
	public int YMin;

	// Token: 0x0400012B RID: 299
	public int YMax;
}
