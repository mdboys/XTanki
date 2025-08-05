using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002869 RID: 10345
	public class ComponentNotFoundException : Exception
	{
		// Token: 0x06008A46 RID: 35398 RVA: 0x00052B8C File Offset: 0x00050D8C
		[NullableContext(1)]
		public ComponentNotFoundException(Entity entity, Type componentClass)
			: base(string.Format("{0} entity={1}", componentClass.Name, entity))
		{
		}
	}
}
