using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000215 RID: 533
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0000B5FD File Offset: 0x000097FD
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04000762 RID: 1890
		public readonly float min;
	}
}
