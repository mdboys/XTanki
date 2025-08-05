using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A7B RID: 10875
	[NullableContext(1)]
	[Nullable(0)]
	public class AnsiColorTerminalAppender : AppenderSkeleton
	{
		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06009532 RID: 38194 RVA: 0x000594EC File Offset: 0x000576EC
		// (set) Token: 0x06009533 RID: 38195 RVA: 0x0014524C File Offset: 0x0014344C
		public virtual string Target
		{
			get
			{
				if (this.m_writeToErrorStream)
				{
					return "Console.Error";
				}
				return "Console.Out";
			}
			set
			{
				string text = value.Trim();
				if (string.Compare("Console.Error", text, true, CultureInfo.InvariantCulture) == 0)
				{
					this.m_writeToErrorStream = true;
					return;
				}
				this.m_writeToErrorStream = false;
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06009534 RID: 38196 RVA: 0x00005B7A File Offset: 0x00003D7A
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009535 RID: 38197 RVA: 0x00059501 File Offset: 0x00057701
		public void AddMapping(AnsiColorTerminalAppender.LevelColors mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x06009536 RID: 38198 RVA: 0x00145284 File Offset: 0x00143484
		protected override void Append(LoggingEvent loggingEvent)
		{
			string text = base.RenderLoggingEvent(loggingEvent);
			AnsiColorTerminalAppender.LevelColors levelColors = this.m_levelMapping.Lookup(loggingEvent.Level) as AnsiColorTerminalAppender.LevelColors;
			if (levelColors != null)
			{
				text = levelColors.CombinedColor + text;
			}
			text = ((text.Length > 1) ? ((!text.EndsWith("\r\n") && !text.EndsWith("\n\r")) ? ((!text.EndsWith("\n") && !text.EndsWith("\r")) ? (text + "\u001b[0m") : text.Insert(text.Length - 1, "\u001b[0m")) : text.Insert(text.Length - 2, "\u001b[0m")) : ((text[0] != '\n' && text[0] != '\r') ? (text + "\u001b[0m") : ("\u001b[0m" + text)));
			if (this.m_writeToErrorStream)
			{
				Console.Error.Write(text);
				return;
			}
			Console.Write(text);
		}

		// Token: 0x06009537 RID: 38199 RVA: 0x0005950F File Offset: 0x0005770F
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			this.m_levelMapping.ActivateOptions();
		}

		// Token: 0x04006273 RID: 25203
		public const string ConsoleOut = "Console.Out";

		// Token: 0x04006274 RID: 25204
		public const string ConsoleError = "Console.Error";

		// Token: 0x04006275 RID: 25205
		private const string PostEventCodes = "\u001b[0m";

		// Token: 0x04006276 RID: 25206
		private readonly LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x04006277 RID: 25207
		private bool m_writeToErrorStream;

		// Token: 0x02002A7C RID: 10876
		[NullableContext(0)]
		[Flags]
		public enum AnsiAttributes
		{
			// Token: 0x04006279 RID: 25209
			Bright = 1,
			// Token: 0x0400627A RID: 25210
			Dim = 2,
			// Token: 0x0400627B RID: 25211
			Underscore = 4,
			// Token: 0x0400627C RID: 25212
			Blink = 8,
			// Token: 0x0400627D RID: 25213
			Reverse = 16,
			// Token: 0x0400627E RID: 25214
			Hidden = 32,
			// Token: 0x0400627F RID: 25215
			Strikethrough = 64,
			// Token: 0x04006280 RID: 25216
			Light = 128
		}

		// Token: 0x02002A7D RID: 10877
		[NullableContext(0)]
		public enum AnsiColor
		{
			// Token: 0x04006282 RID: 25218
			Black,
			// Token: 0x04006283 RID: 25219
			Red,
			// Token: 0x04006284 RID: 25220
			Green,
			// Token: 0x04006285 RID: 25221
			Yellow,
			// Token: 0x04006286 RID: 25222
			Blue,
			// Token: 0x04006287 RID: 25223
			Magenta,
			// Token: 0x04006288 RID: 25224
			Cyan,
			// Token: 0x04006289 RID: 25225
			White
		}

		// Token: 0x02002A7E RID: 10878
		[Nullable(0)]
		public class LevelColors : LevelMappingEntry
		{
			// Token: 0x1700175D RID: 5981
			// (get) Token: 0x06009539 RID: 38201 RVA: 0x00059535 File Offset: 0x00057735
			// (set) Token: 0x0600953A RID: 38202 RVA: 0x0005953D File Offset: 0x0005773D
			public AnsiColorTerminalAppender.AnsiColor ForeColor { get; set; }

			// Token: 0x1700175E RID: 5982
			// (get) Token: 0x0600953B RID: 38203 RVA: 0x00059546 File Offset: 0x00057746
			// (set) Token: 0x0600953C RID: 38204 RVA: 0x0005954E File Offset: 0x0005774E
			public AnsiColorTerminalAppender.AnsiColor BackColor { get; set; }

			// Token: 0x1700175F RID: 5983
			// (get) Token: 0x0600953D RID: 38205 RVA: 0x00059557 File Offset: 0x00057757
			// (set) Token: 0x0600953E RID: 38206 RVA: 0x0005955F File Offset: 0x0005775F
			public AnsiColorTerminalAppender.AnsiAttributes Attributes { get; set; }

			// Token: 0x17001760 RID: 5984
			// (get) Token: 0x0600953F RID: 38207 RVA: 0x00059568 File Offset: 0x00057768
			// (set) Token: 0x06009540 RID: 38208 RVA: 0x00059570 File Offset: 0x00057770
			internal string CombinedColor { get; private set; } = string.Empty;

			// Token: 0x06009541 RID: 38209 RVA: 0x00145380 File Offset: 0x00143580
			public override void ActivateOptions()
			{
				base.ActivateOptions();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("\u001b[0;");
				int num = (((this.Attributes & AnsiColorTerminalAppender.AnsiAttributes.Light) > (AnsiColorTerminalAppender.AnsiAttributes)0) ? 60 : 0);
				stringBuilder.Append((int)(30 + num + this.ForeColor));
				stringBuilder.Append(';');
				stringBuilder.Append((int)(40 + num + this.BackColor));
				if ((this.Attributes & AnsiColorTerminalAppender.AnsiAttributes.Bright) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";1");
				}
				if ((this.Attributes & AnsiColorTerminalAppender.AnsiAttributes.Dim) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";2");
				}
				if ((this.Attributes & AnsiColorTerminalAppender.AnsiAttributes.Underscore) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";4");
				}
				if ((this.Attributes & AnsiColorTerminalAppender.AnsiAttributes.Blink) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";5");
				}
				if ((this.Attributes & AnsiColorTerminalAppender.AnsiAttributes.Reverse) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";7");
				}
				if ((this.Attributes & AnsiColorTerminalAppender.AnsiAttributes.Hidden) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";8");
				}
				if ((this.Attributes & AnsiColorTerminalAppender.AnsiAttributes.Strikethrough) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";9");
				}
				stringBuilder.Append('m');
				this.CombinedColor = stringBuilder.ToString();
			}
		}
	}
}
