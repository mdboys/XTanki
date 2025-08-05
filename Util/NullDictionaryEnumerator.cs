using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029C6 RID: 10694
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NullDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06009090 RID: 37008 RVA: 0x00005698 File Offset: 0x00003898
		private NullDictionaryEnumerator()
		{
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06009091 RID: 37009 RVA: 0x000568A5 File Offset: 0x00054AA5
		public static NullDictionaryEnumerator Instance { get; } = new NullDictionaryEnumerator();

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06009092 RID: 37010 RVA: 0x000564E5 File Offset: 0x000546E5
		public object Current
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06009093 RID: 37011 RVA: 0x000564E5 File Offset: 0x000546E5
		public object Key
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06009094 RID: 37012 RVA: 0x000564E5 File Offset: 0x000546E5
		public object Value
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06009095 RID: 37013 RVA: 0x0013BD44 File Offset: 0x00139F44
		public DictionaryEntry Entry
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06009096 RID: 37014 RVA: 0x00007F86 File Offset: 0x00006186
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x06009097 RID: 37015 RVA: 0x0000568E File Offset: 0x0000388E
		public void Reset()
		{
		}
	}
}
