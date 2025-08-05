using System;
using System.Runtime.CompilerServices;
using log4net.Util.TypeConverters;

namespace log4net.Layout
{
	// Token: 0x02002A1E RID: 10782
	public class RawLayoutConverter : IConvertFrom
	{
		// Token: 0x06009319 RID: 37657 RVA: 0x00057EC9 File Offset: 0x000560C9
		[NullableContext(1)]
		public bool CanConvertFrom(Type sourceType)
		{
			return typeof(ILayout).IsAssignableFrom(sourceType);
		}

		// Token: 0x0600931A RID: 37658 RVA: 0x001413A8 File Offset: 0x0013F5A8
		[NullableContext(1)]
		public object ConvertFrom(object source)
		{
			ILayout layout = source as ILayout;
			if (layout != null)
			{
				return new Layout2RawLayoutAdapter(layout);
			}
			throw ConversionNotSupportedException.Create(typeof(IRawLayout), source);
		}
	}
}
