using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;

namespace log4net.Util
{
	// Token: 0x020029DE RID: 10718
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Transform
	{
		// Token: 0x06009176 RID: 37238 RVA: 0x00005698 File Offset: 0x00003898
		private Transform()
		{
		}

		// Token: 0x06009177 RID: 37239 RVA: 0x0013D580 File Offset: 0x0013B780
		public static void WriteEscapedXmlString(XmlWriter writer, string textData, string invalidCharReplacement)
		{
			string text = Transform.MaskXmlInvalidCharacters(textData, invalidCharReplacement);
			int num = 12 * (1 + Transform.CountSubstrings(text, "]]>"));
			if (3 * (Transform.CountSubstrings(text, "<") + Transform.CountSubstrings(text, ">")) + 4 * Transform.CountSubstrings(text, "&") <= num)
			{
				writer.WriteString(text);
				return;
			}
			int i = text.IndexOf("]]>");
			if (i < 0)
			{
				writer.WriteCData(text);
				return;
			}
			int num2 = 0;
			while (i > -1)
			{
				writer.WriteCData(text.Substring(num2, i - num2));
				if (i == text.Length - 3)
				{
					num2 = text.Length;
					writer.WriteString("]]>");
					break;
				}
				writer.WriteString("]]");
				num2 = i + 2;
				i = text.IndexOf("]]>", num2);
			}
			if (num2 < text.Length)
			{
				writer.WriteCData(text.Substring(num2));
			}
		}

		// Token: 0x06009178 RID: 37240 RVA: 0x000571BB File Offset: 0x000553BB
		public static string MaskXmlInvalidCharacters(string textData, string mask)
		{
			return Transform.INVALIDCHARS.Replace(textData, mask);
		}

		// Token: 0x06009179 RID: 37241 RVA: 0x0013D65C File Offset: 0x0013B85C
		private static int CountSubstrings(string text, string substring)
		{
			int num = 0;
			int i = 0;
			int length = text.Length;
			int length2 = substring.Length;
			if (length == 0)
			{
				return 0;
			}
			if (length2 == 0)
			{
				return 0;
			}
			while (i < length)
			{
				int num2 = text.IndexOf(substring, i);
				if (num2 == -1)
				{
					break;
				}
				num++;
				i = num2 + length2;
			}
			return num;
		}

		// Token: 0x04006133 RID: 24883
		private const string CDATA_END = "]]>";

		// Token: 0x04006134 RID: 24884
		private const string CDATA_UNESCAPABLE_TOKEN = "]]";

		// Token: 0x04006135 RID: 24885
		private static readonly Regex INVALIDCHARS = new Regex("[^\\x09\\x0A\\x0D\\x20-\\uD7FF\\uE000-\\uFFFD]", RegexOptions.None);
	}
}
