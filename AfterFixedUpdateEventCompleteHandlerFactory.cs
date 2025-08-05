using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002845 RID: 10309
	public class AfterFixedUpdateEventCompleteHandlerFactory : BroadcastEventHandlerFactory
	{
		// Token: 0x060089DE RID: 35294 RVA: 0x000527AF File Offset: 0x000509AF
		public AfterFixedUpdateEventCompleteHandlerFactory()
			: base(typeof(OnEventComplete), typeof(AfterFixedUpdateEvent))
		{
		}

		// Token: 0x060089DF RID: 35295 RVA: 0x000527CB File Offset: 0x000509CB
		[NullableContext(1)]
		protected override Handler CreateHandlerInstance(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
		{
			return new AfterFixedUpdateEventCompleteHandler(method, methodHandle, handlerArgumentsDescription);
		}
	}
}
