using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Plugin
{
	// Token: 0x02002A0D RID: 10765
	[NullableContext(1)]
	[Nullable(0)]
	public class PluginCollection : ICollection, IEnumerable, IList, ICloneable
	{
		// Token: 0x0600928D RID: 37517 RVA: 0x0005793D File Offset: 0x00055B3D
		public PluginCollection()
		{
			this.m_array = new IPlugin[16];
		}

		// Token: 0x0600928E RID: 37518 RVA: 0x00057952 File Offset: 0x00055B52
		public PluginCollection(int capacity)
		{
			this.m_array = new IPlugin[capacity];
		}

		// Token: 0x0600928F RID: 37519 RVA: 0x00057966 File Offset: 0x00055B66
		public PluginCollection(PluginCollection c)
		{
			this.m_array = new IPlugin[c.Count];
			this.AddRange(c);
		}

		// Token: 0x06009290 RID: 37520 RVA: 0x00057987 File Offset: 0x00055B87
		public PluginCollection(IPlugin[] a)
		{
			this.m_array = new IPlugin[a.Length];
			this.AddRange(a);
		}

		// Token: 0x06009291 RID: 37521 RVA: 0x000579A5 File Offset: 0x00055BA5
		public PluginCollection(ICollection col)
		{
			this.m_array = new IPlugin[col.Count];
			this.AddRange(col);
		}

		// Token: 0x06009292 RID: 37522 RVA: 0x000579C6 File Offset: 0x00055BC6
		protected internal PluginCollection(PluginCollection.Tag tag)
		{
			this.m_array = null;
		}

		// Token: 0x170016D8 RID: 5848
		public virtual IPlugin this[int index]
		{
			get
			{
				this.ValidateIndex(index);
				return this.m_array[index];
			}
			set
			{
				this.ValidateIndex(index);
				this.m_version++;
				this.m_array[index] = value;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06009295 RID: 37525 RVA: 0x00057A06 File Offset: 0x00055C06
		// (set) Token: 0x06009296 RID: 37526 RVA: 0x0014038C File Offset: 0x0013E58C
		public virtual int Capacity
		{
			get
			{
				return this.m_array.Length;
			}
			set
			{
				if (value < this.m_count)
				{
					value = this.m_count;
				}
				if (value != this.m_array.Length)
				{
					if (value > 0)
					{
						IPlugin[] array = new IPlugin[value];
						Array.Copy(this.m_array, 0, array, 0, this.m_count);
						this.m_array = array;
						return;
					}
					this.m_array = new IPlugin[16];
				}
			}
		}

		// Token: 0x06009297 RID: 37527 RVA: 0x001403EC File Offset: 0x0013E5EC
		public virtual object Clone()
		{
			PluginCollection pluginCollection = new PluginCollection(this.m_count);
			Array.Copy(this.m_array, 0, pluginCollection.m_array, 0, this.m_count);
			pluginCollection.m_count = this.m_count;
			pluginCollection.m_version = this.m_version;
			return pluginCollection;
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06009298 RID: 37528 RVA: 0x00057A10 File Offset: 0x00055C10
		public virtual int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06009299 RID: 37529 RVA: 0x00057A18 File Offset: 0x00055C18
		public virtual bool IsSynchronized
		{
			get
			{
				return this.m_array.IsSynchronized;
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x0600929A RID: 37530 RVA: 0x00057A25 File Offset: 0x00055C25
		public virtual object SyncRoot
		{
			get
			{
				return this.m_array.SyncRoot;
			}
		}

		// Token: 0x0600929B RID: 37531 RVA: 0x00057A32 File Offset: 0x00055C32
		void ICollection.CopyTo(Array array, int start)
		{
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x0600929C RID: 37532 RVA: 0x00057A48 File Offset: 0x00055C48
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)this.GetEnumerator();
		}

		// Token: 0x170016DD RID: 5853
		object IList.this[int i]
		{
			get
			{
				return this[i];
			}
			set
			{
				this[i] = (IPlugin)value;
			}
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x0600929F RID: 37535 RVA: 0x00007F86 File Offset: 0x00006186
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x060092A0 RID: 37536 RVA: 0x00007F86 File Offset: 0x00006186
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060092A1 RID: 37537 RVA: 0x00057A6D File Offset: 0x00055C6D
		public virtual void Clear()
		{
			this.m_version++;
			this.m_array = new IPlugin[16];
			this.m_count = 0;
		}

		// Token: 0x060092A2 RID: 37538 RVA: 0x00057A91 File Offset: 0x00055C91
		int IList.Add(object x)
		{
			return this.Add((IPlugin)x);
		}

		// Token: 0x060092A3 RID: 37539 RVA: 0x00057A9F File Offset: 0x00055C9F
		bool IList.Contains(object x)
		{
			return this.Contains((IPlugin)x);
		}

		// Token: 0x060092A4 RID: 37540 RVA: 0x00057AAD File Offset: 0x00055CAD
		int IList.IndexOf(object x)
		{
			return this.IndexOf((IPlugin)x);
		}

		// Token: 0x060092A5 RID: 37541 RVA: 0x00057ABB File Offset: 0x00055CBB
		void IList.Insert(int pos, object x)
		{
			this.Insert(pos, (IPlugin)x);
		}

		// Token: 0x060092A6 RID: 37542 RVA: 0x00057ACA File Offset: 0x00055CCA
		void IList.Remove(object x)
		{
			this.Remove((IPlugin)x);
		}

		// Token: 0x060092A7 RID: 37543 RVA: 0x00057AD8 File Offset: 0x00055CD8
		void IList.RemoveAt(int pos)
		{
			this.RemoveAt(pos);
		}

		// Token: 0x060092A8 RID: 37544 RVA: 0x00057AE1 File Offset: 0x00055CE1
		public static PluginCollection ReadOnly(PluginCollection list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new PluginCollection.ReadOnlyPluginCollection(list);
		}

		// Token: 0x060092A9 RID: 37545 RVA: 0x00057AF7 File Offset: 0x00055CF7
		public virtual void CopyTo(IPlugin[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x060092AA RID: 37546 RVA: 0x00057B01 File Offset: 0x00055D01
		public virtual void CopyTo(IPlugin[] array, int start)
		{
			if (this.m_count > array.GetUpperBound(0) + 1 - start)
			{
				throw new ArgumentException("Destination array was not long enough.");
			}
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x060092AB RID: 37547 RVA: 0x00140438 File Offset: 0x0013E638
		public virtual int Add(IPlugin item)
		{
			if (this.m_count == this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + 1);
			}
			this.m_array[this.m_count] = item;
			this.m_version++;
			int count = this.m_count;
			this.m_count = count + 1;
			return count;
		}

		// Token: 0x060092AC RID: 37548 RVA: 0x00140490 File Offset: 0x0013E690
		public virtual bool Contains(IPlugin item)
		{
			for (int num = 0; num != this.m_count; num++)
			{
				if (this.m_array[num].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060092AD RID: 37549 RVA: 0x001404C4 File Offset: 0x0013E6C4
		public virtual int IndexOf(IPlugin item)
		{
			for (int num = 0; num != this.m_count; num++)
			{
				if (this.m_array[num].Equals(item))
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x060092AE RID: 37550 RVA: 0x001404F8 File Offset: 0x0013E6F8
		public virtual void Insert(int index, IPlugin item)
		{
			this.ValidateIndex(index, true);
			if (this.m_count == this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + 1);
			}
			if (index < this.m_count)
			{
				Array.Copy(this.m_array, index, this.m_array, index + 1, this.m_count - index);
			}
			this.m_array[index] = item;
			this.m_count++;
			this.m_version++;
		}

		// Token: 0x060092AF RID: 37551 RVA: 0x00140578 File Offset: 0x0013E778
		public virtual void Remove(IPlugin item)
		{
			int num = this.IndexOf(item);
			if (num < 0)
			{
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");
			}
			this.m_version++;
			this.RemoveAt(num);
		}

		// Token: 0x060092B0 RID: 37552 RVA: 0x001405B4 File Offset: 0x0013E7B4
		public virtual void RemoveAt(int index)
		{
			this.ValidateIndex(index);
			this.m_count--;
			if (index < this.m_count)
			{
				Array.Copy(this.m_array, index + 1, this.m_array, index, this.m_count - index);
			}
			Array.Copy(new IPlugin[1], 0, this.m_array, this.m_count, 1);
			this.m_version++;
		}

		// Token: 0x060092B1 RID: 37553 RVA: 0x00057B35 File Offset: 0x00055D35
		public virtual PluginCollection.IPluginCollectionEnumerator GetEnumerator()
		{
			return new PluginCollection.Enumerator(this);
		}

		// Token: 0x060092B2 RID: 37554 RVA: 0x00140624 File Offset: 0x0013E824
		public virtual int AddRange(PluginCollection x)
		{
			if (this.m_count + x.Count >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + x.Count);
			}
			Array.Copy(x.m_array, 0, this.m_array, this.m_count, x.Count);
			this.m_count += x.Count;
			this.m_version++;
			return this.m_count;
		}

		// Token: 0x060092B3 RID: 37555 RVA: 0x001406A0 File Offset: 0x0013E8A0
		public virtual int AddRange(IPlugin[] x)
		{
			if (this.m_count + x.Length >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + x.Length);
			}
			Array.Copy(x, 0, this.m_array, this.m_count, x.Length);
			this.m_count += x.Length;
			this.m_version++;
			return this.m_count;
		}

		// Token: 0x060092B4 RID: 37556 RVA: 0x0014070C File Offset: 0x0013E90C
		public virtual int AddRange(ICollection col)
		{
			if (this.m_count + col.Count >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + col.Count);
			}
			foreach (object obj in col)
			{
				this.Add((IPlugin)obj);
			}
			return this.m_count;
		}

		// Token: 0x060092B5 RID: 37557 RVA: 0x00057B3D File Offset: 0x00055D3D
		public virtual void TrimToSize()
		{
			this.Capacity = this.m_count;
		}

		// Token: 0x060092B6 RID: 37558 RVA: 0x00057B4B File Offset: 0x00055D4B
		private void ValidateIndex(int i)
		{
			this.ValidateIndex(i, false);
		}

		// Token: 0x060092B7 RID: 37559 RVA: 0x00140794 File Offset: 0x0013E994
		private void ValidateIndex(int i, bool allowEqualEnd)
		{
			int num = ((!allowEqualEnd) ? (this.m_count - 1) : this.m_count);
			if (i < 0 || i > num)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("i", i, string.Format("Index was out of range. Must be non-negative and less than the size of the collection. [{0}] Specified argument was out of the range of valid values.", i));
			}
		}

		// Token: 0x060092B8 RID: 37560 RVA: 0x001407E0 File Offset: 0x0013E9E0
		private void EnsureCapacity(int min)
		{
			int num = ((this.m_array.Length != 0) ? (this.m_array.Length * 2) : 16);
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x04006184 RID: 24964
		private const int DEFAULT_CAPACITY = 16;

		// Token: 0x04006185 RID: 24965
		private IPlugin[] m_array;

		// Token: 0x04006186 RID: 24966
		private int m_count;

		// Token: 0x04006187 RID: 24967
		private int m_version;

		// Token: 0x02002A0E RID: 10766
		public interface IPluginCollectionEnumerator
		{
			// Token: 0x170016E0 RID: 5856
			// (get) Token: 0x060092B9 RID: 37561
			IPlugin Current { get; }

			// Token: 0x060092BA RID: 37562
			bool MoveNext();

			// Token: 0x060092BB RID: 37563
			void Reset();
		}

		// Token: 0x02002A0F RID: 10767
		[NullableContext(0)]
		protected internal enum Tag
		{
			// Token: 0x04006189 RID: 24969
			Default
		}

		// Token: 0x02002A10 RID: 10768
		[Nullable(0)]
		private sealed class Enumerator : IEnumerator, PluginCollection.IPluginCollectionEnumerator
		{
			// Token: 0x060092BC RID: 37564 RVA: 0x00057B55 File Offset: 0x00055D55
			internal Enumerator(PluginCollection tc)
			{
				this.m_collection = tc;
				this.m_index = -1;
				this.m_version = tc.m_version;
			}

			// Token: 0x170016E1 RID: 5857
			// (get) Token: 0x060092BD RID: 37565 RVA: 0x00057B77 File Offset: 0x00055D77
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060092BE RID: 37566 RVA: 0x00140814 File Offset: 0x0013EA14
			public bool MoveNext()
			{
				if (this.m_version != this.m_collection.m_version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.m_index++;
				return this.m_index < this.m_collection.Count;
			}

			// Token: 0x060092BF RID: 37567 RVA: 0x00057B7F File Offset: 0x00055D7F
			public void Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x170016E2 RID: 5858
			// (get) Token: 0x060092C0 RID: 37568 RVA: 0x00057B88 File Offset: 0x00055D88
			public IPlugin Current
			{
				get
				{
					return this.m_collection[this.m_index];
				}
			}

			// Token: 0x0400618A RID: 24970
			private readonly PluginCollection m_collection;

			// Token: 0x0400618B RID: 24971
			private int m_index;

			// Token: 0x0400618C RID: 24972
			private readonly int m_version;
		}

		// Token: 0x02002A11 RID: 10769
		[Nullable(0)]
		private sealed class ReadOnlyPluginCollection : PluginCollection
		{
			// Token: 0x060092C1 RID: 37569 RVA: 0x00057B9B File Offset: 0x00055D9B
			internal ReadOnlyPluginCollection(PluginCollection list)
				: base(PluginCollection.Tag.Default)
			{
				this.m_collection = list;
			}

			// Token: 0x170016E3 RID: 5859
			// (get) Token: 0x060092C2 RID: 37570 RVA: 0x00057BAB File Offset: 0x00055DAB
			public override int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x170016E4 RID: 5860
			// (get) Token: 0x060092C3 RID: 37571 RVA: 0x00057BB8 File Offset: 0x00055DB8
			public override bool IsSynchronized
			{
				get
				{
					return this.m_collection.IsSynchronized;
				}
			}

			// Token: 0x170016E5 RID: 5861
			// (get) Token: 0x060092C4 RID: 37572 RVA: 0x00057BC5 File Offset: 0x00055DC5
			public override object SyncRoot
			{
				get
				{
					return this.m_collection.SyncRoot;
				}
			}

			// Token: 0x170016E6 RID: 5862
			public override IPlugin this[int i]
			{
				get
				{
					return this.m_collection[i];
				}
				set
				{
					throw new NotSupportedException("This is a Read Only Collection and can not be modified");
				}
			}

			// Token: 0x170016E7 RID: 5863
			// (get) Token: 0x060092C7 RID: 37575 RVA: 0x00005B7A File Offset: 0x00003D7A
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170016E8 RID: 5864
			// (get) Token: 0x060092C8 RID: 37576 RVA: 0x00005B7A File Offset: 0x00003D7A
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170016E9 RID: 5865
			// (get) Token: 0x060092C9 RID: 37577 RVA: 0x00057BEC File Offset: 0x00055DEC
			// (set) Token: 0x060092CA RID: 37578 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int Capacity
			{
				get
				{
					return this.m_collection.Capacity;
				}
				set
				{
					throw new NotSupportedException("This is a Read Only Collection and can not be modified");
				}
			}

			// Token: 0x060092CB RID: 37579 RVA: 0x00057BF9 File Offset: 0x00055DF9
			public override void CopyTo(IPlugin[] array)
			{
				this.m_collection.CopyTo(array);
			}

			// Token: 0x060092CC RID: 37580 RVA: 0x00057C07 File Offset: 0x00055E07
			public override void CopyTo(IPlugin[] array, int start)
			{
				this.m_collection.CopyTo(array, start);
			}

			// Token: 0x060092CD RID: 37581 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int Add(IPlugin x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060092CE RID: 37582 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Clear()
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060092CF RID: 37583 RVA: 0x00057C16 File Offset: 0x00055E16
			public override bool Contains(IPlugin x)
			{
				return this.m_collection.Contains(x);
			}

			// Token: 0x060092D0 RID: 37584 RVA: 0x00057C24 File Offset: 0x00055E24
			public override int IndexOf(IPlugin x)
			{
				return this.m_collection.IndexOf(x);
			}

			// Token: 0x060092D1 RID: 37585 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Insert(int pos, IPlugin x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060092D2 RID: 37586 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Remove(IPlugin x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060092D3 RID: 37587 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void RemoveAt(int pos)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060092D4 RID: 37588 RVA: 0x00057C32 File Offset: 0x00055E32
			public override PluginCollection.IPluginCollectionEnumerator GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x060092D5 RID: 37589 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int AddRange(PluginCollection x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060092D6 RID: 37590 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int AddRange(IPlugin[] x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0400618D RID: 24973
			private readonly PluginCollection m_collection;
		}
	}
}
