using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tanks.Lobby.ClientControls.API;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000039 RID: 57
[NullableContext(1)]
[Nullable(0)]
public class KeyboardNavigation : MonoBehaviour
{
	// Token: 0x060000D6 RID: 214 RVA: 0x0005F774 File Offset: 0x0005D974
	private void Update()
	{
		Selectable selectable = null;
		if (EventSystem.current == null)
		{
			return;
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		Selectable component = currentSelectedGameObject.GetComponent<Selectable>();
		if (component == null)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			this.traversed.Clear();
			selectable = ((!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) ? this.FindDown(component) : this.FindUp(component));
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			this.traversed.Clear();
			if (component is InputField && !this.HasCustomNavigation(component))
			{
				selectable = this.FindDown(component);
			}
		}
		if (selectable != null)
		{
			selectable.Select();
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0005F834 File Offset: 0x0005DA34
	private bool HasCustomNavigation(Selectable current)
	{
		InputFieldReturnSelector component = current.gameObject.GetComponent<InputFieldReturnSelector>();
		return component != null && component.CanNavigateToSelectable();
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0005F860 File Offset: 0x0005DA60
	private Selectable FindUp(Selectable current)
	{
		if (this.traversed.Contains(current))
		{
			return null;
		}
		this.traversed.Add(current);
		Selectable selectable = current.FindSelectableOnUp();
		if (this.IsValidSelectable(selectable))
		{
			return selectable;
		}
		return this.FindUp(selectable);
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0005F8A4 File Offset: 0x0005DAA4
	private Selectable FindDown(Selectable current)
	{
		if (this.traversed.Contains(current))
		{
			return null;
		}
		this.traversed.Add(current);
		Selectable selectable = current.FindSelectableOnDown();
		if (this.IsValidSelectable(selectable))
		{
			return selectable;
		}
		return this.FindDown(selectable);
	}

	// Token: 0x060000DA RID: 218 RVA: 0x0005F8E8 File Offset: 0x0005DAE8
	private bool IsValidSelectable(Selectable selectable)
	{
		if (!(selectable == null))
		{
			if (selectable != null && selectable.interactable)
			{
				GameObject gameObject = selectable.gameObject;
				if (gameObject != null)
				{
					return gameObject.activeSelf;
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x04000081 RID: 129
	private readonly HashSet<Selectable> traversed = new HashSet<Selectable>();
}
