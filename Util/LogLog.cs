using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029C2 RID: 10690
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LogLog
	{
		// Token: 0x0600906B RID: 36971 RVA: 0x00056619 File Offset: 0x00054819
		public LogLog(Type source, string prefix, string message, Exception exception)
		{
			this.TimeStamp = DateTime.Now;
			this.Source = source;
			this.Prefix = prefix;
			this.Message = message;
			this.Exception = exception;
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x0600906C RID: 36972 RVA: 0x00056649 File Offset: 0x00054849
		public Type Source { get; }

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x0600906D RID: 36973 RVA: 0x00056651 File Offset: 0x00054851
		public DateTime TimeStamp { get; }

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x0600906E RID: 36974 RVA: 0x00056659 File Offset: 0x00054859
		public string Prefix { get; }

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x0600906F RID: 36975 RVA: 0x00056661 File Offset: 0x00054861
		public string Message { get; }

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06009070 RID: 36976 RVA: 0x00056669 File Offset: 0x00054869
		public Exception Exception { get; }

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06009071 RID: 36977 RVA: 0x00056671 File Offset: 0x00054871
		// (set) Token: 0x06009072 RID: 36978 RVA: 0x00056678 File Offset: 0x00054878
		public static bool InternalDebugging { get; set; }

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06009073 RID: 36979 RVA: 0x00056680 File Offset: 0x00054880
		// (set) Token: 0x06009074 RID: 36980 RVA: 0x00056687 File Offset: 0x00054887
		public static bool QuietMode { get; set; }

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06009075 RID: 36981 RVA: 0x0005668F File Offset: 0x0005488F
		// (set) Token: 0x06009076 RID: 36982 RVA: 0x00056696 File Offset: 0x00054896
		public static bool EmitInternalMessages { get; set; } = true;

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06009077 RID: 36983 RVA: 0x0005669E File Offset: 0x0005489E
		public static bool IsDebugEnabled
		{
			get
			{
				return LogLog.InternalDebugging && !LogLog.QuietMode;
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06009078 RID: 36984 RVA: 0x000566B1 File Offset: 0x000548B1
		public static bool IsWarnEnabled
		{
			get
			{
				return !LogLog.QuietMode;
			}
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06009079 RID: 36985 RVA: 0x000566B1 File Offset: 0x000548B1
		public static bool IsErrorEnabled
		{
			get
			{
				return !LogLog.QuietMode;
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600907A RID: 36986 RVA: 0x0013BC7C File Offset: 0x00139E7C
		// (remove) Token: 0x0600907B RID: 36987 RVA: 0x0013BCB0 File Offset: 0x00139EB0
		public static event LogReceivedEventHandler LogReceived;

		// Token: 0x0600907C RID: 36988 RVA: 0x000566BB File Offset: 0x000548BB
		public override string ToString()
		{
			return this.Prefix + this.Source.Name + ": " + this.Message;
		}

		// Token: 0x0600907D RID: 36989 RVA: 0x000566DE File Offset: 0x000548DE
		public static void OnLogReceived(Type source, string prefix, string message, Exception exception)
		{
			LogReceivedEventHandler logReceived = LogLog.LogReceived;
			if (logReceived == null)
			{
				return;
			}
			logReceived(null, new LogReceivedEventArgs(new LogLog(source, prefix, message, exception)));
		}

		// Token: 0x0600907E RID: 36990 RVA: 0x000566FE File Offset: 0x000548FE
		public static void Debug(Type source, string message)
		{
			if (LogLog.IsDebugEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitOutLine("log4net: " + message);
				}
				LogLog.OnLogReceived(source, "log4net: ", message, null);
			}
		}

		// Token: 0x0600907F RID: 36991 RVA: 0x0005672B File Offset: 0x0005492B
		public static void Debug(Type source, string message, Exception exception)
		{
			if (!LogLog.IsDebugEnabled)
			{
				return;
			}
			if (LogLog.EmitInternalMessages)
			{
				LogLog.EmitOutLine("log4net: " + message);
				if (exception != null)
				{
					LogLog.EmitOutLine(exception.ToString());
				}
			}
			LogLog.OnLogReceived(source, "log4net: ", message, exception);
		}

		// Token: 0x06009080 RID: 36992 RVA: 0x00056767 File Offset: 0x00054967
		public static void Warn(Type source, string message)
		{
			if (LogLog.IsWarnEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitErrorLine("log4net:WARN " + message);
				}
				LogLog.OnLogReceived(source, "log4net:WARN ", message, null);
			}
		}

		// Token: 0x06009081 RID: 36993 RVA: 0x00056794 File Offset: 0x00054994
		public static void Warn(Type source, string message, Exception exception)
		{
			if (!LogLog.IsWarnEnabled)
			{
				return;
			}
			if (LogLog.EmitInternalMessages)
			{
				LogLog.EmitErrorLine("log4net:WARN " + message);
				if (exception != null)
				{
					LogLog.EmitErrorLine(exception.ToString());
				}
			}
			LogLog.OnLogReceived(source, "log4net:WARN ", message, exception);
		}

		// Token: 0x06009082 RID: 36994 RVA: 0x000567D0 File Offset: 0x000549D0
		public static void Error(Type source, string message)
		{
			if (LogLog.IsErrorEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitErrorLine("log4net:ERROR " + message);
				}
				LogLog.OnLogReceived(source, "log4net:ERROR ", message, null);
			}
		}

		// Token: 0x06009083 RID: 36995 RVA: 0x000567FD File Offset: 0x000549FD
		public static void Error(Type source, string message, Exception exception)
		{
			if (!LogLog.IsErrorEnabled)
			{
				return;
			}
			if (LogLog.EmitInternalMessages)
			{
				LogLog.EmitErrorLine("log4net:ERROR " + message);
				if (exception != null)
				{
					LogLog.EmitErrorLine(exception.ToString());
				}
			}
			LogLog.OnLogReceived(source, "log4net:ERROR ", message, exception);
		}

		// Token: 0x06009084 RID: 36996 RVA: 0x0013BCE4 File Offset: 0x00139EE4
		private static void EmitOutLine(string message)
		{
			try
			{
				Console.Out.WriteLine(message);
			}
			catch
			{
			}
		}

		// Token: 0x06009085 RID: 36997 RVA: 0x0013BD14 File Offset: 0x00139F14
		private static void EmitErrorLine(string message)
		{
			try
			{
				Console.Error.WriteLine(message);
			}
			catch
			{
			}
		}

		// Token: 0x040060E3 RID: 24803
		private const string PREFIX = "log4net: ";

		// Token: 0x040060E4 RID: 24804
		private const string ERR_PREFIX = "log4net:ERROR ";

		// Token: 0x040060E5 RID: 24805
		private const string WARN_PREFIX = "log4net:WARN ";

		// Token: 0x020029C3 RID: 10691
		[Nullable(0)]
		public class LogReceivedAdapter : IDisposable
		{
			// Token: 0x06009086 RID: 36998 RVA: 0x00056839 File Offset: 0x00054A39
			public LogReceivedAdapter(IList items)
			{
				this.Items = items;
				this.handler = new LogReceivedEventHandler(this.LogLog_LogReceived);
				LogLog.LogReceived += this.handler;
			}

			// Token: 0x17001675 RID: 5749
			// (get) Token: 0x06009087 RID: 36999 RVA: 0x00056865 File Offset: 0x00054A65
			public IList Items { get; }

			// Token: 0x06009088 RID: 37000 RVA: 0x0005686D File Offset: 0x00054A6D
			public void Dispose()
			{
				LogLog.LogReceived -= this.handler;
			}

			// Token: 0x06009089 RID: 37001 RVA: 0x0005687A File Offset: 0x00054A7A
			private void LogLog_LogReceived(object source, LogReceivedEventArgs e)
			{
				this.Items.Add(e.LogLog);
			}

			// Token: 0x040060EF RID: 24815
			private readonly LogReceivedEventHandler handler;
		}
	}
}
