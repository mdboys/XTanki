using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LeopotamGroup.Collections
{
	// Token: 0x02002AE5 RID: 10981
	[NullableContext(1)]
	[Nullable(0)]
	public class FastStack<[Nullable(2)] T>
	{
		// Token: 0x06009769 RID: 38761 RVA: 0x0005A731 File Offset: 0x00058931
		public FastStack()
			: this(null)
		{
		}

		// Token: 0x0600976A RID: 38762 RVA: 0x0005A73A File Offset: 0x0005893A
		public FastStack(EqualityComparer<T> comparer)
			: this(8, comparer)
		{
		}

		// Token: 0x0600976B RID: 38763 RVA: 0x00148C58 File Offset: 0x00146E58
		public FastStack(int capacity, EqualityComparer<T> comparer = null)
		{
			Type typeFromHandle = typeof(T);
			this._isNullable = !typeFromHandle.IsValueType || Nullable.GetUnderlyingType(typeFromHandle) != null;
			this._capacity = ((capacity <= 8) ? 8 : capacity);
			this.Count = 0;
			this._comparer = comparer;
			this._items = new T[this._capacity];
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x0600976C RID: 38764 RVA: 0x0005A744 File Offset: 0x00058944
		// (set) Token: 0x0600976D RID: 38765 RVA: 0x0005A74C File Offset: 0x0005894C
		public int Count { get; private set; }

		// Token: 0x0600976E RID: 38766 RVA: 0x00148CC0 File Offset: 0x00146EC0
		public void Clear()
		{
			if (this._isNullable)
			{
				for (int i = this.Count - 1; i >= 0; i--)
				{
					this._items[i] = default(T);
				}
			}
			this.Count = 0;
		}

		// Token: 0x0600976F RID: 38767 RVA: 0x00148D04 File Offset: 0x00146F04
		public bool Contains(T item)
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
			return i != -1;
		}

		// Token: 0x06009770 RID: 38768 RVA: 0x0005A755 File Offset: 0x00058955
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this.Count);
		}

		// Token: 0x06009771 RID: 38769 RVA: 0x0005A76B File Offset: 0x0005896B
		public T Peek()
		{
			if (this.Count == 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._items[this.Count - 1];
		}

		// Token: 0x06009772 RID: 38770 RVA: 0x00148DA4 File Offset: 0x00146FA4
		public T Pop()
		{
			if (this.Count == 0)
			{
				throw new IndexOutOfRangeException();
			}
			int count = this.Count;
			this.Count = count - 1;
			T t = this._items[this.Count];
			if (this._isNullable)
			{
				this._items[this.Count] = default(T);
			}
			return t;
		}

		// Token: 0x06009773 RID: 38771 RVA: 0x00148E04 File Offset: 0x00147004
		public void Push(T item)
		{
			if (this.Count == this._capacity)
			{
				if (this._capacity > 0)
				{
					this._capacity <<= 1;
				}
				else
				{
					this._capacity = 8;
				}
				T[] array = new T[this._capacity];
				Array.Copy(this._items, array, this.Count);
				this._items = array;
			}
			this._items[this.Count] = item;
			int count = this.Count;
			this.Count = count + 1;
		}

		// Token: 0x06009774 RID: 38772 RVA: 0x00148E88 File Offset: 0x00147088
		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			if (this.Count > 0)
			{
				Array.Copy(this._items, array, this.Count);
			}
			return array;
		}

		// Token: 0x06009775 RID: 38773 RVA: 0x00005B56 File Offset: 0x00003D56
		public void TrimExcess()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06009776 RID: 38774 RVA: 0x0005A78E File Offset: 0x0005898E
		public void UseCastToObjectComparer(bool state)
		{
			this._useObjectCastComparer = state;
		}

		// Token: 0x04006391 RID: 25489
		private const int InitCapacity = 8;

		// Token: 0x04006392 RID: 25490
		private readonly EqualityComparer<T> _comparer;

		// Token: 0x04006393 RID: 25491
		private readonly bool _isNullable;

		// Token: 0x04006394 RID: 25492
		private int _capacity;

		// Token: 0x04006395 RID: 25493
		private T[] _items;

		// Token: 0x04006396 RID: 25494
		private bool _useObjectCastComparer;
	}
}
