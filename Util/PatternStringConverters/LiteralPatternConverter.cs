using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029EF RID: 10735
	[NullableContext(1)]
	[Nullable(0)]
	internal class LiteralPatternConverter : PatternConverter
	{
		// Token: 0x060091B6 RID: 37302 RVA: 0x0013DD60 File Offset: 0x0013BF60
		public override PatternConverter SetNext(PatternConverter pc)
		{
			LiteralPatternConverter literalPatternConverter = pc as LiteralPatternConverter;
			if (literalPatternConverter != null)
			{
				this.Option += literalPatternConverter.Option;
				return this;
			}
			return base.SetNext(pc);
		}

		// Token: 0x060091B7 RID: 37303 RVA: 0x00057313 File Offset: 0x00055513
		public override void Format(TextWriter writer, object state)
		{
			writer.Write(this.Option);
		}

		// Token: 0x060091B8 RID: 37304 RVA: 0x00057321 File Offset: 0x00055521
		protected override void Convert(TextWriter writer, object state)
		{
			throw new InvalidOperationException("Should never get here because of the overridden Format method");
		}
	}
}
