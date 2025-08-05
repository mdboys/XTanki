using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029D2 RID: 10706
	[NullableContext(1)]
	[Nullable(0)]
	public class QuietTextWriter : TextWriterAdapter
	{
		// Token: 0x06009103 RID: 37123 RVA: 0x00056D34 File Offset: 0x00054F34
		public QuietTextWriter(TextWriter writer, IErrorHandler errorHandler)
			: base(writer)
		{
			if (errorHandler == null)
			{
				throw new ArgumentNullException("errorHandler");
			}
			this.ErrorHandler = errorHandler;
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06009104 RID: 37124 RVA: 0x00056D52 File Offset: 0x00054F52
		// (set) Token: 0x06009105 RID: 37125 RVA: 0x00056D5A File Offset: 0x00054F5A
		public IErrorHandler ErrorHandler
		{
			get
			{
				return this.m_errorHandler;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_errorHandler = value;
			}
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06009106 RID: 37126 RVA: 0x00056D71 File Offset: 0x00054F71
		// (set) Token: 0x06009107 RID: 37127 RVA: 0x00056D79 File Offset: 0x00054F79
		public bool Closed { get; private set; }

		// Token: 0x06009108 RID: 37128 RVA: 0x0013CACC File Offset: 0x0013ACCC
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception ex)
			{
				this.m_errorHandler.Error(string.Format("Failed to write [{0}].", value), ex, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x06009109 RID: 37129 RVA: 0x0013CB14 File Offset: 0x0013AD14
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception ex)
			{
				this.m_errorHandler.Error("Failed to write buffer.", ex, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x0600910A RID: 37130 RVA: 0x0013CB54 File Offset: 0x0013AD54
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception ex)
			{
				this.m_errorHandler.Error("Failed to write [" + value + "].", ex, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x0600910B RID: 37131 RVA: 0x00056D82 File Offset: 0x00054F82
		public override void Close()
		{
			this.Closed = true;
			base.Close();
		}

		// Token: 0x04006118 RID: 24856
		private IErrorHandler m_errorHandler;
	}
}
