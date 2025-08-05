using System;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002961 RID: 10593
	[NullableContext(1)]
	public interface EntityStateMachine
	{
		// Token: 0x06008E73 RID: 36467
		[NullableContext(0)]
		void AddState<T>() where T : Node, new();

		// Token: 0x06008E74 RID: 36468
		T ChangeState<[Nullable(0)] T>() where T : Node;

		// Token: 0x06008E75 RID: 36469
		Node ChangeState(Type t);

		// Token: 0x06008E76 RID: 36470
		void AttachToEntity(Entity entity);
	}
}
