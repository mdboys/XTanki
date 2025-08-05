using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000233 RID: 563
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TrackballAttribute : PropertyAttribute
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x0000B996 File Offset: 0x00009B96
		public TrackballAttribute(string method)
		{
			this.method = method;
		}

		// Token: 0x04000822 RID: 2082
		public readonly string method;
	}
}
