using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200284B RID: 10315
	[NullableContext(1)]
	[Nullable(0)]
	public class AssemblyTypeCollector
	{
		// Token: 0x060089F3 RID: 35315 RVA: 0x00130F28 File Offset: 0x0012F128
		public static IEnumerable<Type> CollectEmptyEventTypes()
		{
			return from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly assembly) => assembly.GetTypes())
				where type.IsSubclassOf(typeof(Event)) && !type.IsAbstract && AssemblyTypeCollector.IsEmptyType(type)
				select (type);
		}

		// Token: 0x060089F4 RID: 35316 RVA: 0x000528CF File Offset: 0x00050ACF
		private static bool IsEmptyType(Type type)
		{
			return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).Length == 0 && type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).Length == 0;
		}
	}
}
