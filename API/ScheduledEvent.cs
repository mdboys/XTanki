using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200297E RID: 10622
	[NullableContext(1)]
	public interface ScheduledEvent
	{
		// Token: 0x06008ECF RID: 36559
		ScheduleManager Manager();
	}
}
