using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002852 RID: 10322
	[NullableContext(1)]
	[Nullable(0)]
	public class AutoRemoveComponentsRegistryImpl : AutoRemoveComponentsRegistry, EngineHandlerRegistrationListener
	{
		// Token: 0x06008A08 RID: 35336 RVA: 0x0005296B File Offset: 0x00050B6B
		public AutoRemoveComponentsRegistryImpl(EngineService engineService)
		{
			engineService.AddSystemProcessingListener(this);
		}

		// Token: 0x06008A09 RID: 35337 RVA: 0x00052985 File Offset: 0x00050B85
		public bool IsComponentAutoRemoved(Type componentType)
		{
			return this._componentTypes.Contains(componentType);
		}

		// Token: 0x06008A0A RID: 35338 RVA: 0x00131070 File Offset: 0x0012F270
		public void OnHandlerAdded(Handler handler)
		{
			if (handler.EventType != typeof(NodeRemoveEvent))
			{
				return;
			}
			foreach (HandlerArgument handlerArgument in handler.ContextArguments)
			{
				ICollection<Type> components = handlerArgument.NodeDescription.Components;
				if (!this.IsNodeAutoRemoved(components))
				{
					this.RegisterOneComponent(components);
				}
			}
		}

		// Token: 0x06008A0B RID: 35339 RVA: 0x00052993 File Offset: 0x00050B93
		private bool IsNodeAutoRemoved(ICollection<Type> components)
		{
			return components.Any(new Func<Type, bool>(this._componentTypes.Contains));
		}

		// Token: 0x06008A0C RID: 35340 RVA: 0x001310E4 File Offset: 0x0012F2E4
		private void RegisterOneComponent(ICollection<Type> components)
		{
			foreach (Type type in components)
			{
				if (!type.IsDefined(typeof(SkipAutoRemove), true))
				{
					this._componentTypes.Add(type);
					break;
				}
			}
		}

		// Token: 0x04005E57 RID: 24151
		private readonly HashSet<Type> _componentTypes = new HashSet<Type>();
	}
}
