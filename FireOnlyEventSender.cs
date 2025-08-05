using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A6 RID: 10406
	[NullableContext(1)]
	[Nullable(0)]
	public class FireOnlyEventSender : EventSender
	{
		// Token: 0x06008BBB RID: 35771 RVA: 0x00053C12 File Offset: 0x00051E12
		public FireOnlyEventSender(ICollection<Handler> fireHandlers)
		{
		}

		// Token: 0x06008BBC RID: 35772 RVA: 0x00053C21 File Offset: 0x00051E21
		public void Send(Flow flow, Event e, ICollection<Entity> entities)
		{
			flow.TryInvoke(this.<fireHandlers>P, e, entities);
		}

		// Token: 0x04005ED4 RID: 24276
		[CompilerGenerated]
		private ICollection<Handler> <fireHandlers>P = fireHandlers;
	}
}
