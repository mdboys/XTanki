using System;
using System.Runtime.CompilerServices;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E7 RID: 10727
	[NullableContext(1)]
	[Nullable(0)]
	internal class PatternStringConverter : IConvertTo, IConvertFrom
	{
		// Token: 0x0600919A RID: 37274 RVA: 0x000571DB File Offset: 0x000553DB
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x0600919B RID: 37275 RVA: 0x0013DA8C File Offset: 0x0013BC8C
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null)
			{
				return new PatternString(text);
			}
			throw ConversionNotSupportedException.Create(typeof(PatternString), source);
		}

		// Token: 0x0600919C RID: 37276 RVA: 0x0005726F File Offset: 0x0005546F
		public bool CanConvertTo(Type targetType)
		{
			return typeof(string).IsAssignableFrom(targetType);
		}

		// Token: 0x0600919D RID: 37277 RVA: 0x0013DABC File Offset: 0x0013BCBC
		public object ConvertTo(object source, Type targetType)
		{
			PatternString patternString = source as PatternString;
			if (patternString != null && this.CanConvertTo(targetType))
			{
				return patternString.Format();
			}
			throw ConversionNotSupportedException.Create(targetType, source);
		}
	}
}
