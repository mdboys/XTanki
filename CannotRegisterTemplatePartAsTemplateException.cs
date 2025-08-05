using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200285B RID: 10331
	public class CannotRegisterTemplatePartAsTemplateException : Exception
	{
		// Token: 0x06008A28 RID: 35368 RVA: 0x00052B00 File Offset: 0x00050D00
		[NullableContext(1)]
		public CannotRegisterTemplatePartAsTemplateException(Type templateClass)
			: base(string.Format("templateClass={0}", templateClass))
		{
		}
	}
}
