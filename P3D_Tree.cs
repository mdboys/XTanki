using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200005F RID: 95
[NullableContext(1)]
[Nullable(0)]
public class P3D_Tree
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006519 File Offset: 0x00004719
	public static P3D_Tree TempInstance
	{
		get
		{
			if (P3D_Tree.tempInstance == null)
			{
				P3D_Tree.tempInstance = new P3D_Tree();
			}
			return P3D_Tree.tempInstance;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060001C1 RID: 449 RVA: 0x00006531 File Offset: 0x00004731
	public bool IsReady
	{
		get
		{
			return this.nodes.Count > 0;
		}
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00064180 File Offset: 0x00062380
	public void Clear()
	{
		this.mesh = null;
		this.vertexCount = 0;
		this.subMeshIndex = 0;
		for (int i = this.triangles.Count - 1; i >= 0; i--)
		{
			P3D_Triangle.Despawn(this.triangles[i]);
		}
		this.triangles.Clear();
		for (int j = this.nodes.Count - 1; j >= 0; j--)
		{
			P3D_Node.Despawn(this.nodes[j]);
		}
		this.nodes.Clear();
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0006420C File Offset: 0x0006240C
	public void ClearResults()
	{
		for (int i = P3D_Tree.results.Count - 1; i >= 0; i--)
		{
			P3D_Result.Despawn(P3D_Tree.results[i]);
		}
		P3D_Tree.results.Clear();
		P3D_Tree.potentials.Clear();
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00064258 File Offset: 0x00062458
	public void SetMesh(Mesh newMesh, int newSubMeshIndex = 0, bool forceUpdate = false)
	{
		if (newMesh != null)
		{
			if (forceUpdate || !(newMesh == this.mesh) || newSubMeshIndex != this.subMeshIndex || newMesh.vertexCount != this.vertexCount)
			{
				this.Clear();
				this.mesh = newMesh;
				this.subMeshIndex = newSubMeshIndex;
				this.vertexCount = newMesh.vertexCount;
				this.ExtractTriangles();
				this.ConstructNodes();
				return;
			}
		}
		else
		{
			this.Clear();
		}
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x000642CC File Offset: 0x000624CC
	public void SetMesh(GameObject gameObject, int subMeshIndex = 0, bool forceUpdate = false)
	{
		Mesh mesh = null;
		Mesh mesh2 = P3D_Helper.GetMesh(gameObject, ref mesh);
		if (mesh != null)
		{
			P3D_Helper.Destroy<Mesh>(mesh);
			throw new Exception("P3D_Tree cannot manage baked meshes, call SetMesh with the Mesh directly to use animated meshes");
		}
		this.SetMesh(mesh2, subMeshIndex, forceUpdate);
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00064308 File Offset: 0x00062508
	public P3D_Result FindNearest(Vector3 point, float maxDistance)
	{
		this.ClearResults();
		if (this.IsReady && maxDistance > 0f)
		{
			float num = maxDistance * maxDistance;
			P3D_Triangle p3D_Triangle = null;
			Vector3 vector = default(Vector3);
			this.BeginSearchDistance(point, num);
			for (int i = P3D_Tree.potentials.Count - 1; i >= 0; i--)
			{
				P3D_Triangle p3D_Triangle2 = P3D_Tree.potentials[i];
				Vector3 vector2;
				float num2 = P3D_Helper.ClosestBarycentric(point, p3D_Triangle2, out vector2);
				if (num2 <= num)
				{
					num = num2;
					p3D_Triangle = p3D_Triangle2;
					vector = vector2;
				}
			}
			if (p3D_Triangle != null)
			{
				return this.GetResult(p3D_Triangle, vector, Mathf.Sqrt(num) / maxDistance);
			}
		}
		return null;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00064394 File Offset: 0x00062594
	public P3D_Result FindBetweenNearest(Vector3 startPoint, Vector3 endPoint)
	{
		this.ClearResults();
		if (this.IsReady)
		{
			float num = float.PositiveInfinity;
			P3D_Triangle p3D_Triangle = null;
			Vector3 vector = default(Vector3);
			this.BeginSearchBetween(startPoint, endPoint);
			for (int i = P3D_Tree.potentials.Count - 1; i >= 0; i--)
			{
				P3D_Triangle p3D_Triangle2 = P3D_Tree.potentials[i];
				Vector3 vector2;
				float num2;
				if (P3D_Helper.IntersectBarycentric(startPoint, endPoint, p3D_Triangle2, out vector2, out num2) && num2 < num)
				{
					num = num2;
					p3D_Triangle = p3D_Triangle2;
					vector = vector2;
				}
			}
			if (p3D_Triangle != null)
			{
				return this.GetResult(p3D_Triangle, vector, num);
			}
		}
		return null;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00064418 File Offset: 0x00062618
	public List<P3D_Result> FindBetweenAll(Vector3 startPoint, Vector3 endPoint)
	{
		this.ClearResults();
		if (this.IsReady)
		{
			this.BeginSearchBetween(startPoint, endPoint);
			for (int i = P3D_Tree.potentials.Count - 1; i >= 0; i--)
			{
				P3D_Triangle p3D_Triangle = P3D_Tree.potentials[i];
				Vector3 vector;
				float num;
				if (P3D_Helper.IntersectBarycentric(startPoint, endPoint, p3D_Triangle, out vector, out num))
				{
					this.AddToResults(p3D_Triangle, vector, num);
				}
			}
		}
		return P3D_Tree.results;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0006447C File Offset: 0x0006267C
	public P3D_Result FindPerpendicularNearest(Vector3 point, float maxDistance)
	{
		this.ClearResults();
		if (this.IsReady && maxDistance > 0f)
		{
			float num = maxDistance * maxDistance;
			P3D_Triangle p3D_Triangle = null;
			Vector3 vector = default(Vector3);
			this.BeginSearchDistance(point, num);
			for (int i = P3D_Tree.potentials.Count - 1; i >= 0; i--)
			{
				P3D_Triangle p3D_Triangle2 = P3D_Tree.potentials[i];
				Vector3 vector2 = default(Vector3);
				float num2 = 0f;
				if (P3D_Helper.ClosestBarycentric(point, p3D_Triangle2, ref vector2, ref num2) && num2 <= num)
				{
					num = num2;
					p3D_Triangle = p3D_Triangle2;
					vector = vector2;
				}
			}
			if (p3D_Triangle != null)
			{
				return this.GetResult(p3D_Triangle, vector, Mathf.Sqrt(num) / maxDistance);
			}
		}
		return null;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0006451C File Offset: 0x0006271C
	public List<P3D_Result> FindPerpendicularAll(Vector3 point, float maxDistance)
	{
		this.ClearResults();
		if (this.IsReady && maxDistance > 0f)
		{
			float num = maxDistance * maxDistance;
			this.BeginSearchDistance(point, num);
			for (int i = P3D_Tree.potentials.Count - 1; i >= 0; i--)
			{
				P3D_Triangle p3D_Triangle = P3D_Tree.potentials[i];
				Vector3 vector = default(Vector3);
				float num2 = 0f;
				if (P3D_Helper.ClosestBarycentric(point, p3D_Triangle, ref vector, ref num2) && num2 <= num)
				{
					this.AddToResults(p3D_Triangle, vector, Mathf.Sqrt(num2) / maxDistance);
				}
			}
		}
		return P3D_Tree.results;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00006541 File Offset: 0x00004741
	private void BeginSearchDistance(Vector3 point, float maxDistanceSqr)
	{
		this.SearchDistance(this.nodes[0], point, maxDistanceSqr);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x000645A8 File Offset: 0x000627A8
	private void SearchDistance(P3D_Node node, Vector3 point, float maxDistanceSqr)
	{
		if (node.Bound.SqrDistance(point) >= maxDistanceSqr)
		{
			return;
		}
		if (node.Split)
		{
			if (node.PositiveIndex != 0)
			{
				this.SearchDistance(this.nodes[node.PositiveIndex], point, maxDistanceSqr);
			}
			if (node.NegativeIndex != 0)
			{
				this.SearchDistance(this.nodes[node.NegativeIndex], point, maxDistanceSqr);
				return;
			}
		}
		else
		{
			this.AddToPotentials(node);
		}
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00064618 File Offset: 0x00062818
	private void BeginSearchBetween(Vector3 startPoint, Vector3 endPoint)
	{
		Vector3 vector = endPoint - startPoint;
		Ray ray = new Ray(startPoint, vector);
		float magnitude = vector.magnitude;
		this.SearchBetween(this.nodes[0], ray, magnitude);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00064654 File Offset: 0x00062854
	private void SearchBetween(P3D_Node node, Ray ray, float maxDistance)
	{
		float num;
		if (!node.Bound.IntersectRay(ray, out num) || num > maxDistance)
		{
			return;
		}
		if (node.Split)
		{
			if (node.PositiveIndex != 0)
			{
				this.SearchBetween(this.nodes[node.PositiveIndex], ray, maxDistance);
			}
			if (node.NegativeIndex != 0)
			{
				this.SearchBetween(this.nodes[node.NegativeIndex], ray, maxDistance);
				return;
			}
		}
		else
		{
			this.AddToPotentials(node);
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x000646C8 File Offset: 0x000628C8
	private void AddToPotentials(P3D_Node node)
	{
		for (int i = node.TriangleIndex; i < node.TriangleIndex + node.TriangleCount; i++)
		{
			P3D_Tree.potentials.Add(this.triangles[i]);
		}
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00064708 File Offset: 0x00062908
	private void AddToResults(P3D_Triangle triangle, Vector3 weights, float distance01)
	{
		P3D_Result p3D_Result = P3D_Result.Spawn();
		p3D_Result.Triangle = triangle;
		p3D_Result.Weights = weights;
		p3D_Result.Distance01 = distance01;
		P3D_Tree.results.Add(p3D_Result);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00006557 File Offset: 0x00004757
	private P3D_Result GetResult(P3D_Triangle triangle, Vector3 weights, float distance01)
	{
		this.ClearResults();
		this.AddToResults(triangle, weights, distance01);
		return P3D_Tree.results[0];
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0006473C File Offset: 0x0006293C
	private void ExtractTriangles()
	{
		if (this.subMeshIndex < 0 || this.mesh.subMeshCount < 0)
		{
			return;
		}
		int num = Mathf.Min(this.subMeshIndex, this.mesh.subMeshCount - 1);
		int[] array = this.mesh.GetTriangles(num);
		Vector3[] vertices = this.mesh.vertices;
		Vector2[] uv = this.mesh.uv;
		Vector2[] uv2 = this.mesh.uv2;
		if (array.Length == 0)
		{
			return;
		}
		for (int i = array.Length / 3 - 1; i >= 0; i--)
		{
			P3D_Triangle p3D_Triangle = P3D_Triangle.Spawn();
			int num2 = array[i * 3];
			int num3 = array[i * 3 + 1];
			int num4 = array[i * 3 + 2];
			p3D_Triangle.PointA = vertices[num2];
			p3D_Triangle.PointB = vertices[num3];
			p3D_Triangle.PointC = vertices[num4];
			p3D_Triangle.Coord1A = uv[num2];
			p3D_Triangle.Coord1B = uv[num3];
			p3D_Triangle.Coord1C = uv[num4];
			if (uv2.Length != 0)
			{
				p3D_Triangle.Coord2A = uv2[num2];
				p3D_Triangle.Coord2B = uv2[num3];
				p3D_Triangle.Coord2C = uv2[num4];
			}
			this.triangles.Add(p3D_Triangle);
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0006488C File Offset: 0x00062A8C
	private void ConstructNodes()
	{
		P3D_Node p3D_Node = P3D_Node.Spawn();
		this.nodes.Add(p3D_Node);
		this.Pack(p3D_Node, 0, this.triangles.Count);
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x000648C0 File Offset: 0x00062AC0
	private void Pack(P3D_Node node, int min, int max)
	{
		int num = max - min;
		node.TriangleIndex = min;
		node.TriangleCount = num;
		node.Split = num >= 5;
		node.CalculateBound(this.triangles);
		if (node.Split)
		{
			int num2 = (min + max) / 2;
			this.SortTriangles(min, max);
			node.PositiveIndex = this.nodes.Count;
			P3D_Node p3D_Node = P3D_Node.Spawn();
			this.nodes.Add(p3D_Node);
			this.Pack(p3D_Node, min, num2);
			node.NegativeIndex = this.nodes.Count;
			P3D_Node p3D_Node2 = P3D_Node.Spawn();
			this.nodes.Add(p3D_Node2);
			this.Pack(p3D_Node2, num2, max);
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00064968 File Offset: 0x00062B68
	private void SortTriangles(int minIndex, int maxIndex)
	{
		P3D_Tree.potentials.Clear();
		Vector3 vector = this.triangles[minIndex].Min;
		Vector3 vector2 = this.triangles[minIndex].Max;
		Vector3 vector3 = Vector3.zero;
		for (int i = minIndex; i < maxIndex; i++)
		{
			P3D_Triangle p3D_Triangle = this.triangles[i];
			vector = Vector3.Min(vector, p3D_Triangle.Min);
			vector2 = Vector3.Max(vector2, p3D_Triangle.Max);
			vector3 += p3D_Triangle.PointA + p3D_Triangle.PointB + p3D_Triangle.PointC;
			P3D_Tree.potentials.Add(p3D_Triangle);
		}
		Vector3 vector4 = vector2 - vector;
		if (vector4.x > vector4.y && vector4.x > vector4.z)
		{
			float num = P3D_Helper.Divide(vector3.x, (float)this.triangles.Count * 3f);
			for (int j = P3D_Tree.potentials.Count - 1; j >= 0; j--)
			{
				P3D_Triangle p3D_Triangle2 = P3D_Tree.potentials[j];
				this.SortTriangle(p3D_Triangle2, ref minIndex, ref maxIndex, p3D_Triangle2.MidX >= num);
			}
			return;
		}
		if (vector4.y > vector4.x && vector4.y > vector4.z)
		{
			float num2 = P3D_Helper.Divide(vector3.y, (float)this.triangles.Count * 3f);
			for (int k = P3D_Tree.potentials.Count - 1; k >= 0; k--)
			{
				P3D_Triangle p3D_Triangle3 = P3D_Tree.potentials[k];
				this.SortTriangle(p3D_Triangle3, ref minIndex, ref maxIndex, p3D_Triangle3.MidY >= num2);
			}
			return;
		}
		float num3 = P3D_Helper.Divide(vector3.z, (float)this.triangles.Count * 3f);
		for (int l = P3D_Tree.potentials.Count - 1; l >= 0; l--)
		{
			P3D_Triangle p3D_Triangle4 = P3D_Tree.potentials[l];
			this.SortTriangle(p3D_Triangle4, ref minIndex, ref maxIndex, p3D_Triangle4.MidZ >= num3);
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00006573 File Offset: 0x00004773
	private void SortTriangle(P3D_Triangle triangle, ref int minIndex, ref int maxIndex, bool abovePivot)
	{
		if (abovePivot)
		{
			this.triangles[maxIndex - 1] = triangle;
			maxIndex--;
			return;
		}
		this.triangles[minIndex] = triangle;
		minIndex++;
	}

	// Token: 0x04000136 RID: 310
	private static readonly List<P3D_Triangle> potentials = new List<P3D_Triangle>();

	// Token: 0x04000137 RID: 311
	private static readonly List<P3D_Result> results = new List<P3D_Result>();

	// Token: 0x04000138 RID: 312
	private static P3D_Tree tempInstance;

	// Token: 0x04000139 RID: 313
	[SerializeField]
	private Mesh mesh;

	// Token: 0x0400013A RID: 314
	[SerializeField]
	private readonly List<P3D_Node> nodes = new List<P3D_Node>();

	// Token: 0x0400013B RID: 315
	[SerializeField]
	private int subMeshIndex;

	// Token: 0x0400013C RID: 316
	[SerializeField]
	private readonly List<P3D_Triangle> triangles = new List<P3D_Triangle>();

	// Token: 0x0400013D RID: 317
	[SerializeField]
	private int vertexCount;
}
