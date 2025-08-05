using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002849 RID: 10313
	public class ArgumentMustBeNodeException : Exception
	{
		// Token: 0x060089E7 RID: 35303 RVA: 0x0005283F File Offset: 0x00050A3F
		[NullableContext(1)]
		public ArgumentMustBeNodeException(MethodInfo method, ParameterInfo parameterInfo)
			: base(string.Format("method={0},type={1}", method, parameterInfo.ParameterType))
		{
		}
	}
}
