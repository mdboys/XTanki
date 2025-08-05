using System;
using System.Runtime.CompilerServices;
using Edelweiss.DecalSystem;
using UnityEngine;

namespace Edelweiss.TextureAtlas
{
	// Token: 0x02002AE6 RID: 10982
	[NullableContext(1)]
	[Nullable(0)]
	public class TextureAtlasAsset : ScriptableObject
	{
		// Token: 0x04006398 RID: 25496
		public Material material;

		// Token: 0x04006399 RID: 25497
		public UVChannelModificationEnum uvChannelModification;

		// Token: 0x0400639A RID: 25498
		public UVRectangle[] uvRectangles = new UVRectangle[0];

		// Token: 0x0400639B RID: 25499
		public UVRectangle[] uv2Rectangles = new UVRectangle[0];
	}
}
