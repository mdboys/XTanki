using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002886 RID: 10374
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EngineServiceImpl : EngineServiceInternal, EngineService
	{
		// Token: 0x06008ABC RID: 35516 RVA: 0x00132608 File Offset: 0x00130808
		public EngineServiceImpl(TemplateRegistry templateRegistry, HandlerCollector handlerCollector, EventMaker eventMaker, ComponentBitIdRegistry componentBitIdRegistry)
		{
			this._templateRegistry = templateRegistry;
			if (!this._instanceFieldsInitialized)
			{
				this.InitializeInstanceFields();
				this._instanceFieldsInitialized = true;
			}
			this.HandlerCollector = handlerCollector;
			this.EventMaker = eventMaker;
			this.BroadcastEventHandlerCollector = new BroadcastEventHandlerCollector(this.HandlerCollector);
			this.HandlerStateListener = new HandlerStateListener(this.HandlerCollector);
			this.DelayedEventManager = new DelayedEventManager(this);
			this.Engine = this.CreateDefaultEngine(this.DelayedEventManager);
			this.SystemRegistry = new SystemRegistry(templateRegistry, this);
			this.ComponentBitIdRegistry = componentBitIdRegistry;
			this._engineDefaultRegistrator.Register();
			this.CollectEmptyEventInstancesForReuse();
			this._flow = new Flow(this);
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06008ABD RID: 35517 RVA: 0x000532E9 File Offset: 0x000514E9
		// (set) Token: 0x06008ABE RID: 35518 RVA: 0x000532F1 File Offset: 0x000514F1
		public bool IsRunning { get; private set; }

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06008ABF RID: 35519 RVA: 0x000532FA File Offset: 0x000514FA
		public ICollection<ComponentConstructor> ComponentConstructors { get; } = new List<ComponentConstructor>();

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06008AC0 RID: 35520 RVA: 0x00053302 File Offset: 0x00051502
		public HandlerCollector HandlerCollector { get; }

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06008AC1 RID: 35521 RVA: 0x0005330A File Offset: 0x0005150A
		public EventMaker EventMaker { get; }

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06008AC2 RID: 35522 RVA: 0x00053312 File Offset: 0x00051512
		public BroadcastEventHandlerCollector BroadcastEventHandlerCollector { get; }

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06008AC3 RID: 35523 RVA: 0x0005331A File Offset: 0x0005151A
		public DelayedEventManager DelayedEventManager { get; }

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06008AC4 RID: 35524 RVA: 0x00053322 File Offset: 0x00051522
		public EntityRegistry EntityRegistry { get; } = new EntityRegistryImpl();

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06008AC5 RID: 35525 RVA: 0x0005332A File Offset: 0x0005152A
		public NodeCollectorImpl NodeCollector { get; } = new NodeCollectorImpl();

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06008AC6 RID: 35526 RVA: 0x00053332 File Offset: 0x00051532
		// (set) Token: 0x06008AC7 RID: 35527 RVA: 0x0005333A File Offset: 0x0005153A
		public Entity EntityStub { get; private set; }

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06008AC8 RID: 35528 RVA: 0x00053343 File Offset: 0x00051543
		public Engine Engine { get; }

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06008AC9 RID: 35529 RVA: 0x0005334B File Offset: 0x0005154B
		public ComponentBitIdRegistry ComponentBitIdRegistry { get; }

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06008ACA RID: 35530 RVA: 0x00053353 File Offset: 0x00051553
		public NodeCache NodeCache { get; } = new NodeCache();

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06008ACB RID: 35531 RVA: 0x0005335B File Offset: 0x0005155B
		public HandlerStateListener HandlerStateListener { get; }

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06008ACC RID: 35532 RVA: 0x00053363 File Offset: 0x00051563
		public HandlerContextDataStorage HandlerContextDataStorage { get; } = new HandlerContextDataStorage();

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06008ACD RID: 35533 RVA: 0x0005336B File Offset: 0x0005156B
		public ICollection<FlowListener> FlowListeners { get; } = new HashSet<FlowListener>();

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06008ACE RID: 35534 RVA: 0x00053373 File Offset: 0x00051573
		public ICollection<ComponentListener> ComponentListeners { get; } = new HashSet<ComponentListener>();

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06008ACF RID: 35535 RVA: 0x0005337B File Offset: 0x0005157B
		public ICollection<EventListener> EventListeners { get; } = new HashSet<EventListener>();

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06008AD0 RID: 35536 RVA: 0x00053383 File Offset: 0x00051583
		public TypeInstancesStorage<Event> EventInstancesStorageForReuse { get; } = new TypeInstancesStorage<Event>();

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06008AD1 RID: 35537 RVA: 0x0005338B File Offset: 0x0005158B
		public SystemRegistry SystemRegistry { get; }

		// Token: 0x06008AD2 RID: 35538 RVA: 0x00053393 File Offset: 0x00051593
		public void RunECSKernel()
		{
			if (this.IsRunning)
			{
				return;
			}
			this.HandlerCollector.SortHandlers();
			this.IsRunning = true;
			this.EntityStub = new EntityStub();
			this.EntityRegistry.RegisterEntity(this.EntityStub);
		}

		// Token: 0x06008AD3 RID: 35539 RVA: 0x000533CC File Offset: 0x000515CC
		public void RegisterTasksForHandler(Type handlerType)
		{
			this.HandlerCollector.RegisterTasksForHandler(handlerType);
		}

		// Token: 0x06008AD4 RID: 35540 RVA: 0x000533DA File Offset: 0x000515DA
		public void RegisterHandlerFactory(HandlerFactory factory)
		{
			this.HandlerCollector.RegisterHandlerFactory(factory);
		}

		// Token: 0x06008AD5 RID: 35541 RVA: 0x000533E8 File Offset: 0x000515E8
		public void RegisterSystem(ECSSystem system)
		{
			this.SystemRegistry.RegisterSystem(system);
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x000533F6 File Offset: 0x000515F6
		public void AddSystemProcessingListener(EngineHandlerRegistrationListener listener)
		{
			this.HandlerCollector.AddHandlerListener(listener);
		}

		// Token: 0x06008AD7 RID: 35543 RVA: 0x00053404 File Offset: 0x00051604
		public Flow GetFlow()
		{
			return this._flow;
		}

		// Token: 0x06008AD8 RID: 35544 RVA: 0x0005340C File Offset: 0x0005160C
		public void RegisterComponentConstructor(ComponentConstructor componentConstructor)
		{
			this.ComponentConstructors.Add(componentConstructor);
		}

		// Token: 0x06008AD9 RID: 35545 RVA: 0x0005341A File Offset: 0x0005161A
		public void RequireInitState()
		{
			if (this.IsRunning)
			{
				throw new RegistrationAfterStartECSException();
			}
		}

		// Token: 0x06008ADA RID: 35546 RVA: 0x0005342A File Offset: 0x0005162A
		public EntityBuilder CreateEntityBuilder()
		{
			return new EntityBuilder(this, this.EntityRegistry, this._templateRegistry);
		}

		// Token: 0x06008ADB RID: 35547 RVA: 0x0005343E File Offset: 0x0005163E
		public void AddFlowListener(FlowListener flowListener)
		{
			this.FlowListeners.Add(flowListener);
		}

		// Token: 0x06008ADC RID: 35548 RVA: 0x0005344C File Offset: 0x0005164C
		public void AddComponentListener(ComponentListener componentListener)
		{
			this.ComponentListeners.Add(componentListener);
		}

		// Token: 0x06008ADD RID: 35549 RVA: 0x0005345A File Offset: 0x0005165A
		public void AddEventListener(EventListener eventListener)
		{
			this.EventListeners.Add(eventListener);
		}

		// Token: 0x06008ADE RID: 35550 RVA: 0x00053468 File Offset: 0x00051668
		private void InitializeInstanceFields()
		{
			this._engineDefaultRegistrator = new EngineDefaultRegistrator(this);
		}

		// Token: 0x06008ADF RID: 35551 RVA: 0x00053476 File Offset: 0x00051676
		private Engine CreateDefaultEngine(DelayedEventManager delayedEventManager)
		{
			EngineImpl engineImpl = new EngineImpl();
			engineImpl.Init(this._templateRegistry, delayedEventManager);
			return engineImpl;
		}

		// Token: 0x06008AE0 RID: 35552 RVA: 0x0013271C File Offset: 0x0013091C
		private void CollectEmptyEventInstancesForReuse()
		{
			foreach (Type type in AssemblyTypeCollector.CollectEmptyEventTypes())
			{
				this.EventInstancesStorageForReuse.AddInstance(type);
			}
		}

		// Token: 0x04005E8C RID: 24204
		private readonly Flow _flow;

		// Token: 0x04005E8D RID: 24205
		private readonly bool _instanceFieldsInitialized;

		// Token: 0x04005E8E RID: 24206
		private readonly TemplateRegistry _templateRegistry;

		// Token: 0x04005E8F RID: 24207
		private EngineDefaultRegistrator _engineDefaultRegistrator;
	}
}
