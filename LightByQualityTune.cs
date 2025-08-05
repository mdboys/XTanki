using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
[RequireComponent(typeof(Light))]
public class LightByQualityTune : MonoBehaviour
{
	// Token: 0x060000DC RID: 220 RVA: 0x0005F920 File Offset: 0x0005DB20
	private void Start()
	{
		int qualityLevel = QualitySettings.GetQualityLevel();
		if (qualityLevel <= this.disableQualityLevel)
		{
			base.gameObject.SetActive(false);
		}
		if (qualityLevel <= this.lowQualityLevel)
		{
			Light component = base.GetComponent<Light>();
			component.intensity = this.lowQualityIntensity;
			component.color = this.lowQualityColor;
		}
	}

	// Token: 0x04000082 RID: 130
	public float lowQualityIntensity = 1f;

	// Token: 0x04000083 RID: 131
	public Color lowQualityColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04000084 RID: 132
	public int lowQualityLevel = 1;

	// Token: 0x04000085 RID: 133
	public int disableQualityLevel = -1;
}
