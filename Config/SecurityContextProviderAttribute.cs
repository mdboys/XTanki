using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Repository;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x02002A78 RID: 10872
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class SecurityContextProviderAttribute : ConfiguratorAttribute
	{
		// Token: 0x06009514 RID: 38164 RVA: 0x00059418 File Offset: 0x00057618
		public SecurityContextProviderAttribute(Type providerType)
			: base(100)
		{
			this.m_providerType = providerType;
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06009515 RID: 38165 RVA: 0x00059429 File Offset: 0x00057629
		// (set) Token: 0x06009516 RID: 38166 RVA: 0x00059431 File Offset: 0x00057631
		public Type ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			set
			{
				this.m_providerType = value;
			}
		}

		// Token: 0x06009517 RID: 38167 RVA: 0x001447FC File Offset: 0x001429FC
		public override void Configure(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
			if (this.m_providerType == null)
			{
				LogLog.Error(SecurityContextProviderAttribute.declaringType, "Attribute specified on assembly [" + sourceAssembly.FullName + "] with null ProviderType.");
				return;
			}
			LogLog.Debug(SecurityContextProviderAttribute.declaringType, "Creating provider of type [" + this.m_providerType.FullName + "]");
			SecurityContextProvider securityContextProvider = Activator.CreateInstance(this.m_providerType) as SecurityContextProvider;
			if (securityContextProvider == null)
			{
				LogLog.Error(SecurityContextProviderAttribute.declaringType, "Failed to create SecurityContextProvider instance of type [" + this.m_providerType.Name + "].");
				return;
			}
			SecurityContextProvider.DefaultProvider = securityContextProvider;
		}

		// Token: 0x0400626A RID: 25194
		private static readonly Type declaringType = typeof(SecurityContextProviderAttribute);

		// Token: 0x0400626B RID: 25195
		private Type m_providerType;
	}
}
