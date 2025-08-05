using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
[Serializable]
public struct P3D_Group
{
	// Token: 0x06000147 RID: 327 RVA: 0x000060A4 File Offset: 0x000042A4
	public P3D_Group(int newIndex)
	{
		if (newIndex <= 0)
		{
			this.index = 0;
			return;
		}
		if (newIndex >= 31)
		{
			this.index = 31;
			return;
		}
		this.index = newIndex;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000060C7 File Offset: 0x000042C7
	public static implicit operator int(P3D_Group group)
	{
		return group.index;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000060CF File Offset: 0x000042CF
	public static implicit operator P3D_Group(int index)
	{
		return new P3D_Group(index);
	}

	// Token: 0x040000FA RID: 250
	[SerializeField]
	private int index;
}
