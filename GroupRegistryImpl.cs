using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x020028BA RID: 10426
	[NullableContext(1)]
	[Nullable(0)]
	public class GroupRegistryImpl : GroupRegistry
	{
		// Token: 0x06008BF0 RID: 35824 RVA: 0x00053E49 File Offset: 0x00052049
		public T FindOrCreateGroup<[Nullable(0)] T>(long key) where T : GroupComponent
		{
			return (T)((object)this.FindOrCreateGroup(typeof(T), key));
		}

		// Token: 0x06008BF1 RID: 35825 RVA: 0x00133AB0 File Offset: 0x00131CB0
		public GroupComponent FindOrCreateGroup(Type groupClass, long key)
		{
			if (!this.groups.ContainsKey(groupClass))
			{
				this.groups[groupClass] = new Dictionary<long, GroupComponent>();
			}
			IDictionary<long, GroupComponent> dictionary = this.groups[groupClass];
			if (!dictionary.ContainsKey(key))
			{
				dictionary[key] = GroupRegistryImpl.CreateGroupComponent(groupClass, key);
			}
			return dictionary[key];
		}

		// Token: 0x06008BF2 RID: 35826 RVA: 0x00133B08 File Offset: 0x00131D08
		public GroupComponent FindOrRegisterGroup(GroupComponent groupComponent)
		{
			Type type = groupComponent.GetType();
			long key = groupComponent.Key;
			if (!this.groups.ContainsKey(type))
			{
				this.groups[type] = new Dictionary<long, GroupComponent>();
			}
			IDictionary<long, GroupComponent> dictionary = this.groups[type];
			if (!dictionary.ContainsKey(key))
			{
				dictionary[key] = groupComponent;
			}
			return dictionary[key];
		}

		// Token: 0x06008BF3 RID: 35827 RVA: 0x00133B68 File Offset: 0x00131D68
		private static GroupComponent CreateGroupComponent(Type groupClass, long key)
		{
			ConstructorInfo constructor = groupClass.GetConstructor(new Type[] { typeof(long) });
			if (constructor != null)
			{
				return (GroupComponent)constructor.Invoke(new object[] { key });
			}
			throw new ComponentInstantiatingException(groupClass);
		}

		// Token: 0x04005EE6 RID: 24294
		private readonly IDictionary<Type, IDictionary<long, GroupComponent>> groups = new Dictionary<Type, IDictionary<long, GroupComponent>>();
	}
}
