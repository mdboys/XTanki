using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002974 RID: 10612
	[NullableContext(1)]
	[Nullable(0)]
	public class Node
	{
		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06008EB2 RID: 36530 RVA: 0x00055973 File Offset: 0x00053B73
		// (set) Token: 0x06008EB3 RID: 36531 RVA: 0x0005597B File Offset: 0x00053B7B
		public Entity Entity { get; set; }

		// Token: 0x06008EB4 RID: 36532 RVA: 0x00137648 File Offset: 0x00135848
		[NullableContext(2)]
		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o is Entity)
			{
				return this.Entity.Equals(o);
			}
			if (o != null && base.GetType() == o.GetType())
			{
				Node node = o as Node;
				if (node != null)
				{
					if (this.Entity == null)
					{
						return node.Entity == null;
					}
					return this.Entity.Equals(node.Entity);
				}
			}
			return false;
		}

		// Token: 0x06008EB5 RID: 36533 RVA: 0x00055984 File Offset: 0x00053B84
		public override int GetHashCode()
		{
			return this.Entity.GetHashCode();
		}

		// Token: 0x06008EB6 RID: 36534 RVA: 0x00055991 File Offset: 0x00053B91
		public override string ToString()
		{
			return this.Entity.ToString();
		}
	}
}
