using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029CC RID: 10700
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PatternParser
	{
		// Token: 0x060090CE RID: 37070 RVA: 0x00056AFE File Offset: 0x00054CFE
		public PatternParser(string pattern)
		{
			this.m_pattern = pattern;
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x060090CF RID: 37071 RVA: 0x00056B18 File Offset: 0x00054D18
		public Hashtable PatternConverters { get; } = new Hashtable();

		// Token: 0x060090D0 RID: 37072 RVA: 0x0013C33C File Offset: 0x0013A53C
		public PatternConverter Parse()
		{
			string[] array = this.BuildCache();
			this.ParseInternal(this.m_pattern, array);
			return this.m_head;
		}

		// Token: 0x060090D1 RID: 37073 RVA: 0x0013C364 File Offset: 0x0013A564
		private string[] BuildCache()
		{
			string[] array = new string[this.PatternConverters.Keys.Count];
			this.PatternConverters.Keys.CopyTo(array, 0);
			Array.Sort(array, 0, array.Length, PatternParser.StringLengthComparer.Instance);
			return array;
		}

		// Token: 0x060090D2 RID: 37074 RVA: 0x0013C3AC File Offset: 0x0013A5AC
		private void ParseInternal(string pattern, string[] matches)
		{
			int i = 0;
			while (i < pattern.Length)
			{
				int num = pattern.IndexOf('%', i);
				if (num < 0 || num == pattern.Length - 1)
				{
					this.ProcessLiteral(pattern.Substring(i));
					i = pattern.Length;
				}
				else if (pattern[num + 1] == '%')
				{
					this.ProcessLiteral(pattern.Substring(i, num - i + 1));
					i = num + 2;
				}
				else
				{
					this.ProcessLiteral(pattern.Substring(i, num - i));
					i = num + 1;
					FormattingInfo formattingInfo = new FormattingInfo();
					if (i < pattern.Length && pattern[i] == '-')
					{
						formattingInfo.LeftAlign = true;
						i++;
					}
					while (i < pattern.Length && char.IsDigit(pattern[i]))
					{
						if (formattingInfo.Min < 0)
						{
							formattingInfo.Min = 0;
						}
						formattingInfo.Min = formattingInfo.Min * 10 + int.Parse(pattern[i].ToString(CultureInfo.InvariantCulture), NumberFormatInfo.InvariantInfo);
						i++;
					}
					if (i < pattern.Length && pattern[i] == '.')
					{
						i++;
					}
					while (i < pattern.Length && char.IsDigit(pattern[i]))
					{
						if (formattingInfo.Max == 2147483647)
						{
							formattingInfo.Max = 0;
						}
						formattingInfo.Max = formattingInfo.Max * 10 + int.Parse(pattern[i].ToString(CultureInfo.InvariantCulture), NumberFormatInfo.InvariantInfo);
						i++;
					}
					int num2 = pattern.Length - i;
					for (int j = 0; j < matches.Length; j++)
					{
						if (matches[j].Length <= num2 && string.Compare(pattern, i, matches[j], 0, matches[j].Length, false, CultureInfo.InvariantCulture) == 0)
						{
							i += matches[j].Length;
							string text = null;
							if (i < pattern.Length && pattern[i] == '{')
							{
								i++;
								int num3 = pattern.IndexOf('}', i);
								if (num3 >= 0)
								{
									text = pattern.Substring(i, num3 - i);
									i = num3 + 1;
								}
							}
							this.ProcessConverter(matches[j], text, formattingInfo);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060090D3 RID: 37075 RVA: 0x00056B20 File Offset: 0x00054D20
		private void ProcessLiteral(string text)
		{
			if (text.Length > 0)
			{
				this.ProcessConverter("literal", text, new FormattingInfo());
			}
		}

		// Token: 0x060090D4 RID: 37076 RVA: 0x0013C5D8 File Offset: 0x0013A7D8
		private void ProcessConverter(string converterName, string option, FormattingInfo formattingInfo)
		{
			LogLog.Debug(PatternParser.declaringType, string.Format("Converter [{0}] Option [{1}] Format [min={2},max={3},leftAlign={4}]", new object[] { converterName, option, formattingInfo.Min, formattingInfo.Max, formattingInfo.LeftAlign }));
			ConverterInfo converterInfo = (ConverterInfo)this.PatternConverters[converterName];
			if (converterInfo == null)
			{
				LogLog.Error(PatternParser.declaringType, "Unknown converter name [" + converterName + "] in conversion pattern.");
				return;
			}
			PatternConverter patternConverter = null;
			try
			{
				patternConverter = (PatternConverter)Activator.CreateInstance(converterInfo.Type);
			}
			catch (Exception ex)
			{
				LogLog.Error(PatternParser.declaringType, string.Format("Failed to create instance of Type [{0}] using default constructor. Exception: {1}", converterInfo.Type.FullName, ex));
			}
			patternConverter.FormattingInfo = formattingInfo;
			patternConverter.Option = option;
			patternConverter.Properties = converterInfo.Properties;
			IOptionHandler optionHandler = patternConverter as IOptionHandler;
			if (optionHandler != null)
			{
				optionHandler.ActivateOptions();
			}
			this.AddConverter(patternConverter);
		}

		// Token: 0x060090D5 RID: 37077 RVA: 0x0013C6DC File Offset: 0x0013A8DC
		private void AddConverter(PatternConverter pc)
		{
			if (this.m_head == null)
			{
				this.m_tail = pc;
				this.m_head = pc;
				return;
			}
			this.m_tail = this.m_tail.SetNext(pc);
		}

		// Token: 0x0400610B RID: 24843
		private const char ESCAPE_CHAR = '%';

		// Token: 0x0400610C RID: 24844
		private static readonly Type declaringType = typeof(PatternParser);

		// Token: 0x0400610D RID: 24845
		private PatternConverter m_head;

		// Token: 0x0400610E RID: 24846
		private readonly string m_pattern;

		// Token: 0x0400610F RID: 24847
		private PatternConverter m_tail;

		// Token: 0x020029CD RID: 10701
		[NullableContext(0)]
		private sealed class StringLengthComparer : IComparer
		{
			// Token: 0x060090D7 RID: 37079 RVA: 0x00005698 File Offset: 0x00003898
			private StringLengthComparer()
			{
			}

			// Token: 0x060090D8 RID: 37080 RVA: 0x0013C714 File Offset: 0x0013A914
			[NullableContext(1)]
			public int Compare(object x, object y)
			{
				string text = x as string;
				string text2 = y as string;
				if (text == null && text2 == null)
				{
					return 0;
				}
				if (text == null)
				{
					return 1;
				}
				if (text2 == null)
				{
					return -1;
				}
				return text2.Length.CompareTo(text.Length);
			}

			// Token: 0x04006111 RID: 24849
			[Nullable(1)]
			public static readonly PatternParser.StringLengthComparer Instance = new PatternParser.StringLengthComparer();
		}
	}
}
