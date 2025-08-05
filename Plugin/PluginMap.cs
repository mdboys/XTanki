using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Repository;

namespace log4net.Plugin
{
	// Token: 0x02002A12 RID: 10770
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PluginMap
	{
		// Token: 0x060092D7 RID: 37591 RVA: 0x00057C3F File Offset: 0x00055E3F
		public PluginMap(ILoggerRepository repository)
		{
			this.m_repository = repository;
		}

		// Token: 0x170016EA RID: 5866
		public IPlugin this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				IPlugin plugin;
				lock (this)
				{
					plugin = (IPlugin)this.m_mapName2Plugin[name];
				}
				return plugin;
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x060092D9 RID: 37593 RVA: 0x001408B0 File Offset: 0x0013EAB0
		public PluginCollection AllPlugins
		{
			get
			{
				PluginCollection pluginCollection;
				lock (this)
				{
					pluginCollection = new PluginCollection(this.m_mapName2Plugin.Values);
				}
				return pluginCollection;
			}
		}

		// Token: 0x060092DA RID: 37594 RVA: 0x001408F0 File Offset: 0x0013EAF0
		public void Add(IPlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			IPlugin plugin2 = null;
			lock (this)
			{
				plugin2 = this.m_mapName2Plugin[plugin.Name] as IPlugin;
				this.m_mapName2Plugin[plugin.Name] = plugin;
			}
			if (plugin2 != null)
			{
				plugin2.Shutdown();
			}
			plugin.Attach(this.m_repository);
		}

		// Token: 0x060092DB RID: 37595 RVA: 0x0014096C File Offset: 0x0013EB6C
		public void Remove(IPlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			lock (this)
			{
				this.m_mapName2Plugin.Remove(plugin.Name);
			}
		}

		// Token: 0x0400618E RID: 24974
		private readonly Hashtable m_mapName2Plugin = new Hashtable();

		// Token: 0x0400618F RID: 24975
		private readonly ILoggerRepository m_repository;
	}
}
