using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029C9 RID: 10697
	[NullableContext(1)]
	[Nullable(0)]
	public class OnlyOnceErrorHandler : IErrorHandler
	{
		// Token: 0x060090A2 RID: 37026 RVA: 0x000568DF File Offset: 0x00054ADF
		public OnlyOnceErrorHandler()
		{
			this.m_prefix = string.Empty;
		}

		// Token: 0x060090A3 RID: 37027 RVA: 0x000568F9 File Offset: 0x00054AF9
		public OnlyOnceErrorHandler(string prefix)
		{
			this.m_prefix = prefix;
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x060090A4 RID: 37028 RVA: 0x0005690F File Offset: 0x00054B0F
		// (set) Token: 0x060090A5 RID: 37029 RVA: 0x00056917 File Offset: 0x00054B17
		public bool IsEnabled { get; private set; } = true;

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x060090A6 RID: 37030 RVA: 0x00056920 File Offset: 0x00054B20
		// (set) Token: 0x060090A7 RID: 37031 RVA: 0x00056928 File Offset: 0x00054B28
		public DateTime EnabledDate { get; private set; }

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x060090A8 RID: 37032 RVA: 0x00056931 File Offset: 0x00054B31
		// (set) Token: 0x060090A9 RID: 37033 RVA: 0x00056939 File Offset: 0x00054B39
		public string ErrorMessage { get; private set; }

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x060090AA RID: 37034 RVA: 0x00056942 File Offset: 0x00054B42
		// (set) Token: 0x060090AB RID: 37035 RVA: 0x0005694A File Offset: 0x00054B4A
		public Exception Exception { get; private set; }

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x060090AC RID: 37036 RVA: 0x00056953 File Offset: 0x00054B53
		// (set) Token: 0x060090AD RID: 37037 RVA: 0x0005695B File Offset: 0x00054B5B
		public ErrorCode ErrorCode { get; private set; }

		// Token: 0x060090AE RID: 37038 RVA: 0x00056964 File Offset: 0x00054B64
		public void Error(string message, Exception e, ErrorCode errorCode)
		{
			if (this.IsEnabled)
			{
				this.FirstError(message, e, errorCode);
			}
		}

		// Token: 0x060090AF RID: 37039 RVA: 0x00056977 File Offset: 0x00054B77
		public void Error(string message, Exception e)
		{
			this.Error(message, e, ErrorCode.GenericFailure);
		}

		// Token: 0x060090B0 RID: 37040 RVA: 0x00056982 File Offset: 0x00054B82
		public void Error(string message)
		{
			this.Error(message, null, ErrorCode.GenericFailure);
		}

		// Token: 0x060090B1 RID: 37041 RVA: 0x0005698D File Offset: 0x00054B8D
		public void Reset()
		{
			this.EnabledDate = DateTime.MinValue;
			this.ErrorCode = ErrorCode.GenericFailure;
			this.Exception = null;
			this.ErrorMessage = null;
			this.IsEnabled = true;
		}

		// Token: 0x060090B2 RID: 37042 RVA: 0x0013BD58 File Offset: 0x00139F58
		public virtual void FirstError(string message, Exception e, ErrorCode errorCode)
		{
			this.EnabledDate = DateTime.Now;
			this.ErrorCode = errorCode;
			this.Exception = e;
			this.ErrorMessage = message;
			this.IsEnabled = false;
			if (LogLog.InternalDebugging && !LogLog.QuietMode)
			{
				LogLog.Error(OnlyOnceErrorHandler.declaringType, string.Format("[{0}] ErrorCode: {1}. {2}", this.m_prefix, errorCode, message), e);
			}
		}

		// Token: 0x040060F5 RID: 24821
		private static readonly Type declaringType = typeof(OnlyOnceErrorHandler);

		// Token: 0x040060F6 RID: 24822
		private readonly string m_prefix;
	}
}
