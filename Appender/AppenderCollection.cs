using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A7F RID: 10879
	[NullableContext(1)]
	[Nullable(0)]
	public class AppenderCollection : ICollection, IEnumerable, IList, ICloneable
	{
		// Token: 0x06009543 RID: 38211 RVA: 0x0005958C File Offset: 0x0005778C
		public AppenderCollection()
		{
			this.m_array = new IAppender[16];
		}

		// Token: 0x06009544 RID: 38212 RVA: 0x000595A1 File Offset: 0x000577A1
		public AppenderCollection(int capacity)
		{
			this.m_array = new IAppender[capacity];
		}

		// Token: 0x06009545 RID: 38213 RVA: 0x000595B5 File Offset: 0x000577B5
		public AppenderCollection(AppenderCollection c)
		{
			this.m_array = new IAppender[c.Count];
			this.AddRange(c);
		}

		// Token: 0x06009546 RID: 38214 RVA: 0x000595D6 File Offset: 0x000577D6
		public AppenderCollection(IAppender[] a)
		{
			this.m_array = new IAppender[a.Length];
			this.AddRange(a);
		}

		// Token: 0x06009547 RID: 38215 RVA: 0x000595F4 File Offset: 0x000577F4
		public AppenderCollection(ICollection col)
		{
			this.m_array = new IAppender[col.Count];
			this.AddRange(col);
		}

		// Token: 0x06009548 RID: 38216 RVA: 0x00059615 File Offset: 0x00057815
		protected internal AppenderCollection(AppenderCollection.Tag tag)
		{
			this.m_array = null;
		}

		// Token: 0x17001761 RID: 5985
		public virtual IAppender this[int index]
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

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x0600954B RID: 38219 RVA: 0x00059655 File Offset: 0x00057855
		// (set) Token: 0x0600954C RID: 38220 RVA: 0x001454A0 File Offset: 0x001436A0
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
						IAppender[] array = new IAppender[value];
						Array.Copy(this.m_array, 0, array, 0, this.m_count);
						this.m_array = array;
						return;
					}
					this.m_array = new IAppender[16];
				}
			}
		}

		// Token: 0x0600954D RID: 38221 RVA: 0x00145500 File Offset: 0x00143700
		public virtual object Clone()
		{
			AppenderCollection appenderCollection = new AppenderCollection(this.m_count);
			Array.Copy(this.m_array, 0, appenderCollection.m_array, 0, this.m_count);
			appenderCollection.m_count = this.m_count;
			appenderCollection.m_version = this.m_version;
			return appenderCollection;
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x0600954E RID: 38222 RVA: 0x0005965F File Offset: 0x0005785F
		public virtual int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x0600954F RID: 38223 RVA: 0x00059667 File Offset: 0x00057867
		public virtual bool IsSynchronized
		{
			get
			{
				return this.m_array.IsSynchronized;
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06009550 RID: 38224 RVA: 0x00059674 File Offset: 0x00057874
		public virtual object SyncRoot
		{
			get
			{
				return this.m_array.SyncRoot;
			}
		}

		// Token: 0x06009551 RID: 38225 RVA: 0x00059681 File Offset: 0x00057881
		void ICollection.CopyTo(Array array, int start)
		{
			if (this.m_count > 0)
			{
				Array.Copy(this.m_array, 0, array, start, this.m_count);
			}
		}

		// Token: 0x06009552 RID: 38226 RVA: 0x000596A0 File Offset: 0x000578A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)this.GetEnumerator();
		}

		// Token: 0x17001766 RID: 5990
		object IList.this[int i]
		{
			get
			{
				return this[i];
			}
			set
			{
				this[i] = (IAppender)value;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06009555 RID: 38229 RVA: 0x00007F86 File Offset: 0x00006186
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06009556 RID: 38230 RVA: 0x00007F86 File Offset: 0x00006186
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009557 RID: 38231 RVA: 0x000596C5 File Offset: 0x000578C5
		public virtual void Clear()
		{
			this.m_version++;
			this.m_array = new IAppender[16];
			this.m_count = 0;
		}

		// Token: 0x06009558 RID: 38232 RVA: 0x000596E9 File Offset: 0x000578E9
		int IList.Add(object x)
		{
			return this.Add((IAppender)x);
		}

		// Token: 0x06009559 RID: 38233 RVA: 0x000596F7 File Offset: 0x000578F7
		bool IList.Contains(object x)
		{
			return this.Contains((IAppender)x);
		}

		// Token: 0x0600955A RID: 38234 RVA: 0x00059705 File Offset: 0x00057905
		int IList.IndexOf(object x)
		{
			return this.IndexOf((IAppender)x);
		}

		// Token: 0x0600955B RID: 38235 RVA: 0x00059713 File Offset: 0x00057913
		void IList.Insert(int pos, object x)
		{
			this.Insert(pos, (IAppender)x);
		}

		// Token: 0x0600955C RID: 38236 RVA: 0x00059722 File Offset: 0x00057922
		void IList.Remove(object x)
		{
			this.Remove((IAppender)x);
		}

		// Token: 0x0600955D RID: 38237 RVA: 0x00059730 File Offset: 0x00057930
		void IList.RemoveAt(int pos)
		{
			this.RemoveAt(pos);
		}

		// Token: 0x0600955E RID: 38238 RVA: 0x00059739 File Offset: 0x00057939
		public static AppenderCollection ReadOnly(AppenderCollection list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new AppenderCollection.ReadOnlyAppenderCollection(list);
		}

		// Token: 0x0600955F RID: 38239 RVA: 0x0005974F File Offset: 0x0005794F
		public virtual void CopyTo(IAppender[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06009560 RID: 38240 RVA: 0x00059759 File Offset: 0x00057959
		public virtual void CopyTo(IAppender[] array, int start)
		{
			if (this.m_count > array.GetUpperBound(0) + 1 - start)
			{
				throw new ArgumentException("Destination array was not long enough.");
			}
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x06009561 RID: 38241 RVA: 0x0014554C File Offset: 0x0014374C
		public virtual int Add(IAppender item)
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

		// Token: 0x06009562 RID: 38242 RVA: 0x001455A4 File Offset: 0x001437A4
		public virtual bool Contains(IAppender item)
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

		// Token: 0x06009563 RID: 38243 RVA: 0x001455D8 File Offset: 0x001437D8
		public virtual int IndexOf(IAppender item)
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

		// Token: 0x06009564 RID: 38244 RVA: 0x0014560C File Offset: 0x0014380C
		public virtual void Insert(int index, IAppender item)
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

		// Token: 0x06009565 RID: 38245 RVA: 0x0014568C File Offset: 0x0014388C
		public virtual void Remove(IAppender item)
		{
			int num = this.IndexOf(item);
			if (num < 0)
			{
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");
			}
			this.m_version++;
			this.RemoveAt(num);
		}

		// Token: 0x06009566 RID: 38246 RVA: 0x001456C8 File Offset: 0x001438C8
		public virtual void RemoveAt(int index)
		{
			this.ValidateIndex(index);
			this.m_count--;
			if (index < this.m_count)
			{
				Array.Copy(this.m_array, index + 1, this.m_array, index, this.m_count - index);
			}
			Array.Copy(new IAppender[1], 0, this.m_array, this.m_count, 1);
			this.m_version++;
		}

		// Token: 0x06009567 RID: 38247 RVA: 0x0005978D File Offset: 0x0005798D
		public virtual AppenderCollection.IAppenderCollectionEnumerator GetEnumerator()
		{
			return new AppenderCollection.Enumerator(this);
		}

		// Token: 0x06009568 RID: 38248 RVA: 0x00145738 File Offset: 0x00143938
		public virtual int AddRange(AppenderCollection x)
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

		// Token: 0x06009569 RID: 38249 RVA: 0x001457B4 File Offset: 0x001439B4
		public virtual int AddRange(IAppender[] x)
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

		// Token: 0x0600956A RID: 38250 RVA: 0x00145820 File Offset: 0x00143A20
		public virtual int AddRange(ICollection col)
		{
			if (this.m_count + col.Count >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + col.Count);
			}
			foreach (object obj in col)
			{
				this.Add((IAppender)obj);
			}
			return this.m_count;
		}

		// Token: 0x0600956B RID: 38251 RVA: 0x00059795 File Offset: 0x00057995
		public virtual void TrimToSize()
		{
			this.Capacity = this.m_count;
		}

		// Token: 0x0600956C RID: 38252 RVA: 0x001458A8 File Offset: 0x00143AA8
		public virtual IAppender[] ToArray()
		{
			IAppender[] array = new IAppender[this.m_count];
			if (this.m_count > 0)
			{
				Array.Copy(this.m_array, 0, array, 0, this.m_count);
			}
			return array;
		}

		// Token: 0x0600956D RID: 38253 RVA: 0x000597A3 File Offset: 0x000579A3
		private void ValidateIndex(int i)
		{
			this.ValidateIndex(i, false);
		}

		// Token: 0x0600956E RID: 38254 RVA: 0x001458E0 File Offset: 0x00143AE0
		private void ValidateIndex(int i, bool allowEqualEnd)
		{
			int num = ((!allowEqualEnd) ? (this.m_count - 1) : this.m_count);
			if (i < 0 || i > num)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("i", i, string.Format("Index was out of range. Must be non-negative and less than the size of the collection. [{0}] Specified argument was out of the range of valid values.", i));
			}
		}

		// Token: 0x0600956F RID: 38255 RVA: 0x0014592C File Offset: 0x00143B2C
		private void EnsureCapacity(int min)
		{
			int num = ((this.m_array.Length != 0) ? (this.m_array.Length * 2) : 16);
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x0400628E RID: 25230
		private const int DEFAULT_CAPACITY = 16;

		// Token: 0x0400628F RID: 25231
		public static readonly AppenderCollection EmptyCollection = AppenderCollection.ReadOnly(new AppenderCollection(0));

		// Token: 0x04006290 RID: 25232
		private IAppender[] m_array;

		// Token: 0x04006291 RID: 25233
		private int m_count;

		// Token: 0x04006292 RID: 25234
		private int m_version;

		// Token: 0x02002A80 RID: 10880
		public interface IAppenderCollectionEnumerator
		{
			// Token: 0x17001769 RID: 5993
			// (get) Token: 0x06009571 RID: 38257
			IAppender Current { get; }

			// Token: 0x06009572 RID: 38258
			bool MoveNext();

			// Token: 0x06009573 RID: 38259
			void Reset();
		}

		// Token: 0x02002A81 RID: 10881
		[NullableContext(0)]
		protected internal enum Tag
		{
			// Token: 0x04006294 RID: 25236
			Default
		}

		// Token: 0x02002A82 RID: 10882
		[Nullable(0)]
		private sealed class Enumerator : IEnumerator, AppenderCollection.IAppenderCollectionEnumerator
		{
			// Token: 0x06009574 RID: 38260 RVA: 0x000597BF File Offset: 0x000579BF
			internal Enumerator(AppenderCollection tc)
			{
				this.m_collection = tc;
				this.m_index = -1;
				this.m_version = tc.m_version;
			}

			// Token: 0x1700176A RID: 5994
			// (get) Token: 0x06009575 RID: 38261 RVA: 0x000597E1 File Offset: 0x000579E1
			public IAppender Current
			{
				get
				{
					return this.m_collection[this.m_index];
				}
			}

			// Token: 0x1700176B RID: 5995
			// (get) Token: 0x06009576 RID: 38262 RVA: 0x000597F4 File Offset: 0x000579F4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06009577 RID: 38263 RVA: 0x00145960 File Offset: 0x00143B60
			public bool MoveNext()
			{
				if (this.m_version != this.m_collection.m_version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.m_index++;
				return this.m_index < this.m_collection.Count;
			}

			// Token: 0x06009578 RID: 38264 RVA: 0x000597FC File Offset: 0x000579FC
			public void Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x04006295 RID: 25237
			private readonly AppenderCollection m_collection;

			// Token: 0x04006296 RID: 25238
			private int m_index;

			// Token: 0x04006297 RID: 25239
			private readonly int m_version;
		}

		// Token: 0x02002A83 RID: 10883
		[Nullable(0)]
		private sealed class ReadOnlyAppenderCollection : AppenderCollection, ICollection, IEnumerable
		{
			// Token: 0x06009579 RID: 38265 RVA: 0x00059805 File Offset: 0x00057A05
			internal ReadOnlyAppenderCollection(AppenderCollection list)
				: base(AppenderCollection.Tag.Default)
			{
				this.m_collection = list;
			}

			// Token: 0x1700176C RID: 5996
			public override IAppender this[int i]
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

			// Token: 0x1700176D RID: 5997
			// (get) Token: 0x0600957C RID: 38268 RVA: 0x00005B7A File Offset: 0x00003D7A
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700176E RID: 5998
			// (get) Token: 0x0600957D RID: 38269 RVA: 0x00005B7A File Offset: 0x00003D7A
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700176F RID: 5999
			// (get) Token: 0x0600957E RID: 38270 RVA: 0x00059823 File Offset: 0x00057A23
			// (set) Token: 0x0600957F RID: 38271 RVA: 0x00057BE0 File Offset: 0x00055DE0
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

			// Token: 0x17001770 RID: 6000
			// (get) Token: 0x06009580 RID: 38272 RVA: 0x00059830 File Offset: 0x00057A30
			public override int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x17001771 RID: 6001
			// (get) Token: 0x06009581 RID: 38273 RVA: 0x0005983D File Offset: 0x00057A3D
			public override bool IsSynchronized
			{
				get
				{
					return this.m_collection.IsSynchronized;
				}
			}

			// Token: 0x17001772 RID: 6002
			// (get) Token: 0x06009582 RID: 38274 RVA: 0x0005984A File Offset: 0x00057A4A
			public override object SyncRoot
			{
				get
				{
					return this.m_collection.SyncRoot;
				}
			}

			// Token: 0x06009583 RID: 38275 RVA: 0x00059857 File Offset: 0x00057A57
			void ICollection.CopyTo(Array array, int start)
			{
				((ICollection)this.m_collection).CopyTo(array, start);
			}

			// Token: 0x06009584 RID: 38276 RVA: 0x00059866 File Offset: 0x00057A66
			public override void CopyTo(IAppender[] array)
			{
				this.m_collection.CopyTo(array);
			}

			// Token: 0x06009585 RID: 38277 RVA: 0x00059874 File Offset: 0x00057A74
			public override void CopyTo(IAppender[] array, int start)
			{
				this.m_collection.CopyTo(array, start);
			}

			// Token: 0x06009586 RID: 38278 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int Add(IAppender x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06009587 RID: 38279 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Clear()
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06009588 RID: 38280 RVA: 0x00059883 File Offset: 0x00057A83
			public override bool Contains(IAppender x)
			{
				return this.m_collection.Contains(x);
			}

			// Token: 0x06009589 RID: 38281 RVA: 0x00059891 File Offset: 0x00057A91
			public override int IndexOf(IAppender x)
			{
				return this.m_collection.IndexOf(x);
			}

			// Token: 0x0600958A RID: 38282 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Insert(int pos, IAppender x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600958B RID: 38283 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Remove(IAppender x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600958C RID: 38284 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void RemoveAt(int pos)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600958D RID: 38285 RVA: 0x0005989F File Offset: 0x00057A9F
			public override AppenderCollection.IAppenderCollectionEnumerator GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x0600958E RID: 38286 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int AddRange(AppenderCollection x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600958F RID: 38287 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int AddRange(IAppender[] x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06009590 RID: 38288 RVA: 0x000598AC File Offset: 0x00057AAC
			public override IAppender[] ToArray()
			{
				return this.m_collection.ToArray();
			}

			// Token: 0x06009591 RID: 38289 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void TrimToSize()
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x04006298 RID: 25240
			private readonly AppenderCollection m_collection;
		}
	}
}
