using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Tanks.Lobby.ClientGarage.API;
using UnityEngine;

// Token: 0x0200002E RID: 46
[NullableContext(1)]
[Nullable(0)]
public class GameModesStepValidator : MonoBehaviour, ITutorialShowStepValidator
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060000BD RID: 189 RVA: 0x00005B6B File Offset: 0x00003D6B
	// (set) Token: 0x060000BE RID: 190 RVA: 0x00005B72 File Offset: 0x00003D72
	[Inject]
	public static EngineServiceInternal EngineService { get; set; }

	// Token: 0x060000BF RID: 191 RVA: 0x00005B7A File Offset: 0x00003D7A
	public bool ShowAllowed(long stepId)
	{
		return true;
	}
}
