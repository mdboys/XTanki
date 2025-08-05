using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002865 RID: 10341
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ComponentEvent : Event
	{
		// Token: 0x06008A40 RID: 35392 RVA: 0x00052C64 File Offset: 0x00050E64
		protected ComponentEvent(Type componentType)
		{
			this.ComponentType = componentType;
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06008A41 RID: 35393 RVA: 0x00052C73 File Offset: 0x00050E73
		// (set) Token: 0x06008A42 RID: 35394 RVA: 0x00052C7B File Offset: 0x00050E7B
		public Type ComponentType { get; private set; }
	}
}
