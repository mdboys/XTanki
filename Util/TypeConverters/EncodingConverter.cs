using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E2 RID: 10722
	internal class EncodingConverter : IConvertFrom
	{
		// Token: 0x0600918C RID: 37260 RVA: 0x000571DB File Offset: 0x000553DB
		[NullableContext(1)]
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x0600918D RID: 37261 RVA: 0x0013D990 File Offset: 0x0013BB90
		[NullableContext(1)]
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return Encoding.GetEncoding(text);
			}
			throw ConversionNotSupportedException.Create(typeof(Encoding), source);
		}
	}
}
