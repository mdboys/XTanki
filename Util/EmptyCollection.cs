using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029BC RID: 10684
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public sealed class EmptyCollection : ICollection, IEnumerable
	{
		// Token: 0x06009039 RID: 36921 RVA: 0x00005698 File Offset: 0x00003898
		private EmptyCollection()
		{
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x0600903A RID: 36922 RVA: 0x000564BA File Offset: 0x000546BA
		public static EmptyCollection Instance { get; } = new EmptyCollection();

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x0600903B RID: 36923 RVA: 0x00005B7A File Offset: 0x00003D7A
		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x0600903C RID: 36924 RVA: 0x00007F86 File Offset: 0x00006186
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x0600903D RID: 36925 RVA: 0x0005192B File Offset: 0x0004FB2B
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600903E RID: 36926 RVA: 0x0000568E File Offset: 0x0000388E
		public void CopyTo(Array array, int index)
		{
		}

		// Token: 0x0600903F RID: 36927 RVA: 0x000564C1 File Offset: 0x000546C1
		public IEnumerator GetEnumerator()
		{
			return NullEnumerator.Instance;
		}
	}
}
