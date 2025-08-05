using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200002D RID: 45
[NullableContext(1)]
[Nullable(0)]
public class FitIn : MonoBehaviour
{
	// Token: 0x060000BA RID: 186 RVA: 0x00005B5D File Offset: 0x00003D5D
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0005F288 File Offset: 0x0005D488
	private void Update()
	{
		RectTransform component = base.GetComponent<RectTransform>();
		float num = 0f - component.anchoredPosition.y - this.content.anchoredPosition.y + component.rect.height;
		Vector2 anchoredPosition = this.content.anchoredPosition;
		if (num > this.viewport.rect.height && this.animator.GetBool("selected"))
		{
			anchoredPosition.y += num - this.viewport.rect.height;
			this.content.anchoredPosition = anchoredPosition;
		}
		if (anchoredPosition.y + this.viewport.rect.height > this.content.rect.height && !this.animator.GetBool("selected"))
		{
			anchoredPosition.y = Mathf.Max(0f, this.content.rect.height - this.viewport.rect.height);
			this.content.anchoredPosition = anchoredPosition;
		}
	}

	// Token: 0x04000068 RID: 104
	[SerializeField]
	private RectTransform content;

	// Token: 0x04000069 RID: 105
	[SerializeField]
	private RectTransform viewport;

	// Token: 0x0400006A RID: 106
	private Animator animator;
}
