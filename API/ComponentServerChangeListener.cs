using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002951 RID: 10577
	[NullableContext(1)]
	public interface ComponentServerChangeListener
	{
		// Token: 0x06008E17 RID: 36375
		void ChangedOnServer(Entity entity);
	}
}
