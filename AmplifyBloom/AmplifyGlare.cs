using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CD9 RID: 11481
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public sealed class AmplifyGlare : IAmplifyItem
	{
		// Token: 0x0600A06B RID: 41067 RVA: 0x0015ED8C File Offset: 0x0015CF8C
		public AmplifyGlare()
		{
			this.m_currentGlareIdx = (int)this.m_currentGlareType;
			this.m_cromaticAberrationGrad = new Gradient();
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
			this.m_cromaticAberrationGrad.SetKeys(array, array2);
			this._rtBuffer = new RenderTexture[16];
			this.m_weigthsMat = new Matrix4x4[4];
			this.m_offsetsMat = new Matrix4x4[4];
			this.m_amplifyGlareCache = new AmplifyGlareCache();
			this.m_whiteReference = new Color(0.63f, 0.63f, 0.63f, 0f);
			this.m_aTanFoV = Mathf.Atan(0.3926991f);
			this.m_starDefArr = new StarDefData[]
			{
				new StarDefData(StarLibType.Cross, "Cross", 2, 4, 1f, 0.85f, 0f, 0.5f, -1f, 90f),
				new StarDefData(StarLibType.Cross_Filter, "CrossFilter", 2, 4, 1f, 0.95f, 0f, 0.5f, -1f, 90f),
				new StarDefData(StarLibType.Snow_Cross, "snowCross", 3, 4, 1f, 0.96f, 0.349f, 0.5f, -1f, -1f),
				new StarDefData(StarLibType.Vertical, "Vertical", 1, 4, 1f, 0.96f, 0f, 0f, -1f, -1f),
				new StarDefData(StarLibType.Sunny_Cross, "SunnyCross", 4, 4, 1f, 0.88f, 0f, 0f, 0.95f, 45f)
			};
			this.m_glareDefArr = new GlareDefData[]
			{
				new GlareDefData(StarLibType.Cross, 0f, 0.5f),
				new GlareDefData(StarLibType.Cross_Filter, 0.44f, 0.5f),
				new GlareDefData(StarLibType.Cross_Filter, 1.22f, 1.5f),
				new GlareDefData(StarLibType.Snow_Cross, 0.17f, 0.5f),
				new GlareDefData(StarLibType.Snow_Cross, 0.7f, 1.5f),
				new GlareDefData(StarLibType.Sunny_Cross, 0f, 0.5f),
				new GlareDefData(StarLibType.Sunny_Cross, 0.79f, 1.5f),
				new GlareDefData(StarLibType.Vertical, 1.57f, 0.5f),
				new GlareDefData(StarLibType.Vertical, 0f, 0.5f)
			};
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x0600A06C RID: 41068 RVA: 0x0005D2D2 File Offset: 0x0005B4D2
		// (set) Token: 0x0600A06D RID: 41069 RVA: 0x0005D2DA File Offset: 0x0005B4DA
		public GlareLibType CurrentGlare
		{
			get
			{
				return this.m_currentGlareType;
			}
			set
			{
				if (this.m_currentGlareType != value)
				{
					this.m_currentGlareType = value;
					this.m_currentGlareIdx = (int)value;
					this.m_isDirty = true;
				}
			}
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x0600A06E RID: 41070 RVA: 0x0005D2FA File Offset: 0x0005B4FA
		// (set) Token: 0x0600A06F RID: 41071 RVA: 0x0005D302 File Offset: 0x0005B502
		public int GlareMaxPassCount
		{
			get
			{
				return this.m_glareMaxPassCount;
			}
			set
			{
				this.m_glareMaxPassCount = value;
				this.m_isDirty = true;
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x0600A070 RID: 41072 RVA: 0x0005D312 File Offset: 0x0005B512
		// (set) Token: 0x0600A071 RID: 41073 RVA: 0x0005D31A File Offset: 0x0005B51A
		public float PerPassDisplacement
		{
			get
			{
				return this.m_perPassDisplacement;
			}
			set
			{
				this.m_perPassDisplacement = value;
				this.m_isDirty = true;
			}
		}

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x0600A072 RID: 41074 RVA: 0x0005D32A File Offset: 0x0005B52A
		// (set) Token: 0x0600A073 RID: 41075 RVA: 0x0005D332 File Offset: 0x0005B532
		public float Intensity
		{
			get
			{
				return this.m_intensity;
			}
			set
			{
				this.m_intensity = ((value >= 0f) ? value : 0f);
				this.m_isDirty = true;
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x0600A074 RID: 41076 RVA: 0x0005D351 File Offset: 0x0005B551
		// (set) Token: 0x0600A075 RID: 41077 RVA: 0x0005D359 File Offset: 0x0005B559
		public Color OverallTint
		{
			get
			{
				return this._overallTint;
			}
			set
			{
				this._overallTint = value;
				this.m_isDirty = true;
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x0600A076 RID: 41078 RVA: 0x0005D369 File Offset: 0x0005B569
		// (set) Token: 0x0600A077 RID: 41079 RVA: 0x0005D371 File Offset: 0x0005B571
		public bool ApplyLensGlare
		{
			get
			{
				return this.m_applyGlare;
			}
			set
			{
				this.m_applyGlare = value;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x0600A078 RID: 41080 RVA: 0x0005D37A File Offset: 0x0005B57A
		// (set) Token: 0x0600A079 RID: 41081 RVA: 0x0005D382 File Offset: 0x0005B582
		public Gradient CromaticColorGradient
		{
			get
			{
				return this.m_cromaticAberrationGrad;
			}
			set
			{
				this.m_cromaticAberrationGrad = value;
				this.m_isDirty = true;
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x0600A07A RID: 41082 RVA: 0x0005D392 File Offset: 0x0005B592
		// (set) Token: 0x0600A07B RID: 41083 RVA: 0x0005D39A File Offset: 0x0005B59A
		public float OverallStreakScale
		{
			get
			{
				return this.m_overallStreakScale;
			}
			set
			{
				this.m_overallStreakScale = value;
				this.m_isDirty = true;
			}
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x0600A07C RID: 41084 RVA: 0x0005D3AA File Offset: 0x0005B5AA
		// (set) Token: 0x0600A07D RID: 41085 RVA: 0x0005D3B2 File Offset: 0x0005B5B2
		public GlareDefData[] CustomGlareDef
		{
			get
			{
				return this.m_customGlareDef;
			}
			set
			{
				this.m_customGlareDef = value;
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x0600A07E RID: 41086 RVA: 0x0005D3BB File Offset: 0x0005B5BB
		// (set) Token: 0x0600A07F RID: 41087 RVA: 0x0005D3C3 File Offset: 0x0005B5C3
		public int CustomGlareDefIdx
		{
			get
			{
				return this.m_customGlareDefIdx;
			}
			set
			{
				this.m_customGlareDefIdx = value;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x0600A080 RID: 41088 RVA: 0x0005D3CC File Offset: 0x0005B5CC
		// (set) Token: 0x0600A081 RID: 41089 RVA: 0x0015F0F8 File Offset: 0x0015D2F8
		public int CustomGlareDefAmount
		{
			get
			{
				return this.m_customGlareDefAmount;
			}
			set
			{
				if (value == this.m_customGlareDefAmount)
				{
					return;
				}
				if (value == 0)
				{
					this.m_customGlareDef = null;
					this.m_customGlareDefIdx = 0;
					this.m_customGlareDefAmount = 0;
					return;
				}
				GlareDefData[] array = new GlareDefData[value];
				for (int i = 0; i < value; i++)
				{
					if (i < this.m_customGlareDefAmount)
					{
						array[i] = this.m_customGlareDef[i];
					}
					else
					{
						array[i] = new GlareDefData();
					}
				}
				this.m_customGlareDefIdx = Mathf.Clamp(this.m_customGlareDefIdx, 0, value - 1);
				this.m_customGlareDef = array;
				this.m_customGlareDefAmount = value;
			}
		}

		// Token: 0x0600A082 RID: 41090 RVA: 0x0015F180 File Offset: 0x0015D380
		public void Destroy()
		{
			for (int i = 0; i < this.m_starDefArr.Length; i++)
			{
				this.m_starDefArr[i].Destroy();
			}
			this.m_glareDefArr = null;
			this.m_weigthsMat = null;
			this.m_offsetsMat = null;
			for (int j = 0; j < this._rtBuffer.Length; j++)
			{
				if (this._rtBuffer[j] != null)
				{
					AmplifyUtils.ReleaseTempRenderTarget(this._rtBuffer[j]);
					this._rtBuffer[j] = null;
				}
			}
			this._rtBuffer = null;
			this.m_amplifyGlareCache.Destroy();
			this.m_amplifyGlareCache = null;
		}

		// Token: 0x0600A083 RID: 41091 RVA: 0x0005D3D4 File Offset: 0x0005B5D4
		public void SetDirty()
		{
			this.m_isDirty = true;
		}

		// Token: 0x0600A084 RID: 41092 RVA: 0x0015F214 File Offset: 0x0015D414
		public void OnRenderFromCache(RenderTexture source, RenderTexture dest, Material material, float glareIntensity, float cameraRotation)
		{
			for (int i = 0; i < this.m_amplifyGlareCache.TotalRT; i++)
			{
				this._rtBuffer[i] = AmplifyUtils.GetTempRenderTarget(source.width, source.height);
			}
			int j = 0;
			for (int k = 0; k < this.m_amplifyGlareCache.StarDef.StarlinesCount; k++)
			{
				for (int l = 0; l < this.m_amplifyGlareCache.CurrentPassCount; l++)
				{
					this.UpdateMatrixesForPass(material, this.m_amplifyGlareCache.Starlines[k].Passes[l].Offsets, this.m_amplifyGlareCache.Starlines[k].Passes[l].Weights, glareIntensity, cameraRotation * this.m_amplifyGlareCache.StarDef.CameraRotInfluence);
					if (l == 0)
					{
						Graphics.Blit(source, this._rtBuffer[j], material, 2);
					}
					else
					{
						Graphics.Blit(this._rtBuffer[j - 1], this._rtBuffer[j], material, 2);
					}
					j++;
				}
			}
			for (int m = 0; m < this.m_amplifyGlareCache.StarDef.StarlinesCount; m++)
			{
				material.SetVector(AmplifyUtils.AnamorphicGlareWeightsStr[m], this.m_amplifyGlareCache.AverageWeight);
				int num = (m + 1) * this.m_amplifyGlareCache.CurrentPassCount - 1;
				material.SetTexture(AmplifyUtils.AnamorphicRTS[m], this._rtBuffer[num]);
			}
			int num2 = 19 + this.m_amplifyGlareCache.StarDef.StarlinesCount - 1;
			dest.DiscardContents();
			Graphics.Blit(this._rtBuffer[0], dest, material, num2);
			for (j = 0; j < this._rtBuffer.Length; j++)
			{
				AmplifyUtils.ReleaseTempRenderTarget(this._rtBuffer[j]);
				this._rtBuffer[j] = null;
			}
		}

		// Token: 0x0600A085 RID: 41093 RVA: 0x0015F3D0 File Offset: 0x0015D5D0
		public void UpdateMatrixesForPass(Material material, Vector4[] offsets, Vector4[] weights, float glareIntensity, float rotation)
		{
			float num = Mathf.Cos(rotation);
			float num2 = Mathf.Sin(rotation);
			for (int i = 0; i < 16; i++)
			{
				int num3 = i >> 2;
				int num4 = i & 3;
				this.m_offsetsMat[num3][num4, 0] = offsets[i].x * num - offsets[i].y * num2;
				this.m_offsetsMat[num3][num4, 1] = offsets[i].x * num2 + offsets[i].y * num;
				this.m_weigthsMat[num3][num4, 0] = glareIntensity * weights[i].x;
				this.m_weigthsMat[num3][num4, 1] = glareIntensity * weights[i].y;
				this.m_weigthsMat[num3][num4, 2] = glareIntensity * weights[i].z;
			}
			for (int j = 0; j < 4; j++)
			{
				material.SetMatrix(AmplifyUtils.AnamorphicGlareOffsetsMatStr[j], this.m_offsetsMat[j]);
				material.SetMatrix(AmplifyUtils.AnamorphicGlareWeightsMatStr[j], this.m_weigthsMat[j]);
			}
		}

		// Token: 0x0600A086 RID: 41094 RVA: 0x0015F51C File Offset: 0x0015D71C
		public void OnRenderImage(Material material, RenderTexture source, RenderTexture dest, float cameraRot)
		{
			Graphics.Blit(Texture2D.blackTexture, dest);
			if (this.m_isDirty || this.m_currentWidth != source.width || this.m_currentHeight != source.height)
			{
				this.m_isDirty = false;
				this.m_currentWidth = source.width;
				this.m_currentHeight = source.height;
				bool flag = false;
				GlareDefData glareDefData;
				if (this.m_currentGlareType == GlareLibType.Custom)
				{
					GlareDefData[] customGlareDef = this.m_customGlareDef;
					if (customGlareDef != null && customGlareDef.Length > 0)
					{
						glareDefData = this.m_customGlareDef[this.m_customGlareDefIdx];
						flag = true;
					}
					else
					{
						glareDefData = this.m_glareDefArr[0];
					}
				}
				else
				{
					glareDefData = this.m_glareDefArr[this.m_currentGlareIdx];
				}
				this.m_amplifyGlareCache.GlareDef = glareDefData;
				float num = (float)source.width;
				float num2 = (float)source.height;
				StarDefData starDefData = ((!flag) ? this.m_starDefArr[(int)glareDefData.StarType] : glareDefData.CustomStarData);
				this.m_amplifyGlareCache.StarDef = starDefData;
				int num3 = ((this.m_glareMaxPassCount >= starDefData.PassCount) ? starDefData.PassCount : this.m_glareMaxPassCount);
				this.m_amplifyGlareCache.CurrentPassCount = num3;
				float num4 = glareDefData.StarInclination + starDefData.Inclination;
				for (int i = 0; i < this.m_glareMaxPassCount; i++)
				{
					float num5 = (float)(i + 1) / (float)this.m_glareMaxPassCount;
					for (int j = 0; j < 8; j++)
					{
						Color color = this._overallTint * Color.Lerp(this.m_cromaticAberrationGrad.Evaluate((float)j / 7f), this.m_whiteReference, num5);
						this.m_amplifyGlareCache.CromaticAberrationMat[i, j] = Color.Lerp(this.m_whiteReference, color, glareDefData.ChromaticAberration);
					}
				}
				this.m_amplifyGlareCache.TotalRT = starDefData.StarlinesCount * num3;
				for (int k = 0; k < this.m_amplifyGlareCache.TotalRT; k++)
				{
					this._rtBuffer[k] = AmplifyUtils.GetTempRenderTarget(source.width, source.height);
				}
				int l = 0;
				for (int m = 0; m < starDefData.StarlinesCount; m++)
				{
					StarLineData starLineData = starDefData.StarLinesArr[m];
					float num6 = num4 + starLineData.Inclination;
					float num7 = Mathf.Sin(num6);
					float num8 = Mathf.Cos(num6);
					Vector2 vector = default(Vector2);
					vector.x = num8 / num * (starLineData.SampleLength * this.m_overallStreakScale);
					vector.y = num7 / num2 * (starLineData.SampleLength * this.m_overallStreakScale);
					float num9 = (this.m_aTanFoV + 0.1f) * 280f / (num + num2) * 1.2f;
					for (int n = 0; n < num3; n++)
					{
						for (int num10 = 0; num10 < 8; num10++)
						{
							float num11 = Mathf.Pow(starLineData.Attenuation, num9 * (float)num10);
							this.m_amplifyGlareCache.Starlines[m].Passes[n].Weights[num10] = this.m_amplifyGlareCache.CromaticAberrationMat[num3 - 1 - n, num10] * num11 * ((float)n + 1f) * 0.5f;
							this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets[num10].x = vector.x * (float)num10;
							this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets[num10].y = vector.y * (float)num10;
							if (Mathf.Abs(this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets[num10].x) >= 0.9f || Mathf.Abs(this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets[num10].y) >= 0.9f)
							{
								this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets[num10].x = 0f;
								this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets[num10].y = 0f;
								this.m_amplifyGlareCache.Starlines[m].Passes[n].Weights[num10] *= 0f;
							}
						}
						for (int num12 = 8; num12 < 16; num12++)
						{
							this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets[num12] = -this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets[num12 - 8];
							this.m_amplifyGlareCache.Starlines[m].Passes[n].Weights[num12] = this.m_amplifyGlareCache.Starlines[m].Passes[n].Weights[num12 - 8];
						}
						this.UpdateMatrixesForPass(material, this.m_amplifyGlareCache.Starlines[m].Passes[n].Offsets, this.m_amplifyGlareCache.Starlines[m].Passes[n].Weights, this.m_intensity, starDefData.CameraRotInfluence * cameraRot);
						if (n == 0)
						{
							Graphics.Blit(source, this._rtBuffer[l], material, 2);
						}
						else
						{
							Graphics.Blit(this._rtBuffer[l - 1], this._rtBuffer[l], material, 2);
						}
						l++;
						vector *= this.m_perPassDisplacement;
						num9 *= this.m_perPassDisplacement;
					}
				}
				this.m_amplifyGlareCache.AverageWeight = Vector4.one / (float)starDefData.StarlinesCount;
				for (int num13 = 0; num13 < starDefData.StarlinesCount; num13++)
				{
					material.SetVector(AmplifyUtils.AnamorphicGlareWeightsStr[num13], this.m_amplifyGlareCache.AverageWeight);
					int num14 = (num13 + 1) * num3 - 1;
					material.SetTexture(AmplifyUtils.AnamorphicRTS[num13], this._rtBuffer[num14]);
				}
				int num15 = 19 + starDefData.StarlinesCount - 1;
				dest.DiscardContents();
				Graphics.Blit(this._rtBuffer[0], dest, material, num15);
				for (l = 0; l < this._rtBuffer.Length; l++)
				{
					AmplifyUtils.ReleaseTempRenderTarget(this._rtBuffer[l]);
					this._rtBuffer[l] = null;
				}
				return;
			}
			this.OnRenderFromCache(source, dest, material, this.m_intensity, cameraRot);
		}

		// Token: 0x040067AE RID: 26542
		public const int MaxLineSamples = 8;

		// Token: 0x040067AF RID: 26543
		public const int MaxTotalSamples = 16;

		// Token: 0x040067B0 RID: 26544
		public const int MaxStarLines = 4;

		// Token: 0x040067B1 RID: 26545
		public const int MaxPasses = 4;

		// Token: 0x040067B2 RID: 26546
		public const int MaxCustomGlare = 32;

		// Token: 0x040067B3 RID: 26547
		[SerializeField]
		private GlareDefData[] m_customGlareDef;

		// Token: 0x040067B4 RID: 26548
		[SerializeField]
		private int m_customGlareDefIdx;

		// Token: 0x040067B5 RID: 26549
		[SerializeField]
		private int m_customGlareDefAmount;

		// Token: 0x040067B6 RID: 26550
		[SerializeField]
		private bool m_applyGlare = true;

		// Token: 0x040067B7 RID: 26551
		[SerializeField]
		private Color _overallTint = Color.white;

		// Token: 0x040067B8 RID: 26552
		[SerializeField]
		private Gradient m_cromaticAberrationGrad;

		// Token: 0x040067B9 RID: 26553
		[SerializeField]
		private int m_glareMaxPassCount = 4;

		// Token: 0x040067BA RID: 26554
		[SerializeField]
		private int m_currentWidth;

		// Token: 0x040067BB RID: 26555
		[SerializeField]
		private int m_currentHeight;

		// Token: 0x040067BC RID: 26556
		[SerializeField]
		private GlareLibType m_currentGlareType;

		// Token: 0x040067BD RID: 26557
		[SerializeField]
		private int m_currentGlareIdx;

		// Token: 0x040067BE RID: 26558
		[SerializeField]
		private float m_perPassDisplacement = 4f;

		// Token: 0x040067BF RID: 26559
		[SerializeField]
		private float m_intensity = 0.17f;

		// Token: 0x040067C0 RID: 26560
		[SerializeField]
		private float m_overallStreakScale = 1f;

		// Token: 0x040067C1 RID: 26561
		private RenderTexture[] _rtBuffer;

		// Token: 0x040067C2 RID: 26562
		private AmplifyGlareCache m_amplifyGlareCache;

		// Token: 0x040067C3 RID: 26563
		private float m_aTanFoV;

		// Token: 0x040067C4 RID: 26564
		private GlareDefData[] m_glareDefArr;

		// Token: 0x040067C5 RID: 26565
		private bool m_isDirty = true;

		// Token: 0x040067C6 RID: 26566
		private Matrix4x4[] m_offsetsMat;

		// Token: 0x040067C7 RID: 26567
		private StarDefData[] m_starDefArr;

		// Token: 0x040067C8 RID: 26568
		private Matrix4x4[] m_weigthsMat;

		// Token: 0x040067C9 RID: 26569
		private Color m_whiteReference;
	}
}
