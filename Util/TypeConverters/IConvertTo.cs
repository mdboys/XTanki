using System;
using System.Runtime.CompilerServices;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E4 RID: 10724
	[NullableContext(1)]
	public interface IConvertTo
	{
		// Token: 0x06009191 RID: 37265
		bool CanConvertTo(Type targetType);

		// Token: 0x06009192 RID: 37266
		object ConvertTo(object source, Type targetType);
	}
}
