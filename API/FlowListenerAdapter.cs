using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200296B RID: 10603
	public class FlowListenerAdapter : FlowListener
	{
		// Token: 0x06008E97 RID: 36503 RVA: 0x0000568E File Offset: 0x0000388E
		public void OnFlowFinish()
		{
		}

		// Token: 0x06008E98 RID: 36504 RVA: 0x0000568E File Offset: 0x0000388E
		public void OnFlowClean()
		{
		}

		// Token: 0x04005FDE RID: 24542
		[Nullable(1)]
		public static readonly FlowListenerAdapter Stub = new FlowListenerAdapter();
	}
}
