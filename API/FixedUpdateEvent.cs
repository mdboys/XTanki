using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002967 RID: 10599
	public class FixedUpdateEvent : BaseUpdateEvent
	{
		// Token: 0x04005FCC RID: 24524
		[Nullable(1)]
		public static readonly FixedUpdateEvent Instance = new FixedUpdateEvent();
	}
}
