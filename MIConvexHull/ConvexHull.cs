using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x02002996 RID: 10646
	[NullableContext(1)]
	[Nullable(0)]
	public static class ConvexHull
	{
		// Token: 0x06008F08 RID: 36616 RVA: 0x00055B88 File Offset: 0x00053D88
		public static ConvexHull<TVertex, TFace> Create<[Nullable(0)] TVertex, [Nullable(0)] TFace>(IList<TVertex> data, ConvexHullComputationConfig config = null) where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
		{
			return ConvexHull<TVertex, TFace>.Create(data, config);
		}

		// Token: 0x06008F09 RID: 36617 RVA: 0x00055B91 File Offset: 0x00053D91
		public static ConvexHull<TVertex, DefaultConvexFace<TVertex>> Create<[Nullable(0)] TVertex>(IList<TVertex> data, ConvexHullComputationConfig config = null) where TVertex : IVertex
		{
			return ConvexHull<TVertex, DefaultConvexFace<TVertex>>.Create(data, config);
		}

		// Token: 0x06008F0A RID: 36618 RVA: 0x00055B9A File Offset: 0x00053D9A
		public static ConvexHull<DefaultVertex, DefaultConvexFace<DefaultVertex>> Create(IList<double[]> data, ConvexHullComputationConfig config = null)
		{
			return ConvexHull<DefaultVertex, DefaultConvexFace<DefaultVertex>>.Create(data.Select((double[] p) => new DefaultVertex
			{
				Position = p.ToArray<double>()
			}).ToList<DefaultVertex>(), config);
		}
	}
}
