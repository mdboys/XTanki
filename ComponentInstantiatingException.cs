using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002868 RID: 10344
	public class ComponentInstantiatingException : Exception
	{
		// Token: 0x06008A45 RID: 35397 RVA: 0x00052AF2 File Offset: 0x00050CF2
		[NullableContext(1)]
		public ComponentInstantiatingException(Type componentClass)
			: base(componentClass.FullName)
		{
		}
	}
}
