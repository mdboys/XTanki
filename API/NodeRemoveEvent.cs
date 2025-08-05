using System;
using System.Runtime.CompilerServices;
using Platform.Library.ClientProtocol.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002977 RID: 10615
	[SerialVersionUID(2223729871283165639L)]
	public class NodeRemoveEvent : Event
	{
		// Token: 0x04005FE4 RID: 24548
		[Nullable(1)]
		public static readonly NodeRemoveEvent Instance = new NodeRemoveEvent();
	}
}
