using System;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

namespace CurvedUI
{
	// Token: 0x02002CBC RID: 11452
	public class CurvedUIEventSystem : EventSystem
	{
		// Token: 0x06009F38 RID: 40760 RVA: 0x0005C646 File Offset: 0x0005A846
		protected override void Awake()
		{
			base.Awake();
			CurvedUIEventSystem.instance = this;
		}

		// Token: 0x06009F39 RID: 40761 RVA: 0x0005C654 File Offset: 0x0005A854
		protected override void OnApplicationFocus(bool hasFocus)
		{
			base.OnApplicationFocus(true);
		}

		// Token: 0x0400670B RID: 26379
		[Nullable(1)]
		public static CurvedUIEventSystem instance;
	}
}
