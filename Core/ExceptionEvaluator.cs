using System;
using System.Runtime.CompilerServices;

namespace log4net.Core
{
	// Token: 0x02002A4E RID: 10830
	[NullableContext(1)]
	[Nullable(0)]
	public class ExceptionEvaluator : ITriggeringEventEvaluator
	{
		// Token: 0x060093C0 RID: 37824 RVA: 0x00005698 File Offset: 0x00003898
		public ExceptionEvaluator()
		{
		}

		// Token: 0x060093C1 RID: 37825 RVA: 0x00058409 File Offset: 0x00056609
		public ExceptionEvaluator(Type exType, bool triggerOnSubClass)
		{
			if (exType == null)
			{
				throw new ArgumentNullException("exType");
			}
			this.ExceptionType = exType;
			this.TriggerOnSubclass = triggerOnSubClass;
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x060093C2 RID: 37826 RVA: 0x0005842D File Offset: 0x0005662D
		// (set) Token: 0x060093C3 RID: 37827 RVA: 0x00058435 File Offset: 0x00056635
		public Type ExceptionType { get; set; }

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x060093C4 RID: 37828 RVA: 0x0005843E File Offset: 0x0005663E
		// (set) Token: 0x060093C5 RID: 37829 RVA: 0x00058446 File Offset: 0x00056646
		public bool TriggerOnSubclass { get; set; }

		// Token: 0x060093C6 RID: 37830 RVA: 0x00142944 File Offset: 0x00140B44
		public bool IsTriggeringEvent(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.TriggerOnSubclass && loggingEvent.ExceptionObject != null)
			{
				Type type = loggingEvent.ExceptionObject.GetType();
				return type == this.ExceptionType || type.IsSubclassOf(this.ExceptionType);
			}
			return !this.TriggerOnSubclass && loggingEvent.ExceptionObject != null && loggingEvent.ExceptionObject.GetType() == this.ExceptionType;
		}
	}
}
