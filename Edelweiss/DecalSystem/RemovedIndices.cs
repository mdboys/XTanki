using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B11 RID: 11025
	internal class RemovedIndices
	{
		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x060098C6 RID: 39110 RVA: 0x0005B368 File Offset: 0x00059568
		public int Count
		{
			get
			{
				return this.m_RemovedIndices.Count;
			}
		}

		// Token: 0x060098C7 RID: 39111 RVA: 0x0005B375 File Offset: 0x00059575
		public void Clear()
		{
			this.m_RemovedIndices.Clear();
			this.m_GreatestUnremovedValue = -1;
		}

		// Token: 0x060098C8 RID: 39112 RVA: 0x00150BC4 File Offset: 0x0014EDC4
		public void AddRemovedIndex(int a_Index)
		{
			if (a_Index < this.Count)
			{
				this.m_RemovedIndices[a_Index] = -1;
				this.m_GreatestUnremovedValue = -1;
				for (int i = a_Index + 1; i < this.Count; i++)
				{
					int num = this.m_RemovedIndices[i] - 1;
					if (num >= 0)
					{
						this.m_GreatestUnremovedValue = num;
					}
					this.m_RemovedIndices[i] = num;
				}
				return;
			}
			int num2 = this.m_GreatestUnremovedValue + 1;
			for (int j = this.Count; j < a_Index; j++)
			{
				this.m_RemovedIndices.Add(num2);
				this.m_GreatestUnremovedValue = num2;
				num2++;
			}
			this.m_RemovedIndices.Add(-1);
		}

		// Token: 0x060098C9 RID: 39113 RVA: 0x0005B389 File Offset: 0x00059589
		public bool IsRemovedIndex(int a_Index)
		{
			return a_Index < this.Count && this.m_RemovedIndices[a_Index] < 0;
		}

		// Token: 0x060098CA RID: 39114 RVA: 0x0005B3A5 File Offset: 0x000595A5
		public int AdjustedIndex(int a_Index)
		{
			if (a_Index >= this.Count)
			{
				return this.m_GreatestUnremovedValue + a_Index - this.Count + 1;
			}
			return this.m_RemovedIndices[a_Index];
		}

		// Token: 0x0400643C RID: 25660
		private int m_GreatestUnremovedValue = -1;

		// Token: 0x0400643D RID: 25661
		[Nullable(1)]
		private readonly List<int> m_RemovedIndices = new List<int>();
	}
}
