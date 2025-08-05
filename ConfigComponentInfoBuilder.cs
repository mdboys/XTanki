using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200286F RID: 10351
	[NullableContext(1)]
	[Nullable(0)]
	public class ConfigComponentInfoBuilder : ComponentInfoBuilder
	{
		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06008A50 RID: 35408 RVA: 0x00052D2D File Offset: 0x00050F2D
		Type ComponentInfoBuilder.TemplateComponentInfoClass
		{
			get
			{
				return typeof(ConfigComponentInfo);
			}
		}

		// Token: 0x06008A51 RID: 35409 RVA: 0x00052D39 File Offset: 0x00050F39
		public bool IsAcceptable(MethodInfo componentMethod)
		{
			return componentMethod.GetCustomAttributes(typeof(PersistentConfig), false).Length == 1;
		}

		// Token: 0x06008A52 RID: 35410 RVA: 0x00131720 File Offset: 0x0012F920
		public ComponentInfo Build(MethodInfo componentMethod, ComponentDescriptionImpl componentDescription)
		{
			PersistentConfig persistentConfig = (PersistentConfig)componentMethod.GetCustomAttributes(typeof(PersistentConfig), false)[0];
			string text = persistentConfig.Value;
			if (text.Length == 0)
			{
				text = componentDescription.FieldName;
			}
			return new ConfigComponentInfo(text, persistentConfig.ConfigOptional);
		}
	}
}
