using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200298E RID: 10638
	public class UpdateEvent : BaseUpdateEvent
	{
		// Token: 0x04005FF0 RID: 24560
		[Nullable(1)]
		public static readonly UpdateEvent Instance = new UpdateEvent();
	}
}
