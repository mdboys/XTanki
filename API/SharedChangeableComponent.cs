using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002981 RID: 10625
	public class SharedChangeableComponent : Component, AttachToEntityListener, DetachFromEntityListener
	{
		// Token: 0x06008ED2 RID: 36562 RVA: 0x00055A0A File Offset: 0x00053C0A
		[NullableContext(1)]
		public void AttachedToEntity(Entity entity)
		{
			this.entity = (EntityInternal)entity;
		}

		// Token: 0x06008ED3 RID: 36563 RVA: 0x00055A18 File Offset: 0x00053C18
		[NullableContext(1)]
		public void DetachedFromEntity(Entity entity)
		{
			this.entity = null;
		}

		// Token: 0x06008ED4 RID: 36564 RVA: 0x00055A21 File Offset: 0x00053C21
		public void OnChange()
		{
			if (this.entity != null && this.entity.HasComponent(base.GetType()))
			{
				this.entity.NotifyComponentChange(base.GetType());
			}
		}

		// Token: 0x04005FE9 RID: 24553
		[Nullable(1)]
		private EntityInternal entity;
	}
}
