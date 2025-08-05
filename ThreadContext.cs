using System;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net
{
	// Token: 0x020029B5 RID: 10677
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThreadContext
	{
		// Token: 0x0600900E RID: 36878 RVA: 0x00005698 File Offset: 0x00003898
		private ThreadContext()
		{
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x0600900F RID: 36879 RVA: 0x0005637F File Offset: 0x0005457F
		public static ThreadContextProperties Properties { get; } = new ThreadContextProperties();

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06009010 RID: 36880 RVA: 0x00056386 File Offset: 0x00054586
		public static ThreadContextStacks Stacks { get; } = new ThreadContextStacks(ThreadContext.Properties);
	}
}
