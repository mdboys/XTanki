using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace AmplifyBloom
{
	// Token: 0x02002CD5 RID: 11477
	[NullableContext(1)]
	[Nullable(0)]
	[AddComponentMenu("")]
	[Serializable]
	public class AmplifyBloomBase : MonoBehaviour
	{
		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06009FFE RID: 40958 RVA: 0x0005CE32 File Offset: 0x0005B032
		public AmplifyGlare LensGlareInstance
		{
			get
			{
				return this.m_anamorphicGlare;
			}
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06009FFF RID: 40959 RVA: 0x0005CE3A File Offset: 0x0005B03A
		public AmplifyBokeh BokehFilterInstance
		{
			get
			{
				return this.m_bokehFilter;
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x0600A000 RID: 40960 RVA: 0x0005CE42 File Offset: 0x0005B042
		public AmplifyLensFlare LensFlareInstance
		{
			get
			{
				return this.m_lensFlare;
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x0600A001 RID: 40961 RVA: 0x0005CE4A File Offset: 0x0005B04A
		// (set) Token: 0x0600A002 RID: 40962 RVA: 0x0005CE52 File Offset: 0x0005B052
		public bool ApplyLensDirt
		{
			get
			{
				return this.m_applyLensDirt;
			}
			set
			{
				this.m_applyLensDirt = value;
			}
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x0600A003 RID: 40963 RVA: 0x0005CE5B File Offset: 0x0005B05B
		// (set) Token: 0x0600A004 RID: 40964 RVA: 0x0005CE63 File Offset: 0x0005B063
		public float LensDirtStrength
		{
			get
			{
				return this.m_lensDirtStrength;
			}
			set
			{
				this.m_lensDirtStrength = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x0600A005 RID: 40965 RVA: 0x0005CE7B File Offset: 0x0005B07B
		// (set) Token: 0x0600A006 RID: 40966 RVA: 0x0005CE83 File Offset: 0x0005B083
		public Texture LensDirtTexture
		{
			get
			{
				return this.m_lensDirtTexture;
			}
			set
			{
				this.m_lensDirtTexture = value;
			}
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x0600A007 RID: 40967 RVA: 0x0005CE8C File Offset: 0x0005B08C
		// (set) Token: 0x0600A008 RID: 40968 RVA: 0x0005CE94 File Offset: 0x0005B094
		public bool ApplyLensStardurst
		{
			get
			{
				return this.m_applyLensStardurst;
			}
			set
			{
				this.m_applyLensStardurst = value;
			}
		}

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x0600A009 RID: 40969 RVA: 0x0005CE9D File Offset: 0x0005B09D
		// (set) Token: 0x0600A00A RID: 40970 RVA: 0x0005CEA5 File Offset: 0x0005B0A5
		public Texture LensStardurstTex
		{
			get
			{
				return this.m_lensStardurstTex;
			}
			set
			{
				this.m_lensStardurstTex = value;
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x0600A00B RID: 40971 RVA: 0x0005CEAE File Offset: 0x0005B0AE
		// (set) Token: 0x0600A00C RID: 40972 RVA: 0x0005CEB6 File Offset: 0x0005B0B6
		public float LensStarburstStrength
		{
			get
			{
				return this.m_lensStarburstStrength;
			}
			set
			{
				this.m_lensStarburstStrength = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x0600A00D RID: 40973 RVA: 0x0005CECE File Offset: 0x0005B0CE
		// (set) Token: 0x0600A00E RID: 40974 RVA: 0x0005CEDB File Offset: 0x0005B0DB
		public PrecisionModes CurrentPrecisionMode
		{
			get
			{
				if (this.m_highPrecision)
				{
					return PrecisionModes.High;
				}
				return PrecisionModes.Low;
			}
			set
			{
				this.HighPrecision = value == PrecisionModes.High;
			}
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x0600A00F RID: 40975 RVA: 0x0005CEE7 File Offset: 0x0005B0E7
		// (set) Token: 0x0600A010 RID: 40976 RVA: 0x0005CEEF File Offset: 0x0005B0EF
		public bool HighPrecision
		{
			get
			{
				return this.m_highPrecision;
			}
			set
			{
				if (this.m_highPrecision != value)
				{
					this.m_highPrecision = value;
					this.CleanTempFilterRT();
				}
			}
		}

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x0600A011 RID: 40977 RVA: 0x0005CF07 File Offset: 0x0005B107
		// (set) Token: 0x0600A012 RID: 40978 RVA: 0x0005CF14 File Offset: 0x0005B114
		public float BloomRange
		{
			get
			{
				return this.m_bloomRange.x;
			}
			set
			{
				this.m_bloomRange.x = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x0600A013 RID: 40979 RVA: 0x0005CF31 File Offset: 0x0005B131
		// (set) Token: 0x0600A014 RID: 40980 RVA: 0x0005CF39 File Offset: 0x0005B139
		public float OverallThreshold
		{
			get
			{
				return this.m_overallThreshold;
			}
			set
			{
				this.m_overallThreshold = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x0600A015 RID: 40981 RVA: 0x0005CF51 File Offset: 0x0005B151
		// (set) Token: 0x0600A016 RID: 40982 RVA: 0x0005CF59 File Offset: 0x0005B159
		public Vector4 BloomParams
		{
			get
			{
				return this.m_bloomParams;
			}
			set
			{
				this.m_bloomParams = value;
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x0600A017 RID: 40983 RVA: 0x0005CF62 File Offset: 0x0005B162
		// (set) Token: 0x0600A018 RID: 40984 RVA: 0x0005CF6F File Offset: 0x0005B16F
		public float OverallIntensity
		{
			get
			{
				return this.m_bloomParams.x;
			}
			set
			{
				this.m_bloomParams.x = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x0600A019 RID: 40985 RVA: 0x0005CF8C File Offset: 0x0005B18C
		// (set) Token: 0x0600A01A RID: 40986 RVA: 0x0005CF99 File Offset: 0x0005B199
		public float BloomScale
		{
			get
			{
				return this.m_bloomParams.w;
			}
			set
			{
				this.m_bloomParams.w = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x0600A01B RID: 40987 RVA: 0x0005CFB6 File Offset: 0x0005B1B6
		// (set) Token: 0x0600A01C RID: 40988 RVA: 0x0005CFC3 File Offset: 0x0005B1C3
		public float UpscaleBlurRadius
		{
			get
			{
				return this.m_bloomParams.z;
			}
			set
			{
				this.m_bloomParams.z = value;
			}
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x0600A01D RID: 40989 RVA: 0x0005CFD1 File Offset: 0x0005B1D1
		// (set) Token: 0x0600A01E RID: 40990 RVA: 0x0005CFD9 File Offset: 0x0005B1D9
		public bool TemporalFilteringActive
		{
			get
			{
				return this.m_temporalFilteringActive;
			}
			set
			{
				if (this.m_temporalFilteringActive != value)
				{
					this.CleanTempFilterRT();
				}
				this.m_temporalFilteringActive = value;
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x0600A01F RID: 40991 RVA: 0x0005CFF1 File Offset: 0x0005B1F1
		// (set) Token: 0x0600A020 RID: 40992 RVA: 0x0005CFF9 File Offset: 0x0005B1F9
		public float TemporalFilteringValue
		{
			get
			{
				return this.m_temporalFilteringValue;
			}
			set
			{
				this.m_temporalFilteringValue = value;
			}
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x0600A021 RID: 40993 RVA: 0x0005D002 File Offset: 0x0005B202
		public int SoftMaxdownscales
		{
			get
			{
				return this.m_softMaxdownscales;
			}
		}

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x0600A022 RID: 40994 RVA: 0x0005D00A File Offset: 0x0005B20A
		// (set) Token: 0x0600A023 RID: 40995 RVA: 0x0005D012 File Offset: 0x0005B212
		public int BloomDownsampleCount
		{
			get
			{
				return this.m_bloomDownsampleCount;
			}
			set
			{
				this.m_bloomDownsampleCount = Mathf.Clamp(value, 1, this.m_softMaxdownscales);
			}
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x0600A024 RID: 40996 RVA: 0x0005D027 File Offset: 0x0005B227
		// (set) Token: 0x0600A025 RID: 40997 RVA: 0x0005D02F File Offset: 0x0005B22F
		public int FeaturesSourceId
		{
			get
			{
				return this.m_featuresSourceId;
			}
			set
			{
				this.m_featuresSourceId = Mathf.Clamp(value, 0, this.m_bloomDownsampleCount - 1);
			}
		}

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x0600A026 RID: 40998 RVA: 0x0005D046 File Offset: 0x0005B246
		public bool[] DownscaleSettingsFoldout
		{
			get
			{
				return this.m_downscaleSettingsFoldout;
			}
		}

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x0600A027 RID: 40999 RVA: 0x0005D04E File Offset: 0x0005B24E
		public float[] UpscaleWeights
		{
			get
			{
				return this.m_upscaleWeights;
			}
		}

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x0600A028 RID: 41000 RVA: 0x0005D056 File Offset: 0x0005B256
		public float[] LensDirtWeights
		{
			get
			{
				return this.m_lensDirtWeights;
			}
		}

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x0600A029 RID: 41001 RVA: 0x0005D05E File Offset: 0x0005B25E
		public float[] LensStarburstWeights
		{
			get
			{
				return this.m_lensStarburstWeights;
			}
		}

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x0600A02A RID: 41002 RVA: 0x0005D066 File Offset: 0x0005B266
		public float[] GaussianRadius
		{
			get
			{
				return this.m_gaussianRadius;
			}
		}

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x0600A02B RID: 41003 RVA: 0x0005D06E File Offset: 0x0005B26E
		public int[] GaussianSteps
		{
			get
			{
				return this.m_gaussianSteps;
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x0600A02C RID: 41004 RVA: 0x0005D076 File Offset: 0x0005B276
		// (set) Token: 0x0600A02D RID: 41005 RVA: 0x0005D07E File Offset: 0x0005B27E
		public AnimationCurve TemporalFilteringCurve
		{
			get
			{
				return this.m_temporalFilteringCurve;
			}
			set
			{
				this.m_temporalFilteringCurve = value;
			}
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x0600A02E RID: 41006 RVA: 0x0005D087 File Offset: 0x0005B287
		// (set) Token: 0x0600A02F RID: 41007 RVA: 0x0005D08F File Offset: 0x0005B28F
		public bool SeparateFeaturesThreshold
		{
			get
			{
				return this.m_separateFeaturesThreshold;
			}
			set
			{
				this.m_separateFeaturesThreshold = value;
			}
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x0600A030 RID: 41008 RVA: 0x0005D098 File Offset: 0x0005B298
		// (set) Token: 0x0600A031 RID: 41009 RVA: 0x0005D0A0 File Offset: 0x0005B2A0
		public float FeaturesThreshold
		{
			get
			{
				return this.m_featuresThreshold;
			}
			set
			{
				this.m_featuresThreshold = ((value >= 0f) ? value : 0f);
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x0600A032 RID: 41010 RVA: 0x0005D0B8 File Offset: 0x0005B2B8
		// (set) Token: 0x0600A033 RID: 41011 RVA: 0x0005D0C0 File Offset: 0x0005B2C0
		public DebugToScreenEnum DebugToScreen
		{
			get
			{
				return this.m_debugToScreen;
			}
			set
			{
				this.m_debugToScreen = value;
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x0600A034 RID: 41012 RVA: 0x0005D0C9 File Offset: 0x0005B2C9
		// (set) Token: 0x0600A035 RID: 41013 RVA: 0x0005D0D1 File Offset: 0x0005B2D1
		public UpscaleQualityEnum UpscaleQuality
		{
			get
			{
				return this.m_upscaleQuality;
			}
			set
			{
				this.m_upscaleQuality = value;
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x0600A036 RID: 41014 RVA: 0x0005D0DA File Offset: 0x0005B2DA
		// (set) Token: 0x0600A037 RID: 41015 RVA: 0x0005D0E2 File Offset: 0x0005B2E2
		public bool ShowDebugMessages
		{
			get
			{
				return this.m_showDebugMessages;
			}
			set
			{
				this.m_showDebugMessages = value;
			}
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x0600A038 RID: 41016 RVA: 0x0005D0EB File Offset: 0x0005B2EB
		// (set) Token: 0x0600A039 RID: 41017 RVA: 0x0005D0F3 File Offset: 0x0005B2F3
		public MainThresholdSizeEnum MainThresholdSize
		{
			get
			{
				return this.m_mainThresholdSize;
			}
			set
			{
				this.m_mainThresholdSize = value;
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x0600A03A RID: 41018 RVA: 0x0005D0FC File Offset: 0x0005B2FC
		// (set) Token: 0x0600A03B RID: 41019 RVA: 0x0005D104 File Offset: 0x0005B304
		public RenderTexture TargetTexture
		{
			get
			{
				return this.m_targetTexture;
			}
			set
			{
				this.m_targetTexture = value;
			}
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x0600A03C RID: 41020 RVA: 0x0005D10D File Offset: 0x0005B30D
		// (set) Token: 0x0600A03D RID: 41021 RVA: 0x0005D115 File Offset: 0x0005B315
		public Texture MaskTexture
		{
			get
			{
				return this.m_maskTexture;
			}
			set
			{
				this.m_maskTexture = value;
			}
		}

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x0600A03E RID: 41022 RVA: 0x0005D11E File Offset: 0x0005B31E
		// (set) Token: 0x0600A03F RID: 41023 RVA: 0x0005D12B File Offset: 0x0005B32B
		public bool ApplyBokehFilter
		{
			get
			{
				return this.m_bokehFilter.ApplyBokeh;
			}
			set
			{
				this.m_bokehFilter.ApplyBokeh = value;
			}
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x0600A040 RID: 41024 RVA: 0x0005D139 File Offset: 0x0005B339
		// (set) Token: 0x0600A041 RID: 41025 RVA: 0x0005D146 File Offset: 0x0005B346
		public bool ApplyLensFlare
		{
			get
			{
				return this.m_lensFlare.ApplyLensFlare;
			}
			set
			{
				this.m_lensFlare.ApplyLensFlare = value;
			}
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x0600A042 RID: 41026 RVA: 0x0005D154 File Offset: 0x0005B354
		// (set) Token: 0x0600A043 RID: 41027 RVA: 0x0005D161 File Offset: 0x0005B361
		public bool ApplyLensGlare
		{
			get
			{
				return this.m_anamorphicGlare.ApplyLensGlare;
			}
			set
			{
				this.m_anamorphicGlare.ApplyLensGlare = value;
			}
		}

		// Token: 0x0600A044 RID: 41028 RVA: 0x0015D7A0 File Offset: 0x0015B9A0
		private void Awake()
		{
			if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
			{
				AmplifyUtils.DebugLog("Null graphics device detected. Skipping effect silently.", LogType.Error);
				this.silentError = true;
				return;
			}
			if (!AmplifyUtils.IsInitialized)
			{
				AmplifyUtils.InitializeIds();
			}
			for (int i = 0; i < 6; i++)
			{
				this.m_tempDownsamplesSizes[i] = new Vector2(0f, 0f);
			}
			this.m_cameraTransform = base.transform;
			this.m_tempFilterBuffer = null;
			this.m_starburstMat = Matrix4x4.identity;
			if (this.m_temporalFilteringCurve == null)
			{
				this.m_temporalFilteringCurve = new AnimationCurve(new Keyframe[]
				{
					new Keyframe(0f, 0f),
					new Keyframe(1f, 0.999f)
				});
			}
			this.m_bloomShader = Shader.Find("Hidden/AmplifyBloom");
			if (this.m_bloomShader != null)
			{
				this.m_bloomMaterial = new Material(this.m_bloomShader)
				{
					hideFlags = HideFlags.DontSave
				};
			}
			else
			{
				AmplifyUtils.DebugLog("Main Bloom shader not found", LogType.Error);
				base.gameObject.SetActive(false);
			}
			this.m_finalCompositionShader = Shader.Find("Hidden/BloomFinal");
			if (this.m_finalCompositionShader != null)
			{
				this.m_finalCompositionMaterial = new Material(this.m_finalCompositionShader);
				if (!this.m_finalCompositionMaterial.GetTag(AmplifyUtils.ShaderModeTag, false).Equals(AmplifyUtils.ShaderModeValue))
				{
					if (this.m_showDebugMessages)
					{
						AmplifyUtils.DebugLog("Amplify Bloom is running on a limited hardware and may lead to a decrease on its visual quality.", LogType.Warning);
					}
				}
				else
				{
					this.m_softMaxdownscales = 6;
				}
				this.m_finalCompositionMaterial.hideFlags = HideFlags.DontSave;
				if (this.m_lensDirtTexture == null)
				{
					this.m_lensDirtTexture = this.m_finalCompositionMaterial.GetTexture(AmplifyUtils.LensDirtRTId);
				}
				if (this.m_lensStardurstTex == null)
				{
					this.m_lensStardurstTex = this.m_finalCompositionMaterial.GetTexture(AmplifyUtils.LensStarburstRTId);
				}
			}
			else
			{
				AmplifyUtils.DebugLog("Bloom Composition shader not found", LogType.Error);
				base.gameObject.SetActive(false);
			}
			this.m_camera = base.GetComponent<Camera>();
			this.m_camera.depthTextureMode |= DepthTextureMode.Depth;
			this.m_lensFlare.CreateLUTexture();
		}

		// Token: 0x0600A045 RID: 41029 RVA: 0x0015D9B8 File Offset: 0x0015BBB8
		private void OnDestroy()
		{
			if (this.m_bokehFilter != null)
			{
				this.m_bokehFilter.Destroy();
				this.m_bokehFilter = null;
			}
			if (this.m_anamorphicGlare != null)
			{
				this.m_anamorphicGlare.Destroy();
				this.m_anamorphicGlare = null;
			}
			if (this.m_lensFlare != null)
			{
				this.m_lensFlare.Destroy();
				this.m_lensFlare = null;
			}
		}

		// Token: 0x0600A046 RID: 41030 RVA: 0x0015DA14 File Offset: 0x0015BC14
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			if (this.silentError)
			{
				return;
			}
			if (!AmplifyUtils.IsInitialized)
			{
				AmplifyUtils.InitializeIds();
			}
			if (this.m_highPrecision)
			{
				AmplifyUtils.EnsureKeywordEnabled(this.m_bloomMaterial, AmplifyUtils.HighPrecisionKeyword, true);
				AmplifyUtils.EnsureKeywordEnabled(this.m_finalCompositionMaterial, AmplifyUtils.HighPrecisionKeyword, true);
				AmplifyUtils.CurrentRTFormat = RenderTextureFormat.DefaultHDR;
			}
			else
			{
				AmplifyUtils.EnsureKeywordEnabled(this.m_bloomMaterial, AmplifyUtils.HighPrecisionKeyword, false);
				AmplifyUtils.EnsureKeywordEnabled(this.m_finalCompositionMaterial, AmplifyUtils.HighPrecisionKeyword, false);
				AmplifyUtils.CurrentRTFormat = RenderTextureFormat.Default;
			}
			float num = Mathf.Acos(Vector3.Dot(this.m_cameraTransform.right, Vector3.right));
			if (Vector3.Cross(this.m_cameraTransform.right, Vector3.right).y > 0f)
			{
				num = 0f - num;
			}
			RenderTexture renderTexture = null;
			RenderTexture renderTexture2 = null;
			if (!this.m_highPrecision)
			{
				this.m_bloomRange.y = 1f / this.m_bloomRange.x;
				this.m_bloomMaterial.SetVector(AmplifyUtils.BloomRangeId, this.m_bloomRange);
				this.m_finalCompositionMaterial.SetVector(AmplifyUtils.BloomRangeId, this.m_bloomRange);
			}
			this.m_bloomParams.y = this.m_overallThreshold;
			this.m_bloomMaterial.SetVector(AmplifyUtils.BloomParamsId, this.m_bloomParams);
			this.m_finalCompositionMaterial.SetVector(AmplifyUtils.BloomParamsId, this.m_bloomParams);
			int num2 = 1;
			MainThresholdSizeEnum mainThresholdSize = this.m_mainThresholdSize;
			if (mainThresholdSize != MainThresholdSizeEnum.Half)
			{
				if (mainThresholdSize == MainThresholdSizeEnum.Quarter)
				{
					num2 = 4;
				}
			}
			else
			{
				num2 = 2;
			}
			RenderTexture tempRenderTarget = AmplifyUtils.GetTempRenderTarget(src.width / num2, src.height / num2);
			if (this.m_maskTexture != null)
			{
				this.m_bloomMaterial.SetTexture(AmplifyUtils.MaskTextureId, this.m_maskTexture);
				Graphics.Blit(src, tempRenderTarget, this.m_bloomMaterial, 1);
			}
			else
			{
				Graphics.Blit(src, tempRenderTarget, this.m_bloomMaterial, 0);
			}
			if (this.m_debugToScreen == DebugToScreenEnum.MainThreshold)
			{
				Graphics.Blit(tempRenderTarget, dest, this.m_bloomMaterial, 33);
				AmplifyUtils.ReleaseAllRT();
				return;
			}
			bool flag = true;
			RenderTexture renderTexture3 = tempRenderTarget;
			if (this.m_bloomDownsampleCount > 0)
			{
				flag = false;
				int num3 = tempRenderTarget.width;
				int num4 = tempRenderTarget.height;
				for (int i = 0; i < this.m_bloomDownsampleCount; i++)
				{
					this.m_tempDownsamplesSizes[i].x = (float)num3;
					this.m_tempDownsamplesSizes[i].y = (float)num4;
					num3 = num3 + 1 >> 1;
					num4 = num4 + 1 >> 1;
					this.m_tempAuxDownsampleRTs[i] = AmplifyUtils.GetTempRenderTarget(num3, num4);
					if (i == 0)
					{
						if (!this.m_temporalFilteringActive || this.m_gaussianSteps[i] != 0)
						{
							if (this.m_upscaleQuality == UpscaleQualityEnum.Realistic)
							{
								Graphics.Blit(renderTexture3, this.m_tempAuxDownsampleRTs[i], this.m_bloomMaterial, 10);
							}
							else
							{
								Graphics.Blit(renderTexture3, this.m_tempAuxDownsampleRTs[i], this.m_bloomMaterial, 11);
							}
						}
						else
						{
							if (this.m_tempFilterBuffer != null && this.m_temporalFilteringActive)
							{
								float num5 = this.m_temporalFilteringCurve.Evaluate(this.m_temporalFilteringValue);
								this.m_bloomMaterial.SetFloat(AmplifyUtils.TempFilterValueId, num5);
								this.m_bloomMaterial.SetTexture(AmplifyUtils.AnamorphicRTS[0], this.m_tempFilterBuffer);
								if (this.m_upscaleQuality == UpscaleQualityEnum.Realistic)
								{
									Graphics.Blit(renderTexture3, this.m_tempAuxDownsampleRTs[i], this.m_bloomMaterial, 12);
								}
								else
								{
									Graphics.Blit(renderTexture3, this.m_tempAuxDownsampleRTs[i], this.m_bloomMaterial, 13);
								}
							}
							else if (this.m_upscaleQuality == UpscaleQualityEnum.Realistic)
							{
								Graphics.Blit(renderTexture3, this.m_tempAuxDownsampleRTs[i], this.m_bloomMaterial, 10);
							}
							else
							{
								Graphics.Blit(renderTexture3, this.m_tempAuxDownsampleRTs[i], this.m_bloomMaterial, 11);
							}
							bool flag2 = false;
							if (this.m_tempFilterBuffer != null)
							{
								if (this.m_tempFilterBuffer.format != this.m_tempAuxDownsampleRTs[i].format || this.m_tempFilterBuffer.width != this.m_tempAuxDownsampleRTs[i].width || this.m_tempFilterBuffer.height != this.m_tempAuxDownsampleRTs[i].height)
								{
									this.CleanTempFilterRT();
									flag2 = true;
								}
							}
							else
							{
								flag2 = true;
							}
							if (flag2)
							{
								this.CreateTempFilterRT(this.m_tempAuxDownsampleRTs[i]);
							}
							this.m_tempFilterBuffer.DiscardContents();
							Graphics.Blit(this.m_tempAuxDownsampleRTs[i], this.m_tempFilterBuffer);
							if (this.m_debugToScreen == DebugToScreenEnum.TemporalFilter)
							{
								Graphics.Blit(this.m_tempAuxDownsampleRTs[i], dest);
								AmplifyUtils.ReleaseAllRT();
								return;
							}
						}
					}
					else
					{
						Graphics.Blit(this.m_tempAuxDownsampleRTs[i - 1], this.m_tempAuxDownsampleRTs[i], this.m_bloomMaterial, 9);
					}
					if (this.m_gaussianSteps[i] > 0)
					{
						this.ApplyGaussianBlur(this.m_tempAuxDownsampleRTs[i], this.m_gaussianSteps[i], this.m_gaussianRadius[i], i == 0);
						if (this.m_temporalFilteringActive && this.m_debugToScreen == DebugToScreenEnum.TemporalFilter)
						{
							Graphics.Blit(this.m_tempAuxDownsampleRTs[i], dest);
							AmplifyUtils.ReleaseAllRT();
							return;
						}
					}
				}
				renderTexture3 = this.m_tempAuxDownsampleRTs[this.m_featuresSourceId];
				AmplifyUtils.ReleaseTempRenderTarget(tempRenderTarget);
			}
			AmplifyBokeh amplifyBokeh = this.m_bokehFilter;
			if (amplifyBokeh != null && amplifyBokeh.ApplyBokeh && amplifyBokeh.ApplyOnBloomSource)
			{
				this.m_bokehFilter.ApplyBokehFilter(renderTexture3, this.m_bloomMaterial);
				if (this.m_debugToScreen == DebugToScreenEnum.BokehFilter)
				{
					Graphics.Blit(renderTexture3, dest);
					AmplifyUtils.ReleaseAllRT();
					return;
				}
			}
			bool flag3 = false;
			RenderTexture renderTexture4;
			if (this.m_separateFeaturesThreshold)
			{
				this.m_bloomParams.y = this.m_featuresThreshold;
				this.m_bloomMaterial.SetVector(AmplifyUtils.BloomParamsId, this.m_bloomParams);
				this.m_finalCompositionMaterial.SetVector(AmplifyUtils.BloomParamsId, this.m_bloomParams);
				renderTexture4 = AmplifyUtils.GetTempRenderTarget(renderTexture3.width, renderTexture3.height);
				flag3 = true;
				Graphics.Blit(renderTexture3, renderTexture4, this.m_bloomMaterial, 0);
				if (this.m_debugToScreen == DebugToScreenEnum.FeaturesThreshold)
				{
					Graphics.Blit(renderTexture4, dest);
					AmplifyUtils.ReleaseAllRT();
					return;
				}
			}
			else
			{
				renderTexture4 = renderTexture3;
			}
			amplifyBokeh = this.m_bokehFilter;
			if (amplifyBokeh != null && amplifyBokeh.ApplyBokeh && !amplifyBokeh.ApplyOnBloomSource)
			{
				if (!flag3)
				{
					flag3 = true;
					renderTexture4 = AmplifyUtils.GetTempRenderTarget(renderTexture3.width, renderTexture3.height);
					Graphics.Blit(renderTexture3, renderTexture4);
				}
				this.m_bokehFilter.ApplyBokehFilter(renderTexture4, this.m_bloomMaterial);
				if (this.m_debugToScreen == DebugToScreenEnum.BokehFilter)
				{
					Graphics.Blit(renderTexture4, dest);
					AmplifyUtils.ReleaseAllRT();
					return;
				}
			}
			if (this.m_lensFlare.ApplyLensFlare && this.m_debugToScreen != DebugToScreenEnum.Bloom)
			{
				renderTexture = this.m_lensFlare.ApplyFlare(this.m_bloomMaterial, renderTexture4);
				this.ApplyGaussianBlur(renderTexture, this.m_lensFlare.LensFlareGaussianBlurAmount, 1f, false);
				if (this.m_debugToScreen == DebugToScreenEnum.LensFlare)
				{
					Graphics.Blit(renderTexture, dest);
					AmplifyUtils.ReleaseAllRT();
					return;
				}
			}
			if (this.m_anamorphicGlare.ApplyLensGlare && this.m_debugToScreen != DebugToScreenEnum.Bloom)
			{
				renderTexture2 = AmplifyUtils.GetTempRenderTarget(renderTexture3.width, renderTexture3.height);
				this.m_anamorphicGlare.OnRenderImage(this.m_bloomMaterial, renderTexture4, renderTexture2, num);
				if (this.m_debugToScreen == DebugToScreenEnum.LensGlare)
				{
					Graphics.Blit(renderTexture2, dest);
					AmplifyUtils.ReleaseAllRT();
					return;
				}
			}
			if (flag3)
			{
				AmplifyUtils.ReleaseTempRenderTarget(renderTexture4);
			}
			if (flag)
			{
				this.ApplyGaussianBlur(renderTexture3, this.m_gaussianSteps[0], this.m_gaussianRadius[0], false);
			}
			if (this.m_bloomDownsampleCount > 0)
			{
				if (this.m_bloomDownsampleCount == 1)
				{
					if (this.m_upscaleQuality == UpscaleQualityEnum.Realistic)
					{
						this.ApplyUpscale();
						this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.MipResultsRTS[0], this.m_tempUpscaleRTs[0]);
					}
					else
					{
						this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.MipResultsRTS[0], this.m_tempAuxDownsampleRTs[0]);
					}
					this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.UpscaleWeightsStr[0], this.m_upscaleWeights[0]);
				}
				else if (this.m_upscaleQuality == UpscaleQualityEnum.Realistic)
				{
					this.ApplyUpscale();
					for (int j = 0; j < this.m_bloomDownsampleCount; j++)
					{
						int num6 = this.m_bloomDownsampleCount - j - 1;
						this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.MipResultsRTS[num6], this.m_tempUpscaleRTs[j]);
						this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.UpscaleWeightsStr[num6], this.m_upscaleWeights[j]);
					}
				}
				else
				{
					for (int k = 0; k < this.m_bloomDownsampleCount; k++)
					{
						int num7 = this.m_bloomDownsampleCount - 1 - k;
						this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.MipResultsRTS[num7], this.m_tempAuxDownsampleRTs[num7]);
						this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.UpscaleWeightsStr[num7], this.m_upscaleWeights[k]);
					}
				}
			}
			else
			{
				this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.MipResultsRTS[0], renderTexture3);
				this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.UpscaleWeightsStr[0], 1f);
			}
			if (this.m_debugToScreen == DebugToScreenEnum.Bloom)
			{
				this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.SourceContributionId, 0f);
				this.FinalComposition(0f, 1f, src, dest, 0);
				return;
			}
			if (this.m_bloomDownsampleCount > 1)
			{
				for (int l = 0; l < this.m_bloomDownsampleCount; l++)
				{
					this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.LensDirtWeightsStr[this.m_bloomDownsampleCount - l - 1], this.m_lensDirtWeights[l]);
					this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.LensStarburstWeightsStr[this.m_bloomDownsampleCount - l - 1], this.m_lensStarburstWeights[l]);
				}
			}
			else
			{
				this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.LensDirtWeightsStr[0], this.m_lensDirtWeights[0]);
				this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.LensStarburstWeightsStr[0], this.m_lensStarburstWeights[0]);
			}
			if (this.m_lensFlare.ApplyLensFlare)
			{
				this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.LensFlareRTId, renderTexture);
			}
			if (this.m_anamorphicGlare.ApplyLensGlare)
			{
				this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.LensGlareRTId, renderTexture2);
			}
			if (this.m_applyLensDirt)
			{
				this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.LensDirtRTId, this.m_lensDirtTexture);
				this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.LensDirtStrengthId, this.m_lensDirtStrength * 1f);
				if (this.m_debugToScreen == DebugToScreenEnum.LensDirt)
				{
					this.FinalComposition(0f, 0f, src, dest, 2);
					return;
				}
			}
			if (this.m_applyLensStardurst)
			{
				this.m_starburstMat[0, 0] = Mathf.Cos(num);
				this.m_starburstMat[0, 1] = 0f - Mathf.Sin(num);
				this.m_starburstMat[1, 0] = Mathf.Sin(num);
				this.m_starburstMat[1, 1] = Mathf.Cos(num);
				this.m_finalCompositionMaterial.SetMatrix(AmplifyUtils.LensFlareStarMatrixId, this.m_starburstMat);
				this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.LensFlareStarburstStrengthId, this.m_lensStarburstStrength * 1f);
				this.m_finalCompositionMaterial.SetTexture(AmplifyUtils.LensStarburstRTId, this.m_lensStardurstTex);
				if (this.m_debugToScreen == DebugToScreenEnum.LensStarburst)
				{
					this.FinalComposition(0f, 0f, src, dest, 1);
					return;
				}
			}
			if (this.m_targetTexture != null)
			{
				this.m_targetTexture.DiscardContents();
				this.FinalComposition(0f, 1f, src, this.m_targetTexture, -1);
				Graphics.Blit(src, dest);
				return;
			}
			this.FinalComposition(1f, 1f, src, dest, -1);
		}

		// Token: 0x0600A047 RID: 41031 RVA: 0x0015E4F8 File Offset: 0x0015C6F8
		private void ApplyGaussianBlur(RenderTexture renderTexture, int amount, float radius = 1f, bool applyTemporal = false)
		{
			if (amount == 0)
			{
				return;
			}
			this.m_bloomMaterial.SetFloat(AmplifyUtils.BlurRadiusId, radius);
			RenderTexture tempRenderTarget = AmplifyUtils.GetTempRenderTarget(renderTexture.width, renderTexture.height);
			for (int i = 0; i < amount; i++)
			{
				tempRenderTarget.DiscardContents();
				Graphics.Blit(renderTexture, tempRenderTarget, this.m_bloomMaterial, 14);
				if (this.m_temporalFilteringActive && applyTemporal && i == amount - 1)
				{
					if (this.m_tempFilterBuffer != null && this.m_temporalFilteringActive)
					{
						float num = this.m_temporalFilteringCurve.Evaluate(this.m_temporalFilteringValue);
						this.m_bloomMaterial.SetFloat(AmplifyUtils.TempFilterValueId, num);
						this.m_bloomMaterial.SetTexture(AmplifyUtils.AnamorphicRTS[0], this.m_tempFilterBuffer);
						renderTexture.DiscardContents();
						Graphics.Blit(tempRenderTarget, renderTexture, this.m_bloomMaterial, 16);
					}
					else
					{
						renderTexture.DiscardContents();
						Graphics.Blit(tempRenderTarget, renderTexture, this.m_bloomMaterial, 15);
					}
					bool flag = false;
					if (this.m_tempFilterBuffer != null)
					{
						if (this.m_tempFilterBuffer.format != renderTexture.format || this.m_tempFilterBuffer.width != renderTexture.width || this.m_tempFilterBuffer.height != renderTexture.height)
						{
							this.CleanTempFilterRT();
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						this.CreateTempFilterRT(renderTexture);
					}
					this.m_tempFilterBuffer.DiscardContents();
					Graphics.Blit(renderTexture, this.m_tempFilterBuffer);
				}
				else
				{
					renderTexture.DiscardContents();
					Graphics.Blit(tempRenderTarget, renderTexture, this.m_bloomMaterial, 15);
				}
			}
			AmplifyUtils.ReleaseTempRenderTarget(tempRenderTarget);
		}

		// Token: 0x0600A048 RID: 41032 RVA: 0x0015E67C File Offset: 0x0015C87C
		private void CreateTempFilterRT(RenderTexture source)
		{
			if (this.m_tempFilterBuffer != null)
			{
				this.CleanTempFilterRT();
			}
			this.m_tempFilterBuffer = new RenderTexture(source.width, source.height, 0, source.format, AmplifyUtils.CurrentReadWriteMode)
			{
				filterMode = AmplifyUtils.CurrentFilterMode,
				wrapMode = AmplifyUtils.CurrentWrapMode
			};
			this.m_tempFilterBuffer.Create();
		}

		// Token: 0x0600A049 RID: 41033 RVA: 0x0005D16F File Offset: 0x0005B36F
		private void CleanTempFilterRT()
		{
			if (this.m_tempFilterBuffer != null)
			{
				RenderTexture.active = null;
				this.m_tempFilterBuffer.Release();
				global::UnityEngine.Object.DestroyImmediate(this.m_tempFilterBuffer);
				this.m_tempFilterBuffer = null;
			}
		}

		// Token: 0x0600A04A RID: 41034 RVA: 0x0015E6E4 File Offset: 0x0015C8E4
		private void FinalComposition(float srcContribution, float upscaleContribution, RenderTexture src, RenderTexture dest, int forcePassId)
		{
			this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.SourceContributionId, srcContribution);
			this.m_finalCompositionMaterial.SetFloat(AmplifyUtils.UpscaleContributionId, upscaleContribution);
			int num = 0;
			if (forcePassId > -1)
			{
				num = forcePassId;
			}
			else
			{
				if (this.LensFlareInstance.ApplyLensFlare)
				{
					num |= 8;
				}
				if (this.LensGlareInstance.ApplyLensGlare)
				{
					num |= 4;
				}
				if (this.m_applyLensDirt)
				{
					num |= 2;
				}
				if (this.m_applyLensStardurst)
				{
					num |= 1;
				}
			}
			num += (this.m_bloomDownsampleCount - 1) * 16;
			Graphics.Blit(src, dest, this.m_finalCompositionMaterial, num);
			AmplifyUtils.ReleaseAllRT();
		}

		// Token: 0x0600A04B RID: 41035 RVA: 0x0015E77C File Offset: 0x0015C97C
		private void ApplyUpscale()
		{
			int num = this.m_bloomDownsampleCount - 1;
			int num2 = 0;
			for (int i = num; i > -1; i--)
			{
				this.m_tempUpscaleRTs[num2] = AmplifyUtils.GetTempRenderTarget((int)this.m_tempDownsamplesSizes[i].x, (int)this.m_tempDownsamplesSizes[i].y);
				if (i == num)
				{
					Graphics.Blit(this.m_tempAuxDownsampleRTs[num], this.m_tempUpscaleRTs[num2], this.m_bloomMaterial, 17);
				}
				else
				{
					this.m_bloomMaterial.SetTexture(AmplifyUtils.AnamorphicRTS[0], this.m_tempUpscaleRTs[num2 - 1]);
					Graphics.Blit(this.m_tempAuxDownsampleRTs[i], this.m_tempUpscaleRTs[num2], this.m_bloomMaterial, 18);
				}
				num2++;
			}
		}

		// Token: 0x04006771 RID: 26481
		public const int MaxGhosts = 5;

		// Token: 0x04006772 RID: 26482
		public const int MinDownscales = 1;

		// Token: 0x04006773 RID: 26483
		public const int MaxDownscales = 6;

		// Token: 0x04006774 RID: 26484
		public const int MaxGaussian = 8;

		// Token: 0x04006775 RID: 26485
		private const float MaxDirtIntensity = 1f;

		// Token: 0x04006776 RID: 26486
		private const float MaxStarburstIntensity = 1f;

		// Token: 0x04006777 RID: 26487
		[SerializeField]
		private Texture m_maskTexture;

		// Token: 0x04006778 RID: 26488
		[SerializeField]
		private RenderTexture m_targetTexture;

		// Token: 0x04006779 RID: 26489
		[SerializeField]
		private bool m_showDebugMessages = true;

		// Token: 0x0400677A RID: 26490
		[SerializeField]
		private int m_softMaxdownscales = 6;

		// Token: 0x0400677B RID: 26491
		[SerializeField]
		private DebugToScreenEnum m_debugToScreen;

		// Token: 0x0400677C RID: 26492
		[SerializeField]
		private bool m_highPrecision;

		// Token: 0x0400677D RID: 26493
		[SerializeField]
		private Vector4 m_bloomRange = new Vector4(500f, 1f, 0f, 0f);

		// Token: 0x0400677E RID: 26494
		[SerializeField]
		private float m_overallThreshold = 0.53f;

		// Token: 0x0400677F RID: 26495
		[SerializeField]
		private Vector4 m_bloomParams = new Vector4(0.8f, 1f, 1f, 1f);

		// Token: 0x04006780 RID: 26496
		[SerializeField]
		private bool m_temporalFilteringActive;

		// Token: 0x04006781 RID: 26497
		[SerializeField]
		private float m_temporalFilteringValue = 0.05f;

		// Token: 0x04006782 RID: 26498
		[SerializeField]
		private int m_bloomDownsampleCount = 6;

		// Token: 0x04006783 RID: 26499
		[SerializeField]
		private AnimationCurve m_temporalFilteringCurve;

		// Token: 0x04006784 RID: 26500
		[SerializeField]
		private bool m_separateFeaturesThreshold;

		// Token: 0x04006785 RID: 26501
		[SerializeField]
		private float m_featuresThreshold = 0.05f;

		// Token: 0x04006786 RID: 26502
		[SerializeField]
		private AmplifyLensFlare m_lensFlare = new AmplifyLensFlare();

		// Token: 0x04006787 RID: 26503
		[SerializeField]
		private bool m_applyLensDirt = true;

		// Token: 0x04006788 RID: 26504
		[SerializeField]
		private float m_lensDirtStrength = 2f;

		// Token: 0x04006789 RID: 26505
		[SerializeField]
		private Texture m_lensDirtTexture;

		// Token: 0x0400678A RID: 26506
		[SerializeField]
		private bool m_applyLensStardurst = true;

		// Token: 0x0400678B RID: 26507
		[SerializeField]
		private Texture m_lensStardurstTex;

		// Token: 0x0400678C RID: 26508
		[SerializeField]
		private float m_lensStarburstStrength = 2f;

		// Token: 0x0400678D RID: 26509
		[SerializeField]
		private AmplifyGlare m_anamorphicGlare = new AmplifyGlare();

		// Token: 0x0400678E RID: 26510
		[SerializeField]
		private AmplifyBokeh m_bokehFilter = new AmplifyBokeh();

		// Token: 0x0400678F RID: 26511
		[SerializeField]
		private float[] m_upscaleWeights = new float[] { 0.0842f, 0.1282f, 0.1648f, 0.2197f, 0.2197f, 0.1831f };

		// Token: 0x04006790 RID: 26512
		[SerializeField]
		private float[] m_gaussianRadius = new float[] { 1f, 1f, 1f, 1f, 1f, 1f };

		// Token: 0x04006791 RID: 26513
		[SerializeField]
		private int[] m_gaussianSteps = new int[] { 1, 1, 1, 1, 1, 1 };

		// Token: 0x04006792 RID: 26514
		[SerializeField]
		private float[] m_lensDirtWeights = new float[] { 0.067f, 0.102f, 0.1311f, 0.1749f, 0.2332f, 0.3f };

		// Token: 0x04006793 RID: 26515
		[SerializeField]
		private float[] m_lensStarburstWeights = new float[] { 0.067f, 0.102f, 0.1311f, 0.1749f, 0.2332f, 0.3f };

		// Token: 0x04006794 RID: 26516
		[SerializeField]
		private bool[] m_downscaleSettingsFoldout = new bool[6];

		// Token: 0x04006795 RID: 26517
		[SerializeField]
		private int m_featuresSourceId;

		// Token: 0x04006796 RID: 26518
		[SerializeField]
		private UpscaleQualityEnum m_upscaleQuality;

		// Token: 0x04006797 RID: 26519
		[SerializeField]
		private MainThresholdSizeEnum m_mainThresholdSize;

		// Token: 0x04006798 RID: 26520
		private Material m_bloomMaterial;

		// Token: 0x04006799 RID: 26521
		private Shader m_bloomShader;

		// Token: 0x0400679A RID: 26522
		private Camera m_camera;

		// Token: 0x0400679B RID: 26523
		private Transform m_cameraTransform;

		// Token: 0x0400679C RID: 26524
		private Material m_finalCompositionMaterial;

		// Token: 0x0400679D RID: 26525
		private Shader m_finalCompositionShader;

		// Token: 0x0400679E RID: 26526
		private Matrix4x4 m_starburstMat;

		// Token: 0x0400679F RID: 26527
		private RenderTexture[] m_tempAuxDownsampleRTs = new RenderTexture[6];

		// Token: 0x040067A0 RID: 26528
		private Vector2[] m_tempDownsamplesSizes = new Vector2[6];

		// Token: 0x040067A1 RID: 26529
		private RenderTexture m_tempFilterBuffer;

		// Token: 0x040067A2 RID: 26530
		private RenderTexture[] m_tempUpscaleRTs = new RenderTexture[6];

		// Token: 0x040067A3 RID: 26531
		private bool silentError;
	}
}
