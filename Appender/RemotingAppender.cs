using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02002A9B RID: 10907
	[NullableContext(1)]
	[Nullable(0)]
	public class RemotingAppender : BufferingAppenderSkeleton
	{
		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06009654 RID: 38484 RVA: 0x00059F70 File Offset: 0x00058170
		// (set) Token: 0x06009655 RID: 38485 RVA: 0x00059F78 File Offset: 0x00058178
		public string Sink { get; set; }

		// Token: 0x06009656 RID: 38486 RVA: 0x00147114 File Offset: 0x00145314
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			IDictionary dictionary = new Hashtable();
			dictionary["typeFilterLevel"] = "Full";
			this.m_sinkObj = (RemotingAppender.IRemoteLoggingSink)Activator.GetObject(typeof(RemotingAppender.IRemoteLoggingSink), this.Sink, dictionary);
		}

		// Token: 0x06009657 RID: 38487 RVA: 0x00147160 File Offset: 0x00145360
		protected override void SendBuffer(LoggingEvent[] events)
		{
			this.BeginAsyncSend();
			if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendBufferCallback), events))
			{
				this.EndAsyncSend();
				this.ErrorHandler.Error("RemotingAppender [" + base.Name + "] failed to ThreadPool.QueueUserWorkItem logging events in SendBuffer.");
			}
		}

		// Token: 0x06009658 RID: 38488 RVA: 0x00059F81 File Offset: 0x00058181
		protected override void OnClose()
		{
			base.OnClose();
			if (!this.m_workQueueEmptyEvent.WaitOne(30000, false))
			{
				this.ErrorHandler.Error("RemotingAppender [" + base.Name + "] failed to send all queued events before close, in OnClose.");
			}
		}

		// Token: 0x06009659 RID: 38489 RVA: 0x00059FBC File Offset: 0x000581BC
		private void BeginAsyncSend()
		{
			this.m_workQueueEmptyEvent.Reset();
			Interlocked.Increment(ref this.m_queuedCallbackCount);
		}

		// Token: 0x0600965A RID: 38490 RVA: 0x00059FD6 File Offset: 0x000581D6
		private void EndAsyncSend()
		{
			if (Interlocked.Decrement(ref this.m_queuedCallbackCount) <= 0)
			{
				this.m_workQueueEmptyEvent.Set();
			}
		}

		// Token: 0x0600965B RID: 38491 RVA: 0x001471B0 File Offset: 0x001453B0
		private void SendBufferCallback(object state)
		{
			try
			{
				LoggingEvent[] array = (LoggingEvent[])state;
				this.m_sinkObj.LogEvents(array);
			}
			catch (Exception ex)
			{
				this.ErrorHandler.Error("Failed in SendBufferCallback", ex);
			}
			finally
			{
				this.EndAsyncSend();
			}
		}

		// Token: 0x04006317 RID: 25367
		private int m_queuedCallbackCount;

		// Token: 0x04006318 RID: 25368
		private RemotingAppender.IRemoteLoggingSink m_sinkObj;

		// Token: 0x04006319 RID: 25369
		private readonly ManualResetEvent m_workQueueEmptyEvent = new ManualResetEvent(true);

		// Token: 0x02002A9C RID: 10908
		public interface IRemoteLoggingSink
		{
			// Token: 0x0600965D RID: 38493
			void LogEvents(LoggingEvent[] events);
		}
	}
}
