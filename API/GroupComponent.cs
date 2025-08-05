using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200296C RID: 10604
	[NullableContext(1)]
	[Nullable(0)]
	[SkipAutoRemove]
	public class GroupComponent : Component, AttachToEntityListener, DetachFromEntityListener, EntityListener
	{
		// Token: 0x06008E9B RID: 36507 RVA: 0x000558A9 File Offset: 0x00053AA9
		public GroupComponent(long key)
		{
		}

		// Token: 0x06008E9C RID: 36508 RVA: 0x000558C3 File Offset: 0x00053AC3
		protected GroupComponent(Entity keyEntity)
			: this(keyEntity.Id)
		{
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06008E9D RID: 36509 RVA: 0x000558D1 File Offset: 0x00053AD1
		public long Key { get; } = key;

		// Token: 0x06008E9E RID: 36510 RVA: 0x00137564 File Offset: 0x00135764
		public void AttachedToEntity(Entity entity)
		{
			EntityInternal entityInternal = (EntityInternal)entity;
			entityInternal.AddEntityListener(this);
			foreach (NodeDescription nodeDescription in entityInternal.NodeDescriptionStorage.GetNodeDescriptions())
			{
				this._nodeCollector.Attach(entityInternal, nodeDescription);
			}
		}

		// Token: 0x06008E9F RID: 36511 RVA: 0x001375CC File Offset: 0x001357CC
		public void DetachedFromEntity(Entity entity)
		{
			EntityInternal entityInternal = (EntityInternal)entity;
			this.OnRemoveMemberWithoutRemovingListener(entityInternal);
			entityInternal.RemoveEntityListener(this);
		}

		// Token: 0x06008EA0 RID: 36512 RVA: 0x000558D9 File Offset: 0x00053AD9
		public void OnNodeAdded(Entity entity, NodeDescription nodeDescription)
		{
			this._nodeCollector.Attach(entity, nodeDescription);
		}

		// Token: 0x06008EA1 RID: 36513 RVA: 0x000558E8 File Offset: 0x00053AE8
		public void OnNodeRemoved(Entity entity, NodeDescription nodeDescription)
		{
			this._nodeCollector.Detach(entity, nodeDescription);
		}

		// Token: 0x06008EA2 RID: 36514 RVA: 0x000558F7 File Offset: 0x00053AF7
		public void OnEntityDeleted(Entity entity)
		{
			this.OnRemoveMemberWithoutRemovingListener((EntityInternal)entity);
		}

		// Token: 0x06008EA3 RID: 36515 RVA: 0x00055905 File Offset: 0x00053B05
		public GroupComponent Attach(Entity entity)
		{
			entity.AddComponent(this);
			return this;
		}

		// Token: 0x06008EA4 RID: 36516 RVA: 0x0005590F File Offset: 0x00053B0F
		public void Detach(Entity entity)
		{
			entity.RemoveComponent(base.GetType());
		}

		// Token: 0x06008EA5 RID: 36517 RVA: 0x0005591D File Offset: 0x00053B1D
		public ICollection<Entity> GetGroupMembers(NodeDescription nodeDescription)
		{
			return this._nodeCollector.GetEntities(nodeDescription);
		}

		// Token: 0x06008EA6 RID: 36518 RVA: 0x0005592B File Offset: 0x00053B2B
		public override string ToString()
		{
			return string.Format("{0}[key={1}]", base.GetType().Name, this.Key);
		}

		// Token: 0x06008EA7 RID: 36519 RVA: 0x001375F0 File Offset: 0x001357F0
		private void OnRemoveMemberWithoutRemovingListener(EntityInternal member)
		{
			foreach (NodeDescription nodeDescription in member.NodeDescriptionStorage.GetNodeDescriptions())
			{
				this._nodeCollector.Detach(member, nodeDescription);
			}
		}

		// Token: 0x04005FDF RID: 24543
		private readonly NodeCollector _nodeCollector = new GroupNodeCollectorImpl();
	}
}
