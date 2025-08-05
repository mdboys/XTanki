using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CurvedUI
{
	// Token: 0x02002CC1 RID: 11457
	[NullableContext(1)]
	[Nullable(0)]
	public class CurvedUIRaycaster : GraphicRaycaster
	{
		// Token: 0x06009F5E RID: 40798 RVA: 0x001590EC File Offset: 0x001572EC
		protected override void Awake()
		{
			base.Awake();
			this.myCanvas = base.GetComponent<Canvas>();
			this.mySettings = base.GetComponent<CurvedUISettings>();
			this.cyllinderMidPoint = new Vector3(0f, 0f, 0f - this.mySettings.GetCyllinderRadiusInCanvasSpace());
			if (this.myCanvas.worldCamera == null && Camera.main != null)
			{
				this.myCanvas.worldCamera = Camera.main;
			}
		}

		// Token: 0x06009F5F RID: 40799 RVA: 0x0005C840 File Offset: 0x0005AA40
		protected override void Start()
		{
			this.CreateCollider();
		}

		// Token: 0x06009F60 RID: 40800 RVA: 0x00159170 File Offset: 0x00157370
		protected virtual void Update()
		{
			if (this.pointingAtCanvas && CurvedUIInputModule.ControlMethod == CurvedUIInputModule.CUIControlMethod.GAZE && CurvedUIInputModule.Instance.GazeUseTimedClick)
			{
				this.ProcessGazeTimedClick();
				this.selectablesUnderGazeLastFrame.Clear();
				this.selectablesUnderGazeLastFrame.AddRange(this.selectablesUnderGaze);
				this.selectablesUnderGaze.Clear();
				this.selectablesUnderGaze.AddRange(this.objectsUnderPointer);
				this.selectablesUnderGaze.RemoveAll((GameObject obj) => obj.GetComponent<Selectable>() == null);
				if (CurvedUIInputModule.Instance.GazeTimedClickProgressImage != null)
				{
					if (CurvedUIInputModule.Instance.GazeTimedClickProgressImage.type != Image.Type.Filled)
					{
						CurvedUIInputModule.Instance.GazeTimedClickProgressImage.type = Image.Type.Filled;
					}
					CurvedUIInputModule.Instance.GazeTimedClickProgressImage.fillAmount = (Time.time - this.objectsUnderGazeLastChangeTime).RemapAndClamp(CurvedUIInputModule.Instance.GazeClickTimerDelay, CurvedUIInputModule.Instance.GazeClickTimer + CurvedUIInputModule.Instance.GazeClickTimerDelay, 0f, 1f);
				}
			}
			this.pointingAtCanvas = false;
		}

		// Token: 0x06009F61 RID: 40801 RVA: 0x00159290 File Offset: 0x00157490
		private void ProcessGazeTimedClick()
		{
			if (this.selectablesUnderGazeLastFrame.Count == 0 || this.selectablesUnderGazeLastFrame.Count != this.selectablesUnderGaze.Count)
			{
				this.ResetGazeTimedClick();
				return;
			}
			int num = 0;
			while (num < this.selectablesUnderGazeLastFrame.Count && num < this.selectablesUnderGaze.Count)
			{
				if (this.selectablesUnderGazeLastFrame[num].GetInstanceID() != this.selectablesUnderGaze[num].GetInstanceID())
				{
					this.ResetGazeTimedClick();
					return;
				}
				num++;
			}
			if (!this.gazeClickExecuted && Time.time > this.objectsUnderGazeLastChangeTime + CurvedUIInputModule.Instance.GazeClickTimer + CurvedUIInputModule.Instance.GazeClickTimerDelay)
			{
				this.Click();
				this.gazeClickExecuted = true;
			}
		}

		// Token: 0x06009F62 RID: 40802 RVA: 0x0005C848 File Offset: 0x0005AA48
		private void ResetGazeTimedClick()
		{
			this.objectsUnderGazeLastChangeTime = Time.time;
			this.gazeClickExecuted = false;
		}

		// Token: 0x06009F63 RID: 40803 RVA: 0x00159350 File Offset: 0x00157550
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (!this.mySettings.Interactable)
			{
				return;
			}
			if (this.myCanvas.worldCamera == null)
			{
				Debug.LogWarning("CurvedUIRaycaster requires Canvas to have a world camera reference to process events!", this.myCanvas.gameObject);
			}
			Camera worldCamera = this.myCanvas.worldCamera;
			Ray ray;
			switch (CurvedUIInputModule.ControlMethod)
			{
			case CurvedUIInputModule.CUIControlMethod.MOUSE:
				ray = worldCamera.ScreenPointToRay(eventData.position);
				goto IL_011E;
			case CurvedUIInputModule.CUIControlMethod.GAZE:
				break;
			case CurvedUIInputModule.CUIControlMethod.WORLD_MOUSE:
				ray = new Ray(worldCamera.transform.position, this.mySettings.CanvasToCurvedCanvas(CurvedUIInputModule.Instance.WorldSpaceMouseInCanvasSpace) - this.myCanvas.worldCamera.transform.position);
				goto IL_011E;
			case CurvedUIInputModule.CUIControlMethod.CUSTOM_RAY:
			case CurvedUIInputModule.CUIControlMethod.VIVE:
			case CurvedUIInputModule.CUIControlMethod.OCULUS_TOUCH:
				ray = CurvedUIInputModule.CustomControllerRay;
				this.UpdateSelectedObjects(eventData);
				goto IL_011E;
			case (CurvedUIInputModule.CUIControlMethod)6:
				goto IL_0116;
			case CurvedUIInputModule.CUIControlMethod.GOOGLEVR:
				Debug.LogError("CURVEDUI: Missing GoogleVR support code. Enable GoogleVR control method on CurvedUISettings component.");
				break;
			default:
				goto IL_0116;
			}
			ray = new Ray(worldCamera.transform.position, worldCamera.transform.forward);
			this.UpdateSelectedObjects(eventData);
			goto IL_011E;
			IL_0116:
			ray = default(Ray);
			IL_011E:
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			if (!this.overrideEventData)
			{
				pointerEventData.pointerEnter = eventData.pointerEnter;
				pointerEventData.rawPointerPress = eventData.rawPointerPress;
				pointerEventData.pointerDrag = eventData.pointerDrag;
				pointerEventData.pointerCurrentRaycast = eventData.pointerCurrentRaycast;
				pointerEventData.pointerPressRaycast = eventData.pointerPressRaycast;
				pointerEventData.hovered = new List<GameObject>();
				pointerEventData.hovered.AddRange(eventData.hovered);
				pointerEventData.eligibleForClick = eventData.eligibleForClick;
				pointerEventData.pointerId = eventData.pointerId;
				pointerEventData.position = eventData.position;
				pointerEventData.delta = eventData.delta;
				pointerEventData.pressPosition = eventData.pressPosition;
				pointerEventData.clickTime = eventData.clickTime;
				pointerEventData.clickCount = eventData.clickCount;
				pointerEventData.scrollDelta = eventData.scrollDelta;
				pointerEventData.useDragThreshold = eventData.useDragThreshold;
				pointerEventData.dragging = eventData.dragging;
				pointerEventData.button = eventData.button;
			}
			if (this.mySettings.Angle != 0 && this.mySettings.enabled)
			{
				Vector2 position = eventData.position;
				switch (this.mySettings.Shape)
				{
				case CurvedUISettings.CurvedUIShape.CYLINDER:
					if (!this.RaycastToCyllinderCanvas(ray, out position, false))
					{
						return;
					}
					break;
				case CurvedUISettings.CurvedUIShape.RING:
					if (!this.RaycastToRingCanvas(ray, out position, false))
					{
						return;
					}
					break;
				case CurvedUISettings.CurvedUIShape.SPHERE:
					if (!this.RaycastToSphereCanvas(ray, out position, false))
					{
						return;
					}
					break;
				case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
					if (!this.RaycastToCyllinderVerticalCanvas(ray, out position, false))
					{
						return;
					}
					break;
				}
				this.pointingAtCanvas = true;
				PointerEventData pointerEventData2 = ((!this.overrideEventData) ? pointerEventData : eventData);
				if (pointerEventData2.pressPosition == pointerEventData2.position)
				{
					pointerEventData2.pressPosition = position;
				}
				pointerEventData2.position = position;
				if (CurvedUIInputModule.ControlMethod == CurvedUIInputModule.CUIControlMethod.VIVE)
				{
					pointerEventData2.delta = position - this.lastCanvasPos;
					this.lastCanvasPos = position;
				}
			}
			this.objectsUnderPointer = eventData.hovered;
			base.Raycast((!this.overrideEventData) ? pointerEventData : eventData, resultAppendList);
		}

		// Token: 0x06009F64 RID: 40804 RVA: 0x00159670 File Offset: 0x00157870
		private LayerMask GetLayerMaskForMyLayer()
		{
			int num = -1;
			if (this.mySettings.RaycastMyLayerOnly)
			{
				num = 1 << base.gameObject.layer;
			}
			return num;
		}

		// Token: 0x06009F65 RID: 40805 RVA: 0x001596A4 File Offset: 0x001578A4
		public virtual bool RaycastToCyllinderCanvas(Ray ray3D, out Vector2 o_canvasPos, bool OutputInCanvasSpace = false)
		{
			if (this.showDebug)
			{
				Debug.DrawLine(ray3D.origin, ray3D.GetPoint(1000f), Color.red);
			}
			LayerMask layerMaskForMyLayer = this.GetLayerMaskForMyLayer();
			RaycastHit raycastHit;
			if (!Physics.Raycast(ray3D, out raycastHit, float.PositiveInfinity, layerMaskForMyLayer))
			{
				o_canvasPos = Vector2.zero;
				return false;
			}
			if (this.overrideEventData && raycastHit.collider.gameObject != base.gameObject && (this.colliderContainer == null || raycastHit.collider.transform.parent != this.colliderContainer.transform))
			{
				o_canvasPos = Vector2.zero;
				return false;
			}
			Vector3 vector = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(raycastHit.point);
			Vector3 normalized = (vector - this.cyllinderMidPoint).normalized;
			float num = 0f - this.AngleSigned(normalized.ModifyY(0f), (this.mySettings.Angle >= 0) ? Vector3.forward : Vector3.back, Vector3.up);
			Vector2 size = this.myCanvas.GetComponent<RectTransform>().rect.size;
			Vector2 vector2 = new Vector3(0f, 0f, 0f);
			vector2.x = num.Remap((float)(-(float)this.mySettings.Angle) / 2f, (float)this.mySettings.Angle / 2f, (0f - size.x) / 2f, size.x / 2f);
			vector2.y = vector.y;
			if (OutputInCanvasSpace)
			{
				o_canvasPos = vector2;
			}
			else
			{
				o_canvasPos = this.myCanvas.worldCamera.WorldToScreenPoint(this.myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(vector2));
			}
			if (this.showDebug)
			{
				Debug.DrawLine(raycastHit.point, raycastHit.point.ModifyY(raycastHit.point.y + 10f), Color.green);
				Debug.DrawLine(raycastHit.point, this.myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(this.cyllinderMidPoint), Color.yellow);
			}
			return true;
		}

		// Token: 0x06009F66 RID: 40806 RVA: 0x00159918 File Offset: 0x00157B18
		public virtual bool RaycastToCyllinderVerticalCanvas(Ray ray3D, out Vector2 o_canvasPos, bool OutputInCanvasSpace = false)
		{
			if (this.showDebug)
			{
				Debug.DrawLine(ray3D.origin, ray3D.GetPoint(1000f), Color.red);
			}
			LayerMask layerMaskForMyLayer = this.GetLayerMaskForMyLayer();
			RaycastHit raycastHit;
			if (!Physics.Raycast(ray3D, out raycastHit, float.PositiveInfinity, layerMaskForMyLayer))
			{
				o_canvasPos = Vector2.zero;
				return false;
			}
			if (this.overrideEventData && raycastHit.collider.gameObject != base.gameObject && (this.colliderContainer == null || raycastHit.collider.transform.parent != this.colliderContainer.transform))
			{
				o_canvasPos = Vector2.zero;
				return false;
			}
			Vector3 vector = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(raycastHit.point);
			Vector3 normalized = (vector - this.cyllinderMidPoint).normalized;
			float num = 0f - this.AngleSigned(normalized.ModifyX(0f), (this.mySettings.Angle >= 0) ? Vector3.forward : Vector3.back, Vector3.left);
			Vector2 size = this.myCanvas.GetComponent<RectTransform>().rect.size;
			Vector2 vector2 = new Vector3(0f, 0f, 0f);
			vector2.y = num.Remap((float)(-(float)this.mySettings.Angle) / 2f, (float)this.mySettings.Angle / 2f, (0f - size.y) / 2f, size.y / 2f);
			vector2.x = vector.x;
			if (OutputInCanvasSpace)
			{
				o_canvasPos = vector2;
			}
			else
			{
				o_canvasPos = this.myCanvas.worldCamera.WorldToScreenPoint(this.myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(vector2));
			}
			if (this.showDebug)
			{
				Debug.DrawLine(raycastHit.point, raycastHit.point.ModifyY(raycastHit.point.y + 10f), Color.green);
				Debug.DrawLine(raycastHit.point, this.myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(this.cyllinderMidPoint), Color.yellow);
			}
			return true;
		}

		// Token: 0x06009F67 RID: 40807 RVA: 0x00159B8C File Offset: 0x00157D8C
		public virtual bool RaycastToRingCanvas(Ray ray3D, out Vector2 o_canvasPos, bool OutputInCanvasSpace = false)
		{
			LayerMask layerMaskForMyLayer = this.GetLayerMaskForMyLayer();
			RaycastHit raycastHit;
			if (!Physics.Raycast(ray3D, out raycastHit, float.PositiveInfinity, layerMaskForMyLayer))
			{
				o_canvasPos = Vector2.zero;
				return false;
			}
			if (this.overrideEventData && raycastHit.collider.gameObject != base.gameObject && (this.colliderContainer == null || raycastHit.collider.transform.parent != this.colliderContainer.transform))
			{
				o_canvasPos = Vector2.zero;
				return false;
			}
			Vector3 vector = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(raycastHit.point);
			Vector3 normalized = vector.ModifyZ(0f).normalized;
			Vector2 size = this.myCanvas.GetComponent<RectTransform>().rect.size;
			float num = 0f - this.AngleSigned(normalized.ModifyZ(0f), Vector3.up, Vector3.back);
			Vector2 vector2 = new Vector2(0f, 0f);
			if (this.showDebug)
			{
				Debug.Log(string.Format("angle: {0}", num));
			}
			if (num < 0f)
			{
				vector2.x = num.Remap(0f, (float)(-(float)this.mySettings.Angle), (0f - size.x) / 2f, size.x / 2f);
			}
			else
			{
				vector2.x = num.Remap(360f, (float)(360 - this.mySettings.Angle), (0f - size.x) / 2f, size.x / 2f);
			}
			vector2.y = vector.magnitude.Remap((float)this.mySettings.RingExternalDiameter * 0.5f * (1f - this.mySettings.RingFill), (float)this.mySettings.RingExternalDiameter * 0.5f, (0f - size.y) * 0.5f * (float)((!this.mySettings.RingFlipVertical) ? 1 : (-1)), size.y * 0.5f * (float)((!this.mySettings.RingFlipVertical) ? 1 : (-1)));
			if (OutputInCanvasSpace)
			{
				o_canvasPos = vector2;
			}
			else
			{
				o_canvasPos = this.myCanvas.worldCamera.WorldToScreenPoint(this.myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(vector2));
			}
			return true;
		}

		// Token: 0x06009F68 RID: 40808 RVA: 0x00159E30 File Offset: 0x00158030
		public virtual bool RaycastToSphereCanvas(Ray ray3D, out Vector2 o_canvasPos, bool OutputInCanvasSpace = false)
		{
			LayerMask layerMaskForMyLayer = this.GetLayerMaskForMyLayer();
			RaycastHit raycastHit;
			if (!Physics.Raycast(ray3D, out raycastHit, float.PositiveInfinity, layerMaskForMyLayer))
			{
				o_canvasPos = Vector2.zero;
				return false;
			}
			if (this.overrideEventData && raycastHit.collider.gameObject != base.gameObject && (this.colliderContainer == null || raycastHit.collider.transform.parent != this.colliderContainer.transform))
			{
				o_canvasPos = Vector2.zero;
				return false;
			}
			Vector2 size = this.myCanvas.GetComponent<RectTransform>().rect.size;
			float num = ((!this.mySettings.PreserveAspect) ? (size.x / 2f) : this.mySettings.GetCyllinderRadiusInCanvasSpace());
			Vector3 vector = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(raycastHit.point);
			Vector3 vector2 = new Vector3(0f, 0f, (!this.mySettings.PreserveAspect) ? 0f : (0f - num));
			Vector3 normalized = (vector - vector2).normalized;
			Vector3 vector3 = Vector3.Cross(normalized, normalized.ModifyY(0f)).normalized * (float)((normalized.y < 0f) ? 1 : (-1));
			float num2 = 0f - this.AngleSigned(normalized.ModifyY(0f), (this.mySettings.Angle <= 0) ? Vector3.back : Vector3.forward, (this.mySettings.Angle <= 0) ? Vector3.down : Vector3.up);
			float num3 = 0f - this.AngleSigned(normalized, normalized.ModifyY(0f), vector3);
			float num4 = (float)Mathf.Abs(this.mySettings.Angle) * 0.5f;
			float num5 = Mathf.Abs((!this.mySettings.PreserveAspect) ? ((float)this.mySettings.VerticalAngle * 0.5f) : (num4 * size.y / size.x));
			Vector2 vector4 = new Vector2(num2.Remap(0f - num4, num4, (0f - size.x) * 0.5f, size.x * 0.5f), num3.Remap(0f - num5, num5, (0f - size.y) * 0.5f, size.y * 0.5f));
			if (this.showDebug)
			{
				Debug.Log(string.Format("h: {0} / v: {1} poc: {2}", num2, num3, vector4));
				Debug.DrawRay(this.myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(vector2), this.myCanvas.transform.localToWorldMatrix.MultiplyVector(normalized) * Mathf.Abs(num), Color.red);
				Debug.DrawRay(this.myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(vector2), this.myCanvas.transform.localToWorldMatrix.MultiplyVector(vector3) * 300f, Color.magenta);
			}
			if (OutputInCanvasSpace)
			{
				o_canvasPos = vector4;
			}
			else
			{
				o_canvasPos = this.myCanvas.worldCamera.WorldToScreenPoint(this.myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(vector4));
			}
			return true;
		}

		// Token: 0x06009F69 RID: 40809 RVA: 0x0015A1CC File Offset: 0x001583CC
		protected void CreateCollider()
		{
			List<Collider> list = new List<Collider>();
			list.AddRange(base.GetComponents<Collider>());
			for (int i = 0; i < list.Count; i++)
			{
				global::UnityEngine.Object.Destroy(list[i]);
			}
			if (this.mySettings.BlocksRaycasts)
			{
				if (this.mySettings.Shape == CurvedUISettings.CurvedUIShape.SPHERE)
				{
					CurvedUISettings curvedUISettings = this.mySettings;
					if (curvedUISettings != null && !curvedUISettings.PreserveAspect && curvedUISettings.VerticalAngle == 0)
					{
						return;
					}
				}
				switch (this.mySettings.Shape)
				{
				case CurvedUISettings.CurvedUIShape.CYLINDER:
					if (this.mySettings.ForceUseBoxCollider || base.GetComponent<Rigidbody>() != null || base.GetComponentInParent<Rigidbody>() != null)
					{
						if (this.colliderContainer != null)
						{
							global::UnityEngine.Object.Destroy(this.colliderContainer);
						}
						this.colliderContainer = this.CreateConvexCyllinderCollider(false);
						return;
					}
					this.SetupMeshColliderUsingMesh(this.CreateCyllinderColliderMesh(false));
					return;
				case CurvedUISettings.CurvedUIShape.RING:
					base.gameObject.AddComponent<BoxCollider>().size = new Vector3((float)this.mySettings.RingExternalDiameter, (float)this.mySettings.RingExternalDiameter, 1f);
					return;
				case CurvedUISettings.CurvedUIShape.SPHERE:
					if (base.GetComponent<Rigidbody>() != null || base.GetComponentInParent<Rigidbody>() != null)
					{
						Debug.LogWarning("CurvedUI: Sphere shape canvases as children of rigidbodies do not support user input. Switch to Cyllinder shape or remove the rigidbody from parent.", base.gameObject);
					}
					this.SetupMeshColliderUsingMesh(this.CreateSphereColliderMesh());
					return;
				case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
					if (this.mySettings.ForceUseBoxCollider || base.GetComponent<Rigidbody>() != null || base.GetComponentInParent<Rigidbody>() != null)
					{
						if (this.colliderContainer != null)
						{
							global::UnityEngine.Object.Destroy(this.colliderContainer);
						}
						this.colliderContainer = this.CreateConvexCyllinderCollider(true);
						return;
					}
					this.SetupMeshColliderUsingMesh(this.CreateCyllinderColliderMesh(true));
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06009F6A RID: 40810 RVA: 0x0015A388 File Offset: 0x00158588
		private void SetupMeshColliderUsingMesh(Mesh meshie)
		{
			MeshFilter meshFilter = this.AddComponentIfMissing<MeshFilter>();
			MeshCollider meshCollider = base.gameObject.AddComponent<MeshCollider>();
			meshFilter.mesh = meshie;
			meshCollider.sharedMesh = meshie;
		}

		// Token: 0x06009F6B RID: 40811 RVA: 0x0015A3B4 File Offset: 0x001585B4
		private GameObject CreateConvexCyllinderCollider(bool vertical = false)
		{
			GameObject gameObject = new GameObject("_CurvedUIColliders")
			{
				layer = base.gameObject.layer
			};
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.ResetTransform();
			Mesh mesh = new Mesh();
			Vector3[] array = new Vector3[4];
			(this.myCanvas.transform as RectTransform).GetWorldCorners(array);
			mesh.vertices = array;
			if (vertical)
			{
				array[0] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[1]);
				array[1] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[2]);
				array[2] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[0]);
				array[3] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[3]);
			}
			else
			{
				array[0] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[1]);
				array[1] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[0]);
				array[2] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[2]);
				array[3] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[3]);
			}
			mesh.vertices = array;
			List<Vector3> list = new List<Vector3>();
			int num = Mathf.Max(8, Mathf.RoundToInt((float)(this.mySettings.BaseCircleSegments * Mathf.Abs(this.mySettings.Angle)) / 360f));
			for (int i = 0; i < num; i++)
			{
				list.Add(Vector3.Lerp(mesh.vertices[0], mesh.vertices[2], (float)i * 1f / (float)(num - 1)));
			}
			if (this.mySettings.Angle != 0)
			{
				Rect rect = this.myCanvas.GetComponent<RectTransform>().rect;
				float cyllinderRadiusInCanvasSpace = this.mySettings.GetCyllinderRadiusInCanvasSpace();
				for (int j = 0; j < list.Count; j++)
				{
					Vector3 vector = list[j];
					if (vertical)
					{
						float num2 = list[j].y / rect.size.y * (float)this.mySettings.Angle * 0.017453292f;
						vector.y = Mathf.Sin(num2) * cyllinderRadiusInCanvasSpace;
						vector.z += Mathf.Cos(num2) * cyllinderRadiusInCanvasSpace - cyllinderRadiusInCanvasSpace;
						list[j] = vector;
					}
					else
					{
						float num3 = list[j].x / rect.size.x * (float)this.mySettings.Angle * 0.017453292f;
						vector.x = Mathf.Sin(num3) * cyllinderRadiusInCanvasSpace;
						vector.z += Mathf.Cos(num3) * cyllinderRadiusInCanvasSpace - cyllinderRadiusInCanvasSpace;
						list[j] = vector;
					}
				}
			}
			for (int k = 0; k < list.Count - 1; k++)
			{
				GameObject gameObject2 = new GameObject("Box collider")
				{
					layer = base.gameObject.layer
				};
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.ResetTransform();
				gameObject2.AddComponent<BoxCollider>();
				if (vertical)
				{
					gameObject2.transform.localPosition = new Vector3(0f, (list[k + 1].y + list[k].y) * 0.5f, (list[k + 1].z + list[k].z) * 0.5f);
					gameObject2.transform.localScale = new Vector3(0.1f, Vector3.Distance(array[0], array[1]), Vector3.Distance(list[k + 1], list[k]));
					gameObject2.transform.localRotation = Quaternion.LookRotation(list[k + 1] - list[k], array[0] - array[1]);
				}
				else
				{
					gameObject2.transform.localPosition = new Vector3((list[k + 1].x + list[k].x) * 0.5f, 0f, (list[k + 1].z + list[k].z) * 0.5f);
					gameObject2.transform.localScale = new Vector3(0.1f, Vector3.Distance(array[0], array[1]), Vector3.Distance(list[k + 1], list[k]));
					gameObject2.transform.localRotation = Quaternion.LookRotation(list[k + 1] - list[k], array[0] - array[1]);
				}
			}
			return gameObject;
		}

		// Token: 0x06009F6C RID: 40812 RVA: 0x0015A948 File Offset: 0x00158B48
		private Mesh CreateCyllinderColliderMesh(bool vertical = false)
		{
			Mesh mesh = new Mesh();
			Vector3[] array = new Vector3[4];
			(this.myCanvas.transform as RectTransform).GetWorldCorners(array);
			mesh.vertices = array;
			if (vertical)
			{
				array[0] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[1]);
				array[1] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[2]);
				array[2] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[0]);
				array[3] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[3]);
			}
			else
			{
				array[0] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[1]);
				array[1] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[0]);
				array[2] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[2]);
				array[3] = this.myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(mesh.vertices[3]);
			}
			mesh.vertices = array;
			List<Vector3> list = new List<Vector3>();
			int num = Mathf.Max(8, Mathf.RoundToInt((float)(this.mySettings.BaseCircleSegments * Mathf.Abs(this.mySettings.Angle)) / 360f));
			for (int i = 0; i < num; i++)
			{
				list.Add(Vector3.Lerp(mesh.vertices[0], mesh.vertices[2], (float)i * 1f / (float)(num - 1)));
				list.Add(Vector3.Lerp(mesh.vertices[1], mesh.vertices[3], (float)i * 1f / (float)(num - 1)));
			}
			if (this.mySettings.Angle != 0)
			{
				Rect rect = this.myCanvas.GetComponent<RectTransform>().rect;
				float cyllinderRadiusInCanvasSpace = base.GetComponent<CurvedUISettings>().GetCyllinderRadiusInCanvasSpace();
				for (int j = 0; j < list.Count; j++)
				{
					Vector3 vector = list[j];
					if (vertical)
					{
						float num2 = list[j].y / rect.size.y * (float)this.mySettings.Angle * 0.017453292f;
						vector.y = Mathf.Sin(num2) * cyllinderRadiusInCanvasSpace;
						vector.z += Mathf.Cos(num2) * cyllinderRadiusInCanvasSpace - cyllinderRadiusInCanvasSpace;
						list[j] = vector;
					}
					else
					{
						float num3 = list[j].x / rect.size.x * (float)this.mySettings.Angle * 0.017453292f;
						vector.x = Mathf.Sin(num3) * cyllinderRadiusInCanvasSpace;
						vector.z += Mathf.Cos(num3) * cyllinderRadiusInCanvasSpace - cyllinderRadiusInCanvasSpace;
						list[j] = vector;
					}
				}
			}
			mesh.vertices = list.ToArray();
			List<int> list2 = new List<int>();
			for (int k = 0; k < list.Count / 2 - 1; k++)
			{
				if (vertical)
				{
					list2.Add(k * 2);
					list2.Add(k * 2 + 1);
					list2.Add(k * 2 + 2);
					list2.Add(k * 2 + 1);
					list2.Add(k * 2 + 3);
					list2.Add(k * 2 + 2);
				}
				else
				{
					list2.Add(k * 2 + 2);
					list2.Add(k * 2 + 1);
					list2.Add(k * 2);
					list2.Add(k * 2 + 2);
					list2.Add(k * 2 + 3);
					list2.Add(k * 2 + 1);
				}
			}
			mesh.triangles = list2.ToArray();
			return mesh;
		}

		// Token: 0x06009F6D RID: 40813 RVA: 0x0015AD94 File Offset: 0x00158F94
		private Mesh CreateSphereColliderMesh()
		{
			Mesh mesh = new Mesh();
			Vector3[] array = new Vector3[4];
			(this.myCanvas.transform as RectTransform).GetWorldCorners(array);
			List<Vector3> list = array.ToList<Vector3>();
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = this.mySettings.transform.worldToLocalMatrix.MultiplyPoint3x4(list[i]);
			}
			if (this.mySettings.Angle != 0)
			{
				int count = list.Count;
				for (int j = 0; j < count; j += 4)
				{
					this.ModifyQuad(list, j, this.mySettings.GetTesslationSize(true));
				}
				list.RemoveRange(0, count);
				float num = (float)this.mySettings.VerticalAngle;
				float num2 = (float)this.mySettings.Angle;
				Vector2 size = (this.myCanvas.transform as RectTransform).rect.size;
				float num3 = this.mySettings.GetCyllinderRadiusInCanvasSpace();
				if (this.mySettings.PreserveAspect)
				{
					num = (float)this.mySettings.Angle * (size.y / size.x);
				}
				else
				{
					num3 = size.x / 2f;
				}
				for (int k = 0; k < list.Count; k++)
				{
					float num4 = (list[k].x / size.x).Remap(-0.5f, 0.5f, (180f - num2) / 2f - 90f, 180f - (180f - num2) / 2f - 90f);
					num4 *= 0.017453292f;
					float num5 = (list[k].y / size.y).Remap(-0.5f, 0.5f, (180f - num) / 2f, 180f - (180f - num) / 2f);
					num5 *= 0.017453292f;
					list[k] = new Vector3(Mathf.Sin(num5) * Mathf.Sin(num4) * num3, (0f - num3) * Mathf.Cos(num5), Mathf.Sin(num5) * Mathf.Cos(num4) * num3 + ((!this.mySettings.PreserveAspect) ? 0f : (0f - num3)));
				}
			}
			mesh.vertices = list.ToArray();
			List<int> list2 = new List<int>();
			for (int l = 0; l < list.Count; l += 4)
			{
				list2.Add(l);
				list2.Add(l + 1);
				list2.Add(l + 2);
				list2.Add(l + 3);
				list2.Add(l);
				list2.Add(l + 2);
			}
			mesh.triangles = list2.ToArray();
			return mesh;
		}

		// Token: 0x06009F6E RID: 40814 RVA: 0x0005C85C File Offset: 0x0005AA5C
		private float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
		{
			return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
		}

		// Token: 0x06009F6F RID: 40815 RVA: 0x0015B06C File Offset: 0x0015926C
		private bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x06009F70 RID: 40816 RVA: 0x0015B098 File Offset: 0x00159298
		protected virtual void ProcessMove(PointerEventData pointerEvent)
		{
			GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			this.HandlePointerExitAndEnter(pointerEvent, gameObject);
		}

		// Token: 0x06009F71 RID: 40817 RVA: 0x0015B0BC File Offset: 0x001592BC
		protected void UpdateSelectedObjects(PointerEventData eventData)
		{
			bool flag = false;
			using (List<GameObject>.Enumerator enumerator = eventData.hovered.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == eventData.selectedObject)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				eventData.selectedObject = null;
			}
			foreach (GameObject gameObject in eventData.hovered)
			{
				if (!(gameObject == null))
				{
					Graphic component = gameObject.GetComponent<Graphic>();
					if (gameObject.GetComponent<Selectable>() != null && component != null && component.depth != -1 && component.raycastTarget)
					{
						if (eventData.selectedObject != gameObject)
						{
							eventData.selectedObject = gameObject;
							break;
						}
						break;
					}
				}
			}
			if (this.mySettings.ControlMethod == CurvedUIInputModule.CUIControlMethod.GAZE && eventData.IsPointerMoving() && eventData.pointerDrag != null && !eventData.dragging && this.ShouldStartDrag(eventData.pressPosition, eventData.position, (float)EventSystem.current.pixelDragThreshold, eventData.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
				eventData.dragging = true;
			}
		}

		// Token: 0x06009F72 RID: 40818 RVA: 0x0015B218 File Offset: 0x00159418
		protected void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
		{
			if (newEnterTarget == null || currentPointerData.pointerEnter == null)
			{
				for (int i = 0; i < currentPointerData.hovered.Count; i++)
				{
					ExecuteEvents.Execute<IPointerExitHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerExitHandler);
				}
				currentPointerData.hovered.Clear();
				if (newEnterTarget == null)
				{
					currentPointerData.pointerEnter = newEnterTarget;
					return;
				}
			}
			if (currentPointerData.pointerEnter == newEnterTarget && newEnterTarget)
			{
				return;
			}
			GameObject gameObject = CurvedUIRaycaster.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			if (currentPointerData.pointerEnter != null)
			{
				Transform transform = currentPointerData.pointerEnter.transform;
				while (transform != null && (!(gameObject != null) || !(gameObject.transform == transform)))
				{
					ExecuteEvents.Execute<IPointerExitHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
					currentPointerData.hovered.Remove(transform.gameObject);
					transform = transform.parent;
				}
			}
			currentPointerData.pointerEnter = newEnterTarget;
			if (newEnterTarget != null)
			{
				Transform transform2 = newEnterTarget.transform;
				while (transform2 != null && transform2.gameObject != gameObject)
				{
					ExecuteEvents.Execute<IPointerEnterHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
					currentPointerData.hovered.Add(transform2.gameObject);
					transform2 = transform2.parent;
				}
			}
		}

		// Token: 0x06009F73 RID: 40819 RVA: 0x0015B36C File Offset: 0x0015956C
		protected static GameObject FindCommonRoot(GameObject g1, GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			Transform transform = g1.transform;
			while (transform != null)
			{
				Transform transform2 = g2.transform;
				while (transform2 != null)
				{
					if (transform == transform2)
					{
						return transform.gameObject;
					}
					transform2 = transform2.parent;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x06009F74 RID: 40820 RVA: 0x0015B3D0 File Offset: 0x001595D0
		private bool GetScreenSpacePointByRay(Ray ray, out Vector2 o_positionOnCanvas)
		{
			switch (this.mySettings.Shape)
			{
			case CurvedUISettings.CurvedUIShape.CYLINDER:
				return this.RaycastToCyllinderCanvas(ray, out o_positionOnCanvas, false);
			case CurvedUISettings.CurvedUIShape.RING:
				return this.RaycastToRingCanvas(ray, out o_positionOnCanvas, false);
			case CurvedUISettings.CurvedUIShape.SPHERE:
				return this.RaycastToSphereCanvas(ray, out o_positionOnCanvas, false);
			case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
				return this.RaycastToCyllinderVerticalCanvas(ray, out o_positionOnCanvas, false);
			default:
				o_positionOnCanvas = Vector2.zero;
				return false;
			}
		}

		// Token: 0x06009F75 RID: 40821 RVA: 0x0005C87D File Offset: 0x0005AA7D
		public void RebuildCollider()
		{
			this.cyllinderMidPoint = new Vector3(0f, 0f, 0f - this.mySettings.GetCyllinderRadiusInCanvasSpace());
			this.CreateCollider();
		}

		// Token: 0x06009F76 RID: 40822 RVA: 0x0005C8AB File Offset: 0x0005AAAB
		public List<GameObject> GetObjectsUnderPointer()
		{
			if (this.objectsUnderPointer == null)
			{
				this.objectsUnderPointer = new List<GameObject>();
			}
			return this.objectsUnderPointer;
		}

		// Token: 0x06009F77 RID: 40823 RVA: 0x0005C8C6 File Offset: 0x0005AAC6
		public List<GameObject> GetObjectsUnderScreenPos(Vector2 screenPos, Camera eventCamera = null)
		{
			if (eventCamera == null)
			{
				eventCamera = this.myCanvas.worldCamera;
			}
			return this.GetObjectsHitByRay(eventCamera.ScreenPointToRay(screenPos));
		}

		// Token: 0x06009F78 RID: 40824 RVA: 0x0015B438 File Offset: 0x00159638
		public List<GameObject> GetObjectsHitByRay(Ray ray)
		{
			List<GameObject> list = new List<GameObject>();
			Vector2 vector;
			if (!this.GetScreenSpacePointByRay(ray, out vector))
			{
				return list;
			}
			List<Graphic> list2 = new List<Graphic>();
			IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(this.myCanvas);
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				Graphic graphic = graphicsForCanvas[i];
				if (graphic.depth != -1 && graphic.raycastTarget && RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, vector, this.eventCamera) && graphic.Raycast(vector, this.eventCamera))
				{
					list2.Add(graphic);
				}
			}
			list2.Sort((Graphic g1, Graphic g2) => g2.depth.CompareTo(g1.depth));
			for (int j = 0; j < list2.Count; j++)
			{
				list.Add(list2[j].gameObject);
			}
			list2.Clear();
			return list;
		}

		// Token: 0x06009F79 RID: 40825 RVA: 0x0015B520 File Offset: 0x00159720
		public void Click()
		{
			for (int i = 0; i < this.GetObjectsUnderPointer().Count; i++)
			{
				ExecuteEvents.Execute<IPointerClickHandler>(this.GetObjectsUnderPointer()[i], new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			}
		}

		// Token: 0x06009F7A RID: 40826 RVA: 0x0015B564 File Offset: 0x00159764
		private void ModifyQuad(List<Vector3> verts, int vertexIndex, Vector2 requiredSize)
		{
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < 4; i++)
			{
				list.Add(verts[vertexIndex + i]);
			}
			Vector3 vector = list[2] - list[1];
			Vector3 vector2 = list[1] - list[0];
			int num = Mathf.CeilToInt(vector.magnitude * (1f / Mathf.Max(1f, requiredSize.x)));
			int num2 = Mathf.CeilToInt(vector2.magnitude * (1f / Mathf.Max(1f, requiredSize.y)));
			float num3 = 0f;
			for (int j = 0; j < num2; j++)
			{
				float num4 = ((float)j + 1f) / (float)num2;
				float num5 = 0f;
				for (int k = 0; k < num; k++)
				{
					float num6 = ((float)k + 1f) / (float)num;
					verts.Add(this.TesselateQuad(list, num5, num3));
					verts.Add(this.TesselateQuad(list, num5, num4));
					verts.Add(this.TesselateQuad(list, num6, num4));
					verts.Add(this.TesselateQuad(list, num6, num3));
					num5 = num6;
				}
				num3 = num4;
			}
		}

		// Token: 0x06009F7B RID: 40827 RVA: 0x0015B6A8 File Offset: 0x001598A8
		private Vector3 TesselateQuad(List<Vector3> quad, float x, float y)
		{
			Vector3 vector = Vector3.zero;
			List<float> list = new List<float>(4)
			{
				(1f - x) * (1f - y),
				(1f - x) * y,
				x * y,
				x * (1f - y)
			};
			for (int i = 0; i < 4; i++)
			{
				vector += quad[i] * list[i];
			}
			return vector;
		}

		// Token: 0x04006718 RID: 26392
		[SerializeField]
		private bool showDebug;

		// Token: 0x04006719 RID: 26393
		private GameObject colliderContainer;

		// Token: 0x0400671A RID: 26394
		private Vector3 cyllinderMidPoint;

		// Token: 0x0400671B RID: 26395
		private bool gazeClickExecuted;

		// Token: 0x0400671C RID: 26396
		private Vector2 lastCanvasPos = Vector2.zero;

		// Token: 0x0400671D RID: 26397
		private Canvas myCanvas;

		// Token: 0x0400671E RID: 26398
		private CurvedUISettings mySettings;

		// Token: 0x0400671F RID: 26399
		private float objectsUnderGazeLastChangeTime;

		// Token: 0x04006720 RID: 26400
		private List<GameObject> objectsUnderPointer = new List<GameObject>();

		// Token: 0x04006721 RID: 26401
		private readonly bool overrideEventData = true;

		// Token: 0x04006722 RID: 26402
		private bool pointingAtCanvas;

		// Token: 0x04006723 RID: 26403
		private readonly List<GameObject> selectablesUnderGaze = new List<GameObject>();

		// Token: 0x04006724 RID: 26404
		private readonly List<GameObject> selectablesUnderGazeLastFrame = new List<GameObject>();
	}
}
