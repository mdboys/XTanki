using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002864 RID: 10340
	[NullableContext(1)]
	[Nullable(0)]
	public class ComponentDescriptionImpl : ComponentDescription
	{
		// Token: 0x06008A39 RID: 35385 RVA: 0x00052C0A File Offset: 0x00050E0A
		public ComponentDescriptionImpl(MethodInfo componentMethod)
		{
			this.FieldName = this.<componentMethod>P.Name;
			this.ComponentType = ComponentDescriptionImpl.GetComponentType(this.<componentMethod>P);
			base..ctor();
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06008A3A RID: 35386 RVA: 0x00052C46 File Offset: 0x00050E46
		public string FieldName { get; }

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06008A3B RID: 35387 RVA: 0x00052C4E File Offset: 0x00050E4E
		public Type ComponentType { get; }

		// Token: 0x06008A3C RID: 35388 RVA: 0x0013160C File Offset: 0x0012F80C
		public T GetInfo<[Nullable(0)] T>() where T : ComponentInfo
		{
			Type typeFromHandle = typeof(T);
			ComponentInfo componentInfo;
			if (!this._infos.TryGetValue(typeFromHandle, out componentInfo))
			{
				throw new ComponentInfoNotFoundException(typeFromHandle, this.<componentMethod>P);
			}
			return (T)((object)componentInfo);
		}

		// Token: 0x06008A3D RID: 35389 RVA: 0x00052C56 File Offset: 0x00050E56
		public bool IsInfoPresent(Type infoType)
		{
			return this._infos.ContainsKey(infoType);
		}

		// Token: 0x06008A3E RID: 35390 RVA: 0x00131648 File Offset: 0x0012F848
		public void CollectInfo(ICollection<ComponentInfoBuilder> builders)
		{
			foreach (ComponentInfoBuilder componentInfoBuilder in builders)
			{
				if (componentInfoBuilder.IsAcceptable(this.<componentMethod>P))
				{
					this._infos[componentInfoBuilder.TemplateComponentInfoClass] = componentInfoBuilder.Build(this.<componentMethod>P, this);
				}
			}
		}

		// Token: 0x06008A3F RID: 35391 RVA: 0x001316B8 File Offset: 0x0012F8B8
		private static Type GetComponentType(MethodInfo componentMethod)
		{
			Type returnType = componentMethod.ReturnType;
			if (!typeof(Component).IsAssignableFrom(returnType))
			{
				throw new WrongComponentTypeException(returnType);
			}
			return returnType;
		}

		// Token: 0x04005E67 RID: 24167
		[CompilerGenerated]
		private MethodInfo <componentMethod>P = componentMethod;

		// Token: 0x04005E68 RID: 24168
		private readonly IDictionary<Type, ComponentInfo> _infos = new Dictionary<Type, ComponentInfo>();
	}
}
