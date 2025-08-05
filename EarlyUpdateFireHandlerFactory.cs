using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200287D RID: 10365
	public class EarlyUpdateFireHandlerFactory : BroadcastEventHandlerFactory
	{
		// Token: 0x06008A7E RID: 35454 RVA: 0x00052F00 File Offset: 0x00051100
		public EarlyUpdateFireHandlerFactory()
			: base(typeof(OnEventFire), typeof(EarlyUpdateEvent))
		{
		}

		// Token: 0x06008A7F RID: 35455 RVA: 0x00052F1C File Offset: 0x0005111C
		[NullableContext(1)]
		protected override Handler CreateHandlerInstance(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
		{
			return new EarlyUpdateFireHandler(method, methodHandle, handlerArgumentsDescription);
		}
	}
}
