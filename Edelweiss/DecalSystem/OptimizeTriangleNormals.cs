using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B0D RID: 11021
	internal class OptimizeTriangleNormals
	{
		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x060098BC RID: 39100 RVA: 0x0005B2D3 File Offset: 0x000594D3
		public int Count
		{
			get
			{
				return this.m_TriangleIndexToNormalDictionary.Count;
			}
		}

		// Token: 0x17001837 RID: 6199
		public Vector3 this[int a_TriangleIndex]
		{
			get
			{
				return this.m_TriangleIndexToNormalDictionary[a_TriangleIndex];
			}
		}

		// Token: 0x060098BE RID: 39102 RVA: 0x0005B2EE File Offset: 0x000594EE
		public void Clear()
		{
			this.m_TriangleIndexToNormalDictionary.Clear();
		}

		// Token: 0x060098BF RID: 39103 RVA: 0x0005B2FB File Offset: 0x000594FB
		public bool HasTriangleNormal(int a_TriangleIndex)
		{
			return this.m_TriangleIndexToNormalDictionary.ContainsKey(a_TriangleIndex);
		}

		// Token: 0x060098C0 RID: 39104 RVA: 0x0005B309 File Offset: 0x00059509
		public void AddTriangleNormal(int a_TriangleIndex, Vector3 a_TriangleNormal)
		{
			this.m_TriangleIndexToNormalDictionary.Add(a_TriangleIndex, a_TriangleNormal);
		}

		// Token: 0x060098C1 RID: 39105 RVA: 0x0005B318 File Offset: 0x00059518
		public void RemoveTriangleNormal(int a_TriangleIndex)
		{
			this.m_TriangleIndexToNormalDictionary.Remove(a_TriangleIndex);
		}

		// Token: 0x0400642F RID: 25647
		[Nullable(1)]
		private readonly SortedDictionary<int, Vector3> m_TriangleIndexToNormalDictionary = new SortedDictionary<int, Vector3>();
	}
}
