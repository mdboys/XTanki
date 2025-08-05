using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LeopotamGroup.Collections
{
	// Token: 0x02002AE4 RID: 10980
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class FastList<[Nullable(2)] T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600974A RID: 38730 RVA: 0x0005A618 File Offset: 0x00058818
		public FastList()
			: this(null)
		{
		}

		// Token: 0x0600974B RID: 38731 RVA: 0x0005A621 File Offset: 0x00058821
		public FastList(EqualityComparer<T> comparer)
			: this(8, comparer)
		{
		}

		// Token: 0x0600974C RID: 38732 RVA: 0x00148790 File Offset: 0x00146990
		public FastList(int capacity, EqualityComparer<T> comparer = null)
		{
			Type typeFromHandle = typeof(T);
			this._isNullable = !typeFromHandle.IsValueType || Nullable.GetUnderlyingType(typeFromHandle) != null;
			this.Capacity = ((capacity <= 8) ? 8 : capacity);
			this.Count = 0;
			this._comparer = comparer;
			this._items = new T[this.Capacity];
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x0600974D RID: 38733 RVA: 0x0005A62B File Offset: 0x0005882B
		// (set) Token: 0x0600974E RID: 38734 RVA: 0x0005A633 File Offset: 0x00058833
		public int Capacity { get; private set; }

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x0600974F RID: 38735 RVA: 0x0005A63C File Offset: 0x0005883C
		// (set) Token: 0x06009750 RID: 38736 RVA: 0x0005A644 File Offset: 0x00058844
		public int Count { get; private set; }

		// Token: 0x170017D4 RID: 6100
		public T this[int index]
		{
			get
			{
				if (index >= this.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._items[index];
			}
			set
			{
				if (index >= this.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				this._items[index] = value;
			}
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x06009753 RID: 38739 RVA: 0x00007F86 File Offset: 0x00006186
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009754 RID: 38740 RVA: 0x001487F8 File Offset: 0x001469F8
		public void Add(T item)
		{
			if (this.Count == this.Capacity)
			{
				if (this.Capacity > 0)
				{
					this.Capacity <<= 1;
				}
				else
				{
					this.Capacity = 8;
				}
				T[] array = new T[this.Capacity];
				Array.Copy(this._items, array, this.Count);
				this._items = array;
			}
			this._items[this.Count] = item;
			int count = this.Count;
			this.Count = count + 1;
		}

		// Token: 0x06009755 RID: 38741 RVA: 0x0005A688 File Offset: 0x00058888
		public void Clear()
		{
			this.Clear(false);
		}

		// Token: 0x06009756 RID: 38742 RVA: 0x0005A691 File Offset: 0x00058891
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06009757 RID: 38743 RVA: 0x0005A6A0 File Offset: 0x000588A0
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this.Count);
		}

		// Token: 0x06009758 RID: 38744 RVA: 0x0014887C File Offset: 0x00146A7C
		public int IndexOf(T item)
		{
			int i;
			if (this._useObjectCastComparer && this._isNullable)
			{
				for (i = this.Count - 1; i >= 0; i--)
				{
					if (this._items[i] == item)
					{
						break;
					}
				}
			}
			else if (this._comparer != null)
			{
				for (i = this.Count - 1; i >= 0; i--)
				{
					if (this._comparer.Equals(this._items[i], item))
					{
						break;
					}
				}
			}
			else
			{
				i = Array.IndexOf<T>(this._items, item, 0, this.Count);
			}
			return i;
		}

		// Token: 0x06009759 RID: 38745 RVA: 0x00148914 File Offset: 0x00146B14
		public void Insert(int index, T item)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.Reserve(1, false, false);
			Array.Copy(this._items, index, this._items, index + 1, this.Count - index);
			this._items[index] = item;
			int count = this.Count;
			this.Count = count + 1;
		}

		// Token: 0x0600975A RID: 38746 RVA: 0x00005B56 File Offset: 0x00003D56
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600975B RID: 38747 RVA: 0x00005B56 File Offset: 0x00003D56
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600975C RID: 38748 RVA: 0x00148978 File Offset: 0x00146B78
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num == -1)
			{
				return false;
			}
			this.RemoveAt(num);
			return true;
		}

		// Token: 0x0600975D RID: 38749 RVA: 0x0014899C File Offset: 0x00146B9C
		public void RemoveAt(int id)
		{
			if (id >= 0 && id < this.Count)
			{
				int count = this.Count;
				this.Count = count - 1;
				Array.Copy(this._items, id + 1, this._items, id, this.Count - id);
			}
		}

		// Token: 0x0600975E RID: 38750 RVA: 0x001489E4 File Offset: 0x00146BE4
		public void AddRange(IEnumerable<T> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			ICollection<T> collection = data as ICollection<T>;
			if (collection != null)
			{
				int count = collection.Count;
				if (count > 0)
				{
					this.Reserve(count, false, false);
					collection.CopyTo(this._items, this.Count);
					this.Count += count;
				}
				return;
			}
			foreach (T t in data)
			{
				this.Add(t);
			}
		}

		// Token: 0x0600975F RID: 38751 RVA: 0x0005A6B6 File Offset: 0x000588B6
		public void AssignData(T[] data, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._items = data;
			this.Count = ((count >= 0) ? count : 0);
			this.Capacity = this._items.Length;
		}

		// Token: 0x06009760 RID: 38752 RVA: 0x00148A7C File Offset: 0x00146C7C
		public void Clear(bool forceSetDefaultValues)
		{
			if (this._isNullable || forceSetDefaultValues)
			{
				for (int i = this.Count - 1; i >= 0; i--)
				{
					this._items[i] = default(T);
				}
			}
			this.Count = 0;
		}

		// Token: 0x06009761 RID: 38753 RVA: 0x0005A6E9 File Offset: 0x000588E9
		public void FillWithEmpty(int amount, bool clearCollection = false, bool forceSetDefaultValues = true)
		{
			if (amount > 0)
			{
				if (clearCollection)
				{
					this.Count = 0;
				}
				this.Reserve(amount, clearCollection, forceSetDefaultValues);
				this.Count += amount;
			}
		}

		// Token: 0x06009762 RID: 38754 RVA: 0x0005A710 File Offset: 0x00058910
		public T[] GetData()
		{
			return this._items;
		}

		// Token: 0x06009763 RID: 38755 RVA: 0x0005A718 File Offset: 0x00058918
		public T[] GetData(out int count)
		{
			count = this.Count;
			return this._items;
		}

		// Token: 0x06009764 RID: 38756 RVA: 0x00148AC4 File Offset: 0x00146CC4
		public bool RemoveLast(bool forceSetDefaultValues = true)
		{
			if (this.Count <= 0)
			{
				return false;
			}
			int count = this.Count;
			this.Count = count - 1;
			if (forceSetDefaultValues)
			{
				this._items[this.Count] = default(T);
			}
			return true;
		}

		// Token: 0x06009765 RID: 38757 RVA: 0x00148B0C File Offset: 0x00146D0C
		public void Reserve(int amount, bool totalAmount = false, bool forceSetDefaultValues = true)
		{
			if (amount <= 0)
			{
				return;
			}
			int num = ((!totalAmount) ? this.Count : 0) + amount;
			if (num > this.Capacity)
			{
				if (this.Capacity <= 0)
				{
					this.Capacity = 8;
				}
				while (this.Capacity < num)
				{
					this.Capacity <<= 1;
				}
				T[] array = new T[this.Capacity];
				Array.Copy(this._items, array, this.Count);
				this._items = array;
			}
			if (forceSetDefaultValues)
			{
				for (int i = this.Count; i < num; i++)
				{
					this._items[i] = default(T);
				}
			}
		}

		// Token: 0x06009766 RID: 38758 RVA: 0x00148BB0 File Offset: 0x00146DB0
		public void Reverse()
		{
			if (this.Count > 0)
			{
				int i = 0;
				int num = this.Count >> 1;
				while (i < num)
				{
					T t = this._items[i];
					this._items[i] = this._items[this.Count - i - 1];
					this._items[this.Count - i - 1] = t;
					i++;
				}
			}
		}

		// Token: 0x06009767 RID: 38759 RVA: 0x00148C20 File Offset: 0x00146E20
		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			if (this.Count > 0)
			{
				Array.Copy(this._items, array, this.Count);
			}
			return array;
		}

		// Token: 0x06009768 RID: 38760 RVA: 0x0005A728 File Offset: 0x00058928
		public void UseCastToObjectComparer(bool state)
		{
			this._useObjectCastComparer = state;
		}

		// Token: 0x0400638A RID: 25482
		private const int InitCapacity = 8;

		// Token: 0x0400638B RID: 25483
		private readonly EqualityComparer<T> _comparer;

		// Token: 0x0400638C RID: 25484
		private readonly bool _isNullable;

		// Token: 0x0400638D RID: 25485
		private T[] _items;

		// Token: 0x0400638E RID: 25486
		private bool _useObjectCastComparer;
	}
}
