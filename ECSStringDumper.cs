using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200287F RID: 10367
	[NullableContext(1)]
	[Nullable(0)]
	public static class ECSStringDumper
	{
		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06008A81 RID: 35457 RVA: 0x00052F26 File Offset: 0x00051126
		// (set) Token: 0x06008A82 RID: 35458 RVA: 0x00052F2D File Offset: 0x0005112D
		[Inject]
		private static EngineServiceInternal EngineService { get; set; }

		// Token: 0x06008A83 RID: 35459 RVA: 0x00131AD8 File Offset: 0x0012FCD8
		public static string Build()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Entity entity in ECSStringDumper.EngineService.EntityRegistry.GetAllEntities())
			{
				if (!ECSStringDumper.EngineService.EntityStub.Equals(entity))
				{
					string text = string.Format("[Entity: Id={0}, Name={1}]\n", entity.Id, entity.Name);
					stringBuilder.Append(text);
					IEnumerable<Component> enumerable;
					if (entity is EntityStub)
					{
						ICollection<Component> collection = new List<Component>();
						enumerable = collection;
					}
					else
					{
						enumerable = ((EntityInternal)entity).Components;
					}
					foreach (Component component in enumerable)
					{
						stringBuilder.Append("[Component: ");
						stringBuilder.Append(EcsToStringUtil.ToStringWithProperties(component, ", "));
						stringBuilder.Append("]\n");
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
