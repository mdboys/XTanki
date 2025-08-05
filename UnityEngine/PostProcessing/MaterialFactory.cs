using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000214 RID: 532
	public sealed class MaterialFactory : IDisposable
	{
		// Token: 0x06000982 RID: 2434 RVA: 0x0000B5EA File Offset: 0x000097EA
		public MaterialFactory()
		{
			this.m_Materials = new Dictionary<string, Material>();
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00080664 File Offset: 0x0007E864
		public void Dispose()
		{
			foreach (KeyValuePair<string, Material> keyValuePair in this.m_Materials)
			{
				GraphicsUtils.Destroy(keyValuePair.Value);
			}
			this.m_Materials.Clear();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000806A8 File Offset: 0x0007E8A8
		[NullableContext(1)]
		public Material Get(string shaderName)
		{
			Material material;
			if (!this.m_Materials.TryGetValue(shaderName, out material))
			{
				Shader shader = Shader.Find(shaderName);
				if (shader == null)
				{
					throw new ArgumentException("Shader not found (" + shaderName + ")");
				}
				material = new Material(shader)
				{
					name = string.Format("PostFX - {0}", shaderName.Substring(shaderName.LastIndexOf("/") + 1)),
					hideFlags = HideFlags.DontSave
				};
				this.m_Materials.Add(shaderName, material);
			}
			return material;
		}

		// Token: 0x04000761 RID: 1889
		[Nullable(1)]
		private readonly Dictionary<string, Material> m_Materials;
	}
}
