using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000222 RID: 546
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public abstract class PostProcessingComponentRenderTexture<[Nullable(0)] T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x0000568E File Offset: 0x0000388E
		public virtual void Prepare(Material material)
		{
		}
	}
}
