using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002983 RID: 10627
	public class SingleNode<T> : AbstractSingleNode where T : Component
	{
		// Token: 0x04005FEA RID: 24554
		[Nullable(1)]
		public T component;
	}
}
