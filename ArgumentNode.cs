using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Kernel.OSGi.ClientCore.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200284A RID: 10314
	[NullableContext(1)]
	[Nullable(0)]
	public class ArgumentNode
	{
		// Token: 0x060089E8 RID: 35304 RVA: 0x00052858 File Offset: 0x00050A58
		public ArgumentNode(HandlerArgument argument)
		{
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x060089E9 RID: 35305 RVA: 0x00052872 File Offset: 0x00050A72
		// (set) Token: 0x060089EA RID: 35306 RVA: 0x00052879 File Offset: 0x00050A79
		[Inject]
		private static FlowInstancesCache Cache { get; set; }

		// Token: 0x060089EB RID: 35307 RVA: 0x00052881 File Offset: 0x00050A81
		public ArgumentNode Init()
		{
			this.Clear();
			return this;
		}

		// Token: 0x060089EC RID: 35308 RVA: 0x0005288A File Offset: 0x00050A8A
		public void Clear()
		{
			this.EntityNodes.Clear();
			this.Filled = false;
			this.LinkBreak = this.Argument.SelectAll || this.Argument.Collection;
		}

		// Token: 0x060089ED RID: 35309 RVA: 0x000528BF File Offset: 0x00050ABF
		private bool IsEmpty()
		{
			return this.EntityNodes.Count == 0;
		}

		// Token: 0x060089EE RID: 35310 RVA: 0x00130D34 File Offset: 0x0012EF34
		public void ConvertToCollection()
		{
			IList collection = this.GetCollection();
			this.EntityNodes.Clear();
			EntityNode entityNode = ArgumentNode.Cache.entityNode.GetInstance().Init(this, null);
			entityNode.InvokeArgument = collection;
			this.EntityNodes.Add(entityNode);
		}

		// Token: 0x060089EF RID: 35311 RVA: 0x00130D80 File Offset: 0x0012EF80
		private IList GetCollection()
		{
			IList genericListInstance = ArgumentNode.Cache.GetGenericListInstance(this.Argument.ClassInstanceDescription.NodeClass, this.EntityNodes.Count);
			foreach (EntityNode entityNode in this.EntityNodes)
			{
				genericListInstance.Add(entityNode.InvokeArgument);
			}
			return genericListInstance;
		}

		// Token: 0x060089F0 RID: 35312 RVA: 0x00130E00 File Offset: 0x0012F000
		public void ConvertToOptional()
		{
			if (this.IsEmpty())
			{
				this.LinkBreak = true;
				EntityNode entityNode = ArgumentNode.Cache.entityNode.GetInstance().Init(this, null);
				entityNode.ConvertToOptional();
				this.EntityNodes.Add(entityNode);
				return;
			}
			foreach (EntityNode entityNode2 in this.EntityNodes)
			{
				entityNode2.ConvertToOptional();
			}
		}

		// Token: 0x060089F1 RID: 35313 RVA: 0x00130E8C File Offset: 0x0012F08C
		public bool TryGetEntityNode(Entity entity, [Nullable(2)] out EntityNode entityNode)
		{
			NodeClassInstanceDescription classInstanceDescription = this.Argument.ClassInstanceDescription;
			entityNode = null;
			if (!((EntityInternal)entity).CanCast(classInstanceDescription.NodeDescription))
			{
				return false;
			}
			entityNode = ArgumentNode.Cache.entityNode.GetInstance().Init(this, entity);
			return true;
		}

		// Token: 0x060089F2 RID: 35314 RVA: 0x00130ED8 File Offset: 0x0012F0D8
		public void PrepareInvokeArguments()
		{
			foreach (EntityNode entityNode in this.EntityNodes)
			{
				entityNode.PrepareInvokeArgument();
			}
		}

		// Token: 0x04005E4C RID: 24140
		public readonly HandlerArgument Argument = argument;

		// Token: 0x04005E4D RID: 24141
		public readonly List<EntityNode> EntityNodes = new List<EntityNode>();

		// Token: 0x04005E4E RID: 24142
		public bool Filled;

		// Token: 0x04005E4F RID: 24143
		public bool LinkBreak;
	}
}
