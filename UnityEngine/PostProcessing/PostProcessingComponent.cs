using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200021F RID: 543
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PostProcessingComponent<[Nullable(0)] T> : PostProcessingComponentBase where T : PostProcessingModel
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0000B7A4 File Offset: 0x000099A4
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0000B7AC File Offset: 0x000099AC
		public T model { get; internal set; }

		// Token: 0x060009B9 RID: 2489 RVA: 0x0000B7B5 File Offset: 0x000099B5
		public virtual void Init(PostProcessingContext pcontext, T pmodel)
		{
			this.context = pcontext;
			this.model = pmodel;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0000B7C5 File Offset: 0x000099C5
		public override PostProcessingModel GetModel()
		{
			return this.model;
		}
	}
}
