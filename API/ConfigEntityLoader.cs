using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002953 RID: 10579
	[NullableContext(1)]
	public interface ConfigEntityLoader
	{
		// Token: 0x06008E1B RID: 36379
		void LoadEntities(Engine engine);
	}
}
