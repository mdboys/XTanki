using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002857 RID: 10327
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BroadcastEventHandlerFactory : ConcreteEventHandlerFactory
	{
		// Token: 0x06008A1D RID: 35357 RVA: 0x00052A76 File Offset: 0x00050C76
		protected BroadcastEventHandlerFactory(Type annotationEventClass, Type parameterClass)
			: base(annotationEventClass, parameterClass)
		{
		}

		// Token: 0x06008A1E RID: 35358 RVA: 0x00052A80 File Offset: 0x00050C80
		protected override void Validate(MethodInfo method, HandlerArgumentsDescription argumentsDescription)
		{
			base.Validate(method, argumentsDescription);
			if (argumentsDescription.HandlerArguments.Count((HandlerArgument handlerArgument) => handlerArgument.Context) > 1)
			{
				throw new MultipleContextNodesNotSupportedException(method);
			}
		}
	}
}
