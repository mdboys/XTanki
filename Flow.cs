using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028AB RID: 10411
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Flow
	{
		// Token: 0x06008BC3 RID: 35779 RVA: 0x00133778 File Offset: 0x00131978
		public Flow(EngineServiceInternal engineService)
		{
			Flow.Current = this;
			this.NodeCollector = engineService.NodeCollector;
			this.EntityRegistry = engineService.EntityRegistry;
			this._eventMaker = engineService.EventMaker;
			this._engineService = engineService;
			this._nodeChangedHandlerResolver = new NodeChangedHandlerResolver();
			this._broadcastHandlerResolver = new BroadcastHandlerResolver(engineService.BroadcastEventHandlerCollector);
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06008BC4 RID: 35780 RVA: 0x00053CA7 File Offset: 0x00051EA7
		// (set) Token: 0x06008BC5 RID: 35781 RVA: 0x00053CAE File Offset: 0x00051EAE
		public static Flow Current { get; private set; }

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06008BC6 RID: 35782 RVA: 0x00053CB6 File Offset: 0x00051EB6
		public NodeCollectorImpl NodeCollector { get; }

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06008BC7 RID: 35783 RVA: 0x00053CBE File Offset: 0x00051EBE
		public EntityRegistry EntityRegistry { get; }

		// Token: 0x06008BC8 RID: 35784 RVA: 0x001337D8 File Offset: 0x001319D8
		public void TryInvoke(ICollection<Handler> handlers, Event eventInstance, ICollection<Entity> contextEntities)
		{
			foreach (HandlerInvokeData handlerInvokeData in HandlerResolver.Resolve(handlers, eventInstance, contextEntities))
			{
				handlerInvokeData.Invoke();
			}
		}

		// Token: 0x06008BC9 RID: 35785 RVA: 0x00133824 File Offset: 0x00131A24
		public void TryInvoke(ICollection<Handler> fireHandlers, ICollection<Handler> completeHandlers, Event eventInstance, Entity entity, ICollection<NodeDescription> changedNodes)
		{
			IEnumerable<HandlerInvokeData> enumerable = this._nodeChangedHandlerResolver.Resolve(fireHandlers, eventInstance, entity, changedNodes);
			IList<HandlerInvokeData> list = this._nodeChangedHandlerResolver.Resolve(completeHandlers, eventInstance, entity, changedNodes);
			foreach (HandlerInvokeData handlerInvokeData in enumerable)
			{
				handlerInvokeData.Invoke();
			}
			foreach (HandlerInvokeData handlerInvokeData2 in list)
			{
				handlerInvokeData2.Invoke();
			}
		}

		// Token: 0x06008BCA RID: 35786 RVA: 0x001338C0 File Offset: 0x00131AC0
		public void TryInvoke(Event eventInstance, Type handlerType)
		{
			foreach (HandlerInvokeData handlerInvokeData in this._broadcastHandlerResolver.Resolve(eventInstance, handlerType))
			{
				handlerInvokeData.Invoke();
			}
		}

		// Token: 0x06008BCB RID: 35787 RVA: 0x00053CC6 File Offset: 0x00051EC6
		public void SendEvent(Event e, Entity entity)
		{
			this.SendEvent(e, Collections.SingletonList<Entity>(entity));
		}

		// Token: 0x06008BCC RID: 35788 RVA: 0x00053CD5 File Offset: 0x00051ED5
		public void SendEvent(Event e, ICollection<Entity> entities)
		{
			this.NotifySendEvent(e, entities);
			this.SendEventSilent(e, entities);
		}

		// Token: 0x06008BCD RID: 35789 RVA: 0x00133914 File Offset: 0x00131B14
		private void NotifySendEvent(Event e, ICollection<Entity> entities)
		{
			this._engineService.EventListeners.ForEach(delegate(EventListener listener)
			{
				listener.OnEventSend(e, entities);
			});
		}

		// Token: 0x06008BCE RID: 35790 RVA: 0x00053CE7 File Offset: 0x00051EE7
		public void SendEventSilent(Event e, ICollection<Entity> entities)
		{
			this._eventMaker.Send(this, e, entities);
		}

		// Token: 0x06008BCF RID: 35791 RVA: 0x00053CF7 File Offset: 0x00051EF7
		public void Finish()
		{
			this._engineService.FlowListeners.ForEach(delegate(FlowListener listener)
			{
				listener.OnFlowFinish();
			});
		}

		// Token: 0x06008BD0 RID: 35792 RVA: 0x00053D28 File Offset: 0x00051F28
		public void Clean()
		{
			this._engineService.FlowListeners.ForEach(delegate(FlowListener listener)
			{
				listener.OnFlowClean();
			});
		}

		// Token: 0x04005ED5 RID: 24277
		private readonly EngineServiceInternal _engineService;

		// Token: 0x04005ED6 RID: 24278
		private readonly BroadcastHandlerResolver _broadcastHandlerResolver;

		// Token: 0x04005ED7 RID: 24279
		private readonly EventMaker _eventMaker;

		// Token: 0x04005ED8 RID: 24280
		private readonly NodeChangedHandlerResolver _nodeChangedHandlerResolver;
	}
}
