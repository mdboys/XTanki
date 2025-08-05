using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A7 RID: 10407
	public class FixedUpdateEventCompleteHandler : EventCompleteHandler
	{
		// Token: 0x06008BBD RID: 35773 RVA: 0x00053C31 File Offset: 0x00051E31
		[NullableContext(1)]
		public FixedUpdateEventCompleteHandler(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
			: base(typeof(FixedUpdateEvent), method, methodHandle, handlerArgumentsDescription)
		{
		}
	}
}
