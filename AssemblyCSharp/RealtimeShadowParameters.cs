using System;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x02002CD4 RID: 11476
	[ExecuteInEditMode]
	public class RealtimeShadowParameters : MonoBehaviour
	{
		// Token: 0x06009FFC RID: 40956 RVA: 0x0005CE10 File Offset: 0x0005B010
		private void OnEnable()
		{
			Shader.SetGlobalColor("_ShadowMixColor", this.shadowColor);
			Shader.SetGlobalFloat("_ShadowMixStrength", this.shadowStrength);
		}

		// Token: 0x0400676F RID: 26479
		public Color shadowColor;

		// Token: 0x04006770 RID: 26480
		public float shadowStrength;
	}
}
