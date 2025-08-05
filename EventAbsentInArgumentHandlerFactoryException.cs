using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200289C RID: 10396
	[NullableContext(1)]
	[Nullable(0)]
	public class EventAbsentInArgumentHandlerFactoryException : HandlerFactoryException
	{
		// Token: 0x06008BA0 RID: 35744 RVA: 0x00053A83 File Offset: 0x00051C83
		public EventAbsentInArgumentHandlerFactoryException(MethodInfo method)
			: base(method)
		{
		}

		// Token: 0x06008BA1 RID: 35745 RVA: 0x00053A8C File Offset: 0x00051C8C
		public EventAbsentInArgumentHandlerFactoryException(MethodInfo method, Type type)
			: base(method, type)
		{
		}
	}
}
