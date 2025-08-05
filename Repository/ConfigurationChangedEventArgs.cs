using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace log4net.Repository
{
	// Token: 0x020029F6 RID: 10742
	[NullableContext(1)]
	[Nullable(0)]
	public class ConfigurationChangedEventArgs : EventArgs
	{
		// Token: 0x060091CB RID: 37323 RVA: 0x0005739A File Offset: 0x0005559A
		public ConfigurationChangedEventArgs(ICollection configurationMessages)
		{
			this.ConfigurationMessages = configurationMessages;
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x060091CC RID: 37324 RVA: 0x000573A9 File Offset: 0x000555A9
		public ICollection ConfigurationMessages { get; }
	}
}
