using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public struct P3D_Matrix
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600016C RID: 364 RVA: 0x000628E8 File Offset: 0x00060AE8
	public static P3D_Matrix Identity
	{
		get
		{
			return new P3D_Matrix
			{
				m00 = 1f,
				m10 = 0f,
				m20 = 0f,
				m01 = 0f,
				m11 = 1f,
				m21 = 0f,
				m02 = 0f,
				m12 = 0f,
				m22 = 1f
			};
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600016D RID: 365 RVA: 0x0006296C File Offset: 0x00060B6C
	public P3D_Matrix Inverse
	{
		get
		{
			double num = (double)(this.m00 * (this.m11 * this.m22 - this.m21 * this.m12) - this.m01 * (this.m10 * this.m22 - this.m12 * this.m20) + this.m02 * (this.m10 * this.m21 - this.m11 * this.m20));
			if (num != 0.0)
			{
				float num2 = (float)(1.0 / num);
				return new P3D_Matrix
				{
					m00 = (this.m11 * this.m22 - this.m21 * this.m12) * num2,
					m10 = (0f - (this.m10 * this.m22 - this.m12 * this.m20)) * num2,
					m20 = (this.m10 * this.m21 - this.m20 * this.m11) * num2,
					m01 = (0f - (this.m01 * this.m22 - this.m02 * this.m21)) * num2,
					m11 = (this.m00 * this.m22 - this.m02 * this.m20) * num2,
					m12 = (0f - (this.m00 * this.m12 - this.m10 * this.m02)) * num2,
					m02 = (this.m01 * this.m12 - this.m02 * this.m11) * num2,
					m22 = (this.m00 * this.m11 - this.m10 * this.m01) * num2,
					m21 = (0f - (this.m00 * this.m21 - this.m20 * this.m01)) * num2
				};
			}
			return P3D_Matrix.Identity;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600016E RID: 366 RVA: 0x00062B6C File Offset: 0x00060D6C
	public Matrix4x4 Matrix4x4
	{
		get
		{
			Matrix4x4 identity = Matrix4x4.identity;
			identity.m00 = this.m00;
			identity.m10 = this.m10;
			identity.m20 = this.m20;
			identity.m01 = this.m01;
			identity.m11 = this.m11;
			identity.m21 = this.m21;
			identity.m02 = this.m02;
			identity.m12 = this.m12;
			identity.m22 = this.m22;
			return identity;
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00062BF8 File Offset: 0x00060DF8
	public static P3D_Matrix Translation(float x, float y)
	{
		return new P3D_Matrix
		{
			m00 = 1f,
			m10 = 0f,
			m20 = 0f,
			m01 = 0f,
			m11 = 1f,
			m21 = 0f,
			m02 = x,
			m12 = y,
			m22 = 1f
		};
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00062C74 File Offset: 0x00060E74
	public static P3D_Matrix Scaling(float x, float y)
	{
		return new P3D_Matrix
		{
			m00 = x,
			m10 = 0f,
			m20 = 0f,
			m01 = 0f,
			m11 = y,
			m21 = 0f,
			m02 = 0f,
			m12 = 0f,
			m22 = 1f
		};
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00062CF0 File Offset: 0x00060EF0
	public static P3D_Matrix Rotation(float a)
	{
		float num = Mathf.Sin(a);
		float num2 = Mathf.Cos(a);
		return new P3D_Matrix
		{
			m00 = num2,
			m10 = 0f - num,
			m20 = 0f,
			m01 = num,
			m11 = num2,
			m21 = 0f,
			m02 = 0f,
			m12 = 0f,
			m22 = 1f
		};
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00062D78 File Offset: 0x00060F78
	public Vector2 MultiplyPoint(Vector2 v)
	{
		return new Vector2
		{
			x = this.m00 * v.x + this.m01 * v.y + this.m02,
			y = this.m10 * v.x + this.m11 * v.y + this.m12
		};
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00062DE0 File Offset: 0x00060FE0
	public Vector2 MultiplyPoint(float x, float y)
	{
		return new Vector2
		{
			x = this.m00 * x + this.m01 * y + this.m02,
			y = this.m10 * x + this.m11 * y + this.m12
		};
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00062E34 File Offset: 0x00061034
	public static P3D_Matrix operator *(P3D_Matrix lhs, P3D_Matrix rhs)
	{
		return new P3D_Matrix
		{
			m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20,
			m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21,
			m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22,
			m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20,
			m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21,
			m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22,
			m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20,
			m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21,
			m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22
		};
	}

	// Token: 0x040000FE RID: 254
	public float m00;

	// Token: 0x040000FF RID: 255
	public float m10;

	// Token: 0x04000100 RID: 256
	public float m20;

	// Token: 0x04000101 RID: 257
	public float m01;

	// Token: 0x04000102 RID: 258
	public float m11;

	// Token: 0x04000103 RID: 259
	public float m21;

	// Token: 0x04000104 RID: 260
	public float m02;

	// Token: 0x04000105 RID: 261
	public float m12;

	// Token: 0x04000106 RID: 262
	public float m22;
}
