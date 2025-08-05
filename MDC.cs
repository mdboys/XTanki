using System;
using System.Runtime.CompilerServices;

namespace log4net
{
	// Token: 0x020029B3 RID: 10675
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MDC
	{
		// Token: 0x06009000 RID: 36864 RVA: 0x00005698 File Offset: 0x00003898
		private MDC()
		{
		}

		// Token: 0x06009001 RID: 36865 RVA: 0x000562BA File Offset: 0x000544BA
		public static string Get(string key)
		{
			object obj = ThreadContext.Properties[key];
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x06009002 RID: 36866 RVA: 0x000562D2 File Offset: 0x000544D2
		public static void Set(string key, string value)
		{
			ThreadContext.Properties[key] = value;
		}

		// Token: 0x06009003 RID: 36867 RVA: 0x000562E0 File Offset: 0x000544E0
		public static void Remove(string key)
		{
			ThreadContext.Properties.Remove(key);
		}

		// Token: 0x06009004 RID: 36868 RVA: 0x000562ED File Offset: 0x000544ED
		public static void Clear()
		{
			ThreadContext.Properties.Clear();
		}
	}
}
