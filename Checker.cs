using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200285D RID: 10333
	[NullableContext(1)]
	[Nullable(0)]
	public class Checker
	{
		// Token: 0x06008A2A RID: 35370 RVA: 0x00052B2C File Offset: 0x00050D2C
		public static void RequireNotEmpty(ICollection c)
		{
			if (c.Count != 0)
			{
				throw new EmptyCollectionNotSupportedException();
			}
		}

		// Token: 0x06008A2B RID: 35371 RVA: 0x00052B3C File Offset: 0x00050D3C
		public static void RequireOneOnly<[Nullable(2)] T>(ICollection<T> c)
		{
			if (c.Count != 1)
			{
				throw new RequiredOneElementOnlyException();
			}
		}
	}
}
