using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200001E RID: 30
[NullableContext(1)]
[Nullable(0)]
[RequireComponent(typeof(Image))]
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06000091 RID: 145 RVA: 0x00005A24 File Offset: 0x00003C24
	private void Awake()
	{
		this.itemContent = base.transform.GetChild(0).gameObject;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0005ED50 File Offset: 0x0005CF50
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Right)
		{
			DragAndDropItem.sourceCell = base.GetComponentInParent<DragAndDropCell>();
			DragAndDropItem.draggedItem = this;
			DragAndDropItem.draggedItemContentCopy = this.GetCopy(this.itemContent);
			Canvas componentInParent = DragAndDropItem.sourceCell.transform.parent.GetComponentInParent<Canvas>();
			if (componentInParent != null)
			{
				DragAndDropItem.draggedItemContentCopy.transform.SetParent(componentInParent.transform, false);
				DragAndDropItem.draggedItemContentCopy.transform.SetAsLastSibling();
			}
			this.MakeVisible(false);
			DragAndDropItem.DragEvent onItemDragStartEvent = DragAndDropItem.OnItemDragStartEvent;
			if (onItemDragStartEvent == null)
			{
				return;
			}
			onItemDragStartEvent(this, eventData);
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
	public void OnDrag(PointerEventData data)
	{
		if (data.button != PointerEventData.InputButton.Right && DragAndDropItem.draggedItemContentCopy != null)
		{
			Canvas componentInParent = DragAndDropItem.sourceCell.transform.parent.GetComponentInParent<Canvas>();
			Vector2 vector;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(componentInParent.GetComponent<RectTransform>(), Input.mousePosition, componentInParent.worldCamera, out vector))
			{
				DragAndDropItem.draggedItemContentCopy.GetComponent<RectTransform>().anchoredPosition = vector;
			}
		}
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0005EE4C File Offset: 0x0005D04C
	public void OnEndDrag(PointerEventData eventData)
	{
		if (eventData == null || eventData.button != PointerEventData.InputButton.Right)
		{
			if (DragAndDropItem.draggedItemContentCopy != null)
			{
				global::UnityEngine.Object.Destroy(DragAndDropItem.draggedItemContentCopy);
			}
			this.MakeVisible(true);
			DragAndDropItem.DragEvent onItemDragEndEvent = DragAndDropItem.OnItemDragEndEvent;
			if (onItemDragEndEvent != null)
			{
				onItemDragEndEvent(this, eventData);
			}
			DragAndDropItem.draggedItem = null;
			DragAndDropItem.draggedItemContentCopy = null;
			DragAndDropItem.sourceCell = null;
		}
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000095 RID: 149 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
	// (remove) Token: 0x06000096 RID: 150 RVA: 0x0005EEDC File Offset: 0x0005D0DC
	public static event DragAndDropItem.DragEvent OnItemDragStartEvent;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000097 RID: 151 RVA: 0x0005EF10 File Offset: 0x0005D110
	// (remove) Token: 0x06000098 RID: 152 RVA: 0x0005EF44 File Offset: 0x0005D144
	public static event DragAndDropItem.DragEvent OnItemDragEndEvent;

	// Token: 0x06000099 RID: 153 RVA: 0x0005EF78 File Offset: 0x0005D178
	private GameObject GetCopy(GameObject draggedItemContent)
	{
		GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(draggedItemContent);
		gameObject.layer = base.gameObject.layer;
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.anchorMax = new Vector2(0.5f, 0.5f);
		component.anchorMin = new Vector2(0.5f, 0.5f);
		component.pivot = new Vector2(0.5f, 0.5f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = draggedItemContent.GetComponent<RectTransform>().rect.size;
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00005A3D File Offset: 0x00003C3D
	public void MakeVisible(bool condition)
	{
		this.itemContent.GetComponent<CanvasGroup>().alpha = condition > false;
		this.itemContent.transform.GetChild(1).gameObject.SetActive(condition);
	}

	// Token: 0x04000057 RID: 87
	public static DragAndDropItem draggedItem;

	// Token: 0x04000058 RID: 88
	public static GameObject draggedItemContentCopy;

	// Token: 0x04000059 RID: 89
	public static DragAndDropCell sourceCell;

	// Token: 0x0400005A RID: 90
	private GameObject itemContent;

	// Token: 0x0200001F RID: 31
	// (Invoke) Token: 0x0600009D RID: 157
	[NullableContext(0)]
	public delegate void DragEvent(DragAndDropItem item, PointerEventData eventData);
}
