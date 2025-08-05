using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000224 RID: 548
	[Serializable]
	public abstract class PostProcessingModel
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0000B866 File Offset: 0x00009A66
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0000B86E File Offset: 0x00009A6E
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
			set
			{
				this.m_Enabled = value;
				if (value)
				{
					this.OnValidate();
				}
			}
		}

		// Token: 0x060009D4 RID: 2516
		public abstract void Reset();

		// Token: 0x060009D5 RID: 2517 RVA: 0x0000568E File Offset: 0x0000388E
		public virtual void OnValidate()
		{
		}

		// Token: 0x040007BC RID: 1980
		[SerializeField]
		[GetSet("enabled")]
		private bool m_Enabled;
	}
}
