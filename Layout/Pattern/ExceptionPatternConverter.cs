using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A28 RID: 10792
	internal sealed class ExceptionPatternConverter : PatternLayoutConverter
	{
		// Token: 0x06009347 RID: 37703 RVA: 0x00058040 File Offset: 0x00056240
		public ExceptionPatternConverter()
		{
			this.IgnoresException = false;
		}

		// Token: 0x06009348 RID: 37704 RVA: 0x00141CD4 File Offset: 0x0013FED4
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent.ExceptionObject != null)
			{
				string text = this.Option;
				if (text != null && text.Length > 0)
				{
					text = this.Option.ToLower();
					if (text == "message")
					{
						PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.Message);
						return;
					}
					if (text == "source")
					{
						PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.Source);
						return;
					}
					if (text == "stacktrace")
					{
						PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.StackTrace);
						return;
					}
					if (text == "targetsite")
					{
						PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.TargetSite);
						return;
					}
					if (!(text == "helplink"))
					{
						return;
					}
					PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.HelpLink);
					return;
				}
			}
			string exceptionString = loggingEvent.GetExceptionString();
			if (exceptionString != null && exceptionString.Length > 0)
			{
				writer.WriteLine(exceptionString);
			}
		}
	}
}
