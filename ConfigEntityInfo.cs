using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002870 RID: 10352
	[NullableContext(1)]
	[Nullable(0)]
	public class ConfigEntityInfo
	{
		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06008A54 RID: 35412 RVA: 0x00052D51 File Offset: 0x00050F51
		// (set) Token: 0x06008A55 RID: 35413 RVA: 0x00052D59 File Offset: 0x00050F59
		public string Path { get; set; }

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06008A56 RID: 35414 RVA: 0x00052D62 File Offset: 0x00050F62
		// (set) Token: 0x06008A57 RID: 35415 RVA: 0x00052D6A File Offset: 0x00050F6A
		public long TemplateId { get; set; }
	}
}
