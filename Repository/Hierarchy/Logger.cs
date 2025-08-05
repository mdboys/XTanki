using System;
using System.Runtime.CompilerServices;
using System.Security;
using log4net.Appender;
using log4net.Core;
using log4net.Util;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x02002A03 RID: 10755
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Logger : IAppenderAttachable, ILogger
	{
		// Token: 0x0600924D RID: 37453 RVA: 0x000577A0 File Offset: 0x000559A0
		protected Logger(string name)
		{
			this.m_name = string.Intern(name);
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x0600924E RID: 37454 RVA: 0x000577C6 File Offset: 0x000559C6
		// (set) Token: 0x0600924F RID: 37455 RVA: 0x000577CE File Offset: 0x000559CE
		public virtual Logger Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06009250 RID: 37456 RVA: 0x000577D7 File Offset: 0x000559D7
		// (set) Token: 0x06009251 RID: 37457 RVA: 0x000577DF File Offset: 0x000559DF
		public virtual bool Additivity
		{
			get
			{
				return this.m_additive;
			}
			set
			{
				this.m_additive = value;
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06009252 RID: 37458 RVA: 0x0013EA30 File Offset: 0x0013CC30
		public virtual Level EffectiveLevel
		{
			get
			{
				for (Logger logger = this; logger != null; logger = logger.m_parent)
				{
					Level level = logger.m_level;
					if (level != null)
					{
						return level;
					}
				}
				return null;
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06009253 RID: 37459 RVA: 0x000577E8 File Offset: 0x000559E8
		// (set) Token: 0x06009254 RID: 37460 RVA: 0x000577F0 File Offset: 0x000559F0
		public virtual Hierarchy Hierarchy
		{
			get
			{
				return this.m_hierarchy;
			}
			set
			{
				this.m_hierarchy = value;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06009255 RID: 37461 RVA: 0x000577F9 File Offset: 0x000559F9
		// (set) Token: 0x06009256 RID: 37462 RVA: 0x00057801 File Offset: 0x00055A01
		public virtual Level Level
		{
			get
			{
				return this.m_level;
			}
			set
			{
				this.m_level = value;
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06009257 RID: 37463 RVA: 0x0013EA58 File Offset: 0x0013CC58
		public virtual AppenderCollection Appenders
		{
			get
			{
				this.m_appenderLock.AcquireReaderLock();
				AppenderCollection appenderCollection;
				try
				{
					if (this.m_appenderAttachedImpl == null)
					{
						appenderCollection = AppenderCollection.EmptyCollection;
					}
					else
					{
						appenderCollection = this.m_appenderAttachedImpl.Appenders;
					}
				}
				finally
				{
					this.m_appenderLock.ReleaseReaderLock();
				}
				return appenderCollection;
			}
		}

		// Token: 0x06009258 RID: 37464 RVA: 0x0013EAAC File Offset: 0x0013CCAC
		public virtual void AddAppender(IAppender newAppender)
		{
			if (newAppender == null)
			{
				throw new ArgumentNullException("newAppender");
			}
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (this.m_appenderAttachedImpl == null)
				{
					this.m_appenderAttachedImpl = new AppenderAttachedImpl();
				}
				this.m_appenderAttachedImpl.AddAppender(newAppender);
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06009259 RID: 37465 RVA: 0x0013EB10 File Offset: 0x0013CD10
		public virtual IAppender GetAppender(string name)
		{
			this.m_appenderLock.AcquireReaderLock();
			IAppender appender;
			try
			{
				if (this.m_appenderAttachedImpl == null || name == null)
				{
					appender = null;
				}
				else
				{
					appender = this.m_appenderAttachedImpl.GetAppender(name);
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseReaderLock();
			}
			return appender;
		}

		// Token: 0x0600925A RID: 37466 RVA: 0x0013EB64 File Offset: 0x0013CD64
		public virtual void RemoveAllAppenders()
		{
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (this.m_appenderAttachedImpl != null)
				{
					this.m_appenderAttachedImpl.RemoveAllAppenders();
					this.m_appenderAttachedImpl = null;
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
		}

		// Token: 0x0600925B RID: 37467 RVA: 0x0013EBB4 File Offset: 0x0013CDB4
		public virtual IAppender RemoveAppender(IAppender appender)
		{
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (appender != null && this.m_appenderAttachedImpl != null)
				{
					return this.m_appenderAttachedImpl.RemoveAppender(appender);
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
			return null;
		}

		// Token: 0x0600925C RID: 37468 RVA: 0x0013EC08 File Offset: 0x0013CE08
		public virtual IAppender RemoveAppender(string name)
		{
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (name != null && this.m_appenderAttachedImpl != null)
				{
					return this.m_appenderAttachedImpl.RemoveAppender(name);
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
			return null;
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x0600925D RID: 37469 RVA: 0x0005780A File Offset: 0x00055A0A
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x0600925E RID: 37470 RVA: 0x000577E8 File Offset: 0x000559E8
		public ILoggerRepository Repository
		{
			get
			{
				return this.m_hierarchy;
			}
		}

		// Token: 0x0600925F RID: 37471 RVA: 0x0013EC5C File Offset: 0x0013CE5C
		public virtual void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
		{
			try
			{
				if (this.IsEnabledFor(level))
				{
					this.ForcedLog((callerStackBoundaryDeclaringType == null) ? Logger.declaringType : callerStackBoundaryDeclaringType, level, message, exception);
				}
			}
			catch (Exception ex)
			{
				LogLog.Error(Logger.declaringType, "Exception while logging", ex);
			}
		}

		// Token: 0x06009260 RID: 37472 RVA: 0x0013ECAC File Offset: 0x0013CEAC
		public virtual void Log(LoggingEvent logEvent)
		{
			try
			{
				if (logEvent != null && this.IsEnabledFor(logEvent.Level))
				{
					this.ForcedLog(logEvent);
				}
			}
			catch (Exception ex)
			{
				LogLog.Error(Logger.declaringType, "Exception while logging", ex);
			}
		}

		// Token: 0x06009261 RID: 37473 RVA: 0x0013ECF8 File Offset: 0x0013CEF8
		public virtual bool IsEnabledFor(Level level)
		{
			try
			{
				if (level != null)
				{
					if (this.m_hierarchy.IsDisabled(level))
					{
						return false;
					}
					return level >= this.EffectiveLevel;
				}
			}
			catch (Exception ex)
			{
				LogLog.Error(Logger.declaringType, "Exception while logging", ex);
			}
			return false;
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x0013ED58 File Offset: 0x0013CF58
		protected virtual void CallAppenders(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			int num = 0;
			for (Logger logger = this; logger != null; logger = logger.m_parent)
			{
				if (logger.m_appenderAttachedImpl != null)
				{
					logger.m_appenderLock.AcquireReaderLock();
					try
					{
						if (logger.m_appenderAttachedImpl != null)
						{
							num += logger.m_appenderAttachedImpl.AppendLoopOnAppenders(loggingEvent);
						}
					}
					finally
					{
						logger.m_appenderLock.ReleaseReaderLock();
					}
				}
				if (!logger.m_additive)
				{
					break;
				}
			}
			if (!this.m_hierarchy.EmittedNoAppenderWarning && num == 0)
			{
				LogLog.Debug(Logger.declaringType, string.Concat(new string[]
				{
					"No appenders could be found for logger [",
					this.Name,
					"] repository [",
					this.Repository.Name,
					"]"
				}));
				LogLog.Debug(Logger.declaringType, "Please initialize the log4net system properly.");
				try
				{
					LogLog.Debug(Logger.declaringType, "    Current AppDomain context information: ");
					LogLog.Debug(Logger.declaringType, "       BaseDirectory   : " + SystemInfo.ApplicationBaseDirectory);
					LogLog.Debug(Logger.declaringType, "       FriendlyName    : " + AppDomain.CurrentDomain.FriendlyName);
					LogLog.Debug(Logger.declaringType, "       DynamicDirectory: " + AppDomain.CurrentDomain.DynamicDirectory);
				}
				catch (SecurityException)
				{
				}
				this.m_hierarchy.EmittedNoAppenderWarning = true;
			}
		}

		// Token: 0x06009263 RID: 37475 RVA: 0x0013EEC0 File Offset: 0x0013D0C0
		public virtual void CloseNestedAppenders()
		{
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (this.m_appenderAttachedImpl != null)
				{
					foreach (IAppender appender in this.m_appenderAttachedImpl.Appenders)
					{
						if (appender is IAppenderAttachable)
						{
							appender.Close();
						}
					}
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06009264 RID: 37476 RVA: 0x00057812 File Offset: 0x00055A12
		public virtual void Log(Level level, object message, Exception exception)
		{
			if (this.IsEnabledFor(level))
			{
				this.ForcedLog(Logger.declaringType, level, message, exception);
			}
		}

		// Token: 0x06009265 RID: 37477 RVA: 0x0005782B File Offset: 0x00055A2B
		protected virtual void ForcedLog(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
		{
			this.CallAppenders(new LoggingEvent(callerStackBoundaryDeclaringType, this.Hierarchy, this.Name, level, message, exception));
		}

		// Token: 0x06009266 RID: 37478 RVA: 0x00057849 File Offset: 0x00055A49
		protected virtual void ForcedLog(LoggingEvent logEvent)
		{
			logEvent.EnsureRepository(this.Hierarchy);
			this.CallAppenders(logEvent);
		}

		// Token: 0x0400615B RID: 24923
		private static readonly Type declaringType = typeof(Logger);

		// Token: 0x0400615C RID: 24924
		private readonly ReaderWriterLock m_appenderLock = new ReaderWriterLock();

		// Token: 0x0400615D RID: 24925
		private readonly string m_name;

		// Token: 0x0400615E RID: 24926
		private bool m_additive = true;

		// Token: 0x0400615F RID: 24927
		private AppenderAttachedImpl m_appenderAttachedImpl;

		// Token: 0x04006160 RID: 24928
		private Hierarchy m_hierarchy;

		// Token: 0x04006161 RID: 24929
		private Level m_level;

		// Token: 0x04006162 RID: 24930
		private Logger m_parent;
	}
}
