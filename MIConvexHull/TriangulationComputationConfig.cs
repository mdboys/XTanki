using System;

namespace MIConvexHull
{
	// Token: 0x020029A8 RID: 10664
	public class TriangulationComputationConfig : ConvexHullComputationConfig
	{
		// Token: 0x06008F71 RID: 36721 RVA: 0x00055F20 File Offset: 0x00054120
		public TriangulationComputationConfig()
		{
			this.ZeroCellVolumeTolerance = 1E-05;
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06008F72 RID: 36722 RVA: 0x00055F37 File Offset: 0x00054137
		// (set) Token: 0x06008F73 RID: 36723 RVA: 0x00055F3F File Offset: 0x0005413F
		public double ZeroCellVolumeTolerance { get; set; }
	}
}
