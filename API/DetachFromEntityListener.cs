using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002956 RID: 10582
	[NullableContext(1)]
	public interface DetachFromEntityListener
	{
		// Token: 0x06008E1E RID: 36382
		void DetachedFromEntity(Entity entity);
	}
}
