using System;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002893 RID: 10387
	public class EntityNotFoundException : Exception
	{
		// Token: 0x06008B52 RID: 35666 RVA: 0x00053890 File Offset: 0x00051A90
		public EntityNotFoundException(long entityId)
			: base(string.Format("entityId = {0}", entityId))
		{
		}
	}
}
