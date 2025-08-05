using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200005D RID: 93
[NullableContext(1)]
[Nullable(0)]
[Serializable]
public class P3D_Result
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x060001AE RID: 430 RVA: 0x00063CC8 File Offset: 0x00061EC8
	public Vector2 UV1
	{
		get
		{
			return this.Triangle.Coord1A * this.Weights.x + this.Triangle.Coord1B * this.Weights.y + this.Triangle.Coord1C * this.Weights.z;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x060001AF RID: 431 RVA: 0x00063D30 File Offset: 0x00061F30
	public Vector2 UV2
	{
		get
		{
			return this.Triangle.Coord2A * this.Weights.x + this.Triangle.Coord2B * this.Weights.y + this.Triangle.Coord2C * this.Weights.z;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060001B0 RID: 432 RVA: 0x00063D98 File Offset: 0x00061F98
	public Vector2 Point
	{
		get
		{
			return this.Triangle.PointA * this.Weights.x + this.Triangle.PointB * this.Weights.y + this.Triangle.PointC * this.Weights.z;
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00063E08 File Offset: 0x00062008
	public static P3D_Result Spawn()
	{
		if (P3D_Result.pool.Count > 0)
		{
			int num = P3D_Result.pool.Count - 1;
			P3D_Result p3D_Result = P3D_Result.pool[num];
			P3D_Result.pool.RemoveAt(num);
			return p3D_Result;
		}
		return new P3D_Result();
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00006490 File Offset: 0x00004690
	public static P3D_Result Despawn(P3D_Result result)
	{
		P3D_Result.pool.Add(result);
		return null;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00063E4C File Offset: 0x0006204C
	public Vector2 GetUV(P3D_CoordType coord)
	{
		Vector2 vector;
		if (coord != P3D_CoordType.UV1)
		{
			if (coord != P3D_CoordType.UV2)
			{
				vector = default(Vector2);
			}
			else
			{
				vector = this.UV2;
			}
		}
		else
		{
			vector = this.UV1;
		}
		return vector;
	}

	// Token: 0x0400012C RID: 300
	private static List<P3D_Result> pool = new List<P3D_Result>();

	// Token: 0x0400012D RID: 301
	public Vector3 Weights;

	// Token: 0x0400012E RID: 302
	public P3D_Triangle Triangle;

	// Token: 0x0400012F RID: 303
	public float Distance01;
}
