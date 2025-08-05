using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002874 RID: 10356
	[NullableContext(1)]
	[Nullable(0)]
	public class ConvertEntityToNodeException : Exception
	{
		// Token: 0x06008A67 RID: 35431 RVA: 0x00052DDE File Offset: 0x00050FDE
		public ConvertEntityToNodeException(NodeDescription nodeDescription, Entity entity)
			: base(string.Format("nodeDescription = {0}, entity = {1}", nodeDescription, entity))
		{
		}

		// Token: 0x06008A68 RID: 35432 RVA: 0x00052DF2 File Offset: 0x00050FF2
		public ConvertEntityToNodeException(Type nodeClass, Entity entity, Exception e)
			: base(string.Format("nodeClass = {0}, entity = {1}", nodeClass, entity), e)
		{
		}
	}
}
