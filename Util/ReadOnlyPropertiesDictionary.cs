using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;

namespace log4net.Util
{
	// Token: 0x020029D4 RID: 10708
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class ReadOnlyPropertiesDictionary : ISerializable, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06009111 RID: 37137 RVA: 0x00056DDA File Offset: 0x00054FDA
		public ReadOnlyPropertiesDictionary()
		{
		}

		// Token: 0x06009112 RID: 37138 RVA: 0x0013CB9C File Offset: 0x0013AD9C
		public ReadOnlyPropertiesDictionary(ReadOnlyPropertiesDictionary propertiesDictionary)
		{
			foreach (object obj in ((IEnumerable)propertiesDictionary))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.InnerHashtable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}

		// Token: 0x06009113 RID: 37139 RVA: 0x0013CC14 File Offset: 0x0013AE14
		protected ReadOnlyPropertiesDictionary(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InnerHashtable[XmlConvert.DecodeName(serializationEntry.Name)] = serializationEntry.Value;
			}
		}

		// Token: 0x17001695 RID: 5781
		public virtual object this[string key]
		{
			get
			{
				return this.InnerHashtable[key];
			}
			set
			{
				throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
			}
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06009116 RID: 37142 RVA: 0x00056DF9 File Offset: 0x00054FF9
		protected Hashtable InnerHashtable { get; } = new Hashtable();

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06009117 RID: 37143 RVA: 0x00005B7A File Offset: 0x00003D7A
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001698 RID: 5784
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is string))
				{
					throw new ArgumentException("key must be a string");
				}
				return this.InnerHashtable[key];
			}
			set
			{
				throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x0600911A RID: 37146 RVA: 0x00056C3B File Offset: 0x00054E3B
		ICollection IDictionary.Values
		{
			get
			{
				return this.InnerHashtable.Values;
			}
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x0600911B RID: 37147 RVA: 0x00056C48 File Offset: 0x00054E48
		ICollection IDictionary.Keys
		{
			get
			{
				return this.InnerHashtable.Keys;
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x0600911C RID: 37148 RVA: 0x00056E22 File Offset: 0x00055022
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.InnerHashtable.IsFixedSize;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x0600911D RID: 37149 RVA: 0x00056C55 File Offset: 0x00054E55
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerHashtable.IsSynchronized;
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x0600911E RID: 37150 RVA: 0x00056C62 File Offset: 0x00054E62
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerHashtable.SyncRoot;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x0600911F RID: 37151 RVA: 0x00056E2F File Offset: 0x0005502F
		public int Count
		{
			get
			{
				return this.InnerHashtable.Count;
			}
		}

		// Token: 0x06009120 RID: 37152 RVA: 0x00056C6F File Offset: 0x00054E6F
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		// Token: 0x06009121 RID: 37153 RVA: 0x00056DED File Offset: 0x00054FED
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
		}

		// Token: 0x06009122 RID: 37154 RVA: 0x00056C8A File Offset: 0x00054E8A
		bool IDictionary.Contains(object key)
		{
			return this.InnerHashtable.Contains(key);
		}

		// Token: 0x06009123 RID: 37155 RVA: 0x00056DED File Offset: 0x00054FED
		public virtual void Clear()
		{
			throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
		}

		// Token: 0x06009124 RID: 37156 RVA: 0x00056DED File Offset: 0x00054FED
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
		}

		// Token: 0x06009125 RID: 37157 RVA: 0x00056CCC File Offset: 0x00054ECC
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerHashtable.CopyTo(array, index);
		}

		// Token: 0x06009126 RID: 37158 RVA: 0x00056CDB File Offset: 0x00054EDB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.InnerHashtable).GetEnumerator();
		}

		// Token: 0x06009127 RID: 37159 RVA: 0x0013CC68 File Offset: 0x0013AE68
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			foreach (object obj in this.InnerHashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key as string;
				object value = dictionaryEntry.Value;
				if (text != null && value != null && value.GetType().IsSerializable)
				{
					info.AddValue(XmlConvert.EncodeLocalName(text), value);
				}
			}
		}

		// Token: 0x06009128 RID: 37160 RVA: 0x0013CCF4 File Offset: 0x0013AEF4
		public string[] GetKeys()
		{
			string[] array = new string[this.InnerHashtable.Count];
			this.InnerHashtable.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06009129 RID: 37161 RVA: 0x00056C8A File Offset: 0x00054E8A
		public bool Contains(string key)
		{
			return this.InnerHashtable.Contains(key);
		}
	}
}
