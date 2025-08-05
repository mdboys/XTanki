using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CurvedUI
{
	// Token: 0x02002CC5 RID: 11461
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	public class CurvedUITMP : MonoBehaviour
	{
		// Token: 0x06009FBD RID: 40893 RVA: 0x0015C1D4 File Offset: 0x0015A3D4
		private void LateUpdate()
		{
			if (this.tmp != null)
			{
				if (this.savedSize != (base.transform as RectTransform).rect.size)
				{
					this.tesselationRequired = true;
				}
				else if (this.savedCanvasSize != this.mySettings.transform.localScale)
				{
					this.tesselationRequired = true;
				}
				else if (!this.savedPos.AlmostEqual(this.mySettings.transform.worldToLocalMatrix.MultiplyPoint3x4(base.transform.position), 0.01))
				{
					this.curvingRequired = true;
				}
				else if (!this.savedUp.AlmostEqual(this.mySettings.transform.worldToLocalMatrix.MultiplyVector(base.transform.up), 0.01))
				{
					this.curvingRequired = true;
				}
				if (this.Dirty || this.tesselationRequired || this.savedMesh == null || this.vh == null || (this.curvingRequired && !Application.isPlaying))
				{
					this.tmp.renderMode = TextRenderFlags.Render;
					this.tmp.ForceMeshUpdate();
					this.vh = new VertexHelper(this.tmp.mesh);
					this.crvdVE.TesselationRequired = true;
					this.crvdVE.ModifyMesh(this.vh);
					this.savedMesh = new Mesh();
					this.vh.FillMesh(this.savedMesh);
					this.tmp.renderMode = TextRenderFlags.DontRender;
					this.tesselationRequired = false;
					this.Dirty = false;
					this.savedSize = (base.transform as RectTransform).rect.size;
					this.savedUp = this.mySettings.transform.worldToLocalMatrix.MultiplyVector(base.transform.up);
					this.savedPos = this.mySettings.transform.worldToLocalMatrix.MultiplyPoint3x4(base.transform.position);
					this.savedCanvasSize = this.mySettings.transform.localScale;
					this.FindSubmeshes();
					foreach (CurvedUITMPSubmesh curvedUITMPSubmesh in this.subMeshes)
					{
						curvedUITMPSubmesh.UpdateSubmesh(true, false);
					}
				}
				if (this.curvingRequired)
				{
					this.crvdVE.TesselationRequired = false;
					this.crvdVE.CurvingRequired = true;
					this.crvdVE.ModifyMesh(this.vh);
					this.vh.FillMesh(this.savedMesh);
					this.curvingRequired = false;
					this.savedSize = (base.transform as RectTransform).rect.size;
					this.savedUp = this.mySettings.transform.worldToLocalMatrix.MultiplyVector(base.transform.up);
					this.savedPos = this.mySettings.transform.worldToLocalMatrix.MultiplyPoint3x4(base.transform.position);
					foreach (CurvedUITMPSubmesh curvedUITMPSubmesh2 in this.subMeshes)
					{
						curvedUITMPSubmesh2.UpdateSubmesh(false, true);
					}
				}
				this.tmp.canvasRenderer.SetMesh(this.savedMesh);
				return;
			}
			this.FindTMP();
		}

		// Token: 0x06009FBE RID: 40894 RVA: 0x0015C578 File Offset: 0x0015A778
		private void OnEnable()
		{
			this.FindTMP();
			if (this.tmp != null)
			{
				this.tmp.RegisterDirtyMaterialCallback(new UnityAction(this.TesselationRequiredCallback));
				TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<global::UnityEngine.Object>(this.TMPTextChangedCallback));
			}
		}

		// Token: 0x06009FBF RID: 40895 RVA: 0x0005CC53 File Offset: 0x0005AE53
		private void OnDisable()
		{
			if (this.tmp != null)
			{
				this.tmp.UnregisterDirtyMaterialCallback(new UnityAction(this.TesselationRequiredCallback));
				TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<global::UnityEngine.Object>(this.TMPTextChangedCallback));
			}
		}

		// Token: 0x06009FC0 RID: 40896 RVA: 0x0015C5C8 File Offset: 0x0015A7C8
		private void FindTMP()
		{
			if (base.GetComponent<TextMeshProUGUI>() != null)
			{
				this.tmp = base.gameObject.GetComponent<TextMeshProUGUI>();
				this.crvdVE = base.gameObject.GetComponent<CurvedUIVertexEffect>();
				this.mySettings = base.GetComponentInParent<CurvedUISettings>();
				base.transform.hasChanged = false;
				this.FindSubmeshes();
			}
		}

		// Token: 0x06009FC1 RID: 40897 RVA: 0x0015C624 File Offset: 0x0015A824
		private void FindSubmeshes()
		{
			TMP_SubMeshUI[] componentsInChildren = base.GetComponentsInChildren<TMP_SubMeshUI>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				CurvedUITMPSubmesh curvedUITMPSubmesh = componentsInChildren[i].gameObject.AddComponentIfMissing<CurvedUITMPSubmesh>();
				if (!this.subMeshes.Contains(curvedUITMPSubmesh))
				{
					this.subMeshes.Add(curvedUITMPSubmesh);
				}
			}
		}

		// Token: 0x06009FC2 RID: 40898 RVA: 0x0005CC90 File Offset: 0x0005AE90
		private void TMPTextChangedCallback(object obj)
		{
			if (obj == this.tmp)
			{
				this.tesselationRequired = true;
			}
		}

		// Token: 0x06009FC3 RID: 40899 RVA: 0x0005CCA2 File Offset: 0x0005AEA2
		private void TesselationRequiredCallback()
		{
			this.tesselationRequired = true;
			this.curvingRequired = true;
		}

		// Token: 0x0400673E RID: 26430
		[HideInInspector]
		public bool Dirty;

		// Token: 0x0400673F RID: 26431
		private CurvedUIVertexEffect crvdVE;

		// Token: 0x04006740 RID: 26432
		private bool curvingRequired;

		// Token: 0x04006741 RID: 26433
		private CurvedUISettings mySettings;

		// Token: 0x04006742 RID: 26434
		private Vector3 savedCanvasSize;

		// Token: 0x04006743 RID: 26435
		private Mesh savedMesh;

		// Token: 0x04006744 RID: 26436
		private Vector3 savedPos;

		// Token: 0x04006745 RID: 26437
		private Vector2 savedSize;

		// Token: 0x04006746 RID: 26438
		private Vector3 savedUp;

		// Token: 0x04006747 RID: 26439
		private readonly List<CurvedUITMPSubmesh> subMeshes = new List<CurvedUITMPSubmesh>();

		// Token: 0x04006748 RID: 26440
		private bool tesselationRequired;

		// Token: 0x04006749 RID: 26441
		private TextMeshProUGUI tmp;

		// Token: 0x0400674A RID: 26442
		private VertexHelper vh;
	}
}
