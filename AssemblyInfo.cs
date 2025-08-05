using System;
using System.Runtime.CompilerServices;

namespace log4net
{
	// Token: 0x020029AF RID: 10671
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AssemblyInfo
	{
		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06008FAB RID: 36779 RVA: 0x0013B10C File Offset: 0x0013930C
		public static string Info
		{
			get
			{
				return string.Format("Apache log4net version {0} compiled for {1}{2} {3}", new object[]
				{
					"1.2.13",
					"Mono",
					string.Empty,
					2.0m
				});
			}
		}

		// Token: 0x040060C3 RID: 24771
		public const string Version = "1.2.13";

		// Token: 0x040060C4 RID: 24772
		public const decimal TargetFrameworkVersion = 2.0m;

		// Token: 0x040060C5 RID: 24773
		public const string TargetFramework = "Mono";

		// Token: 0x040060C6 RID: 24774
		public const bool ClientProfile = false;
	}
}
