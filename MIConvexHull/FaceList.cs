using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x020029A0 RID: 10656
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class FaceList
	{
		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06008F47 RID: 36679 RVA: 0x00055DA0 File Offset: 0x00053FA0
		// (set) Token: 0x06008F48 RID: 36680 RVA: 0x00055DA8 File Offset: 0x00053FA8
		public ConvexFaceInternal First { get; private set; }

		// Token: 0x06008F49 RID: 36681 RVA: 0x00055DB1 File Offset: 0x00053FB1
		private void AddFirst(ConvexFaceInternal face)
		{
			face.InList = true;
			this.First.Previous = face;
			face.Next = this.First;
			this.First = face;
		}

		// Token: 0x06008F4A RID: 36682 RVA: 0x00139154 File Offset: 0x00137354
		public void Add(ConvexFaceInternal face)
		{
			if (face.InList)
			{
				if (this.First.VerticesBeyond.Count < face.VerticesBeyond.Count)
				{
					this.Remove(face);
					this.AddFirst(face);
				}
				return;
			}
			face.InList = true;
			if (this.First != null && this.First.VerticesBeyond.Count < face.VerticesBeyond.Count)
			{
				this.First.Previous = face;
				face.Next = this.First;
				this.First = face;
				return;
			}
			if (this.last != null)
			{
				this.last.Next = face;
			}
			face.Previous = this.last;
			this.last = face;
			if (this.First == null)
			{
				this.First = face;
			}
		}

		// Token: 0x06008F4B RID: 36683 RVA: 0x00139218 File Offset: 0x00137418
		public void Remove(ConvexFaceInternal face)
		{
			if (face.InList)
			{
				face.InList = false;
				if (face.Previous != null)
				{
					face.Previous.Next = face.Next;
				}
				else if (face.Previous == null)
				{
					this.First = face.Next;
				}
				if (face.Next != null)
				{
					face.Next.Previous = face.Previous;
				}
				else if (face.Next == null)
				{
					this.last = face.Previous;
				}
				face.Next = null;
				face.Previous = null;
			}
		}

		// Token: 0x04006035 RID: 24629
		private ConvexFaceInternal last;
	}
}
