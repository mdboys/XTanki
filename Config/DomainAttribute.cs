using System;
using System.Runtime.CompilerServices;

namespace log4net.Config
{
	// Token: 0x02002A75 RID: 10869
	[AttributeUsage(AttributeTargets.Assembly)]
	[Obsolete("Use RepositoryAttribute instead of DomainAttribute")]
	[Serializable]
	public sealed class DomainAttribute : RepositoryAttribute
	{
		// Token: 0x06009504 RID: 38148 RVA: 0x0005935C File Offset: 0x0005755C
		public DomainAttribute()
		{
		}

		// Token: 0x06009505 RID: 38149 RVA: 0x00059364 File Offset: 0x00057564
		[NullableContext(1)]
		public DomainAttribute(string name)
			: base(name)
		{
		}
	}
}
