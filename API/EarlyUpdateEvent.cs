using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002957 RID: 10583
	public class EarlyUpdateEvent : BaseUpdateEvent
	{
		// Token: 0x04005FC0 RID: 24512
		[Nullable(1)]
		public static readonly EarlyUpdateEvent Instance = new EarlyUpdateEvent();
	}
}
