using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002856 RID: 10326
	[NullableContext(1)]
	[Nullable(0)]
	public class BroadcastEventHandlerCollector : EntityListener
	{
		// Token: 0x06008A17 RID: 35351 RVA: 0x00052A36 File Offset: 0x00050C36
		public BroadcastEventHandlerCollector(HandlerCollector handlerCollector)
		{
		}

		// Token: 0x06008A18 RID: 35352 RVA: 0x00131208 File Offset: 0x0012F408
		public void OnNodeAdded(Entity entity, NodeDescription node)
		{
			foreach (KeyValuePair<Type, BroadcastInvokeDataStorage> keyValuePair in this._handlersByType)
			{
				ICollection<Handler> handlers = this.<handlerCollector>P.GetHandlers(keyValuePair.Key, node);
				keyValuePair.Value.Add(entity, handlers);
			}
		}

		// Token: 0x06008A19 RID: 35353 RVA: 0x00131278 File Offset: 0x0012F478
		public void OnNodeRemoved(Entity entity, NodeDescription node)
		{
			foreach (KeyValuePair<Type, BroadcastInvokeDataStorage> keyValuePair in this._handlersByType)
			{
				ICollection<Handler> handlers = this.<handlerCollector>P.GetHandlers(keyValuePair.Key, node);
				keyValuePair.Value.Remove(entity, handlers);
			}
		}

		// Token: 0x06008A1A RID: 35354 RVA: 0x001312E8 File Offset: 0x0012F4E8
		public void OnEntityDeleted(Entity entity)
		{
			foreach (BroadcastInvokeDataStorage broadcastInvokeDataStorage in this._handlersByType.Values)
			{
				broadcastInvokeDataStorage.Remove(entity);
			}
		}

		// Token: 0x06008A1B RID: 35355 RVA: 0x00052A50 File Offset: 0x00050C50
		public void Register(Type handlerType)
		{
			this._handlersByType[handlerType] = new BroadcastInvokeDataStorage();
		}

		// Token: 0x06008A1C RID: 35356 RVA: 0x00052A63 File Offset: 0x00050C63
		public IList<HandlerBroadcastInvokeData> GetHandlers(Type handlerType)
		{
			return this._handlersByType[handlerType].ContextInvokeDatas;
		}

		// Token: 0x04005E5D RID: 24157
		[CompilerGenerated]
		private HandlerCollector <handlerCollector>P = handlerCollector;

		// Token: 0x04005E5E RID: 24158
		private readonly Dictionary<Type, BroadcastInvokeDataStorage> _handlersByType = new Dictionary<Type, BroadcastInvokeDataStorage>();
	}
}
