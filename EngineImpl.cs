using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002884 RID: 10372
	[NullableContext(1)]
	[Nullable(0)]
	public class EngineImpl : Engine
	{
		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06008A9B RID: 35483 RVA: 0x00053082 File Offset: 0x00051282
		// (set) Token: 0x06008A9C RID: 35484 RVA: 0x00053089 File Offset: 0x00051289
		[Inject]
		protected static NodeDescriptionRegistry NodeDescriptionRegistry { get; set; }

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06008A9D RID: 35485 RVA: 0x00053091 File Offset: 0x00051291
		// (set) Token: 0x06008A9E RID: 35486 RVA: 0x00053098 File Offset: 0x00051298
		[Inject]
		private static FlowInstancesCache Cache { get; set; }

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06008A9F RID: 35487 RVA: 0x000530A0 File Offset: 0x000512A0
		// (set) Token: 0x06008AA0 RID: 35488 RVA: 0x000530A7 File Offset: 0x000512A7
		[Inject]
		protected static EngineService EngineService { get; set; }

		// Token: 0x06008AA1 RID: 35489 RVA: 0x000530AF File Offset: 0x000512AF
		public Entity CreateEntity(string name)
		{
			return EngineImpl.EngineService.CreateEntityBuilder().SetName(name).Build(true);
		}

		// Token: 0x06008AA2 RID: 35490 RVA: 0x000530C7 File Offset: 0x000512C7
		public Entity CreateEntity<[Nullable(0)] T>() where T : Template
		{
			return EngineImpl.EngineService.CreateEntityBuilder().SetTemplate(typeof(T)).Build(true);
		}

		// Token: 0x06008AA3 RID: 35491 RVA: 0x000530E8 File Offset: 0x000512E8
		public Entity CreateEntity<[Nullable(0)] T>(string configPath) where T : Template
		{
			return EngineImpl.EngineService.CreateEntityBuilder().SetConfig(configPath).SetTemplate(typeof(T))
				.Build(true);
		}

		// Token: 0x06008AA4 RID: 35492 RVA: 0x0005310F File Offset: 0x0005130F
		public Entity CreateEntity(Type templateType, string configPath)
		{
			return EngineImpl.EngineService.CreateEntityBuilder().SetConfig(configPath).SetTemplate(templateType)
				.Build(true);
		}

		// Token: 0x06008AA5 RID: 35493 RVA: 0x0005312D File Offset: 0x0005132D
		public Entity CreateEntity<[Nullable(0)] T>(string configPath, long id) where T : Template
		{
			return EngineImpl.EngineService.CreateEntityBuilder().SetId(id).SetConfig(configPath)
				.SetTemplate(typeof(T))
				.Build(true);
		}

		// Token: 0x06008AA6 RID: 35494 RVA: 0x0005315A File Offset: 0x0005135A
		public Entity CreateEntity(long templateId, string configPath, long id)
		{
			return EngineImpl.EngineService.CreateEntityBuilder().SetId(id).SetTemplate(this._templateRegistry.GetTemplateInfo(templateId))
				.SetConfig(configPath)
				.Build(true);
		}

		// Token: 0x06008AA7 RID: 35495 RVA: 0x00053189 File Offset: 0x00051389
		public Entity CreateEntity(long templateId, string configPath)
		{
			return EngineImpl.EngineService.CreateEntityBuilder().SetTemplate(this._templateRegistry.GetTemplateInfo(templateId)).SetConfig(configPath)
				.Build(true);
		}

		// Token: 0x06008AA8 RID: 35496 RVA: 0x000531B2 File Offset: 0x000513B2
		public void DeleteEntity(Entity entity)
		{
			entity.AddComponent<DeletedEntityComponent>();
			Flow.Current.EntityRegistry.Remove(entity.Id);
			((EntityInternal)entity).OnDelete();
		}

		// Token: 0x06008AA9 RID: 35497 RVA: 0x000531DA File Offset: 0x000513DA
		public EventBuilder NewEvent(Event eventInstance)
		{
			return EngineImpl.Cache.eventBuilder.GetInstance().Init(this._delayedEventManager, Flow.Current, eventInstance);
		}

		// Token: 0x06008AAA RID: 35498 RVA: 0x000531FC File Offset: 0x000513FC
		public EventBuilder NewEvent<[Nullable(0)] T>() where T : Event, new()
		{
			return this.NewEvent(EngineImpl.CreateOrReuseEventInstance<T>());
		}

		// Token: 0x06008AAB RID: 35499 RVA: 0x0005320E File Offset: 0x0005140E
		[NullableContext(0)]
		public void ScheduleEvent<T>() where T : Event, new()
		{
			this.ScheduleEvent<T>(this._fakeEntity);
		}

		// Token: 0x06008AAC RID: 35500 RVA: 0x0005321C File Offset: 0x0005141C
		public void ScheduleEvent<[Nullable(0)] T>(Node node) where T : Event, new()
		{
			this.ScheduleEvent<T>(node.Entity);
		}

		// Token: 0x06008AAD RID: 35501 RVA: 0x0005322A File Offset: 0x0005142A
		public void ScheduleEvent<[Nullable(0)] T>(Entity entity) where T : Event, new()
		{
			this.NewEvent(EngineImpl.CreateOrReuseEventInstance<T>()).Attach(entity).Schedule();
		}

		// Token: 0x06008AAE RID: 35502 RVA: 0x00053247 File Offset: 0x00051447
		public void ScheduleEvent(Event eventInstance, Node node)
		{
			this.ScheduleEvent(eventInstance, node.Entity);
		}

		// Token: 0x06008AAF RID: 35503 RVA: 0x00053256 File Offset: 0x00051456
		public void ScheduleEvent(Event eventInstance, Entity entity)
		{
			this.NewEvent(eventInstance).Attach(entity).Schedule();
		}

		// Token: 0x06008AB0 RID: 35504 RVA: 0x0005326A File Offset: 0x0005146A
		public IList<TN> Select<[Nullable(0)] TN>(Entity entity, Type groupComponentType) where TN : Node
		{
			if (!typeof(GroupComponent).IsAssignableFrom(groupComponentType))
			{
				throw new NotGroupComponentException(groupComponentType);
			}
			return this.DoSelect<TN>(entity, groupComponentType);
		}

		// Token: 0x06008AB1 RID: 35505 RVA: 0x00132448 File Offset: 0x00130648
		public ICollection<TN> SelectAll<[Nullable(0)] TN>() where TN : Node
		{
			NodeClassInstanceDescription nodeDesc = EngineImpl.NodeDescriptionRegistry.GetOrCreateNodeClassDescription(typeof(TN), null);
			return (from e in this.SelectAllEntities<TN>()
				select (TN)((object)((EntityInternal)e).GetNode(nodeDesc))).ToList<TN>();
		}

		// Token: 0x06008AB2 RID: 35506 RVA: 0x00132494 File Offset: 0x00130694
		public ICollection<Entity> SelectAllEntities<[Nullable(0)] TN>() where TN : Node
		{
			NodeClassInstanceDescription orCreateNodeClassDescription = EngineImpl.NodeDescriptionRegistry.GetOrCreateNodeClassDescription(typeof(TN), null);
			return Flow.Current.NodeCollector.GetEntities(orCreateNodeClassDescription.NodeDescription);
		}

		// Token: 0x06008AB3 RID: 35507 RVA: 0x0005328D File Offset: 0x0005148D
		public virtual void Init(TemplateRegistry templateRegistry, DelayedEventManager delayedEventManager)
		{
			this._templateRegistry = templateRegistry;
			this._delayedEventManager = delayedEventManager;
		}

		// Token: 0x06008AB4 RID: 35508 RVA: 0x001324CC File Offset: 0x001306CC
		private static T CreateOrReuseEventInstance<[Nullable(0)] T>() where T : Event, new()
		{
			Event @event;
			if (EngineImpl.EngineService.EventInstancesStorageForReuse.TryGetInstance(typeof(T), out @event))
			{
				return (T)((object)@event);
			}
			return new T();
		}

		// Token: 0x06008AB5 RID: 35509 RVA: 0x0005329D File Offset: 0x0005149D
		protected IList<TN> Select<[Nullable(0)] TN, [Nullable(0)] TG>(Entity entity) where TN : Node where TG : GroupComponent
		{
			return this.DoSelect<TN>(entity, typeof(TG));
		}

		// Token: 0x06008AB6 RID: 35510 RVA: 0x00132504 File Offset: 0x00130704
		private IList<TN> DoSelect<[Nullable(0)] TN>(Entity entity, Type groupComponentType) where TN : Node
		{
			GroupComponent groupComponent;
			if ((groupComponent = (GroupComponent)((EntityUnsafe)entity).GetComponentUnsafe(groupComponentType)) == null)
			{
				return Collections.EmptyList<TN>();
			}
			NodeClassInstanceDescription orCreateNodeClassDescription = EngineImpl.NodeDescriptionRegistry.GetOrCreateNodeClassDescription(typeof(TN), null);
			EngineImpl.NodeDescriptionRegistry.AssertRegister(orCreateNodeClassDescription.NodeDescription);
			ICollection<Entity> groupMembers;
			if ((groupMembers = groupComponent.GetGroupMembers(orCreateNodeClassDescription.NodeDescription)).Count == 0)
			{
				return Collections.EmptyList<TN>();
			}
			if (groupMembers.Count == 1)
			{
				return Collections.SingletonList<TN>((TN)((object)EngineImpl.GetNode(groupMembers.Single<Entity>(), orCreateNodeClassDescription)));
			}
			return (IList<TN>)this.ConvertNodeCollection(orCreateNodeClassDescription, groupMembers);
		}

		// Token: 0x06008AB7 RID: 35511 RVA: 0x0013259C File Offset: 0x0013079C
		private IList ConvertNodeCollection(NodeClassInstanceDescription nodeClassInstanceDescription, ICollection<Entity> entities)
		{
			int count = entities.Count;
			IList genericListInstance = EngineImpl.Cache.GetGenericListInstance(nodeClassInstanceDescription.NodeClass, count);
			foreach (Entity entity in entities)
			{
				Node node = EngineImpl.GetNode(entity, nodeClassInstanceDescription);
				genericListInstance.Add(node);
			}
			return genericListInstance;
		}

		// Token: 0x06008AB8 RID: 35512 RVA: 0x000532B0 File Offset: 0x000514B0
		private static Node GetNode(Entity entity, NodeClassInstanceDescription nodeClassInstanceDescription)
		{
			return ((EntityInternal)entity).GetNode(nodeClassInstanceDescription);
		}

		// Token: 0x04005E85 RID: 24197
		private readonly Entity _fakeEntity = new EntityStub();

		// Token: 0x04005E86 RID: 24198
		private DelayedEventManager _delayedEventManager;

		// Token: 0x04005E87 RID: 24199
		private TemplateRegistry _templateRegistry;
	}
}
