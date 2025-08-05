using System;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;
using Tanks.Lobby.ClientUserProfile.API;

// Token: 0x02000090 RID: 144
[Shared]
[SerialVersionUID(32195187150433L)]
public class UserPublisherComponent : Component
{
	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060002CE RID: 718 RVA: 0x0000709F File Offset: 0x0000529F
	// (set) Token: 0x060002CF RID: 719 RVA: 0x000070A7 File Offset: 0x000052A7
	public Publisher Publisher { get; set; }
}
