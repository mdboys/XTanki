using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200289A RID: 10394
	[NullableContext(1)]
	public interface EntityUnsafe
	{
		// Token: 0x06008B9B RID: 35739
		[return: Nullable(2)]
		Component GetComponentUnsafe(Type componentType);
	}
}
