using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class RandomOffset : MonoBehaviour
{
	// Token: 0x060001F0 RID: 496 RVA: 0x00006736 File Offset: 0x00004936
	private void OnEnable()
	{
		base.GetComponent<Animator>().SetFloat(RandomOffset.Offset, global::UnityEngine.Random.Range(this.min, this.max));
	}

	// Token: 0x04000152 RID: 338
	private static readonly int Offset = Animator.StringToHash("offset");

	// Token: 0x04000153 RID: 339
	[SerializeField]
	private float min;

	// Token: 0x04000154 RID: 340
	[SerializeField]
	private float max = 1f;
}
