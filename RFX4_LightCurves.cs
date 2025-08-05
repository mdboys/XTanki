using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000067 RID: 103
[NullableContext(1)]
[Nullable(0)]
public class RFX4_LightCurves : MonoBehaviour
{
	// Token: 0x060001F3 RID: 499 RVA: 0x0000677D File Offset: 0x0000497D
	private void Awake()
	{
		this.lightSource = base.GetComponent<Light>();
		this.lightSource.intensity = this.LightCurve.Evaluate(0f);
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00064CF0 File Offset: 0x00062EF0
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float num2 = this.LightCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			this.lightSource.intensity = num2;
			this.lightSource.shadows = (this.UseShadowsIfPossible ? LightShadows.Soft : LightShadows.None);
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

	// Token: 0x060001F5 RID: 501 RVA: 0x000067A6 File Offset: 0x000049A6
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x04000155 RID: 341
	public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000156 RID: 342
	public float GraphTimeMultiplier = 1f;

	// Token: 0x04000157 RID: 343
	public float GraphIntensityMultiplier = 1f;

	// Token: 0x04000158 RID: 344
	public bool IsLoop;

	// Token: 0x04000159 RID: 345
	public bool UseShadowsIfPossible;

	// Token: 0x0400015A RID: 346
	[HideInInspector]
	public bool canUpdate;

	// Token: 0x0400015B RID: 347
	private Light lightSource;

	// Token: 0x0400015C RID: 348
	private float startTime;
}
