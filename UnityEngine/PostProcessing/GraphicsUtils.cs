using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000213 RID: 531
	[NullableContext(1)]
	[Nullable(0)]
	public static class GraphicsUtils
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0000B5B1 File Offset: 0x000097B1
		public static bool isLinearColorSpace
		{
			get
			{
				return QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0000B5BB File Offset: 0x000097BB
		public static bool supportsDX11
		{
			get
			{
				return SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00080324 File Offset: 0x0007E524
		public static Texture2D whiteTexture
		{
			get
			{
				if (GraphicsUtils.s_WhiteTexture != null)
				{
					return GraphicsUtils.s_WhiteTexture;
				}
				GraphicsUtils.s_WhiteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
				GraphicsUtils.s_WhiteTexture.SetPixel(0, 0, new Color(1f, 1f, 1f, 1f));
				GraphicsUtils.s_WhiteTexture.Apply();
				return GraphicsUtils.s_WhiteTexture;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00080388 File Offset: 0x0007E588
		public static Mesh quad
		{
			get
			{
				if (GraphicsUtils.s_Quad != null)
				{
					return GraphicsUtils.s_Quad;
				}
				Vector3[] array = new Vector3[]
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(1f, 1f, 0f),
					new Vector3(1f, -1f, 0f),
					new Vector3(-1f, 1f, 0f)
				};
				Vector2[] array2 = new Vector2[]
				{
					new Vector2(0f, 0f),
					new Vector2(1f, 1f),
					new Vector2(1f, 0f),
					new Vector2(0f, 1f)
				};
				int[] array3 = new int[] { 0, 1, 2, 1, 0, 3 };
				GraphicsUtils.s_Quad = new Mesh
				{
					vertices = array,
					uv = array2,
					triangles = array3
				};
				GraphicsUtils.s_Quad.RecalculateNormals();
				GraphicsUtils.s_Quad.RecalculateBounds();
				return GraphicsUtils.s_Quad;
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000804C4 File Offset: 0x0007E6C4
		public static void Blit(Material material, int pass)
		{
			GL.PushMatrix();
			GL.LoadOrtho();
			material.SetPass(pass);
			GL.Begin(5);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, 0.1f);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, 0.1f);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, 0.1f);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, 0.1f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00080580 File Offset: 0x0007E780
		public static void ClearAndBlit(Texture source, RenderTexture destination, Material material, int pass, bool clearColor = true, bool clearDepth = false)
		{
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = destination;
			GL.Clear(false, clearColor, Color.clear);
			GL.PushMatrix();
			GL.LoadOrtho();
			material.SetTexture("_MainTex", source);
			material.SetPass(pass);
			GL.Begin(5);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, 0.1f);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, 0.1f);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, 0.1f);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, 0.1f);
			GL.End();
			GL.PopMatrix();
			RenderTexture.active = active;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000B5CD File Offset: 0x000097CD
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0000B5DE File Offset: 0x000097DE
		public static void Dispose()
		{
			GraphicsUtils.Destroy(GraphicsUtils.s_Quad);
		}

		// Token: 0x0400075F RID: 1887
		private static Texture2D s_WhiteTexture;

		// Token: 0x04000760 RID: 1888
		private static Mesh s_Quad;
	}
}
