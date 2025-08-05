using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200288C RID: 10380
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityComponentStorage
	{
		// Token: 0x06008AE7 RID: 35559 RVA: 0x000534B1 File Offset: 0x000516B1
		public EntityComponentStorage(EntityInternal entity, ComponentBitIdRegistry componentBitIdRegistry)
		{
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06008AE8 RID: 35560 RVA: 0x000534DD File Offset: 0x000516DD
		public BitSet BitId { get; } = new BitSet();

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06008AE9 RID: 35561 RVA: 0x000534E5 File Offset: 0x000516E5
		public ICollection<Type> ComponentClasses
		{
			get
			{
				return this._components.Keys;
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06008AEA RID: 35562 RVA: 0x000534F2 File Offset: 0x000516F2
		public ICollection<Component> Components
		{
			get
			{
				return this._components.Values;
			}
		}

		// Token: 0x06008AEB RID: 35563 RVA: 0x00132770 File Offset: 0x00130970
		public void AddComponentImmediately(Type comType, Component component)
		{
			try
			{
				this._components.Add(comType, component);
				this.BitId.Set(this.<componentBitIdRegistry>P.GetComponentBitId(comType));
			}
			catch (ArgumentException)
			{
				throw new ComponentAlreadyExistsInEntityException(this.<entity>P, comType);
			}
		}

		// Token: 0x06008AEC RID: 35564 RVA: 0x000534FF File Offset: 0x000516FF
		public bool HasComponent(Type componentClass)
		{
			return this._components.ContainsKey(componentClass);
		}

		// Token: 0x06008AED RID: 35565 RVA: 0x001327C4 File Offset: 0x001309C4
		public Component GetComponent(Type componentClass)
		{
			Component component;
			try
			{
				component = this._components[componentClass];
			}
			catch (KeyNotFoundException)
			{
				throw new ComponentNotFoundException(this.<entity>P, componentClass);
			}
			return component;
		}

		// Token: 0x06008AEE RID: 35566 RVA: 0x00132800 File Offset: 0x00130A00
		[return: Nullable(2)]
		public Component GetComponentUnsafe(Type componentType)
		{
			Component component;
			if (!this._components.TryGetValue(componentType, out component))
			{
				return null;
			}
			return component;
		}

		// Token: 0x06008AEF RID: 35567 RVA: 0x00132820 File Offset: 0x00130A20
		public void ChangeComponent(Component component)
		{
			Type type = component.GetType();
			this.AssertComponentFound(type);
			this._components[type] = component;
		}

		// Token: 0x06008AF0 RID: 35568 RVA: 0x00132848 File Offset: 0x00130A48
		public Component RemoveComponentImmediately(Type componentClass)
		{
			Component component2;
			try
			{
				Component component = this._components[componentClass];
				this._components.Remove(componentClass);
				this.BitId.Clear(this.<componentBitIdRegistry>P.GetComponentBitId(componentClass));
				component2 = component;
			}
			catch (KeyNotFoundException)
			{
				throw new ComponentNotFoundException(this.<entity>P, componentClass);
			}
			return component2;
		}

		// Token: 0x06008AF1 RID: 35569 RVA: 0x0005350D File Offset: 0x0005170D
		private void AssertComponentFound(Type componentClass)
		{
			if (!this._components.ContainsKey(componentClass))
			{
				throw new ComponentNotFoundException(this.<entity>P, componentClass);
			}
		}

		// Token: 0x06008AF2 RID: 35570 RVA: 0x0005352A File Offset: 0x0005172A
		public void OnEntityDelete()
		{
			this._components.Clear();
			this.BitId.Clear();
		}

		// Token: 0x04005EA3 RID: 24227
		[CompilerGenerated]
		private EntityInternal <entity>P = entity;

		// Token: 0x04005EA4 RID: 24228
		[CompilerGenerated]
		private ComponentBitIdRegistry <componentBitIdRegistry>P = componentBitIdRegistry;

		// Token: 0x04005EA5 RID: 24229
		private readonly IDictionary<Type, Component> _components = new Dictionary<Type, Component>();
	}
}
