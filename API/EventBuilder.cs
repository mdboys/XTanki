using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002965 RID: 10597
	[NullableContext(1)]
	public interface EventBuilder
	{
		// Token: 0x06008E7D RID: 36477
		EventBuilder Attach(Entity entity);

		// Token: 0x06008E7E RID: 36478
		EventBuilder Attach(Node node);

		// Token: 0x06008E7F RID: 36479
		EventBuilder Attach<[Nullable(0)] T>(ICollection<T> nodes) where T : Node;

		// Token: 0x06008E80 RID: 36480
		EventBuilder AttachAll(ICollection<Entity> entities);

		// Token: 0x06008E81 RID: 36481
		EventBuilder AttachAll(params Entity[] entities);

		// Token: 0x06008E82 RID: 36482
		EventBuilder AttachAll(params Node[] nodes);

		// Token: 0x06008E83 RID: 36483
		void Schedule();

		// Token: 0x06008E84 RID: 36484
		ScheduledEvent ScheduleDelayed(float timeInSec);

		// Token: 0x06008E85 RID: 36485
		ScheduledEvent SchedulePeriodic(float timeInSec);
	}
}
