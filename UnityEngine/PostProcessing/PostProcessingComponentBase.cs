using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000220 RID: 544
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PostProcessingComponentBase
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060009BC RID: 2492
		public abstract bool active { get; }

		// Token: 0x060009BD RID: 2493 RVA: 0x00007F86 File Offset: 0x00006186
		public virtual DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.None;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0000568E File Offset: 0x0000388E
		public virtual void OnEnable()
		{
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0000568E File Offset: 0x0000388E
		public virtual void OnDisable()
		{
		}

		// Token: 0x060009C0 RID: 2496
		public abstract PostProcessingModel GetModel();

		// Token: 0x040007B6 RID: 1974
		public PostProcessingContext context;
	}
}
