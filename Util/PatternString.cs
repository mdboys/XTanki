using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util.PatternStringConverters;

namespace log4net.Util
{
	// Token: 0x020029CE RID: 10702
	[NullableContext(1)]
	[Nullable(0)]
	public class PatternString : IOptionHandler
	{
		// Token: 0x060090DA RID: 37082 RVA: 0x0013C758 File Offset: 0x0013A958
		static PatternString()
		{
			PatternString.s_globalRulesRegistry.Add("appdomain", typeof(AppDomainPatternConverter));
			PatternString.s_globalRulesRegistry.Add("date", typeof(DatePatternConverter));
			PatternString.s_globalRulesRegistry.Add("env", typeof(EnvironmentPatternConverter));
			PatternString.s_globalRulesRegistry.Add("envFolderPath", typeof(EnvironmentFolderPathPatternConverter));
			PatternString.s_globalRulesRegistry.Add("identity", typeof(IdentityPatternConverter));
			PatternString.s_globalRulesRegistry.Add("literal", typeof(LiteralPatternConverter));
			PatternString.s_globalRulesRegistry.Add("newline", typeof(NewLinePatternConverter));
			PatternString.s_globalRulesRegistry.Add("processid", typeof(ProcessIdPatternConverter));
			PatternString.s_globalRulesRegistry.Add("property", typeof(PropertyPatternConverter));
			PatternString.s_globalRulesRegistry.Add("random", typeof(RandomStringPatternConverter));
			PatternString.s_globalRulesRegistry.Add("username", typeof(UserNamePatternConverter));
			PatternString.s_globalRulesRegistry.Add("utcdate", typeof(UtcDatePatternConverter));
			PatternString.s_globalRulesRegistry.Add("utcDate", typeof(UtcDatePatternConverter));
			PatternString.s_globalRulesRegistry.Add("UtcDate", typeof(UtcDatePatternConverter));
		}

		// Token: 0x060090DB RID: 37083 RVA: 0x00056B59 File Offset: 0x00054D59
		public PatternString()
		{
		}

		// Token: 0x060090DC RID: 37084 RVA: 0x00056B6C File Offset: 0x00054D6C
		public PatternString(string pattern)
		{
			this.ConversionPattern = pattern;
			this.ActivateOptions();
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x060090DD RID: 37085 RVA: 0x00056B8C File Offset: 0x00054D8C
		// (set) Token: 0x060090DE RID: 37086 RVA: 0x00056B94 File Offset: 0x00054D94
		public string ConversionPattern { get; set; }

		// Token: 0x060090DF RID: 37087 RVA: 0x00056B9D File Offset: 0x00054D9D
		public virtual void ActivateOptions()
		{
			this.m_head = this.CreatePatternParser(this.ConversionPattern).Parse();
		}

		// Token: 0x060090E0 RID: 37088 RVA: 0x0013C8D0 File Offset: 0x0013AAD0
		private PatternParser CreatePatternParser(string pattern)
		{
			PatternParser patternParser = new PatternParser(pattern);
			foreach (object obj in PatternString.s_globalRulesRegistry)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				ConverterInfo converterInfo = new ConverterInfo
				{
					Name = (string)dictionaryEntry.Key,
					Type = (Type)dictionaryEntry.Value
				};
				patternParser.PatternConverters.Add(dictionaryEntry.Key, converterInfo);
			}
			foreach (object obj2 in this.m_instanceRulesRegistry)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				patternParser.PatternConverters[dictionaryEntry2.Key] = dictionaryEntry2.Value;
			}
			return patternParser;
		}

		// Token: 0x060090E1 RID: 37089 RVA: 0x0013C9C8 File Offset: 0x0013ABC8
		public void Format(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			for (PatternConverter patternConverter = this.m_head; patternConverter != null; patternConverter = patternConverter.Next)
			{
				patternConverter.Format(writer, null);
			}
		}

		// Token: 0x060090E2 RID: 37090 RVA: 0x0013CA00 File Offset: 0x0013AC00
		public string Format()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.Format(stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x060090E3 RID: 37091 RVA: 0x0013CA28 File Offset: 0x0013AC28
		public void AddConverter(ConverterInfo converterInfo)
		{
			if (converterInfo == null)
			{
				throw new ArgumentNullException("converterInfo");
			}
			if (!typeof(PatternConverter).IsAssignableFrom(converterInfo.Type))
			{
				throw new ArgumentException(string.Format("The converter type specified [{0}] must be a subclass of log4net.Util.PatternConverter", converterInfo.Type), "converterInfo");
			}
			this.m_instanceRulesRegistry[converterInfo.Name] = converterInfo;
		}

		// Token: 0x060090E4 RID: 37092 RVA: 0x0013CA88 File Offset: 0x0013AC88
		public void AddConverter(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ConverterInfo converterInfo = new ConverterInfo
			{
				Name = name,
				Type = type
			};
			this.AddConverter(converterInfo);
		}

		// Token: 0x04006112 RID: 24850
		private static readonly Hashtable s_globalRulesRegistry = new Hashtable(15);

		// Token: 0x04006113 RID: 24851
		private PatternConverter m_head;

		// Token: 0x04006114 RID: 24852
		private readonly Hashtable m_instanceRulesRegistry = new Hashtable();
	}
}
