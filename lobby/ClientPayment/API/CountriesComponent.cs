using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Lobby.ClientPayment.API
{
	// Token: 0x02002AD1 RID: 10961
	[NullableContext(1)]
	[Nullable(0)]
	public class CountriesComponent : Component
	{
		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06009711 RID: 38673 RVA: 0x0005A4B1 File Offset: 0x000586B1
		// (set) Token: 0x06009712 RID: 38674 RVA: 0x0005A4B9 File Offset: 0x000586B9
		public Dictionary<string, string> Names { get; set; }
	}
}
