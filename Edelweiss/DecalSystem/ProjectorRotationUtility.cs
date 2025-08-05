using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B0F RID: 11023
	public class ProjectorRotationUtility
	{
		// Token: 0x060098C3 RID: 39107 RVA: 0x0005B33A File Offset: 0x0005953A
		public static Quaternion ProjectorRotation(Vector3 a_ProjectionDirection, Vector3 a_ProjectionUpDirection)
		{
			return Quaternion.LookRotation(a_ProjectionDirection, a_ProjectionUpDirection) * ProjectorRotationUtility.s_RotationOffset;
		}

		// Token: 0x04006437 RID: 25655
		private static readonly Quaternion s_RotationOffset = Quaternion.Euler(-90f, 0f, 0f);
	}
}
