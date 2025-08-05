using System;
using System.Collections;
using System.Runtime.CompilerServices;
using MeshBrush;
using UnityEngine;

// Token: 0x0200002B RID: 43
[NullableContext(1)]
[Nullable(0)]
public class Example_Runtime : MonoBehaviour
{
	// Token: 0x060000B1 RID: 177 RVA: 0x0005F0D0 File Offset: 0x0005D2D0
	private void Start()
	{
		base.StartCoroutine(this.PaintExampleCubes());
		if (!base.GetComponent<RuntimeAPI>())
		{
			base.gameObject.AddComponent<RuntimeAPI>();
		}
		this.mb = base.GetComponent<RuntimeAPI>();
		for (int i = 0; i < this.exampleCubes.Length; i++)
		{
			if (this.exampleCubes[i] == null)
			{
				Debug.LogError("One or more GameObjects in the set of meshes to paint are unassigned.");
			}
		}
		this.mb.brushRadius = 10f;
		this.mb.amount = 7;
		this.mb.delayBetweenPaintStrokes = 0.2f;
		this.mb.randomScale = new Vector4(0.4f, 1.4f, 0.5f, 1.5f);
		this.mb.randomRotation = 100f;
		this.mb.meshOffset = 1.5f;
		this.mb.scattering = 75f;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00005B12 File Offset: 0x00003D12
	private IEnumerator PaintExampleCubes()
	{
		Example_Runtime.<PaintExampleCubes>d__5 <PaintExampleCubes>d__ = new Example_Runtime.<PaintExampleCubes>d__5(0);
		<PaintExampleCubes>d__.<>4__this = this;
		return <PaintExampleCubes>d__;
	}

	// Token: 0x04000061 RID: 97
	public GameObject[] exampleCubes = new GameObject[2];

	// Token: 0x04000062 RID: 98
	private RaycastHit hit;

	// Token: 0x04000063 RID: 99
	private RuntimeAPI mb;

	// Token: 0x04000064 RID: 100
	private Ray paintRay;
}
