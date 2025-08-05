using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[Serializable]
public struct P3D_GroupMask
{
	// Token: 0x0600014A RID: 330 RVA: 0x000060D7 File Offset: 0x000042D7
	public P3D_GroupMask(int newMask)
	{
		this.mask = newMask;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000060E0 File Offset: 0x000042E0
	public static implicit operator int(P3D_GroupMask groupMask)
	{
		return groupMask.mask;
	}

	// Token: 0x0600014C RID: 332 RVA: 0x000060E8 File Offset: 0x000042E8
	public static implicit operator P3D_GroupMask(int mask)
	{
		return new P3D_GroupMask(mask);
	}

	// Token: 0x040000FB RID: 251
	[SerializeField]
	private int mask;
}
