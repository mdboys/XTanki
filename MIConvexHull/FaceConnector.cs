using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x0200299F RID: 10655
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class FaceConnector
	{
		// Token: 0x06008F43 RID: 36675 RVA: 0x00055D4E File Offset: 0x00053F4E
		public FaceConnector(int dimension)
		{
			this.Vertices = new int[dimension - 1];
		}

		// Token: 0x06008F44 RID: 36676 RVA: 0x0013908C File Offset: 0x0013728C
		public void Update(ConvexFaceInternal face, int edgeIndex, int dim)
		{
			this.Face = face;
			this.EdgeIndex = edgeIndex;
			uint num = 23U;
			int[] vertices = face.Vertices;
			int num2 = 0;
			for (int i = 0; i < edgeIndex; i++)
			{
				this.Vertices[num2++] = vertices[i];
				num += 31U * num + (uint)vertices[i];
			}
			for (int j = edgeIndex + 1; j < vertices.Length; j++)
			{
				this.Vertices[num2++] = vertices[j];
				num += 31U * num + (uint)vertices[j];
			}
			this.HashCode = num;
		}

		// Token: 0x06008F45 RID: 36677 RVA: 0x00139110 File Offset: 0x00137310
		public static bool AreConnectable(FaceConnector a, FaceConnector b, int dim)
		{
			if (a.HashCode != b.HashCode)
			{
				return false;
			}
			int[] vertices = a.Vertices;
			int[] vertices2 = b.Vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (vertices[i] != vertices2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008F46 RID: 36678 RVA: 0x00055D64 File Offset: 0x00053F64
		public static void Connect(FaceConnector a, FaceConnector b)
		{
			a.Face.AdjacentFaces[a.EdgeIndex] = b.Face.Index;
			b.Face.AdjacentFaces[b.EdgeIndex] = a.Face.Index;
		}

		// Token: 0x0400602F RID: 24623
		public int EdgeIndex;

		// Token: 0x04006030 RID: 24624
		public ConvexFaceInternal Face;

		// Token: 0x04006031 RID: 24625
		public uint HashCode;

		// Token: 0x04006032 RID: 24626
		public FaceConnector Next;

		// Token: 0x04006033 RID: 24627
		public FaceConnector Previous;

		// Token: 0x04006034 RID: 24628
		public int[] Vertices;
	}
}
