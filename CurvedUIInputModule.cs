using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200001A RID: 26
[NullableContext(1)]
[Nullable(0)]
[ExecuteInEditMode]
public class CurvedUIInputModule : StandaloneInputModule
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000064 RID: 100 RVA: 0x00005875 File Offset: 0x00003A75
	// (set) Token: 0x06000065 RID: 101 RVA: 0x00005893 File Offset: 0x00003A93
	public static CurvedUIInputModule Instance
	{
		get
		{
			if (CurvedUIInputModule.instance == null)
			{
				CurvedUIInputModule.instance = CurvedUIInputModule.EnableInputModule<CurvedUIInputModule>();
			}
			return CurvedUIInputModule.instance;
		}
		private set
		{
			CurvedUIInputModule.instance = value;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000066 RID: 102 RVA: 0x0000589B File Offset: 0x00003A9B
	// (set) Token: 0x06000067 RID: 103 RVA: 0x000058A7 File Offset: 0x00003AA7
	public static Ray CustomControllerRay
	{
		get
		{
			return CurvedUIInputModule.Instance.customControllerRay;
		}
		set
		{
			CurvedUIInputModule.Instance.customControllerRay = value;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000068 RID: 104 RVA: 0x000058B4 File Offset: 0x00003AB4
	// (set) Token: 0x06000069 RID: 105 RVA: 0x000058C0 File Offset: 0x00003AC0
	[Obsolete("Misspelled. Use CustomControllerButtonDown instead.")]
	public static bool CustromControllerButtonDown
	{
		get
		{
			return CurvedUIInputModule.Instance.pressedDown;
		}
		set
		{
			CurvedUIInputModule.Instance.pressedDown = value;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600006A RID: 106 RVA: 0x000058B4 File Offset: 0x00003AB4
	// (set) Token: 0x0600006B RID: 107 RVA: 0x000058C0 File Offset: 0x00003AC0
	public static bool CustomControllerButtonDown
	{
		get
		{
			return CurvedUIInputModule.Instance.pressedDown;
		}
		set
		{
			CurvedUIInputModule.Instance.pressedDown = value;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600006C RID: 108 RVA: 0x000058CD File Offset: 0x00003ACD
	// (set) Token: 0x0600006D RID: 109 RVA: 0x000058D5 File Offset: 0x00003AD5
	public Vector2 WorldSpaceMouseInCanvasSpace
	{
		get
		{
			return this.worldSpaceMouseInCanvasSpace;
		}
		set
		{
			this.worldSpaceMouseInCanvasSpace = value;
			this.lastWorldSpaceMouseOnCanvas = value;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600006E RID: 110 RVA: 0x000058E5 File Offset: 0x00003AE5
	public Vector2 WorldSpaceMouseInCanvasSpaceDelta
	{
		get
		{
			return this.worldSpaceMouseInCanvasSpace - this.lastWorldSpaceMouseOnCanvas;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x0600006F RID: 111 RVA: 0x000058F8 File Offset: 0x00003AF8
	// (set) Token: 0x06000070 RID: 112 RVA: 0x00005900 File Offset: 0x00003B00
	public float WorldSpaceMouseSensitivity
	{
		get
		{
			return this.worldSpaceMouseSensitivity;
		}
		set
		{
			this.worldSpaceMouseSensitivity = value;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000071 RID: 113 RVA: 0x00005909 File Offset: 0x00003B09
	// (set) Token: 0x06000072 RID: 114 RVA: 0x00005915 File Offset: 0x00003B15
	public static CurvedUIInputModule.CUIControlMethod ControlMethod
	{
		get
		{
			return CurvedUIInputModule.Instance.controlMethod;
		}
		set
		{
			if (CurvedUIInputModule.Instance.controlMethod != value)
			{
				CurvedUIInputModule.Instance.controlMethod = value;
			}
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000073 RID: 115 RVA: 0x0000592F File Offset: 0x00003B2F
	// (set) Token: 0x06000074 RID: 116 RVA: 0x00005937 File Offset: 0x00003B37
	public GameObject CurrentPointedAt { get; private set; }

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000075 RID: 117 RVA: 0x00005940 File Offset: 0x00003B40
	// (set) Token: 0x06000076 RID: 118 RVA: 0x00005948 File Offset: 0x00003B48
	public CurvedUIInputModule.Hand UsedHand
	{
		get
		{
			return this.usedHand;
		}
		set
		{
			this.usedHand = value;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000077 RID: 119 RVA: 0x00005951 File Offset: 0x00003B51
	// (set) Token: 0x06000078 RID: 120 RVA: 0x00005959 File Offset: 0x00003B59
	public bool GazeUseTimedClick
	{
		get
		{
			return this.gazeUseTimedClick;
		}
		set
		{
			this.gazeUseTimedClick = value;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000079 RID: 121 RVA: 0x00005962 File Offset: 0x00003B62
	// (set) Token: 0x0600007A RID: 122 RVA: 0x0000596A File Offset: 0x00003B6A
	public float GazeClickTimer
	{
		get
		{
			return this.gazeClickTimer;
		}
		set
		{
			this.gazeClickTimer = Mathf.Max(value, 0f);
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600007B RID: 123 RVA: 0x0000597D File Offset: 0x00003B7D
	// (set) Token: 0x0600007C RID: 124 RVA: 0x00005985 File Offset: 0x00003B85
	public float GazeClickTimerDelay
	{
		get
		{
			return this.gazeClickTimerDelay;
		}
		set
		{
			this.gazeClickTimerDelay = Mathf.Max(value, 0f);
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x0600007D RID: 125 RVA: 0x00005998 File Offset: 0x00003B98
	public float GazeTimerProgress { get; }

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x0600007E RID: 126 RVA: 0x000059A0 File Offset: 0x00003BA0
	// (set) Token: 0x0600007F RID: 127 RVA: 0x000059A8 File Offset: 0x00003BA8
	public Image GazeTimedClickProgressImage
	{
		get
		{
			return this.gazeTimedClickProgressImage;
		}
		set
		{
			this.gazeTimedClickProgressImage = value;
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000059B1 File Offset: 0x00003BB1
	protected override void Awake()
	{
		if (Application.isPlaying)
		{
			CurvedUIInputModule.Instance = this;
			base.Awake();
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x000059C6 File Offset: 0x00003BC6
	protected override void Start()
	{
		if (Application.isPlaying)
		{
			base.Start();
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0005E7AC File Offset: 0x0005C9AC
	public override void Process()
	{
		switch (this.controlMethod)
		{
		case CurvedUIInputModule.CUIControlMethod.GAZE:
			this.ProcessGaze();
			return;
		case CurvedUIInputModule.CUIControlMethod.WORLD_MOUSE:
			if (Input.touchCount > 0)
			{
				this.worldSpaceMouseOnCanvasDelta = Input.GetTouch(0).deltaPosition * this.worldSpaceMouseSensitivity;
			}
			else
			{
				this.worldSpaceMouseOnCanvasDelta = new Vector2((Input.mousePosition - this.lastMouseOnScreenPos).x, (Input.mousePosition - this.lastMouseOnScreenPos).y) * this.worldSpaceMouseSensitivity;
				this.lastMouseOnScreenPos = Input.mousePosition;
			}
			this.lastWorldSpaceMouseOnCanvas = this.worldSpaceMouseInCanvasSpace;
			this.worldSpaceMouseInCanvasSpace += this.worldSpaceMouseOnCanvasDelta;
			base.Process();
			return;
		case CurvedUIInputModule.CUIControlMethod.CUSTOM_RAY:
			this.ProcessCustomRayController();
			return;
		case CurvedUIInputModule.CUIControlMethod.VIVE:
			this.ProcessViveControllers();
			return;
		case CurvedUIInputModule.CUIControlMethod.OCULUS_TOUCH:
			this.ProcessOculusTouchController();
			return;
		default:
			base.Process();
			return;
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0005E89C File Offset: 0x0005CA9C
	protected virtual void ProcessGaze()
	{
		bool flag = base.SendUpdateEventToSelectedObject();
		if (base.eventSystem.sendNavigationEvents)
		{
			if (!flag)
			{
				flag |= base.SendMoveEventToSelectedObject();
			}
			if (!flag)
			{
				base.SendSubmitEventToSelectedObject();
			}
		}
		base.ProcessMouseEvent();
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0005E8DC File Offset: 0x0005CADC
	protected virtual void ProcessCustomRayController()
	{
		PointerEventData buttonData = this.GetMousePointerEventData(0).GetButtonState(PointerEventData.InputButton.Left).eventData.buttonData;
		base.SendUpdateEventToSelectedObject();
		this.CurrentPointedAt = buttonData.pointerCurrentRaycast.gameObject;
		this.ProcessDownRelease(buttonData, this.pressedDown && !this.pressedLastFrame, !this.pressedDown && this.pressedLastFrame);
		this.ProcessMove(buttonData);
		if (this.pressedDown)
		{
			this.ProcessDrag(buttonData);
			if (!Mathf.Approximately(buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(buttonData.pointerCurrentRaycast.gameObject), buttonData, ExecuteEvents.scrollHandler);
			}
		}
		this.pressedLastFrame = this.pressedDown;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0005E9A4 File Offset: 0x0005CBA4
	protected virtual void ProcessDownRelease(PointerEventData eventData, bool down, bool released)
	{
		GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
		if (down)
		{
			eventData.eligibleForClick = true;
			eventData.delta = Vector2.zero;
			eventData.dragging = false;
			eventData.useDragThreshold = true;
			eventData.pressPosition = eventData.position;
			eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;
			base.DeselectIfSelectionChanged(gameObject, eventData);
			if (eventData.pointerEnter != gameObject)
			{
				base.HandlePointerExitAndEnter(eventData, gameObject);
				eventData.pointerEnter = gameObject;
			}
			GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, eventData, ExecuteEvents.pointerDownHandler);
			if (gameObject2 == null)
			{
				gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			}
			float unscaledTime = Time.unscaledTime;
			if (gameObject2 == eventData.lastPress)
			{
				if (unscaledTime - eventData.clickTime < 0.3f)
				{
					int clickCount = eventData.clickCount;
					eventData.clickCount = clickCount + 1;
				}
				else
				{
					eventData.clickCount = 1;
				}
				eventData.clickTime = unscaledTime;
			}
			else
			{
				eventData.clickCount = 1;
			}
			eventData.pointerPress = gameObject2;
			eventData.rawPointerPress = gameObject;
			eventData.clickTime = unscaledTime;
			eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
			if (eventData.pointerDrag != null)
			{
				ExecuteEvents.Execute<IInitializePotentialDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);
			}
		}
		if (released)
		{
			ExecuteEvents.Execute<IPointerUpHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			if (eventData.pointerPress == eventHandler && eventData.eligibleForClick)
			{
				ExecuteEvents.Execute<IPointerClickHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerClickHandler);
			}
			else if (eventData.pointerDrag != null && eventData.dragging)
			{
				ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, eventData, ExecuteEvents.dropHandler);
			}
			eventData.eligibleForClick = false;
			eventData.pointerPress = null;
			eventData.rawPointerPress = null;
			if (eventData.pointerDrag != null && eventData.dragging)
			{
				ExecuteEvents.Execute<IEndDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
			}
			eventData.dragging = false;
			eventData.pointerDrag = null;
			ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(eventData.pointerEnter, eventData, ExecuteEvents.pointerExitHandler);
			eventData.pointerEnter = null;
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000568E File Offset: 0x0000388E
	protected virtual void ProcessViveControllers()
	{
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000568E File Offset: 0x0000388E
	protected virtual void ProcessOculusTouchController()
	{
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0005EBA4 File Offset: 0x0005CDA4
	private static T EnableInputModule<[Nullable(0)] T>() where T : BaseInputModule
	{
		bool flag = true;
		EventSystem eventSystem = global::UnityEngine.Object.FindObjectOfType<EventSystem>();
		if (eventSystem == null)
		{
			Debug.LogError("CurvedUI: Your EventSystem component is missing from the scene! Unity Canvas will not track interactions without it.");
			return default(T);
		}
		foreach (BaseInputModule baseInputModule in eventSystem.GetComponents<BaseInputModule>())
		{
			if (baseInputModule is T)
			{
				flag = false;
				baseInputModule.enabled = true;
			}
			else if (CurvedUIInputModule.disableOtherInputModulesOnStart)
			{
				baseInputModule.enabled = false;
			}
		}
		if (flag)
		{
			eventSystem.gameObject.AddComponent<T>();
		}
		return eventSystem.GetComponent<T>();
	}

	// Token: 0x04000035 RID: 53
	private static readonly bool disableOtherInputModulesOnStart = true;

	// Token: 0x04000036 RID: 54
	private static CurvedUIInputModule instance;

	// Token: 0x04000037 RID: 55
	[SerializeField]
	private CurvedUIInputModule.CUIControlMethod controlMethod;

	// Token: 0x04000038 RID: 56
	[SerializeField]
	private string submitButtonName = "Fire1";

	// Token: 0x04000039 RID: 57
	[SerializeField]
	private bool gazeUseTimedClick;

	// Token: 0x0400003A RID: 58
	[SerializeField]
	private float gazeClickTimer = 2f;

	// Token: 0x0400003B RID: 59
	[SerializeField]
	private float gazeClickTimerDelay = 1f;

	// Token: 0x0400003C RID: 60
	[SerializeField]
	private Image gazeTimedClickProgressImage;

	// Token: 0x0400003D RID: 61
	[SerializeField]
	private float worldSpaceMouseSensitivity = 1f;

	// Token: 0x0400003E RID: 62
	[SerializeField]
	private CurvedUIInputModule.Hand usedHand = CurvedUIInputModule.Hand.Right;

	// Token: 0x0400003F RID: 63
	private GameObject currentDragging;

	// Token: 0x04000040 RID: 64
	private Ray customControllerRay;

	// Token: 0x04000041 RID: 65
	private float dragThreshold = 10f;

	// Token: 0x04000042 RID: 66
	private Vector3 lastMouseOnScreenPos = Vector2.zero;

	// Token: 0x04000043 RID: 67
	private Vector2 lastWorldSpaceMouseOnCanvas = Vector2.zero;

	// Token: 0x04000044 RID: 68
	private bool pressedDown;

	// Token: 0x04000045 RID: 69
	private bool pressedLastFrame;

	// Token: 0x04000046 RID: 70
	private Vector2 worldSpaceMouseInCanvasSpace = Vector2.zero;

	// Token: 0x04000047 RID: 71
	private Vector2 worldSpaceMouseOnCanvasDelta = Vector2.zero;

	// Token: 0x0200001B RID: 27
	[NullableContext(0)]
	public enum CUIControlMethod
	{
		// Token: 0x0400004B RID: 75
		MOUSE,
		// Token: 0x0400004C RID: 76
		GAZE,
		// Token: 0x0400004D RID: 77
		WORLD_MOUSE,
		// Token: 0x0400004E RID: 78
		CUSTOM_RAY,
		// Token: 0x0400004F RID: 79
		VIVE,
		// Token: 0x04000050 RID: 80
		OCULUS_TOUCH,
		// Token: 0x04000051 RID: 81
		GOOGLEVR = 7
	}

	// Token: 0x0200001C RID: 28
	[NullableContext(0)]
	public enum Hand
	{
		// Token: 0x04000053 RID: 83
		Both,
		// Token: 0x04000054 RID: 84
		Right,
		// Token: 0x04000055 RID: 85
		Left
	}
}
