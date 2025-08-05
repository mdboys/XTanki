using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Tanks.Lobby.ClientControls.API;
using Tanks.Lobby.ClientGarage.Impl;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200008B RID: 139
[NullableContext(1)]
[Nullable(0)]
public class TutorialIntroDialog : MonoBehaviour
{
	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060002B7 RID: 695 RVA: 0x00006F7C File Offset: 0x0000517C
	// (set) Token: 0x060002B8 RID: 696 RVA: 0x00006F83 File Offset: 0x00005183
	[Inject]
	public static EngineServiceInternal EngineService { get; set; }

	// Token: 0x060002B9 RID: 697 RVA: 0x000681E0 File Offset: 0x000663E0
	public void Show(Entity tutorialStep, bool canSkipTutorial)
	{
		this.tutorialStep = tutorialStep;
		this.canSkipTutorial = canSkipTutorial;
		this.animatedText.ResultText = ((!canSkipTutorial) ? this.introWithoutQuestionText.Value : this.introText.Value);
		this.animatedText.Animate();
		this.showTimer = 0f;
		base.gameObject.SetActive(true);
		this.yesButton.gameObject.SetActive(true);
		this.yesButton.GetComponentInChildren<TextMeshProUGUI>().text = ((!canSkipTutorial) ? "ok" : this.yesText.Value);
		this.noButton.gameObject.SetActive(canSkipTutorial);
		this.sarcasmButton.gameObject.SetActive(true);
		this.startTutorial.gameObject.SetActive(false);
		this.skipTutorial.gameObject.SetActive(false);
		this.yesButton.onClick.RemoveAllListeners();
		this.yesButton.onClick.AddListener(new UnityAction(this.ContinueTutorial));
		this.noButton.onClick.RemoveAllListeners();
		this.noButton.onClick.AddListener(new UnityAction(this.ShowConfirmText));
		this.sarcasmButton.onClick.RemoveAllListeners();
		this.sarcasmButton.onClick.AddListener(new UnityAction(this.ShowSarcasm));
		this.startTutorial.onClick.RemoveAllListeners();
		this.startTutorial.onClick.AddListener(new UnityAction(this.ContinueTutorial));
		this.skipTutorial.onClick.RemoveAllListeners();
		this.skipTutorial.onClick.AddListener(new UnityAction(this.DisableTutorials));
		this.yesButton.interactable = true;
		this.noButton.interactable = true;
		this.sarcasmButton.interactable = true;
		this.sarcasmButton.interactable = true;
		this.startTutorial.interactable = true;
		this.skipTutorial.interactable = true;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x000683E4 File Offset: 0x000665E4
	private void ShowConfirmText()
	{
		if (this.canSkipTutorial)
		{
			this.yesButton.gameObject.SetActive(false);
			this.noButton.gameObject.SetActive(false);
			this.sarcasmButton.gameObject.SetActive(false);
			this.animatedText.ResultText = this.confirmText.Value + "\n\n<color=#A0A0A0>" + this.tipText.Value;
			this.animatedText.Animate();
			this.startTutorial.gameObject.SetActive(true);
			this.skipTutorial.gameObject.SetActive(true);
		}
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00006F8B File Offset: 0x0000518B
	private void ShowSarcasm()
	{
		this.sarcasmButton.gameObject.SetActive(false);
		this.animatedText.ResultText = this.sarcasmText.Value;
		this.animatedText.Animate();
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00006FBF File Offset: 0x000051BF
	private void DisableTutorials()
	{
		if (this.tutorialStep != null)
		{
			TutorialIntroDialog.EngineService.Engine.ScheduleEvent<SkipAllTutorialsEvent>(this.tutorialStep);
			this.tutorialStep = null;
			TutorialCanvas.Instance.Hide();
		}
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00006FEF File Offset: 0x000051EF
	private void ContinueTutorial()
	{
		TutorialCanvas.Instance.Hide();
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00068488 File Offset: 0x00066688
	public void TutorialHidden()
	{
		base.gameObject.SetActive(false);
		Entity entity = this.tutorialStep;
		this.tutorialStep = null;
		if (entity != null)
		{
			TutorialIntroDialog.EngineService.Engine.ScheduleEvent<TutorialStepCompleteEvent>(entity);
		}
	}

	// Token: 0x04000210 RID: 528
	[SerializeField]
	protected AnimatedText animatedText;

	// Token: 0x04000211 RID: 529
	[SerializeField]
	private Button yesButton;

	// Token: 0x04000212 RID: 530
	[SerializeField]
	private Button noButton;

	// Token: 0x04000213 RID: 531
	[SerializeField]
	private Button sarcasmButton;

	// Token: 0x04000214 RID: 532
	[SerializeField]
	private Button startTutorial;

	// Token: 0x04000215 RID: 533
	[SerializeField]
	private Button skipTutorial;

	// Token: 0x04000216 RID: 534
	[SerializeField]
	private LocalizedField yesText;

	// Token: 0x04000217 RID: 535
	[SerializeField]
	private LocalizedField introText;

	// Token: 0x04000218 RID: 536
	[SerializeField]
	private LocalizedField introWithoutQuestionText;

	// Token: 0x04000219 RID: 537
	[SerializeField]
	private LocalizedField confirmText;

	// Token: 0x0400021A RID: 538
	[SerializeField]
	private LocalizedField tipText;

	// Token: 0x0400021B RID: 539
	[SerializeField]
	private LocalizedField sarcasmText;

	// Token: 0x0400021C RID: 540
	private bool canSkipTutorial;

	// Token: 0x0400021D RID: 541
	private float showTimer;

	// Token: 0x0400021E RID: 542
	private Entity tutorialStep;
}
