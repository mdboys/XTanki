using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B22 RID: 11042
	internal static class Vector4Extension
	{
		// Token: 0x06009921 RID: 39201 RVA: 0x00153738 File Offset: 0x00151938
		public static bool Approximately(Vector4 a_Vector1, Vector4 a_Vector2)
		{
			return Mathf.Approximately(a_Vector1.x, a_Vector2.x) && Mathf.Approximately(a_Vector1.y, a_Vector2.y) && Mathf.Approximately(a_Vector1.z, a_Vector2.z) && Mathf.Approximately(a_Vector1.w, a_Vector2.w);
		}

		// Token: 0x06009922 RID: 39202 RVA: 0x00153794 File Offset: 0x00151994
		public static bool Approximately(Vector4 a_Vector1, Vector4 a_Vector2, float a_MaximumAbsoluteError, float a_MaximumRelativeError)
		{
			return MathfExtension.Approximately(a_Vector1.x, a_Vector2.x, a_MaximumAbsoluteError, a_MaximumRelativeError) && MathfExtension.Approximately(a_Vector1.y, a_Vector2.y, a_MaximumAbsoluteError, a_MaximumRelativeError) && MathfExtension.Approximately(a_Vector1.z, a_Vector2.z, a_MaximumAbsoluteError, a_MaximumRelativeError) && MathfExtension.Approximately(a_Vector1.w, a_Vector2.w, a_MaximumAbsoluteError, a_MaximumRelativeError);
		}
	}
}
