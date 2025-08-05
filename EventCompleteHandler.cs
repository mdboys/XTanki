using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200289E RID: 10398
	public class EventCompleteHandler : Handler
	{
		// Token: 0x06008BAD RID: 35757 RVA: 0x00053B0B File Offset: 0x00051D0B
		[NullableContext(1)]
		public EventCompleteHandler(Type eventType, MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
			: base(eventType, method, methodHandle, handlerArgumentsDescription)
		{
		}
	}
}
