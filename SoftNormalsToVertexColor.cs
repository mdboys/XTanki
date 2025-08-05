using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000081 RID: 129
[RequireComponent(typeof(MeshFilter))]
public class SoftNormalsToVertexColor : MonoBehaviour
{
	// Token: 0x06000268 RID: 616 RVA: 0x00006CA3 File Offset: 0x00004EA3
	private void Awake()
	{
		if (this.generateOnAwake)
		{
			this.TryGenerate();
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00006CB3 File Offset: 0x00004EB3
	private void OnDrawGizmos()
	{
		if (this.generateNow)
		{
			this.generateNow = false;
			this.TryGenerate();
		}
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00066CF4 File Offset: 0x00064EF4
	private void TryGenerate()
	{
		MeshFilter component = base.GetComponent<MeshFilter>();
		if (component == null)
		{
			Debug.LogError("MeshFilter missing on the vertex color generator", base.gameObject);
			return;
		}
		if (component.sharedMesh == null)
		{
			Debug.LogError("Assign a mesh to the MeshFilter before generating vertex colors", base.gameObject);
			return;
		}
		this.Generate(component.sharedMesh);
		Debug.Log("Vertex colors generated", base.gameObject);
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00066D60 File Offset: 0x00064F60
	[NullableContext(1)]
	private void Generate(Mesh m)
	{
		Vector3[] normals = m.normals;
		Vector3[] vertices = m.vertices;
		Color[] array = new Color[normals.Length];
		List<List<int>> list = new List<List<int>>();
		for (int i = 0; i < vertices.Length; i++)
		{
			bool flag = false;
			foreach (List<int> list2 in list)
			{
				if (vertices[list2[0]] == vertices[i])
				{
					list2.Add(i);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				List<int> list3 = new List<int>(1) { i };
				list.Add(list3);
			}
		}
		foreach (List<int> list4 in list)
		{
			Vector3 vector = Vector3.zero;
			foreach (int num in list4)
			{
				vector += normals[num];
			}
			vector.Normalize();
			if (this.method == SoftNormalsToVertexColor.Method.AngularDeviation)
			{
				float num2 = 0f;
				foreach (int num3 in list4)
				{
					num2 += Vector3.Dot(normals[num3], vector);
				}
				num2 /= (float)list4.Count;
				float num4 = Mathf.Acos(num2) * 57.29578f;
				float num5 = 180f - num4 - 90f;
				float num6 = 0.5f / Mathf.Sin(num5 * 0.017453292f);
				vector *= num6;
			}
			foreach (int num7 in list4)
			{
				array[num7] = new Color(vector.x, vector.y, vector.z);
			}
		}
		m.colors = array;
	}

	// Token: 0x040001D3 RID: 467
	public SoftNormalsToVertexColor.Method method = SoftNormalsToVertexColor.Method.AngularDeviation;

	// Token: 0x040001D4 RID: 468
	public bool generateOnAwake;

	// Token: 0x040001D5 RID: 469
	public bool generateNow;

	// Token: 0x02000082 RID: 130
	public enum Method
	{
		// Token: 0x040001D7 RID: 471
		Simple,
		// Token: 0x040001D8 RID: 472
		AngularDeviation
	}
}
