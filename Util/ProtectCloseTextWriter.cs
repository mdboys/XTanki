using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029D1 RID: 10705
	[NullableContext(1)]
	[Nullable(0)]
	public class ProtectCloseTextWriter : TextWriterAdapter
	{
		// Token: 0x06009100 RID: 37120 RVA: 0x00056D22 File Offset: 0x00054F22
		public ProtectCloseTextWriter(TextWriter writer)
			: base(writer)
		{
		}

		// Token: 0x06009101 RID: 37121 RVA: 0x00056D2B File Offset: 0x00054F2B
		public void Attach(TextWriter writer)
		{
			base.Writer = writer;
		}

		// Token: 0x06009102 RID: 37122 RVA: 0x0000568E File Offset: 0x0000388E
		public override void Close()
		{
		}
	}
}
