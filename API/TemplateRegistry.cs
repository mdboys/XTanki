using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200298B RID: 10635
	[NullableContext(1)]
	public interface TemplateRegistry
	{
		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06008EE3 RID: 36579
		ICollection<ComponentInfoBuilder> ComponentInfoBuilders { get; }

		// Token: 0x06008EE4 RID: 36580
		TemplateDescription GetTemplateInfo(long id);

		// Token: 0x06008EE5 RID: 36581
		TemplateDescription GetTemplateInfo(Type templateType);

		// Token: 0x06008EE6 RID: 36582
		void Register(Type templateClass);

		// Token: 0x06008EE7 RID: 36583
		[NullableContext(0)]
		void Register<T>() where T : Template;

		// Token: 0x06008EE8 RID: 36584
		[NullableContext(0)]
		void RegisterPart<T>() where T : Template;

		// Token: 0x06008EE9 RID: 36585
		ICollection<Type> GetParentTemplates(Type templateType);
	}
}
