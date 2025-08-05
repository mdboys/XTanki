using System;
using UnityEngine;

namespace LeopotamGroup.Pooling
{
	// Token: 0x02002AE3 RID: 10979
	public sealed class RecycleAfterTime : MonoBehaviour
	{
		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06009744 RID: 38724 RVA: 0x0005A5CB File Offset: 0x000587CB
		// (set) Token: 0x06009745 RID: 38725 RVA: 0x0005A5D3 File Offset: 0x000587D3
		public float Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				this._timeout = value;
			}
		}

		// Token: 0x06009746 RID: 38726 RVA: 0x0005A5DC File Offset: 0x000587DC
		private void LateUpdate()
		{
			if (Time.time >= this._endTime)
			{
				this.OnRecycle();
			}
		}

		// Token: 0x06009747 RID: 38727 RVA: 0x0005A5F1 File Offset: 0x000587F1
		private void OnEnable()
		{
			this._endTime = Time.time + this._timeout;
		}

		// Token: 0x06009748 RID: 38728 RVA: 0x00148764 File Offset: 0x00146964
		private void OnRecycle()
		{
			IPoolObject component = base.GetComponent<IPoolObject>();
			if (component != null)
			{
				component.PoolRecycle(true);
				return;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x04006388 RID: 25480
		[SerializeField]
		private float _timeout = 1f;

		// Token: 0x04006389 RID: 25481
		private float _endTime;
	}
}
