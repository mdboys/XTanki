using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02002A3E RID: 10814
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class FilterSkeleton : IFilter, IOptionHandler
	{
		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x0600937D RID: 37757 RVA: 0x000581DF File Offset: 0x000563DF
		// (set) Token: 0x0600937E RID: 37758 RVA: 0x000581E7 File Offset: 0x000563E7
		public IFilter Next { get; set; }

		// Token: 0x0600937F RID: 37759 RVA: 0x0000568E File Offset: 0x0000388E
		public virtual void ActivateOptions()
		{
		}

		// Token: 0x06009380 RID: 37760
		public abstract FilterDecision Decide(LoggingEvent loggingEvent);
	}
}
