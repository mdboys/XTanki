using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A97 RID: 10903
	[NullableContext(1)]
	[Nullable(0)]
	public class RemoteSyslogAppender : UdpAppender
	{
		// Token: 0x06009647 RID: 38471 RVA: 0x00059EDC File Offset: 0x000580DC
		public RemoteSyslogAppender()
		{
			base.RemotePort = 514;
			base.RemoteAddress = IPAddress.Parse("127.0.0.1");
			base.Encoding = Encoding.ASCII;
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06009648 RID: 38472 RVA: 0x00059F1C File Offset: 0x0005811C
		// (set) Token: 0x06009649 RID: 38473 RVA: 0x00059F24 File Offset: 0x00058124
		public PatternLayout Identity { get; set; }

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x0600964A RID: 38474 RVA: 0x00059F2D File Offset: 0x0005812D
		// (set) Token: 0x0600964B RID: 38475 RVA: 0x00059F35 File Offset: 0x00058135
		public RemoteSyslogAppender.SyslogFacility Facility { get; set; } = RemoteSyslogAppender.SyslogFacility.User;

		// Token: 0x0600964C RID: 38476 RVA: 0x00059F3E File Offset: 0x0005813E
		public void AddMapping(RemoteSyslogAppender.LevelSeverity mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x0600964D RID: 38477 RVA: 0x00146EA8 File Offset: 0x001450A8
		protected override void Append(LoggingEvent loggingEvent)
		{
			try
			{
				int num = RemoteSyslogAppender.GeneratePriority(this.Facility, this.GetSeverity(loggingEvent.Level));
				string text = ((this.Identity == null) ? loggingEvent.Domain : this.Identity.Format(loggingEvent));
				string text2 = base.RenderLoggingEvent(loggingEvent);
				int i = 0;
				StringBuilder stringBuilder = new StringBuilder();
				while (i < text2.Length)
				{
					stringBuilder.Length = 0;
					stringBuilder.Append('<');
					stringBuilder.Append(num);
					stringBuilder.Append('>');
					stringBuilder.Append(text);
					stringBuilder.Append(": ");
					while (i < text2.Length)
					{
						char c = text2[i];
						if (c >= ' ' && c <= '~')
						{
							stringBuilder.Append(c);
						}
						else
						{
							bool flag = c == '\n' || c == '\r';
							if (flag)
							{
								if (text2.Length > i + 1 && (text2[i + 1] == '\r' || text2[i + 1] == '\n'))
								{
									i++;
								}
								i++;
								break;
							}
						}
						i++;
					}
					byte[] bytes = base.Encoding.GetBytes(stringBuilder.ToString());
					base.Client.Send(bytes, bytes.Length, base.RemoteEndPoint);
				}
			}
			catch (Exception ex)
			{
				this.ErrorHandler.Error(string.Format("Unable to send logging event to remote syslog {0} on port {1}.", base.RemoteAddress, base.RemotePort), ex, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x0600964E RID: 38478 RVA: 0x00059F4C File Offset: 0x0005814C
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			this.m_levelMapping.ActivateOptions();
		}

		// Token: 0x0600964F RID: 38479 RVA: 0x00147038 File Offset: 0x00145238
		protected virtual RemoteSyslogAppender.SyslogSeverity GetSeverity(Level level)
		{
			RemoteSyslogAppender.LevelSeverity levelSeverity = this.m_levelMapping.Lookup(level) as RemoteSyslogAppender.LevelSeverity;
			if (levelSeverity != null)
			{
				return levelSeverity.Severity;
			}
			if (level >= Level.Alert)
			{
				return RemoteSyslogAppender.SyslogSeverity.Alert;
			}
			if (level >= Level.Critical)
			{
				return RemoteSyslogAppender.SyslogSeverity.Critical;
			}
			if (level >= Level.Error)
			{
				return RemoteSyslogAppender.SyslogSeverity.Error;
			}
			if (level >= Level.Warn)
			{
				return RemoteSyslogAppender.SyslogSeverity.Warning;
			}
			if (level >= Level.Notice)
			{
				return RemoteSyslogAppender.SyslogSeverity.Notice;
			}
			if (level >= Level.Info)
			{
				return RemoteSyslogAppender.SyslogSeverity.Informational;
			}
			return RemoteSyslogAppender.SyslogSeverity.Debug;
		}

		// Token: 0x06009650 RID: 38480 RVA: 0x001470BC File Offset: 0x001452BC
		public static int GeneratePriority(RemoteSyslogAppender.SyslogFacility facility, RemoteSyslogAppender.SyslogSeverity severity)
		{
			bool flag = facility < RemoteSyslogAppender.SyslogFacility.Kernel || facility > RemoteSyslogAppender.SyslogFacility.Local7;
			if (flag)
			{
				throw new ArgumentException("SyslogFacility out of range", "facility");
			}
			flag = severity < RemoteSyslogAppender.SyslogSeverity.Emergency || severity > RemoteSyslogAppender.SyslogSeverity.Debug;
			if (flag)
			{
				throw new ArgumentException("SyslogSeverity out of range", "severity");
			}
			return (int)(facility * RemoteSyslogAppender.SyslogFacility.Uucp + (int)severity);
		}

		// Token: 0x040062EE RID: 25326
		private const int DefaultSyslogPort = 514;

		// Token: 0x040062EF RID: 25327
		private const int c_renderBufferSize = 256;

		// Token: 0x040062F0 RID: 25328
		private const int c_renderBufferMaxCapacity = 1024;

		// Token: 0x040062F1 RID: 25329
		private readonly LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x02002A98 RID: 10904
		[NullableContext(0)]
		public enum SyslogFacility
		{
			// Token: 0x040062F5 RID: 25333
			Kernel,
			// Token: 0x040062F6 RID: 25334
			User,
			// Token: 0x040062F7 RID: 25335
			Mail,
			// Token: 0x040062F8 RID: 25336
			Daemons,
			// Token: 0x040062F9 RID: 25337
			Authorization,
			// Token: 0x040062FA RID: 25338
			Syslog,
			// Token: 0x040062FB RID: 25339
			Printer,
			// Token: 0x040062FC RID: 25340
			News,
			// Token: 0x040062FD RID: 25341
			Uucp,
			// Token: 0x040062FE RID: 25342
			Clock,
			// Token: 0x040062FF RID: 25343
			Authorization2,
			// Token: 0x04006300 RID: 25344
			Ftp,
			// Token: 0x04006301 RID: 25345
			Ntp,
			// Token: 0x04006302 RID: 25346
			Audit,
			// Token: 0x04006303 RID: 25347
			Alert,
			// Token: 0x04006304 RID: 25348
			Clock2,
			// Token: 0x04006305 RID: 25349
			Local0,
			// Token: 0x04006306 RID: 25350
			Local1,
			// Token: 0x04006307 RID: 25351
			Local2,
			// Token: 0x04006308 RID: 25352
			Local3,
			// Token: 0x04006309 RID: 25353
			Local4,
			// Token: 0x0400630A RID: 25354
			Local5,
			// Token: 0x0400630B RID: 25355
			Local6,
			// Token: 0x0400630C RID: 25356
			Local7
		}

		// Token: 0x02002A99 RID: 10905
		[NullableContext(0)]
		public enum SyslogSeverity
		{
			// Token: 0x0400630E RID: 25358
			Emergency,
			// Token: 0x0400630F RID: 25359
			Alert,
			// Token: 0x04006310 RID: 25360
			Critical,
			// Token: 0x04006311 RID: 25361
			Error,
			// Token: 0x04006312 RID: 25362
			Warning,
			// Token: 0x04006313 RID: 25363
			Notice,
			// Token: 0x04006314 RID: 25364
			Informational,
			// Token: 0x04006315 RID: 25365
			Debug
		}

		// Token: 0x02002A9A RID: 10906
		[NullableContext(0)]
		public class LevelSeverity : LevelMappingEntry
		{
			// Token: 0x17001797 RID: 6039
			// (get) Token: 0x06009651 RID: 38481 RVA: 0x00059F5F File Offset: 0x0005815F
			// (set) Token: 0x06009652 RID: 38482 RVA: 0x00059F67 File Offset: 0x00058167
			public RemoteSyslogAppender.SyslogSeverity Severity { get; set; }
		}
	}
}
