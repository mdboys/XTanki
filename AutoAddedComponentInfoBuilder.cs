using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200284F RID: 10319
	[Nullable(new byte[] { 0, 1 })]
	public class AutoAddedComponentInfoBuilder : AnnotationComponentInfoBuilder<AutoAddedComponentInfo>
	{
		// Token: 0x06008A03 RID: 35331 RVA: 0x00052939 File Offset: 0x00050B39
		public AutoAddedComponentInfoBuilder()
			: base(typeof(AutoAdded))
		{
		}
	}
}
