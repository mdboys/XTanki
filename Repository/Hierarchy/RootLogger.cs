using System;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x02002A08 RID: 10760
	[NullableContext(1)]
	[Nullable(0)]
	public class RootLogger : Logger
	{
		// Token: 0x06009272 RID: 37490 RVA: 0x000578BE File Offset: 0x00055ABE
		public RootLogger(Level level)
			: base("root")
		{
			this.Level = level;
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06009273 RID: 37491 RVA: 0x000578D2 File Offset: 0x00055AD2
		public override Level EffectiveLevel
		{
			get
			{
				return base.Level;
			}
		}

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06009274 RID: 37492 RVA: 0x000578D2 File Offset: 0x00055AD2
		// (set) Token: 0x06009275 RID: 37493 RVA: 0x000578DA File Offset: 0x00055ADA
		public override Level Level
		{
			get
			{
				return base.Level;
			}
			set
			{
				if (value == null)
				{
					LogLog.Error(RootLogger.declaringType, "You have tried to set a null level to root.", new LogException());
					return;
				}
				base.Level = value;
			}
		}

		// Token: 0x04006166 RID: 24934
		private static readonly Type declaringType = typeof(RootLogger);
	}
}
