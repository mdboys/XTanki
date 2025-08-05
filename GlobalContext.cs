using System;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net
{
	// Token: 0x020029B0 RID: 10672
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GlobalContext
	{
		// Token: 0x06008FAE RID: 36782 RVA: 0x00056126 File Offset: 0x00054326
		static GlobalContext()
		{
			GlobalContextProperties globalContextProperties = new GlobalContextProperties();
			globalContextProperties["log4net:HostName"] = SystemInfo.HostName;
			GlobalContext.Properties = globalContextProperties;
		}

		// Token: 0x06008FAF RID: 36783 RVA: 0x00005698 File Offset: 0x00003898
		private GlobalContext()
		{
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06008FB0 RID: 36784 RVA: 0x00056142 File Offset: 0x00054342
		public static GlobalContextProperties Properties { get; }
	}
}
