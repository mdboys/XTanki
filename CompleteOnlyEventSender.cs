using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002860 RID: 10336
	[NullableContext(1)]
	[Nullable(0)]
	public class CompleteOnlyEventSender : EventSender
	{
		// Token: 0x06008A31 RID: 35377 RVA: 0x00052B6D File Offset: 0x00050D6D
		internal CompleteOnlyEventSender(ICollection<Handler> completeHandlers)
		{
			this.completeHandlers = completeHandlers;
		}

		// Token: 0x06008A32 RID: 35378 RVA: 0x00052B7C File Offset: 0x00050D7C
		public void Send(Flow flow, Event e, ICollection<Entity> entities)
		{
			flow.TryInvoke(this.completeHandlers, e, entities);
		}

		// Token: 0x04005E64 RID: 24164
		private readonly ICollection<Handler> completeHandlers;
	}
}
