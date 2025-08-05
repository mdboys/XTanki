using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020029A9 RID: 10665
	[NullableContext(1)]
	[Nullable(0)]
	public class CombineUtility
	{
		// Token: 0x06008F74 RID: 36724 RVA: 0x00139F94 File Offset: 0x00138194
		public static Mesh Combine(CombineUtility.MeshInstance[] combines, bool generateStrips)
		{
			CombineUtility.vertexCount = 0;
			CombineUtility.triangleCount = 0;
			CombineUtility.stripCount = 0;
			foreach (CombineUtility.MeshInstance meshInstance in combines)
			{
				if (meshInstance.mesh)
				{
					CombineUtility.vertexCount += meshInstance.mesh.vertexCount;
					if (generateStrips)
					{
						CombineUtility.curStripCount = meshInstance.mesh.GetTriangles(meshInstance.subMeshIndex).Length;
						if (CombineUtility.curStripCount != 0)
						{
							if (CombineUtility.stripCount != 0)
							{
								if ((CombineUtility.stripCount & 1) == 1)
								{
									CombineUtility.stripCount += 3;
								}
								else
								{
									CombineUtility.stripCount += 2;
								}
							}
							CombineUtility.stripCount += CombineUtility.curStripCount;
						}
						else
						{
							generateStrips = false;
						}
					}
				}
			}
			if (!generateStrips)
			{
				foreach (CombineUtility.MeshInstance meshInstance2 in combines)
				{
					if (meshInstance2.mesh)
					{
						CombineUtility.triangleCount += meshInstance2.mesh.GetTriangles(meshInstance2.subMeshIndex).Length;
					}
				}
			}
			CombineUtility.vertices = new Vector3[CombineUtility.vertexCount];
			CombineUtility.normals = new Vector3[CombineUtility.vertexCount];
			CombineUtility.tangents = new Vector4[CombineUtility.vertexCount];
			CombineUtility.uv = new Vector2[CombineUtility.vertexCount];
			CombineUtility.uv1 = new Vector2[CombineUtility.vertexCount];
			CombineUtility.colors = new Color[CombineUtility.vertexCount];
			CombineUtility.triangles = new int[CombineUtility.triangleCount];
			CombineUtility.strip = new int[CombineUtility.stripCount];
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance3 in combines)
			{
				if (meshInstance3.mesh)
				{
					CombineUtility.Copy(meshInstance3.mesh.vertexCount, meshInstance3.mesh.vertices, CombineUtility.vertices, ref CombineUtility.offset, meshInstance3.transform);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance4 in combines)
			{
				if (meshInstance4.mesh)
				{
					CombineUtility.invTranspose = meshInstance4.transform;
					CombineUtility.invTranspose = CombineUtility.invTranspose.inverse.transpose;
					CombineUtility.CopyNormal(meshInstance4.mesh.vertexCount, meshInstance4.mesh.normals, CombineUtility.normals, ref CombineUtility.offset, CombineUtility.invTranspose);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance5 in combines)
			{
				if (meshInstance5.mesh)
				{
					CombineUtility.invTranspose = meshInstance5.transform;
					CombineUtility.invTranspose = CombineUtility.invTranspose.inverse.transpose;
					CombineUtility.CopyTangents(meshInstance5.mesh.vertexCount, meshInstance5.mesh.tangents, CombineUtility.tangents, ref CombineUtility.offset, CombineUtility.invTranspose);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance6 in combines)
			{
				if (meshInstance6.mesh)
				{
					CombineUtility.Copy(meshInstance6.mesh.vertexCount, meshInstance6.mesh.uv, CombineUtility.uv, ref CombineUtility.offset);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance7 in combines)
			{
				if (meshInstance7.mesh)
				{
					CombineUtility.Copy(meshInstance7.mesh.vertexCount, meshInstance7.mesh.uv2, CombineUtility.uv1, ref CombineUtility.offset);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance8 in combines)
			{
				if (meshInstance8.mesh)
				{
					CombineUtility.CopyColors(meshInstance8.mesh.vertexCount, meshInstance8.mesh.colors, CombineUtility.colors, ref CombineUtility.offset);
				}
			}
			CombineUtility.triangleOffset = 0;
			CombineUtility.stripOffset = 0;
			CombineUtility.vertexOffset = 0;
			foreach (CombineUtility.MeshInstance meshInstance9 in combines)
			{
				if (meshInstance9.mesh)
				{
					if (generateStrips)
					{
						int[] array = meshInstance9.mesh.GetTriangles(meshInstance9.subMeshIndex);
						if (CombineUtility.stripOffset != 0)
						{
							if ((CombineUtility.stripOffset & 1) == 1)
							{
								CombineUtility.strip[CombineUtility.stripOffset] = CombineUtility.strip[CombineUtility.stripOffset - 1];
								CombineUtility.strip[CombineUtility.stripOffset + 1] = array[0] + CombineUtility.vertexOffset;
								CombineUtility.strip[CombineUtility.stripOffset + 2] = array[0] + CombineUtility.vertexOffset;
								CombineUtility.stripOffset += 3;
							}
							else
							{
								CombineUtility.strip[CombineUtility.stripOffset] = CombineUtility.strip[CombineUtility.stripOffset - 1];
								CombineUtility.strip[CombineUtility.stripOffset + 1] = array[0] + CombineUtility.vertexOffset;
								CombineUtility.stripOffset += 2;
							}
						}
						for (int num4 = 0; num4 < array.Length; num4++)
						{
							CombineUtility.strip[num4 + CombineUtility.stripOffset] = array[num4] + CombineUtility.vertexOffset;
						}
						CombineUtility.stripOffset += array.Length;
					}
					else
					{
						int[] array2 = meshInstance9.mesh.GetTriangles(meshInstance9.subMeshIndex);
						for (int num5 = 0; num5 < array2.Length; num5++)
						{
							CombineUtility.triangles[num5 + CombineUtility.triangleOffset] = array2[num5] + CombineUtility.vertexOffset;
						}
						CombineUtility.triangleOffset += array2.Length;
					}
					CombineUtility.vertexOffset += meshInstance9.mesh.vertexCount;
				}
			}
			Mesh mesh = new Mesh
			{
				name = "Combined Mesh",
				vertices = CombineUtility.vertices,
				normals = CombineUtility.normals,
				colors = CombineUtility.colors,
				uv = CombineUtility.uv,
				uv2 = CombineUtility.uv1,
				tangents = CombineUtility.tangents
			};
			if (generateStrips)
			{
				mesh.SetTriangles(CombineUtility.strip, 0);
			}
			else
			{
				mesh.triangles = CombineUtility.triangles;
			}
			return mesh;
		}

		// Token: 0x06008F75 RID: 36725 RVA: 0x0013A590 File Offset: 0x00138790
		private static void Copy(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = transform.MultiplyPoint(src[i]);
			}
			offset += vertexcount;
		}

		// Token: 0x06008F76 RID: 36726 RVA: 0x0013A5D0 File Offset: 0x001387D0
		private static void CopyNormal(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = transform.MultiplyVector(src[i]).normalized;
			}
			offset += vertexcount;
		}

		// Token: 0x06008F77 RID: 36727 RVA: 0x0013A618 File Offset: 0x00138818
		private static void Copy(int vertexcount, Vector2[] src, Vector2[] dst, ref int offset)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = src[i];
			}
			offset += vertexcount;
		}

		// Token: 0x06008F78 RID: 36728 RVA: 0x0013A650 File Offset: 0x00138850
		private static void CopyColors(int vertexcount, Color[] src, Color[] dst, ref int offset)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = src[i];
			}
			offset += vertexcount;
		}

		// Token: 0x06008F79 RID: 36729 RVA: 0x0013A688 File Offset: 0x00138888
		private static void CopyTangents(int vertexcount, Vector4[] src, Vector4[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				CombineUtility.p4 = src[i];
				CombineUtility.p = new Vector3(CombineUtility.p4.x, CombineUtility.p4.y, CombineUtility.p4.z);
				CombineUtility.p = transform.MultiplyVector(CombineUtility.p).normalized;
				dst[i + offset] = new Vector4(CombineUtility.p.x, CombineUtility.p.y, CombineUtility.p.z, CombineUtility.p4.w);
			}
			offset += vertexcount;
		}

		// Token: 0x04006053 RID: 24659
		public const string combinedMeshName = "Combined Mesh";

		// Token: 0x04006054 RID: 24660
		private static int vertexCount;

		// Token: 0x04006055 RID: 24661
		private static int triangleCount;

		// Token: 0x04006056 RID: 24662
		private static int stripCount;

		// Token: 0x04006057 RID: 24663
		private static int curStripCount;

		// Token: 0x04006058 RID: 24664
		private static Vector3[] vertices;

		// Token: 0x04006059 RID: 24665
		private static Vector3[] normals;

		// Token: 0x0400605A RID: 24666
		private static Vector4[] tangents;

		// Token: 0x0400605B RID: 24667
		private static Vector2[] uv;

		// Token: 0x0400605C RID: 24668
		private static Vector2[] uv1;

		// Token: 0x0400605D RID: 24669
		private static Color[] colors;

		// Token: 0x0400605E RID: 24670
		private static int[] triangles;

		// Token: 0x0400605F RID: 24671
		private static int[] strip;

		// Token: 0x04006060 RID: 24672
		private static int offset;

		// Token: 0x04006061 RID: 24673
		private static int triangleOffset;

		// Token: 0x04006062 RID: 24674
		private static int stripOffset;

		// Token: 0x04006063 RID: 24675
		private static int vertexOffset;

		// Token: 0x04006064 RID: 24676
		private static Matrix4x4 invTranspose;

		// Token: 0x04006065 RID: 24677
		private static Vector4 p4;

		// Token: 0x04006066 RID: 24678
		private static Vector3 p;

		// Token: 0x020029AA RID: 10666
		[NullableContext(0)]
		public struct MeshInstance
		{
			// Token: 0x04006067 RID: 24679
			[Nullable(1)]
			public Mesh mesh;

			// Token: 0x04006068 RID: 24680
			public int subMeshIndex;

			// Token: 0x04006069 RID: 24681
			public Matrix4x4 transform;
		}
	}
}
