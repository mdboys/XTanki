using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002946 RID: 10566
	[NullableContext(1)]
	public interface AttachToEntityListener
	{
		// Token: 0x06008E05 RID: 36357
		void AttachedToEntity(Entity entity);
	}
}
