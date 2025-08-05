using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x02002A1C RID: 10780
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LayoutSkeleton : ILayout, IOptionHandler
	{
		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06009304 RID: 37636 RVA: 0x00057E28 File Offset: 0x00056028
		public virtual string ContentType
		{
			get
			{
				return "text/plain";
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06009305 RID: 37637 RVA: 0x00057E2F File Offset: 0x0005602F
		// (set) Token: 0x06009306 RID: 37638 RVA: 0x00057E37 File Offset: 0x00056037
		public virtual string Header { get; set; }

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06009307 RID: 37639 RVA: 0x00057E40 File Offset: 0x00056040
		// (set) Token: 0x06009308 RID: 37640 RVA: 0x00057E48 File Offset: 0x00056048
		public virtual string Footer { get; set; }

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06009309 RID: 37641 RVA: 0x00057E51 File Offset: 0x00056051
		// (set) Token: 0x0600930A RID: 37642 RVA: 0x00057E59 File Offset: 0x00056059
		public virtual bool IgnoresException { get; set; } = true;

		// Token: 0x0600930B RID: 37643
		public abstract void Format(TextWriter writer, LoggingEvent loggingEvent);

		// Token: 0x0600930C RID: 37644
		public abstract void ActivateOptions();

		// Token: 0x0600930D RID: 37645 RVA: 0x00140D80 File Offset: 0x0013EF80
		public string Format(LoggingEvent loggingEvent)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.Format(stringWriter, loggingEvent);
			return stringWriter.ToString();
		}
	}
}
