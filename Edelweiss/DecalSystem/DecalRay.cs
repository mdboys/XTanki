using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AF2 RID: 10994
	internal struct DecalRay
	{
		// Token: 0x0600979F RID: 38815 RVA: 0x0005A932 File Offset: 0x00058B32
		public DecalRay(Vector3 a_Origin, Vector3 a_Direction)
		{
			this.origin = a_Origin;
			this.direction = a_Direction;
		}

		// Token: 0x040063B9 RID: 25529
		public Vector3 origin;

		// Token: 0x040063BA RID: 25530
		public Vector3 direction;
	}
}
