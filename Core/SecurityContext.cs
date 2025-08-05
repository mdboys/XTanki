using System;
using System.Runtime.CompilerServices;

namespace log4net.Core
{
	// Token: 0x02002A6B RID: 10859
	public abstract class SecurityContext
	{
		// Token: 0x060094D7 RID: 38103
		[NullableContext(1)]
		public abstract IDisposable Impersonate(object state);
	}
}
