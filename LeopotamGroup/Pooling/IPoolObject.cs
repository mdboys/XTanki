using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LeopotamGroup.Pooling
{
	// Token: 0x02002AE0 RID: 10976
	[NullableContext(1)]
	public interface IPoolObject
	{
		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06009730 RID: 38704
		// (set) Token: 0x06009731 RID: 38705
		PoolContainer PoolContainer { get; set; }

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06009732 RID: 38706
		Transform PoolTransform { get; }

		// Token: 0x06009733 RID: 38707
		void PoolRecycle(bool checkForDoubleRecycle = true);
	}
}
