using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Layout.Pattern;
using log4net.Util;
using log4net.Util.PatternStringConverters;

namespace log4net.Layout
{
	// Token: 0x02002A1D RID: 10781
	[NullableContext(1)]
	[Nullable(0)]
	public class PatternLayout : LayoutSkeleton
	{
		// Token: 0x06009310 RID: 37648 RVA: 0x00057E71 File Offset: 0x00056071
		public PatternLayout()
			: this("%message%newline")
		{
		}

		// Token: 0x06009311 RID: 37649 RVA: 0x00057E7E File Offset: 0x0005607E
		public PatternLayout(string pattern)
		{
			this.IgnoresException = true;
			this.ConversionPattern = pattern;
			if (this.ConversionPattern == null)
			{
				this.ConversionPattern = "%message%newline";
			}
			this.ActivateOptions();
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06009312 RID: 37650 RVA: 0x00057EB8 File Offset: 0x000560B8
		// (set) Token: 0x06009313 RID: 37651 RVA: 0x00057EC0 File Offset: 0x000560C0
		public string ConversionPattern { get; set; }

		// Token: 0x06009314 RID: 37652 RVA: 0x00141174 File Offset: 0x0013F374
		protected virtual PatternParser CreatePatternParser(string pattern)
		{
			PatternParser patternParser = new PatternParser(pattern);
			foreach (object obj in PatternLayout.s_globalRulesRegistry)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				ConverterInfo converterInfo = new ConverterInfo
				{
					Name = (string)dictionaryEntry.Key,
					Type = (Type)dictionaryEntry.Value
				};
				patternParser.PatternConverters[dictionaryEntry.Key] = converterInfo;
			}
			foreach (object obj2 in this.m_instanceRulesRegistry)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				patternParser.PatternConverters[dictionaryEntry2.Key] = dictionaryEntry2.Value;
			}
			return patternParser;
		}

		// Token: 0x06009315 RID: 37653 RVA: 0x0014126C File Offset: 0x0013F46C
		public override void ActivateOptions()
		{
			this.m_head = this.CreatePatternParser(this.ConversionPattern).Parse();
			for (PatternConverter patternConverter = this.m_head; patternConverter != null; patternConverter = patternConverter.Next)
			{
				PatternLayoutConverter patternLayoutConverter = patternConverter as PatternLayoutConverter;
				if (patternLayoutConverter != null && !patternLayoutConverter.IgnoresException)
				{
					this.IgnoresException = false;
					return;
				}
			}
		}

		// Token: 0x06009316 RID: 37654 RVA: 0x001412C0 File Offset: 0x0013F4C0
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			for (PatternConverter patternConverter = this.m_head; patternConverter != null; patternConverter = patternConverter.Next)
			{
				patternConverter.Format(writer, loggingEvent);
			}
		}

		// Token: 0x06009317 RID: 37655 RVA: 0x00141304 File Offset: 0x0013F504
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

		// Token: 0x06009318 RID: 37656 RVA: 0x00141364 File Offset: 0x0013F564
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

		// Token: 0x0400619C RID: 24988
		public const string DefaultConversionPattern = "%message%newline";

		// Token: 0x0400619D RID: 24989
		public const string DetailConversionPattern = "%timestamp [%thread] %level %logger %ndc - %message%newline";

		// Token: 0x0400619E RID: 24990
		private static readonly Hashtable s_globalRulesRegistry = new Hashtable(45)
		{
			{
				"literal",
				typeof(LiteralPatternConverter)
			},
			{
				"newline",
				typeof(NewLinePatternConverter)
			},
			{
				"n",
				typeof(NewLinePatternConverter)
			},
			{
				"c",
				typeof(LoggerPatternConverter)
			},
			{
				"logger",
				typeof(LoggerPatternConverter)
			},
			{
				"C",
				typeof(TypeNamePatternConverter)
			},
			{
				"class",
				typeof(TypeNamePatternConverter)
			},
			{
				"type",
				typeof(TypeNamePatternConverter)
			},
			{
				"d",
				typeof(log4net.Layout.Pattern.DatePatternConverter)
			},
			{
				"date",
				typeof(log4net.Layout.Pattern.DatePatternConverter)
			},
			{
				"exception",
				typeof(ExceptionPatternConverter)
			},
			{
				"F",
				typeof(FileLocationPatternConverter)
			},
			{
				"file",
				typeof(FileLocationPatternConverter)
			},
			{
				"l",
				typeof(FullLocationPatternConverter)
			},
			{
				"location",
				typeof(FullLocationPatternConverter)
			},
			{
				"L",
				typeof(LineLocationPatternConverter)
			},
			{
				"line",
				typeof(LineLocationPatternConverter)
			},
			{
				"m",
				typeof(MessagePatternConverter)
			},
			{
				"message",
				typeof(MessagePatternConverter)
			},
			{
				"M",
				typeof(MethodLocationPatternConverter)
			},
			{
				"method",
				typeof(MethodLocationPatternConverter)
			},
			{
				"p",
				typeof(LevelPatternConverter)
			},
			{
				"level",
				typeof(LevelPatternConverter)
			},
			{
				"P",
				typeof(log4net.Layout.Pattern.PropertyPatternConverter)
			},
			{
				"property",
				typeof(log4net.Layout.Pattern.PropertyPatternConverter)
			},
			{
				"properties",
				typeof(log4net.Layout.Pattern.PropertyPatternConverter)
			},
			{
				"r",
				typeof(RelativeTimePatternConverter)
			},
			{
				"timestamp",
				typeof(RelativeTimePatternConverter)
			},
			{
				"stacktrace",
				typeof(StackTracePatternConverter)
			},
			{
				"stacktracedetail",
				typeof(StackTraceDetailPatternConverter)
			},
			{
				"t",
				typeof(ThreadPatternConverter)
			},
			{
				"thread",
				typeof(ThreadPatternConverter)
			},
			{
				"x",
				typeof(NdcPatternConverter)
			},
			{
				"ndc",
				typeof(NdcPatternConverter)
			},
			{
				"X",
				typeof(log4net.Layout.Pattern.PropertyPatternConverter)
			},
			{
				"mdc",
				typeof(log4net.Layout.Pattern.PropertyPatternConverter)
			},
			{
				"a",
				typeof(log4net.Layout.Pattern.AppDomainPatternConverter)
			},
			{
				"appdomain",
				typeof(log4net.Layout.Pattern.AppDomainPatternConverter)
			},
			{
				"u",
				typeof(log4net.Layout.Pattern.IdentityPatternConverter)
			},
			{
				"identity",
				typeof(log4net.Layout.Pattern.IdentityPatternConverter)
			},
			{
				"utcdate",
				typeof(log4net.Layout.Pattern.UtcDatePatternConverter)
			},
			{
				"utcDate",
				typeof(log4net.Layout.Pattern.UtcDatePatternConverter)
			},
			{
				"UtcDate",
				typeof(log4net.Layout.Pattern.UtcDatePatternConverter)
			},
			{
				"w",
				typeof(log4net.Layout.Pattern.UserNamePatternConverter)
			},
			{
				"username",
				typeof(log4net.Layout.Pattern.UserNamePatternConverter)
			}
		};

		// Token: 0x0400619F RID: 24991
		private PatternConverter m_head;

		// Token: 0x040061A0 RID: 24992
		private readonly Hashtable m_instanceRulesRegistry = new Hashtable();
	}
}
