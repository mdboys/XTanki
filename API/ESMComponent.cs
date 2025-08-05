using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002963 RID: 10595
	[NullableContext(1)]
	[Nullable(0)]
	public class ESMComponent : Component, AttachToEntityListener
	{
		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06008E78 RID: 36472 RVA: 0x00055814 File Offset: 0x00053A14
		public EntityStateMachine Esm
		{
			get
			{
				return this.esm;
			}
		}

		// Token: 0x06008E79 RID: 36473 RVA: 0x0005581C File Offset: 0x00053A1C
		public void AttachedToEntity(Entity entity)
		{
			this.Esm.AttachToEntity(entity);
		}

		// Token: 0x04005FCB RID: 24523
		public EntityStateMachine esm = new EntityStateMachineImpl();
	}
}
