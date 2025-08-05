using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200295E RID: 10590
	[NullableContext(1)]
	public interface Entity
	{
		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06008E55 RID: 36437
		long Id { get; }

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06008E56 RID: 36438
		// (set) Token: 0x06008E57 RID: 36439
		string Name { get; set; }

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06008E58 RID: 36440
		bool Alive { get; }

		// Token: 0x06008E59 RID: 36441
		Component CreateNewComponentInstance(Type componentType);

		// Token: 0x06008E5A RID: 36442
		void AddComponent(Component component);

		// Token: 0x06008E5B RID: 36443
		[NullableContext(0)]
		void AddComponent<T>() where T : Component, new();

		// Token: 0x06008E5C RID: 36444
		[NullableContext(0)]
		void AddComponentIfAbsent<T>() where T : Component, new();

		// Token: 0x06008E5D RID: 36445
		Component GetComponent(Type componentType);

		// Token: 0x06008E5E RID: 36446
		T GetComponent<[Nullable(0)] T>() where T : Component;

		// Token: 0x06008E5F RID: 36447
		void RemoveComponent(Type componentType);

		// Token: 0x06008E60 RID: 36448
		[NullableContext(0)]
		void RemoveComponent<T>() where T : Component;

		// Token: 0x06008E61 RID: 36449
		[NullableContext(0)]
		void RemoveComponentIfPresent<T>() where T : Component;

		// Token: 0x06008E62 RID: 36450
		[NullableContext(0)]
		bool HasComponent<T>() where T : Component;

		// Token: 0x06008E63 RID: 36451
		bool HasComponent(Type type);

		// Token: 0x06008E64 RID: 36452
		T CreateGroup<[Nullable(0)] T>() where T : GroupComponent;

		// Token: 0x06008E65 RID: 36453
		T AddComponentAndGetInstance<[Nullable(2)] T>();

		// Token: 0x06008E66 RID: 36454
		bool IsSameGroup<[Nullable(0)] T>(Entity otherEntity) where T : GroupComponent;
	}
}
