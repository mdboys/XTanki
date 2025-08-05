using System;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A6C RID: 10860
	[NullableContext(1)]
	[Nullable(0)]
	public class SecurityContextProvider
	{
		// Token: 0x060094D9 RID: 38105 RVA: 0x00005698 File Offset: 0x00003898
		protected SecurityContextProvider()
		{
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x060094DA RID: 38106 RVA: 0x00059208 File Offset: 0x00057408
		// (set) Token: 0x060094DB RID: 38107 RVA: 0x0005920F File Offset: 0x0005740F
		public static SecurityContextProvider DefaultProvider { get; set; } = new SecurityContextProvider();

		// Token: 0x060094DC RID: 38108 RVA: 0x00059217 File Offset: 0x00057417
		public virtual SecurityContext CreateSecurityContext(object consumer)
		{
			return NullSecurityContext.Instance;
		}
	}
}
