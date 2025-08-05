using System;
using System.Runtime.CompilerServices;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x02002A02 RID: 10754
	[NullableContext(1)]
	public interface ILoggerFactory
	{
		// Token: 0x0600924C RID: 37452
		Logger CreateLogger(ILoggerRepository repository, string name);
	}
}
