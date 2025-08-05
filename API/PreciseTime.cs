using System;
using UnityEngine;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200297D RID: 10621
	public class PreciseTime
	{
		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06008EC8 RID: 36552 RVA: 0x000559DB File Offset: 0x00053BDB
		public static double Time
		{
			get
			{
				return (double)global::UnityEngine.Time.timeSinceLevelLoad;
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06008EC9 RID: 36553 RVA: 0x000559E3 File Offset: 0x00053BE3
		// (set) Token: 0x06008ECA RID: 36554 RVA: 0x000559EA File Offset: 0x00053BEA
		public static TimeType TimeType { get; private set; }

		// Token: 0x06008ECB RID: 36555 RVA: 0x000559F2 File Offset: 0x00053BF2
		internal static void Update(float deltaTime)
		{
			PreciseTime.TimeType = TimeType.UPDATE;
		}

		// Token: 0x06008ECC RID: 36556 RVA: 0x000559FA File Offset: 0x00053BFA
		internal static void FixedUpdate(float fixedDeltaTime)
		{
			PreciseTime.TimeType = TimeType.FIXED;
		}

		// Token: 0x06008ECD RID: 36557 RVA: 0x00055A02 File Offset: 0x00053C02
		internal static void AfterFixedUpdate()
		{
			PreciseTime.TimeType = TimeType.LAST_FIXED;
		}
	}
}
