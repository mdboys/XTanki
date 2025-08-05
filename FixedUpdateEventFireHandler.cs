using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A9 RID: 10409
	public class FixedUpdateEventFireHandler : EventFireHandler
	{
		// Token: 0x06008BC0 RID: 35776 RVA: 0x00053C6C File Offset: 0x00051E6C
		[NullableContext(1)]
		public FixedUpdateEventFireHandler(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
			: base(typeof(FixedUpdateEvent), method, methodHandle, handlerArgumentsDescription)
		{
		}
	}
}
