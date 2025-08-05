using System;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Layout
{
	// Token: 0x02002A17 RID: 10775
	[NullableContext(1)]
	[Nullable(0)]
	public class DynamicPatternLayout : PatternLayout
	{
		// Token: 0x060092F3 RID: 37619 RVA: 0x00057D59 File Offset: 0x00055F59
		public DynamicPatternLayout()
		{
		}

		// Token: 0x060092F4 RID: 37620 RVA: 0x00057D81 File Offset: 0x00055F81
		public DynamicPatternLayout(string pattern)
			: base(pattern)
		{
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x060092F5 RID: 37621 RVA: 0x00057DAA File Offset: 0x00055FAA
		// (set) Token: 0x060092F6 RID: 37622 RVA: 0x00057DB7 File Offset: 0x00055FB7
		public override string Header
		{
			get
			{
				return this.m_headerPatternString.Format();
			}
			set
			{
				base.Header = value;
				this.m_headerPatternString = new PatternString(value);
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x060092F7 RID: 37623 RVA: 0x00057DCC File Offset: 0x00055FCC
		// (set) Token: 0x060092F8 RID: 37624 RVA: 0x00057DD9 File Offset: 0x00055FD9
		public override string Footer
		{
			get
			{
				return this.m_footerPatternString.Format();
			}
			set
			{
				base.Footer = value;
				this.m_footerPatternString = new PatternString(value);
			}
		}

		// Token: 0x04006196 RID: 24982
		private PatternString m_footerPatternString = new PatternString(string.Empty);

		// Token: 0x04006197 RID: 24983
		private PatternString m_headerPatternString = new PatternString(string.Empty);
	}
}
