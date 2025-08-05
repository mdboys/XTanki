using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000035 RID: 53
[NullableContext(1)]
[Nullable(0)]
public class HPBarFillEnd : BarFillEnd
{
	// Token: 0x1700002D RID: 45
	// (set) Token: 0x060000CB RID: 203 RVA: 0x0005F454 File Offset: 0x0005D654
	public override float FillAmount
	{
		set
		{
			base.FillAmount = value;
			this.image.offsetMax = new Vector2(this.image.offsetMax.x, 0f - this.topCurve.Evaluate(value));
			this.image.offsetMin = new Vector2(this.image.offsetMin.x, this.bottomCurve.Evaluate(value));
		}
	}

	// Token: 0x0400006F RID: 111
	[SerializeField]
	private AnimationCurve topCurve;

	// Token: 0x04000070 RID: 112
	[SerializeField]
	private AnimationCurve bottomCurve;
}
