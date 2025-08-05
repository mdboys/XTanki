using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using UnityEngine;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002876 RID: 10358
	[NullableContext(1)]
	[Nullable(0)]
	public class DelayedEventManager
	{
		// Token: 0x06008A6A RID: 35434 RVA: 0x00052E07 File Offset: 0x00051007
		public DelayedEventManager(EngineServiceInternal engine)
		{
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x001318D8 File Offset: 0x0012FAD8
		public ScheduleManager SchedulePeriodicEvent(Platform.Kernel.ECS.ClientEntitySystem.API.Event e, ICollection<Entity> entities, float timeInSec)
		{
			PeriodicEventTask periodicEventTask = new PeriodicEventTask(e, this.<engine>P, entities, timeInSec);
			this._periodicTasks.AddLast(periodicEventTask);
			return periodicEventTask;
		}

		// Token: 0x06008A6C RID: 35436 RVA: 0x00131904 File Offset: 0x0012FB04
		public ScheduleManager ScheduleDelayedEvent(Platform.Kernel.ECS.ClientEntitySystem.API.Event e, ICollection<Entity> entities, float timeInSec)
		{
			DelayedEventTask delayedEventTask = new DelayedEventTask(e, entities, this.<engine>P, (double)(Time.time + timeInSec));
			this._delayedTasks.AddLast(delayedEventTask);
			return delayedEventTask;
		}

		// Token: 0x06008A6D RID: 35437 RVA: 0x00052E2C File Offset: 0x0005102C
		public void Update(double time)
		{
			this.UpdatePeriodicTasks(time);
			this.UpdateDelayedTasks(time);
		}

		// Token: 0x06008A6E RID: 35438 RVA: 0x00131938 File Offset: 0x0012FB38
		private void UpdateDelayedTasks(double time)
		{
			LinkedListNode<DelayedEventTask> next;
			for (LinkedListNode<DelayedEventTask> linkedListNode = this._delayedTasks.First; linkedListNode != null; linkedListNode = next)
			{
				DelayedEventTask value = linkedListNode.Value;
				next = linkedListNode.Next;
				if (value.IsCanceled())
				{
					this._delayedTasks.Remove(value);
				}
				else
				{
					this.TryUpdate(time, value);
				}
			}
		}

		// Token: 0x06008A6F RID: 35439 RVA: 0x00131984 File Offset: 0x0012FB84
		private void TryUpdate(double time, DelayedEventTask task)
		{
			try
			{
				if (task.Update(time))
				{
					this._delayedTasks.Remove(task);
				}
			}
			catch
			{
				this._delayedTasks.Remove(task);
				throw;
			}
		}

		// Token: 0x06008A70 RID: 35440 RVA: 0x001319CC File Offset: 0x0012FBCC
		private void UpdatePeriodicTasks(double time)
		{
			LinkedListNode<PeriodicEventTask> next;
			for (LinkedListNode<PeriodicEventTask> linkedListNode = this._periodicTasks.First; linkedListNode != null; linkedListNode = next)
			{
				PeriodicEventTask value = linkedListNode.Value;
				next = linkedListNode.Next;
				if (value.IsCanceled())
				{
					this._periodicTasks.Remove(linkedListNode);
				}
				else
				{
					value.Update(time);
				}
			}
		}

		// Token: 0x04005E78 RID: 24184
		[CompilerGenerated]
		private EngineServiceInternal <engine>P = engine;

		// Token: 0x04005E79 RID: 24185
		private readonly LinkedList<DelayedEventTask> _delayedTasks = new LinkedList<DelayedEventTask>();

		// Token: 0x04005E7A RID: 24186
		private readonly LinkedList<PeriodicEventTask> _periodicTasks = new LinkedList<PeriodicEventTask>();
	}
}
