using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002976 RID: 10614
	[NullableContext(1)]
	public interface NodeDescriptionRegistry
	{
		// Token: 0x06008EBA RID: 36538
		void AddNodeDescription(NodeDescription nodeDescription);

		// Token: 0x06008EBB RID: 36539
		ICollection<NodeDescription> GetNodeDescriptions(Type componentClass);

		// Token: 0x06008EBC RID: 36540
		ICollection<NodeDescription> GetNodeDescriptionsByNotComponent(Type componentClass);

		// Token: 0x06008EBD RID: 36541
		ICollection<NodeDescription> GetNodeDescriptionsWithNotComponentsOnly();

		// Token: 0x06008EBE RID: 36542
		NodeClassInstanceDescription GetOrCreateNodeClassDescription(Type nodeClass, [Nullable(new byte[] { 2, 1 })] ICollection<Type> additionalComponents = null);

		// Token: 0x06008EBF RID: 36543
		ICollection<NodeClassInstanceDescription> GetClassInstanceDescriptionByComponent(Type component);

		// Token: 0x06008EC0 RID: 36544
		void AssertRegister(NodeDescription nodeDescription);
	}
}
