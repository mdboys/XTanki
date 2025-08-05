using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002964 RID: 10596
	public class Event
	{
		// Token: 0x06008E7B RID: 36475 RVA: 0x0005583D File Offset: 0x00053A3D
		[NullableContext(1)]
		public override string ToString()
		{
			return EcsToStringUtil.ToStringWithProperties(this, ", ");
		}
	}
}
