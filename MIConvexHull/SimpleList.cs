using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x020029A7 RID: 10663
	[NullableContext(1)]
	[Nullable(0)]
	internal class SimpleList<[Nullable(2)] T>
	{
		// Token: 0x17001632 RID: 5682
		public T this[int i]
		{
			get
			{
				return this.items[i];
			}
			set
			{
				this.items[i] = value;
			}
		}

		// Token: 0x06008F6B RID: 36715 RVA: 0x00139EC8 File Offset: 0x001380C8
		private void EnsureCapacity()
		{
			if (this.capacity == 0)
			{
				this.capacity = 32;
				this.items = new T[32];
				return;
			}
			T[] array = new T[this.capacity * 2];
			Array.Copy(this.items, array, this.capacity);
			this.capacity = 2 * this.capacity;
			this.items = array;
		}

		// Token: 0x06008F6C RID: 36716 RVA: 0x00139F28 File Offset: 0x00138128
		public void Add(T item)
		{
			if (this.Count + 1 > this.capacity)
			{
				this.EnsureCapacity();
			}
			T[] array = this.items;
			int count = this.Count;
			this.Count = count + 1;
			array[count] = item;
		}

		// Token: 0x06008F6D RID: 36717 RVA: 0x00139F28 File Offset: 0x00138128
		public void Push(T item)
		{
			if (this.Count + 1 > this.capacity)
			{
				this.EnsureCapacity();
			}
			T[] array = this.items;
			int count = this.Count;
			this.Count = count + 1;
			array[count] = item;
		}

		// Token: 0x06008F6E RID: 36718 RVA: 0x00139F68 File Offset: 0x00138168
		public T Pop()
		{
			T[] array = this.items;
			int num = this.Count - 1;
			this.Count = num;
			return array[num];
		}

		// Token: 0x06008F6F RID: 36719 RVA: 0x00055F17 File Offset: 0x00054117
		public void Clear()
		{
			this.Count = 0;
		}

		// Token: 0x0400604F RID: 24655
		private int capacity;

		// Token: 0x04006050 RID: 24656
		public int Count;

		// Token: 0x04006051 RID: 24657
		private T[] items;
	}
}
