using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Tanks.Lobby.ClientControls.API;

namespace Lobby.ClientGarage.Impl
{
	// Token: 0x02002ADC RID: 10972
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockKeyboardForListOnConfirmButtonSystem : ECSSystem
	{
		// Token: 0x06009728 RID: 38696 RVA: 0x0005A517 File Offset: 0x00058717
		[OnEventFire]
		public void Block(ConfirmButtonClickEvent e, Node node, [JoinAll] [Combine] SingleNode<SimpleHorizontalListComponent> list)
		{
			list.component.IsKeyboardNavigationAllowed = false;
		}

		// Token: 0x06009729 RID: 38697 RVA: 0x0005A525 File Offset: 0x00058725
		[OnEventFire]
		public void Unblock(ConfirmButtonClickNoEvent e, Node node, [JoinAll] [Combine] SingleNode<SimpleHorizontalListComponent> list)
		{
			list.component.IsKeyboardNavigationAllowed = true;
		}

		// Token: 0x0600972A RID: 38698 RVA: 0x0005A525 File Offset: 0x00058725
		[OnEventFire]
		public void Unblock(ConfirmButtonClickYesEvent e, Node node, [JoinAll] [Combine] SingleNode<SimpleHorizontalListComponent> list)
		{
			list.component.IsKeyboardNavigationAllowed = true;
		}
	}
}
