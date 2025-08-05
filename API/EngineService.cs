using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200295C RID: 10588
	[NullableContext(1)]
	public interface EngineService
	{
		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06008E39 RID: 36409
		Engine Engine { get; }

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06008E3A RID: 36410
		TypeInstancesStorage<Event> EventInstancesStorageForReuse { get; }

		// Token: 0x06008E3B RID: 36411
		void RegisterSystem(ECSSystem system);

		// Token: 0x06008E3C RID: 36412
		void AddSystemProcessingListener(EngineHandlerRegistrationListener listener);

		// Token: 0x06008E3D RID: 36413
		EntityBuilder CreateEntityBuilder();

		// Token: 0x06008E3E RID: 36414
		void AddFlowListener(FlowListener flowListener);

		// Token: 0x06008E3F RID: 36415
		void AddComponentListener(ComponentListener componentListener);

		// Token: 0x06008E40 RID: 36416
		void AddEventListener(EventListener eventListener);
	}
}
