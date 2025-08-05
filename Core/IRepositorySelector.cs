using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Repository;

namespace log4net.Core
{
	// Token: 0x02002A56 RID: 10838
	[NullableContext(1)]
	public interface IRepositorySelector
	{
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060093D8 RID: 37848
		// (remove) Token: 0x060093D9 RID: 37849
		event LoggerRepositoryCreationEventHandler LoggerRepositoryCreatedEvent;

		// Token: 0x060093DA RID: 37850
		ILoggerRepository GetRepository(Assembly assembly);

		// Token: 0x060093DB RID: 37851
		ILoggerRepository GetRepository(string repositoryName);

		// Token: 0x060093DC RID: 37852
		ILoggerRepository CreateRepository(Assembly assembly, Type repositoryType);

		// Token: 0x060093DD RID: 37853
		ILoggerRepository CreateRepository(string repositoryName, Type repositoryType);

		// Token: 0x060093DE RID: 37854
		bool ExistsRepository(string repositoryName);

		// Token: 0x060093DF RID: 37855
		ILoggerRepository[] GetAllRepositories();
	}
}
