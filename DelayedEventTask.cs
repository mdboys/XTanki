using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002877 RID: 10359
	[NullableContext(1)]
	[Nullable(0)]
	public class DelayedEventTask : ScheduleManager
	{
		// Token: 0x06008A71 RID: 35441 RVA: 0x00131A18 File Offset: 0x0012FC18
		public DelayedEventTask(Event e, ICollection<Entity> entities, EngineServiceInternal engine, double timeToExecute)
		{
			HashSet<Entity> hashSet = new HashSet<Entity>();
			foreach (Entity entity in entities)
			{
				hashSet.Add(entity);
			}
			this._entities = hashSet;
			base..ctor();
		}

		// Token: 0x06008A72 RID: 35442 RVA: 0x00052E3C File Offset: 0x0005103C
		public bool Cancel()
		{
			this._canceled = true;
			return this._invoked;
		}

		// Token: 0x06008A73 RID: 35443 RVA: 0x00131A8C File Offset: 0x0012FC8C
		public bool Update(double time)
		{
			if (this.<timeToExecute>P > time)
			{
				return this._invoked;
			}
			Flow flow = this.<engine>P.GetFlow();
			this.RemoveDeletedEntities();
			flow.SendEvent(this.<e>P, this._entities);
			this._invoked = true;
			return this._invoked;
		}

		// Token: 0x06008A74 RID: 35444 RVA: 0x00052E4B File Offset: 0x0005104B
		private void RemoveDeletedEntities()
		{
			this._entities.RemoveWhere((Entity entity) => !((EntityImpl)entity).Alive);
		}

		// Token: 0x06008A75 RID: 35445 RVA: 0x00052E78 File Offset: 0x00051078
		public bool IsCanceled()
		{
			return this._canceled;
		}

		// Token: 0x04005E7B RID: 24187
		[CompilerGenerated]
		private Event <e>P = e;

		// Token: 0x04005E7C RID: 24188
		[CompilerGenerated]
		private EngineServiceInternal <engine>P = engine;

		// Token: 0x04005E7D RID: 24189
		[CompilerGenerated]
		private double <timeToExecute>P = timeToExecute;

		// Token: 0x04005E7E RID: 24190
		private readonly HashSet<Entity> _entities;

		// Token: 0x04005E7F RID: 24191
		private bool _canceled;

		// Token: 0x04005E80 RID: 24192
		private bool _invoked;
	}
}
