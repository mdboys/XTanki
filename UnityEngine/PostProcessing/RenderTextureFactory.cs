using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000226 RID: 550
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RenderTextureFactory : IDisposable
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x0000B880 File Offset: 0x00009A80
		public RenderTextureFactory()
		{
			this.m_TemporaryRTs = new HashSet<RenderTexture>();
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000B893 File Offset: 0x00009A93
		public void Dispose()
		{
			this.ReleaseAll();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00081EE4 File Offset: 0x000800E4
		public RenderTexture Get(RenderTexture baseRenderTexture)
		{
			return this.Get(baseRenderTexture.width, baseRenderTexture.height, baseRenderTexture.depth, baseRenderTexture.format, (!baseRenderTexture.sRGB) ? RenderTextureReadWrite.Linear : RenderTextureReadWrite.sRGB, baseRenderTexture.filterMode, baseRenderTexture.wrapMode, "FactoryTempTexture");
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00081F2C File Offset: 0x0008012C
		public RenderTexture Get(int width, int height, int depthBuffer = 0, RenderTextureFormat format = RenderTextureFormat.ARGBHalf, RenderTextureReadWrite rw = RenderTextureReadWrite.Default, FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp, string name = "FactoryTempTexture")
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, depthBuffer, format);
			temporary.filterMode = filterMode;
			temporary.wrapMode = wrapMode;
			temporary.name = name;
			this.m_TemporaryRTs.Add(temporary);
			return temporary;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000B89B File Offset: 0x00009A9B
		public void Release(RenderTexture rt)
		{
			if (!(rt == null))
			{
				if (!this.m_TemporaryRTs.Contains(rt))
				{
					throw new ArgumentException(string.Format("Attempting to remove a RenderTexture that was not allocated: {0}", rt));
				}
				this.m_TemporaryRTs.Remove(rt);
				RenderTexture.ReleaseTemporary(rt);
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00081F6C File Offset: 0x0008016C
		public void ReleaseAll()
		{
			foreach (RenderTexture renderTexture in this.m_TemporaryRTs)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			this.m_TemporaryRTs.Clear();
		}

		// Token: 0x040007CC RID: 1996
		private readonly HashSet<RenderTexture> m_TemporaryRTs;
	}
}
