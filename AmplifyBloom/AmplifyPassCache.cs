using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CDC RID: 11484
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class AmplifyPassCache
	{
		// Token: 0x0600A0B4 RID: 41140 RVA: 0x0005D655 File Offset: 0x0005B855
		public AmplifyPassCache()
		{
			this.Offsets = new Vector4[16];
			this.Weights = new Vector4[16];
		}

		// Token: 0x0600A0B5 RID: 41141 RVA: 0x0005D677 File Offset: 0x0005B877
		public void Destroy()
		{
			this.Offsets = null;
			this.Weights = null;
		}

		// Token: 0x040067DF RID: 26591
		[SerializeField]
		internal Vector4[] Offsets;

		// Token: 0x040067E0 RID: 26592
		[SerializeField]
		internal Vector4[] Weights;
	}
}
