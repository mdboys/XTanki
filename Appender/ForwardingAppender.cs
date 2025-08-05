using System;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A8F RID: 10895
	[NullableContext(1)]
	[Nullable(0)]
	public class ForwardingAppender : AppenderSkeleton, IAppenderAttachable
	{
		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x0600961E RID: 38430 RVA: 0x00146A54 File Offset: 0x00144C54
		public virtual AppenderCollection Appenders
		{
			get
			{
				AppenderCollection appenderCollection;
				lock (this)
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
				return appenderCollection;
			}
		}

		// Token: 0x0600961F RID: 38431 RVA: 0x00146AA0 File Offset: 0x00144CA0
		public virtual void AddAppender(IAppender newAppender)
		{
			if (newAppender == null)
			{
				throw new ArgumentNullException("newAppender");
			}
			lock (this)
			{
				if (this.m_appenderAttachedImpl == null)
				{
					this.m_appenderAttachedImpl = new AppenderAttachedImpl();
				}
				this.m_appenderAttachedImpl.AddAppender(newAppender);
			}
		}

		// Token: 0x06009620 RID: 38432 RVA: 0x00146AFC File Offset: 0x00144CFC
		public virtual IAppender GetAppender(string name)
		{
			IAppender appender;
			lock (this)
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
			return appender;
		}

		// Token: 0x06009621 RID: 38433 RVA: 0x00146B48 File Offset: 0x00144D48
		public virtual void RemoveAllAppenders()
		{
			lock (this)
			{
				if (this.m_appenderAttachedImpl != null)
				{
					this.m_appenderAttachedImpl.RemoveAllAppenders();
					this.m_appenderAttachedImpl = null;
				}
			}
		}

		// Token: 0x06009622 RID: 38434 RVA: 0x00146B90 File Offset: 0x00144D90
		public virtual IAppender RemoveAppender(IAppender appender)
		{
			lock (this)
			{
				if (appender != null && this.m_appenderAttachedImpl != null)
				{
					return this.m_appenderAttachedImpl.RemoveAppender(appender);
				}
			}
			return null;
		}

		// Token: 0x06009623 RID: 38435 RVA: 0x00146BDC File Offset: 0x00144DDC
		public virtual IAppender RemoveAppender(string name)
		{
			lock (this)
			{
				if (name != null && this.m_appenderAttachedImpl != null)
				{
					return this.m_appenderAttachedImpl.RemoveAppender(name);
				}
			}
			return null;
		}

		// Token: 0x06009624 RID: 38436 RVA: 0x00146C28 File Offset: 0x00144E28
		protected override void OnClose()
		{
			lock (this)
			{
				AppenderAttachedImpl appenderAttachedImpl = this.m_appenderAttachedImpl;
				if (appenderAttachedImpl != null)
				{
					appenderAttachedImpl.RemoveAllAppenders();
				}
			}
		}

		// Token: 0x06009625 RID: 38437 RVA: 0x00059DE5 File Offset: 0x00057FE5
		protected override void Append(LoggingEvent loggingEvent)
		{
			AppenderAttachedImpl appenderAttachedImpl = this.m_appenderAttachedImpl;
			if (appenderAttachedImpl == null)
			{
				return;
			}
			appenderAttachedImpl.AppendLoopOnAppenders(loggingEvent);
		}

		// Token: 0x06009626 RID: 38438 RVA: 0x00059DF9 File Offset: 0x00057FF9
		protected override void Append(LoggingEvent[] loggingEvents)
		{
			AppenderAttachedImpl appenderAttachedImpl = this.m_appenderAttachedImpl;
			if (appenderAttachedImpl == null)
			{
				return;
			}
			appenderAttachedImpl.AppendLoopOnAppenders(loggingEvents);
		}

		// Token: 0x040062C4 RID: 25284
		private AppenderAttachedImpl m_appenderAttachedImpl;
	}
}
