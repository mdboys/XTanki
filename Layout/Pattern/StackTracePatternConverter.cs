using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A37 RID: 10807
	[NullableContext(1)]
	[Nullable(0)]
	internal class StackTracePatternConverter : PatternLayoutConverter, IOptionHandler
	{
		// Token: 0x0600936D RID: 37741 RVA: 0x00142088 File Offset: 0x00140288
		public void ActivateOptions()
		{
			if (this.Option == null)
			{
				return;
			}
			string text = this.Option.Trim();
			if (text.Length == 0)
			{
				return;
			}
			int num;
			if (!SystemInfo.TryParse(text, out num))
			{
				LogLog.Error(StackTracePatternConverter.declaringType, "StackTracePatternConverter: StackFrameLevel option \"" + text + "\" not a decimal integer.");
				return;
			}
			if (num <= 0)
			{
				LogLog.Error(StackTracePatternConverter.declaringType, "StackTracePatternConverter: StackeFrameLevel option (" + text + ") isn't a positive integer.");
				return;
			}
			this.m_stackFrameLevel = num;
		}

		// Token: 0x0600936E RID: 37742 RVA: 0x00142100 File Offset: 0x00140300
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			StackFrameItem[] stackFrames = loggingEvent.LocationInformation.StackFrames;
			if (stackFrames == null || stackFrames.Length == 0)
			{
				LogLog.Error(StackTracePatternConverter.declaringType, "loggingEvent.LocationInformation.StackFrames was null or empty.");
				return;
			}
			int i = this.m_stackFrameLevel - 1;
			while (i >= 0)
			{
				if (i >= stackFrames.Length)
				{
					i--;
				}
				else
				{
					StackFrameItem stackFrameItem = stackFrames[i];
					writer.Write("{0}.{1}", stackFrameItem.ClassName, this.GetMethodInformation(stackFrameItem.Method));
					if (i > 0)
					{
						writer.Write(" > ");
					}
					i--;
				}
			}
		}

		// Token: 0x0600936F RID: 37743 RVA: 0x0005816D File Offset: 0x0005636D
		internal virtual string GetMethodInformation(MethodItem method)
		{
			return method.Name;
		}

		// Token: 0x040061CB RID: 25035
		private static readonly Type declaringType = typeof(StackTracePatternConverter);

		// Token: 0x040061CC RID: 25036
		private int m_stackFrameLevel = 1;
	}
}
