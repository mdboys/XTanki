using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200008D RID: 141
public class UIWindowBase : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	// Token: 0x060002C1 RID: 705 RVA: 0x00006FFB File Offset: 0x000051FB
	private void Start()
	{
		this.m_transform = base.GetComponent<RectTransform>();
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00007009 File Offset: 0x00005209
	[NullableContext(1)]
	public void OnDrag(PointerEventData eventData)
	{
		this.m_transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0000703C File Offset: 0x0000523C
	public void ChangeStrength(float value)
	{
		base.GetComponent<Image>().material.SetFloat("_Size", value);
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00007054 File Offset: 0x00005254
	public void ChangeVibrancy(float value)
	{
		base.GetComponent<Image>().material.SetFloat("_Vibrancy", value);
	}

	// Token: 0x04000220 RID: 544
	[Nullable(1)]
	private RectTransform m_transform;
}
