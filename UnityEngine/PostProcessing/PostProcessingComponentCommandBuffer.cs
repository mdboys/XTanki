using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000221 RID: 545
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public abstract class PostProcessingComponentCommandBuffer<[Nullable(0)] T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x060009C2 RID: 2498
		public abstract CameraEvent GetCameraEvent();

		// Token: 0x060009C3 RID: 2499
		public abstract string GetName();

		// Token: 0x060009C4 RID: 2500
		public abstract void PopulateCommandBuffer(CommandBuffer cb);
	}
}
