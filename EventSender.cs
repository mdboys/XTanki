using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A4 RID: 10404
	[NullableContext(1)]
	public interface EventSender
	{
		// Token: 0x06008BB8 RID: 35768
		void Send(Flow flow, Event e, ICollection<Entity> entities);
	}
}
