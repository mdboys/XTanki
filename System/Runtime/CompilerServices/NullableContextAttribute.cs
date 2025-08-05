using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200000C RID: 12
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableContextAttribute : Attribute
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00005670 File Offset: 0x00003870
		public NullableContextAttribute(byte A_1)
		{
			this.Flag = A_1;
		}

		// Token: 0x04000015 RID: 21
		public readonly byte Flag;
	}
}
