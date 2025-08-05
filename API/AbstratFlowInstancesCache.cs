using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientDataStructures.API;
using Platform.Library.ClientDataStructures.Impl.Cache;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002943 RID: 10563
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AbstratFlowInstancesCache : FlowListener
	{
		// Token: 0x06008DF9 RID: 36345 RVA: 0x00055646 File Offset: 0x00053846
		public AbstratFlowInstancesCache()
		{
			AbstratFlowInstancesCache.EngineService.AddFlowListener(this);
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06008DFA RID: 36346 RVA: 0x00055664 File Offset: 0x00053864
		// (set) Token: 0x06008DFB RID: 36347 RVA: 0x0005566B File Offset: 0x0005386B
		[Inject]
		private static EngineService EngineService { get; set; }

		// Token: 0x06008DFC RID: 36348 RVA: 0x0000568E File Offset: 0x0000388E
		public void OnFlowFinish()
		{
		}

		// Token: 0x06008DFD RID: 36349 RVA: 0x00055673 File Offset: 0x00053873
		public virtual void OnFlowClean()
		{
			this._caches.ForEach(delegate(AbstractCache c)
			{
				c.FreeAll();
			});
		}

		// Token: 0x06008DFE RID: 36350 RVA: 0x00137140 File Offset: 0x00135340
		protected Cache<T> Register<[Nullable(2)] T>()
		{
			CacheImpl<T> cacheImpl = new CacheImpl<T>();
			this._caches.Add(cacheImpl);
			return cacheImpl;
		}

		// Token: 0x06008DFF RID: 36351 RVA: 0x00137160 File Offset: 0x00135360
		protected Cache<T> Register<[Nullable(2)] T>(Action<T> cleaner)
		{
			CacheImpl<T> cacheImpl = new CacheImpl<T>(cleaner);
			this._caches.Add(cacheImpl);
			return cacheImpl;
		}

		// Token: 0x04005FBA RID: 24506
		private readonly List<AbstractCache> _caches = new List<AbstractCache>();
	}
}
