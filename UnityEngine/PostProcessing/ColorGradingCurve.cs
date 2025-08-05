using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001EE RID: 494
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public sealed class ColorGradingCurve
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x0000B216 File Offset: 0x00009416
		public ColorGradingCurve(AnimationCurve curve, float zeroValue, bool loop, Vector2 bounds)
		{
			this.curve = curve;
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			this.m_Range = bounds.magnitude;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0007ED28 File Offset: 0x0007CF28
		public void Cache()
		{
			if (!this.m_Loop)
			{
				return;
			}
			int length = this.curve.length;
			if (length >= 2)
			{
				if (this.m_InternalLoopingCurve == null)
				{
					this.m_InternalLoopingCurve = new AnimationCurve();
				}
				Keyframe keyframe = this.curve[length - 1];
				keyframe.time -= this.m_Range;
				Keyframe keyframe2 = this.curve[0];
				keyframe2.time += this.m_Range;
				this.m_InternalLoopingCurve.keys = this.curve.keys;
				this.m_InternalLoopingCurve.AddKey(keyframe);
				this.m_InternalLoopingCurve.AddKey(keyframe2);
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0007EDD8 File Offset: 0x0007CFD8
		public float Evaluate(float t)
		{
			if (this.curve.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.curve.length == 1)
			{
				return this.curve.Evaluate(t);
			}
			return this.m_InternalLoopingCurve.Evaluate(t);
		}

		// Token: 0x040006CF RID: 1743
		public AnimationCurve curve;

		// Token: 0x040006D0 RID: 1744
		[SerializeField]
		private bool m_Loop;

		// Token: 0x040006D1 RID: 1745
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x040006D2 RID: 1746
		[SerializeField]
		private float m_Range;

		// Token: 0x040006D3 RID: 1747
		private AnimationCurve m_InternalLoopingCurve;
	}
}
