using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02002A46 RID: 10822
	[NullableContext(1)]
	[Nullable(0)]
	public class StringMatchFilter : FilterSkeleton
	{
		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x0600939F RID: 37791 RVA: 0x00058306 File Offset: 0x00056506
		// (set) Token: 0x060093A0 RID: 37792 RVA: 0x0005830E File Offset: 0x0005650E
		public bool AcceptOnMatch
		{
			get
			{
				return this.m_acceptOnMatch;
			}
			set
			{
				this.m_acceptOnMatch = value;
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x060093A1 RID: 37793 RVA: 0x00058317 File Offset: 0x00056517
		// (set) Token: 0x060093A2 RID: 37794 RVA: 0x0005831F File Offset: 0x0005651F
		public string StringToMatch
		{
			get
			{
				return this.m_stringToMatch;
			}
			set
			{
				this.m_stringToMatch = value;
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x060093A3 RID: 37795 RVA: 0x00058328 File Offset: 0x00056528
		// (set) Token: 0x060093A4 RID: 37796 RVA: 0x00058330 File Offset: 0x00056530
		public string RegexToMatch
		{
			get
			{
				return this.m_stringRegexToMatch;
			}
			set
			{
				this.m_stringRegexToMatch = value;
			}
		}

		// Token: 0x060093A5 RID: 37797 RVA: 0x00058339 File Offset: 0x00056539
		public override void ActivateOptions()
		{
			if (this.m_stringRegexToMatch != null)
			{
				this.m_regexToMatch = new Regex(this.m_stringRegexToMatch, RegexOptions.None);
			}
		}

		// Token: 0x060093A6 RID: 37798 RVA: 0x00142330 File Offset: 0x00140530
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			string renderedMessage = loggingEvent.RenderedMessage;
			if (renderedMessage == null || (this.m_stringToMatch == null && this.m_regexToMatch == null))
			{
				return FilterDecision.Neutral;
			}
			if (this.m_regexToMatch != null)
			{
				if (!this.m_regexToMatch.Match(renderedMessage).Success)
				{
					return FilterDecision.Neutral;
				}
				if (this.m_acceptOnMatch)
				{
					return FilterDecision.Accept;
				}
				return FilterDecision.Deny;
			}
			else
			{
				if (this.m_stringToMatch == null)
				{
					return FilterDecision.Neutral;
				}
				if (renderedMessage.IndexOf(this.m_stringToMatch) == -1)
				{
					return FilterDecision.Neutral;
				}
				if (this.m_acceptOnMatch)
				{
					return FilterDecision.Accept;
				}
				return FilterDecision.Deny;
			}
		}

		// Token: 0x040061DB RID: 25051
		protected bool m_acceptOnMatch = true;

		// Token: 0x040061DC RID: 25052
		protected Regex m_regexToMatch;

		// Token: 0x040061DD RID: 25053
		protected string m_stringRegexToMatch;

		// Token: 0x040061DE RID: 25054
		protected string m_stringToMatch;
	}
}
