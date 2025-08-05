using System;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002948 RID: 10568
	public abstract class BaseUpdateEvent : Event
	{
		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06008E07 RID: 36359 RVA: 0x000556BF File Offset: 0x000538BF
		// (set) Token: 0x06008E08 RID: 36360 RVA: 0x000556C7 File Offset: 0x000538C7
		public float DeltaTime { get; set; }
	}
}
