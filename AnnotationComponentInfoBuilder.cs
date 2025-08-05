using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002848 RID: 10312
	[NullableContext(1)]
	[Nullable(0)]
	public class AnnotationComponentInfoBuilder<[Nullable(0)] T> : ComponentInfoBuilder where T : ComponentInfo, new()
	{
		// Token: 0x060089E3 RID: 35299 RVA: 0x00052810 File Offset: 0x00050A10
		protected AnnotationComponentInfoBuilder(Type annotationType)
		{
			this._annotationType = annotationType;
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x060089E4 RID: 35300 RVA: 0x0005281F File Offset: 0x00050A1F
		public Type TemplateComponentInfoClass
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x060089E5 RID: 35301 RVA: 0x0005282B File Offset: 0x00050A2B
		public bool IsAcceptable(MethodInfo componentMethod)
		{
			return componentMethod.GetCustomAttributes(this._annotationType, true).Length == 1;
		}

		// Token: 0x060089E6 RID: 35302 RVA: 0x0004D529 File Offset: 0x0004B729
		public ComponentInfo Build(MethodInfo componentMethod, ComponentDescriptionImpl componentDescription)
		{
			return new T();
		}

		// Token: 0x04005E4B RID: 24139
		private readonly Type _annotationType;
	}
}
