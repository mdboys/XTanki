using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002962 RID: 10594
	public class EntityStateNotRegisteredException : Exception
	{
		// Token: 0x06008E77 RID: 36471 RVA: 0x00055801 File Offset: 0x00053A01
		[NullableContext(1)]
		public EntityStateNotRegisteredException(Type type)
			: base(string.Format("State {0} is not registered", type))
		{
		}
	}
}
