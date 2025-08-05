using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200294F RID: 10575
	[NullableContext(1)]
	public interface ComponentInfoBuilder
	{
		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06008E11 RID: 36369
		Type TemplateComponentInfoClass { get; }

		// Token: 0x06008E12 RID: 36370
		bool IsAcceptable(MethodInfo componentMethod);

		// Token: 0x06008E13 RID: 36371
		ComponentInfo Build(MethodInfo componentMethod, ComponentDescriptionImpl componentDescription);
	}
}
