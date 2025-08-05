using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Repository;

namespace log4net.Config
{
	// Token: 0x02002A74 RID: 10868
	[AttributeUsage(AttributeTargets.Assembly)]
	public abstract class ConfiguratorAttribute : Attribute, IComparable
	{
		// Token: 0x06009501 RID: 38145 RVA: 0x0005934D File Offset: 0x0005754D
		protected ConfiguratorAttribute(int priority)
		{
			this.m_priority = priority;
		}

		// Token: 0x06009502 RID: 38146 RVA: 0x0014475C File Offset: 0x0014295C
		[NullableContext(1)]
		public int CompareTo(object obj)
		{
			if (this == obj)
			{
				return 0;
			}
			int num = -1;
			ConfiguratorAttribute configuratorAttribute = obj as ConfiguratorAttribute;
			if (configuratorAttribute != null)
			{
				num = configuratorAttribute.m_priority.CompareTo(this.m_priority);
				if (num == 0)
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x06009503 RID: 38147
		[NullableContext(1)]
		public abstract void Configure(Assembly sourceAssembly, ILoggerRepository targetRepository);

		// Token: 0x04006265 RID: 25189
		private readonly int m_priority;
	}
}
