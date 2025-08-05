using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace log4net.Core
{
	// Token: 0x02002A61 RID: 10849
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class LogException : ApplicationException
	{
		// Token: 0x06009452 RID: 37970 RVA: 0x000571EA File Offset: 0x000553EA
		public LogException()
		{
		}

		// Token: 0x06009453 RID: 37971 RVA: 0x000571F2 File Offset: 0x000553F2
		public LogException(string message)
			: base(message)
		{
		}

		// Token: 0x06009454 RID: 37972 RVA: 0x000571FB File Offset: 0x000553FB
		public LogException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06009455 RID: 37973 RVA: 0x00057205 File Offset: 0x00055405
		protected LogException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
