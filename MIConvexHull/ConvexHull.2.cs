using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x02002998 RID: 10648
	[NullableContext(1)]
	[Nullable(0)]
	public class ConvexHull<[Nullable(0)] TVertex, [Nullable(0)] TFace> where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
	{
		// Token: 0x06008F0E RID: 36622 RVA: 0x00005698 File Offset: 0x00003898
		internal ConvexHull()
		{
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06008F0F RID: 36623 RVA: 0x00055BEB File Offset: 0x00053DEB
		// (set) Token: 0x06008F10 RID: 36624 RVA: 0x00055BF3 File Offset: 0x00053DF3
		public IEnumerable<TVertex> Points { get; internal set; }

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06008F11 RID: 36625 RVA: 0x00055BFC File Offset: 0x00053DFC
		// (set) Token: 0x06008F12 RID: 36626 RVA: 0x00055C04 File Offset: 0x00053E04
		public IEnumerable<TFace> Faces { get; internal set; }

		// Token: 0x06008F13 RID: 36627 RVA: 0x00055C0D File Offset: 0x00053E0D
		public static ConvexHull<TVertex, TFace> Create(IList<TVertex> data, ConvexHullComputationConfig config)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return ConvexHullInternal.GetConvexHull<TVertex, TFace>(data, config);
		}
	}
}
