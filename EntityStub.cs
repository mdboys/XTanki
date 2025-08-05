using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002898 RID: 10392
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityStub : EntityInternal, Entity
	{
		// Token: 0x06008B71 RID: 35697 RVA: 0x000539B5 File Offset: 0x00051BB5
		public EntityStub()
		{
			this._node = new Node
			{
				Entity = this
			};
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06008B72 RID: 35698 RVA: 0x000539CF File Offset: 0x00051BCF
		// (set) Token: 0x06008B73 RID: 35699 RVA: 0x000539D6 File Offset: 0x00051BD6
		[Inject]
		private static EngineService EngineService { get; set; }

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06008B74 RID: 35700 RVA: 0x0004B734 File Offset: 0x00049934
		public long Id
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06008B75 RID: 35701 RVA: 0x000539DE File Offset: 0x00051BDE
		// (set) Token: 0x06008B76 RID: 35702 RVA: 0x00005B56 File Offset: 0x00003D56
		public string Name
		{
			get
			{
				return "Stub";
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06008B77 RID: 35703 RVA: 0x00005B56 File Offset: 0x00003D56
		public NodeDescriptionStorage NodeDescriptionStorage
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06008B78 RID: 35704 RVA: 0x00005B56 File Offset: 0x00003D56
		public BitSet ComponentsBitId
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06008B79 RID: 35705 RVA: 0x00005B56 File Offset: 0x00003D56
		public ICollection<Type> ComponentClasses
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06008B7A RID: 35706 RVA: 0x00005B56 File Offset: 0x00003D56
		public ICollection<Component> Components
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06008B7B RID: 35707 RVA: 0x000539E5 File Offset: 0x00051BE5
		// (set) Token: 0x06008B7C RID: 35708 RVA: 0x00005B56 File Offset: 0x00003D56
		[Nullable(new byte[] { 0, 1 })]
		public Optional<TemplateAccessor> TemplateAccessor
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return Optional<Platform.Kernel.ECS.ClientEntitySystem.Impl.TemplateAccessor>.Empty();
			}
			[param: Nullable(new byte[] { 0, 1 })]
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06008B7D RID: 35709 RVA: 0x00005B7A File Offset: 0x00003D7A
		public bool Alive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008B7E RID: 35710 RVA: 0x000539EC File Offset: 0x00051BEC
		public Component CreateNewComponentInstance(Type componentType)
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", componentType));
		}

		// Token: 0x06008B7F RID: 35711 RVA: 0x000539FE File Offset: 0x00051BFE
		public void AddComponent(Component component)
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", component.GetType()));
		}

		// Token: 0x06008B80 RID: 35712 RVA: 0x00005B56 File Offset: 0x00003D56
		public void NotifyComponentChange(Type componentType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008B81 RID: 35713 RVA: 0x00005B56 File Offset: 0x00003D56
		public void AddEntityListener(EntityListener entityListener)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008B82 RID: 35714 RVA: 0x00005B56 File Offset: 0x00003D56
		public void RemoveEntityListener(EntityListener entityListener)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008B83 RID: 35715 RVA: 0x00053A15 File Offset: 0x00051C15
		public bool Contains(NodeDescription node)
		{
			return node.IsEmpty;
		}

		// Token: 0x06008B84 RID: 35716 RVA: 0x00005B56 File Offset: 0x00003D56
		public void AddComponentSilent(Component component)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008B85 RID: 35717 RVA: 0x00005B56 File Offset: 0x00003D56
		public void RemoveComponentSilent(Type componentType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008B86 RID: 35718 RVA: 0x00005B56 File Offset: 0x00003D56
		public void Init()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008B87 RID: 35719 RVA: 0x000539FE File Offset: 0x00051BFE
		public void ChangeComponent(Component component)
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", component.GetType()));
		}

		// Token: 0x06008B88 RID: 35720 RVA: 0x00005B56 File Offset: 0x00003D56
		public void OnDelete()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008B89 RID: 35721 RVA: 0x00007FCD File Offset: 0x000061CD
		public bool IsSameGroup<[Nullable(0)] T>(Entity otherEntity) where T : GroupComponent
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008B8A RID: 35722 RVA: 0x00053A15 File Offset: 0x00051C15
		public bool CanCast(NodeDescription desc)
		{
			return desc.IsEmpty;
		}

		// Token: 0x06008B8B RID: 35723 RVA: 0x00053A1D File Offset: 0x00051C1D
		public Node GetNode(NodeClassInstanceDescription instanceDescription)
		{
			if (!instanceDescription.NodeDescription.IsEmpty)
			{
				throw new NotSupportedException();
			}
			return this._node;
		}

		// Token: 0x06008B8C RID: 35724 RVA: 0x00053A38 File Offset: 0x00051C38
		[NullableContext(0)]
		public void AddComponent<T>() where T : Component, new()
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", typeof(T)));
		}

		// Token: 0x06008B8D RID: 35725 RVA: 0x00053A38 File Offset: 0x00051C38
		[NullableContext(0)]
		public void AddComponentIfAbsent<T>() where T : Component, new()
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", typeof(T)));
		}

		// Token: 0x06008B8E RID: 35726 RVA: 0x000539EC File Offset: 0x00051BEC
		public Component GetComponent(Type componentType)
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", componentType));
		}

		// Token: 0x06008B8F RID: 35727 RVA: 0x0013345C File Offset: 0x0013165C
		public T GetComponent<[Nullable(0)] T>() where T : Component
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", typeof(T)));
		}

		// Token: 0x06008B90 RID: 35728 RVA: 0x000539EC File Offset: 0x00051BEC
		public void RemoveComponent(Type componentType)
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", componentType));
		}

		// Token: 0x06008B91 RID: 35729 RVA: 0x00053A38 File Offset: 0x00051C38
		[NullableContext(0)]
		public void RemoveComponent<T>() where T : Component
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", typeof(T)));
		}

		// Token: 0x06008B92 RID: 35730 RVA: 0x00053A38 File Offset: 0x00051C38
		[NullableContext(0)]
		public void RemoveComponentIfPresent<T>() where T : Component
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", typeof(T)));
		}

		// Token: 0x06008B93 RID: 35731 RVA: 0x00007F86 File Offset: 0x00006186
		[NullableContext(0)]
		public bool HasComponent<T>() where T : Component
		{
			return false;
		}

		// Token: 0x06008B94 RID: 35732 RVA: 0x00007F86 File Offset: 0x00006186
		public bool HasComponent(Type type)
		{
			return false;
		}

		// Token: 0x06008B95 RID: 35733 RVA: 0x0013345C File Offset: 0x0013165C
		public T CreateGroup<[Nullable(0)] T>() where T : GroupComponent
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", typeof(T)));
		}

		// Token: 0x06008B96 RID: 35734 RVA: 0x0013345C File Offset: 0x0013165C
		public T AddComponentAndGetInstance<[Nullable(2)] T>()
		{
			throw new NotSupportedException(string.Format("ComponentType: {0}", typeof(T)));
		}

		// Token: 0x04005EC4 RID: 24260
		private readonly Node _node;
	}
}
