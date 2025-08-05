using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AEC RID: 10988
	internal class CutEdges
	{
		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06009783 RID: 38787 RVA: 0x0005A810 File Offset: 0x00058A10
		public int Count
		{
			get
			{
				return this.m_CutEdgeDictionary.Count;
			}
		}

		// Token: 0x170017DB RID: 6107
		public CutEdge this[CutEdge a_CutEdge]
		{
			get
			{
				return this.m_CutEdgeDictionary[a_CutEdge];
			}
			set
			{
				this.m_CutEdgeDictionary[a_CutEdge] = value;
			}
		}

		// Token: 0x06009786 RID: 38790 RVA: 0x0005A83A File Offset: 0x00058A3A
		public void Clear()
		{
			this.m_CutEdgeDictionary.Clear();
		}

		// Token: 0x06009787 RID: 38791 RVA: 0x0005A847 File Offset: 0x00058A47
		public bool HasEdge(CutEdge a_CutEdge)
		{
			return this.m_CutEdgeDictionary.ContainsKey(a_CutEdge);
		}

		// Token: 0x06009788 RID: 38792 RVA: 0x0005A855 File Offset: 0x00058A55
		public void AddEdge(CutEdge a_CutEdge)
		{
			this.m_CutEdgeDictionary.Add(a_CutEdge, a_CutEdge);
		}

		// Token: 0x040063A7 RID: 25511
		[Nullable(1)]
		private readonly SortedDictionary<CutEdge, CutEdge> m_CutEdgeDictionary = new SortedDictionary<CutEdge, CutEdge>();
	}
}
