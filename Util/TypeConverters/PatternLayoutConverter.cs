using System;
using System.Runtime.CompilerServices;
using log4net.Layout;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E6 RID: 10726
	internal class PatternLayoutConverter : IConvertFrom
	{
		// Token: 0x06009197 RID: 37271 RVA: 0x000571DB File Offset: 0x000553DB
		[NullableContext(1)]
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06009198 RID: 37272 RVA: 0x0013DA5C File Offset: 0x0013BC5C
		[NullableContext(1)]
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return new PatternLayout(text);
			}
			throw ConversionNotSupportedException.Create(typeof(PatternLayout), source);
		}
	}
}
