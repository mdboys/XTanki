using System;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200296D RID: 10605
	public class InternalPreciseTime
	{
		// Token: 0x06008EA8 RID: 36520 RVA: 0x0005594D File Offset: 0x00053B4D
		public static void Update(float deltaTime)
		{
			PreciseTime.Update(deltaTime);
		}

		// Token: 0x06008EA9 RID: 36521 RVA: 0x00055955 File Offset: 0x00053B55
		public static void FixedUpdate(float fixedDeltaTime)
		{
			PreciseTime.FixedUpdate(fixedDeltaTime);
		}

		// Token: 0x06008EAA RID: 36522 RVA: 0x0005595D File Offset: 0x00053B5D
		public static void AfterFixedUpdate()
		{
			PreciseTime.AfterFixedUpdate();
		}
	}
}
