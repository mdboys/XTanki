﻿using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B06 RID: 11014
	public static class MatrixExtension
	{
		// Token: 0x06009898 RID: 39064 RVA: 0x0014F878 File Offset: 0x0014DA78
		public static float Determinant(this Matrix4x4 a_Matrix)
		{
			return a_Matrix.m00 * (a_Matrix.m11 * (a_Matrix.m22 * a_Matrix.m33 - a_Matrix.m32 * a_Matrix.m23) - a_Matrix.m12 * (a_Matrix.m21 * a_Matrix.m33 - a_Matrix.m31 * a_Matrix.m23) + a_Matrix.m13 * (a_Matrix.m21 * a_Matrix.m32 - a_Matrix.m31 * a_Matrix.m22)) - a_Matrix.m01 * (a_Matrix.m10 * (a_Matrix.m22 * a_Matrix.m33 - a_Matrix.m32 * a_Matrix.m23) - a_Matrix.m12 * (a_Matrix.m20 * a_Matrix.m33 - a_Matrix.m30 * a_Matrix.m23) + a_Matrix.m13 * (a_Matrix.m20 * a_Matrix.m32 - a_Matrix.m30 * a_Matrix.m22)) + a_Matrix.m02 * (a_Matrix.m10 * (a_Matrix.m21 * a_Matrix.m33 - a_Matrix.m31 * a_Matrix.m23) - a_Matrix.m11 * (a_Matrix.m20 * a_Matrix.m33 - a_Matrix.m30 * a_Matrix.m23) + a_Matrix.m13 * (a_Matrix.m20 * a_Matrix.m31 - a_Matrix.m30 * a_Matrix.m21)) - a_Matrix.m03 * (a_Matrix.m10 * (a_Matrix.m21 * a_Matrix.m32 - a_Matrix.m31 * a_Matrix.m22) - a_Matrix.m11 * (a_Matrix.m20 * a_Matrix.m32 - a_Matrix.m30 * a_Matrix.m22) + a_Matrix.m12 * (a_Matrix.m20 * a_Matrix.m31 - a_Matrix.m30 * a_Matrix.m21));
		}

		// Token: 0x06009899 RID: 39065 RVA: 0x0014FA44 File Offset: 0x0014DC44
		public static Matrix4x4 Lerp(Matrix4x4 a_From, Matrix4x4 a_To, float a_Value)
		{
			Matrix4x4 matrix4x = default(Matrix4x4);
			for (int i = 0; i < 16; i++)
			{
				matrix4x[i] = Mathf.Lerp(a_From[i], a_To[i], a_Value);
			}
			return matrix4x;
		}
	}
}
