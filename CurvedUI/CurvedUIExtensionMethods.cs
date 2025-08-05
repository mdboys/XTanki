using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CurvedUI
{
	// Token: 0x02002CBD RID: 11453
	[NullableContext(1)]
	[Nullable(0)]
	public static class CurvedUIExtensionMethods
	{
		// Token: 0x06009F3B RID: 40763 RVA: 0x0005C665 File Offset: 0x0005A865
		public static bool AlmostEqual(this Vector3 a, Vector3 b, double accuracy = 0.01)
		{
			return (double)Vector3.SqrMagnitude(a - b) < accuracy;
		}

		// Token: 0x06009F3C RID: 40764 RVA: 0x0005C677 File Offset: 0x0005A877
		public static float Remap(this float value, float from1, float to1, float from2, float to2)
		{
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

		// Token: 0x06009F3D RID: 40765 RVA: 0x0005C687 File Offset: 0x0005A887
		public static float RemapAndClamp(this float value, float from1, float to1, float from2, float to2)
		{
			return value.Remap(from1, to1, from2, to2).Clamp(from2, to2);
		}

		// Token: 0x06009F3E RID: 40766 RVA: 0x0005C69C File Offset: 0x0005A89C
		public static float Remap(this int value, float from1, float to1, float from2, float to2)
		{
			return ((float)value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

		// Token: 0x06009F3F RID: 40767 RVA: 0x0005C677 File Offset: 0x0005A877
		public static double Remap(this double value, double from1, double to1, double from2, double to2)
		{
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

		// Token: 0x06009F40 RID: 40768 RVA: 0x0005C6AD File Offset: 0x0005A8AD
		public static float Clamp(this float value, float min, float max)
		{
			return Mathf.Clamp(value, min, max);
		}

		// Token: 0x06009F41 RID: 40769 RVA: 0x0005C6B7 File Offset: 0x0005A8B7
		public static float Clamp(this int value, int min, int max)
		{
			return (float)Mathf.Clamp(value, min, max);
		}

		// Token: 0x06009F42 RID: 40770 RVA: 0x0005C6C2 File Offset: 0x0005A8C2
		public static int Abs(this int value)
		{
			return Mathf.Abs(value);
		}

		// Token: 0x06009F43 RID: 40771 RVA: 0x0005C6CA File Offset: 0x0005A8CA
		public static float Abs(this float value)
		{
			return Mathf.Abs(value);
		}

		// Token: 0x06009F44 RID: 40772 RVA: 0x0005C6D2 File Offset: 0x0005A8D2
		public static int ToInt(this float value)
		{
			return Mathf.RoundToInt(value);
		}

		// Token: 0x06009F45 RID: 40773 RVA: 0x0005C6DA File Offset: 0x0005A8DA
		public static int FloorToInt(this float value)
		{
			return Mathf.FloorToInt(value);
		}

		// Token: 0x06009F46 RID: 40774 RVA: 0x0005C6DA File Offset: 0x0005A8DA
		public static int CeilToInt(this float value)
		{
			return Mathf.FloorToInt(value);
		}

		// Token: 0x06009F47 RID: 40775 RVA: 0x0005C6E2 File Offset: 0x0005A8E2
		public static Vector3 ModifyX(this Vector3 trans, float newVal)
		{
			trans = new Vector3(newVal, trans.y, trans.z);
			return trans;
		}

		// Token: 0x06009F48 RID: 40776 RVA: 0x0005C6F9 File Offset: 0x0005A8F9
		public static Vector3 ModifyY(this Vector3 trans, float newVal)
		{
			trans = new Vector3(trans.x, newVal, trans.z);
			return trans;
		}

		// Token: 0x06009F49 RID: 40777 RVA: 0x0005C710 File Offset: 0x0005A910
		public static Vector3 ModifyZ(this Vector3 trans, float newVal)
		{
			trans = new Vector3(trans.x, trans.y, newVal);
			return trans;
		}

		// Token: 0x06009F4A RID: 40778 RVA: 0x0005C727 File Offset: 0x0005A927
		public static Vector2 ModifyVectorX(this Vector2 trans, float newVal)
		{
			trans = new Vector3(newVal, trans.y);
			return trans;
		}

		// Token: 0x06009F4B RID: 40779 RVA: 0x0005C73D File Offset: 0x0005A93D
		public static Vector2 ModifyVectorY(this Vector2 trans, float newVal)
		{
			trans = new Vector3(trans.x, newVal);
			return trans;
		}

		// Token: 0x06009F4C RID: 40780 RVA: 0x0005C753 File Offset: 0x0005A953
		public static void ResetTransform(this Transform trans)
		{
			trans.localPosition = Vector3.zero;
			trans.localRotation = Quaternion.identity;
			trans.localScale = Vector3.one;
		}

		// Token: 0x06009F4D RID: 40781 RVA: 0x0005C776 File Offset: 0x0005A976
		public static T AddComponentIfMissing<[Nullable(0)] T>(this GameObject go) where T : Component
		{
			if (go.GetComponent<T>() == null)
			{
				return go.AddComponent<T>();
			}
			return go.GetComponent<T>();
		}

		// Token: 0x06009F4E RID: 40782 RVA: 0x0005C798 File Offset: 0x0005A998
		public static T AddComponentIfMissing<[Nullable(0)] T>(this Component go) where T : Component
		{
			return go.gameObject.AddComponentIfMissing<T>();
		}
	}
}
