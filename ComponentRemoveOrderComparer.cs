using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200286B RID: 10347
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public class ComponentRemoveOrderComparer : Comparer<Type>
	{
		// Token: 0x06008A48 RID: 35400 RVA: 0x00052CD0 File Offset: 0x00050ED0
		public override int Compare(Type x, Type y)
		{
			return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
		}
	}
}
