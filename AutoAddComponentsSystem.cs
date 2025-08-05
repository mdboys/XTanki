using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200284D RID: 10317
	[NullableContext(1)]
	[Nullable(0)]
	public class AutoAddComponentsSystem : ECSSystem
	{
		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x060089FB RID: 35323 RVA: 0x00052922 File Offset: 0x00050B22
		// (set) Token: 0x060089FC RID: 35324 RVA: 0x00052929 File Offset: 0x00050B29
		[Inject]
		private static GroupRegistry GroupRegistry { get; set; }

		// Token: 0x060089FD RID: 35325 RVA: 0x00052931 File Offset: 0x00050B31
		[OnEventFire]
		public void AutoAddComponentsIfNeedOnLoadedEntity(NodeAddedEvent e, SingleNode<SharedEntityComponent> any)
		{
			AutoAddComponentsSystem.AutoAddComponentsIfNeed(any);
		}

		// Token: 0x060089FE RID: 35326 RVA: 0x00052931 File Offset: 0x00050B31
		[OnEventFire]
		public void AutoAddComponentsIfNeedOnNewEntity(NodeAddedEvent e, SingleNode<NewEntityComponent> any)
		{
			AutoAddComponentsSystem.AutoAddComponentsIfNeed(any);
		}

		// Token: 0x060089FF RID: 35327 RVA: 0x00130FAC File Offset: 0x0012F1AC
		private static void AutoAddComponentsIfNeed(Node any)
		{
			EntityInternal entityInternal = (EntityInternal)any.Entity;
			if (!entityInternal.TemplateAccessor.IsPresent())
			{
				return;
			}
			TemplateDescription templateDescription = entityInternal.TemplateAccessor.Get().TemplateDescription;
			AutoAddComponentsSystem.AutoAddComponents(entityInternal, templateDescription);
		}

		// Token: 0x06008A00 RID: 35328 RVA: 0x00130FF4 File Offset: 0x0012F1F4
		private static void AutoAddComponents(EntityInternal newEntity, TemplateDescription templateDescription)
		{
			foreach (Type type in templateDescription.GetAutoAddedComponentTypes())
			{
				Component component;
				if (!typeof(GroupComponent).IsAssignableFrom(type))
				{
					component = newEntity.CreateNewComponentInstance(type);
				}
				else
				{
					Component component2 = AutoAddComponentsSystem.GroupRegistry.FindOrCreateGroup(type, newEntity.Id);
					component = component2;
				}
				Component component3 = component;
				newEntity.AddComponent(component3);
			}
		}
	}
}
