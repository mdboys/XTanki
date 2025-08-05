using System;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200296A RID: 10602
	public interface FlowListener
	{
		// Token: 0x06008E95 RID: 36501
		void OnFlowFinish();

		// Token: 0x06008E96 RID: 36502
		void OnFlowClean();
	}
}
