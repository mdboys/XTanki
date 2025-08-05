using System;
using System.Runtime.CompilerServices;

namespace log4net.Core
{
	// Token: 0x02002A58 RID: 10840
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public sealed class Level : IComparable
	{
		// Token: 0x060093E1 RID: 37857 RVA: 0x0005844F File Offset: 0x0005664F
		public Level(int level, string levelName, string displayName)
		{
			if (levelName == null)
			{
				throw new ArgumentNullException("levelName");
			}
			this.Value = level;
			this.Name = string.Intern(levelName);
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			this.DisplayName = displayName;
		}

		// Token: 0x060093E2 RID: 37858 RVA: 0x0005848E File Offset: 0x0005668E
		public Level(int level, string levelName)
			: this(level, levelName, levelName)
		{
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x060093E3 RID: 37859 RVA: 0x00058499 File Offset: 0x00056699
		public string Name { get; }

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x060093E4 RID: 37860 RVA: 0x000584A1 File Offset: 0x000566A1
		public int Value { get; }

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x060093E5 RID: 37861 RVA: 0x000584A9 File Offset: 0x000566A9
		public string DisplayName { get; }

		// Token: 0x060093E6 RID: 37862 RVA: 0x001429B8 File Offset: 0x00140BB8
		public int CompareTo(object r)
		{
			Level level = r as Level;
			if (level != null)
			{
				return Level.Compare(this, level);
			}
			throw new ArgumentException(string.Format("Parameter: r, Value: [{0}] is not an instance of Level", r));
		}

		// Token: 0x060093E7 RID: 37863 RVA: 0x000584B1 File Offset: 0x000566B1
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060093E8 RID: 37864 RVA: 0x001429F0 File Offset: 0x00140BF0
		public override bool Equals(object o)
		{
			Level level = o as Level;
			if (level != null)
			{
				return this.Value == level.Value;
			}
			return base.Equals(o);
		}

		// Token: 0x060093E9 RID: 37865 RVA: 0x000584B9 File Offset: 0x000566B9
		public override int GetHashCode()
		{
			return this.Value;
		}

		// Token: 0x060093EA RID: 37866 RVA: 0x000584C1 File Offset: 0x000566C1
		public static bool operator >(Level l, Level r)
		{
			return l.Value > r.Value;
		}

		// Token: 0x060093EB RID: 37867 RVA: 0x000584D1 File Offset: 0x000566D1
		public static bool operator <(Level l, Level r)
		{
			return l.Value < r.Value;
		}

		// Token: 0x060093EC RID: 37868 RVA: 0x000584E1 File Offset: 0x000566E1
		public static bool operator >=(Level l, Level r)
		{
			return l.Value >= r.Value;
		}

		// Token: 0x060093ED RID: 37869 RVA: 0x000584F4 File Offset: 0x000566F4
		public static bool operator <=(Level l, Level r)
		{
			return l.Value <= r.Value;
		}

		// Token: 0x060093EE RID: 37870 RVA: 0x00058507 File Offset: 0x00056707
		public static bool operator ==(Level l, Level r)
		{
			if (l != null && r != null)
			{
				return l.Value == r.Value;
			}
			return l == r;
		}

		// Token: 0x060093EF RID: 37871 RVA: 0x00058522 File Offset: 0x00056722
		public static bool operator !=(Level l, Level r)
		{
			return !(l == r);
		}

		// Token: 0x060093F0 RID: 37872 RVA: 0x00142A24 File Offset: 0x00140C24
		public static int Compare(Level l, Level r)
		{
			if (l == r)
			{
				return 0;
			}
			if (l == null && r == null)
			{
				return 0;
			}
			if (l == null)
			{
				return -1;
			}
			if (r == null)
			{
				return 1;
			}
			return l.Value.CompareTo(r.Value);
		}

		// Token: 0x04006204 RID: 25092
		public static readonly Level Off = new Level(int.MaxValue, "OFF");

		// Token: 0x04006205 RID: 25093
		public static readonly Level Log4Net_Debug = new Level(120000, "log4net:DEBUG");

		// Token: 0x04006206 RID: 25094
		public static readonly Level Emergency = new Level(120000, "EMERGENCY");

		// Token: 0x04006207 RID: 25095
		public static readonly Level Fatal = new Level(110000, "FATAL");

		// Token: 0x04006208 RID: 25096
		public static readonly Level Alert = new Level(100000, "ALERT");

		// Token: 0x04006209 RID: 25097
		public static readonly Level Critical = new Level(90000, "CRITICAL");

		// Token: 0x0400620A RID: 25098
		public static readonly Level Severe = new Level(80000, "SEVERE");

		// Token: 0x0400620B RID: 25099
		public static readonly Level Error = new Level(70000, "ERROR");

		// Token: 0x0400620C RID: 25100
		public static readonly Level Warn = new Level(60000, "WARN");

		// Token: 0x0400620D RID: 25101
		public static readonly Level Notice = new Level(50000, "NOTICE");

		// Token: 0x0400620E RID: 25102
		public static readonly Level Info = new Level(40000, "INFO");

		// Token: 0x0400620F RID: 25103
		public static readonly Level Debug = new Level(30000, "DEBUG");

		// Token: 0x04006210 RID: 25104
		public static readonly Level Fine = new Level(30000, "FINE");

		// Token: 0x04006211 RID: 25105
		public static readonly Level Trace = new Level(20000, "TRACE");

		// Token: 0x04006212 RID: 25106
		public static readonly Level Finer = new Level(20000, "FINER");

		// Token: 0x04006213 RID: 25107
		public static readonly Level Verbose = new Level(10000, "VERBOSE");

		// Token: 0x04006214 RID: 25108
		public static readonly Level Finest = new Level(10000, "FINEST");

		// Token: 0x04006215 RID: 25109
		public static readonly Level All = new Level(int.MinValue, "ALL");
	}
}
