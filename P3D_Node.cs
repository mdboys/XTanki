using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000058 RID: 88
[NullableContext(1)]
[Nullable(0)]
[Serializable]
public class P3D_Node
{
	// Token: 0x06000175 RID: 373 RVA: 0x00062FFC File Offset: 0x000611FC
	public static P3D_Node Spawn()
	{
		if (P3D_Node.pool.Count > 0)
		{
			int num = P3D_Node.pool.Count - 1;
			P3D_Node p3D_Node = P3D_Node.pool[num];
			P3D_Node.pool.RemoveAt(num);
			return p3D_Node;
		}
		return new P3D_Node();
	}

	// Token: 0x06000176 RID: 374 RVA: 0x000061BD File Offset: 0x000043BD
	public static P3D_Node Despawn(P3D_Node node)
	{
		P3D_Node.pool.Add(node);
		node.Bound = default(Bounds);
		node.Split = false;
		node.PositiveIndex = 0;
		node.NegativeIndex = 0;
		node.TriangleIndex = 0;
		node.TriangleCount = 0;
		return null;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00063040 File Offset: 0x00061240
	public void CalculateBound(List<P3D_Triangle> triangles)
	{
		if (triangles.Count > 0 && this.TriangleCount > 0)
		{
			Vector3 vector = triangles[this.TriangleIndex].Min;
			Vector3 vector2 = triangles[this.TriangleIndex].Max;
			for (int i = this.TriangleIndex + this.TriangleCount - 1; i > this.TriangleIndex; i--)
			{
				P3D_Triangle p3D_Triangle = triangles[i];
				vector = Vector3.Min(vector, p3D_Triangle.Min);
				vector2 = Vector3.Max(vector2, p3D_Triangle.Max);
			}
			this.Bound.SetMinMax(vector, vector2);
		}
	}

	// Token: 0x04000107 RID: 263
	private static List<P3D_Node> pool = new List<P3D_Node>();

	// Token: 0x04000108 RID: 264
	public Bounds Bound;

	// Token: 0x04000109 RID: 265
	public bool Split;

	// Token: 0x0400010A RID: 266
	public int PositiveIndex;

	// Token: 0x0400010B RID: 267
	public int NegativeIndex;

	// Token: 0x0400010C RID: 268
	public int TriangleIndex;

	// Token: 0x0400010D RID: 269
	public int TriangleCount;
}
