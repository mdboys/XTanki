using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000015 RID: 21
[NullableContext(1)]
[Nullable(0)]
public class ChatSection : MonoBehaviour
{
	// Token: 0x0600004B RID: 75 RVA: 0x0005E540 File Offset: 0x0005C740
	public void SwitchHideState()
	{
		this.hiden = !this.hiden;
		this.hideIcon.transform.localScale = new Vector3(1f, (float)(this.hiden ? 1 : (-1)), 1f) * 0.25f;
		foreach (object obj in base.transform)
		{
			if (obj != this.header)
			{
				((Transform)obj).gameObject.SetActive(!this.hiden);
			}
		}
	}

	// Token: 0x04000025 RID: 37
	[SerializeField]
	private Transform header;

	// Token: 0x04000026 RID: 38
	[SerializeField]
	private Transform hideIcon;

	// Token: 0x04000027 RID: 39
	private bool hiden;
}
