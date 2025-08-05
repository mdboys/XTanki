using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

// Token: 0x0200008F RID: 143
[NullableContext(1)]
[Nullable(0)]
[Shared]
[SerialVersionUID(1623342142134L)]
public class UpdateGoodsPriceEvent : Event
{
	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000706C File Offset: 0x0000526C
	// (set) Token: 0x060002C8 RID: 712 RVA: 0x00007074 File Offset: 0x00005274
	public string Currency { get; set; }

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000707D File Offset: 0x0000527D
	// (set) Token: 0x060002CA RID: 714 RVA: 0x00007085 File Offset: 0x00005285
	public double Price { get; set; }

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060002CB RID: 715 RVA: 0x0000708E File Offset: 0x0000528E
	// (set) Token: 0x060002CC RID: 716 RVA: 0x00007096 File Offset: 0x00005296
	public float DiscountCoeff { get; set; }
}
