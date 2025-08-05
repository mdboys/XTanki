using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200289F RID: 10399
	public class EventCompleteHandlerFactory : AbstractHandlerFactory
	{
		// Token: 0x06008BAE RID: 35758 RVA: 0x00053B18 File Offset: 0x00051D18
		public EventCompleteHandlerFactory()
			: base(typeof(OnEventComplete), Collections.SingletonList<Type>(typeof(Event)))
		{
		}

		// Token: 0x06008BAF RID: 35759 RVA: 0x00053B39 File Offset: 0x00051D39
		[NullableContext(1)]
		protected override Handler CreateHandlerInstance(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
		{
			return new EventCompleteHandler(method.GetParameters()[0].ParameterType, method, methodHandle, handlerArgumentsDescription);
		}
	}
}
