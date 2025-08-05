using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AF9 RID: 11001
	public class Edition
	{
		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060097E5 RID: 38885 RVA: 0x00007F86 File Offset: 0x00006186
		public static bool IsDecalSystemFree
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060097E6 RID: 38886 RVA: 0x0005AAF0 File Offset: 0x00058CF0
		public static bool IsDX11
		{
			get
			{
				return SystemInfo.graphicsShaderLevel >= 41;
			}
		}
	}
}
