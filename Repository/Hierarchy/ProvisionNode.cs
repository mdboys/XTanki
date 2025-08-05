using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x02002A07 RID: 10759
	internal sealed class ProvisionNode : ArrayList
	{
		// Token: 0x06009271 RID: 37489 RVA: 0x000578AE File Offset: 0x00055AAE
		[NullableContext(1)]
		internal ProvisionNode(Logger log)
		{
			this.Add(log);
		}
	}
}
