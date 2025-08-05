using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002894 RID: 10388
	[NullableContext(1)]
	public interface EntityRegistry
	{
		// Token: 0x06008B53 RID: 35667
		void Remove(long id);

		// Token: 0x06008B54 RID: 35668
		bool ContainsEntity(long id);

		// Token: 0x06008B55 RID: 35669
		void RegisterEntity(Entity entity);

		// Token: 0x06008B56 RID: 35670
		Entity GetEntity(long id);

		// Token: 0x06008B57 RID: 35671
		ICollection<Entity> GetAllEntities();
	}
}
