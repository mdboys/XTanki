using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B1F RID: 11039
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class UVRectangle
	{
		// Token: 0x06009918 RID: 39192 RVA: 0x001535E8 File Offset: 0x001517E8
		public UVRectangle()
		{
			this.name = "UVRectangle";
			this.lowerLeftUV = Vector2.zero;
			this.upperRightUV = Vector2.one;
		}

		// Token: 0x06009919 RID: 39193 RVA: 0x00153644 File Offset: 0x00151844
		public UVRectangle(UVRectangle a_Other)
		{
			this.name = string.Copy(a_Other.name);
			this.lowerLeftUV = a_Other.lowerLeftUV;
			this.upperRightUV = a_Other.upperRightUV;
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x0600991A RID: 39194 RVA: 0x0005B684 File Offset: 0x00059884
		public Vector2 Size
		{
			get
			{
				return this.upperRightUV - this.lowerLeftUV;
			}
		}

		// Token: 0x0600991B RID: 39195 RVA: 0x0005B697 File Offset: 0x00059897
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x04006466 RID: 25702
		public string name = "UVRectangle";

		// Token: 0x04006467 RID: 25703
		public Vector2 lowerLeftUV = Vector2.zero;

		// Token: 0x04006468 RID: 25704
		public Vector2 upperRightUV = Vector3.one;
	}
}
