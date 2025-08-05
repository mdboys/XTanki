using System;
using System.Runtime.CompilerServices;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E9 RID: 10729
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface)]
	public sealed class TypeConverterAttribute : Attribute
	{
		// Token: 0x060091A2 RID: 37282 RVA: 0x00005641 File Offset: 0x00003841
		public TypeConverterAttribute()
		{
		}

		// Token: 0x060091A3 RID: 37283 RVA: 0x00057281 File Offset: 0x00055481
		public TypeConverterAttribute(string typeName)
		{
			this.ConverterTypeName = typeName;
		}

		// Token: 0x060091A4 RID: 37284 RVA: 0x00057290 File Offset: 0x00055490
		public TypeConverterAttribute(Type converterType)
		{
			this.ConverterTypeName = SystemInfo.AssemblyQualifiedName(converterType);
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x060091A5 RID: 37285 RVA: 0x000572A4 File Offset: 0x000554A4
		// (set) Token: 0x060091A6 RID: 37286 RVA: 0x000572AC File Offset: 0x000554AC
		public string ConverterTypeName { get; set; }
	}
}
