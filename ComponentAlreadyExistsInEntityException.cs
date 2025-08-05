using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002861 RID: 10337
	public class ComponentAlreadyExistsInEntityException : Exception
	{
		// Token: 0x06008A33 RID: 35379 RVA: 0x00052B8C File Offset: 0x00050D8C
		[NullableContext(1)]
		public ComponentAlreadyExistsInEntityException(EntityInternal entity, Type componentClass)
			: base(string.Format("{0} entity={1}", componentClass.Name, entity))
		{
		}
	}
}
