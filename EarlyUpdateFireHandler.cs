using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200287C RID: 10364
	public class EarlyUpdateFireHandler : EventFireHandler
	{
		// Token: 0x06008A7D RID: 35453 RVA: 0x00052EEB File Offset: 0x000510EB
		[NullableContext(1)]
		public EarlyUpdateFireHandler(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
			: base(typeof(EarlyUpdateEvent), method, methodHandle, handlerArgumentsDescription)
		{
		}
	}
}
