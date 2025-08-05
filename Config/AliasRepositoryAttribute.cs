using System;
using System.Runtime.CompilerServices;

namespace log4net.Config
{
	// Token: 0x02002A72 RID: 10866
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public class AliasRepositoryAttribute : Attribute
	{
		// Token: 0x060094F5 RID: 38133 RVA: 0x000592E8 File Offset: 0x000574E8
		public AliasRepositoryAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x060094F6 RID: 38134 RVA: 0x000592F7 File Offset: 0x000574F7
		// (set) Token: 0x060094F7 RID: 38135 RVA: 0x000592FF File Offset: 0x000574FF
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x04006263 RID: 25187
		private string m_name;
	}
}
