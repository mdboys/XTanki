using System;
using System.Runtime.CompilerServices;
using Tanks.Lobby.ClientControls.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008A RID: 138
[NullableContext(1)]
[Nullable(0)]
public class TimerWithAction : MonoBehaviour
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060002AA RID: 682 RVA: 0x00006F0B File Offset: 0x0000510B
	// (set) Token: 0x060002AB RID: 683 RVA: 0x00006F13 File Offset: 0x00005113
	public float StartTime
	{
		get
		{
			return this._startTime;
		}
		set
		{
			this._startTime = value;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060002AC RID: 684 RVA: 0x00006F1C File Offset: 0x0000511C
	// (set) Token: 0x060002AD RID: 685 RVA: 0x00006F24 File Offset: 0x00005124
	public Button.ButtonClickedEvent OnTimeEndAction
	{
		get
		{
			return this._onTimeEndAction;
		}
		set
		{
			this._onTimeEndAction = value;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060002AE RID: 686 RVA: 0x00006F2D File Offset: 0x0000512D
	// (set) Token: 0x060002AF RID: 687 RVA: 0x00006F35 File Offset: 0x00005135
	public LocalizedField ActionDescription
	{
		get
		{
			return this._actionDescription;
		}
		set
		{
			this._actionDescription = value;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060002B0 RID: 688 RVA: 0x00006F3E File Offset: 0x0000513E
	// (set) Token: 0x060002B1 RID: 689 RVA: 0x00006F46 File Offset: 0x00005146
	public TextMeshProUGUI DescriptionText
	{
		get
		{
			return this._descriptionText;
		}
		set
		{
			this._descriptionText = value;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060002B2 RID: 690 RVA: 0x00006F4F File Offset: 0x0000514F
	// (set) Token: 0x060002B3 RID: 691 RVA: 0x00006F57 File Offset: 0x00005157
	public float CurrentTime { get; set; }

	// Token: 0x060002B4 RID: 692 RVA: 0x00068148 File Offset: 0x00066348
	private void Update()
	{
		if (this.CurrentTime > 0f)
		{
			if (this.DescriptionText != null && !string.IsNullOrEmpty(this.ActionDescription.Value))
			{
				this.DescriptionText.text = string.Format(this.ActionDescription, this.CurrentTime);
			}
			this.CurrentTime -= Time.deltaTime;
			if (this.CurrentTime <= 0f)
			{
				this.CurrentTime = 0f;
				this.OnTimeEndAction.Invoke();
			}
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00006F60 File Offset: 0x00005160
	private void OnEnable()
	{
		if (this.CurrentTime <= this.StartTime)
		{
			this.CurrentTime = this.StartTime;
		}
	}

	// Token: 0x0400020B RID: 523
	[Header("Time To Action")]
	[SerializeField]
	private float _startTime;

	// Token: 0x0400020C RID: 524
	[Header("Action")]
	[SerializeField]
	private Button.ButtonClickedEvent _onTimeEndAction;

	// Token: 0x0400020D RID: 525
	[Header("Description")]
	[SerializeField]
	private LocalizedField _actionDescription;

	// Token: 0x0400020E RID: 526
	[SerializeField]
	private TextMeshProUGUI _descriptionText;
}
