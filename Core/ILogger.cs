using System;
using System.Runtime.CompilerServices;
using log4net.Repository;

namespace log4net.Core
{
	// Token: 0x02002A53 RID: 10835
	[NullableContext(1)]
	public interface ILogger
	{
		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x060093D1 RID: 37841
		string Name { get; }

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x060093D2 RID: 37842
		ILoggerRepository Repository { get; }

		// Token: 0x060093D3 RID: 37843
		void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception);

		// Token: 0x060093D4 RID: 37844
		void Log(LoggingEvent logEvent);

		// Token: 0x060093D5 RID: 37845
		bool IsEnabledFor(Level level);
	}
}
