using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002890 RID: 10384
	[NullableContext(1)]
	public interface EntityListener
	{
		// Token: 0x06008B49 RID: 35657
		void OnNodeAdded(Entity entity, NodeDescription nodeDescription);

		// Token: 0x06008B4A RID: 35658
		void OnNodeRemoved(Entity entity, NodeDescription nodeDescription);

		// Token: 0x06008B4B RID: 35659
		void OnEntityDeleted(Entity entity);
	}
}
