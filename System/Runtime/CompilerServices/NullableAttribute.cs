using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200000B RID: 11
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableAttribute : Attribute
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00005649 File Offset: 0x00003849
		public NullableAttribute(byte A_1)
		{
			this.NullableFlags = new byte[] { A_1 };
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00005661 File Offset: 0x00003861
		public NullableAttribute(byte[] A_1)
		{
			this.NullableFlags = A_1;
		}

		// Token: 0x04000014 RID: 20
		public readonly byte[] NullableFlags;
	}
}
