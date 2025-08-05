using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LeopotamGroup.Pooling
{
	// Token: 0x02002AE2 RID: 10978
	[NullableContext(1)]
	[Nullable(0)]
	public class PoolObject : MonoBehaviour, IPoolObject
	{
		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x0600973F RID: 38719 RVA: 0x0005A59B File Offset: 0x0005879B
		// (set) Token: 0x06009740 RID: 38720 RVA: 0x0005A5A3 File Offset: 0x000587A3
		public virtual PoolContainer PoolContainer
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06009741 RID: 38721 RVA: 0x0005A5AC File Offset: 0x000587AC
		public virtual Transform PoolTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x06009742 RID: 38722 RVA: 0x0005A5B4 File Offset: 0x000587B4
		public virtual void PoolRecycle(bool checkDoubleRecycles = true)
		{
			if (this._container != null)
			{
				this._container.Recycle(this, checkDoubleRecycles);
			}
		}

		// Token: 0x04006387 RID: 25479
		private PoolContainer _container;
	}
}
