using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientProtocol.API;

namespace Platform.Common.ClientECSCommon.API
{
	// Token: 0x02002991 RID: 10641
	[NullableContext(1)]
	[Nullable(0)]
	[Shared]
	[SerialVersionUID(1446024502618L)]
	public class NameComponent : Component
	{
		// Token: 0x06008EF3 RID: 36595 RVA: 0x00005698 File Offset: 0x00003898
		public NameComponent()
		{
		}

		// Token: 0x06008EF4 RID: 36596 RVA: 0x00055A80 File Offset: 0x00053C80
		public NameComponent(string name)
		{
			this.Name = name;
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06008EF5 RID: 36597 RVA: 0x00055A8F File Offset: 0x00053C8F
		// (set) Token: 0x06008EF6 RID: 36598 RVA: 0x00055A97 File Offset: 0x00053C97
		public string Name { get; set; }
	}
}
