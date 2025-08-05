using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002853 RID: 10323
	[NullableContext(1)]
	[Nullable(0)]
	public class AutoRemoveComponentsSystem : ECSSystem
	{
		// Token: 0x06008A0D RID: 35341 RVA: 0x000529AD File Offset: 0x00050BAD
		public AutoRemoveComponentsSystem(AutoRemoveComponentsRegistry autoRemoveComponentsRegistry)
		{
			AutoRemoveComponentsSystem.registry = autoRemoveComponentsRegistry;
		}

		// Token: 0x06008A0E RID: 35342 RVA: 0x00131148 File Offset: 0x0012F348
		[OnEventComplete]
		public void AutoRemoveComponentsIfNeed(NodeAddedEvent e, SingleNode<DeletedEntityComponent> node)
		{
			List<Type> componentsToRemove = AutoRemoveComponentsSystem.GetComponentsToRemove(node);
			if (componentsToRemove.Count > 0)
			{
				base.ScheduleEvent(new AutoRemoveComponentsEvent(componentsToRemove), node);
			}
		}

		// Token: 0x06008A0F RID: 35343 RVA: 0x00131174 File Offset: 0x0012F374
		[OnEventFire]
		public void RemoveComponents(AutoRemoveComponentsEvent e, Node node)
		{
			List<Type> componentsToRemove = e.ComponentsToRemove;
			componentsToRemove.Sort(new ComponentRemoveOrderComparer());
			Func<Type, bool> <>9__0;
			Func<Type, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (Type item) => node.Entity.HasComponent(item));
			}
			foreach (Type type in componentsToRemove.Where(func))
			{
				node.Entity.RemoveComponent(type);
			}
		}

		// Token: 0x06008A10 RID: 35344 RVA: 0x00131148 File Offset: 0x0012F348
		[OnEventComplete]
		public void RepeatRemoveComponents(AutoRemoveComponentsEvent e, Node node)
		{
			List<Type> componentsToRemove = AutoRemoveComponentsSystem.GetComponentsToRemove(node);
			if (componentsToRemove.Count > 0)
			{
				base.ScheduleEvent(new AutoRemoveComponentsEvent(componentsToRemove), node);
			}
		}

		// Token: 0x06008A11 RID: 35345 RVA: 0x000529BB File Offset: 0x00050BBB
		private static List<Type> GetComponentsToRemove(Node node)
		{
			return ((EntityInternal)node.Entity).ComponentClasses.Where((Type componentType) => AutoRemoveComponentsSystem.registry.IsComponentAutoRemoved(componentType) && componentType != typeof(DeletedEntityComponent)).ToList<Type>();
		}

		// Token: 0x04005E58 RID: 24152
		private static AutoRemoveComponentsRegistry registry;
	}
}
