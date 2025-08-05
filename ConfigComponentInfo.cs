using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200286E RID: 10350
	[NullableContext(1)]
	[Nullable(0)]
	public class ConfigComponentInfo : ComponentInfo
	{
		// Token: 0x06008A4D RID: 35405 RVA: 0x00052D07 File Offset: 0x00050F07
		public ConfigComponentInfo(string keyName, bool configOptional)
		{
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06008A4E RID: 35406 RVA: 0x00052D1D File Offset: 0x00050F1D
		public string KeyName { get; } = keyName;

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06008A4F RID: 35407 RVA: 0x00052D25 File Offset: 0x00050F25
		public bool ConfigOptional { get; } = configOptional;
	}
}
