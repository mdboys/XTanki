using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002978 RID: 10616
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class Not : Attribute
	{
		// Token: 0x06008EC3 RID: 36547 RVA: 0x000559B6 File Offset: 0x00053BB6
		public Not(Type type)
		{
		}

		// Token: 0x04005FE5 RID: 24549
		internal readonly Type Value = type;
	}
}
