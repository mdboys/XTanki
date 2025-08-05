using System;
using System.Runtime.CompilerServices;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E3 RID: 10723
	[NullableContext(1)]
	public interface IConvertFrom
	{
		// Token: 0x0600918F RID: 37263
		bool CanConvertFrom(Type sourceType);

		// Token: 0x06009190 RID: 37264
		object ConvertFrom(object source);
	}
}
