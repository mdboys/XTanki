using System;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200288B RID: 10379
	public class EntityByIdNotFoundException : Exception
	{
		// Token: 0x06008AE6 RID: 35558 RVA: 0x0004E0F3 File Offset: 0x0004C2F3
		public EntityByIdNotFoundException(long id)
			: base(string.Format("id={0}", id))
		{
		}
	}
}
