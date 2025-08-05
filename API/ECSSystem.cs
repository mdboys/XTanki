using System;
using System.Runtime.CompilerServices;
using log4net;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;
using Platform.Library.ClientLogger.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002959 RID: 10585
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ECSSystem : EngineImpl
	{
		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06008E22 RID: 36386 RVA: 0x000556FE File Offset: 0x000538FE
		// (set) Token: 0x06008E23 RID: 36387 RVA: 0x00055706 File Offset: 0x00053906
		private protected ILog Log { protected get; private set; }

		// Token: 0x06008E24 RID: 36388 RVA: 0x0005570F File Offset: 0x0005390F
		public override void Init(TemplateRegistry templateRegistry, DelayedEventManager delayedEventManager)
		{
			base.Init(templateRegistry, delayedEventManager);
			this.Log = LoggerProvider.GetLogger(this);
		}

		// Token: 0x06008E25 RID: 36389 RVA: 0x00055725 File Offset: 0x00053925
		protected static Entity GetEntityById(long entityId)
		{
			return Flow.Current.EntityRegistry.GetEntity(entityId);
		}
	}
}
