using System;
using System.Runtime.CompilerServices;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E8 RID: 10728
	internal class TypeConverter : IConvertFrom
	{
		// Token: 0x0600919F RID: 37279 RVA: 0x000571DB File Offset: 0x000553DB
		[NullableContext(1)]
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x060091A0 RID: 37280 RVA: 0x0013DAEC File Offset: 0x0013BCEC
		[NullableContext(1)]
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return SystemInfo.GetTypeFromString(text, true, true);
			}
			throw ConversionNotSupportedException.Create(typeof(Type), source);
		}
	}
}
