using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

// Token: 0x02000037 RID: 55
[NullableContext(1)]
[Nullable(0)]
[Shared]
[SerialVersionUID(636174034381039231L)]
public class ItemsPackFromConfigComponent : Component
{
	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005BF7 File Offset: 0x00003DF7
	// (set) Token: 0x060000D4 RID: 212 RVA: 0x00005BFF File Offset: 0x00003DFF
	public List<long> Pack { get; set; }
}
