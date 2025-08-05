using System;
using System.Runtime.CompilerServices;

namespace log4net.Config
{
	// Token: 0x02002A77 RID: 10871
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public class RepositoryAttribute : Attribute
	{
		// Token: 0x0600950E RID: 38158 RVA: 0x00005641 File Offset: 0x00003841
		public RepositoryAttribute()
		{
		}

		// Token: 0x0600950F RID: 38159 RVA: 0x000593E7 File Offset: 0x000575E7
		public RepositoryAttribute(string name)
		{
			this.m_name = name;
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x06009510 RID: 38160 RVA: 0x000593F6 File Offset: 0x000575F6
		// (set) Token: 0x06009511 RID: 38161 RVA: 0x000593FE File Offset: 0x000575FE
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

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06009512 RID: 38162 RVA: 0x00059407 File Offset: 0x00057607
		// (set) Token: 0x06009513 RID: 38163 RVA: 0x0005940F File Offset: 0x0005760F
		public Type RepositoryType
		{
			get
			{
				return this.m_repositoryType;
			}
			set
			{
				this.m_repositoryType = value;
			}
		}

		// Token: 0x04006268 RID: 25192
		private string m_name;

		// Token: 0x04006269 RID: 25193
		private Type m_repositoryType;
	}
}
