using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02002A45 RID: 10821
	[NullableContext(1)]
	[Nullable(0)]
	public class PropertyFilter : StringMatchFilter
	{
		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x0600939B RID: 37787 RVA: 0x000582ED File Offset: 0x000564ED
		// (set) Token: 0x0600939C RID: 37788 RVA: 0x000582F5 File Offset: 0x000564F5
		public string Key { get; set; }

		// Token: 0x0600939D RID: 37789 RVA: 0x00142288 File Offset: 0x00140488
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.Key == null)
			{
				return FilterDecision.Neutral;
			}
			object obj = loggingEvent.LookupProperty(this.Key);
			string text = loggingEvent.Repository.RendererMap.FindAndRender(obj);
			if (text == null || (this.m_stringToMatch == null && this.m_regexToMatch == null))
			{
				return FilterDecision.Neutral;
			}
			if (this.m_regexToMatch != null)
			{
				if (!this.m_regexToMatch.Match(text).Success)
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
				if (text.IndexOf(this.m_stringToMatch) == -1)
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
	}
}
