using System;
using System.Runtime.CompilerServices;
using log4net.Appender;

namespace log4net.Core
{
	// Token: 0x02002A50 RID: 10832
	[NullableContext(1)]
	public interface IAppenderAttachable
	{
		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x060093C7 RID: 37831
		AppenderCollection Appenders { get; }

		// Token: 0x060093C8 RID: 37832
		void AddAppender(IAppender appender);

		// Token: 0x060093C9 RID: 37833
		IAppender GetAppender(string name);

		// Token: 0x060093CA RID: 37834
		void RemoveAllAppenders();

		// Token: 0x060093CB RID: 37835
		IAppender RemoveAppender(IAppender appender);

		// Token: 0x060093CC RID: 37836
		IAppender RemoveAppender(string name);
	}
}
