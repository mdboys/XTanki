using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B0C RID: 11020
	[NullableContext(1)]
	[Nullable(0)]
	internal class OptimizeEdges
	{
		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x060098B3 RID: 39091 RVA: 0x0005B25D File Offset: 0x0005945D
		public int Count
		{
			get
			{
				return this.m_EdgeDictionary.Count;
			}
		}

		// Token: 0x17001835 RID: 6197
		public OptimizeEdge this[OptimizeEdge a_OptimizeEdge]
		{
			get
			{
				return this.m_EdgeDictionary[a_OptimizeEdge];
			}
			set
			{
				this.m_EdgeDictionary[a_OptimizeEdge] = value;
			}
		}

		// Token: 0x060098B6 RID: 39094 RVA: 0x0005B287 File Offset: 0x00059487
		public void Clear()
		{
			this.m_EdgeDictionary.Clear();
		}

		// Token: 0x060098B7 RID: 39095 RVA: 0x0005B294 File Offset: 0x00059494
		public bool HasEdge(OptimizeEdge a_OptimizeEdge)
		{
			return this.m_EdgeDictionary.ContainsKey(a_OptimizeEdge);
		}

		// Token: 0x060098B8 RID: 39096 RVA: 0x0005B2A2 File Offset: 0x000594A2
		public void AddEdge(OptimizeEdge a_OptimizeEdge)
		{
			this.m_EdgeDictionary.Add(a_OptimizeEdge, a_OptimizeEdge);
		}

		// Token: 0x060098B9 RID: 39097 RVA: 0x0005B2B1 File Offset: 0x000594B1
		public void RemoveEdge(OptimizeEdge a_OptimizeEdge)
		{
			this.m_EdgeDictionary.Remove(a_OptimizeEdge);
		}

		// Token: 0x060098BA RID: 39098 RVA: 0x00150B64 File Offset: 0x0014ED64
		public List<OptimizeEdge> OptimizedEdgeList()
		{
			List<OptimizeEdge> list = new List<OptimizeEdge>();
			foreach (OptimizeEdge optimizeEdge in this.m_EdgeDictionary.Keys)
			{
				list.Add(optimizeEdge);
			}
			return list;
		}

		// Token: 0x0400642E RID: 25646
		private readonly SortedDictionary<OptimizeEdge, OptimizeEdge> m_EdgeDictionary = new SortedDictionary<OptimizeEdge, OptimizeEdge>();
	}
}
