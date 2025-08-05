using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

namespace Lobby.ClientPayment.API
{
	// Token: 0x02002AD2 RID: 10962
	[NullableContext(1)]
	[SerialVersionUID(635993398222494398L)]
	public interface CountriesTemplate : Template
	{
		// Token: 0x06009714 RID: 38676
		[AutoAdded]
		[PersistentConfig("", false)]
		CountriesComponent countries();

		// Token: 0x06009715 RID: 38677
		[AutoAdded]
		[PersistentConfig("", false)]
		PhoneCodesComponent phoneCodes();
	}
}
