using System;
using System.Runtime.CompilerServices;
using Platform.Library.ClientUnityIntegration.API;
using Tanks.Lobby.ClientGarage.API;
using Tanks.Lobby.ClientGarage.Impl;

// Token: 0x02000087 RID: 135
public class TankPartItemPropertiesButtonComponent : BehaviourComponent
{
	// Token: 0x040001F0 RID: 496
	public TankPartModuleType tankPartModuleType;

	// Token: 0x040001F1 RID: 497
	[Nullable(1)]
	public TankPartItemPropertiesUIComponent itemPropertiesUiComponent;
}
