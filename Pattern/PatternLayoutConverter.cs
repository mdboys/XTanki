using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02002A33 RID: 10803
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PatternLayoutConverter : PatternConverter
	{
		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06009360 RID: 37728 RVA: 0x000580FF File Offset: 0x000562FF
		// (set) Token: 0x06009361 RID: 37729 RVA: 0x00058107 File Offset: 0x00056307
		public virtual bool IgnoresException { get; set; } = true;

		// Token: 0x06009362 RID: 37730
		protected abstract void Convert(TextWriter writer, LoggingEvent loggingEvent);

		// Token: 0x06009363 RID: 37731 RVA: 0x00141F28 File Offset: 0x00140128
		protected override void Convert(TextWriter writer, object state)
		{
			LoggingEvent loggingEvent = state as LoggingEvent;
			if (loggingEvent != null)
			{
				this.Convert(writer, loggingEvent);
				return;
			}
			throw new ArgumentException("state must be of type [" + typeof(LoggingEvent).FullName + "]", "state");
		}
	}
}
