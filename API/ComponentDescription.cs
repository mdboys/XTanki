using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200294D RID: 10573
	[NullableContext(1)]
	public interface ComponentDescription
	{
		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06008E0E RID: 36366
		Type ComponentType { get; }

		// Token: 0x06008E0F RID: 36367
		T GetInfo<[Nullable(0)] T>() where T : ComponentInfo;

		// Token: 0x06008E10 RID: 36368
		bool IsInfoPresent(Type type);
	}
}
