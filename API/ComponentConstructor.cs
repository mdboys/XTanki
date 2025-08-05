using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200294C RID: 10572
	[NullableContext(1)]
	public interface ComponentConstructor
	{
		// Token: 0x06008E0C RID: 36364
		bool IsAcceptable(Type componentType, EntityInternal entity);

		// Token: 0x06008E0D RID: 36365
		Component GetComponentInstance(Type componentType, EntityInternal entity);
	}
}
