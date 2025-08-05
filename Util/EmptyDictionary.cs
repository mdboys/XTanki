using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029BD RID: 10685
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public sealed class EmptyDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06009041 RID: 36929 RVA: 0x00005698 File Offset: 0x00003898
		private EmptyDictionary()
		{
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06009042 RID: 36930 RVA: 0x000564D4 File Offset: 0x000546D4
		public static EmptyDictionary Instance { get; } = new EmptyDictionary();

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06009043 RID: 36931 RVA: 0x00005B7A File Offset: 0x00003D7A
		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06009044 RID: 36932 RVA: 0x00007F86 File Offset: 0x00006186
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06009045 RID: 36933 RVA: 0x0005192B File Offset: 0x0004FB2B
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06009046 RID: 36934 RVA: 0x00005B7A File Offset: 0x00003D7A
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06009047 RID: 36935 RVA: 0x00005B7A File Offset: 0x00003D7A
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06009048 RID: 36936 RVA: 0x000564DB File Offset: 0x000546DB
		public ICollection Keys
		{
			get
			{
				return EmptyCollection.Instance;
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06009049 RID: 36937 RVA: 0x000564DB File Offset: 0x000546DB
		public ICollection Values
		{
			get
			{
				return EmptyCollection.Instance;
			}
		}

		// Token: 0x17001664 RID: 5732
		public object this[object key]
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600904C RID: 36940 RVA: 0x0000568E File Offset: 0x0000388E
		public void CopyTo(Array array, int index)
		{
		}

		// Token: 0x0600904D RID: 36941 RVA: 0x000564C1 File Offset: 0x000546C1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return NullEnumerator.Instance;
		}

		// Token: 0x0600904E RID: 36942 RVA: 0x000564E5 File Offset: 0x000546E5
		public void Add(object key, object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600904F RID: 36943 RVA: 0x000564E5 File Offset: 0x000546E5
		public void Clear()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06009050 RID: 36944 RVA: 0x00007F86 File Offset: 0x00006186
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x06009051 RID: 36945 RVA: 0x000564EC File Offset: 0x000546EC
		public IDictionaryEnumerator GetEnumerator()
		{
			return NullDictionaryEnumerator.Instance;
		}

		// Token: 0x06009052 RID: 36946 RVA: 0x000564E5 File Offset: 0x000546E5
		public void Remove(object key)
		{
			throw new InvalidOperationException();
		}
	}
}
