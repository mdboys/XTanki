using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000060 RID: 96
[NullableContext(1)]
[Nullable(0)]
[Serializable]
public class P3D_Triangle
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060001D9 RID: 473 RVA: 0x000065D8 File Offset: 0x000047D8
	public Vector3 Edge1
	{
		get
		{
			return this.PointB - this.PointA;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060001DA RID: 474 RVA: 0x000065EB File Offset: 0x000047EB
	public Vector3 Edge2
	{
		get
		{
			return this.PointC - this.PointA;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060001DB RID: 475 RVA: 0x000065FE File Offset: 0x000047FE
	public Vector3 Min
	{
		get
		{
			return Vector3.Min(this.PointA, Vector3.Min(this.PointB, this.PointC));
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060001DC RID: 476 RVA: 0x0000661C File Offset: 0x0000481C
	public Vector3 Max
	{
		get
		{
			return Vector3.Max(this.PointA, Vector3.Max(this.PointB, this.PointC));
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060001DD RID: 477 RVA: 0x0000663A File Offset: 0x0000483A
	public float MidX
	{
		get
		{
			return (this.PointA.x + this.PointB.x + this.PointC.x) / 3f;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060001DE RID: 478 RVA: 0x00006665 File Offset: 0x00004865
	public float MidY
	{
		get
		{
			return (this.PointA.y + this.PointB.y + this.PointC.y) / 3f;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060001DF RID: 479 RVA: 0x00006690 File Offset: 0x00004890
	public float MidZ
	{
		get
		{
			return (this.PointA.z + this.PointB.z + this.PointC.z) / 3f;
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00064B80 File Offset: 0x00062D80
	public static P3D_Triangle Spawn()
	{
		if (P3D_Triangle.pool.Count > 0)
		{
			int num = P3D_Triangle.pool.Count - 1;
			P3D_Triangle p3D_Triangle = P3D_Triangle.pool[num];
			P3D_Triangle.pool.RemoveAt(num);
			return p3D_Triangle;
		}
		return new P3D_Triangle();
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x000066BB File Offset: 0x000048BB
	public static P3D_Triangle Despawn(P3D_Triangle triangle)
	{
		P3D_Triangle.pool.Add(triangle);
		return null;
	}

	// Token: 0x0400013E RID: 318
	private static List<P3D_Triangle> pool = new List<P3D_Triangle>();

	// Token: 0x0400013F RID: 319
	public Vector3 PointA;

	// Token: 0x04000140 RID: 320
	public Vector3 PointB;

	// Token: 0x04000141 RID: 321
	public Vector3 PointC;

	// Token: 0x04000142 RID: 322
	public Vector2 Coord1A;

	// Token: 0x04000143 RID: 323
	public Vector2 Coord1B;

	// Token: 0x04000144 RID: 324
	public Vector2 Coord1C;

	// Token: 0x04000145 RID: 325
	public Vector2 Coord2A;

	// Token: 0x04000146 RID: 326
	public Vector2 Coord2B;

	// Token: 0x04000147 RID: 327
	public Vector2 Coord2C;
}
