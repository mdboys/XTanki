using System;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A68 RID: 10856
	[NullableContext(1)]
	[Nullable(0)]
	public struct LoggingEventData
	{
		// Token: 0x04006240 RID: 25152
		public string LoggerName;

		// Token: 0x04006241 RID: 25153
		public Level Level;

		// Token: 0x04006242 RID: 25154
		public string Message;

		// Token: 0x04006243 RID: 25155
		public string ThreadName;

		// Token: 0x04006244 RID: 25156
		public DateTime TimeStamp;

		// Token: 0x04006245 RID: 25157
		public LocationInfo LocationInfo;

		// Token: 0x04006246 RID: 25158
		public string UserName;

		// Token: 0x04006247 RID: 25159
		public string Identity;

		// Token: 0x04006248 RID: 25160
		public string ExceptionString;

		// Token: 0x04006249 RID: 25161
		public string Domain;

		// Token: 0x0400624A RID: 25162
		public PropertiesDictionary Properties;
	}
}
