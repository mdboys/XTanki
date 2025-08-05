using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002989 RID: 10633
	[NullableContext(1)]
	public interface TemplateDescription
	{
		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06008EDC RID: 36572
		long TemplateId { get; }

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06008EDD RID: 36573
		string TemplateName { get; }

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06008EDE RID: 36574
		Type TemplateClass { get; }

		// Token: 0x06008EDF RID: 36575
		bool IsComponentDescriptionPresent(Type componentType);

		// Token: 0x06008EE0 RID: 36576
		ComponentDescription GetComponentDescription(Type componentType);

		// Token: 0x06008EE1 RID: 36577
		ICollection<Type> GetAutoAddedComponentTypes();
	}
}
