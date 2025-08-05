using System;
using System.Runtime.CompilerServices;
using Lobby.ClientPayment.API;
using Lobby.ClientPayment.Impl;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Tanks.Lobby.ClientPayment.Impl;

// Token: 0x02000031 RID: 49
public class GoodsPriceSystem : ECSSystem
{
	// Token: 0x060000C4 RID: 196 RVA: 0x0005F3B4 File Offset: 0x0005D5B4
	[NullableContext(1)]
	[OnEventFire]
	public void UpdateGoodsPrice(UpdateGoodsPriceEvent e, GoodsPriceSystem.GoodsNode goods)
	{
		if (goods.Entity.HasComponent<GoodsPriceComponent>())
		{
			GoodsPriceComponent component = goods.Entity.GetComponent<GoodsPriceComponent>();
			component.Currency = e.Currency;
			component.Price = e.Price;
		}
		else
		{
			GoodsPriceComponent goodsPriceComponent = new GoodsPriceComponent
			{
				Currency = e.Currency,
				Price = e.Price
			};
			goods.Entity.AddComponent(goodsPriceComponent);
		}
		if (goods.Entity.HasComponent<SpecialOfferComponent>())
		{
			goods.Entity.GetComponent<SpecialOfferComponent>().Discount = (double)(e.DiscountCoeff * 100f);
		}
		base.ScheduleEvent<GoodsChangedEvent>(goods.Entity);
	}

	// Token: 0x02000032 RID: 50
	public class GoodsNode : Node
	{
		// Token: 0x0400006D RID: 109
		[Nullable(1)]
		public GoodsComponent goods;
	}
}
