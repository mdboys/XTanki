using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200288A RID: 10378
	public class EntityAlreadyUnsharedException : Exception
	{
		// Token: 0x06008AE5 RID: 35557 RVA: 0x0005348A File Offset: 0x0005168A
		[NullableContext(1)]
		public EntityAlreadyUnsharedException(Entity entity)
			: base(string.Format("entity={0}", entity))
		{
		}
	}
}
