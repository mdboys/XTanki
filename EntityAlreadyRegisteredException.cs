using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002888 RID: 10376
	public class EntityAlreadyRegisteredException : Exception
	{
		// Token: 0x06008AE3 RID: 35555 RVA: 0x0005348A File Offset: 0x0005168A
		[NullableContext(1)]
		public EntityAlreadyRegisteredException(Entity newEntity)
			: base(string.Format("entity={0}", newEntity))
		{
		}
	}
}
