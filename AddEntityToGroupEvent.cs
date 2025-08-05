using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002843 RID: 10307
	[NullableContext(1)]
	[Nullable(0)]
	public class AddEntityToGroupEvent : Event
	{
		// Token: 0x060089DB RID: 35291 RVA: 0x00052783 File Offset: 0x00050983
		public AddEntityToGroupEvent(GroupComponent group)
		{
			this.Group = group;
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x060089DC RID: 35292 RVA: 0x00052792 File Offset: 0x00050992
		public GroupComponent Group { get; }
	}
}
