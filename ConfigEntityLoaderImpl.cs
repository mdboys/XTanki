using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.System.Data.Statics.ClientConfigurator.API;
using Platform.System.Data.Statics.ClientYaml.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002871 RID: 10353
	[NullableContext(1)]
	[Nullable(0)]
	public class ConfigEntityLoaderImpl : ConfigEntityLoader
	{
		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06008A59 RID: 35417 RVA: 0x00052D73 File Offset: 0x00050F73
		// (set) Token: 0x06008A5A RID: 35418 RVA: 0x00052D7A File Offset: 0x00050F7A
		[Inject]
		private static ConfigurationService ConfigurationService { get; set; } = null;

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06008A5B RID: 35419 RVA: 0x00052D82 File Offset: 0x00050F82
		// (set) Token: 0x06008A5C RID: 35420 RVA: 0x00052D89 File Offset: 0x00050F89
		[Inject]
		private static SharedEntityRegistry SharedEntityRegistry { get; set; } = null;

		// Token: 0x06008A5D RID: 35421 RVA: 0x00131768 File Offset: 0x0012F968
		public void LoadEntities(Engine engine)
		{
			if (!ConfigEntityLoaderImpl.ConfigurationService.HasConfig(ConfigEntityLoaderImpl.STARTUP_CONFIG_PATH))
			{
				return;
			}
			foreach (ConfigEntityInfo configEntityInfo in from node in ConfigEntityLoaderImpl.ConfigurationService.GetConfig(ConfigEntityLoaderImpl.STARTUP_CONFIG_PATH).GetChildListNodes(ConfigEntityLoaderImpl.STARTUP_ROOT_NODE)
				select node.ConvertTo<ConfigEntityInfo>())
			{
				if (ConfigEntityLoaderImpl.ConfigurationService.HasConfig(configEntityInfo.Path))
				{
					EntityInternal entityInternal = ConfigEntityLoaderImpl.SharedEntityRegistry.CreateEntity(configEntityInfo.TemplateId, configEntityInfo.Path, (long)ConfigEntityLoaderImpl.GetConfigEntityId(configEntityInfo.Path));
					ConfigEntityLoaderImpl.SharedEntityRegistry.SetShared(entityInternal.Id);
				}
			}
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x00052D91 File Offset: 0x00050F91
		private static int GetConfigEntityId(string path)
		{
			return ConfigurationEntityIdCalculator.Calculate(path);
		}

		// Token: 0x04005E71 RID: 24177
		private static readonly string STARTUP_CONFIG_PATH = "service/configentityloader";

		// Token: 0x04005E72 RID: 24178
		private static readonly string STARTUP_ROOT_NODE = "autoCreatedEntities";
	}
}
