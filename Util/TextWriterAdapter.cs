using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace log4net.Util
{
	// Token: 0x020029D8 RID: 10712
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class TextWriterAdapter : TextWriter
	{
		// Token: 0x06009150 RID: 37200 RVA: 0x00056F88 File Offset: 0x00055188
		protected TextWriterAdapter(TextWriter writer)
		{
			this.Writer = writer;
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06009151 RID: 37201 RVA: 0x00056F97 File Offset: 0x00055197
		// (set) Token: 0x06009152 RID: 37202 RVA: 0x00056F9F File Offset: 0x0005519F
		protected TextWriter Writer { get; set; }

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06009153 RID: 37203 RVA: 0x00056FA8 File Offset: 0x000551A8
		public override Encoding Encoding
		{
			get
			{
				return this.Writer.Encoding;
			}
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06009154 RID: 37204 RVA: 0x00056FB5 File Offset: 0x000551B5
		public override IFormatProvider FormatProvider
		{
			get
			{
				return this.Writer.FormatProvider;
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06009155 RID: 37205 RVA: 0x00056FC2 File Offset: 0x000551C2
		// (set) Token: 0x06009156 RID: 37206 RVA: 0x00056FCF File Offset: 0x000551CF
		public override string NewLine
		{
			get
			{
				return this.Writer.NewLine;
			}
			set
			{
				this.Writer.NewLine = value;
			}
		}

		// Token: 0x06009157 RID: 37207 RVA: 0x00056FDD File Offset: 0x000551DD
		public override void Close()
		{
			this.Writer.Close();
		}

		// Token: 0x06009158 RID: 37208 RVA: 0x00056FEA File Offset: 0x000551EA
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDisposable)this.Writer).Dispose();
			}
		}

		// Token: 0x06009159 RID: 37209 RVA: 0x00056FFA File Offset: 0x000551FA
		public override void Flush()
		{
			this.Writer.Flush();
		}

		// Token: 0x0600915A RID: 37210 RVA: 0x00057007 File Offset: 0x00055207
		public override void Write(char value)
		{
			this.Writer.Write(value);
		}

		// Token: 0x0600915B RID: 37211 RVA: 0x00057015 File Offset: 0x00055215
		public override void Write(char[] buffer, int index, int count)
		{
			this.Writer.Write(buffer, index, count);
		}

		// Token: 0x0600915C RID: 37212 RVA: 0x00057025 File Offset: 0x00055225
		public override void Write(string value)
		{
			this.Writer.Write(value);
		}
	}
}
