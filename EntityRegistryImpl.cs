using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002895 RID: 10389
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityRegistryImpl : EntityRegistry
	{
		// Token: 0x06008B58 RID: 35672 RVA: 0x000538A8 File Offset: 0x00051AA8
		public ICollection<Entity> GetAllEntities()
		{
			return this._entities.Values;
		}

		// Token: 0x06008B59 RID: 35673 RVA: 0x000538B5 File Offset: 0x00051AB5
		public void Remove(long id)
		{
			if (!this._entities.Remove(id))
			{
				throw new EntityByIdNotFoundException(id);
			}
		}

		// Token: 0x06008B5A RID: 35674 RVA: 0x000538CC File Offset: 0x00051ACC
		public bool ContainsEntity(long id)
		{
			return this._entities.ContainsKey(id);
		}

		// Token: 0x06008B5B RID: 35675 RVA: 0x001330D8 File Offset: 0x001312D8
		public void RegisterEntity(Entity entity)
		{
			try
			{
				this._entities.Add(entity.Id, entity);
			}
			catch (ArgumentException)
			{
				throw new EntityAlreadyRegisteredException(entity);
			}
		}

		// Token: 0x06008B5C RID: 35676 RVA: 0x00133114 File Offset: 0x00131314
		public Entity GetEntity(long id)
		{
			Entity entity;
			try
			{
				entity = this._entities[id];
			}
			catch (KeyNotFoundException)
			{
				throw new EntityByIdNotFoundException(id);
			}
			return entity;
		}

		// Token: 0x04005EBA RID: 24250
		private readonly IDictionary<long, Entity> _entities = new Dictionary<long, Entity>();
	}
}
