using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000016 RID: 22
public class Clicker : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x0600004D RID: 77 RVA: 0x0000568E File Offset: 0x0000388E
	private void Start()
	{
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000568E File Offset: 0x0000388E
	private void Update()
	{
	}

	// Token: 0x0600004F RID: 79 RVA: 0x0005E5F4 File Offset: 0x0005C7F4
	[NullableContext(1)]
	public void OnPointerClick(PointerEventData eventData)
	{
		if (Clicker.selected != null)
		{
			Clicker.selected.GetComponent<Animator>().SetBool("selected", false);
		}
		if (Clicker.selected != this)
		{
			Clicker.selected = this;
			base.GetComponent<Animator>().SetBool("selected", true);
			return;
		}
		Clicker.selected = null;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000057A6 File Offset: 0x000039A6
	[NullableContext(1)]
	public void OnPointerEnter(PointerEventData eventData)
	{
		base.GetComponent<Animator>().SetBool("over", true);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000057B9 File Offset: 0x000039B9
	[NullableContext(1)]
	public void OnPointerExit(PointerEventData eventData)
	{
		base.GetComponent<Animator>().SetBool("over", false);
	}

	// Token: 0x04000028 RID: 40
	[Nullable(1)]
	private static Clicker selected;
}
