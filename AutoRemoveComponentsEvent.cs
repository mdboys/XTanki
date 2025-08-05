using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002850 RID: 10320
	[NullableContext(1)]
	[Nullable(0)]
	[SerialVersionUID(1446717399705L)]
	public class AutoRemoveComponentsEvent : Event
	{
		// Token: 0x06008A04 RID: 35332 RVA: 0x0005294B File Offset: 0x00050B4B
		public AutoRemoveComponentsEvent(List<Type> componentsToRemove)
		{
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06008A05 RID: 35333 RVA: 0x0005295A File Offset: 0x00050B5A
		// (set) Token: 0x06008A06 RID: 35334 RVA: 0x00052962 File Offset: 0x00050B62
		public List<Type> ComponentsToRemove { get; set; } = componentsToRemove;
	}
}
