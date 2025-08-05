using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientUnityIntegration.API;
using Tanks.Lobby.ClientGarage.API;
using Tanks.Lobby.ClientGarage.Impl;

// Token: 0x02000065 RID: 101
[NullableContext(1)]
[Nullable(0)]
public class QuickGameStepValidator : ECSBehaviour, ITutorialShowStepValidator
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060001EC RID: 492 RVA: 0x0000671F File Offset: 0x0000491F
	// (set) Token: 0x060001ED RID: 493 RVA: 0x00006726 File Offset: 0x00004926
	[Inject]
	public new static EngineServiceInternal EngineService { get; set; }

	// Token: 0x060001EE RID: 494 RVA: 0x00064CBC File Offset: 0x00062EBC
	public bool ShowAllowed(long stepId)
	{
		GetEnergyCountTutorialValidationDataEvent getEnergyCountTutorialValidationDataEvent = new GetEnergyCountTutorialValidationDataEvent();
		base.ScheduleEvent(getEnergyCountTutorialValidationDataEvent, QuickGameStepValidator.EngineService.EntityStub);
		return getEnergyCountTutorialValidationDataEvent.Quantums <= 0L;
	}
}
