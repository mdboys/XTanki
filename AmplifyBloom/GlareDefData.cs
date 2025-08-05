using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CE2 RID: 11490
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class GlareDefData
	{
		// Token: 0x0600A0C0 RID: 41152 RVA: 0x0005D6DD File Offset: 0x0005B8DD
		public GlareDefData()
		{
			this.m_customStarData = new StarDefData();
		}

		// Token: 0x0600A0C1 RID: 41153 RVA: 0x0005D6F7 File Offset: 0x0005B8F7
		public GlareDefData(StarLibType starType, float starInclination, float chromaticAberration)
		{
			this.m_starType = starType;
			this.m_starInclination = starInclination;
			this.m_chromaticAberration = chromaticAberration;
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x0600A0C2 RID: 41154 RVA: 0x0005D71B File Offset: 0x0005B91B
		// (set) Token: 0x0600A0C3 RID: 41155 RVA: 0x0005D723 File Offset: 0x0005B923
		public StarLibType StarType
		{
			get
			{
				return this.m_starType;
			}
			set
			{
				this.m_starType = value;
			}
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x0600A0C4 RID: 41156 RVA: 0x0005D72C File Offset: 0x0005B92C
		// (set) Token: 0x0600A0C5 RID: 41157 RVA: 0x0005D734 File Offset: 0x0005B934
		public float StarInclination
		{
			get
			{
				return this.m_starInclination;
			}
			set
			{
				this.m_starInclination = value;
			}
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x0600A0C6 RID: 41158 RVA: 0x0005D73D File Offset: 0x0005B93D
		// (set) Token: 0x0600A0C7 RID: 41159 RVA: 0x0005D74B File Offset: 0x0005B94B
		public float StarInclinationDeg
		{
			get
			{
				return this.m_starInclination * 57.29578f;
			}
			set
			{
				this.m_starInclination = value * 0.017453292f;
			}
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x0600A0C8 RID: 41160 RVA: 0x0005D75A File Offset: 0x0005B95A
		// (set) Token: 0x0600A0C9 RID: 41161 RVA: 0x0005D762 File Offset: 0x0005B962
		public float ChromaticAberration
		{
			get
			{
				return this.m_chromaticAberration;
			}
			set
			{
				this.m_chromaticAberration = value;
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x0600A0CA RID: 41162 RVA: 0x0005D76B File Offset: 0x0005B96B
		// (set) Token: 0x0600A0CB RID: 41163 RVA: 0x0005D773 File Offset: 0x0005B973
		public StarDefData CustomStarData
		{
			get
			{
				return this.m_customStarData;
			}
			set
			{
				this.m_customStarData = value;
			}
		}

		// Token: 0x0400683B RID: 26683
		public bool FoldoutValue = true;

		// Token: 0x0400683C RID: 26684
		[SerializeField]
		private StarLibType m_starType;

		// Token: 0x0400683D RID: 26685
		[SerializeField]
		private float m_starInclination;

		// Token: 0x0400683E RID: 26686
		[SerializeField]
		private float m_chromaticAberration;

		// Token: 0x0400683F RID: 26687
		[SerializeField]
		private StarDefData m_customStarData;
	}
}
