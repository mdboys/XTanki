using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using log4net.Repository;

namespace log4net.Util
{
	// Token: 0x020029CB RID: 10699
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PatternConverter
	{
		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x060090BE RID: 37054 RVA: 0x000569E2 File Offset: 0x00054BE2
		public virtual PatternConverter Next
		{
			get
			{
				return this.m_next;
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x060090BF RID: 37055 RVA: 0x000569EA File Offset: 0x00054BEA
		// (set) Token: 0x060090C0 RID: 37056 RVA: 0x00056A03 File Offset: 0x00054C03
		public virtual FormattingInfo FormattingInfo
		{
			get
			{
				return new FormattingInfo(this.m_min, this.m_max, this.m_leftAlign);
			}
			set
			{
				this.m_min = value.Min;
				this.m_max = value.Max;
				this.m_leftAlign = value.LeftAlign;
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x060090C1 RID: 37057 RVA: 0x00056A29 File Offset: 0x00054C29
		// (set) Token: 0x060090C2 RID: 37058 RVA: 0x00056A31 File Offset: 0x00054C31
		public virtual string Option { get; set; }

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x060090C3 RID: 37059 RVA: 0x00056A3A File Offset: 0x00054C3A
		// (set) Token: 0x060090C4 RID: 37060 RVA: 0x00056A42 File Offset: 0x00054C42
		public PropertiesDictionary Properties { get; set; }

		// Token: 0x060090C5 RID: 37061
		protected abstract void Convert(TextWriter writer, object state);

		// Token: 0x060090C6 RID: 37062 RVA: 0x00056A4B File Offset: 0x00054C4B
		public virtual PatternConverter SetNext(PatternConverter patternConverter)
		{
			this.m_next = patternConverter;
			return this.m_next;
		}

		// Token: 0x060090C7 RID: 37063 RVA: 0x0013C18C File Offset: 0x0013A38C
		public virtual void Format(TextWriter writer, object state)
		{
			if (this.m_min < 0 && this.m_max == 2147483647)
			{
				this.Convert(writer, state);
				return;
			}
			string text = null;
			ReusableStringWriter formatWriter = this.m_formatWriter;
			int num;
			lock (formatWriter)
			{
				this.m_formatWriter.Reset(1024, 256);
				this.Convert(this.m_formatWriter, state);
				StringBuilder stringBuilder = this.m_formatWriter.GetStringBuilder();
				num = stringBuilder.Length;
				if (num > this.m_max)
				{
					text = stringBuilder.ToString(num - this.m_max, this.m_max);
					num = this.m_max;
				}
				else
				{
					text = stringBuilder.ToString();
				}
			}
			if (num >= this.m_min)
			{
				writer.Write(text);
				return;
			}
			if (this.m_leftAlign)
			{
				writer.Write(text);
				PatternConverter.SpacePad(writer, this.m_min - num);
				return;
			}
			PatternConverter.SpacePad(writer, this.m_min - num);
			writer.Write(text);
		}

		// Token: 0x060090C8 RID: 37064 RVA: 0x0013C288 File Offset: 0x0013A488
		protected static void SpacePad(TextWriter writer, int length)
		{
			while (length >= 32)
			{
				writer.Write(PatternConverter.SPACES[5]);
				length -= 32;
			}
			for (int i = 4; i >= 0; i--)
			{
				if ((length & (1 << i)) != 0)
				{
					writer.Write(PatternConverter.SPACES[i]);
				}
			}
		}

		// Token: 0x060090C9 RID: 37065 RVA: 0x00056A5A File Offset: 0x00054C5A
		protected static void WriteDictionary(TextWriter writer, ILoggerRepository repository, IDictionary value)
		{
			PatternConverter.WriteDictionary(writer, repository, value.GetEnumerator());
		}

		// Token: 0x060090CA RID: 37066 RVA: 0x0013C2D4 File Offset: 0x0013A4D4
		protected static void WriteDictionary(TextWriter writer, ILoggerRepository repository, IDictionaryEnumerator value)
		{
			writer.Write("{");
			bool flag = true;
			while (value.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					writer.Write(", ");
				}
				PatternConverter.WriteObject(writer, repository, value.Key);
				writer.Write("=");
				PatternConverter.WriteObject(writer, repository, value.Value);
			}
			writer.Write("}");
		}

		// Token: 0x060090CB RID: 37067 RVA: 0x00056A69 File Offset: 0x00054C69
		protected static void WriteObject(TextWriter writer, ILoggerRepository repository, object value)
		{
			if (repository != null)
			{
				repository.RendererMap.FindAndRender(value, writer);
				return;
			}
			if (value == null)
			{
				writer.Write(SystemInfo.NullText);
				return;
			}
			writer.Write(value.ToString());
		}

		// Token: 0x04006101 RID: 24833
		private const int c_renderBufferSize = 256;

		// Token: 0x04006102 RID: 24834
		private const int c_renderBufferMaxCapacity = 1024;

		// Token: 0x04006103 RID: 24835
		private static readonly string[] SPACES = new string[] { " ", "  ", "    ", "        ", "                ", "                                " };

		// Token: 0x04006104 RID: 24836
		private readonly ReusableStringWriter m_formatWriter = new ReusableStringWriter(CultureInfo.InvariantCulture);

		// Token: 0x04006105 RID: 24837
		private bool m_leftAlign;

		// Token: 0x04006106 RID: 24838
		private int m_max = int.MaxValue;

		// Token: 0x04006107 RID: 24839
		private int m_min = -1;

		// Token: 0x04006108 RID: 24840
		private PatternConverter m_next;
	}
}
