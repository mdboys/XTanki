using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002891 RID: 10385
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityNode
	{
		// Token: 0x06008B4C RID: 35660 RVA: 0x0005384D File Offset: 0x00051A4D
		public void Clear()
		{
			this.NextArgumentEntityNodes.Clear();
		}

		// Token: 0x06008B4D RID: 35661 RVA: 0x0005385A File Offset: 0x00051A5A
		public EntityNode Init(ArgumentNode argumentNode, Entity entity)
		{
			this.Entity = entity;
			this.ArgumentNode = argumentNode;
			this.NextArgumentEntityNodes.Clear();
			this.InvokeArgument = null;
			return this;
		}

		// Token: 0x06008B4E RID: 35662 RVA: 0x0013305C File Offset: 0x0013125C
		public void PrepareInvokeArgument()
		{
			NodeClassInstanceDescription classInstanceDescription = this.ArgumentNode.Argument.ClassInstanceDescription;
			this.InvokeArgument = ((EntityInternal)this.Entity).GetNode(classInstanceDescription);
		}

		// Token: 0x06008B4F RID: 35663 RVA: 0x00133094 File Offset: 0x00131294
		public void ConvertToOptional()
		{
			MethodInfo method = this.ArgumentNode.Argument.ArgumentType.GetMethod("NullableOf");
			this.InvokeArgument = method.Invoke(null, new object[] { this.InvokeArgument });
		}

		// Token: 0x04005EB6 RID: 24246
		public ArgumentNode ArgumentNode;

		// Token: 0x04005EB7 RID: 24247
		public Entity Entity;

		// Token: 0x04005EB8 RID: 24248
		public object InvokeArgument;

		// Token: 0x04005EB9 RID: 24249
		public readonly List<EntityNode> NextArgumentEntityNodes = new List<EntityNode>();
	}
}
