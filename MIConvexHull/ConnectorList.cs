using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x02002993 RID: 10643
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ConnectorList
	{
		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06008EFA RID: 36602 RVA: 0x00055AB1 File Offset: 0x00053CB1
		// (set) Token: 0x06008EFB RID: 36603 RVA: 0x00055AB9 File Offset: 0x00053CB9
		public FaceConnector First { get; private set; }

		// Token: 0x06008EFC RID: 36604 RVA: 0x00055AC2 File Offset: 0x00053CC2
		private void AddFirst(FaceConnector connector)
		{
			this.First.Previous = connector;
			connector.Next = this.First;
			this.First = connector;
		}

		// Token: 0x06008EFD RID: 36605 RVA: 0x00055AE3 File Offset: 0x00053CE3
		public void Add(FaceConnector element)
		{
			if (this.last != null)
			{
				this.last.Next = element;
			}
			element.Previous = this.last;
			this.last = element;
			if (this.First == null)
			{
				this.First = element;
			}
		}

		// Token: 0x06008EFE RID: 36606 RVA: 0x001376B0 File Offset: 0x001358B0
		public void Remove(FaceConnector connector)
		{
			if (connector.Previous != null)
			{
				connector.Previous.Next = connector.Next;
			}
			else if (connector.Previous == null)
			{
				this.First = connector.Next;
			}
			if (connector.Next != null)
			{
				connector.Next.Previous = connector.Previous;
			}
			else if (connector.Next == null)
			{
				this.last = connector.Previous;
			}
			connector.Next = null;
			connector.Previous = null;
		}

		// Token: 0x04005FF4 RID: 24564
		private FaceConnector last;
	}
}
