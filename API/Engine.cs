using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200295A RID: 10586
	[NullableContext(1)]
	public interface Engine
	{
		// Token: 0x06008E27 RID: 36391
		Entity CreateEntity(string name);

		// Token: 0x06008E28 RID: 36392
		Entity CreateEntity<[Nullable(0)] T>() where T : Template;

		// Token: 0x06008E29 RID: 36393
		Entity CreateEntity<[Nullable(0)] T>(string configPath) where T : Template;

		// Token: 0x06008E2A RID: 36394
		Entity CreateEntity(Type templateType, string configPath);

		// Token: 0x06008E2B RID: 36395
		Entity CreateEntity<[Nullable(0)] T>(string configPath, long id) where T : Template;

		// Token: 0x06008E2C RID: 36396
		Entity CreateEntity(long templateId, string configPath, long id);

		// Token: 0x06008E2D RID: 36397
		Entity CreateEntity(long templateId, string configPath);

		// Token: 0x06008E2E RID: 36398
		void DeleteEntity(Entity entity);

		// Token: 0x06008E2F RID: 36399
		EventBuilder NewEvent(Event eventInstance);

		// Token: 0x06008E30 RID: 36400
		EventBuilder NewEvent<[Nullable(0)] T>() where T : Event, new();

		// Token: 0x06008E31 RID: 36401
		[NullableContext(0)]
		void ScheduleEvent<T>() where T : Event, new();

		// Token: 0x06008E32 RID: 36402
		void ScheduleEvent<[Nullable(0)] T>(Entity entity) where T : Event, new();

		// Token: 0x06008E33 RID: 36403
		void ScheduleEvent<[Nullable(0)] T>(Node node) where T : Event, new();

		// Token: 0x06008E34 RID: 36404
		void ScheduleEvent(Event eventInstance, Entity entity);

		// Token: 0x06008E35 RID: 36405
		IList<T> Select<[Nullable(0)] T>(Entity entity, Type groupComponentType) where T : Node;

		// Token: 0x06008E36 RID: 36406
		ICollection<T> SelectAll<[Nullable(0)] T>() where T : Node;

		// Token: 0x06008E37 RID: 36407
		ICollection<Entity> SelectAllEntities<[Nullable(0)] T>() where T : Node;
	}
}
