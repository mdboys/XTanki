using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002844 RID: 10308
	public class AfterFixedUpdateEventCompleteHandler : EventCompleteHandler
	{
		// Token: 0x060089DD RID: 35293 RVA: 0x0005279A File Offset: 0x0005099A
		[NullableContext(1)]
		public AfterFixedUpdateEventCompleteHandler(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
			: base(typeof(AfterFixedUpdateEvent), method, methodHandle, handlerArgumentsDescription)
		{
		}
	}
}
