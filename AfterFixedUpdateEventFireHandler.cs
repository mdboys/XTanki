using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002846 RID: 10310
	public class AfterFixedUpdateEventFireHandler : EventFireHandler
	{
		// Token: 0x060089E0 RID: 35296 RVA: 0x000527D5 File Offset: 0x000509D5
		[NullableContext(1)]
		public AfterFixedUpdateEventFireHandler(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
			: base(typeof(AfterFixedUpdateEvent), method, methodHandle, handlerArgumentsDescription)
		{
		}
	}
}
