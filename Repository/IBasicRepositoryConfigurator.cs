using System;
using System.Runtime.CompilerServices;
using log4net.Appender;

namespace log4net.Repository
{
	// Token: 0x020029F7 RID: 10743
	[NullableContext(1)]
	public interface IBasicRepositoryConfigurator
	{
		// Token: 0x060091CD RID: 37325
		void Configure(IAppender appender);

		// Token: 0x060091CE RID: 37326
		void Configure(params IAppender[] appenders);
	}
}
