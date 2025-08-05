using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x0200299C RID: 10652
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	public class DefaultConvexFace<TVertex> : ConvexFace<TVertex, DefaultConvexFace<TVertex>> where TVertex : IVertex
	{
	}
}
