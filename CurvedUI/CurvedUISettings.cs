using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CurvedUI
{
	// Token: 0x02002CC3 RID: 11459
	[NullableContext(1)]
	[Nullable(0)]
	[AddComponentMenu("CurvedUI/CurvedUISettings")]
	[RequireComponent(typeof(Canvas))]
	public class CurvedUISettings : MonoBehaviour
	{
		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06009F81 RID: 40833 RVA: 0x0005C945 File Offset: 0x0005AB45
		private RectTransform RectTransform
		{
			get
			{
				if (this.m_rectTransform == null)
				{
					this.m_rectTransform = base.transform as RectTransform;
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06009F82 RID: 40834 RVA: 0x0005C96C File Offset: 0x0005AB6C
		public int BaseCircleSegments { get; } = 24;

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06009F83 RID: 40835 RVA: 0x0005C974 File Offset: 0x0005AB74
		// (set) Token: 0x06009F84 RID: 40836 RVA: 0x0005C97C File Offset: 0x0005AB7C
		public int Angle
		{
			get
			{
				return this.angle;
			}
			set
			{
				if (this.angle != value)
				{
					this.SetUIAngle(value);
				}
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x06009F85 RID: 40837 RVA: 0x0005C98E File Offset: 0x0005AB8E
		// (set) Token: 0x06009F86 RID: 40838 RVA: 0x0005C996 File Offset: 0x0005AB96
		public float Quality
		{
			get
			{
				return this.quality;
			}
			set
			{
				if (this.quality != value)
				{
					this.quality = value;
					this.SetUIAngle(this.angle);
				}
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x06009F87 RID: 40839 RVA: 0x0005C9B4 File Offset: 0x0005ABB4
		// (set) Token: 0x06009F88 RID: 40840 RVA: 0x0005C9BC File Offset: 0x0005ABBC
		public CurvedUISettings.CurvedUIShape Shape
		{
			get
			{
				return this.shape;
			}
			set
			{
				if (this.shape != value)
				{
					this.shape = value;
					this.SetUIAngle(this.angle);
				}
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x06009F89 RID: 40841 RVA: 0x0005C9DA File Offset: 0x0005ABDA
		// (set) Token: 0x06009F8A RID: 40842 RVA: 0x0005C9E2 File Offset: 0x0005ABE2
		public int VerticalAngle
		{
			get
			{
				return this.vertAngle;
			}
			set
			{
				if (this.vertAngle != value)
				{
					this.vertAngle = value;
					this.SetUIAngle(this.angle);
				}
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06009F8B RID: 40843 RVA: 0x0005CA00 File Offset: 0x0005AC00
		// (set) Token: 0x06009F8C RID: 40844 RVA: 0x0005CA08 File Offset: 0x0005AC08
		public float RingFill
		{
			get
			{
				return this.ringFill;
			}
			set
			{
				if (this.ringFill != value)
				{
					this.ringFill = value;
					this.SetUIAngle(this.angle);
				}
			}
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06009F8D RID: 40845 RVA: 0x0005CA26 File Offset: 0x0005AC26
		public float SavedRadius
		{
			get
			{
				if (this.savedRadius == 0f)
				{
					this.savedRadius = this.GetCyllinderRadiusInCanvasSpace();
				}
				return this.savedRadius;
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06009F8E RID: 40846 RVA: 0x0005CA47 File Offset: 0x0005AC47
		// (set) Token: 0x06009F8F RID: 40847 RVA: 0x0005CA4F File Offset: 0x0005AC4F
		public int RingExternalDiameter
		{
			get
			{
				return this.ringExternalDiamater;
			}
			set
			{
				if (this.ringExternalDiamater != value)
				{
					this.ringExternalDiamater = value;
					this.SetUIAngle(this.angle);
				}
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06009F90 RID: 40848 RVA: 0x0005CA6D File Offset: 0x0005AC6D
		// (set) Token: 0x06009F91 RID: 40849 RVA: 0x0005CA75 File Offset: 0x0005AC75
		public bool RingFlipVertical
		{
			get
			{
				return this.ringFlipVertical;
			}
			set
			{
				if (this.ringFlipVertical != value)
				{
					this.ringFlipVertical = value;
					this.SetUIAngle(this.angle);
				}
			}
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06009F92 RID: 40850 RVA: 0x0005CA93 File Offset: 0x0005AC93
		// (set) Token: 0x06009F93 RID: 40851 RVA: 0x0005CA9B File Offset: 0x0005AC9B
		public bool PreserveAspect
		{
			get
			{
				return this.preserveAspect;
			}
			set
			{
				if (this.preserveAspect != value)
				{
					this.preserveAspect = value;
					this.SetUIAngle(this.angle);
				}
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06009F94 RID: 40852 RVA: 0x0005CAB9 File Offset: 0x0005ACB9
		// (set) Token: 0x06009F95 RID: 40853 RVA: 0x0005CAC1 File Offset: 0x0005ACC1
		public bool Interactable
		{
			get
			{
				return this.interactable;
			}
			set
			{
				this.interactable = value;
			}
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06009F96 RID: 40854 RVA: 0x0005CACA File Offset: 0x0005ACCA
		// (set) Token: 0x06009F97 RID: 40855 RVA: 0x0005CAD2 File Offset: 0x0005ACD2
		public bool ForceUseBoxCollider
		{
			get
			{
				return this.forceUseBoxCollider;
			}
			set
			{
				this.forceUseBoxCollider = value;
			}
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06009F98 RID: 40856 RVA: 0x0005CADB File Offset: 0x0005ACDB
		// (set) Token: 0x06009F99 RID: 40857 RVA: 0x0005CAE3 File Offset: 0x0005ACE3
		public bool BlocksRaycasts
		{
			get
			{
				return this.blocksRaycasts;
			}
			set
			{
				if (this.blocksRaycasts != value)
				{
					this.blocksRaycasts = value;
					if (Application.isPlaying && base.GetComponent<CurvedUIRaycaster>() != null)
					{
						base.GetComponent<CurvedUIRaycaster>().RebuildCollider();
					}
				}
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06009F9A RID: 40858 RVA: 0x0005CB15 File Offset: 0x0005AD15
		// (set) Token: 0x06009F9B RID: 40859 RVA: 0x0005CB1D File Offset: 0x0005AD1D
		public bool RaycastMyLayerOnly
		{
			get
			{
				return this.raycastMyLayerOnly;
			}
			set
			{
				this.raycastMyLayerOnly = value;
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06009F9C RID: 40860 RVA: 0x0005CB26 File Offset: 0x0005AD26
		// (set) Token: 0x06009F9D RID: 40861 RVA: 0x0005CB2D File Offset: 0x0005AD2D
		public CurvedUIInputModule.CUIControlMethod ControlMethod
		{
			get
			{
				return CurvedUIInputModule.ControlMethod;
			}
			set
			{
				CurvedUIInputModule.ControlMethod = value;
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06009F9E RID: 40862 RVA: 0x0005CB35 File Offset: 0x0005AD35
		// (set) Token: 0x06009F9F RID: 40863 RVA: 0x0005CB41 File Offset: 0x0005AD41
		public bool GazeUseTimedClick
		{
			get
			{
				return CurvedUIInputModule.Instance.GazeUseTimedClick;
			}
			set
			{
				CurvedUIInputModule.Instance.GazeUseTimedClick = value;
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06009FA0 RID: 40864 RVA: 0x0005CB4E File Offset: 0x0005AD4E
		// (set) Token: 0x06009FA1 RID: 40865 RVA: 0x0005CB5A File Offset: 0x0005AD5A
		public float GazeClickTimer
		{
			get
			{
				return CurvedUIInputModule.Instance.GazeClickTimer;
			}
			set
			{
				CurvedUIInputModule.Instance.GazeClickTimer = value;
			}
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06009FA2 RID: 40866 RVA: 0x0005CB67 File Offset: 0x0005AD67
		// (set) Token: 0x06009FA3 RID: 40867 RVA: 0x0005CB73 File Offset: 0x0005AD73
		public float GazeClickTimerDelay
		{
			get
			{
				return CurvedUIInputModule.Instance.GazeClickTimerDelay;
			}
			set
			{
				CurvedUIInputModule.Instance.GazeClickTimerDelay = value;
			}
		}

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06009FA4 RID: 40868 RVA: 0x0005CB80 File Offset: 0x0005AD80
		public float GazeTimerProgress
		{
			get
			{
				return CurvedUIInputModule.Instance.GazeTimerProgress;
			}
		}

		// Token: 0x06009FA5 RID: 40869 RVA: 0x0015B74C File Offset: 0x0015994C
		private void Awake()
		{
			if (this.RaycastMyLayerOnly && base.gameObject.layer == 0)
			{
				base.gameObject.layer = 5;
			}
			this.savedRectSize = this.RectTransform.rect.size;
		}

		// Token: 0x06009FA6 RID: 40870 RVA: 0x0005CB8C File Offset: 0x0005AD8C
		private void Start()
		{
			if (this.myCanvas == null)
			{
				this.myCanvas = base.GetComponent<Canvas>();
			}
			this.savedRadius = this.GetCyllinderRadiusInCanvasSpace();
		}

		// Token: 0x06009FA7 RID: 40871 RVA: 0x0015B794 File Offset: 0x00159994
		private void Update()
		{
			if (this.RectTransform.rect.size != this.savedRectSize)
			{
				this.savedRectSize = this.RectTransform.rect.size;
				this.SetUIAngle(this.angle);
			}
			if (this.savedRectSize.x == 0f || this.savedRectSize.y == 0f)
			{
				Debug.LogError("CurvedUI: Your Canvas size must be bigger than 0!");
			}
		}

		// Token: 0x06009FA8 RID: 40872 RVA: 0x0005CBB4 File Offset: 0x0005ADB4
		private void OnEnable()
		{
			this.SetAllDirty();
		}

		// Token: 0x06009FA9 RID: 40873 RVA: 0x0005CBB4 File Offset: 0x0005ADB4
		private void OnDisable()
		{
			this.SetAllDirty();
		}

		// Token: 0x06009FAA RID: 40874 RVA: 0x0015B814 File Offset: 0x00159A14
		public void SetAllDirty()
		{
			Graphic[] componentsInChildren = base.GetComponentsInChildren<Graphic>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetAllDirty();
			}
		}

		// Token: 0x06009FAB RID: 40875 RVA: 0x0015B840 File Offset: 0x00159A40
		private void SetUIAngle(int newAngle)
		{
			if (this.myCanvas == null)
			{
				this.myCanvas = base.GetComponent<Canvas>();
			}
			if (newAngle == 0)
			{
				newAngle = 1;
			}
			this.angle = newAngle;
			this.savedRadius = this.GetCyllinderRadiusInCanvasSpace();
			CurvedUIVertexEffect[] componentsInChildren = base.GetComponentsInChildren<CurvedUIVertexEffect>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].TesselationRequired = true;
			}
			Graphic[] componentsInChildren2 = base.GetComponentsInChildren<Graphic>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				componentsInChildren2[i].SetVerticesDirty();
			}
			if (Application.isPlaying && base.GetComponent<CurvedUIRaycaster>() != null)
			{
				base.GetComponent<CurvedUIRaycaster>().RebuildCollider();
			}
		}

		// Token: 0x06009FAC RID: 40876 RVA: 0x0015B8DC File Offset: 0x00159ADC
		private Vector3 CanvasToCyllinder(Vector3 pos)
		{
			float num = pos.x / this.savedRectSize.x * (float)this.Angle * 0.017453292f;
			pos.x = Mathf.Sin(num) * (this.SavedRadius + pos.z);
			pos.z += Mathf.Cos(num) * (this.SavedRadius + pos.z) - (this.SavedRadius + pos.z);
			return pos;
		}

		// Token: 0x06009FAD RID: 40877 RVA: 0x0015B954 File Offset: 0x00159B54
		private Vector3 CanvasToCyllinderVertical(Vector3 pos)
		{
			float num = pos.y / this.savedRectSize.y * (float)this.Angle * 0.017453292f;
			pos.y = Mathf.Sin(num) * (this.SavedRadius + pos.z);
			pos.z += Mathf.Cos(num) * (this.SavedRadius + pos.z) - (this.SavedRadius + pos.z);
			return pos;
		}

		// Token: 0x06009FAE RID: 40878 RVA: 0x0015B9CC File Offset: 0x00159BCC
		private Vector3 CanvasToRing(Vector3 pos)
		{
			float num = pos.y.Remap(this.savedRectSize.y * 0.5f * (float)(this.RingFlipVertical ? 1 : (-1)), (0f - this.savedRectSize.y) * 0.5f * (float)(this.RingFlipVertical ? 1 : (-1)), (float)this.RingExternalDiameter * (1f - this.RingFill) * 0.5f, (float)this.RingExternalDiameter * 0.5f);
			float num2 = (pos.x / this.savedRectSize.x).Remap(-0.5f, 0.5f, 1.5707964f, (float)this.angle * 0.017453292f + 1.5707964f);
			pos.x = num * Mathf.Cos(num2);
			pos.y = num * Mathf.Sin(num2);
			return pos;
		}

		// Token: 0x06009FAF RID: 40879 RVA: 0x0015BAAC File Offset: 0x00159CAC
		private Vector3 CanvasToSphere(Vector3 pos)
		{
			float num = this.SavedRadius;
			float num2 = (float)this.VerticalAngle;
			if (this.PreserveAspect)
			{
				num2 = (float)this.angle * (this.savedRectSize.y / this.savedRectSize.x);
				num += ((this.Angle <= 0) ? pos.z : (0f - pos.z));
			}
			else
			{
				num = this.savedRectSize.x / 2f + pos.z;
				if (num2 == 0f)
				{
					return Vector3.zero;
				}
			}
			float num3 = (pos.x / this.savedRectSize.x).Remap(-0.5f, 0.5f, (float)(180 - this.angle) / 2f - 90f, 180f - (float)(180 - this.angle) / 2f - 90f);
			num3 *= 0.017453292f;
			float num4 = (pos.y / this.savedRectSize.y).Remap(-0.5f, 0.5f, (180f - num2) / 2f, 180f - (180f - num2) / 2f);
			num4 *= 0.017453292f;
			pos.z = Mathf.Sin(num4) * Mathf.Cos(num3) * num;
			pos.y = (0f - num) * Mathf.Cos(num4);
			pos.x = Mathf.Sin(num4) * Mathf.Sin(num3) * num;
			if (this.PreserveAspect)
			{
				pos.z -= num;
			}
			return pos;
		}

		// Token: 0x06009FB0 RID: 40880 RVA: 0x00158BE4 File Offset: 0x00156DE4
		public void AddEffectToChildren()
		{
			foreach (Graphic graphic in base.GetComponentsInChildren<Graphic>(true))
			{
				if (graphic.GetComponent<CurvedUIVertexEffect>() == null)
				{
					graphic.gameObject.AddComponent<CurvedUIVertexEffect>();
					graphic.SetAllDirty();
				}
			}
			foreach (InputField inputField in base.GetComponentsInChildren<InputField>(true))
			{
				if (inputField.GetComponent<CurvedUIInputFieldCaret>() == null)
				{
					inputField.gameObject.AddComponent<CurvedUIInputFieldCaret>();
				}
			}
			foreach (TextMeshProUGUI textMeshProUGUI in base.GetComponentsInChildren<TextMeshProUGUI>(true))
			{
				if (textMeshProUGUI.GetComponent<CurvedUITMP>() == null)
				{
					textMeshProUGUI.gameObject.AddComponent<CurvedUITMP>();
					textMeshProUGUI.SetAllDirty();
				}
			}
		}

		// Token: 0x06009FB1 RID: 40881 RVA: 0x0015BC3C File Offset: 0x00159E3C
		public Vector3 VertexPositionToCurvedCanvas(Vector3 pos)
		{
			Vector3 vector;
			switch (this.Shape)
			{
			case CurvedUISettings.CurvedUIShape.CYLINDER:
				vector = this.CanvasToCyllinder(pos);
				break;
			case CurvedUISettings.CurvedUIShape.RING:
				vector = this.CanvasToRing(pos);
				break;
			case CurvedUISettings.CurvedUIShape.SPHERE:
				vector = this.CanvasToSphere(pos);
				break;
			case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
				vector = this.CanvasToCyllinderVertical(pos);
				break;
			default:
				vector = Vector3.zero;
				break;
			}
			return vector;
		}

		// Token: 0x06009FB2 RID: 40882 RVA: 0x0015BC98 File Offset: 0x00159E98
		public Vector3 CanvasToCurvedCanvas(Vector3 pos)
		{
			pos = this.VertexPositionToCurvedCanvas(pos);
			if (float.IsNaN(pos.x) || float.IsInfinity(pos.x))
			{
				return Vector3.zero;
			}
			return base.transform.localToWorldMatrix.MultiplyPoint3x4(pos);
		}

		// Token: 0x06009FB3 RID: 40883 RVA: 0x0015BCE4 File Offset: 0x00159EE4
		public Vector3 CanvasToCurvedCanvasNormal(Vector3 pos)
		{
			pos = this.VertexPositionToCurvedCanvas(pos);
			switch (this.Shape)
			{
			case CurvedUISettings.CurvedUIShape.CYLINDER:
				return base.transform.localToWorldMatrix.MultiplyVector((pos - new Vector3(0f, 0f, 0f - this.GetCyllinderRadiusInCanvasSpace())).ModifyY(0f)).normalized;
			case CurvedUISettings.CurvedUIShape.RING:
				return -base.transform.forward;
			case CurvedUISettings.CurvedUIShape.SPHERE:
			{
				Vector3 vector = ((!this.PreserveAspect) ? Vector3.zero : new Vector3(0f, 0f, 0f - this.GetCyllinderRadiusInCanvasSpace()));
				return base.transform.localToWorldMatrix.MultiplyVector(pos - vector).normalized;
			}
			case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
				return base.transform.localToWorldMatrix.MultiplyVector((pos - new Vector3(0f, 0f, 0f - this.GetCyllinderRadiusInCanvasSpace())).ModifyX(0f)).normalized;
			default:
				return Vector3.zero;
			}
		}

		// Token: 0x06009FB4 RID: 40884 RVA: 0x0015BE10 File Offset: 0x0015A010
		public bool RaycastToCanvasSpace(Ray ray, out Vector2 o_positionOnCanvas)
		{
			CurvedUIRaycaster component = base.GetComponent<CurvedUIRaycaster>();
			o_positionOnCanvas = Vector2.zero;
			bool flag;
			switch (this.Shape)
			{
			case CurvedUISettings.CurvedUIShape.CYLINDER:
				flag = component.RaycastToCyllinderCanvas(ray, out o_positionOnCanvas, true);
				break;
			case CurvedUISettings.CurvedUIShape.RING:
				flag = component.RaycastToRingCanvas(ray, out o_positionOnCanvas, true);
				break;
			case CurvedUISettings.CurvedUIShape.SPHERE:
				flag = component.RaycastToSphereCanvas(ray, out o_positionOnCanvas, true);
				break;
			case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
				flag = component.RaycastToCyllinderVerticalCanvas(ray, out o_positionOnCanvas, true);
				break;
			default:
				flag = false;
				break;
			}
			return flag;
		}

		// Token: 0x06009FB5 RID: 40885 RVA: 0x0015BE84 File Offset: 0x0015A084
		public float GetCyllinderRadiusInCanvasSpace()
		{
			float num = ((!this.PreserveAspect) ? (this.RectTransform.rect.size.x * 0.5f / Mathf.Sin(Mathf.Clamp((float)this.angle, -180f, 180f) * 0.5f * 0.017453292f)) : ((this.shape != CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL) ? (this.RectTransform.rect.size.x / (6.2831855f * ((float)this.angle / 360f))) : (this.RectTransform.rect.size.y / (6.2831855f * ((float)this.angle / 360f)))));
			if (this.angle == 0)
			{
				return 0f;
			}
			return num;
		}

		// Token: 0x06009FB6 RID: 40886 RVA: 0x0015BF54 File Offset: 0x0015A154
		public Vector2 GetTesslationSize(bool UnmodifiedByQuality = false)
		{
			Vector2 size = this.RectTransform.rect.size;
			float num = size.x;
			float num2 = size.y;
			if (this.Angle != 0 || (!this.PreserveAspect && this.vertAngle != 0))
			{
				switch (this.shape)
				{
				case CurvedUISettings.CurvedUIShape.CYLINDER:
				case CurvedUISettings.CurvedUIShape.RING:
				case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
					num = Mathf.Min(size.x / 4f, size.x / (Mathf.Abs(this.angle).Remap(0f, 360f, 0f, 1f) * (float)this.BaseCircleSegments));
					num2 = Mathf.Min(size.y / 4f, size.y / (Mathf.Abs(this.angle).Remap(0f, 360f, 0f, 1f) * (float)this.BaseCircleSegments));
					break;
				case CurvedUISettings.CurvedUIShape.SPHERE:
					num = Mathf.Min(size.x / 4f, size.x / (Mathf.Abs(this.angle).Remap(0f, 360f, 0f, 1f) * (float)this.BaseCircleSegments * 0.5f));
					num2 = ((!this.PreserveAspect) ? ((this.VerticalAngle != 0) ? (size.y / (Mathf.Abs(this.VerticalAngle).Remap(0f, 180f, 0f, 1f) * (float)this.BaseCircleSegments * 0.5f)) : 10000f) : (num * size.y / size.x));
					break;
				}
			}
			return new Vector2(num, num2) / ((!UnmodifiedByQuality) ? Mathf.Clamp(this.Quality, 0.01f, 10f) : 1f);
		}

		// Token: 0x06009FB7 RID: 40887 RVA: 0x0015C12C File Offset: 0x0015A32C
		public void SetAllChildrenDirty(bool recalculateCurveOnly = false)
		{
			foreach (CurvedUIVertexEffect curvedUIVertexEffect in base.GetComponentsInChildren<CurvedUIVertexEffect>())
			{
				if (recalculateCurveOnly)
				{
					curvedUIVertexEffect.SetDirty();
				}
				else
				{
					curvedUIVertexEffect.CurvingRequired = true;
				}
			}
		}

		// Token: 0x06009FB8 RID: 40888 RVA: 0x0005CBBC File Offset: 0x0005ADBC
		public void Click()
		{
			if (base.GetComponent<CurvedUIRaycaster>() != null)
			{
				base.GetComponent<CurvedUIRaycaster>().Click();
			}
		}

		// Token: 0x06009FB9 RID: 40889 RVA: 0x0005CBD7 File Offset: 0x0005ADD7
		public List<GameObject> GetObjectsUnderPointer()
		{
			if (base.GetComponent<CurvedUIRaycaster>() != null)
			{
				return base.GetComponent<CurvedUIRaycaster>().GetObjectsUnderPointer();
			}
			return new List<GameObject>();
		}

		// Token: 0x06009FBA RID: 40890 RVA: 0x0005CBF8 File Offset: 0x0005ADF8
		public List<GameObject> GetObjectsUnderScreenPos(Vector2 pos, Camera eventCamera = null)
		{
			if (eventCamera == null)
			{
				eventCamera = this.myCanvas.worldCamera;
			}
			if (base.GetComponent<CurvedUIRaycaster>() != null)
			{
				return base.GetComponent<CurvedUIRaycaster>().GetObjectsUnderScreenPos(pos, eventCamera);
			}
			return new List<GameObject>();
		}

		// Token: 0x06009FBB RID: 40891 RVA: 0x0005CC31 File Offset: 0x0005AE31
		public List<GameObject> GetObjectsHitByRay(Ray ray)
		{
			if (base.GetComponent<CurvedUIRaycaster>() != null)
			{
				return base.GetComponent<CurvedUIRaycaster>().GetObjectsHitByRay(ray);
			}
			return new List<GameObject>();
		}

		// Token: 0x04006728 RID: 26408
		[SerializeField]
		private CurvedUISettings.CurvedUIShape shape;

		// Token: 0x04006729 RID: 26409
		[SerializeField]
		private float quality = 1f;

		// Token: 0x0400672A RID: 26410
		[SerializeField]
		private bool interactable = true;

		// Token: 0x0400672B RID: 26411
		[SerializeField]
		private bool blocksRaycasts = true;

		// Token: 0x0400672C RID: 26412
		[SerializeField]
		private bool raycastMyLayerOnly = true;

		// Token: 0x0400672D RID: 26413
		[SerializeField]
		private bool forceUseBoxCollider = true;

		// Token: 0x0400672E RID: 26414
		[SerializeField]
		private int angle = 90;

		// Token: 0x0400672F RID: 26415
		[SerializeField]
		private bool preserveAspect = true;

		// Token: 0x04006730 RID: 26416
		[SerializeField]
		private int vertAngle = 90;

		// Token: 0x04006731 RID: 26417
		[SerializeField]
		private float ringFill = 0.5f;

		// Token: 0x04006732 RID: 26418
		[SerializeField]
		private int ringExternalDiamater = 1000;

		// Token: 0x04006733 RID: 26419
		[SerializeField]
		private bool ringFlipVertical;

		// Token: 0x04006734 RID: 26420
		private RectTransform m_rectTransform;

		// Token: 0x04006735 RID: 26421
		private Canvas myCanvas;

		// Token: 0x04006736 RID: 26422
		private float savedRadius;

		// Token: 0x04006737 RID: 26423
		private Vector2 savedRectSize;

		// Token: 0x02002CC4 RID: 11460
		[NullableContext(0)]
		public enum CurvedUIShape
		{
			// Token: 0x0400673A RID: 26426
			CYLINDER,
			// Token: 0x0400673B RID: 26427
			RING,
			// Token: 0x0400673C RID: 26428
			SPHERE,
			// Token: 0x0400673D RID: 26429
			CYLINDER_VERTICAL
		}
	}
}
