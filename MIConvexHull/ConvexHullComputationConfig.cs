using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x02002999 RID: 10649
	[NullableContext(1)]
	[Nullable(0)]
	public class ConvexHullComputationConfig
	{
		// Token: 0x06008F14 RID: 36628 RVA: 0x00055C24 File Offset: 0x00053E24
		public ConvexHullComputationConfig()
		{
			this.PlaneDistanceTolerance = 1E-05;
			this.PointTranslationType = PointTranslationType.None;
			this.PointTranslationGenerator = null;
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06008F15 RID: 36629 RVA: 0x00055C49 File Offset: 0x00053E49
		// (set) Token: 0x06008F16 RID: 36630 RVA: 0x00055C51 File Offset: 0x00053E51
		public double PlaneDistanceTolerance { get; set; }

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06008F17 RID: 36631 RVA: 0x00055C5A File Offset: 0x00053E5A
		// (set) Token: 0x06008F18 RID: 36632 RVA: 0x00055C62 File Offset: 0x00053E62
		public PointTranslationType PointTranslationType { get; set; }

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06008F19 RID: 36633 RVA: 0x00055C6B File Offset: 0x00053E6B
		// (set) Token: 0x06008F1A RID: 36634 RVA: 0x00055C73 File Offset: 0x00053E73
		public Func<double> PointTranslationGenerator { get; set; }

		// Token: 0x06008F1B RID: 36635 RVA: 0x00055C7C File Offset: 0x00053E7C
		private static Func<double> Closure(double radius, Random rnd)
		{
			return () => radius * (rnd.NextDouble() - 0.5);
		}

		// Token: 0x06008F1C RID: 36636 RVA: 0x0013772C File Offset: 0x0013592C
		public static Func<double> RandomShiftByRadius(double radius = 1E-06, int? randomSeed = null)
		{
			Random random = ((randomSeed == null) ? new Random() : new Random(randomSeed.Value));
			return ConvexHullComputationConfig.Closure(radius, random);
		}
	}
}
