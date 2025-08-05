using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002945 RID: 10565
	public class AfterFixedUpdateEvent : Event
	{
		// Token: 0x04005FBE RID: 24510
		[Nullable(1)]
		public static readonly AfterFixedUpdateEvent Instance = new AfterFixedUpdateEvent();
	}
}
