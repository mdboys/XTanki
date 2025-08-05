using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A8 RID: 10408
	public class FixedUpdateEventCompleteHandlerFactory : BroadcastEventHandlerFactory
	{
		// Token: 0x06008BBE RID: 35774 RVA: 0x00053C46 File Offset: 0x00051E46
		public FixedUpdateEventCompleteHandlerFactory()
			: base(typeof(OnEventComplete), typeof(FixedUpdateEvent))
		{
		}

		// Token: 0x06008BBF RID: 35775 RVA: 0x00053C62 File Offset: 0x00051E62
		[NullableContext(1)]
		protected override Handler CreateHandlerInstance(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
		{
			return new FixedUpdateEventCompleteHandler(method, methodHandle, handlerArgumentsDescription);
		}
	}
}
