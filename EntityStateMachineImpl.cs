using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002897 RID: 10391
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityStateMachineImpl : EntityStateMachine
	{
		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06008B66 RID: 35686 RVA: 0x00053979 File Offset: 0x00051B79
		// (set) Token: 0x06008B67 RID: 35687 RVA: 0x00053980 File Offset: 0x00051B80
		[Inject]
		public static EngineServiceInternal EngineService { get; set; }

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06008B68 RID: 35688 RVA: 0x00053988 File Offset: 0x00051B88
		// (set) Token: 0x06008B69 RID: 35689 RVA: 0x0005398F File Offset: 0x00051B8F
		[Inject]
		public static NodeDescriptionRegistry NodeDescriptionRegistry { get; set; }

		// Token: 0x06008B6A RID: 35690 RVA: 0x001331A8 File Offset: 0x001313A8
		[NullableContext(0)]
		public void AddState<T>() where T : Node, new()
		{
			Type typeFromHandle = typeof(T);
			if (this.entityStates.ContainsKey(typeFromHandle))
			{
				throw new EntityStateAlreadyRegisteredException(typeFromHandle);
			}
			EntityState entityState = new EntityState(typeFromHandle, EntityStateMachineImpl.NodeDescriptionRegistry.GetOrCreateNodeClassDescription(typeFromHandle, null).NodeDescription);
			if (this.entity != null)
			{
				entityState.Entity = this.entity;
			}
			this.entityStates[typeFromHandle] = entityState;
		}

		// Token: 0x06008B6B RID: 35691 RVA: 0x00133210 File Offset: 0x00131410
		public T ChangeState<[Nullable(0)] T>() where T : Node
		{
			Type typeFromHandle = typeof(T);
			return (T)((object)this.ChangeState(typeFromHandle));
		}

		// Token: 0x06008B6C RID: 35692 RVA: 0x00133234 File Offset: 0x00131434
		public Node ChangeState(Type stateType)
		{
			EntityState entityState;
			if (!this.entityStates.TryGetValue(stateType, out entityState))
			{
				throw new EntityStateNotRegisteredException(stateType);
			}
			Node node = entityState.Node;
			if (this.currentState == entityState)
			{
				return node;
			}
			this.ClearComponents(node);
			this.EnterState(node);
			this.currentState = entityState;
			return node;
		}

		// Token: 0x06008B6D RID: 35693 RVA: 0x00133280 File Offset: 0x00131480
		public void AttachToEntity(Entity entity)
		{
			this.entity = entity;
			foreach (EntityState entityState in this.entityStates.Values)
			{
				entityState.Entity = entity;
			}
		}

		// Token: 0x06008B6E RID: 35694 RVA: 0x001332D8 File Offset: 0x001314D8
		private void EnterState(Node nextState)
		{
			EntityState entityState = this.entityStates[nextState.GetType()];
			foreach (Type type in entityState.Components)
			{
				if (!this.entity.HasComponent(type))
				{
					Component component = this.entity.CreateNewComponentInstance(type);
					entityState.AssignValue(type, component);
					this.entity.AddComponent(component);
				}
				else
				{
					entityState.AssignValue(type, ((EntityInternal)this.entity).GetComponent(type));
				}
			}
		}

		// Token: 0x06008B6F RID: 35695 RVA: 0x0013337C File Offset: 0x0013157C
		private void ClearComponents(Node nextState)
		{
			ICollection<Type> components = this.entityStates[nextState.GetType()].Components;
			foreach (EntityState entityState in this.entityStates.Values)
			{
				foreach (Type type in entityState.Components)
				{
					if (this.entity.HasComponent(type) && !components.Contains(type) && !this.removedComponents.Contains(type))
					{
						this.entity.RemoveComponent(type);
						this.removedComponents.Add(type);
					}
				}
			}
			this.removedComponents.Clear();
		}

		// Token: 0x04005EBE RID: 24254
		private readonly IDictionary<Type, EntityState> entityStates = new Dictionary<Type, EntityState>();

		// Token: 0x04005EBF RID: 24255
		private readonly HashSet<Type> removedComponents = new HashSet<Type>();

		// Token: 0x04005EC0 RID: 24256
		public EntityState currentState;

		// Token: 0x04005EC1 RID: 24257
		private Entity entity;
	}
}
