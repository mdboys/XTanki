using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CE8 RID: 11496
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class StarDefData
	{
		// Token: 0x0600A0CD RID: 41165 RVA: 0x00160570 File Offset: 0x0015E770
		public StarDefData()
		{
		}

		// Token: 0x0600A0CE RID: 41166 RVA: 0x001605C0 File Offset: 0x0015E7C0
		public StarDefData(StarLibType starType, string starName, int starLinesCount, int passCount, float sampleLength, float attenuation, float inclination, float rotation, float longAttenuation = 0f, float customIncrement = -1f)
		{
			this.m_starType = starType;
			this.m_starName = starName;
			this.m_passCount = passCount;
			this.m_sampleLength = sampleLength;
			this.m_attenuation = attenuation;
			this.m_starlinesCount = starLinesCount;
			this.m_inclination = inclination;
			this.m_rotation = rotation;
			this.m_customIncrement = customIncrement;
			this.m_longAttenuation = longAttenuation;
			this.CalculateStarData();
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x0600A0CF RID: 41167 RVA: 0x0005D77C File Offset: 0x0005B97C
		// (set) Token: 0x0600A0D0 RID: 41168 RVA: 0x0005D784 File Offset: 0x0005B984
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

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x0600A0D1 RID: 41169 RVA: 0x0005D78D File Offset: 0x0005B98D
		// (set) Token: 0x0600A0D2 RID: 41170 RVA: 0x0005D795 File Offset: 0x0005B995
		public string StarName
		{
			get
			{
				return this.m_starName;
			}
			set
			{
				this.m_starName = value;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x0600A0D3 RID: 41171 RVA: 0x0005D79E File Offset: 0x0005B99E
		// (set) Token: 0x0600A0D4 RID: 41172 RVA: 0x0005D7A6 File Offset: 0x0005B9A6
		public int StarlinesCount
		{
			get
			{
				return this.m_starlinesCount;
			}
			set
			{
				this.m_starlinesCount = value;
				this.CalculateStarData();
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x0600A0D5 RID: 41173 RVA: 0x0005D7B5 File Offset: 0x0005B9B5
		// (set) Token: 0x0600A0D6 RID: 41174 RVA: 0x0005D7BD File Offset: 0x0005B9BD
		public int PassCount
		{
			get
			{
				return this.m_passCount;
			}
			set
			{
				this.m_passCount = value;
				this.CalculateStarData();
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x0600A0D7 RID: 41175 RVA: 0x0005D7CC File Offset: 0x0005B9CC
		// (set) Token: 0x0600A0D8 RID: 41176 RVA: 0x0005D7D4 File Offset: 0x0005B9D4
		public float SampleLength
		{
			get
			{
				return this.m_sampleLength;
			}
			set
			{
				this.m_sampleLength = value;
				this.CalculateStarData();
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x0600A0D9 RID: 41177 RVA: 0x0005D7E3 File Offset: 0x0005B9E3
		// (set) Token: 0x0600A0DA RID: 41178 RVA: 0x0005D7EB File Offset: 0x0005B9EB
		public float Attenuation
		{
			get
			{
				return this.m_attenuation;
			}
			set
			{
				this.m_attenuation = value;
				this.CalculateStarData();
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x0600A0DB RID: 41179 RVA: 0x0005D7FA File Offset: 0x0005B9FA
		// (set) Token: 0x0600A0DC RID: 41180 RVA: 0x0005D802 File Offset: 0x0005BA02
		public float Inclination
		{
			get
			{
				return this.m_inclination;
			}
			set
			{
				this.m_inclination = value;
				this.CalculateStarData();
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x0600A0DD RID: 41181 RVA: 0x0005D811 File Offset: 0x0005BA11
		// (set) Token: 0x0600A0DE RID: 41182 RVA: 0x0005D819 File Offset: 0x0005BA19
		public float CameraRotInfluence
		{
			get
			{
				return this.m_rotation;
			}
			set
			{
				this.m_rotation = value;
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x0600A0DF RID: 41183 RVA: 0x0005D822 File Offset: 0x0005BA22
		public StarLineData[] StarLinesArr
		{
			get
			{
				return this.m_starLinesArr;
			}
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x0600A0E0 RID: 41184 RVA: 0x0005D82A File Offset: 0x0005BA2A
		// (set) Token: 0x0600A0E1 RID: 41185 RVA: 0x0005D832 File Offset: 0x0005BA32
		public float CustomIncrement
		{
			get
			{
				return this.m_customIncrement;
			}
			set
			{
				this.m_customIncrement = value;
				this.CalculateStarData();
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x0600A0E2 RID: 41186 RVA: 0x0005D841 File Offset: 0x0005BA41
		// (set) Token: 0x0600A0E3 RID: 41187 RVA: 0x0005D849 File Offset: 0x0005BA49
		public float LongAttenuation
		{
			get
			{
				return this.m_longAttenuation;
			}
			set
			{
				this.m_longAttenuation = value;
				this.CalculateStarData();
			}
		}

		// Token: 0x0600A0E4 RID: 41188 RVA: 0x0005D858 File Offset: 0x0005BA58
		public void Destroy()
		{
			this.m_starLinesArr = null;
		}

		// Token: 0x0600A0E5 RID: 41189 RVA: 0x00160660 File Offset: 0x0015E860
		public void CalculateStarData()
		{
			if (this.m_starlinesCount == 0)
			{
				return;
			}
			this.m_starLinesArr = new StarLineData[this.m_starlinesCount];
			float num = ((this.m_customIncrement <= 0f) ? (180f / (float)this.m_starlinesCount) : this.m_customIncrement);
			num *= 0.017453292f;
			for (int i = 0; i < this.m_starlinesCount; i++)
			{
				this.m_starLinesArr[i] = new StarLineData
				{
					PassCount = this.m_passCount,
					SampleLength = this.m_sampleLength
				};
				if (this.m_longAttenuation > 0f)
				{
					this.m_starLinesArr[i].Attenuation = ((i % 2 != 0) ? this.m_attenuation : this.m_longAttenuation);
				}
				else
				{
					this.m_starLinesArr[i].Attenuation = this.m_attenuation;
				}
				this.m_starLinesArr[i].Inclination = num * (float)i;
			}
		}

		// Token: 0x04006856 RID: 26710
		[SerializeField]
		private StarLibType m_starType;

		// Token: 0x04006857 RID: 26711
		[SerializeField]
		private string m_starName = string.Empty;

		// Token: 0x04006858 RID: 26712
		[SerializeField]
		private int m_starlinesCount = 2;

		// Token: 0x04006859 RID: 26713
		[SerializeField]
		private int m_passCount = 4;

		// Token: 0x0400685A RID: 26714
		[SerializeField]
		private float m_sampleLength = 1f;

		// Token: 0x0400685B RID: 26715
		[SerializeField]
		private float m_attenuation = 0.85f;

		// Token: 0x0400685C RID: 26716
		[SerializeField]
		private float m_inclination;

		// Token: 0x0400685D RID: 26717
		[SerializeField]
		private float m_rotation;

		// Token: 0x0400685E RID: 26718
		[SerializeField]
		private StarLineData[] m_starLinesArr;

		// Token: 0x0400685F RID: 26719
		[SerializeField]
		private float m_customIncrement = 90f;

		// Token: 0x04006860 RID: 26720
		[SerializeField]
		private float m_longAttenuation;
	}
}
