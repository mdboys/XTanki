using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200295D RID: 10589
	[NullableContext(1)]
	public interface EngineServiceInternal : EngineService
	{
		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06008E41 RID: 36417
		SystemRegistry SystemRegistry { get; }

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06008E42 RID: 36418
		bool IsRunning { get; }

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06008E43 RID: 36419
		HandlerCollector HandlerCollector { get; }

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06008E44 RID: 36420
		EventMaker EventMaker { get; }

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06008E45 RID: 36421
		BroadcastEventHandlerCollector BroadcastEventHandlerCollector { get; }

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06008E46 RID: 36422
		HandlerStateListener HandlerStateListener { get; }

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06008E47 RID: 36423
		DelayedEventManager DelayedEventManager { get; }

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06008E48 RID: 36424
		Entity EntityStub { get; }

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06008E49 RID: 36425
		ICollection<ComponentConstructor> ComponentConstructors { get; }

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06008E4A RID: 36426
		EntityRegistry EntityRegistry { get; }

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06008E4B RID: 36427
		NodeCollectorImpl NodeCollector { get; }

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06008E4C RID: 36428
		ComponentBitIdRegistry ComponentBitIdRegistry { get; }

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06008E4D RID: 36429
		NodeCache NodeCache { get; }

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06008E4E RID: 36430
		HandlerContextDataStorage HandlerContextDataStorage { get; }

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06008E4F RID: 36431
		ICollection<FlowListener> FlowListeners { get; }

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06008E50 RID: 36432
		ICollection<ComponentListener> ComponentListeners { get; }

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06008E51 RID: 36433
		ICollection<EventListener> EventListeners { get; }

		// Token: 0x06008E52 RID: 36434
		void RunECSKernel();

		// Token: 0x06008E53 RID: 36435
		Flow GetFlow();

		// Token: 0x06008E54 RID: 36436
		void RequireInitState();
	}
}
