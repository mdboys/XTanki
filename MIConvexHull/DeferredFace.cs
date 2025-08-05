using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x0200299E RID: 10654
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DeferredFace
	{
		// Token: 0x0400602A RID: 24618
		public ConvexFaceInternal Face;

		// Token: 0x0400602B RID: 24619
		public int FaceIndex;

		// Token: 0x0400602C RID: 24620
		public ConvexFaceInternal OldFace;

		// Token: 0x0400602D RID: 24621
		public ConvexFaceInternal Pivot;

		// Token: 0x0400602E RID: 24622
		public int PivotIndex;
	}
}
