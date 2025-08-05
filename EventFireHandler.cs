using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A0 RID: 10400
	public class EventFireHandler : Handler
	{
		// Token: 0x06008BB0 RID: 35760 RVA: 0x00053B0B File Offset: 0x00051D0B
		[NullableContext(1)]
		public EventFireHandler(Type eventType, MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
			: base(eventType, method, methodHandle, handlerArgumentsDescription)
		{
		}
	}
}
