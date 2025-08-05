using System;
using System.Globalization;
using log4net.Core;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029F0 RID: 10736
	internal sealed class NewLinePatternConverter : LiteralPatternConverter, IOptionHandler
	{
		// Token: 0x060091BA RID: 37306 RVA: 0x0013DD98 File Offset: 0x0013BF98
		public void ActivateOptions()
		{
			if (string.Compare(this.Option, "DOS", true, CultureInfo.InvariantCulture) == 0)
			{
				this.Option = "\r\n";
				return;
			}
			if (string.Compare(this.Option, "UNIX", true, CultureInfo.InvariantCulture) == 0)
			{
				this.Option = "\n";
				return;
			}
			this.Option = SystemInfo.NewLine;
		}
	}
}
