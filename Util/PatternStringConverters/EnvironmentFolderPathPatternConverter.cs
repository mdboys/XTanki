using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029EC RID: 10732
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class EnvironmentFolderPathPatternConverter : PatternConverter
	{
		// Token: 0x060091AD RID: 37293 RVA: 0x0013DC24 File Offset: 0x0013BE24
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				string option = this.Option;
				if (option != null && option.Length > 0)
				{
					string folderPath = Environment.GetFolderPath((Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), this.Option, true));
					if (folderPath != null && folderPath.Length > 0)
					{
						writer.Write(folderPath);
					}
				}
			}
			catch (SecurityException ex)
			{
				LogLog.Debug(EnvironmentFolderPathPatternConverter.declaringType, "Security exception while trying to expand environment variables. Error Ignored. No Expansion.", ex);
			}
			catch (Exception ex2)
			{
				LogLog.Error(EnvironmentFolderPathPatternConverter.declaringType, "Error occurred while converting environment variable.", ex2);
			}
		}

		// Token: 0x0400613C RID: 24892
		private static readonly Type declaringType = typeof(EnvironmentFolderPathPatternConverter);
	}
}
