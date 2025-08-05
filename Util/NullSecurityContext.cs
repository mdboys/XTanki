using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029C8 RID: 10696
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NullSecurityContext : SecurityContext
	{
		// Token: 0x0600909F RID: 37023 RVA: 0x000568CB File Offset: 0x00054ACB
		private NullSecurityContext()
		{
		}

		// Token: 0x060090A0 RID: 37024 RVA: 0x000564E2 File Offset: 0x000546E2
		public override IDisposable Impersonate(object state)
		{
			return null;
		}

		// Token: 0x040060F4 RID: 24820
		public static readonly NullSecurityContext Instance = new NullSecurityContext();
	}
}
