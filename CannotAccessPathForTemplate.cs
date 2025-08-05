using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200285A RID: 10330
	public class CannotAccessPathForTemplate : Exception
	{
		// Token: 0x06008A27 RID: 35367 RVA: 0x00052AF2 File Offset: 0x00050CF2
		[NullableContext(1)]
		public CannotAccessPathForTemplate(Type templateClass)
			: base(templateClass.FullName)
		{
		}
	}
}
