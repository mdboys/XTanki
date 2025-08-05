using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class GearRotateComponent : MonoBehaviour
{
	// Token: 0x060000C1 RID: 193 RVA: 0x00005B7D File Offset: 0x00003D7D
	private void Update()
	{
		base.transform.Rotate(Vector3.back, this.angle);
	}

	// Token: 0x0400006C RID: 108
	public float angle = 1f;
}
