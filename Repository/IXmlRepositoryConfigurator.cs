using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace log4net.Repository
{
	// Token: 0x020029F9 RID: 10745
	[NullableContext(1)]
	public interface IXmlRepositoryConfigurator
	{
		// Token: 0x060091E8 RID: 37352
		void Configure(XmlElement element);
	}
}
