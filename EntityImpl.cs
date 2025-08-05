using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200288D RID: 10381
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityImpl : EntityInternal, Entity, EntityUnsafe, IComparable<EntityImpl>
	{
		// Token: 0x06008AF3 RID: 35571 RVA: 0x001328A8 File Offset: 0x00130AA8
		public EntityImpl(EngineServiceInternal engineService, long id, string name, [Nullable(new byte[] { 0, 1 })] Optional<TemplateAccessor> templateAccessor)
		{
			this.Id = id;
			this.Name = name;
			this.TemplateAccessor = templateAccessor;
			this.NodeDescriptionStorage = new NodeDescriptionStorage();
			this._storage = new EntityComponentStorage(this, engineService.ComponentBitIdRegistry);
			this._nodeAddedEventMaker = new NodeChangedEventMaker(NodeAddedEvent.Instance, typeof(NodeAddedFireHandler), typeof(NodeAddedCompleteHandler), engineService.HandlerCollector);
			this._nodeRemoveEventMaker = new NodeChangedEventMaker(NodeRemoveEvent.Instance, typeof(NodeRemovedFireHandler), typeof(NodeRemovedCompleteHandler), engineService.HandlerCollector);
			this._engineService = engineService;
			this._nodeProvider = new NodeProvider(this);
			this.Init();
			this.UpdateNodes(EntityImpl.NodeDescriptionRegistry.GetNodeDescriptionsWithNotComponentsOnly());
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06008AF4 RID: 35572 RVA: 0x00053542 File Offset: 0x00051742
		// (set) Token: 0x06008AF5 RID: 35573 RVA: 0x00053549 File Offset: 0x00051749
		[Inject]
		private static NodeDescriptionRegistry NodeDescriptionRegistry { get; set; }

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06008AF6 RID: 35574 RVA: 0x00053551 File Offset: 0x00051751
		// (set) Token: 0x06008AF7 RID: 35575 RVA: 0x00053558 File Offset: 0x00051758
		[Inject]
		private static GroupRegistry GroupRegistry { get; set; }

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06008AF8 RID: 35576 RVA: 0x00053560 File Offset: 0x00051760
		public long Id { get; }

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06008AF9 RID: 35577 RVA: 0x00053568 File Offset: 0x00051768
		// (set) Token: 0x06008AFA RID: 35578 RVA: 0x00053570 File Offset: 0x00051770
		public string Name { get; set; }

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06008AFB RID: 35579 RVA: 0x00053579 File Offset: 0x00051779
		// (set) Token: 0x06008AFC RID: 35580 RVA: 0x00053581 File Offset: 0x00051781
		[Nullable(new byte[] { 0, 1 })]
		public Optional<TemplateAccessor> TemplateAccessor
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			set;
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06008AFD RID: 35581 RVA: 0x0005358A File Offset: 0x0005178A
		// (set) Token: 0x06008AFE RID: 35582 RVA: 0x00053592 File Offset: 0x00051792
		public bool Alive { get; private set; }

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06008AFF RID: 35583 RVA: 0x0005359B File Offset: 0x0005179B
		public ICollection<Component> Components
		{
			get
			{
				return this._storage.Components;
			}
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06008B00 RID: 35584 RVA: 0x000535A8 File Offset: 0x000517A8
		public ICollection<Type> ComponentClasses
		{
			get
			{
				return this._storage.ComponentClasses;
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06008B01 RID: 35585 RVA: 0x000535B5 File Offset: 0x000517B5
		public NodeDescriptionStorage NodeDescriptionStorage { get; }

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06008B02 RID: 35586 RVA: 0x000535BD File Offset: 0x000517BD
		public BitSet ComponentsBitId
		{
			get
			{
				return this._storage.BitId;
			}
		}

		// Token: 0x06008B03 RID: 35587 RVA: 0x0013296C File Offset: 0x00130B6C
		public void Init()
		{
			this.Alive = true;
			this._entityListeners = new List<EntityListener>
			{
				this._engineService.HandlerContextDataStorage,
				this._engineService.HandlerStateListener,
				this._engineService.BroadcastEventHandlerCollector
			};
		}

		// Token: 0x06008B04 RID: 35588 RVA: 0x000535CA File Offset: 0x000517CA
		[NullableContext(0)]
		public void AddComponent<T>() where T : Component, new()
		{
			this.AddComponent(typeof(T));
		}

		// Token: 0x06008B05 RID: 35589 RVA: 0x000535DC File Offset: 0x000517DC
		[NullableContext(0)]
		public void AddComponentIfAbsent<T>() where T : Component, new()
		{
			if (!this.HasComponent<T>())
			{
				this.AddComponent(typeof(T));
			}
		}

		// Token: 0x06008B06 RID: 35590 RVA: 0x001329C0 File Offset: 0x00130BC0
		private void AddComponent(Type componentType)
		{
			Component component = this.CreateNewComponentInstance(componentType);
			this.AddComponent(component);
		}

		// Token: 0x06008B07 RID: 35591 RVA: 0x001329DC File Offset: 0x00130BDC
		public void AddComponent(Component component)
		{
			GroupComponent groupComponent = component as GroupComponent;
			if (groupComponent != null)
			{
				component = EntityImpl.GroupRegistry.FindOrRegisterGroup(groupComponent);
			}
			this.AddComponent(component, true);
		}

		// Token: 0x06008B08 RID: 35592 RVA: 0x000535F6 File Offset: 0x000517F6
		public void AddComponentSilent(Component component)
		{
			this.AddComponent(component, false);
		}

		// Token: 0x06008B09 RID: 35593 RVA: 0x00132A08 File Offset: 0x00130C08
		public void ChangeComponent(Component component)
		{
			bool flag = this.HasComponent(component.GetType()) && this.GetComponent(component.GetType()).Equals(component);
			this._storage.ChangeComponent(component);
			if (!flag)
			{
				this._nodeProvider.OnComponentChanged(component);
			}
			this.NotifyChangedInEntity(component);
		}

		// Token: 0x06008B0A RID: 35594 RVA: 0x00053600 File Offset: 0x00051800
		public Component GetComponent(Type componentType)
		{
			return this._storage.GetComponent(componentType);
		}

		// Token: 0x06008B0B RID: 35595 RVA: 0x0005360E File Offset: 0x0005180E
		public T GetComponent<[Nullable(0)] T>() where T : Component
		{
			return (T)((object)this.GetComponent(typeof(T)));
		}

		// Token: 0x06008B0C RID: 35596 RVA: 0x00053625 File Offset: 0x00051825
		public void OnDelete()
		{
			this.Alive = false;
			this.ClearNodes();
			this.SendEntityDeletedForAllListeners();
			this._storage.OnEntityDelete();
		}

		// Token: 0x06008B0D RID: 35597 RVA: 0x00053645 File Offset: 0x00051845
		public bool CanCast(NodeDescription desc)
		{
			return this._nodeProvider.CanCast(desc);
		}

		// Token: 0x06008B0E RID: 35598 RVA: 0x00053653 File Offset: 0x00051853
		public Node GetNode(NodeClassInstanceDescription instanceDescription)
		{
			return this._nodeProvider.GetNode(instanceDescription);
		}

		// Token: 0x06008B0F RID: 35599 RVA: 0x00132A5C File Offset: 0x00130C5C
		public void NotifyComponentChange(Type componentType)
		{
			Component component = this.GetComponent(componentType);
			foreach (ComponentListener componentListener in this._engineService.ComponentListeners)
			{
				componentListener.OnComponentChanged(this, component);
			}
		}

		// Token: 0x06008B10 RID: 35600 RVA: 0x00053661 File Offset: 0x00051861
		[NullableContext(0)]
		public void RemoveComponent<T>() where T : Component
		{
			this.RemoveComponent(typeof(T));
		}

		// Token: 0x06008B11 RID: 35601 RVA: 0x00053673 File Offset: 0x00051873
		[NullableContext(0)]
		public void RemoveComponentIfPresent<T>() where T : Component
		{
			if (this.HasComponent<T>())
			{
				this.RemoveComponent(typeof(T));
			}
		}

		// Token: 0x06008B12 RID: 35602 RVA: 0x0005368D File Offset: 0x0005188D
		public void RemoveComponent(Type componentType)
		{
			this.UpdateHandlers(componentType);
			this.NotifyComponentRemove(componentType);
			this.RemoveComponentSilent(componentType);
		}

		// Token: 0x06008B13 RID: 35603 RVA: 0x00132AB8 File Offset: 0x00130CB8
		public void RemoveComponentSilent(Type componentType)
		{
			if (!this.HasComponent(componentType) && (this.HasComponent<DeletedEntityComponent>() || EntityImpl.IsSkipExceptionOnAddRemove(componentType)))
			{
				return;
			}
			this.SendNodeRemoved(componentType);
			Component component = this._storage.RemoveComponentImmediately(componentType);
			this.NotifyDetachFromEntity(component);
			this.UpdateNodesOnComponentRemoved(componentType);
		}

		// Token: 0x06008B14 RID: 35604 RVA: 0x000536A4 File Offset: 0x000518A4
		[NullableContext(0)]
		public bool HasComponent<T>() where T : Component
		{
			return this.HasComponent(typeof(T));
		}

		// Token: 0x06008B15 RID: 35605 RVA: 0x000536B6 File Offset: 0x000518B6
		public bool HasComponent(Type type)
		{
			return this._storage.HasComponent(type);
		}

		// Token: 0x06008B16 RID: 35606 RVA: 0x00132B04 File Offset: 0x00130D04
		public T CreateGroup<[Nullable(0)] T>() where T : GroupComponent
		{
			T t = EntityImpl.GroupRegistry.FindOrCreateGroup<T>(this.Id);
			this.AddComponent(t);
			return t;
		}

		// Token: 0x06008B17 RID: 35607 RVA: 0x00132B30 File Offset: 0x00130D30
		public T AddComponentAndGetInstance<[Nullable(2)] T>()
		{
			Component component = this.CreateNewComponentInstance(typeof(T));
			this.AddComponent(component);
			return (T)((object)component);
		}

		// Token: 0x06008B18 RID: 35608 RVA: 0x00132B5C File Offset: 0x00130D5C
		public Component CreateNewComponentInstance(Type componentType)
		{
			foreach (ComponentConstructor componentConstructor in this._engineService.ComponentConstructors)
			{
				if (componentConstructor.IsAcceptable(componentType, this))
				{
					return componentConstructor.GetComponentInstance(componentType, this);
				}
			}
			return (Component)componentType.GetConstructor(new Type[0]).Invoke(Collections.EmptyArray);
		}

		// Token: 0x06008B19 RID: 35609 RVA: 0x000536C4 File Offset: 0x000518C4
		public bool Contains(NodeDescription node)
		{
			return this.ComponentsBitId.Mask(node.NodeComponentBitId) && this.ComponentsBitId.MaskNot(node.NotNodeComponentBitId);
		}

		// Token: 0x06008B1A RID: 35610 RVA: 0x00132BDC File Offset: 0x00130DDC
		public bool IsSameGroup<[Nullable(0)] T>(Entity otherEntity) where T : GroupComponent
		{
			return this.HasComponent<T>() && otherEntity.HasComponent<T>() && this.GetComponent<T>().Key.Equals(otherEntity.GetComponent<T>().Key);
		}

		// Token: 0x06008B1B RID: 35611 RVA: 0x000536EC File Offset: 0x000518EC
		public void AddEntityListener(EntityListener entityListener)
		{
			this._entityListeners.Add(entityListener);
		}

		// Token: 0x06008B1C RID: 35612 RVA: 0x000536FA File Offset: 0x000518FA
		public void RemoveEntityListener(EntityListener entityListener)
		{
			this._entityListeners.Remove(entityListener);
		}

		// Token: 0x06008B1D RID: 35613 RVA: 0x00053709 File Offset: 0x00051909
		[return: Nullable(2)]
		public Component GetComponentUnsafe(Type componentType)
		{
			return this._storage.GetComponentUnsafe(componentType);
		}

		// Token: 0x06008B1E RID: 35614 RVA: 0x00053717 File Offset: 0x00051917
		public int CompareTo(EntityImpl other)
		{
			return (int)(this.Id - other.Id);
		}

		// Token: 0x06008B1F RID: 35615 RVA: 0x00132C24 File Offset: 0x00130E24
		private void AddComponent(Component component, bool sendEvent)
		{
			Type type = component.GetType();
			if (this._storage.HasComponent(type) && EntityImpl.IsSkipExceptionOnAddRemove(type))
			{
				return;
			}
			this.UpdateHandlers(type);
			this.NotifyAttachToEntity(component);
			this._storage.AddComponentImmediately(type, component);
			this.MakeNodes(type, component);
			if (sendEvent)
			{
				this.NotifyAddComponent(component);
			}
			this.PrepareAndSendNodeAddedEvent(component);
		}

		// Token: 0x06008B20 RID: 35616 RVA: 0x00132C84 File Offset: 0x00130E84
		private void NotifyAddComponent(Component component)
		{
			foreach (ComponentListener componentListener in this._engineService.ComponentListeners)
			{
				componentListener.OnComponentAdded(this, component);
			}
		}

		// Token: 0x06008B21 RID: 35617 RVA: 0x00132CD8 File Offset: 0x00130ED8
		private void UpdateHandlers(Type componentType)
		{
			if (componentType.IsSubclassOf(typeof(GroupComponent)))
			{
				this._engineService.HandlerCollector.GetHandlersByGroupComponent(componentType).ForEach(delegate(Handler h)
				{
					h.ChangeVersion();
				});
			}
		}

		// Token: 0x06008B22 RID: 35618 RVA: 0x00053727 File Offset: 0x00051927
		private void PrepareAndSendNodeAddedEvent(Component component)
		{
			this._nodeAddedEventMaker.MakeIfNeed(this, component.GetType());
		}

		// Token: 0x06008B23 RID: 35619 RVA: 0x00132D2C File Offset: 0x00130F2C
		private void NotifyAttachToEntity(Component component)
		{
			AttachToEntityListener attachToEntityListener = component as AttachToEntityListener;
			if (attachToEntityListener != null)
			{
				attachToEntityListener.AttachedToEntity(this);
			}
		}

		// Token: 0x06008B24 RID: 35620 RVA: 0x00132D4C File Offset: 0x00130F4C
		private void NotifyChangedInEntity(Component component)
		{
			ComponentServerChangeListener componentServerChangeListener = component as ComponentServerChangeListener;
			if (componentServerChangeListener != null)
			{
				componentServerChangeListener.ChangedOnServer(this);
			}
		}

		// Token: 0x06008B25 RID: 35621 RVA: 0x00132D6C File Offset: 0x00130F6C
		private void SendEntityDeletedForAllListeners()
		{
			foreach (EntityListener entityListener in this._entityListeners)
			{
				entityListener.OnEntityDeleted(this);
			}
			this._entityListeners.Clear();
		}

		// Token: 0x06008B26 RID: 35622 RVA: 0x00132DC4 File Offset: 0x00130FC4
		private void NotifyComponentRemove(Type componentType)
		{
			Component component = this._storage.GetComponent(componentType);
			foreach (ComponentListener componentListener in this._engineService.ComponentListeners)
			{
				componentListener.OnComponentRemoved(this, component);
			}
		}

		// Token: 0x06008B27 RID: 35623 RVA: 0x0005373B File Offset: 0x0005193B
		private static bool IsSkipExceptionOnAddRemove(Type componentType)
		{
			return componentType.IsDefined(typeof(SkipExceptionOnAddRemoveAttribute), true);
		}

		// Token: 0x06008B28 RID: 35624 RVA: 0x0005374E File Offset: 0x0005194E
		private void SendNodeRemoved(Type componentType)
		{
			this._nodeRemoveEventMaker.MakeIfNeed(this, componentType);
		}

		// Token: 0x06008B29 RID: 35625 RVA: 0x00132E24 File Offset: 0x00131024
		private void NotifyDetachFromEntity(Component component)
		{
			DetachFromEntityListener detachFromEntityListener = component as DetachFromEntityListener;
			if (detachFromEntityListener != null)
			{
				detachFromEntityListener.DetachedFromEntity(this);
			}
		}

		// Token: 0x06008B2A RID: 35626 RVA: 0x00132E44 File Offset: 0x00131044
		private void MakeNodes(Type componentType, Component component)
		{
			this._nodeProvider.OnComponentAdded(component, componentType);
			NodesToChange nodesToChange = NodeCache.GetNodesToChange(this, componentType);
			foreach (NodeDescription nodeDescription in nodesToChange.NodesToAdd)
			{
				this.AddNode(nodeDescription);
			}
			foreach (NodeDescription nodeDescription2 in nodesToChange.NodesToRemove)
			{
				this.RemoveNode(nodeDescription2);
			}
		}

		// Token: 0x06008B2B RID: 35627 RVA: 0x00132EE4 File Offset: 0x001310E4
		private void UpdateNodes(ICollection<NodeDescription> nodes)
		{
			BitSet componentsBitId = this.ComponentsBitId;
			foreach (NodeDescription nodeDescription in nodes)
			{
				if (componentsBitId.Mask(nodeDescription.NodeComponentBitId))
				{
					if (componentsBitId.MaskNot(nodeDescription.NotNodeComponentBitId))
					{
						this.AddNode(nodeDescription);
					}
					else if (this.NodeDescriptionStorage.Contains(nodeDescription))
					{
						this.RemoveNode(nodeDescription);
					}
				}
			}
		}

		// Token: 0x06008B2C RID: 35628 RVA: 0x0005375D File Offset: 0x0005195D
		private void AddNode(NodeDescription node)
		{
			Flow.Current.NodeCollector.Attach(this, node);
			this.NodeDescriptionStorage.AddNode(node);
			this.SendNodeAddedForCollectors(node);
		}

		// Token: 0x06008B2D RID: 35629 RVA: 0x00053783 File Offset: 0x00051983
		private void UpdateNodesOnComponentRemoved(Type componentClass)
		{
			this.NodeDescriptionStorage.GetNodeDescriptions(componentClass).ToList<NodeDescription>().ForEach(new Action<NodeDescription>(this.RemoveNode));
			this.UpdateNodes(EntityImpl.NodeDescriptionRegistry.GetNodeDescriptionsByNotComponent(componentClass));
		}

		// Token: 0x06008B2E RID: 35630 RVA: 0x000537B8 File Offset: 0x000519B8
		private void ClearNodes()
		{
			this.NodeDescriptionStorage.GetNodeDescriptions().ToList<NodeDescription>().ForEach(new Action<NodeDescription>(this.RemoveNode));
			this._nodeProvider.CleanNodes();
		}

		// Token: 0x06008B2F RID: 35631 RVA: 0x000537E6 File Offset: 0x000519E6
		private void RemoveNode(NodeDescription node)
		{
			this.SendNodeRemovedForCollectors(node);
			Flow.Current.NodeCollector.Detach(this, node);
			this.NodeDescriptionStorage.RemoveNode(node);
		}

		// Token: 0x06008B30 RID: 35632 RVA: 0x00132F68 File Offset: 0x00131168
		private void SendNodeAddedForCollectors(NodeDescription nodeDescription)
		{
			foreach (EntityListener entityListener in this._entityListeners)
			{
				entityListener.OnNodeAdded(this, nodeDescription);
			}
		}

		// Token: 0x06008B31 RID: 35633 RVA: 0x00132FB4 File Offset: 0x001311B4
		private void SendNodeRemovedForCollectors(NodeDescription nodeDescription)
		{
			foreach (EntityListener entityListener in this._entityListeners)
			{
				entityListener.OnNodeRemoved(this, nodeDescription);
			}
		}

		// Token: 0x06008B32 RID: 35634 RVA: 0x0005380C File Offset: 0x00051A0C
		private bool Equals(EntityImpl other)
		{
			return this.Id == other.Id;
		}

		// Token: 0x06008B33 RID: 35635 RVA: 0x00133000 File Offset: 0x00131200
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			Node node = obj as Node;
			if (node != null)
			{
				obj = node.Entity;
			}
			EntityImpl entityImpl = obj as EntityImpl;
			return entityImpl != null && this.Equals(entityImpl);
		}

		// Token: 0x06008B34 RID: 35636 RVA: 0x00133040 File Offset: 0x00131240
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x06008B35 RID: 35637 RVA: 0x0005381C File Offset: 0x00051A1C
		public override string ToString()
		{
			return string.Format("{0}({1})", this.Id, this.Name);
		}

		// Token: 0x04005EA7 RID: 24231
		private readonly EntityComponentStorage _storage;

		// Token: 0x04005EA8 RID: 24232
		private readonly NodeChangedEventMaker _nodeAddedEventMaker;

		// Token: 0x04005EA9 RID: 24233
		private readonly NodeChangedEventMaker _nodeRemoveEventMaker;

		// Token: 0x04005EAA RID: 24234
		private readonly EngineServiceInternal _engineService;

		// Token: 0x04005EAB RID: 24235
		private readonly NodeProvider _nodeProvider;

		// Token: 0x04005EAC RID: 24236
		private ICollection<EntityListener> _entityListeners;
	}
}
