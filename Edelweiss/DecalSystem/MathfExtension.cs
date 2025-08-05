using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B05 RID: 11013
	internal static class MathfExtension
	{
		// Token: 0x06009897 RID: 39063 RVA: 0x0014F838 File Offset: 0x0014DA38
		public static bool Approximately(float a_Float1, float a_Float2, float a_MaximumAbsoluteError, float a_MaximumRelativeError)
		{
			bool flag = false;
			float num = Mathf.Abs(a_Float1 - a_Float2);
			if (num <= a_MaximumAbsoluteError)
			{
				flag = true;
			}
			else
			{
				float num2 = Mathf.Abs(a_Float1);
				float num3 = Mathf.Abs(a_Float2);
				float num4 = Mathf.Max(num2, num3);
				if (num <= num4 * a_MaximumRelativeError)
				{
					flag = true;
				}
			}
			return flag;
		}
	}
}
