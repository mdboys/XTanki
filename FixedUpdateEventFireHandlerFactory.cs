using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028AA RID: 10410
	public class FixedUpdateEventFireHandlerFactory : BroadcastEventHandlerFactory
	{
		// Token: 0x06008BC1 RID: 35777 RVA: 0x00053C81 File Offset: 0x00051E81
		public FixedUpdateEventFireHandlerFactory()
			: base(typeof(OnEventFire), typeof(FixedUpdateEvent))
		{
		}

		// Token: 0x06008BC2 RID: 35778 RVA: 0x00053C9D File Offset: 0x00051E9D
		[NullableContext(1)]
		protected override Handler CreateHandlerInstance(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
		{
			return new FixedUpdateEventFireHandler(method, methodHandle, handlerArgumentsDescription);
		}
	}
}
