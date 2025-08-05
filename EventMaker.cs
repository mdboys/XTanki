using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A2 RID: 10402
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EventMaker
	{
		// Token: 0x06008BB3 RID: 35763 RVA: 0x00053B88 File Offset: 0x00051D88
		public EventMaker(HandlerCollector handlerCollector)
		{
		}

		// Token: 0x06008BB4 RID: 35764 RVA: 0x00053BA2 File Offset: 0x00051DA2
		public void Send(Flow flow, Event e, ICollection<Entity> entities)
		{
			this.GetSender(e.GetType()).Send(flow, e, entities);
		}

		// Token: 0x06008BB5 RID: 35765 RVA: 0x00053BB8 File Offset: 0x00051DB8
		private EventSender GetSender(Type eventType)
		{
			return this._senderByEventType.ComputeIfAbsent(eventType, new Func<Type, EventSender>(this.CreateSender));
		}

		// Token: 0x06008BB6 RID: 35766 RVA: 0x001336F4 File Offset: 0x001318F4
		private EventSender CreateSender(Type eventType)
		{
			ICollection<Handler> handlers = this.<handlerCollector>P.GetHandlers(typeof(EventFireHandler), eventType);
			ICollection<Handler> handlers2 = this.<handlerCollector>P.GetHandlers(typeof(EventCompleteHandler), eventType);
			bool flag = handlers.Count > 0;
			bool flag2 = handlers2.Count > 0;
			if (flag && flag2)
			{
				return new FireAndCompleteEventSender(handlers, handlers2);
			}
			if (flag)
			{
				return new FireOnlyEventSender(handlers);
			}
			if (!flag2)
			{
				return EventMaker.Stub;
			}
			return new CompleteOnlyEventSender(handlers2);
		}

		// Token: 0x04005ECC RID: 24268
		[CompilerGenerated]
		private HandlerCollector <handlerCollector>P = handlerCollector;

		// Token: 0x04005ECD RID: 24269
		private static readonly EventSender Stub = new StubEvenSender();

		// Token: 0x04005ECE RID: 24270
		private readonly Dictionary<Type, EventSender> _senderByEventType = new Dictionary<Type, EventSender>();
	}
}
