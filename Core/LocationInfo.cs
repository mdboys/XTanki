using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A60 RID: 10848
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class LocationInfo
	{
		// Token: 0x06009449 RID: 37961 RVA: 0x00143280 File Offset: 0x00141480
		public LocationInfo(Type callerStackBoundaryDeclaringType)
		{
			this.ClassName = "?";
			this.FileName = "?";
			this.LineNumber = "?";
			this.MethodName = "?";
			this.FullInfo = "?";
			if (callerStackBoundaryDeclaringType == null)
			{
				return;
			}
			try
			{
				StackTrace stackTrace = new StackTrace(true);
				int i = 0;
				while (i < stackTrace.FrameCount)
				{
					StackFrame frame = stackTrace.GetFrame(i);
					if (frame == null || frame.GetMethod().DeclaringType != callerStackBoundaryDeclaringType)
					{
						i++;
					}
					else
					{
						IL_0098:
						while (i < stackTrace.FrameCount)
						{
							StackFrame frame2 = stackTrace.GetFrame(i);
							if (frame2 != null && frame2.GetMethod().DeclaringType != callerStackBoundaryDeclaringType)
							{
								break;
							}
							i++;
						}
						if (i >= stackTrace.FrameCount)
						{
							return;
						}
						int num = stackTrace.FrameCount - i;
						ArrayList arrayList = new ArrayList(num);
						this.StackFrames = new StackFrameItem[num];
						for (int j = i; j < stackTrace.FrameCount; j++)
						{
							arrayList.Add(new StackFrameItem(stackTrace.GetFrame(j)));
						}
						arrayList.CopyTo(this.StackFrames, 0);
						StackFrame frame3 = stackTrace.GetFrame(i);
						if (frame3 == null)
						{
							return;
						}
						MethodBase method = frame3.GetMethod();
						if (method != null)
						{
							this.MethodName = method.Name;
							if (method.DeclaringType != null)
							{
								this.ClassName = method.DeclaringType.FullName;
							}
						}
						this.FileName = frame3.GetFileName();
						this.LineNumber = frame3.GetFileLineNumber().ToString(NumberFormatInfo.InvariantInfo);
						this.FullInfo = string.Concat(new string[] { this.ClassName, ".", this.MethodName, "(", this.FileName, ":", this.LineNumber, ")" });
						return;
					}
				}
				goto IL_0098;
			}
			catch (SecurityException)
			{
				LogLog.Debug(LocationInfo.declaringType, "Security exception while trying to get caller stack frame. Error Ignored. Location Information Not Available.");
			}
		}

		// Token: 0x0600944A RID: 37962 RVA: 0x00143484 File Offset: 0x00141684
		public LocationInfo(string className, string methodName, string fileName, string lineNumber)
		{
			this.ClassName = className;
			this.FileName = fileName;
			this.LineNumber = lineNumber;
			this.MethodName = methodName;
			this.FullInfo = string.Concat(new string[] { this.ClassName, ".", this.MethodName, "(", this.FileName, ":", this.LineNumber, ")" });
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x0600944B RID: 37963 RVA: 0x000588B1 File Offset: 0x00056AB1
		public string ClassName { get; }

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x0600944C RID: 37964 RVA: 0x000588B9 File Offset: 0x00056AB9
		public string FileName { get; }

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x0600944D RID: 37965 RVA: 0x000588C1 File Offset: 0x00056AC1
		public string LineNumber { get; }

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x0600944E RID: 37966 RVA: 0x000588C9 File Offset: 0x00056AC9
		public string MethodName { get; }

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x0600944F RID: 37967 RVA: 0x000588D1 File Offset: 0x00056AD1
		public string FullInfo { get; }

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06009450 RID: 37968 RVA: 0x000588D9 File Offset: 0x00056AD9
		public StackFrameItem[] StackFrames { get; }

		// Token: 0x04006225 RID: 25125
		private const string NA = "?";

		// Token: 0x04006226 RID: 25126
		private static readonly Type declaringType = typeof(LocationInfo);
	}
}
