using System;
using System.Runtime.CompilerServices;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x02002A06 RID: 10758
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class LoggerKey
	{
		// Token: 0x0600926E RID: 37486 RVA: 0x00057886 File Offset: 0x00055A86
		internal LoggerKey(string name)
		{
			this.m_name = string.Intern(name);
			this.m_hashCache = name.GetHashCode();
		}

		// Token: 0x0600926F RID: 37487 RVA: 0x000578A6 File Offset: 0x00055AA6
		public override int GetHashCode()
		{
			return this.m_hashCache;
		}

		// Token: 0x06009270 RID: 37488 RVA: 0x0013EF50 File Offset: 0x0013D150
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			LoggerKey loggerKey = obj as LoggerKey;
			return loggerKey != null && this.m_name == loggerKey.m_name;
		}

		// Token: 0x04006164 RID: 24932
		private readonly int m_hashCache;

		// Token: 0x04006165 RID: 24933
		private readonly string m_name;
	}
}
