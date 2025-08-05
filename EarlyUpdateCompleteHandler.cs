using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200287A RID: 10362
	public class EarlyUpdateCompleteHandler : EventCompleteHandler
	{
		// Token: 0x06008A7A RID: 35450 RVA: 0x00052EB0 File Offset: 0x000510B0
		[NullableContext(1)]
		public EarlyUpdateCompleteHandler(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
			: base(typeof(EarlyUpdateEvent), method, methodHandle, handlerArgumentsDescription)
		{
		}
	}
}
