using System;
using System.Runtime.CompilerServices;

namespace log4net.Core
{
	// Token: 0x02002A51 RID: 10833
	[NullableContext(1)]
	public interface IErrorHandler
	{
		// Token: 0x060093CD RID: 37837
		void Error(string message, Exception e, ErrorCode errorCode);

		// Token: 0x060093CE RID: 37838
		void Error(string message, Exception e);

		// Token: 0x060093CF RID: 37839
		void Error(string message);
	}
}
