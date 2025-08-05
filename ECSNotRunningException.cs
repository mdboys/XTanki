using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200287E RID: 10366
	public class ECSNotRunningException : Exception
	{
		// Token: 0x06008A80 RID: 35456 RVA: 0x000097AF File Offset: 0x000079AF
		[NullableContext(1)]
		public ECSNotRunningException(string str)
			: base(str)
		{
		}
	}
}
