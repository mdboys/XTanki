using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace log4net.Util
{
	// Token: 0x020029D5 RID: 10709
	public class ReusableStringWriter : StringWriter
	{
		// Token: 0x0600912A RID: 37162 RVA: 0x00056E3C File Offset: 0x0005503C
		[NullableContext(1)]
		public ReusableStringWriter(IFormatProvider formatProvider)
			: base(formatProvider)
		{
		}

		// Token: 0x0600912B RID: 37163 RVA: 0x0000568E File Offset: 0x0000388E
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x0600912C RID: 37164 RVA: 0x0013CD28 File Offset: 0x0013AF28
		public void Reset(int maxCapacity, int defaultSize)
		{
			StringBuilder stringBuilder = this.GetStringBuilder();
			stringBuilder.Length = 0;
			if (stringBuilder.Capacity > maxCapacity)
			{
				stringBuilder.Capacity = defaultSize;
			}
		}
	}
}
