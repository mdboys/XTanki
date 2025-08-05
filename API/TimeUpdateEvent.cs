using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200298D RID: 10637
	public class TimeUpdateEvent : BaseUpdateEvent
	{
		// Token: 0x04005FEF RID: 24559
		[Nullable(1)]
		public static readonly TimeUpdateEvent Instance = new TimeUpdateEvent();
	}
}
