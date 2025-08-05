using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.System.Data.Statics.ClientConfigurator.API;
using Platform.System.Data.Statics.ClientYaml.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002873 RID: 10355
	[NullableContext(1)]
	[Nullable(0)]
	public static class ConfigurationEntityIdCalculator
	{
		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06008A64 RID: 35428 RVA: 0x00052DCF File Offset: 0x00050FCF
		// (set) Token: 0x06008A65 RID: 35429 RVA: 0x00052DD6 File Offset: 0x00050FD6
		[Inject]
		private static ConfigurationService ConfigurationService { get; set; }

		// Token: 0x06008A66 RID: 35430 RVA: 0x0013183C File Offset: 0x0012FA3C
		public static int Calculate(string path)
		{
			path = path.Replace("\\", "/");
			if (path.StartsWith("/", StringComparison.Ordinal))
			{
				path = path.Substring(1);
			}
			if (path.EndsWith("/", StringComparison.Ordinal))
			{
				path = path.Substring(0, path.Length - 1);
			}
			if (!ConfigurationEntityIdCalculator.ConfigurationService.HasConfig(path))
			{
				return path.GetHashCode();
			}
			YamlNode config = ConfigurationEntityIdCalculator.ConfigurationService.GetConfig(path);
			if (!config.HasValue("id"))
			{
				return path.GetHashCode();
			}
			return int.Parse(config.GetStringValue("id"));
		}
	}
}
