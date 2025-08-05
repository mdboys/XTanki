using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B20 RID: 11040
	internal static class Vector2Extension
	{
		// Token: 0x0600991C RID: 39196 RVA: 0x0005B69F File Offset: 0x0005989F
		public static bool Approximately(Vector2 a_Vector1, Vector2 a_Vector2)
		{
			return Mathf.Approximately(a_Vector1.x, a_Vector2.x) && Mathf.Approximately(a_Vector1.y, a_Vector2.y);
		}

		// Token: 0x0600991D RID: 39197 RVA: 0x0005B6C7 File Offset: 0x000598C7
		public static bool Approximately(Vector2 a_Vector1, Vector2 a_Vector2, float a_MaximumAbsoluteError, float a_MaximumRelativeError)
		{
			return MathfExtension.Approximately(a_Vector1.x, a_Vector2.x, a_MaximumAbsoluteError, a_MaximumRelativeError) && MathfExtension.Approximately(a_Vector1.y, a_Vector2.y, a_MaximumAbsoluteError, a_MaximumRelativeError);
		}
	}
}
