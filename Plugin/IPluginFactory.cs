using System;
using System.Runtime.CompilerServices;

namespace log4net.Plugin
{
	// Token: 0x02002A0C RID: 10764
	[NullableContext(1)]
	public interface IPluginFactory
	{
		// Token: 0x0600928C RID: 37516
		IPlugin CreatePlugin();
	}
}
