using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028A1 RID: 10401
	public class EventFireHandlerFactory : AbstractHandlerFactory
	{
		// Token: 0x06008BB1 RID: 35761 RVA: 0x00053B50 File Offset: 0x00051D50
		public EventFireHandlerFactory()
			: base(typeof(OnEventFire), Collections.SingletonList<Type>(typeof(Event)))
		{
		}

		// Token: 0x06008BB2 RID: 35762 RVA: 0x00053B71 File Offset: 0x00051D71
		[NullableContext(1)]
		protected override Handler CreateHandlerInstance(MethodInfo method, MethodHandle methodHandle, HandlerArgumentsDescription handlerArgumentsDescription)
		{
			return new EventFireHandler(method.GetParameters()[0].ParameterType, method, methodHandle, handlerArgumentsDescription);
		}
	}
}
