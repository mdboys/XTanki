using System;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util.TypeConverters;

namespace log4net.Layout
{
	// Token: 0x02002A1A RID: 10778
	[NullableContext(1)]
	[TypeConverter(typeof(RawLayoutConverter))]
	public interface IRawLayout
	{
		// Token: 0x06009301 RID: 37633
		object Format(LoggingEvent loggingEvent);
	}
}
