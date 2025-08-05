using System;

namespace log4net.Util
{
	// Token: 0x020029BE RID: 10686
	public class FormattingInfo
	{
		// Token: 0x06009054 RID: 36948 RVA: 0x000564FF File Offset: 0x000546FF
		public FormattingInfo()
		{
		}

		// Token: 0x06009055 RID: 36949 RVA: 0x00056519 File Offset: 0x00054719
		public FormattingInfo(int min, int max, bool leftAlign)
		{
			this.Min = min;
			this.Max = max;
			this.LeftAlign = leftAlign;
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06009056 RID: 36950 RVA: 0x00056548 File Offset: 0x00054748
		// (set) Token: 0x06009057 RID: 36951 RVA: 0x00056550 File Offset: 0x00054750
		public int Min { get; set; } = -1;

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06009058 RID: 36952 RVA: 0x00056559 File Offset: 0x00054759
		// (set) Token: 0x06009059 RID: 36953 RVA: 0x00056561 File Offset: 0x00054761
		public int Max { get; set; } = int.MaxValue;

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x0600905A RID: 36954 RVA: 0x0005656A File Offset: 0x0005476A
		// (set) Token: 0x0600905B RID: 36955 RVA: 0x00056572 File Offset: 0x00054772
		public bool LeftAlign { get; set; }
	}
}
