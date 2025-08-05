using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000223 RID: 547
	[NullableContext(1)]
	[Nullable(0)]
	public class PostProcessingContext
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0000B7E2 File Offset: 0x000099E2
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x0000B7EA File Offset: 0x000099EA
		public bool interrupted { get; private set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0000B7F3 File Offset: 0x000099F3
		public bool isGBufferAvailable
		{
			get
			{
				return this.camera.actualRenderingPath == RenderingPath.DeferredShading;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0000B803 File Offset: 0x00009A03
		public bool isHdr
		{
			get
			{
				return this.camera.allowHDR;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0000B810 File Offset: 0x00009A10
		public int width
		{
			get
			{
				return this.camera.pixelWidth;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0000B81D File Offset: 0x00009A1D
		public int height
		{
			get
			{
				return this.camera.pixelHeight;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0000B82A File Offset: 0x00009A2A
		public Rect viewport
		{
			get
			{
				return this.camera.rect;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000B837 File Offset: 0x00009A37
		public void Interrupt()
		{
			this.interrupted = true;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000B840 File Offset: 0x00009A40
		public PostProcessingContext Reset()
		{
			this.profile = null;
			this.camera = null;
			this.materialFactory = null;
			this.renderTextureFactory = null;
			this.interrupted = false;
			return this;
		}

		// Token: 0x040007B7 RID: 1975
		public Camera camera;

		// Token: 0x040007B8 RID: 1976
		public MaterialFactory materialFactory;

		// Token: 0x040007B9 RID: 1977
		public PostProcessingProfile profile;

		// Token: 0x040007BA RID: 1978
		public RenderTextureFactory renderTextureFactory;
	}
}
