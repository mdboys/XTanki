using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UI
{
	// Token: 0x020001C7 RID: 455
	[NullableContext(1)]
	[Nullable(0)]
	[AddComponentMenu("UI/Effects/Gradient")]
	public class Gradient : BaseMeshEffect
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0000ADD1 File Offset: 0x00008FD1
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0000ADD9 File Offset: 0x00008FD9
		public Gradient.Blend BlendMode
		{
			get
			{
				return this._blendMode;
			}
			set
			{
				this._blendMode = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0000ADE2 File Offset: 0x00008FE2
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0000ADEA File Offset: 0x00008FEA
		public Gradient EffectGradient
		{
			get
			{
				return this._effectGradient;
			}
			set
			{
				this._effectGradient = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0000ADF3 File Offset: 0x00008FF3
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0000ADFB File Offset: 0x00008FFB
		public Gradient.Type GradientType
		{
			get
			{
				return this._gradientType;
			}
			set
			{
				this._gradientType = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0000AE04 File Offset: 0x00009004
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0000AE0C File Offset: 0x0000900C
		public float Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0007C6DC File Offset: 0x0007A8DC
		public override void ModifyMesh(VertexHelper helper)
		{
			if (!this.IsActive() || helper.currentVertCount == 0)
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			helper.GetUIVertexStream(list);
			int count = list.Count;
			Gradient.Type gradientType = this.GradientType;
			if (gradientType == Gradient.Type.Horizontal)
			{
				float num = list[0].position.x;
				float num2 = list[0].position.x;
				for (int i = count - 1; i >= 1; i--)
				{
					float x = list[i].position.x;
					if (x > num2)
					{
						num2 = x;
					}
					else if (x < num)
					{
						num = x;
					}
				}
				float num3 = 1f / (num2 - num);
				UIVertex uivertex = default(UIVertex);
				for (int j = 0; j < helper.currentVertCount; j++)
				{
					helper.PopulateUIVertex(ref uivertex, j);
					uivertex.color = this.BlendColor(uivertex.color, this.EffectGradient.Evaluate((uivertex.position.x - num) * num3 - this.Offset));
					helper.SetUIVertex(uivertex, j);
				}
				return;
			}
			if (gradientType != Gradient.Type.Vertical)
			{
				return;
			}
			float num4 = list[0].position.y;
			float num5 = list[0].position.y;
			for (int k = count - 1; k >= 1; k--)
			{
				float y = list[k].position.y;
				if (y > num5)
				{
					num5 = y;
				}
				else if (y < num4)
				{
					num4 = y;
				}
			}
			float num6 = 1f / (num5 - num4);
			UIVertex uivertex2 = default(UIVertex);
			for (int l = 0; l < helper.currentVertCount; l++)
			{
				helper.PopulateUIVertex(ref uivertex2, l);
				uivertex2.color = this.BlendColor(uivertex2.color, this.EffectGradient.Evaluate((uivertex2.position.y - num4) * num6 - this.Offset));
				helper.SetUIVertex(uivertex2, l);
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0007C8F0 File Offset: 0x0007AAF0
		private Color BlendColor(Color colorA, Color colorB)
		{
			Gradient.Blend blendMode = this.BlendMode;
			Color color;
			if (blendMode != Gradient.Blend.Add)
			{
				if (blendMode != Gradient.Blend.Multiply)
				{
					color = colorB;
				}
				else
				{
					color = colorA * colorB;
				}
			}
			else
			{
				color = colorA + colorB;
			}
			return color;
		}

		// Token: 0x0400062C RID: 1580
		[SerializeField]
		private Gradient.Type _gradientType;

		// Token: 0x0400062D RID: 1581
		[SerializeField]
		private Gradient.Blend _blendMode = Gradient.Blend.Multiply;

		// Token: 0x0400062E RID: 1582
		[SerializeField]
		[Range(-1f, 1f)]
		private float _offset;

		// Token: 0x0400062F RID: 1583
		[SerializeField]
		private Gradient _effectGradient = new Gradient
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(Color.black, 0f),
				new GradientColorKey(Color.white, 1f)
			}
		};

		// Token: 0x020001C8 RID: 456
		[NullableContext(0)]
		public enum Blend
		{
			// Token: 0x04000631 RID: 1585
			Override,
			// Token: 0x04000632 RID: 1586
			Add,
			// Token: 0x04000633 RID: 1587
			Multiply
		}

		// Token: 0x020001C9 RID: 457
		[NullableContext(0)]
		public enum Type
		{
			// Token: 0x04000635 RID: 1589
			Horizontal,
			// Token: 0x04000636 RID: 1590
			Vertical
		}
	}
}
