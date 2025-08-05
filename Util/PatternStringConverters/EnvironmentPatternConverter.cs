using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029ED RID: 10733
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class EnvironmentPatternConverter : PatternConverter
	{
		// Token: 0x060091B0 RID: 37296 RVA: 0x0013DCBC File Offset: 0x0013BEBC
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				string option = this.Option;
				if (option != null && option.Length > 0)
				{
					string text = Environment.GetEnvironmentVariable(this.Option);
					if (text == null)
					{
						text = Environment.GetEnvironmentVariable(this.Option, EnvironmentVariableTarget.User);
					}
					if (text == null)
					{
						text = Environment.GetEnvironmentVariable(this.Option, EnvironmentVariableTarget.Machine);
					}
					if (text != null && text.Length > 0)
					{
						writer.Write(text);
					}
				}
			}
			catch (SecurityException ex)
			{
				LogLog.Debug(EnvironmentPatternConverter.declaringType, "Security exception while trying to expand environment variables. Error Ignored. No Expansion.", ex);
			}
			catch (Exception ex2)
			{
				LogLog.Error(EnvironmentPatternConverter.declaringType, "Error occurred while converting environment variable.", ex2);
			}
		}

		// Token: 0x0400613D RID: 24893
		private static readonly Type declaringType = typeof(EnvironmentPatternConverter);
	}
}
