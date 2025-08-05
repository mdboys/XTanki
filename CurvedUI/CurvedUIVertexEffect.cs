using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CurvedUI
{
	// Token: 0x02002CC7 RID: 11463
	[NullableContext(1)]
	[Nullable(0)]
	public class CurvedUIVertexEffect : BaseMeshEffect
	{
		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06009FC8 RID: 40904 RVA: 0x0005CCD3 File Offset: 0x0005AED3
		// (set) Token: 0x06009FC9 RID: 40905 RVA: 0x0005CCDB File Offset: 0x0005AEDB
		public bool TesselationRequired { get; set; } = true;

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06009FCA RID: 40906 RVA: 0x0005CCE4 File Offset: 0x0005AEE4
		// (set) Token: 0x06009FCB RID: 40907 RVA: 0x0005CCEC File Offset: 0x0005AEEC
		public bool CurvingRequired { get; set; } = true;

		// Token: 0x06009FCC RID: 40908 RVA: 0x0015C734 File Offset: 0x0015A934
		private void Update()
		{
			if (this.myTMP || this.myTMPSubMesh)
			{
				return;
			}
			if (!this.TesselationRequired)
			{
				if ((base.transform as RectTransform).rect.size != this.savedRectSize)
				{
					this.TesselationRequired = true;
				}
				else if (this.myGraphic != null)
				{
					if (this.myGraphic.color != this.savedColor)
					{
						this.TesselationRequired = true;
						this.savedColor = this.myGraphic.color;
					}
					else if (this.myImage != null && this.myImage.fillAmount != this.savedFill)
					{
						this.TesselationRequired = true;
						this.savedFill = this.myImage.fillAmount;
					}
				}
			}
			if (!this.TesselationRequired && !this.CurvingRequired)
			{
				Vector3 vector = this.mySettings.transform.worldToLocalMatrix.MultiplyPoint3x4(base.transform.position);
				if (!vector.AlmostEqual(this.savedPos, 0.01) && (this.mySettings.Shape != CurvedUISettings.CurvedUIShape.CYLINDER || (double)Mathf.Pow(vector.x - this.savedPos.x, 2f) > 1E-05 || (double)Mathf.Pow(vector.z - this.savedPos.z, 2f) > 1E-05))
				{
					this.savedPos = vector;
					this.CurvingRequired = true;
				}
				Vector3 normalized = this.mySettings.transform.worldToLocalMatrix.MultiplyVector(base.transform.up).normalized;
				if (!this.savedUp.AlmostEqual(normalized, 0.0001))
				{
					bool flag = normalized.AlmostEqual(Vector3.up.normalized, 0.01);
					bool flag2 = this.savedUp.AlmostEqual(Vector3.up.normalized, 0.01);
					if ((!flag && flag2) || (flag && !flag2))
					{
						this.TesselationRequired = true;
					}
					this.savedUp = normalized;
					this.CurvingRequired = true;
				}
			}
			if (this.myGraphic && (this.TesselationRequired || this.CurvingRequired))
			{
				this.myGraphic.SetVerticesDirty();
			}
		}

		// Token: 0x06009FCD RID: 40909 RVA: 0x0015C9A0 File Offset: 0x0015ABA0
		protected override void OnEnable()
		{
			this.FindParentSettings(false);
			this.myGraphic = base.GetComponent<Graphic>();
			if (this.myGraphic)
			{
				this.myGraphic.RegisterDirtyMaterialCallback(new UnityAction(this.TesselationRequiredCallback));
				this.myGraphic.SetVerticesDirty();
			}
			this.myText = base.GetComponent<Text>();
			if (this.myText)
			{
				this.myText.RegisterDirtyVerticesCallback(new UnityAction(this.TesselationRequiredCallback));
				Font.textureRebuilt += this.FontTextureRebuiltCallback;
			}
			this.myTMP = base.GetComponent<TextMeshProUGUI>();
			this.myTMPSubMesh = base.GetComponent<CurvedUITMPSubmesh>();
		}

		// Token: 0x06009FCE RID: 40910 RVA: 0x0015CA4C File Offset: 0x0015AC4C
		protected override void OnDisable()
		{
			if (this.myGraphic)
			{
				this.myGraphic.UnregisterDirtyMaterialCallback(new UnityAction(this.TesselationRequiredCallback));
			}
			if (this.myText)
			{
				this.myText.UnregisterDirtyVerticesCallback(new UnityAction(this.TesselationRequiredCallback));
				Font.textureRebuilt -= this.FontTextureRebuiltCallback;
			}
		}

		// Token: 0x06009FCF RID: 40911 RVA: 0x0005CCF5 File Offset: 0x0005AEF5
		private void TesselationRequiredCallback()
		{
			this.TesselationRequired = true;
		}

		// Token: 0x06009FD0 RID: 40912 RVA: 0x0005CCFE File Offset: 0x0005AEFE
		private void FontTextureRebuiltCallback(Font fontie)
		{
			if (this.myText.font == fontie)
			{
				this.TesselationRequired = true;
			}
		}

		// Token: 0x06009FD1 RID: 40913 RVA: 0x0015CAB4 File Offset: 0x0015ACB4
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			if (this.mySettings == null)
			{
				this.FindParentSettings(false);
			}
			if (this.mySettings == null || !this.mySettings.enabled)
			{
				return;
			}
			this.CheckTextFontMaterial();
			if (this.TesselationRequired || this.CurvingRequired || this.SavedVertexHelper == null || this.SavedVertexHelper.currentVertCount == 0)
			{
				this.SavedVerteees = new List<UIVertex>();
				vh.GetUIVertexStream(this.SavedVerteees);
				this.ModifyVerts(this.SavedVerteees);
				if (this.SavedVertexHelper == null)
				{
					this.SavedVertexHelper = new VertexHelper();
				}
				else
				{
					this.SavedVertexHelper.Clear();
				}
				if (this.SavedVerteees.Count % 4 == 0)
				{
					for (int i = 0; i < this.SavedVerteees.Count; i += 4)
					{
						this.SavedVertexHelper.AddUIVertexQuad(new UIVertex[]
						{
							this.SavedVerteees[i],
							this.SavedVerteees[i + 1],
							this.SavedVerteees[i + 2],
							this.SavedVerteees[i + 3]
						});
					}
				}
				else
				{
					this.SavedVertexHelper.AddUIVertexTriangleStream(this.SavedVerteees);
				}
				this.SavedVertexHelper.GetUIVertexStream(this.SavedVerteees);
				this.CurvingRequired = false;
			}
			vh.Clear();
			vh.AddUIVertexTriangleStream(this.SavedVerteees);
		}

		// Token: 0x06009FD2 RID: 40914 RVA: 0x0015CC34 File Offset: 0x0015AE34
		private void CheckTextFontMaterial()
		{
			if (this.myText && this.myText.cachedTextGenerator.verts.Count > 0 && this.myText.cachedTextGenerator.verts[0].uv0 != this.savedTextUV0)
			{
				this.savedTextUV0 = this.myText.cachedTextGenerator.verts[0].uv0;
				this.TesselationRequired = true;
			}
		}

		// Token: 0x06009FD3 RID: 40915 RVA: 0x0015CCB8 File Offset: 0x0015AEB8
		public CurvedUISettings FindParentSettings(bool forceNew = false)
		{
			if (this.mySettings == null || forceNew)
			{
				this.mySettings = base.GetComponentInParent<CurvedUISettings>();
				if (this.mySettings == null)
				{
					return null;
				}
				this.myCanvas = this.mySettings.GetComponent<Canvas>();
				this.angle = (float)this.mySettings.Angle;
				this.myImage = base.GetComponent<Image>();
			}
			return this.mySettings;
		}

		// Token: 0x06009FD4 RID: 40916 RVA: 0x0015CD28 File Offset: 0x0015AF28
		private void ModifyVerts(List<UIVertex> verts)
		{
			if (verts == null || verts.Count == 0)
			{
				return;
			}
			this.CanvasToWorld = this.myCanvas.transform.localToWorldMatrix;
			this.CanvasToLocal = this.myCanvas.transform.worldToLocalMatrix;
			this.MyToWorld = base.transform.localToWorldMatrix;
			this.MyToLocal = base.transform.worldToLocalMatrix;
			if (this.TesselationRequired || !Application.isPlaying)
			{
				this.TesselateGeometry(verts);
				this.tesselatedVerts = verts.ToList<UIVertex>();
				this.savedRectSize = (base.transform as RectTransform).rect.size;
				this.TesselationRequired = false;
			}
			this.angle = (float)this.mySettings.Angle;
			float cyllinderRadiusInCanvasSpace = this.mySettings.GetCyllinderRadiusInCanvasSpace();
			Vector2 size = (this.myCanvas.transform as RectTransform).rect.size;
			int count = verts.Count;
			if (this.tesselatedVerts != null)
			{
				UIVertex[] array = new UIVertex[this.tesselatedVerts.Count];
				for (int i = 0; i < this.tesselatedVerts.Count; i++)
				{
					array[i] = this.CurveVertex(this.tesselatedVerts[i], this.angle, cyllinderRadiusInCanvasSpace, size);
				}
				verts.AddRange(array);
				verts.RemoveRange(0, count);
				return;
			}
			UIVertex[] array2 = new UIVertex[verts.Count];
			for (int j = 0; j < count; j++)
			{
				array2[j] = this.CurveVertex(verts[j], this.angle, cyllinderRadiusInCanvasSpace, size);
			}
			verts.AddRange(array2);
			verts.RemoveRange(0, count);
		}

		// Token: 0x06009FD5 RID: 40917 RVA: 0x0015CED8 File Offset: 0x0015B0D8
		private UIVertex CurveVertex(UIVertex input, float cylinder_angle, float radius, Vector2 canvasSize)
		{
			Vector3 vector = input.position;
			vector = this.CanvasToLocal.MultiplyPoint3x4(this.MyToWorld.MultiplyPoint3x4(vector));
			if (this.mySettings.Shape == CurvedUISettings.CurvedUIShape.CYLINDER && this.mySettings.Angle != 0)
			{
				float num = vector.x / canvasSize.x * cylinder_angle * 0.017453292f;
				radius += vector.z;
				vector.x = Mathf.Sin(num) * radius;
				vector.z += Mathf.Cos(num) * radius - radius;
			}
			else if (this.mySettings.Shape == CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL && this.mySettings.Angle != 0)
			{
				float num2 = vector.y / canvasSize.y * cylinder_angle * 0.017453292f;
				radius += vector.z;
				vector.y = Mathf.Sin(num2) * radius;
				vector.z += Mathf.Cos(num2) * radius - radius;
			}
			else if (this.mySettings.Shape == CurvedUISettings.CurvedUIShape.RING)
			{
				float num3 = 0f;
				float num4 = vector.y.Remap(canvasSize.y * 0.5f * (float)(this.mySettings.RingFlipVertical ? 1 : (-1)), (0f - canvasSize.y) * 0.5f * (float)(this.mySettings.RingFlipVertical ? 1 : (-1)), (float)this.mySettings.RingExternalDiameter * (1f - this.mySettings.RingFill) * 0.5f, (float)this.mySettings.RingExternalDiameter * 0.5f);
				float num5 = (vector.x / canvasSize.x).Remap(-0.5f, 0.5f, 1.5707964f, cylinder_angle * 0.017453292f + 1.5707964f) - num3;
				vector.x = num4 * Mathf.Cos(num5);
				vector.y = num4 * Mathf.Sin(num5);
			}
			else if (this.mySettings.Shape == CurvedUISettings.CurvedUIShape.SPHERE && this.mySettings.Angle != 0)
			{
				float num6 = (float)this.mySettings.VerticalAngle;
				float num7 = 0f - vector.z;
				if (this.mySettings.PreserveAspect)
				{
					num6 = cylinder_angle * (canvasSize.y / canvasSize.x);
				}
				else
				{
					radius = canvasSize.x / 2f;
					if (num6 == 0f)
					{
						return input;
					}
				}
				float num8 = (vector.x / canvasSize.x).Remap(-0.5f, 0.5f, (180f - cylinder_angle) / 2f - 90f, 180f - (180f - cylinder_angle) / 2f - 90f);
				num8 *= 0.017453292f;
				float num9 = (vector.y / canvasSize.y).Remap(-0.5f, 0.5f, (180f - num6) / 2f, 180f - (180f - num6) / 2f);
				num9 *= 0.017453292f;
				vector.z = Mathf.Sin(num9) * Mathf.Cos(num8) * (radius + num7);
				vector.y = (0f - (radius + num7)) * Mathf.Cos(num9);
				vector.x = Mathf.Sin(num9) * Mathf.Sin(num8) * (radius + num7);
				if (this.mySettings.PreserveAspect)
				{
					vector.z -= radius;
				}
			}
			input.position = this.MyToLocal.MultiplyPoint3x4(this.CanvasToWorld.MultiplyPoint3x4(vector));
			return input;
		}

		// Token: 0x06009FD6 RID: 40918 RVA: 0x0015D268 File Offset: 0x0015B468
		private void TesselateGeometry(List<UIVertex> verts)
		{
			Vector2 tesslationSize = this.mySettings.GetTesslationSize(false);
			this.TransformMisaligned = !this.savedUp.AlmostEqual(Vector3.up.normalized, 0.01);
			this.TrisToQuads(verts);
			if (this.myText == null && this.myTMP == null && !this.DoNotTesselate)
			{
				int count = verts.Count;
				for (int i = 0; i < count; i += 4)
				{
					this.ModifyQuad(verts, i, tesslationSize);
				}
				verts.RemoveRange(0, count);
			}
		}

		// Token: 0x06009FD7 RID: 40919 RVA: 0x0015D2FC File Offset: 0x0015B4FC
		private void ModifyQuad(List<UIVertex> verts, int vertexIndex, Vector2 requiredSize)
		{
			UIVertex[] array = new UIVertex[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = verts[vertexIndex + i];
			}
			Vector3 vector = array[2].position - array[1].position;
			Vector3 vector2 = array[1].position - array[0].position;
			if (this.myImage != null && this.myImage.type == Image.Type.Filled)
			{
				vector = ((vector.x <= (array[3].position - array[0].position).x) ? (array[3].position - array[0].position) : vector);
				vector2 = ((vector2.y <= (array[2].position - array[3].position).y) ? (array[2].position - array[3].position) : vector2);
			}
			int num = 1;
			int num2 = 1;
			if (this.TransformMisaligned || this.mySettings.Shape == CurvedUISettings.CurvedUIShape.SPHERE || this.mySettings.Shape == CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL)
			{
				num2 = Mathf.CeilToInt(vector2.magnitude * (1f / Mathf.Max(1f, requiredSize.y)));
			}
			if (this.TransformMisaligned || this.mySettings.Shape != CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL)
			{
				num = Mathf.CeilToInt(vector.magnitude * (1f / Mathf.Max(1f, requiredSize.x)));
			}
			bool flag = false;
			bool flag2 = false;
			float num3 = 0f;
			int num4 = 0;
			while (num4 < num2 || !flag)
			{
				flag = true;
				float num5 = ((float)num4 + 1f) / (float)num2;
				float num6 = 0f;
				int num7 = 0;
				while (num7 < num || !flag2)
				{
					flag2 = true;
					float num8 = ((float)num7 + 1f) / (float)num;
					verts.Add(this.TesselateQuad(array, num6, num3));
					verts.Add(this.TesselateQuad(array, num6, num5));
					verts.Add(this.TesselateQuad(array, num8, num5));
					verts.Add(this.TesselateQuad(array, num8, num3));
					num6 = num8;
					num7++;
				}
				num3 = num5;
				num4++;
			}
		}

		// Token: 0x06009FD8 RID: 40920 RVA: 0x0015D568 File Offset: 0x0015B768
		private void TrisToQuads(List<UIVertex> verts)
		{
			int num = 0;
			int count = verts.Count;
			UIVertex[] array = new UIVertex[count / 6 * 4];
			for (int i = 0; i < count; i += 6)
			{
				array[num++] = verts[i];
				array[num++] = verts[i + 1];
				array[num++] = verts[i + 2];
				array[num++] = verts[i + 4];
			}
			verts.AddRange(array);
			verts.RemoveRange(0, count);
		}

		// Token: 0x06009FD9 RID: 40921 RVA: 0x0015D608 File Offset: 0x0015B808
		private UIVertex TesselateQuad(UIVertex[] quad, float x, float y)
		{
			UIVertex uivertex = default(UIVertex);
			float[] array = new float[]
			{
				(1f - x) * (1f - y),
				(1f - x) * y,
				x * y,
				x * (1f - y)
			};
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			Vector3 vector3 = Vector3.zero;
			for (int i = 0; i < 4; i++)
			{
				vector += quad[i].uv0 * array[i];
				vector2 += quad[i].uv1 * array[i];
				vector3 += quad[i].position * array[i];
			}
			uivertex.position = vector3;
			uivertex.color = quad[0].color;
			uivertex.uv0 = vector;
			uivertex.uv1 = vector2;
			uivertex.normal = quad[0].normal;
			uivertex.tangent = quad[0].tangent;
			return uivertex;
		}

		// Token: 0x06009FDA RID: 40922 RVA: 0x0005CCF5 File Offset: 0x0005AEF5
		public void SetDirty()
		{
			this.TesselationRequired = true;
		}

		// Token: 0x0400674D RID: 26445
		[Tooltip("Check to skip tesselation pass on this object. CurvedUI will not create additional vertices to make this object have a smoother curve. Checking this can solve some issues if you create your own procedural mesh for this object. Default false.")]
		public bool DoNotTesselate;

		// Token: 0x0400674E RID: 26446
		[SerializeField]
		[HideInInspector]
		private Vector3 savedPos;

		// Token: 0x0400674F RID: 26447
		[SerializeField]
		[HideInInspector]
		private Vector3 savedUp;

		// Token: 0x04006750 RID: 26448
		[SerializeField]
		[HideInInspector]
		private Vector2 savedRectSize;

		// Token: 0x04006751 RID: 26449
		[SerializeField]
		[HideInInspector]
		private Color savedColor;

		// Token: 0x04006752 RID: 26450
		[SerializeField]
		[HideInInspector]
		private Vector2 savedTextUV0;

		// Token: 0x04006753 RID: 26451
		[SerializeField]
		[HideInInspector]
		private float savedFill;

		// Token: 0x04006754 RID: 26452
		private float angle = 90f;

		// Token: 0x04006755 RID: 26453
		private Matrix4x4 CanvasToLocal;

		// Token: 0x04006756 RID: 26454
		private Matrix4x4 CanvasToWorld;

		// Token: 0x04006757 RID: 26455
		private Canvas myCanvas;

		// Token: 0x04006758 RID: 26456
		private Graphic myGraphic;

		// Token: 0x04006759 RID: 26457
		private Image myImage;

		// Token: 0x0400675A RID: 26458
		private CurvedUISettings mySettings;

		// Token: 0x0400675B RID: 26459
		private Text myText;

		// Token: 0x0400675C RID: 26460
		private TextMeshProUGUI myTMP;

		// Token: 0x0400675D RID: 26461
		private CurvedUITMPSubmesh myTMPSubMesh;

		// Token: 0x0400675E RID: 26462
		private Matrix4x4 MyToLocal;

		// Token: 0x0400675F RID: 26463
		private Matrix4x4 MyToWorld;

		// Token: 0x04006760 RID: 26464
		private List<UIVertex> SavedVerteees;

		// Token: 0x04006761 RID: 26465
		private VertexHelper SavedVertexHelper;

		// Token: 0x04006762 RID: 26466
		private List<UIVertex> tesselatedVerts;

		// Token: 0x04006763 RID: 26467
		private bool TransformMisaligned;
	}
}
