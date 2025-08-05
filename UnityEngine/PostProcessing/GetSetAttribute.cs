using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200020E RID: 526
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GetSetAttribute : PropertyAttribute
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x0000B517 File Offset: 0x00009717
		public GetSetAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x04000753 RID: 1875
		public readonly string name;

		// Token: 0x04000754 RID: 1876
		public bool dirty;
	}
}
