using System;
using System.Runtime.CompilerServices;
using log4net.Repository;

namespace log4net.Plugin
{
	// Token: 0x02002A0B RID: 10763
	[NullableContext(1)]
	public interface IPlugin
	{
		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06009289 RID: 37513
		string Name { get; }

		// Token: 0x0600928A RID: 37514
		void Attach(ILoggerRepository repository);

		// Token: 0x0600928B RID: 37515
		void Shutdown();
	}
}
