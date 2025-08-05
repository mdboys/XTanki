using System;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Assets.lobby.modules.ClientControls.Scripts.API
{
	// Token: 0x02002CD3 RID: 11475
	public class SliderBarValueChangedEvent : Event
	{
		// Token: 0x06009FF8 RID: 40952 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public SliderBarValueChangedEvent()
		{
		}

		// Token: 0x06009FF9 RID: 40953 RVA: 0x0005CDF0 File Offset: 0x0005AFF0
		public SliderBarValueChangedEvent(float value)
		{
			this.Value = value;
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06009FFA RID: 40954 RVA: 0x0005CDFF File Offset: 0x0005AFFF
		// (set) Token: 0x06009FFB RID: 40955 RVA: 0x0005CE07 File Offset: 0x0005B007
		public float Value { get; set; }
	}
}
