using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000040 RID: 64
[NullableContext(1)]
[Nullable(0)]
public class ME_LightCurves : MonoBehaviour
{
	// Token: 0x060000F5 RID: 245 RVA: 0x00005D2C File Offset: 0x00003F2C
	private void Awake()
	{
		this.lightSource = base.GetComponent<Light>();
		this.lightSource.intensity = this.LightCurve.Evaluate(0f);
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x0005F96C File Offset: 0x0005DB6C
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float num2 = this.LightCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			this.lightSource.intensity = num2;
		}
		if (num >= this.GraphTimeMultiplier)
		{
			if (this.IsLoop)
			{
				this.startTime = Time.time;
				return;
			}
			this.canUpdate = false;
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00005D55 File Offset: 0x00003F55
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x0400008F RID: 143
	public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000090 RID: 144
	public float GraphTimeMultiplier = 1f;

	// Token: 0x04000091 RID: 145
	public float GraphIntensityMultiplier = 1f;

	// Token: 0x04000092 RID: 146
	public bool IsLoop;

	// Token: 0x04000093 RID: 147
	private bool canUpdate;

	// Token: 0x04000094 RID: 148
	private Light lightSource;

	// Token: 0x04000095 RID: 149
	private float startTime;
}
