using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200295B RID: 10587
	[NullableContext(1)]
	public interface EngineHandlerRegistrationListener
	{
		// Token: 0x06008E38 RID: 36408
		void OnHandlerAdded(Handler handler);
	}
}
