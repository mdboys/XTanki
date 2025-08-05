using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002867 RID: 10343
	public class ComponentInfoNotFoundException : Exception
	{
		// Token: 0x06008A44 RID: 35396 RVA: 0x00052C9E File Offset: 0x00050E9E
		[NullableContext(1)]
		public ComponentInfoNotFoundException(Type infoType, MethodInfo componentMethod)
			: base(string.Format("infoType={0} componentMethod={1}", infoType, componentMethod))
		{
		}
	}
}
