using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000197 RID: 407
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Contrast Stretch")]
	public class ContrastStretch : MonoBehaviour
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0000A62C File Offset: 0x0000882C
		protected Material materialLum
		{
			get
			{
				if (this.m_materialLum == null)
				{
					this.m_materialLum = new Material(this.shaderLum)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
				}
				return this.m_materialLum;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0000A65B File Offset: 0x0000885B
		protected Material materialReduce
		{
			get
			{
				if (this.m_materialReduce == null)
				{
					this.m_materialReduce = new Material(this.shaderReduce)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
				}
				return this.m_materialReduce;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0000A68A File Offset: 0x0000888A
		protected Material materialAdapt
		{
			get
			{
				if (this.m_materialAdapt == null)
				{
					this.m_materialAdapt = new Material(this.shaderAdapt)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
				}
				return this.m_materialAdapt;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0000A6B9 File Offset: 0x000088B9
		protected Material materialApply
		{
			get
			{
				if (this.m_materialApply == null)
				{
					this.m_materialApply = new Material(this.shaderApply)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
				}
				return this.m_materialApply;
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000768BC File Offset: 0x00074ABC
		private void Start()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
				return;
			}
			if (!this.shaderAdapt.isSupported || !this.shaderApply.isSupported || !this.shaderLum.isSupported || !this.shaderReduce.isSupported)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00076914 File Offset: 0x00074B14
		private void OnEnable()
		{
			for (int i = 0; i < 2; i++)
			{
				if (!this.adaptRenderTex[i])
				{
					this.adaptRenderTex[i] = new RenderTexture(1, 1, 0)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
				}
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00076954 File Offset: 0x00074B54
		private void OnDisable()
		{
			for (int i = 0; i < 2; i++)
			{
				global::UnityEngine.Object.DestroyImmediate(this.adaptRenderTex[i]);
				this.adaptRenderTex[i] = null;
			}
			if (this.m_materialLum)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_materialLum);
			}
			if (this.m_materialReduce)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_materialReduce);
			}
			if (this.m_materialAdapt)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_materialAdapt);
			}
			if (this.m_materialApply)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_materialApply);
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000769E4 File Offset: 0x00074BE4
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			RenderTexture renderTexture = RenderTexture.GetTemporary(source.width, source.height);
			Graphics.Blit(source, renderTexture, this.materialLum);
			while (renderTexture.width > 1 || renderTexture.height > 1)
			{
				int num = renderTexture.width / 2;
				if (num < 1)
				{
					num = 1;
				}
				int num2 = renderTexture.height / 2;
				if (num2 < 1)
				{
					num2 = 1;
				}
				RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
				Graphics.Blit(renderTexture, temporary, this.materialReduce);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			this.CalculateAdaptation(renderTexture);
			this.materialApply.SetTexture("_AdaptTex", this.adaptRenderTex[this.curAdaptIndex]);
			Graphics.Blit(source, destination, this.materialApply);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00076A98 File Offset: 0x00074C98
		private void CalculateAdaptation(Texture curTexture)
		{
			int num = this.curAdaptIndex;
			this.curAdaptIndex = (this.curAdaptIndex + 1) % 2;
			float num2 = 1f - Mathf.Pow(1f - this.adaptationSpeed, 30f * Time.deltaTime);
			num2 = Mathf.Clamp(num2, 0.01f, 1f);
			this.materialAdapt.SetTexture("_CurTex", curTexture);
			this.materialAdapt.SetVector("_AdaptParams", new Vector4(num2, this.limitMinimum, this.limitMaximum, 0f));
			Graphics.SetRenderTarget(this.adaptRenderTex[this.curAdaptIndex]);
			GL.Clear(false, true, Color.black);
			Graphics.Blit(this.adaptRenderTex[num], this.adaptRenderTex[this.curAdaptIndex], this.materialAdapt);
		}

		// Token: 0x040004DB RID: 1243
		public float adaptationSpeed = 0.02f;

		// Token: 0x040004DC RID: 1244
		public float limitMinimum = 0.2f;

		// Token: 0x040004DD RID: 1245
		public float limitMaximum = 0.6f;

		// Token: 0x040004DE RID: 1246
		public Shader shaderLum;

		// Token: 0x040004DF RID: 1247
		public Shader shaderReduce;

		// Token: 0x040004E0 RID: 1248
		public Shader shaderAdapt;

		// Token: 0x040004E1 RID: 1249
		public Shader shaderApply;

		// Token: 0x040004E2 RID: 1250
		private readonly RenderTexture[] adaptRenderTex = new RenderTexture[2];

		// Token: 0x040004E3 RID: 1251
		private int curAdaptIndex;

		// Token: 0x040004E4 RID: 1252
		private Material m_materialAdapt;

		// Token: 0x040004E5 RID: 1253
		private Material m_materialApply;

		// Token: 0x040004E6 RID: 1254
		private Material m_materialLum;

		// Token: 0x040004E7 RID: 1255
		private Material m_materialReduce;
	}
}
