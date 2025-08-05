using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A31 RID: 10801
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class NamedPatternConverter : PatternLayoutConverter, IOptionHandler
	{
		// Token: 0x06009359 RID: 37721 RVA: 0x00141DE8 File Offset: 0x0013FFE8
		public void ActivateOptions()
		{
			this.m_precision = 0;
			if (this.Option == null)
			{
				return;
			}
			string text = this.Option.Trim();
			if (text.Length <= 0)
			{
				return;
			}
			int num;
			if (!SystemInfo.TryParse(text, out num))
			{
				LogLog.Error(NamedPatternConverter.declaringType, "NamedPatternConverter: Precision option \"" + text + "\" not a decimal integer.");
				return;
			}
			if (num <= 0)
			{
				LogLog.Error(NamedPatternConverter.declaringType, "NamedPatternConverter: Precision option (" + text + ") isn't a positive integer.");
				return;
			}
			this.m_precision = num;
		}

		// Token: 0x0600935A RID: 37722
		protected abstract string GetFullyQualifiedName(LoggingEvent loggingEvent);

		// Token: 0x0600935B RID: 37723 RVA: 0x00141E68 File Offset: 0x00140068
		protected sealed override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			string text = this.GetFullyQualifiedName(loggingEvent);
			if (this.m_precision <= 0 || text == null || text.Length < 2)
			{
				writer.Write(text);
				return;
			}
			int num = text.Length;
			string text2 = string.Empty;
			if (text.EndsWith("."))
			{
				text2 = ".";
				text = text.Substring(0, num - 1);
				num--;
			}
			int num2 = text.LastIndexOf(".");
			int num3 = 1;
			while (num2 > 0 && num3 < this.m_precision)
			{
				num2 = text.LastIndexOf('.', num2 - 1);
				num3++;
			}
			if (num2 == -1)
			{
				writer.Write(text + text2);
				return;
			}
			writer.Write(text.Substring(num2 + 1, num - num2 - 1) + text2);
		}

		// Token: 0x040061C6 RID: 25030
		private const string DOT = ".";

		// Token: 0x040061C7 RID: 25031
		private static readonly Type declaringType = typeof(NamedPatternConverter);

		// Token: 0x040061C8 RID: 25032
		private int m_precision;
	}
}
