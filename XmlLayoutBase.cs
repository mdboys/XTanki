using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout
{
	// Token: 0x02002A24 RID: 10788
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class XmlLayoutBase : LayoutSkeleton
	{
		// Token: 0x06009331 RID: 37681 RVA: 0x00057F82 File Offset: 0x00056182
		protected XmlLayoutBase()
			: this(false)
		{
			this.IgnoresException = false;
		}

		// Token: 0x06009332 RID: 37682 RVA: 0x00057F92 File Offset: 0x00056192
		protected XmlLayoutBase(bool locationInfo)
		{
			this.IgnoresException = false;
			this.LocationInfo = locationInfo;
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06009333 RID: 37683 RVA: 0x00057FB3 File Offset: 0x000561B3
		// (set) Token: 0x06009334 RID: 37684 RVA: 0x00057FBB File Offset: 0x000561BB
		public bool LocationInfo { get; set; }

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06009335 RID: 37685 RVA: 0x00057FC4 File Offset: 0x000561C4
		// (set) Token: 0x06009336 RID: 37686 RVA: 0x00057FCC File Offset: 0x000561CC
		public string InvalidCharReplacement { get; set; } = "?";

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06009337 RID: 37687 RVA: 0x00057FD5 File Offset: 0x000561D5
		public override string ContentType
		{
			get
			{
				return "text/xml";
			}
		}

		// Token: 0x06009338 RID: 37688 RVA: 0x0000568E File Offset: 0x0000388E
		public override void ActivateOptions()
		{
		}

		// Token: 0x06009339 RID: 37689 RVA: 0x00141854 File Offset: 0x0013FA54
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			XmlTextWriter xmlTextWriter = new XmlTextWriter(new ProtectCloseTextWriter(writer));
			xmlTextWriter.Formatting = Formatting.None;
			xmlTextWriter.Namespaces = false;
			this.FormatXml(xmlTextWriter, loggingEvent);
			xmlTextWriter.WriteWhitespace(SystemInfo.NewLine);
			xmlTextWriter.Close();
		}

		// Token: 0x0600933A RID: 37690
		protected abstract void FormatXml(XmlWriter writer, LoggingEvent loggingEvent);
	}
}
