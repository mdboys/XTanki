using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[RequireComponent(typeof(Camera))]
public class EnableCameraDepthInForward : MonoBehaviour
{
	// Token: 0x060000AA RID: 170 RVA: 0x00005AC0 File Offset: 0x00003CC0
	private void Start()
	{
		this.Set();
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00005AC8 File Offset: 0x00003CC8
	private void Set()
	{
		if (base.GetComponent<Camera>().depthTextureMode == DepthTextureMode.None)
		{
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
		}
	}
}
