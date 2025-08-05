using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CD7 RID: 11479
	[Serializable]
	public sealed class AmplifyBokeh : IAmplifyItem, ISerializationCallbackReceiver
	{
		// Token: 0x0600A04E RID: 41038 RVA: 0x0015E9A8 File Offset: 0x0015CBA8
		public AmplifyBokeh()
		{
			this.m_bokehOffsets = new List<AmplifyBokehData>();
			this.CreateBokehOffsets(ApertureShape.Hexagon);
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x0600A04F RID: 41039 RVA: 0x0005D1AA File Offset: 0x0005B3AA
		// (set) Token: 0x0600A050 RID: 41040 RVA: 0x0005D1B2 File Offset: 0x0005B3B2
		public ApertureShape ApertureShape
		{
			get
			{
				return this.m_apertureShape;
			}
			set
			{
				if (this.m_apertureShape != value)
				{
					this.m_apertureShape = value;
					this.CreateBokehOffsets(value);
				}
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x0600A051 RID: 41041 RVA: 0x0005D1CB File Offset: 0x0005B3CB
		// (set) Token: 0x0600A052 RID: 41042 RVA: 0x0005D1D3 File Offset: 0x0005B3D3
		public bool ApplyBokeh
		{
			get
			{
				return this.m_isActive;
			}
			set
			{
				this.m_isActive = value;
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x0600A053 RID: 41043 RVA: 0x0005D1DC File Offset: 0x0005B3DC
		// (set) Token: 0x0600A054 RID: 41044 RVA: 0x0005D1E4 File Offset: 0x0005B3E4
		public bool ApplyOnBloomSource
		{
			get
			{
				return this.m_applyOnBloomSource;
			}
			set
			{
				this.m_applyOnBloomSource = value;
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x0600A055 RID: 41045 RVA: 0x0005D1ED File Offset: 0x0005B3ED
		// (set) Token: 0x0600A056 RID: 41046 RVA: 0x0005D1F5 File Offset: 0x0005B3F5
		public float BokehSampleRadius
		{
			get
			{
				return this.m_bokehSampleRadius;
			}
			set
			{
				this.m_bokehSampleRadius = value;
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x0600A057 RID: 41047 RVA: 0x0005D1FE File Offset: 0x0005B3FE
		// (set) Token: 0x0600A058 RID: 41048 RVA: 0x0005D206 File Offset: 0x0005B406
		public float OffsetRotation
		{
			get
			{
				return this.m_offsetRotation;
			}
			set
			{
				this.m_offsetRotation = value;
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x0600A059 RID: 41049 RVA: 0x0005D20F File Offset: 0x0005B40F
		// (set) Token: 0x0600A05A RID: 41050 RVA: 0x0005D217 File Offset: 0x0005B417
		public Vector4 BokehCameraProperties
		{
			get
			{
				return this.m_bokehCameraProperties;
			}
			set
			{
				this.m_bokehCameraProperties = value;
			}
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x0600A05B RID: 41051 RVA: 0x0005D220 File Offset: 0x0005B420
		// (set) Token: 0x0600A05C RID: 41052 RVA: 0x0005D22D File Offset: 0x0005B42D
		public float Aperture
		{
			get
			{
				return this.m_bokehCameraProperties.x;
			}
			set
			{
				this.m_bokehCameraProperties.x = value;
			}
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x0600A05D RID: 41053 RVA: 0x0005D23B File Offset: 0x0005B43B
		// (set) Token: 0x0600A05E RID: 41054 RVA: 0x0005D248 File Offset: 0x0005B448
		public float FocalLength
		{
			get
			{
				return this.m_bokehCameraProperties.y;
			}
			set
			{
				this.m_bokehCameraProperties.y = value;
			}
		}

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x0600A05F RID: 41055 RVA: 0x0005D256 File Offset: 0x0005B456
		// (set) Token: 0x0600A060 RID: 41056 RVA: 0x0005D263 File Offset: 0x0005B463
		public float FocalDistance
		{
			get
			{
				return this.m_bokehCameraProperties.z;
			}
			set
			{
				this.m_bokehCameraProperties.z = value;
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x0600A061 RID: 41057 RVA: 0x0005D271 File Offset: 0x0005B471
		// (set) Token: 0x0600A062 RID: 41058 RVA: 0x0005D27E File Offset: 0x0005B47E
		public float MaxCoCDiameter
		{
			get
			{
				return this.m_bokehCameraProperties.w;
			}
			set
			{
				this.m_bokehCameraProperties.w = value;
			}
		}

		// Token: 0x0600A063 RID: 41059 RVA: 0x0015EA00 File Offset: 0x0015CC00
		public void Destroy()
		{
			for (int i = 0; i < this.m_bokehOffsets.Count; i++)
			{
				this.m_bokehOffsets[i].Destroy();
			}
		}

		// Token: 0x0600A064 RID: 41060 RVA: 0x0005D28C File Offset: 0x0005B48C
		public void OnAfterDeserialize()
		{
			this.CreateBokehOffsets(this.m_apertureShape);
		}

		// Token: 0x0600A065 RID: 41061 RVA: 0x0000568E File Offset: 0x0000388E
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x0600A066 RID: 41062 RVA: 0x0015EA34 File Offset: 0x0015CC34
		private void CreateBokehOffsets(ApertureShape shape)
		{
			this.m_bokehOffsets.Clear();
			switch (shape)
			{
			case ApertureShape.Square:
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation)));
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation + 90f)));
				return;
			case ApertureShape.Hexagon:
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation)));
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation - 75f)));
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation + 75f)));
				return;
			case ApertureShape.Octagon:
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation)));
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation + 65f)));
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation + 90f)));
				this.m_bokehOffsets.Add(new AmplifyBokehData(this.CalculateBokehSamples(8, this.m_offsetRotation + 115f)));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A067 RID: 41063 RVA: 0x0015EB8C File Offset: 0x0015CD8C
		[NullableContext(1)]
		private Vector4[] CalculateBokehSamples(int sampleCount, float angle)
		{
			Vector4[] array = new Vector4[sampleCount];
			float num = 0.017453292f * angle;
			float num2 = (float)Screen.width / (float)Screen.height;
			Vector4 vector = new Vector4(this.m_bokehSampleRadius * Mathf.Cos(num), this.m_bokehSampleRadius * Mathf.Sin(num));
			vector.x /= num2;
			for (int i = 0; i < sampleCount; i++)
			{
				float num3 = (float)i / ((float)sampleCount - 1f);
				array[i] = Vector4.Lerp(-vector, vector, num3);
			}
			return array;
		}

		// Token: 0x0600A068 RID: 41064 RVA: 0x0015EC20 File Offset: 0x0015CE20
		[NullableContext(1)]
		public void ApplyBokehFilter(RenderTexture source, Material material)
		{
			for (int i = 0; i < this.m_bokehOffsets.Count; i++)
			{
				this.m_bokehOffsets[i].BokehRenderTexture = AmplifyUtils.GetTempRenderTarget(source.width, source.height);
			}
			material.SetVector(AmplifyUtils.BokehParamsId, this.m_bokehCameraProperties);
			for (int j = 0; j < this.m_bokehOffsets.Count; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					material.SetVector(AmplifyUtils.AnamorphicGlareWeightsStr[k], this.m_bokehOffsets[j].Offsets[k]);
				}
				Graphics.Blit(source, this.m_bokehOffsets[j].BokehRenderTexture, material, 27);
			}
			for (int l = 0; l < this.m_bokehOffsets.Count - 1; l++)
			{
				material.SetTexture(AmplifyUtils.AnamorphicRTS[l], this.m_bokehOffsets[l].BokehRenderTexture);
			}
			source.DiscardContents();
			Graphics.Blit(this.m_bokehOffsets[this.m_bokehOffsets.Count - 1].BokehRenderTexture, source, material, 28 + (this.m_bokehOffsets.Count - 2));
			for (int m = 0; m < this.m_bokehOffsets.Count; m++)
			{
				AmplifyUtils.ReleaseTempRenderTarget(this.m_bokehOffsets[m].BokehRenderTexture);
				this.m_bokehOffsets[m].BokehRenderTexture = null;
			}
		}

		// Token: 0x040067A4 RID: 26532
		private const int PerPassSampleCount = 8;

		// Token: 0x040067A5 RID: 26533
		[SerializeField]
		private bool m_isActive;

		// Token: 0x040067A6 RID: 26534
		[SerializeField]
		private bool m_applyOnBloomSource;

		// Token: 0x040067A7 RID: 26535
		[SerializeField]
		private float m_bokehSampleRadius = 0.5f;

		// Token: 0x040067A8 RID: 26536
		[SerializeField]
		private Vector4 m_bokehCameraProperties = new Vector4(0.05f, 0.018f, 1.34f, 0.18f);

		// Token: 0x040067A9 RID: 26537
		[SerializeField]
		private float m_offsetRotation;

		// Token: 0x040067AA RID: 26538
		[SerializeField]
		private ApertureShape m_apertureShape = ApertureShape.Hexagon;

		// Token: 0x040067AB RID: 26539
		[Nullable(1)]
		private List<AmplifyBokehData> m_bokehOffsets;
	}
}
