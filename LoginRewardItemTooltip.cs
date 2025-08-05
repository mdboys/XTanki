using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// Token: 0x0200003C RID: 60
[NullableContext(1)]
[Nullable(0)]
[ExecuteInEditMode]
public class LoginRewardItemTooltip : MonoBehaviour
{
	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005C8E File Offset: 0x00003E8E
	// (set) Token: 0x060000E6 RID: 230 RVA: 0x00005CA0 File Offset: 0x00003EA0
	public string Text
	{
		get
		{
			return this.text.GetComponent<TextMeshProUGUI>().text;
		}
		set
		{
			this.text.GetComponent<TextMeshProUGUI>().text = value;
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00005CB3 File Offset: 0x00003EB3
	private void Update()
	{
		this.back.sizeDelta = new Vector2(270f, this.text.sizeDelta.y + 30f);
	}

	// Token: 0x04000089 RID: 137
	[SerializeField]
	private RectTransform text;

	// Token: 0x0400008A RID: 138
	[SerializeField]
	private RectTransform back;
}
