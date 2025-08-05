using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace log4net.Util
{
	// Token: 0x020029D3 RID: 10707
	public sealed class ReaderWriterLock
	{
		// Token: 0x0600910C RID: 37132 RVA: 0x00056D91 File Offset: 0x00054F91
		public ReaderWriterLock()
		{
			this.m_lock = new ReaderWriterLock();
		}

		// Token: 0x0600910D RID: 37133 RVA: 0x00056DA4 File Offset: 0x00054FA4
		public void AcquireReaderLock()
		{
			this.m_lock.AcquireReaderLock(-1);
		}

		// Token: 0x0600910E RID: 37134 RVA: 0x00056DB2 File Offset: 0x00054FB2
		public void ReleaseReaderLock()
		{
			this.m_lock.ReleaseReaderLock();
		}

		// Token: 0x0600910F RID: 37135 RVA: 0x00056DBF File Offset: 0x00054FBF
		public void AcquireWriterLock()
		{
			this.m_lock.AcquireWriterLock(-1);
		}

		// Token: 0x06009110 RID: 37136 RVA: 0x00056DCD File Offset: 0x00054FCD
		public void ReleaseWriterLock()
		{
			this.m_lock.ReleaseWriterLock();
		}

		// Token: 0x0400611A RID: 24858
		[Nullable(1)]
		private readonly ReaderWriterLock m_lock;
	}
}
