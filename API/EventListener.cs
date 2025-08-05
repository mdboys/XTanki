using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002966 RID: 10598
	[NullableContext(1)]
	public interface EventListener
	{
		// Token: 0x06008E86 RID: 36486
		void OnEventSend(Event evt, ICollection<Entity> entities);
	}
}
