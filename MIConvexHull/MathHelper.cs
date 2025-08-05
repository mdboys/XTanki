using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x020029A3 RID: 10659
	[NullableContext(1)]
	[Nullable(0)]
	internal class MathHelper
	{
		// Token: 0x06008F4F RID: 36687 RVA: 0x001392A0 File Offset: 0x001374A0
		public MathHelper(int dimension, double[] positions)
		{
			this.PositionData = positions;
			this.Dimension = dimension;
			this.ntX = new double[this.Dimension];
			this.ntY = new double[this.Dimension];
			this.ntZ = new double[this.Dimension];
			this.nDNormalHelperVector = new double[this.Dimension];
			this.nDMatrix = new double[this.Dimension * this.Dimension];
			this.matrixPivots = new int[this.Dimension];
		}

		// Token: 0x06008F50 RID: 36688 RVA: 0x00139330 File Offset: 0x00137530
		private static void LUFactor(double[] data, int order, int[] ipiv, double[] vecLUcolj)
		{
			for (int i = 0; i < order; i++)
			{
				ipiv[i] = i;
			}
			for (int j = 0; j < order; j++)
			{
				int num = j * order;
				int num2 = num + j;
				for (int k = 0; k < order; k++)
				{
					vecLUcolj[k] = data[num + k];
				}
				for (int l = 0; l < order; l++)
				{
					int num3 = Math.Min(l, j);
					double num4 = 0.0;
					for (int m = 0; m < num3; m++)
					{
						num4 += data[m * order + l] * vecLUcolj[m];
					}
					data[num + l] = (vecLUcolj[l] -= num4);
				}
				int num5 = j;
				for (int n = j + 1; n < order; n++)
				{
					if (Math.Abs(vecLUcolj[n]) > Math.Abs(vecLUcolj[num5]))
					{
						num5 = n;
					}
				}
				if (num5 != j)
				{
					for (int num6 = 0; num6 < order; num6++)
					{
						int num7 = num6 * order;
						int num8 = num7 + num5;
						int num9 = num7 + j;
						double num10 = data[num8];
						data[num8] = data[num9];
						data[num9] = num10;
					}
					ipiv[j] = num5;
				}
				if ((j < order) & (data[num2] != 0.0))
				{
					for (int num11 = j + 1; num11 < order; num11++)
					{
						data[num + num11] /= data[num2];
					}
				}
			}
		}

		// Token: 0x06008F51 RID: 36689 RVA: 0x00139488 File Offset: 0x00137688
		private void FindNormal(int[] vertices, double[] normal)
		{
			int[] array = this.matrixPivots;
			double[] array2 = this.nDMatrix;
			double num = 0.0;
			for (int i = 0; i < this.Dimension; i++)
			{
				for (int j = 0; j < this.Dimension; j++)
				{
					int num2 = vertices[j] * this.Dimension;
					for (int k = 0; k < this.Dimension; k++)
					{
						array2[this.Dimension * k + j] = ((k != i) ? this.PositionData[num2 + k] : 1.0);
					}
				}
				MathHelper.LUFactor(array2, this.Dimension, array, this.nDNormalHelperVector);
				double num3 = 1.0;
				for (int l = 0; l < this.Dimension; l++)
				{
					num3 = ((array[l] == l) ? (num3 * array2[this.Dimension * l + l]) : (num3 * (0.0 - array2[this.Dimension * l + l])));
				}
				normal[i] = num3;
				num += num3 * num3;
			}
			double num4 = 1.0 / Math.Sqrt(num);
			for (int m = 0; m < normal.Length; m++)
			{
				normal[m] *= num4;
			}
		}

		// Token: 0x06008F52 RID: 36690 RVA: 0x001395D4 File Offset: 0x001377D4
		public static double LengthSquared(double[] x)
		{
			double num = 0.0;
			foreach (double num2 in x)
			{
				num += num2 * num2;
			}
			return num;
		}

		// Token: 0x06008F53 RID: 36691 RVA: 0x00139608 File Offset: 0x00137808
		public void SubtractFast(int x, int y, double[] target)
		{
			int num = x * this.Dimension;
			int num2 = y * this.Dimension;
			for (int i = 0; i < target.Length; i++)
			{
				target[i] = this.PositionData[num + i] - this.PositionData[num2 + i];
			}
		}

		// Token: 0x06008F54 RID: 36692 RVA: 0x00139650 File Offset: 0x00137850
		private void FindNormalVector4D(int[] vertices, double[] normal)
		{
			this.SubtractFast(vertices[1], vertices[0], this.ntX);
			this.SubtractFast(vertices[2], vertices[1], this.ntY);
			this.SubtractFast(vertices[3], vertices[2], this.ntZ);
			double[] array = this.ntX;
			double[] array2 = this.ntY;
			double[] array3 = this.ntZ;
			double num = array[3] * (array2[2] * array3[1] - array2[1] * array3[2]) + array[2] * (array2[1] * array3[3] - array2[3] * array3[1]) + array[1] * (array2[3] * array3[2] - array2[2] * array3[3]);
			double num2 = array[3] * (array2[0] * array3[2] - array2[2] * array3[0]) + array[2] * (array2[3] * array3[0] - array2[0] * array3[3]) + array[0] * (array2[2] * array3[3] - array2[3] * array3[2]);
			double num3 = array[3] * (array2[1] * array3[0] - array2[0] * array3[1]) + array[1] * (array2[0] * array3[3] - array2[3] * array3[0]) + array[0] * (array2[3] * array3[1] - array2[1] * array3[3]);
			double num4 = array[2] * (array2[0] * array3[1] - array2[1] * array3[0]) + array[1] * (array2[2] * array3[0] - array2[0] * array3[2]) + array[0] * (array2[1] * array3[2] - array2[2] * array3[1]);
			double num5 = Math.Sqrt(num * num + num2 * num2 + num3 * num3 + num4 * num4);
			double num6 = 1.0 / num5;
			normal[0] = num6 * num;
			normal[1] = num6 * num2;
			normal[2] = num6 * num3;
			normal[3] = num6 * num4;
		}

		// Token: 0x06008F55 RID: 36693 RVA: 0x001397E4 File Offset: 0x001379E4
		private void FindNormalVector3D(int[] vertices, double[] normal)
		{
			this.SubtractFast(vertices[1], vertices[0], this.ntX);
			this.SubtractFast(vertices[2], vertices[1], this.ntY);
			double[] array = this.ntX;
			double[] array2 = this.ntY;
			double num = array[1] * array2[2] - array[2] * array2[1];
			double num2 = array[2] * array2[0] - array[0] * array2[2];
			double num3 = array[0] * array2[1] - array[1] * array2[0];
			double num4 = Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			double num5 = 1.0 / num4;
			normal[0] = num5 * num;
			normal[1] = num5 * num2;
			normal[2] = num5 * num3;
		}

		// Token: 0x06008F56 RID: 36694 RVA: 0x0013988C File Offset: 0x00137A8C
		private void FindNormalVector2D(int[] vertices, double[] normal)
		{
			this.SubtractFast(vertices[1], vertices[0], this.ntX);
			double[] array = this.ntX;
			double num = 0.0 - array[1];
			double num2 = array[0];
			double num3 = Math.Sqrt(num * num + num2 * num2);
			double num4 = 1.0 / num3;
			normal[0] = num4 * num;
			normal[1] = num4 * num2;
		}

		// Token: 0x06008F57 RID: 36695 RVA: 0x001398EC File Offset: 0x00137AEC
		private void FindNormalVectorND(int[] vertices, double[] normal)
		{
			int[] array = this.matrixPivots;
			double[] array2 = this.nDMatrix;
			double num = 0.0;
			for (int i = 0; i < this.Dimension; i++)
			{
				for (int j = 0; j < this.Dimension; j++)
				{
					int num2 = vertices[j] * this.Dimension;
					for (int k = 0; k < this.Dimension; k++)
					{
						array2[this.Dimension * j + k] = ((k != i) ? this.PositionData[num2 + k] : 1.0);
					}
				}
				MathHelper.LUFactor(array2, this.Dimension, array, this.nDNormalHelperVector);
				double num3 = 1.0;
				for (int l = 0; l < this.Dimension; l++)
				{
					num3 = ((array[l] == l) ? (num3 * array2[this.Dimension * l + l]) : (num3 * (0.0 - array2[this.Dimension * l + l])));
				}
				normal[i] = num3;
				num += num3 * num3;
			}
			double num4 = 1.0 / Math.Sqrt(num);
			for (int m = 0; m < normal.Length; m++)
			{
				normal[m] *= num4;
			}
		}

		// Token: 0x06008F58 RID: 36696 RVA: 0x00139A38 File Offset: 0x00137C38
		public void FindNormalVector(int[] vertices, double[] normalData)
		{
			switch (this.Dimension)
			{
			case 2:
				this.FindNormalVector2D(vertices, normalData);
				return;
			case 3:
				this.FindNormalVector3D(vertices, normalData);
				return;
			case 4:
				this.FindNormalVector4D(vertices, normalData);
				return;
			default:
				this.FindNormalVectorND(vertices, normalData);
				return;
			}
		}

		// Token: 0x06008F59 RID: 36697 RVA: 0x00139A88 File Offset: 0x00137C88
		public bool CalculateFacePlane(ConvexFaceInternal face, double[] center)
		{
			int[] vertices = face.Vertices;
			double[] normal = face.Normal;
			this.FindNormalVector(vertices, normal);
			if (double.IsNaN(normal[0]))
			{
				return false;
			}
			double num = 0.0;
			double num2 = 0.0;
			int num3 = vertices[0] * this.Dimension;
			for (int i = 0; i < this.Dimension; i++)
			{
				double num4 = normal[i];
				num += num4 * this.PositionData[num3 + i];
				num2 += num4 * center[i];
			}
			face.Offset = 0.0 - num;
			num2 -= num;
			if (num2 > 0.0)
			{
				for (int j = 0; j < this.Dimension; j++)
				{
					normal[j] = 0.0 - normal[j];
				}
				face.Offset = num;
				face.IsNormalFlipped = true;
			}
			else
			{
				face.IsNormalFlipped = false;
			}
			return true;
		}

		// Token: 0x06008F5A RID: 36698 RVA: 0x00139B70 File Offset: 0x00137D70
		public double GetVertexDistance(int v, ConvexFaceInternal f)
		{
			double[] normal = f.Normal;
			int num = v * this.Dimension;
			double num2 = f.Offset;
			for (int i = 0; i < normal.Length; i++)
			{
				num2 += normal[i] * this.PositionData[num + i];
			}
			return num2;
		}

		// Token: 0x06008F5B RID: 36699 RVA: 0x00139BB4 File Offset: 0x00137DB4
		public static double GetSimplexVolume(ConvexFaceInternal cell, IList<IVertex> vertices, MathHelper.SimplexVolumeBuffer buffer)
		{
			int[] vertices2 = cell.Vertices;
			double[] position = vertices[vertices2[0]].Position;
			double[] data = buffer.Data;
			int dimension = buffer.Dimension;
			double num = 1.0;
			for (int i = 1; i < vertices2.Length; i++)
			{
				num *= (double)(i + 1);
				double[] position2 = vertices[vertices2[i]].Position;
				for (int j = 0; j < position2.Length; j++)
				{
					data[j * dimension + i - 1] = position2[j] - position[j];
				}
			}
			return Math.Abs(MathHelper.DeterminantDestructive(buffer)) / num;
		}

		// Token: 0x06008F5C RID: 36700 RVA: 0x00139C54 File Offset: 0x00137E54
		private static double DeterminantDestructive(MathHelper.SimplexVolumeBuffer buff)
		{
			double[] data = buff.Data;
			switch (buff.Dimension)
			{
			case 0:
				return 0.0;
			case 1:
				return data[0];
			case 2:
				return data[0] * data[3] - data[1] * data[2];
			case 3:
				return data[0] * data[4] * data[8] + data[1] * data[5] * data[6] + data[2] * data[3] * data[7] - data[0] * data[5] * data[7] - data[1] * data[3] * data[8] - data[2] * data[4] * data[6];
			default:
			{
				int[] pivots = buff.Pivots;
				int dimension = buff.Dimension;
				MathHelper.LUFactor(data, dimension, pivots, buff.Helper);
				double num = 1.0;
				for (int i = 0; i < pivots.Length; i++)
				{
					num *= data[dimension * i + i];
					if (pivots[i] != i)
					{
						num *= -1.0;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x04006037 RID: 24631
		private readonly int Dimension;

		// Token: 0x04006038 RID: 24632
		private readonly int[] matrixPivots;

		// Token: 0x04006039 RID: 24633
		private readonly double[] nDMatrix;

		// Token: 0x0400603A RID: 24634
		private readonly double[] nDNormalHelperVector;

		// Token: 0x0400603B RID: 24635
		private readonly double[] ntX;

		// Token: 0x0400603C RID: 24636
		private readonly double[] ntY;

		// Token: 0x0400603D RID: 24637
		private readonly double[] ntZ;

		// Token: 0x0400603E RID: 24638
		private readonly double[] PositionData;

		// Token: 0x020029A4 RID: 10660
		[Nullable(0)]
		public class SimplexVolumeBuffer
		{
			// Token: 0x06008F5D RID: 36701 RVA: 0x00055DE1 File Offset: 0x00053FE1
			public SimplexVolumeBuffer(int dimension)
			{
				this.Dimension = dimension;
				this.Data = new double[dimension * dimension];
				this.Helper = new double[dimension];
				this.Pivots = new int[dimension];
			}

			// Token: 0x0400603F RID: 24639
			public double[] Data;

			// Token: 0x04006040 RID: 24640
			public int Dimension;

			// Token: 0x04006041 RID: 24641
			public double[] Helper;

			// Token: 0x04006042 RID: 24642
			public int[] Pivots;
		}
	}
}
