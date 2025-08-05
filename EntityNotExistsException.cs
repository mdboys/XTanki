using System;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002892 RID: 10386
	public class EntityNotExistsException : Exception
	{
		// Token: 0x06008B51 RID: 35665 RVA: 0x0004E0F3 File Offset: 0x0004C2F3
		public EntityNotExistsException(long id)
			: base(string.Format("id={0}", id))
		{
		}
	}
}
