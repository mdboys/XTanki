using System;
using UnityEngine;

namespace Assets.platform.library.ClientResources.Scripts.API
{
	// Token: 0x02002CD1 RID: 11473
	public static class DiskCaching
	{
		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06009FF2 RID: 40946 RVA: 0x0005CDC9 File Offset: 0x0005AFC9
		// (set) Token: 0x06009FF3 RID: 40947 RVA: 0x0005CDD0 File Offset: 0x0005AFD0
		public static bool Enabled { get; set; } = Caching.enabled;

		// Token: 0x1700189F RID: 6303
		// (set) Token: 0x06009FF4 RID: 40948 RVA: 0x0005CDD8 File Offset: 0x0005AFD8
		public static long MaximumAvailableDiskSpace
		{
			set
			{
				Caching.maximumAvailableDiskSpace = value;
			}
		}

		// Token: 0x170018A0 RID: 6304
		// (set) Token: 0x06009FF5 RID: 40949 RVA: 0x0005CDE0 File Offset: 0x0005AFE0
		public static int ExpirationDelay
		{
			set
			{
				Caching.expirationDelay = value;
			}
		}

		// Token: 0x170018A1 RID: 6305
		// (set) Token: 0x06009FF6 RID: 40950 RVA: 0x0005CDE8 File Offset: 0x0005AFE8
		public static bool CompressionEnambled
		{
			set
			{
				Caching.compressionEnabled = value;
			}
		}
	}
}
