using System;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

namespace Platform.Common.ClientECSCommon.API
{
	// Token: 0x02002990 RID: 10640
	[Shared]
	[SerialVersionUID(1446020883951L)]
	public class CreatedTimestampComponent : Component
	{
		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06008EF0 RID: 36592 RVA: 0x00055A6F File Offset: 0x00053C6F
		// (set) Token: 0x06008EF1 RID: 36593 RVA: 0x00055A77 File Offset: 0x00053C77
		public long Timestamp { get; set; }
	}
}
