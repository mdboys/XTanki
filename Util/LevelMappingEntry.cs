using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029C1 RID: 10689
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LevelMappingEntry : IOptionHandler
	{
		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06009066 RID: 36966 RVA: 0x00056600 File Offset: 0x00054800
		// (set) Token: 0x06009067 RID: 36967 RVA: 0x00056608 File Offset: 0x00054808
		public Level Level { get; set; }

		// Token: 0x06009068 RID: 36968 RVA: 0x0000568E File Offset: 0x0000388E
		public virtual void ActivateOptions()
		{
		}
	}
}
