using System;
using System.Runtime.CompilerServices;
using Tanks.Lobby.ClientGarage.Impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200001D RID: 29
[NullableContext(1)]
[Nullable(0)]
[RequireComponent(typeof(Image))]
public class DragAndDropCell : MonoBehaviour, IDropHandler, IEventSystemHandler
{
	// Token: 0x0600008B RID: 139 RVA: 0x0005ECB4 File Offset: 0x0005CEB4
	public void OnDrop(PointerEventData data)
	{
		if (DragAndDropItem.draggedItemContentCopy != null && DragAndDropItem.draggedItemContentCopy.activeSelf)
		{
			DragAndDropItem draggedItem = DragAndDropItem.draggedItem;
			DragAndDropCell sourceCell = DragAndDropItem.sourceCell;
			this.dropController.OnDrop(sourceCell, this, draggedItem);
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0005ECF4 File Offset: 0x0005CEF4
	public void RemoveItem()
	{
		DragAndDropItem[] componentsInChildren = base.GetComponentsInChildren<DragAndDropItem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			global::UnityEngine.Object.Destroy(componentsInChildren[i].gameObject);
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000059DD File Offset: 0x00003BDD
	public void PlaceItem(DragAndDropItem item)
	{
		if (item != null)
		{
			item.transform.SetParent(base.transform, false);
			item.transform.localPosition = Vector3.zero;
		}
		base.gameObject.SendMessageUpwards("OnItemPlace", item, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00005A1C File Offset: 0x00003C1C
	public DragAndDropItem GetItem()
	{
		return base.GetComponentInChildren<DragAndDropItem>();
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0005ED24 File Offset: 0x0005CF24
	public void SwapItems(DragAndDropCell sourceCell, DragAndDropItem item)
	{
		DragAndDropItem item2 = this.GetItem();
		this.PlaceItem(item);
		if (item2 != null)
		{
			sourceCell.PlaceItem(item2);
		}
	}

	// Token: 0x04000056 RID: 86
	public IDropController dropController;
}
