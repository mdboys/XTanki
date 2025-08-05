using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmplifyBloom
{
	// Token: 0x02002CEC RID: 11500
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class VersionInfo
	{
		// Token: 0x0600A0E7 RID: 41191 RVA: 0x0005D861 File Offset: 0x0005BA61
		private VersionInfo()
		{
			this.m_major = 1;
			this.m_minor = 1;
			this.m_release = 2;
		}

		// Token: 0x0600A0E8 RID: 41192 RVA: 0x0005D87E File Offset: 0x0005BA7E
		private VersionInfo(byte major, byte minor, byte release)
		{
			this.m_major = (int)major;
			this.m_minor = (int)minor;
			this.m_release = (int)release;
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x0600A0E9 RID: 41193 RVA: 0x0005D89B File Offset: 0x0005BA9B
		public int Number
		{
			get
			{
				return this.m_major * 100 + this.m_minor * 10 + this.m_release;
			}
		}

		// Token: 0x0600A0EA RID: 41194 RVA: 0x0005D8B7 File Offset: 0x0005BAB7
		public static string StaticToString()
		{
			return string.Format("{0}.{1}.{2}{3}", new object[]
			{
				1,
				1,
				2,
				VersionInfo.StageSuffix
			});
		}

		// Token: 0x0600A0EB RID: 41195 RVA: 0x00160740 File Offset: 0x0015E940
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}{3}", new object[]
			{
				this.m_major,
				this.m_minor,
				this.m_release,
				VersionInfo.StageSuffix
			});
		}

		// Token: 0x0600A0EC RID: 41196 RVA: 0x0005D8EC File Offset: 0x0005BAEC
		public static VersionInfo Current()
		{
			return new VersionInfo(1, 1, 2);
		}

		// Token: 0x0600A0ED RID: 41197 RVA: 0x0005D8F6 File Offset: 0x0005BAF6
		public static bool Matches(VersionInfo version)
		{
			return version.m_major == 1 && (version != null && version.m_minor == 1) && version.m_release == 2;
		}

		// Token: 0x0400686E RID: 26734
		public const byte Major = 1;

		// Token: 0x0400686F RID: 26735
		public const byte Minor = 1;

		// Token: 0x04006870 RID: 26736
		public const byte Release = 2;

		// Token: 0x04006871 RID: 26737
		private static string StageSuffix = "_dev001";

		// Token: 0x04006872 RID: 26738
		[SerializeField]
		private int m_major;

		// Token: 0x04006873 RID: 26739
		[SerializeField]
		private int m_minor;

		// Token: 0x04006874 RID: 26740
		[SerializeField]
		private int m_release;
	}
}
