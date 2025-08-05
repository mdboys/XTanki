using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CurvedUI
{
	// Token: 0x02002CBF RID: 11455
	public class CurvedUIPointerEventData : PointerEventData
	{
		// Token: 0x06009F5D RID: 40797 RVA: 0x0005C82C File Offset: 0x0005AA2C
		[NullableContext(1)]
		public CurvedUIPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x04006713 RID: 26387
		[Nullable(1)]
		public GameObject Controller;

		// Token: 0x04006714 RID: 26388
		public Vector2 TouchPadAxis = Vector2.zero;

		// Token: 0x02002CC0 RID: 11456
		public enum ControllerType
		{
			// Token: 0x04006716 RID: 26390
			NONE = -1,
			// Token: 0x04006717 RID: 26391
			VIVE
		}
	}
}
