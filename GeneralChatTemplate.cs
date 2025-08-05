using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;
using Tanks.Lobby.ClientCommunicator.API;
using Tanks.Lobby.ClientCommunicator.Impl;

// Token: 0x02000030 RID: 48
[SerialVersionUID(636451756485532125L)]
public interface GeneralChatTemplate : ChatTemplate, Template
{
	// Token: 0x060000C3 RID: 195
	[NullableContext(1)]
	GeneralChatComponent generalChat();
}
