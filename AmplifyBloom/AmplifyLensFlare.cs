using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CDB RID: 11483
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class AmplifyLensFlare : IAmplifyItem
	{
		// Token: 0x0600A089 RID: 41097 RVA: 0x0015FC64 File Offset: 0x0015DE64
		public AmplifyLensFlare()
		{
			this.m_lensGradient = new Gradient();
			GradientColorKey[] array = new GradientColorKey[]
			{
				new GradientColorKey(Color.white, 0f),
				new GradientColorKey(Color.blue, 0.25f),
				new GradientColorKey(Color.green, 0.5f),
				new GradientColorKey(Color.yellow, 0.75f),
				new GradientColorKey(Color.red, 1f)
			};
			GradientAlphaKey[] array2 = new GradientAlphaKey[]
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 0.25f),
				new GradientAlphaKey(1f, 0.5f),
				new GradientAlphaKey(1f, 0.75f),
				new GradientAlphaKey(1f, 1f)
			};
			this.m_lensGradient.SetKeys(array, array2);
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x0600A08A RID: 41098 RVA: 0x0005D3DD File Offset: 0x0005B5DD
		// (set) Token: 0x0600A08B RID: 41099 RVA: 0x0005D3E5 File Offset: 0x0005B5E5
		public bool ApplyLensFlare
		{
			get
			{
				return this.m_applyLensFlare;
			}
			set
			{
				this.m_applyLensFlare = value;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x0600A08C RID: 41100 RVA: 0x0005D3EE File Offset: 0x0005B5EE
		// (set) Token: 0x0600A08D RID: 41101 RVA: 0x0005D3F6 File Offset: 0x0005B5F6
		public float OverallIntensity
		{
			get
			{
				return this.m_overallIntensity;
			}
			set
			{
				this.m_overallIntensity = ((value >= 0f) ? value : 0f);
				this.m_lensFlareGhostsParams.x = value * this.m_normalizedGhostIntensity;
				this.m_lensFlareHaloParams.x = value * this.m_normalizedHaloIntensity;
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x0600A08E RID: 41102 RVA: 0x0005D434 File Offset: 0x0005B634
		// (set) Token: 0x0600A08F RID: 41103 RVA: 0x0005D43C File Offset: 0x0005B63C
		public int LensFlareGhostAmount
		{
			get
			{
				return this.m_lensFlareGhostAmount;
			}
			set
			{
				this.m_lensFlareGhostAmount = value;
			}
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x0600A090 RID: 41104 RVA: 0x0005D445 File Offset: 0x0005B645
		// (set) Token: 0x0600A091 RID: 41105 RVA: 0x0005D44D File Offset: 0x0005B64D
		public Vector4 LensFlareGhostsParams
		{
			get
			{
				return this.m_lensFlareGhostsParams;
			}
			set
			{
				this.m_lensFlareGhostsParams = value;
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x0600A092 RID: 41106 RVA: 0x0005D456 File Offset: 0x0005B656
		// (set) Token: 0x0600A093 RID: 41107 RVA: 0x0005D45E File Offset: 0x0005B65E
		public float LensFlareNormalizedGhostsIntensity
		{
			get
			{
				return this.m_normalizedGhostIntensity;
			}
			set
			{
				this.m_normalizedGhostIntensity = ((value >= 0f) ? value : 0f);
				this.m_lensFlareGhostsParams.x = this.m_overallIntensity * this.m_normalizedGhostIntensity;
			}
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x0600A094 RID: 41108 RVA: 0x0005D48E File Offset: 0x0005B68E
		// (set) Token: 0x0600A095 RID: 41109 RVA: 0x0005D49B File Offset: 0x0005B69B
		public float LensFlareGhostsIntensity
		{
			get
			{
				return this.m_lensFlareGhostsParams.x;
			}
			set
			{
				this.m_lensFlareGhostsParams.x = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x0600A096 RID: 41110 RVA: 0x0005D4B8 File Offset: 0x0005B6B8
		// (set) Token: 0x0600A097 RID: 41111 RVA: 0x0005D4C5 File Offset: 0x0005B6C5
		public float LensFlareGhostsDispersal
		{
			get
			{
				return this.m_lensFlareGhostsParams.y;
			}
			set
			{
				this.m_lensFlareGhostsParams.y = value;
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x0600A098 RID: 41112 RVA: 0x0005D4D3 File Offset: 0x0005B6D3
		// (set) Token: 0x0600A099 RID: 41113 RVA: 0x0005D4E0 File Offset: 0x0005B6E0
		public float LensFlareGhostsPowerFactor
		{
			get
			{
				return this.m_lensFlareGhostsParams.z;
			}
			set
			{
				this.m_lensFlareGhostsParams.z = value;
			}
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x0600A09A RID: 41114 RVA: 0x0005D4EE File Offset: 0x0005B6EE
		// (set) Token: 0x0600A09B RID: 41115 RVA: 0x0005D4FB File Offset: 0x0005B6FB
		public float LensFlareGhostsPowerFalloff
		{
			get
			{
				return this.m_lensFlareGhostsParams.w;
			}
			set
			{
				this.m_lensFlareGhostsParams.w = value;
			}
		}

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x0600A09C RID: 41116 RVA: 0x0005D509 File Offset: 0x0005B709
		// (set) Token: 0x0600A09D RID: 41117 RVA: 0x0005D511 File Offset: 0x0005B711
		public Gradient LensFlareGradient
		{
			get
			{
				return this.m_lensGradient;
			}
			set
			{
				this.m_lensGradient = value;
			}
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x0600A09E RID: 41118 RVA: 0x0005D51A File Offset: 0x0005B71A
		// (set) Token: 0x0600A09F RID: 41119 RVA: 0x0005D522 File Offset: 0x0005B722
		public Vector4 LensFlareHaloParams
		{
			get
			{
				return this.m_lensFlareHaloParams;
			}
			set
			{
				this.m_lensFlareHaloParams = value;
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x0600A0A0 RID: 41120 RVA: 0x0005D52B File Offset: 0x0005B72B
		// (set) Token: 0x0600A0A1 RID: 41121 RVA: 0x0005D533 File Offset: 0x0005B733
		public float LensFlareNormalizedHaloIntensity
		{
			get
			{
				return this.m_normalizedHaloIntensity;
			}
			set
			{
				this.m_normalizedHaloIntensity = ((value >= 0f) ? value : 0f);
				this.m_lensFlareHaloParams.x = this.m_overallIntensity * this.m_normalizedHaloIntensity;
			}
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x0600A0A2 RID: 41122 RVA: 0x0005D563 File Offset: 0x0005B763
		// (set) Token: 0x0600A0A3 RID: 41123 RVA: 0x0005D570 File Offset: 0x0005B770
		public float LensFlareHaloIntensity
		{
			get
			{
				return this.m_lensFlareHaloParams.x;
			}
			set
			{
				this.m_lensFlareHaloParams.x = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x0600A0A4 RID: 41124 RVA: 0x0005D58D File Offset: 0x0005B78D
		// (set) Token: 0x0600A0A5 RID: 41125 RVA: 0x0005D59A File Offset: 0x0005B79A
		public float LensFlareHaloWidth
		{
			get
			{
				return this.m_lensFlareHaloParams.y;
			}
			set
			{
				this.m_lensFlareHaloParams.y = value;
			}
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x0600A0A6 RID: 41126 RVA: 0x0005D5A8 File Offset: 0x0005B7A8
		// (set) Token: 0x0600A0A7 RID: 41127 RVA: 0x0005D5B5 File Offset: 0x0005B7B5
		public float LensFlareHaloPowerFactor
		{
			get
			{
				return this.m_lensFlareHaloParams.z;
			}
			set
			{
				this.m_lensFlareHaloParams.z = value;
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x0600A0A8 RID: 41128 RVA: 0x0005D5C3 File Offset: 0x0005B7C3
		// (set) Token: 0x0600A0A9 RID: 41129 RVA: 0x0005D5D0 File Offset: 0x0005B7D0
		public float LensFlareHaloPowerFalloff
		{
			get
			{
				return this.m_lensFlareHaloParams.w;
			}
			set
			{
				this.m_lensFlareHaloParams.w = value;
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x0600A0AA RID: 41130 RVA: 0x0005D5DE File Offset: 0x0005B7DE
		// (set) Token: 0x0600A0AB RID: 41131 RVA: 0x0005D5E6 File Offset: 0x0005B7E6
		public float LensFlareGhostChrDistortion
		{
			get
			{
				return this.m_lensFlareGhostChrDistortion;
			}
			set
			{
				this.m_lensFlareGhostChrDistortion = value;
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x0600A0AC RID: 41132 RVA: 0x0005D5EF File Offset: 0x0005B7EF
		// (set) Token: 0x0600A0AD RID: 41133 RVA: 0x0005D5F7 File Offset: 0x0005B7F7
		public float LensFlareHaloChrDistortion
		{
			get
			{
				return this.m_lensFlareHaloChrDistortion;
			}
			set
			{
				this.m_lensFlareHaloChrDistortion = value;
			}
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x0600A0AE RID: 41134 RVA: 0x0005D600 File Offset: 0x0005B800
		// (set) Token: 0x0600A0AF RID: 41135 RVA: 0x0005D608 File Offset: 0x0005B808
		public int LensFlareGaussianBlurAmount
		{
			get
			{
				return this.m_lensFlareGaussianBlurAmount;
			}
			set
			{
				this.m_lensFlareGaussianBlurAmount = value;
			}
		}

		// Token: 0x0600A0B0 RID: 41136 RVA: 0x0005D611 File Offset: 0x0005B811
		public void Destroy()
		{
			if (this.m_lensFlareGradTexture != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_lensFlareGradTexture);
				this.m_lensFlareGradTexture = null;
			}
		}

		// Token: 0x0600A0B1 RID: 41137 RVA: 0x0005D633 File Offset: 0x0005B833
		public void CreateLUTexture()
		{
			this.m_lensFlareGradTexture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
			{
				filterMode = FilterMode.Bilinear
			};
			this.TextureFromGradient();
		}

		// Token: 0x0600A0B2 RID: 41138 RVA: 0x0015FE14 File Offset: 0x0015E014
		public RenderTexture ApplyFlare(Material material, RenderTexture source)
		{
			RenderTexture tempRenderTarget = AmplifyUtils.GetTempRenderTarget(source.width, source.height);
			material.SetVector(AmplifyUtils.LensFlareGhostsParamsId, this.m_lensFlareGhostsParams);
			material.SetTexture(AmplifyUtils.LensFlareLUTId, this.m_lensFlareGradTexture);
			material.SetVector(AmplifyUtils.LensFlareHaloParamsId, this.m_lensFlareHaloParams);
			material.SetFloat(AmplifyUtils.LensFlareGhostChrDistortionId, this.m_lensFlareGhostChrDistortion);
			material.SetFloat(AmplifyUtils.LensFlareHaloChrDistortionId, this.m_lensFlareHaloChrDistortion);
			Graphics.Blit(source, tempRenderTarget, material, 3 + this.m_lensFlareGhostAmount);
			return tempRenderTarget;
		}

		// Token: 0x0600A0B3 RID: 41139 RVA: 0x0015FE9C File Offset: 0x0015E09C
		public void TextureFromGradient()
		{
			for (int i = 0; i < 256; i++)
			{
				this.m_lensFlareGradColor[i] = this.m_lensGradient.Evaluate((float)i / 255f);
			}
			this.m_lensFlareGradTexture.SetPixels(this.m_lensFlareGradColor);
			this.m_lensFlareGradTexture.Apply();
		}

		// Token: 0x040067D1 RID: 26577
		private const int LUTTextureWidth = 256;

		// Token: 0x040067D2 RID: 26578
		[SerializeField]
		private float m_overallIntensity = 1f;

		// Token: 0x040067D3 RID: 26579
		[SerializeField]
		private float m_normalizedGhostIntensity = 0.8f;

		// Token: 0x040067D4 RID: 26580
		[SerializeField]
		private float m_normalizedHaloIntensity = 0.1f;

		// Token: 0x040067D5 RID: 26581
		[SerializeField]
		private bool m_applyLensFlare = true;

		// Token: 0x040067D6 RID: 26582
		[SerializeField]
		private int m_lensFlareGhostAmount = 3;

		// Token: 0x040067D7 RID: 26583
		[SerializeField]
		private Vector4 m_lensFlareGhostsParams = new Vector4(0.8f, 0.228f, 1f, 4f);

		// Token: 0x040067D8 RID: 26584
		[SerializeField]
		private float m_lensFlareGhostChrDistortion = 2f;

		// Token: 0x040067D9 RID: 26585
		[SerializeField]
		private Gradient m_lensGradient;

		// Token: 0x040067DA RID: 26586
		[SerializeField]
		private Texture2D m_lensFlareGradTexture;

		// Token: 0x040067DB RID: 26587
		[SerializeField]
		private Vector4 m_lensFlareHaloParams = new Vector4(0.1f, 0.573f, 1f, 128f);

		// Token: 0x040067DC RID: 26588
		[SerializeField]
		private float m_lensFlareHaloChrDistortion = 1.51f;

		// Token: 0x040067DD RID: 26589
		[SerializeField]
		private int m_lensFlareGaussianBlurAmount = 1;

		// Token: 0x040067DE RID: 26590
		private Color[] m_lensFlareGradColor = new Color[256];
	}
}
