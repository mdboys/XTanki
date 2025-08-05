using System;
using System.Runtime.CompilerServices;
using System.Text;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A36 RID: 10806
	[NullableContext(1)]
	[Nullable(0)]
	internal class StackTraceDetailPatternConverter : StackTracePatternConverter
	{
		// Token: 0x0600936A RID: 37738 RVA: 0x00141FCC File Offset: 0x001401CC
		internal override string GetMethodInformation(MethodItem method)
		{
			string text = string.Empty;
			try
			{
				string text2 = string.Empty;
				string[] parameters = method.Parameters;
				StringBuilder stringBuilder = new StringBuilder();
				if (parameters != null && parameters.GetUpperBound(0) > 0)
				{
					for (int i = 0; i <= parameters.GetUpperBound(0); i++)
					{
						stringBuilder.AppendFormat("{0}, ", parameters[i]);
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
					text2 = stringBuilder.ToString();
				}
				text = base.GetMethodInformation(method) + "(" + text2 + ")";
			}
			catch (Exception ex)
			{
				LogLog.Error(StackTraceDetailPatternConverter.declaringType, "An exception ocurred while retreiving method information.", ex);
			}
			return text;
		}

		// Token: 0x040061CA RID: 25034
		private static readonly Type declaringType = typeof(StackTracePatternConverter);
	}
}
