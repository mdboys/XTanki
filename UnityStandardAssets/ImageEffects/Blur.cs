using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200018D RID: 397
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Blur/Blur")]
	public class Blur : MonoBehaviour
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0000A3A3 File Offset: 0x000085A3
		protected Material material
		{
			get
			{
				if (Blur.m_Material == null)
				{
					Blur.m_Material = new Material(this.blurShader)
					{
						hideFlags = HideFlags.DontSave
					};
				}
				return Blur.m_Material;
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0000A3CF File Offset: 0x000085CF
		protected void Start()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
				return;
			}
			if (!this.blurShader || !this.material.shader.isSupported)
			{
				base.enabled = false;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0000A406 File Offset: 0x00008606
		protected void OnDisable()
		{
			if (Blur.m_Material)
			{
				global::UnityEngine.Object.DestroyImmediate(Blur.m_Material);
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00074EBC File Offset: 0x000730BC
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			int num = source.width / 4;
			int num2 = source.height / 4;
			RenderTexture renderTexture = RenderTexture.GetTemporary(num, num2, 0);
			this.DownSample4x(source, renderTexture);
			for (int i = 0; i < this.iterations; i++)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0);
				this.FourTapCone(renderTexture, temporary, i);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			Graphics.Blit(renderTexture, destination);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00074F28 File Offset: 0x00073128
		public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
		{
			float num = 0.5f + (float)iteration * this.blurSpread;
			Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
			{
				new Vector2(0f - num, 0f - num),
				new Vector2(0f - num, num),
				new Vector2(num, num),
				new Vector2(num, 0f - num)
			});
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00074FA8 File Offset: 0x000731A8
		private void DownSample4x(RenderTexture source, RenderTexture dest)
		{
			float num = 1f;
			Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
			{
				new Vector2(0f - num, 0f - num),
				new Vector2(0f - num, num),
				new Vector2(num, num),
				new Vector2(num, 0f - num)
			});
		}

		// Token: 0x04000483 RID: 1155
		private static Material m_Material;

		// Token: 0x04000484 RID: 1156
		public int iterations = 3;

		// Token: 0x04000485 RID: 1157
		public float blurSpread = 0.6f;

		// Token: 0x04000486 RID: 1158
		public Shader blurShader;
	}
}
