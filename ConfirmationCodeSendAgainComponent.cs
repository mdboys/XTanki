using System;
using System.Runtime.CompilerServices;
using Tanks.Lobby.ClientControls.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000018 RID: 24
[NullableContext(1)]
[Nullable(0)]
public class ConfirmationCodeSendAgainComponent : MonoBehaviour
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000055 RID: 85 RVA: 0x000057CC File Offset: 0x000039CC
	// (set) Token: 0x06000056 RID: 86 RVA: 0x0005E6A0 File Offset: 0x0005C8A0
	public long Timer
	{
		get
		{
			return this.timer;
		}
		set
		{
			this.timer = value;
			string text = LocalizationUtils.Localize("ec7ff56e-6fba-4947-87d0-a2a753c0974a");
			text = text.Replace("%seconds%", this.timer.ToString());
			if (this.timerLabel.text != text)
			{
				this.timerLabel.text = text;
			}
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0005E6F8 File Offset: 0x0005C8F8
	private void Update()
	{
		long num = (long)new TimeSpan(DateTime.Now.Ticks - this.lastRequestTicks).TotalSeconds;
		long num2 = this.emailSendThresholdInSeconds - num;
		if (num2 > 0L)
		{
			this.Timer = num2;
			return;
		}
		if (this.timerLabel.gameObject.activeSelf)
		{
			this.HideTimer();
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0005E758 File Offset: 0x0005C958
	public void ShowTimer(long emailSendThreshold)
	{
		this.sendAgainButton.interactable = false;
		this.lastRequestTicks = DateTime.Now.Ticks;
		this.emailSendThresholdInSeconds = emailSendThreshold;
		this.buttonLayoutElement.preferredHeight = 80f;
		this.timerLabel.gameObject.SetActive(true);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000057D4 File Offset: 0x000039D4
	public void HideTimer()
	{
		this.sendAgainButton.interactable = true;
		this.buttonLayoutElement.preferredHeight = 50f;
		this.timerLabel.gameObject.SetActive(false);
	}

	// Token: 0x0400002B RID: 43
	public LayoutElement buttonLayoutElement;

	// Token: 0x0400002C RID: 44
	public TextMeshProUGUI timerLabel;

	// Token: 0x0400002D RID: 45
	public Button sendAgainButton;

	// Token: 0x0400002E RID: 46
	private long emailSendThresholdInSeconds = 60L;

	// Token: 0x0400002F RID: 47
	private long lastRequestTicks;

	// Token: 0x04000030 RID: 48
	private long timer;
}
