using System;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029B9 RID: 10681
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ConverterInfo
	{
		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06009024 RID: 36900 RVA: 0x00056441 File Offset: 0x00054641
		// (set) Token: 0x06009025 RID: 36901 RVA: 0x00056449 File Offset: 0x00054649
		public string Name { get; set; }

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06009026 RID: 36902 RVA: 0x00056452 File Offset: 0x00054652
		// (set) Token: 0x06009027 RID: 36903 RVA: 0x0005645A File Offset: 0x0005465A
		public Type Type { get; set; }

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06009028 RID: 36904 RVA: 0x00056463 File Offset: 0x00054663
		public PropertiesDictionary Properties { get; } = new PropertiesDictionary();

		// Token: 0x06009029 RID: 36905 RVA: 0x0005646B File Offset: 0x0005466B
		public void AddProperty(PropertyEntry entry)
		{
			this.Properties[entry.Key] = entry.Value;
		}
	}
}
