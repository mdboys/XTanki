using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

namespace Platform.Common.ClientECSCommon.API
{
	// Token: 0x02002992 RID: 10642
	[NullableContext(1)]
	[Nullable(0)]
	[SerialVersionUID(1446021176912L)]
	public class OwnerComponent : Component
	{
		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06008EF7 RID: 36599 RVA: 0x00055AA0 File Offset: 0x00053CA0
		// (set) Token: 0x06008EF8 RID: 36600 RVA: 0x00055AA8 File Offset: 0x00053CA8
		private Entity Owner { get; set; }
	}
}
