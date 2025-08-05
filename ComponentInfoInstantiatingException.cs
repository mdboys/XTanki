using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002866 RID: 10342
	public class ComponentInfoInstantiatingException : Exception
	{
		// Token: 0x06008A43 RID: 35395 RVA: 0x00052C84 File Offset: 0x00050E84
		[NullableContext(1)]
		public ComponentInfoInstantiatingException(Type componentInfoClass, Exception e)
			: base(string.Format("component info class = {0}, message = {1}", componentInfoClass, e.Message), e)
		{
		}
	}
}
