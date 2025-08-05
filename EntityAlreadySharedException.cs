using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002889 RID: 10377
	public class EntityAlreadySharedException : Exception
	{
		// Token: 0x06008AE4 RID: 35556 RVA: 0x0005348A File Offset: 0x0005168A
		[NullableContext(1)]
		public EntityAlreadySharedException(Entity entity)
			: base(string.Format("entity={0}", entity))
		{
		}
	}
}
