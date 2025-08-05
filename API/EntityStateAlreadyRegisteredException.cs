using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002960 RID: 10592
	public class EntityStateAlreadyRegisteredException : Exception
	{
		// Token: 0x06008E72 RID: 36466 RVA: 0x00055801 File Offset: 0x00053A01
		[NullableContext(1)]
		public EntityStateAlreadyRegisteredException(Type stateType)
			: base(string.Format("State {0} is not registered", stateType))
		{
		}
	}
}
