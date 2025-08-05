using System;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientUnityIntegration.API;

// Token: 0x0200003F RID: 63
public class MarketItemSaleComponent : Component
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060000EC RID: 236 RVA: 0x00005CE8 File Offset: 0x00003EE8
	// (set) Token: 0x060000ED RID: 237 RVA: 0x00005CF0 File Offset: 0x00003EF0
	public int salePercent { get; set; }

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060000EE RID: 238 RVA: 0x00005CF9 File Offset: 0x00003EF9
	// (set) Token: 0x060000EF RID: 239 RVA: 0x00005D01 File Offset: 0x00003F01
	public int priceOffset { get; set; }

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005D0A File Offset: 0x00003F0A
	// (set) Token: 0x060000F1 RID: 241 RVA: 0x00005D12 File Offset: 0x00003F12
	public int xPriceOffset { get; set; }

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005D1B File Offset: 0x00003F1B
	// (set) Token: 0x060000F3 RID: 243 RVA: 0x00005D23 File Offset: 0x00003F23
	public Date endDate { get; set; }
}
