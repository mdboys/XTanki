using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CDD RID: 11485
	[Serializable]
	public class AmplifyStarlineCache
	{
		// Token: 0x0600A0B6 RID: 41142 RVA: 0x0015FEFC File Offset: 0x0015E0FC
		public AmplifyStarlineCache()
		{
			this.Passes = new AmplifyPassCache[4];
			for (int i = 0; i < 4; i++)
			{
				this.Passes[i] = new AmplifyPassCache();
			}
		}

		// Token: 0x0600A0B7 RID: 41143 RVA: 0x0015FF34 File Offset: 0x0015E134
		public void Destroy()
		{
			for (int i = 0; i < 4; i++)
			{
				this.Passes[i].Destroy();
			}
			this.Passes = null;
		}

		// Token: 0x040067E1 RID: 26593
		[Nullable(1)]
		[SerializeField]
		internal AmplifyPassCache[] Passes;
	}
}
