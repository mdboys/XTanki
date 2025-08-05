using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020029FE RID: 10750
	internal class DefaultLoggerFactory : ILoggerFactory
	{
		// Token: 0x0600921D RID: 37405 RVA: 0x00057552 File Offset: 0x00055752
		[NullableContext(1)]
		public Logger CreateLogger(ILoggerRepository repository, string name)
		{
			if (name == null)
			{
				return new RootLogger(repository.LevelMap.LookupWithDefault(Level.Debug));
			}
			return new DefaultLoggerFactory.LoggerImpl(name);
		}

		// Token: 0x020029FF RID: 10751
		internal sealed class LoggerImpl : Logger
		{
			// Token: 0x0600921F RID: 37407 RVA: 0x00057573 File Offset: 0x00055773
			[NullableContext(1)]
			internal LoggerImpl(string name)
				: base(name)
			{
			}
		}
	}
}
