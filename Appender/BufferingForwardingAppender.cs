using System;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A86 RID: 10886
	[NullableContext(1)]
	[Nullable(0)]
	public class BufferingForwardingAppender : BufferingAppenderSkeleton, IAppenderAttachable
	{
		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x060095C2 RID: 38338 RVA: 0x001460E0 File Offset: 0x001442E0
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

		// Token: 0x060095C3 RID: 38339 RVA: 0x0014612C File Offset: 0x0014432C
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

		// Token: 0x060095C4 RID: 38340 RVA: 0x00146188 File Offset: 0x00144388
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

		// Token: 0x060095C5 RID: 38341 RVA: 0x001461D4 File Offset: 0x001443D4
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

		// Token: 0x060095C6 RID: 38342 RVA: 0x0014621C File Offset: 0x0014441C
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

		// Token: 0x060095C7 RID: 38343 RVA: 0x00146268 File Offset: 0x00144468
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

		// Token: 0x060095C8 RID: 38344 RVA: 0x001462B4 File Offset: 0x001444B4
		protected override void OnClose()
		{
			lock (this)
			{
				base.OnClose();
				AppenderAttachedImpl appenderAttachedImpl = this.m_appenderAttachedImpl;
				if (appenderAttachedImpl != null)
				{
					appenderAttachedImpl.RemoveAllAppenders();
				}
			}
		}

		// Token: 0x060095C9 RID: 38345 RVA: 0x00059A3E File Offset: 0x00057C3E
		protected override void SendBuffer(LoggingEvent[] events)
		{
			AppenderAttachedImpl appenderAttachedImpl = this.m_appenderAttachedImpl;
			if (appenderAttachedImpl == null)
			{
				return;
			}
			appenderAttachedImpl.AppendLoopOnAppenders(events);
		}

		// Token: 0x040062AD RID: 25261
		private AppenderAttachedImpl m_appenderAttachedImpl;
	}
}
