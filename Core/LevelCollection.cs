using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A59 RID: 10841
	[NullableContext(1)]
	[Nullable(0)]
	public class LevelCollection : ICollection, IEnumerable, IList, ICloneable
	{
		// Token: 0x060093F2 RID: 37874 RVA: 0x0005852E File Offset: 0x0005672E
		public LevelCollection()
		{
			this.m_array = new Level[16];
		}

		// Token: 0x060093F3 RID: 37875 RVA: 0x00058543 File Offset: 0x00056743
		public LevelCollection(int capacity)
		{
			this.m_array = new Level[capacity];
		}

		// Token: 0x060093F4 RID: 37876 RVA: 0x00058557 File Offset: 0x00056757
		public LevelCollection(LevelCollection c)
		{
			this.m_array = new Level[c.Count];
			this.AddRange(c);
		}

		// Token: 0x060093F5 RID: 37877 RVA: 0x00058578 File Offset: 0x00056778
		public LevelCollection(Level[] a)
		{
			this.m_array = new Level[a.Length];
			this.AddRange(a);
		}

		// Token: 0x060093F6 RID: 37878 RVA: 0x00058596 File Offset: 0x00056796
		public LevelCollection(ICollection col)
		{
			this.m_array = new Level[col.Count];
			this.AddRange(col);
		}

		// Token: 0x060093F7 RID: 37879 RVA: 0x000585B7 File Offset: 0x000567B7
		protected internal LevelCollection(LevelCollection.Tag tag)
		{
			this.m_array = null;
		}

		// Token: 0x17001719 RID: 5913
		public virtual Level this[int index]
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

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x060093FA RID: 37882 RVA: 0x000585F7 File Offset: 0x000567F7
		// (set) Token: 0x060093FB RID: 37883 RVA: 0x00142BF0 File Offset: 0x00140DF0
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
						Level[] array = new Level[value];
						Array.Copy(this.m_array, 0, array, 0, this.m_count);
						this.m_array = array;
						return;
					}
					this.m_array = new Level[16];
				}
			}
		}

		// Token: 0x060093FC RID: 37884 RVA: 0x00142C50 File Offset: 0x00140E50
		public virtual object Clone()
		{
			LevelCollection levelCollection = new LevelCollection(this.m_count);
			Array.Copy(this.m_array, 0, levelCollection.m_array, 0, this.m_count);
			levelCollection.m_count = this.m_count;
			levelCollection.m_version = this.m_version;
			return levelCollection;
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x060093FD RID: 37885 RVA: 0x00058601 File Offset: 0x00056801
		public virtual int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x060093FE RID: 37886 RVA: 0x00058609 File Offset: 0x00056809
		public virtual bool IsSynchronized
		{
			get
			{
				return this.m_array.IsSynchronized;
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x060093FF RID: 37887 RVA: 0x00058616 File Offset: 0x00056816
		public virtual object SyncRoot
		{
			get
			{
				return this.m_array.SyncRoot;
			}
		}

		// Token: 0x06009400 RID: 37888 RVA: 0x00058623 File Offset: 0x00056823
		void ICollection.CopyTo(Array array, int start)
		{
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x06009401 RID: 37889 RVA: 0x00058639 File Offset: 0x00056839
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)this.GetEnumerator();
		}

		// Token: 0x1700171E RID: 5918
		object IList.this[int i]
		{
			get
			{
				return this[i];
			}
			set
			{
				this[i] = (Level)value;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06009404 RID: 37892 RVA: 0x00007F86 File Offset: 0x00006186
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06009405 RID: 37893 RVA: 0x00007F86 File Offset: 0x00006186
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009406 RID: 37894 RVA: 0x0005865E File Offset: 0x0005685E
		public virtual void Clear()
		{
			this.m_version++;
			this.m_array = new Level[16];
			this.m_count = 0;
		}

		// Token: 0x06009407 RID: 37895 RVA: 0x00058682 File Offset: 0x00056882
		int IList.Add(object x)
		{
			return this.Add((Level)x);
		}

		// Token: 0x06009408 RID: 37896 RVA: 0x00058690 File Offset: 0x00056890
		bool IList.Contains(object x)
		{
			return this.Contains((Level)x);
		}

		// Token: 0x06009409 RID: 37897 RVA: 0x0005869E File Offset: 0x0005689E
		int IList.IndexOf(object x)
		{
			return this.IndexOf((Level)x);
		}

		// Token: 0x0600940A RID: 37898 RVA: 0x000586AC File Offset: 0x000568AC
		void IList.Insert(int pos, object x)
		{
			this.Insert(pos, (Level)x);
		}

		// Token: 0x0600940B RID: 37899 RVA: 0x000586BB File Offset: 0x000568BB
		void IList.Remove(object x)
		{
			this.Remove((Level)x);
		}

		// Token: 0x0600940C RID: 37900 RVA: 0x000586C9 File Offset: 0x000568C9
		void IList.RemoveAt(int pos)
		{
			this.RemoveAt(pos);
		}

		// Token: 0x0600940D RID: 37901 RVA: 0x000586D2 File Offset: 0x000568D2
		public static LevelCollection ReadOnly(LevelCollection list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new LevelCollection.ReadOnlyLevelCollection(list);
		}

		// Token: 0x0600940E RID: 37902 RVA: 0x000586E8 File Offset: 0x000568E8
		public virtual void CopyTo(Level[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x0600940F RID: 37903 RVA: 0x000586F2 File Offset: 0x000568F2
		public virtual void CopyTo(Level[] array, int start)
		{
			if (this.m_count > array.GetUpperBound(0) + 1 - start)
			{
				throw new ArgumentException("Destination array was not long enough.");
			}
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x06009410 RID: 37904 RVA: 0x00142C9C File Offset: 0x00140E9C
		public virtual int Add(Level item)
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

		// Token: 0x06009411 RID: 37905 RVA: 0x00142CF4 File Offset: 0x00140EF4
		public virtual bool Contains(Level item)
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

		// Token: 0x06009412 RID: 37906 RVA: 0x00142D28 File Offset: 0x00140F28
		public virtual int IndexOf(Level item)
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

		// Token: 0x06009413 RID: 37907 RVA: 0x00142D5C File Offset: 0x00140F5C
		public virtual void Insert(int index, Level item)
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

		// Token: 0x06009414 RID: 37908 RVA: 0x00142DDC File Offset: 0x00140FDC
		public virtual void Remove(Level item)
		{
			int num = this.IndexOf(item);
			if (num < 0)
			{
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");
			}
			this.m_version++;
			this.RemoveAt(num);
		}

		// Token: 0x06009415 RID: 37909 RVA: 0x00142E18 File Offset: 0x00141018
		public virtual void RemoveAt(int index)
		{
			this.ValidateIndex(index);
			this.m_count--;
			if (index < this.m_count)
			{
				Array.Copy(this.m_array, index + 1, this.m_array, index, this.m_count - index);
			}
			Array.Copy(new Level[1], 0, this.m_array, this.m_count, 1);
			this.m_version++;
		}

		// Token: 0x06009416 RID: 37910 RVA: 0x00058726 File Offset: 0x00056926
		public virtual LevelCollection.ILevelCollectionEnumerator GetEnumerator()
		{
			return new LevelCollection.Enumerator(this);
		}

		// Token: 0x06009417 RID: 37911 RVA: 0x00142E88 File Offset: 0x00141088
		public virtual int AddRange(LevelCollection x)
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

		// Token: 0x06009418 RID: 37912 RVA: 0x00142F04 File Offset: 0x00141104
		public virtual int AddRange(Level[] x)
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

		// Token: 0x06009419 RID: 37913 RVA: 0x00142F70 File Offset: 0x00141170
		public virtual int AddRange(ICollection col)
		{
			if (this.m_count + col.Count >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + col.Count);
			}
			foreach (object obj in col)
			{
				this.Add((Level)obj);
			}
			return this.m_count;
		}

		// Token: 0x0600941A RID: 37914 RVA: 0x0005872E File Offset: 0x0005692E
		public virtual void TrimToSize()
		{
			this.Capacity = this.m_count;
		}

		// Token: 0x0600941B RID: 37915 RVA: 0x0005873C File Offset: 0x0005693C
		private void ValidateIndex(int i)
		{
			this.ValidateIndex(i, false);
		}

		// Token: 0x0600941C RID: 37916 RVA: 0x00142FF8 File Offset: 0x001411F8
		private void ValidateIndex(int i, bool allowEqualEnd)
		{
			int num = ((!allowEqualEnd) ? (this.m_count - 1) : this.m_count);
			if (i < 0 || i > num)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("i", i, string.Format("Index was out of range. Must be non-negative and less than the size of the collection. [{0}] Specified argument was out of the range of valid values.", i));
			}
		}

		// Token: 0x0600941D RID: 37917 RVA: 0x00143044 File Offset: 0x00141244
		private void EnsureCapacity(int min)
		{
			int num = ((this.m_array.Length != 0) ? (this.m_array.Length * 2) : 16);
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x04006219 RID: 25113
		private const int DEFAULT_CAPACITY = 16;

		// Token: 0x0400621A RID: 25114
		private Level[] m_array;

		// Token: 0x0400621B RID: 25115
		private int m_count;

		// Token: 0x0400621C RID: 25116
		private int m_version;

		// Token: 0x02002A5A RID: 10842
		public interface ILevelCollectionEnumerator
		{
			// Token: 0x17001721 RID: 5921
			// (get) Token: 0x0600941E RID: 37918
			Level Current { get; }

			// Token: 0x0600941F RID: 37919
			bool MoveNext();

			// Token: 0x06009420 RID: 37920
			void Reset();
		}

		// Token: 0x02002A5B RID: 10843
		[NullableContext(0)]
		protected internal enum Tag
		{
			// Token: 0x0400621E RID: 25118
			Default
		}

		// Token: 0x02002A5C RID: 10844
		[Nullable(0)]
		private sealed class Enumerator : IEnumerator, LevelCollection.ILevelCollectionEnumerator
		{
			// Token: 0x06009421 RID: 37921 RVA: 0x00058746 File Offset: 0x00056946
			internal Enumerator(LevelCollection tc)
			{
				this.m_collection = tc;
				this.m_index = -1;
				this.m_version = tc.m_version;
			}

			// Token: 0x17001722 RID: 5922
			// (get) Token: 0x06009422 RID: 37922 RVA: 0x00058768 File Offset: 0x00056968
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06009423 RID: 37923 RVA: 0x00143078 File Offset: 0x00141278
			public bool MoveNext()
			{
				if (this.m_version != this.m_collection.m_version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.m_index++;
				return this.m_index < this.m_collection.Count;
			}

			// Token: 0x06009424 RID: 37924 RVA: 0x00058770 File Offset: 0x00056970
			public void Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x17001723 RID: 5923
			// (get) Token: 0x06009425 RID: 37925 RVA: 0x00058779 File Offset: 0x00056979
			public Level Current
			{
				get
				{
					return this.m_collection[this.m_index];
				}
			}

			// Token: 0x0400621F RID: 25119
			private readonly LevelCollection m_collection;

			// Token: 0x04006220 RID: 25120
			private int m_index;

			// Token: 0x04006221 RID: 25121
			private readonly int m_version;
		}

		// Token: 0x02002A5D RID: 10845
		[Nullable(0)]
		private sealed class ReadOnlyLevelCollection : LevelCollection
		{
			// Token: 0x06009426 RID: 37926 RVA: 0x0005878C File Offset: 0x0005698C
			internal ReadOnlyLevelCollection(LevelCollection list)
				: base(LevelCollection.Tag.Default)
			{
				this.m_collection = list;
			}

			// Token: 0x17001724 RID: 5924
			// (get) Token: 0x06009427 RID: 37927 RVA: 0x0005879C File Offset: 0x0005699C
			public override int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x17001725 RID: 5925
			// (get) Token: 0x06009428 RID: 37928 RVA: 0x000587A9 File Offset: 0x000569A9
			public override bool IsSynchronized
			{
				get
				{
					return this.m_collection.IsSynchronized;
				}
			}

			// Token: 0x17001726 RID: 5926
			// (get) Token: 0x06009429 RID: 37929 RVA: 0x000587B6 File Offset: 0x000569B6
			public override object SyncRoot
			{
				get
				{
					return this.m_collection.SyncRoot;
				}
			}

			// Token: 0x17001727 RID: 5927
			public override Level this[int i]
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

			// Token: 0x17001728 RID: 5928
			// (get) Token: 0x0600942C RID: 37932 RVA: 0x00005B7A File Offset: 0x00003D7A
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001729 RID: 5929
			// (get) Token: 0x0600942D RID: 37933 RVA: 0x00005B7A File Offset: 0x00003D7A
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700172A RID: 5930
			// (get) Token: 0x0600942E RID: 37934 RVA: 0x000587D1 File Offset: 0x000569D1
			// (set) Token: 0x0600942F RID: 37935 RVA: 0x00057BE0 File Offset: 0x00055DE0
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

			// Token: 0x06009430 RID: 37936 RVA: 0x000587DE File Offset: 0x000569DE
			public override void CopyTo(Level[] array)
			{
				this.m_collection.CopyTo(array);
			}

			// Token: 0x06009431 RID: 37937 RVA: 0x000587EC File Offset: 0x000569EC
			public override void CopyTo(Level[] array, int start)
			{
				this.m_collection.CopyTo(array, start);
			}

			// Token: 0x06009432 RID: 37938 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int Add(Level x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06009433 RID: 37939 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Clear()
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06009434 RID: 37940 RVA: 0x000587FB File Offset: 0x000569FB
			public override bool Contains(Level x)
			{
				return this.m_collection.Contains(x);
			}

			// Token: 0x06009435 RID: 37941 RVA: 0x00058809 File Offset: 0x00056A09
			public override int IndexOf(Level x)
			{
				return this.m_collection.IndexOf(x);
			}

			// Token: 0x06009436 RID: 37942 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Insert(int pos, Level x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06009437 RID: 37943 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void Remove(Level x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06009438 RID: 37944 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override void RemoveAt(int pos)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06009439 RID: 37945 RVA: 0x00058817 File Offset: 0x00056A17
			public override LevelCollection.ILevelCollectionEnumerator GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x0600943A RID: 37946 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int AddRange(LevelCollection x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600943B RID: 37947 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public override int AddRange(Level[] x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x04006222 RID: 25122
			private readonly LevelCollection m_collection;
		}
	}
}
