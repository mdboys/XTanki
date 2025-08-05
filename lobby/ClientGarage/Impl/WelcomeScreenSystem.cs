using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientUnityIntegration.API;
using Tanks.Lobby.ClientNavigation.API;

namespace Lobby.ClientGarage.Impl
{
	// Token: 0x02002ADF RID: 10975
	public class WelcomeScreenSystem : ECSSystem
	{
		// Token: 0x0600972E RID: 38702 RVA: 0x0005A533 File Offset: 0x00058733
		[NullableContext(1)]
		[OnEventFire]
		public void ButtonClick(ButtonClickEvent e, SingleNode<WelcomeScreenCloseButton> closeButton, [JoinAll] SingleNode<Dialogs60Component> dialogs)
		{
			dialogs.component.Get<WelcomeScreenDialog>().Hide();
		}
	}
}
