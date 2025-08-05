using System;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029B8 RID: 10680
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ContextPropertiesBase
	{
		// Token: 0x17001650 RID: 5712
		public abstract object this[string key] { get; set; }
	}
}
