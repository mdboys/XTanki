using System;
using System.Runtime.CompilerServices;
using Platform.Library.ClientProtocol.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002975 RID: 10613
	[SerialVersionUID(4343317511298323469L)]
	public class NodeAddedEvent : Event
	{
		// Token: 0x04005FE3 RID: 24547
		[Nullable(1)]
		public static readonly NodeAddedEvent Instance = new NodeAddedEvent();
	}
}
