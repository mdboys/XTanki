using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CurvedUI
{
	// Token: 0x02002CC6 RID: 11462
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	public class CurvedUITMPSubmesh : MonoBehaviour
	{
		// Token: 0x06009FC5 RID: 40901 RVA: 0x0015C670 File Offset: 0x0015A870
		public void UpdateSubmesh(bool tesselate, bool curve)
		{
			TMP_SubMeshUI component = base.gameObject.GetComponent<TMP_SubMeshUI>();
			if (!(component == null))
			{
				CurvedUIVertexEffect curvedUIVertexEffect = base.gameObject.AddComponentIfMissing<CurvedUIVertexEffect>();
				if (tesselate || this.savedMesh == null || this.vh == null || !Application.isPlaying)
				{
					this.vh = new VertexHelper(component.mesh);
					this.ModifyMesh(curvedUIVertexEffect);
					this.savedMesh = new Mesh();
					this.vh.FillMesh(this.savedMesh);
					curvedUIVertexEffect.TesselationRequired = true;
				}
				else if (curve)
				{
					this.ModifyMesh(curvedUIVertexEffect);
					this.vh.FillMesh(this.savedMesh);
					curvedUIVertexEffect.CurvingRequired = true;
				}
				component.canvasRenderer.SetMesh(this.savedMesh);
			}
		}

		// Token: 0x06009FC6 RID: 40902 RVA: 0x0005CCC5 File Offset: 0x0005AEC5
		private void ModifyMesh(CurvedUIVertexEffect crvdVE)
		{
			crvdVE.ModifyMesh(this.vh);
		}

		// Token: 0x0400674B RID: 26443
		private Mesh savedMesh;

		// Token: 0x0400674C RID: 26444
		private VertexHelper vh;
	}
}
