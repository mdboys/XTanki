using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200285C RID: 10332
	public class CanNotRemoveGroupComponentFromRootGroupEntityForNotEmptyGroupException : Exception
	{
		// Token: 0x06008A29 RID: 35369 RVA: 0x00052B13 File Offset: 0x00050D13
		[NullableContext(1)]
		public CanNotRemoveGroupComponentFromRootGroupEntityForNotEmptyGroupException(Type groupClass, Entity entity)
			: base(string.Format("group={0} entity={1}", groupClass.FullName, entity))
		{
		}
	}
}
