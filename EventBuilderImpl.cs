using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200289D RID: 10397
	[NullableContext(1)]
	[Nullable(0)]
	public class EventBuilderImpl : EventBuilder
	{
		// Token: 0x06008BA2 RID: 35746 RVA: 0x00053A96 File Offset: 0x00051C96
		public EventBuilder Attach(Entity entity)
		{
			if (entity == null)
			{
				throw new NullEntityException();
			}
			this._entities.Add(entity);
			return this;
		}

		// Token: 0x06008BA3 RID: 35747 RVA: 0x00053AAE File Offset: 0x00051CAE
		public EventBuilder Attach(Node node)
		{
			return this.Attach(node.Entity);
		}

		// Token: 0x06008BA4 RID: 35748 RVA: 0x00133594 File Offset: 0x00131794
		public EventBuilder Attach<[Nullable(0)] T>(ICollection<T> nodes) where T : Node
		{
			foreach (T t in nodes)
			{
				this.Attach(t.Entity);
			}
			return this;
		}

		// Token: 0x06008BA5 RID: 35749 RVA: 0x001335E8 File Offset: 0x001317E8
		public EventBuilder AttachAll(ICollection<Entity> entities)
		{
			foreach (Entity entity in entities)
			{
				this.Attach(entity);
			}
			return this;
		}

		// Token: 0x06008BA6 RID: 35750 RVA: 0x00133634 File Offset: 0x00131834
		public EventBuilder AttachAll(params Entity[] entities)
		{
			foreach (Entity entity in entities)
			{
				this.Attach(entity);
			}
			return this;
		}

		// Token: 0x06008BA7 RID: 35751 RVA: 0x00133660 File Offset: 0x00131860
		public EventBuilder AttachAll(params Node[] nodes)
		{
			foreach (Node node in nodes)
			{
				this.Attach(node);
			}
			return this;
		}

		// Token: 0x06008BA8 RID: 35752 RVA: 0x00053ABC File Offset: 0x00051CBC
		public void Schedule()
		{
			this._flow.SendEvent(this._eventInstance, this._entities);
		}

		// Token: 0x06008BA9 RID: 35753 RVA: 0x0013368C File Offset: 0x0013188C
		public ScheduledEvent ScheduleDelayed(float timeInSec)
		{
			ScheduleManager scheduleManager = this._delayedEventManager.ScheduleDelayedEvent(this._eventInstance, this._entities, timeInSec);
			return new ScheduledEventImpl(this._eventInstance, scheduleManager);
		}

		// Token: 0x06008BAA RID: 35754 RVA: 0x001336C0 File Offset: 0x001318C0
		public ScheduledEvent SchedulePeriodic(float timeInSec)
		{
			ScheduleManager scheduleManager = this._delayedEventManager.SchedulePeriodicEvent(this._eventInstance, this._entities, timeInSec);
			return new ScheduledEventImpl(this._eventInstance, scheduleManager);
		}

		// Token: 0x06008BAB RID: 35755 RVA: 0x00053AD5 File Offset: 0x00051CD5
		public EventBuilderImpl Init(DelayedEventManager delayedEventManager, Flow flow, Event eventInstance)
		{
			this._delayedEventManager = delayedEventManager;
			this._flow = flow;
			this._eventInstance = eventInstance;
			this._entities.Clear();
			return this;
		}

		// Token: 0x04005EC8 RID: 24264
		private readonly List<Entity> _entities = new List<Entity>();

		// Token: 0x04005EC9 RID: 24265
		private DelayedEventManager _delayedEventManager;

		// Token: 0x04005ECA RID: 24266
		private Event _eventInstance;

		// Token: 0x04005ECB RID: 24267
		private Flow _flow;
	}
}
