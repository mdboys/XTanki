using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002847 RID: 10311
	public class AfterFixedUpdateEventFireHandlerFactory : BroadcastEventHandlerFactory
	{
		// Token: 0x060089E1 RID: 35297 RVA: 0x000527EA File Offset: 0x000509EA
		public AfterFixedUpdateEventFireHandlerFactory()
			: base(typeof(OnEventFire), typeof(AfterFixedUpdateEvent))
		{
		}

		// Token: 0x060089E2 RID: 35298 RVA: 0x00052806 File Offset: 0x00050A06
		[NullableContext(1)]
		protected override Handler CreateHandlerInstance(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
		{
			return new AfterFixedUpdateEventFireHandler(method, methodHandle, handlerArgumentsDescription);
		}
	}
}
