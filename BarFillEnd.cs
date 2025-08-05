using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000010 RID: 16
[ExecuteInEditMode]
public class BarFillEnd : MonoBehaviour
{
	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000036 RID: 54 RVA: 0x000056A0 File Offset: 0x000038A0
	// (set) Token: 0x06000037 RID: 55 RVA: 0x0005E0D8 File Offset: 0x0005C2D8
	public virtual float FillAmount
	{
		get
		{
			return this.fillAmount;
		}
		set
		{
			this.fillAmount = value;
			this.image.anchoredPosition = new Vector2(base.GetComponent<RectTransform>().rect.width * value, this.image.anchoredPosition.y);
			this.image.gameObject.SetActive(value != 0f && value != 1f);
		}
	}

	// Token: 0x04000017 RID: 23
	[Nullable(1)]
	[SerializeField]
	protected RectTransform image;

	// Token: 0x04000018 RID: 24
	private float fillAmount;
}
