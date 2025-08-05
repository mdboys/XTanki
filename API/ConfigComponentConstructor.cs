using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;
using Platform.System.Data.Statics.ClientYaml.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002952 RID: 10578
	[NullableContext(1)]
	[Nullable(0)]
	public class ConfigComponentConstructor : AbstractTemplateComponentConstructor
	{
		// Token: 0x06008E18 RID: 36376 RVA: 0x000556D0 File Offset: 0x000538D0
		protected override bool IsAcceptable(ComponentDescription componentDescription)
		{
			return componentDescription.IsInfoPresent(typeof(ConfigComponentInfo));
		}

		// Token: 0x06008E19 RID: 36377 RVA: 0x00137184 File Offset: 0x00135384
		protected override Component GetComponentInstance(ComponentDescription componentDescription, EntityInternal entity)
		{
			ConfigComponentInfo info = componentDescription.GetInfo<ConfigComponentInfo>();
			TemplateAccessor templateAccessor = entity.TemplateAccessor.Get();
			YamlNode yamlNode = templateAccessor.YamlNode;
			string keyName = info.KeyName;
			if (info.ConfigOptional && !yamlNode.HasValue(keyName))
			{
				return (Component)Activator.CreateInstance(componentDescription.ComponentType);
			}
			Component component;
			try
			{
				component = (Component)yamlNode.GetChildNode(keyName).ConvertTo(componentDescription.ComponentType);
			}
			catch (Exception ex)
			{
				string text = (templateAccessor.HasConfigPath() ? templateAccessor.ConfigPath : yamlNode.ToString());
				throw new Exception(string.Format("Error deserializing component {0} from configs, entity={1}, key={2}, pathOrNode={3}", new object[] { componentDescription.ComponentType, entity, keyName, text }), ex);
			}
			return component;
		}
	}
}
