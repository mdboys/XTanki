using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029BA RID: 10682
	[NullableContext(1)]
	[Nullable(0)]
	public class CountingQuietTextWriter : QuietTextWriter
	{
		// Token: 0x0600902B RID: 36907 RVA: 0x00056497 File Offset: 0x00054697
		public CountingQuietTextWriter(TextWriter writer, IErrorHandler errorHandler)
			: base(writer, errorHandler)
		{
			this.Count = 0L;
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x0600902C RID: 36908 RVA: 0x000564A9 File Offset: 0x000546A9
		// (set) Token: 0x0600902D RID: 36909 RVA: 0x000564B1 File Offset: 0x000546B1
		public long Count { get; set; }

		// Token: 0x0600902E RID: 36910 RVA: 0x0013B5EC File Offset: 0x001397EC
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
				this.Count += (long)this.Encoding.GetByteCount(new char[] { value });
			}
			catch (Exception ex)
			{
				base.ErrorHandler.Error(string.Format("Failed to write [{0}].", value), ex, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x0600902F RID: 36911 RVA: 0x0013B658 File Offset: 0x00139858
		public override void Write(char[] buffer, int index, int count)
		{
			if (count > 0)
			{
				try
				{
					base.Write(buffer, index, count);
					this.Count += (long)this.Encoding.GetByteCount(buffer, index, count);
				}
				catch (Exception ex)
				{
					base.ErrorHandler.Error("Failed to write buffer.", ex, ErrorCode.WriteFailure);
				}
			}
		}

		// Token: 0x06009030 RID: 36912 RVA: 0x0013B6B8 File Offset: 0x001398B8
		public override void Write(string str)
		{
			if (str != null && str.Length > 0)
			{
				try
				{
					base.Write(str);
					this.Count += (long)this.Encoding.GetByteCount(str);
				}
				catch (Exception ex)
				{
					base.ErrorHandler.Error("Failed to write [" + str + "].", ex, ErrorCode.WriteFailure);
				}
			}
		}
	}
}
