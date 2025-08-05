using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002950 RID: 10576
	[NullableContext(1)]
	public interface ComponentListener
	{
		// Token: 0x06008E14 RID: 36372
		void OnComponentAdded(Entity entity, Component component);

		// Token: 0x06008E15 RID: 36373
		void OnComponentRemoved(Entity entity, Component component);

		// Token: 0x06008E16 RID: 36374
		void OnComponentChanged(Entity entity, Component component);
	}
}
