using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002883 RID: 10371
	[NullableContext(1)]
	[Nullable(0)]
	public class EngineDefaultRegistrator
	{
		// Token: 0x06008A95 RID: 35477 RVA: 0x0005301A File Offset: 0x0005121A
		public EngineDefaultRegistrator(EngineServiceImpl engineServiceImpl)
		{
		}

		// Token: 0x06008A96 RID: 35478 RVA: 0x00053029 File Offset: 0x00051229
		private void RegisterComponentConstructor()
		{
			this.<engineServiceImpl>P.RegisterComponentConstructor(new ConfigComponentConstructor());
		}

		// Token: 0x06008A97 RID: 35479 RVA: 0x0005303B File Offset: 0x0005123B
		private void RegisterSystems()
		{
			this.<engineServiceImpl>P.RegisterSystem(new AutoAddComponentsSystem());
			this.<engineServiceImpl>P.RegisterSystem(new AutoRemoveComponentsSystem(new AutoRemoveComponentsRegistryImpl(this.<engineServiceImpl>P)));
		}

		// Token: 0x06008A98 RID: 35480 RVA: 0x001320D4 File Offset: 0x001302D4
		private void RegisterTasks()
		{
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(TimeUpdateFireHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(TimeUpdateCompleteHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(EarlyUpdateFireHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(EarlyUpdateCompleteHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(UpdateEventFireHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(UpdateEventCompleteHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(FixedUpdateEventFireHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(FixedUpdateEventCompleteHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(AfterFixedUpdateEventFireHandler));
			this.<engineServiceImpl>P.BroadcastEventHandlerCollector.Register(typeof(AfterFixedUpdateEventCompleteHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(TimeUpdateFireHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(TimeUpdateCompleteHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(EarlyUpdateFireHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(EarlyUpdateCompleteHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(UpdateEventFireHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(UpdateEventCompleteHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(FixedUpdateEventFireHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(FixedUpdateEventCompleteHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(AfterFixedUpdateEventFireHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(AfterFixedUpdateEventCompleteHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(NodeAddedFireHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(NodeAddedCompleteHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(NodeRemovedFireHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(NodeRemovedCompleteHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(EventFireHandler));
			this.<engineServiceImpl>P.RegisterTasksForHandler(typeof(EventCompleteHandler));
		}

		// Token: 0x06008A99 RID: 35481 RVA: 0x00132338 File Offset: 0x00130538
		private void RegisterHandlerFactory()
		{
			this.<engineServiceImpl>P.RegisterHandlerFactory(new TimeUpdateFireHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new TimeUpdateCompleteHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new EarlyUpdateFireHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new EarlyUpdateCompleteHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new UpdateEventFireHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new UpdateEventCompleteHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new FixedUpdateEventFireHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new FixedUpdateEventCompleteHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new AfterFixedUpdateEventFireHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new AfterFixedUpdateEventCompleteHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new NodeAddedFireHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new NodeAddedCompleteHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new NodeRemovedFireHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new NodeRemovedCompleteHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new EventFireHandlerFactory());
			this.<engineServiceImpl>P.RegisterHandlerFactory(new EventCompleteHandlerFactory());
		}

		// Token: 0x06008A9A RID: 35482 RVA: 0x00053068 File Offset: 0x00051268
		public void Register()
		{
			this.RegisterComponentConstructor();
			this.RegisterHandlerFactory();
			this.RegisterTasks();
			this.RegisterSystems();
		}

		// Token: 0x04005E84 RID: 24196
		[CompilerGenerated]
		private EngineServiceImpl <engineServiceImpl>P = engineServiceImpl;
	}
}
