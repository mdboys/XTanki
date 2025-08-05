using System;
using System.Runtime.CompilerServices;

namespace log4net.Config
{
	// Token: 0x02002A71 RID: 10865
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Obsolete("Use AliasRepositoryAttribute instead of AliasDomainAttribute")]
	[Serializable]
	public sealed class AliasDomainAttribute : AliasRepositoryAttribute
	{
		// Token: 0x060094F4 RID: 38132 RVA: 0x000592DF File Offset: 0x000574DF
		[NullableContext(1)]
		public AliasDomainAttribute(string name)
			: base(name)
		{
		}
	}
}
