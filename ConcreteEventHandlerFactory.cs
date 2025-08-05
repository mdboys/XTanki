using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200286D RID: 10349
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ConcreteEventHandlerFactory : AbstractHandlerFactory
	{
		// Token: 0x06008A4B RID: 35403 RVA: 0x00052CEC File Offset: 0x00050EEC
		protected ConcreteEventHandlerFactory(Type annotationEventClass, Type parameterClass)
			: base(annotationEventClass, Collections.SingletonList<Type>(this.<parameterClass>P))
		{
		}

		// Token: 0x06008A4C RID: 35404 RVA: 0x001316E8 File Offset: 0x0012F8E8
		protected override bool IsSelf(MethodInfo method)
		{
			if (!base.IsSelf(method))
			{
				return false;
			}
			ParameterInfo[] parameters = method.GetParameters();
			return parameters.Length != 0 && parameters[0].ParameterType == this.<parameterClass>P;
		}

		// Token: 0x04005E6C RID: 24172
		[CompilerGenerated]
		private Type <parameterClass>P = parameterClass;
	}
}
