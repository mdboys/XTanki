using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x02002A1B RID: 10779
	[NullableContext(1)]
	[Nullable(0)]
	public class Layout2RawLayoutAdapter : IRawLayout
	{
		// Token: 0x06009302 RID: 37634 RVA: 0x00057E19 File Offset: 0x00056019
		public Layout2RawLayoutAdapter(ILayout layout)
		{
			this.m_layout = layout;
		}

		// Token: 0x06009303 RID: 37635 RVA: 0x00140D54 File Offset: 0x0013EF54
		public virtual object Format(LoggingEvent loggingEvent)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.m_layout.Format(stringWriter, loggingEvent);
			return stringWriter.ToString();
		}

		// Token: 0x04006198 RID: 24984
		private readonly ILayout m_layout;
	}
}
