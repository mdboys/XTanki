using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200000D RID: 13
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = false, Inherited = false)]
	internal sealed class RefSafetyRulesAttribute : Attribute
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000567F File Offset: 0x0000387F
		public RefSafetyRulesAttribute(int A_1)
		{
			this.Version = A_1;
		}

		// Token: 0x04000016 RID: 22
		public readonly int Version;
	}
}
