using System;
using System.Runtime.CompilerServices;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029DF RID: 10719
	internal class BooleanConverter : IConvertFrom
	{
		// Token: 0x0600917B RID: 37243 RVA: 0x000571DB File Offset: 0x000553DB
		[NullableContext(1)]
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x0600917C RID: 37244 RVA: 0x0013D6A4 File Offset: 0x0013B8A4
		[NullableContext(1)]
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return bool.Parse(text);
			}
			throw ConversionNotSupportedException.Create(typeof(bool), source);
		}
	}
}
