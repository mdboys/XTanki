using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000047 RID: 71
public class NotificationRowComponent : MonoBehaviour
{
	// Token: 0x06000120 RID: 288 RVA: 0x000609C8 File Offset: 0x0005EBC8
	private void Awake()
	{
		HorizontalLayoutGroup component = base.GetComponent<HorizontalLayoutGroup>();
		if (Screen.height > 1080)
		{
			component.spacing = (float)(Screen.width / 5);
		}
	}
}
