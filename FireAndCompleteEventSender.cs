using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A5 RID: 10405
	[NullableContext(1)]
	[Nullable(0)]
	public class FireAndCompleteEventSender : EventSender
	{
		// Token: 0x06008BB9 RID: 35769 RVA: 0x00053BDE File Offset: 0x00051DDE
		internal FireAndCompleteEventSender(ICollection<Handler> fireHandlers, ICollection<Handler> completeHandlers)
		{
			this.fireHandlers = fireHandlers;
			this.completeHandlers = completeHandlers;
		}

		// Token: 0x06008BBA RID: 35770 RVA: 0x00053BF4 File Offset: 0x00051DF4
		public void Send(Flow flow, Event e, ICollection<Entity> entities)
		{
			flow.TryInvoke(this.fireHandlers, e, entities);
			flow.TryInvoke(this.completeHandlers, e, entities);
		}

		// Token: 0x04005ED2 RID: 24274
		private readonly ICollection<Handler> completeHandlers;

		// Token: 0x04005ED3 RID: 24275
		private readonly ICollection<Handler> fireHandlers;
	}
}
