using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000062 RID: 98
[NullableContext(1)]
[Nullable(0)]
public class PostEffectsSet : MonoBehaviour
{
	// Token: 0x060001E6 RID: 486 RVA: 0x00064C3C File Offset: 0x00062E3C
	public void SetActive(bool value)
	{
		if (this.effects != null)
		{
			for (int i = 0; i < this.effects.Length; i++)
			{
				this.effects[i].enabled = value;
			}
		}
		base.enabled = value;
	}

	// Token: 0x0400014C RID: 332
	public string qualityName;

	// Token: 0x0400014D RID: 333
	public DepthTextureMode depthTextureMode;

	// Token: 0x0400014E RID: 334
	[SerializeField]
	private MonoBehaviour[] effects;
}
