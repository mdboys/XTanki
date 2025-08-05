using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CD8 RID: 11480
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class AmplifyBokehData
	{
		// Token: 0x0600A069 RID: 41065 RVA: 0x0005D29A File Offset: 0x0005B49A
		public AmplifyBokehData(Vector4[] offsets)
		{
			this.Offsets = offsets;
		}

		// Token: 0x0600A06A RID: 41066 RVA: 0x0005D2A9 File Offset: 0x0005B4A9
		public void Destroy()
		{
			if (this.BokehRenderTexture != null)
			{
				AmplifyUtils.ReleaseTempRenderTarget(this.BokehRenderTexture);
				this.BokehRenderTexture = null;
			}
			this.Offsets = null;
		}

		// Token: 0x040067AC RID: 26540
		internal RenderTexture BokehRenderTexture;

		// Token: 0x040067AD RID: 26541
		internal Vector4[] Offsets;
	}
}
