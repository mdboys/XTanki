using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace log4net.Util
{
	// Token: 0x020029CF RID: 10703
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public sealed class PropertiesDictionary : ReadOnlyPropertiesDictionary, ISerializable, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060090E5 RID: 37093 RVA: 0x00056BB6 File Offset: 0x00054DB6
		public PropertiesDictionary()
		{
		}

		// Token: 0x060090E6 RID: 37094 RVA: 0x00056BBE File Offset: 0x00054DBE
		public PropertiesDictionary(ReadOnlyPropertiesDictionary propertiesDictionary)
			: base(propertiesDictionary)
		{
		}

		// Token: 0x060090E7 RID: 37095 RVA: 0x00056BC7 File Offset: 0x00054DC7
		private PropertiesDictionary(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17001689 RID: 5769
		public override object this[string key]
		{
			get
			{
				return base.InnerHashtable[key];
			}
			set
			{
				base.InnerHashtable[key] = value;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x060090EA RID: 37098 RVA: 0x00007F86 File Offset: 0x00006186
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700168B RID: 5771
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is string))
				{
					throw new ArgumentException("key must be a string", "key");
				}
				return base.InnerHashtable[key];
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException("key must be a string", "key");
				}
				base.InnerHashtable[key] = value;
			}
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x060090ED RID: 37101 RVA: 0x00056C3B File Offset: 0x00054E3B
		ICollection IDictionary.Values
		{
			get
			{
				return base.InnerHashtable.Values;
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x060090EE RID: 37102 RVA: 0x00056C48 File Offset: 0x00054E48
		ICollection IDictionary.Keys
		{
			get
			{
				return base.InnerHashtable.Keys;
			}
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x060090EF RID: 37103 RVA: 0x00007F86 File Offset: 0x00006186
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x060090F0 RID: 37104 RVA: 0x00056C55 File Offset: 0x00054E55
		bool ICollection.IsSynchronized
		{
			get
			{
				return base.InnerHashtable.IsSynchronized;
			}
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x060090F1 RID: 37105 RVA: 0x00056C62 File Offset: 0x00054E62
		object ICollection.SyncRoot
		{
			get
			{
				return base.InnerHashtable.SyncRoot;
			}
		}

		// Token: 0x060090F2 RID: 37106 RVA: 0x00056C6F File Offset: 0x00054E6F
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return base.InnerHashtable.GetEnumerator();
		}

		// Token: 0x060090F3 RID: 37107 RVA: 0x00056C7C File Offset: 0x00054E7C
		void IDictionary.Remove(object key)
		{
			base.InnerHashtable.Remove(key);
		}

		// Token: 0x060090F4 RID: 37108 RVA: 0x00056C8A File Offset: 0x00054E8A
		bool IDictionary.Contains(object key)
		{
			return base.InnerHashtable.Contains(key);
		}

		// Token: 0x060090F5 RID: 37109 RVA: 0x00056C98 File Offset: 0x00054E98
		public override void Clear()
		{
			base.InnerHashtable.Clear();
		}

		// Token: 0x060090F6 RID: 37110 RVA: 0x00056CA5 File Offset: 0x00054EA5
		void IDictionary.Add(object key, object value)
		{
			if (!(key is string))
			{
				throw new ArgumentException("key must be a string", "key");
			}
			base.InnerHashtable.Add(key, value);
		}

		// Token: 0x060090F7 RID: 37111 RVA: 0x00056CCC File Offset: 0x00054ECC
		void ICollection.CopyTo(Array array, int index)
		{
			base.InnerHashtable.CopyTo(array, index);
		}

		// Token: 0x060090F8 RID: 37112 RVA: 0x00056CDB File Offset: 0x00054EDB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)base.InnerHashtable).GetEnumerator();
		}

		// Token: 0x060090F9 RID: 37113 RVA: 0x00056C7C File Offset: 0x00054E7C
		public void Remove(string key)
		{
			base.InnerHashtable.Remove(key);
		}
	}
}
