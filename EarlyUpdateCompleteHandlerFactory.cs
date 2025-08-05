using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200287B RID: 10363
	public class EarlyUpdateCompleteHandlerFactory : BroadcastEventHandlerFactory
	{
		// Token: 0x06008A7B RID: 35451 RVA: 0x00052EC5 File Offset: 0x000510C5
		public EarlyUpdateCompleteHandlerFactory()
			: base(typeof(OnEventComplete), typeof(EarlyUpdateEvent))
		{
		}

		// Token: 0x06008A7C RID: 35452 RVA: 0x00052EE1 File Offset: 0x000510E1
		[NullableContext(1)]
		protected override Handler CreateHandlerInstance(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
		{
			return new EarlyUpdateCompleteHandler(method, methodHandle, handlerArgumentsDescription);
		}
	}
}
