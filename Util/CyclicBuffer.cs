using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029BB RID: 10683
	[NullableContext(1)]
	[Nullable(0)]
	public class CyclicBuffer
	{
		// Token: 0x06009031 RID: 36913 RVA: 0x0013B724 File Offset: 0x00139924
		public CyclicBuffer(int maxSize)
		{
			if (maxSize < 1)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("maxSize", maxSize, string.Format("Parameter: maxSize, Value: [{0}] out of range. Non zero positive integer required", maxSize));
			}
			this.m_maxSize = maxSize;
			this.m_events = new LoggingEvent[maxSize];
			this.m_first = 0;
			this.m_last = 0;
			this.m_numElems = 0;
		}

		// Token: 0x17001655 RID: 5717
		public LoggingEvent this[int i]
		{
			get
			{
				LoggingEvent loggingEvent;
				lock (this)
				{
					if (i < 0 || i >= this.m_numElems)
					{
						loggingEvent = null;
					}
					else
					{
						loggingEvent = this.m_events[(this.m_first + i) % this.m_maxSize];
					}
				}
				return loggingEvent;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06009033 RID: 36915 RVA: 0x0013B7DC File Offset: 0x001399DC
		public int MaxSize
		{
			get
			{
				int maxSize;
				lock (this)
				{
					maxSize = this.m_maxSize;
				}
				return maxSize;
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06009034 RID: 36916 RVA: 0x0013B814 File Offset: 0x00139A14
		public int Length
		{
			get
			{
				int numElems;
				lock (this)
				{
					numElems = this.m_numElems;
				}
				return numElems;
			}
		}

		// Token: 0x06009035 RID: 36917 RVA: 0x0013B84C File Offset: 0x00139A4C
		public LoggingEvent Append(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			LoggingEvent loggingEvent3;
			lock (this)
			{
				LoggingEvent loggingEvent2 = this.m_events[this.m_last];
				this.m_events[this.m_last] = loggingEvent;
				int num = this.m_last + 1;
				this.m_last = num;
				if (num == this.m_maxSize)
				{
					this.m_last = 0;
				}
				if (this.m_numElems < this.m_maxSize)
				{
					this.m_numElems++;
				}
				else
				{
					num = this.m_first + 1;
					this.m_first = num;
					if (num == this.m_maxSize)
					{
						this.m_first = 0;
					}
				}
				if (this.m_numElems < this.m_maxSize)
				{
					loggingEvent3 = null;
				}
				else
				{
					loggingEvent3 = loggingEvent2;
				}
			}
			return loggingEvent3;
		}

		// Token: 0x06009036 RID: 36918 RVA: 0x0013B918 File Offset: 0x00139B18
		public LoggingEvent PopOldest()
		{
			LoggingEvent loggingEvent2;
			lock (this)
			{
				LoggingEvent loggingEvent = null;
				if (this.m_numElems > 0)
				{
					this.m_numElems--;
					loggingEvent = this.m_events[this.m_first];
					this.m_events[this.m_first] = null;
					int num = this.m_first + 1;
					this.m_first = num;
					if (num == this.m_maxSize)
					{
						this.m_first = 0;
					}
				}
				loggingEvent2 = loggingEvent;
			}
			return loggingEvent2;
		}

		// Token: 0x06009037 RID: 36919 RVA: 0x0013B9A0 File Offset: 0x00139BA0
		public LoggingEvent[] PopAll()
		{
			LoggingEvent[] array2;
			lock (this)
			{
				LoggingEvent[] array = new LoggingEvent[this.m_numElems];
				if (this.m_numElems > 0)
				{
					if (this.m_first < this.m_last)
					{
						Array.Copy(this.m_events, this.m_first, array, 0, this.m_numElems);
					}
					else
					{
						Array.Copy(this.m_events, this.m_first, array, 0, this.m_maxSize - this.m_first);
						Array.Copy(this.m_events, 0, array, this.m_maxSize - this.m_first, this.m_last);
					}
				}
				this.Clear();
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06009038 RID: 36920 RVA: 0x0013BA58 File Offset: 0x00139C58
		public void Clear()
		{
			lock (this)
			{
				Array.Clear(this.m_events, 0, this.m_events.Length);
				this.m_first = 0;
				this.m_last = 0;
				this.m_numElems = 0;
			}
		}

		// Token: 0x040060D4 RID: 24788
		private readonly LoggingEvent[] m_events;

		// Token: 0x040060D5 RID: 24789
		private int m_first;

		// Token: 0x040060D6 RID: 24790
		private int m_last;

		// Token: 0x040060D7 RID: 24791
		private readonly int m_maxSize;

		// Token: 0x040060D8 RID: 24792
		private int m_numElems;
	}
}
