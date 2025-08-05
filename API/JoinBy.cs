using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200296F RID: 10607
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = false)]
	public class JoinBy : Attribute
	{
		// Token: 0x06008EAD RID: 36525 RVA: 0x00055964 File Offset: 0x00053B64
		public JoinBy(Type value)
		{
		}

		// Token: 0x04005FE1 RID: 24545
		internal readonly Type Value = value;
	}
}
