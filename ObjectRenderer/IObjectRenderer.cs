using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace log4net.ObjectRenderer
{
	// Token: 0x02002A15 RID: 10773
	[NullableContext(1)]
	public interface IObjectRenderer
	{
		// Token: 0x060092E8 RID: 37608
		void RenderObject(RendererMap rendererMap, object obj, TextWriter writer);
	}
}
