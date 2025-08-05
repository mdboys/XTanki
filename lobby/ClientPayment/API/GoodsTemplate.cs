using System;
using System.Runtime.CompilerServices;
using Lobby.ClientPayment.Impl;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

namespace Lobby.ClientPayment.API
{
	// Token: 0x02002AD5 RID: 10965
	[NullableContext(1)]
	[SerialVersionUID(1455968728337L)]
	public interface GoodsTemplate : Template
	{
		// Token: 0x06009717 RID: 38679
		[AutoAdded]
		GoodsComponent Goods();

		// Token: 0x06009718 RID: 38680
		GoodsPriceComponent GoodsPrice();
	}
}
