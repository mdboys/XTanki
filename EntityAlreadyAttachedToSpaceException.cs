using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002887 RID: 10375
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityAlreadyAttachedToSpaceException : Exception
	{
		// Token: 0x06008AE1 RID: 35553 RVA: 0x0005348A File Offset: 0x0005168A
		public EntityAlreadyAttachedToSpaceException(Entity entity)
			: base(string.Format("entity={0}", entity))
		{
		}

		// Token: 0x06008AE2 RID: 35554 RVA: 0x0005349D File Offset: 0x0005169D
		public EntityAlreadyAttachedToSpaceException(EntityInternal entity, GroupComponent group)
			: base(string.Format("entity={0} exists in attached group={1}", entity, group))
		{
		}
	}
}
