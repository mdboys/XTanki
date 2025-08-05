using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002859 RID: 10329
	[NullableContext(1)]
	[Nullable(0)]
	public class BroadcastInvokeDataStorage
	{
		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06008A22 RID: 35362 RVA: 0x00052AD2 File Offset: 0x00050CD2
		public IList<HandlerBroadcastInvokeData> ContextInvokeDatas
		{
			get
			{
				return this._datas;
			}
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x00131340 File Offset: 0x0012F540
		public void Add(Entity entity, ICollection<Handler> handlers)
		{
			if (handlers.Count == 0)
			{
				return;
			}
			foreach (Handler handler in handlers)
			{
				HandlerBroadcastInvokeData handlerBroadcastInvokeData = new HandlerBroadcastInvokeData(handler, entity);
				this._datas.Add(handlerBroadcastInvokeData);
			}
		}

		// Token: 0x06008A24 RID: 35364 RVA: 0x0013139C File Offset: 0x0012F59C
		public void Remove(Entity entity, ICollection<Handler> handlers)
		{
			if (handlers.Count == 0)
			{
				return;
			}
			foreach (Handler handler in handlers)
			{
				for (int i = this._datas.Count - 1; i >= 0; i--)
				{
					HandlerBroadcastInvokeData handlerBroadcastInvokeData = this._datas[i];
					if (handlerBroadcastInvokeData.Handler == handler && handlerBroadcastInvokeData.Entity.Equals(entity))
					{
						this._datas.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06008A25 RID: 35365 RVA: 0x00131430 File Offset: 0x0012F630
		public void Remove(Entity entity)
		{
			for (int i = this._datas.Count - 1; i >= 0; i--)
			{
				if (this._datas[i].Entity.Equals(entity))
				{
					this._datas.RemoveAt(i);
				}
			}
		}

		// Token: 0x04005E61 RID: 24161
		private readonly List<HandlerBroadcastInvokeData> _datas = new List<HandlerBroadcastInvokeData>(200);
	}
}
