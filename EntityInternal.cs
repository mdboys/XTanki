using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200288F RID: 10383
	[NullableContext(1)]
	public interface EntityInternal : Entity
	{
		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06008B39 RID: 35641
		ICollection<Type> ComponentClasses { get; }

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06008B3A RID: 35642
		ICollection<Component> Components { get; }

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06008B3B RID: 35643
		// (set) Token: 0x06008B3C RID: 35644
		[Nullable(new byte[] { 0, 1 })]
		Optional<TemplateAccessor> TemplateAccessor
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			set;
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06008B3D RID: 35645
		NodeDescriptionStorage NodeDescriptionStorage { get; }

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06008B3E RID: 35646
		BitSet ComponentsBitId { get; }

		// Token: 0x06008B3F RID: 35647
		void Init();

		// Token: 0x06008B40 RID: 35648
		void OnDelete();

		// Token: 0x06008B41 RID: 35649
		bool CanCast(NodeDescription desc);

		// Token: 0x06008B42 RID: 35650
		Node GetNode(NodeClassInstanceDescription instanceDescription);

		// Token: 0x06008B43 RID: 35651
		void NotifyComponentChange(Type componentType);

		// Token: 0x06008B44 RID: 35652
		void AddEntityListener(EntityListener entityListener);

		// Token: 0x06008B45 RID: 35653
		void RemoveEntityListener(EntityListener entityListener);

		// Token: 0x06008B46 RID: 35654
		bool Contains(NodeDescription node);

		// Token: 0x06008B47 RID: 35655
		void AddComponentSilent(Component component);

		// Token: 0x06008B48 RID: 35656
		void RemoveComponentSilent(Type componentType);
	}
}
