using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class PPUFix : MonoBehaviour
{
	// Token: 0x060001E8 RID: 488 RVA: 0x00006711 File Offset: 0x00004911
	private void Start()
	{
		this.canvas = base.GetComponent<Canvas>();
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00064C7C File Offset: 0x00062E7C
	private void Update()
	{
		float num = 100f / this.canvas.scaleFactor;
		if (!Mathf.Approximately(num, this.prevPPU))
		{
			this.prevPPU = num;
			this.canvas.referencePixelsPerUnit = num;
		}
	}

	// Token: 0x0400014F RID: 335
	[Nullable(1)]
	private Canvas canvas;

	// Token: 0x04000150 RID: 336
	private float prevPPU;
}
