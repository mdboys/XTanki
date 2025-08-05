using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002851 RID: 10321
	[NullableContext(1)]
	public interface AutoRemoveComponentsRegistry
	{
		// Token: 0x06008A07 RID: 35335
		bool IsComponentAutoRemoved(Type componentType);
	}
}
