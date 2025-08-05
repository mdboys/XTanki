using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A92 RID: 10898
	[NullableContext(1)]
	[Nullable(0)]
	public class LocalSyslogAppender : AppenderSkeleton
	{
		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x0600962D RID: 38445 RVA: 0x00059E0D File Offset: 0x0005800D
		// (set) Token: 0x0600962E RID: 38446 RVA: 0x00059E15 File Offset: 0x00058015
		public string Identity { get; set; }

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x0600962F RID: 38447 RVA: 0x00059E1E File Offset: 0x0005801E
		// (set) Token: 0x06009630 RID: 38448 RVA: 0x00059E26 File Offset: 0x00058026
		public LocalSyslogAppender.SyslogFacility Facility { get; set; } = LocalSyslogAppender.SyslogFacility.User;

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06009631 RID: 38449 RVA: 0x00005B7A File Offset: 0x00003D7A
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009632 RID: 38450 RVA: 0x00059E2F File Offset: 0x0005802F
		public void AddMapping(LocalSyslogAppender.LevelSeverity mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x06009633 RID: 38451 RVA: 0x00146C68 File Offset: 0x00144E68
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			this.m_levelMapping.ActivateOptions();
			string text = this.Identity;
			if (text == null)
			{
				text = SystemInfo.ApplicationFriendlyName;
			}
			this.m_handleToIdentity = Marshal.StringToHGlobalAnsi(text);
			LocalSyslogAppender.openlog(this.m_handleToIdentity, 1, this.Facility);
		}

		// Token: 0x06009634 RID: 38452 RVA: 0x00146CB4 File Offset: 0x00144EB4
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		protected override void Append(LoggingEvent loggingEvent)
		{
			int num = LocalSyslogAppender.GeneratePriority(this.Facility, this.GetSeverity(loggingEvent.Level));
			string text = base.RenderLoggingEvent(loggingEvent);
			LocalSyslogAppender.syslog(num, "%s", text);
		}

		// Token: 0x06009635 RID: 38453 RVA: 0x00146CEC File Offset: 0x00144EEC
		protected override void OnClose()
		{
			base.OnClose();
			try
			{
				LocalSyslogAppender.closelog();
			}
			catch (DllNotFoundException)
			{
			}
			if (this.m_handleToIdentity != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_handleToIdentity);
			}
		}

		// Token: 0x06009636 RID: 38454 RVA: 0x00146D38 File Offset: 0x00144F38
		protected virtual LocalSyslogAppender.SyslogSeverity GetSeverity(Level level)
		{
			LocalSyslogAppender.LevelSeverity levelSeverity = this.m_levelMapping.Lookup(level) as LocalSyslogAppender.LevelSeverity;
			if (levelSeverity != null)
			{
				return levelSeverity.Severity;
			}
			if (level >= Level.Alert)
			{
				return LocalSyslogAppender.SyslogSeverity.Alert;
			}
			if (level >= Level.Critical)
			{
				return LocalSyslogAppender.SyslogSeverity.Critical;
			}
			if (level >= Level.Error)
			{
				return LocalSyslogAppender.SyslogSeverity.Error;
			}
			if (level >= Level.Warn)
			{
				return LocalSyslogAppender.SyslogSeverity.Warning;
			}
			if (level >= Level.Notice)
			{
				return LocalSyslogAppender.SyslogSeverity.Notice;
			}
			if (level >= Level.Info)
			{
				return LocalSyslogAppender.SyslogSeverity.Informational;
			}
			return LocalSyslogAppender.SyslogSeverity.Debug;
		}

		// Token: 0x06009637 RID: 38455 RVA: 0x00059E3D File Offset: 0x0005803D
		private static int GeneratePriority(LocalSyslogAppender.SyslogFacility facility, LocalSyslogAppender.SyslogSeverity severity)
		{
			return (int)(facility * LocalSyslogAppender.SyslogFacility.Uucp + (int)severity);
		}

		// Token: 0x06009638 RID: 38456
		[DllImport("libc")]
		private static extern void openlog(IntPtr ident, int option, LocalSyslogAppender.SyslogFacility facility);

		// Token: 0x06009639 RID: 38457
		[DllImport("libc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void syslog(int priority, string format, string message);

		// Token: 0x0600963A RID: 38458
		[DllImport("libc")]
		private static extern void closelog();

		// Token: 0x040062C5 RID: 25285
		private IntPtr m_handleToIdentity = IntPtr.Zero;

		// Token: 0x040062C6 RID: 25286
		private readonly LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x02002A93 RID: 10899
		[NullableContext(0)]
		public enum SyslogFacility
		{
			// Token: 0x040062CA RID: 25290
			Kernel,
			// Token: 0x040062CB RID: 25291
			User,
			// Token: 0x040062CC RID: 25292
			Mail,
			// Token: 0x040062CD RID: 25293
			Daemons,
			// Token: 0x040062CE RID: 25294
			Authorization,
			// Token: 0x040062CF RID: 25295
			Syslog,
			// Token: 0x040062D0 RID: 25296
			Printer,
			// Token: 0x040062D1 RID: 25297
			News,
			// Token: 0x040062D2 RID: 25298
			Uucp,
			// Token: 0x040062D3 RID: 25299
			Clock,
			// Token: 0x040062D4 RID: 25300
			Authorization2,
			// Token: 0x040062D5 RID: 25301
			Ftp,
			// Token: 0x040062D6 RID: 25302
			Ntp,
			// Token: 0x040062D7 RID: 25303
			Audit,
			// Token: 0x040062D8 RID: 25304
			Alert,
			// Token: 0x040062D9 RID: 25305
			Clock2,
			// Token: 0x040062DA RID: 25306
			Local0,
			// Token: 0x040062DB RID: 25307
			Local1,
			// Token: 0x040062DC RID: 25308
			Local2,
			// Token: 0x040062DD RID: 25309
			Local3,
			// Token: 0x040062DE RID: 25310
			Local4,
			// Token: 0x040062DF RID: 25311
			Local5,
			// Token: 0x040062E0 RID: 25312
			Local6,
			// Token: 0x040062E1 RID: 25313
			Local7
		}

		// Token: 0x02002A94 RID: 10900
		[NullableContext(0)]
		public enum SyslogSeverity
		{
			// Token: 0x040062E3 RID: 25315
			Emergency,
			// Token: 0x040062E4 RID: 25316
			Alert,
			// Token: 0x040062E5 RID: 25317
			Critical,
			// Token: 0x040062E6 RID: 25318
			Error,
			// Token: 0x040062E7 RID: 25319
			Warning,
			// Token: 0x040062E8 RID: 25320
			Notice,
			// Token: 0x040062E9 RID: 25321
			Informational,
			// Token: 0x040062EA RID: 25322
			Debug
		}

		// Token: 0x02002A95 RID: 10901
		[NullableContext(0)]
		public class LevelSeverity : LevelMappingEntry
		{
			// Token: 0x17001792 RID: 6034
			// (get) Token: 0x0600963C RID: 38460 RVA: 0x00059E69 File Offset: 0x00058069
			// (set) Token: 0x0600963D RID: 38461 RVA: 0x00059E71 File Offset: 0x00058071
			public LocalSyslogAppender.SyslogSeverity Severity { get; set; }
		}
	}
}
