using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B21 RID: 11041
	internal static class Vector3Extension
	{
		// Token: 0x0600991E RID: 39198 RVA: 0x001536A8 File Offset: 0x001518A8
		public static Vector3 MirrorAtPlane(this Vector3 a_Vector, Vector3 a_PlaneNormal)
		{
			float num = Vector3.Dot(a_Vector, a_PlaneNormal);
			if (Mathf.Approximately(num, 0f))
			{
				return a_Vector;
			}
			float num2 = Vector3.SqrMagnitude(a_PlaneNormal);
			float num3 = -2f * num / num2;
			return a_Vector + num3 * a_PlaneNormal;
		}

		// Token: 0x0600991F RID: 39199 RVA: 0x0005B6F3 File Offset: 0x000598F3
		public static bool Approximately(Vector3 a_Vector1, Vector3 a_Vector2)
		{
			return Mathf.Approximately(a_Vector1.x, a_Vector2.x) && Mathf.Approximately(a_Vector1.y, a_Vector2.y) && Mathf.Approximately(a_Vector1.z, a_Vector2.z);
		}

		// Token: 0x06009920 RID: 39200 RVA: 0x001536EC File Offset: 0x001518EC
		public static bool Approximately(Vector3 a_Vector1, Vector3 a_Vector2, float a_MaximumAbsoluteError, float a_MaximumRelativeError)
		{
			return MathfExtension.Approximately(a_Vector1.x, a_Vector2.x, a_MaximumAbsoluteError, a_MaximumRelativeError) && MathfExtension.Approximately(a_Vector1.y, a_Vector2.y, a_MaximumAbsoluteError, a_MaximumRelativeError) && MathfExtension.Approximately(a_Vector1.z, a_Vector2.z, a_MaximumAbsoluteError, a_MaximumRelativeError);
		}
	}
}
