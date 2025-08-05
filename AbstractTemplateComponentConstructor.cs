using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002841 RID: 10305
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AbstractTemplateComponentConstructor : ComponentConstructor
	{
		// Token: 0x060089D3 RID: 35283 RVA: 0x00130CC0 File Offset: 0x0012EEC0
		public bool IsAcceptable(Type componentType, EntityInternal entity)
		{
			Optional<TemplateAccessor> templateAccessor = entity.TemplateAccessor;
			if (!templateAccessor.IsPresent())
			{
				return false;
			}
			TemplateDescription templateDescription = templateAccessor.Get().TemplateDescription;
			return templateDescription.IsComponentDescriptionPresent(componentType) && this.IsAcceptable(templateDescription.GetComponentDescription(componentType));
		}

		// Token: 0x060089D4 RID: 35284 RVA: 0x00130D04 File Offset: 0x0012EF04
		public Component GetComponentInstance(Type componentType, EntityInternal entity)
		{
			return this.GetComponentInstance(entity.TemplateAccessor.Get().TemplateDescription.GetComponentDescription(componentType), entity);
		}

		// Token: 0x060089D5 RID: 35285
		protected abstract bool IsAcceptable(ComponentDescription componentDescription);

		// Token: 0x060089D6 RID: 35286
		protected abstract Component GetComponentInstance(ComponentDescription componentDescription, EntityInternal entity);
	}
}
