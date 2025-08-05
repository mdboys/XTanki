using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CDA RID: 11482
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class AmplifyGlareCache
	{
		// Token: 0x0600A087 RID: 41095 RVA: 0x0015FBE8 File Offset: 0x0015DDE8
		public AmplifyGlareCache()
		{
			this.Starlines = new AmplifyStarlineCache[4];
			this.CromaticAberrationMat = new Vector4[4, 8];
			for (int i = 0; i < 4; i++)
			{
				this.Starlines[i] = new AmplifyStarlineCache();
			}
		}

		// Token: 0x0600A088 RID: 41096 RVA: 0x0015FC30 File Offset: 0x0015DE30
		public void Destroy()
		{
			for (int i = 0; i < 4; i++)
			{
				this.Starlines[i].Destroy();
			}
			this.Starlines = null;
			this.CromaticAberrationMat = null;
		}

		// Token: 0x040067CA RID: 26570
		[SerializeField]
		internal AmplifyStarlineCache[] Starlines;

		// Token: 0x040067CB RID: 26571
		[SerializeField]
		internal Vector4 AverageWeight;

		// Token: 0x040067CC RID: 26572
		[SerializeField]
		internal int TotalRT;

		// Token: 0x040067CD RID: 26573
		[SerializeField]
		internal GlareDefData GlareDef;

		// Token: 0x040067CE RID: 26574
		[SerializeField]
		internal StarDefData StarDef;

		// Token: 0x040067CF RID: 26575
		[SerializeField]
		internal int CurrentPassCount;

		// Token: 0x040067D0 RID: 26576
		[SerializeField]
		internal Vector4[,] CromaticAberrationMat;
	}
}
