using System;
using Tanks.Battle.ClientCore.Impl;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class ColliderTest : MonoBehaviour
{
	// Token: 0x06000053 RID: 83 RVA: 0x0005E650 File Offset: 0x0005C850
	private void Update()
	{
		RaycastHit raycastHit;
		if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
		{
			base.GetComponent<ForceFieldEffect>().DrawWave(raycastHit.point, false);
			Debug.Log("Draw");
		}
	}

	// Token: 0x04000029 RID: 41
	private bool flag;

	// Token: 0x0400002A RID: 42
	private Vector3 hitPoint;
}
