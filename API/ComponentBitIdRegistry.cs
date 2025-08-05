using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200294B RID: 10571
	[NullableContext(1)]
	public interface ComponentBitIdRegistry
	{
		// Token: 0x06008E0B RID: 36363
		int GetComponentBitId(Type componentClass);
	}
}
