using System;
using System.Runtime.CompilerServices;
using log4net.Appender;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029B6 RID: 10678
	[NullableContext(1)]
	[Nullable(0)]
	public class AppenderAttachedImpl : IAppenderAttachable
	{
		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06009012 RID: 36882 RVA: 0x000563A8 File Offset: 0x000545A8
		public AppenderCollection Appenders
		{
			get
			{
				if (this.m_appenderList == null)
				{
					return AppenderCollection.EmptyCollection;
				}
				return AppenderCollection.ReadOnly(this.m_appenderList);
			}
		}

		// Token: 0x06009013 RID: 36883 RVA: 0x0013B1C0 File Offset: 0x001393C0
		public void AddAppender(IAppender newAppender)
		{
			if (newAppender == null)
			{
				throw new ArgumentNullException("newAppender");
			}
			this.m_appenderArray = null;
			if (this.m_appenderList == null)
			{
				this.m_appenderList = new AppenderCollection(1);
			}
			if (!this.m_appenderList.Contains(newAppender))
			{
				this.m_appenderList.Add(newAppender);
			}
		}

		// Token: 0x06009014 RID: 36884 RVA: 0x0013B214 File Offset: 0x00139414
		public IAppender GetAppender(string name)
		{
			if (this.m_appenderList != null && name != null)
			{
				foreach (IAppender appender in this.m_appenderList)
				{
					if (name == appender.Name)
					{
						return appender;
					}
				}
			}
			return null;
		}

		// Token: 0x06009015 RID: 36885 RVA: 0x0013B284 File Offset: 0x00139484
		public void RemoveAllAppenders()
		{
			if (this.m_appenderList == null)
			{
				return;
			}
			foreach (IAppender appender in this.m_appenderList)
			{
				try
				{
					appender.Close();
				}
				catch (Exception ex)
				{
					LogLog.Error(AppenderAttachedImpl.declaringType, "Failed to Close appender [" + appender.Name + "]", ex);
				}
			}
			this.m_appenderList = null;
			this.m_appenderArray = null;
		}

		// Token: 0x06009016 RID: 36886 RVA: 0x000563C3 File Offset: 0x000545C3
		public IAppender RemoveAppender(IAppender appender)
		{
			if (appender != null && this.m_appenderList != null)
			{
				this.m_appenderList.Remove(appender);
				if (this.m_appenderList.Count == 0)
				{
					this.m_appenderList = null;
				}
				this.m_appenderArray = null;
			}
			return appender;
		}

		// Token: 0x06009017 RID: 36887 RVA: 0x000563F8 File Offset: 0x000545F8
		public IAppender RemoveAppender(string name)
		{
			return this.RemoveAppender(this.GetAppender(name));
		}

		// Token: 0x06009018 RID: 36888 RVA: 0x0013B320 File Offset: 0x00139520
		public int AppendLoopOnAppenders(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.m_appenderList == null)
			{
				return 0;
			}
			if (this.m_appenderArray == null)
			{
				this.m_appenderArray = this.m_appenderList.ToArray();
			}
			foreach (IAppender appender in this.m_appenderArray)
			{
				try
				{
					appender.DoAppend(loggingEvent);
				}
				catch (Exception ex)
				{
					LogLog.Error(AppenderAttachedImpl.declaringType, "Failed to append to appender [" + appender.Name + "]", ex);
				}
			}
			return this.m_appenderList.Count;
		}

		// Token: 0x06009019 RID: 36889 RVA: 0x0013B3C0 File Offset: 0x001395C0
		public int AppendLoopOnAppenders(LoggingEvent[] loggingEvents)
		{
			if (loggingEvents == null)
			{
				throw new ArgumentNullException("loggingEvents");
			}
			if (loggingEvents.Length == 0)
			{
				throw new ArgumentException("loggingEvents array must not be empty", "loggingEvents");
			}
			if (loggingEvents.Length == 1)
			{
				return this.AppendLoopOnAppenders(loggingEvents[0]);
			}
			if (this.m_appenderList == null)
			{
				return 0;
			}
			if (this.m_appenderArray == null)
			{
				this.m_appenderArray = this.m_appenderList.ToArray();
			}
			foreach (IAppender appender in this.m_appenderArray)
			{
				try
				{
					AppenderAttachedImpl.CallAppend(appender, loggingEvents);
				}
				catch (Exception ex)
				{
					LogLog.Error(AppenderAttachedImpl.declaringType, "Failed to append to appender [" + appender.Name + "]", ex);
				}
			}
			return this.m_appenderList.Count;
		}

		// Token: 0x0600901A RID: 36890 RVA: 0x0013B484 File Offset: 0x00139684
		private static void CallAppend(IAppender appender, LoggingEvent[] loggingEvents)
		{
			IBulkAppender bulkAppender = appender as IBulkAppender;
			if (bulkAppender != null)
			{
				bulkAppender.DoAppend(loggingEvents);
				return;
			}
			foreach (LoggingEvent loggingEvent in loggingEvents)
			{
				appender.DoAppend(loggingEvent);
			}
		}

		// Token: 0x040060CB RID: 24779
		private static readonly Type declaringType = typeof(AppenderAttachedImpl);

		// Token: 0x040060CC RID: 24780
		private IAppender[] m_appenderArray;

		// Token: 0x040060CD RID: 24781
		private AppenderCollection m_appenderList;
	}
}
