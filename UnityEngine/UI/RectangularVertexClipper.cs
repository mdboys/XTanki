using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UI
{
	// Token: 0x020001CA RID: 458
	[NullableContext(1)]
	[Nullable(0)]
	public class RectangularVertexClipper
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0007C988 File Offset: 0x0007AB88
		public Rect GetCanvasRect(RectTransform t, Canvas c)
		{
			if (c == null)
			{
				return default(Rect);
			}
			t.GetWorldCorners(this.m_WorldCorners);
			Transform component = c.GetComponent<Transform>();
			for (int i = 0; i < 4; i++)
			{
				this.m_CanvasCorners[i] = component.InverseTransformPoint(this.m_WorldCorners[i]);
			}
			return new Rect(this.m_CanvasCorners[0].x, this.m_CanvasCorners[0].y, this.m_CanvasCorners[2].x - this.m_CanvasCorners[0].x, this.m_CanvasCorners[2].y - this.m_CanvasCorners[0].y);
		}

		// Token: 0x04000637 RID: 1591
		private readonly Vector3[] m_CanvasCorners = new Vector3[4];

		// Token: 0x04000638 RID: 1592
		private readonly Vector3[] m_WorldCorners = new Vector3[4];
	}
}
