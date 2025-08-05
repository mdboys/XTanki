using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002879 RID: 10361
	public class DuplicateComponentOnTemplateException : Exception
	{
		// Token: 0x06008A79 RID: 35449 RVA: 0x00052E9C File Offset: 0x0005109C
		[NullableContext(1)]
		public DuplicateComponentOnTemplateException(TemplateDescription templateDescription, Type componentType)
			: base(string.Format("templateDescription={0} componentType={1}", templateDescription, componentType))
		{
		}
	}
}
