using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.System.Data.Statics.ClientYaml.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002899 RID: 10393
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public class EntitySystemActivator : DefaultActivator<AutoCompleting>
	{
		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06008B97 RID: 35735 RVA: 0x00053A53 File Offset: 0x00051C53
		// (set) Token: 0x06008B98 RID: 35736 RVA: 0x00053A5A File Offset: 0x00051C5A
		[Inject]
		private static YamlService YamlService { get; set; }

		// Token: 0x06008B99 RID: 35737 RVA: 0x00133484 File Offset: 0x00131684
		protected override void Activate()
		{
			TemplateRegistryImpl templateRegistryImpl = new TemplateRegistryImpl();
			ConfigEntityLoaderImpl configEntityLoaderImpl = new ConfigEntityLoaderImpl();
			GroupRegistryImpl groupRegistryImpl = new GroupRegistryImpl();
			ComponentBitIdRegistryImpl componentBitIdRegistryImpl = new ComponentBitIdRegistryImpl();
			ServiceRegistry.Current.RegisterService<TemplateRegistry>(templateRegistryImpl);
			ServiceRegistry.Current.RegisterService<ConfigEntityLoader>(configEntityLoaderImpl);
			ServiceRegistry.Current.RegisterService<ComponentBitIdRegistry>(componentBitIdRegistryImpl);
			ServiceRegistry.Current.RegisterService<GroupRegistry>(groupRegistryImpl);
			ServiceRegistry.Current.RegisterService<NodeDescriptionRegistry>(new NodeDescriptionRegistryImpl());
			HandlerCollector handlerCollector = new HandlerCollector();
			EventMaker eventMaker = new EventMaker(handlerCollector);
			EngineServiceImpl engineServiceImpl = new EngineServiceImpl(templateRegistryImpl, handlerCollector, eventMaker, componentBitIdRegistryImpl);
			engineServiceImpl.HandlerCollector.AddHandlerListener(componentBitIdRegistryImpl);
			EntitySystemActivator.YamlService.RegisterConverter(new EntityYamlConverter(engineServiceImpl));
			EntitySystemActivator.YamlService.RegisterConverter(new TemplateDescriptionYamlConverter(templateRegistryImpl));
			ServiceRegistry.Current.RegisterService<EngineService>(engineServiceImpl);
			ServiceRegistry.Current.RegisterService<EngineServiceInternal>(engineServiceImpl);
			ServiceRegistry.Current.RegisterService<FlowInstancesCache>(new FlowInstancesCache());
		}
	}
}
