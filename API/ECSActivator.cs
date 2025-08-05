using System;
using Platform.Kernel.OSGi.ClientCore.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002958 RID: 10584
	public interface ECSActivator : Platform.Kernel.OSGi.ClientCore.API.Activator
	{
		// Token: 0x06008E21 RID: 36385
		void RegisterSystemsAndTemplates();
	}
}
