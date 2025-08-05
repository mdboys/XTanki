using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002842 RID: 10306
	[NullableContext(1)]
	[Nullable(0)]
	[SerialVersionUID(1431504291271L)]
	public class AddChildGroupToGroupEvent : Event
	{
		// Token: 0x060089D8 RID: 35288 RVA: 0x0005275D File Offset: 0x0005095D
		public AddChildGroupToGroupEvent(GroupComponent groupComponent, Type childGroupClass)
		{
			this.Group = groupComponent;
			this.ChildGroupClass = childGroupClass;
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x060089D9 RID: 35289 RVA: 0x00052773 File Offset: 0x00050973
		public GroupComponent Group { get; }

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x060089DA RID: 35290 RVA: 0x0005277B File Offset: 0x0005097B
		public Type ChildGroupClass { get; }
	}
}
