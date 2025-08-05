using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200286A RID: 10346
	public class ComponentNotFoundInTemplateException : Exception
	{
		// Token: 0x06008A47 RID: 35399 RVA: 0x00052CB2 File Offset: 0x00050EB2
		[NullableContext(1)]
		public ComponentNotFoundInTemplateException(Type componentClass, string templateName)
			: base("template= " + templateName + " component= " + componentClass.FullName)
		{
		}
	}
}
