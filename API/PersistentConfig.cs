using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200297C RID: 10620
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public class PersistentConfig : Attribute
	{
		// Token: 0x06008EC7 RID: 36551 RVA: 0x000559C5 File Offset: 0x00053BC5
		public PersistentConfig(string value = "", bool configOptional = false)
		{
		}

		// Token: 0x04005FE6 RID: 24550
		internal readonly bool ConfigOptional = configOptional;

		// Token: 0x04005FE7 RID: 24551
		internal readonly string Value = value;
	}
}
