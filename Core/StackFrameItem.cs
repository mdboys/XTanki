using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A6D RID: 10861
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class StackFrameItem
	{
		// Token: 0x060094DE RID: 38110 RVA: 0x0014437C File Offset: 0x0014257C
		public StackFrameItem(StackFrame frame)
		{
			this.LineNumber = "?";
			this.FileName = "?";
			this.Method = new MethodItem();
			this.ClassName = "?";
			try
			{
				this.LineNumber = frame.GetFileLineNumber().ToString(NumberFormatInfo.InvariantInfo);
				this.FileName = frame.GetFileName();
				MethodBase method = frame.GetMethod();
				if (method != null)
				{
					if (method.DeclaringType != null)
					{
						this.ClassName = method.DeclaringType.FullName;
					}
					this.Method = new MethodItem(method);
				}
			}
			catch (Exception ex)
			{
				LogLog.Error(StackFrameItem.declaringType, "An exception ocurred while retreiving stack frame information.", ex);
			}
			this.FullInfo = string.Concat(new string[]
			{
				this.ClassName,
				".",
				this.Method.Name,
				"(",
				this.FileName,
				":",
				this.LineNumber,
				")"
			});
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x060094DF RID: 38111 RVA: 0x0005922A File Offset: 0x0005742A
		public string ClassName { get; }

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x060094E0 RID: 38112 RVA: 0x00059232 File Offset: 0x00057432
		public string FileName { get; }

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x060094E1 RID: 38113 RVA: 0x0005923A File Offset: 0x0005743A
		public string LineNumber { get; }

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x060094E2 RID: 38114 RVA: 0x00059242 File Offset: 0x00057442
		public MethodItem Method { get; }

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x060094E3 RID: 38115 RVA: 0x0005924A File Offset: 0x0005744A
		public string FullInfo { get; }

		// Token: 0x04006256 RID: 25174
		private const string NA = "?";

		// Token: 0x04006257 RID: 25175
		private static readonly Type declaringType = typeof(StackFrameItem);
	}
}
