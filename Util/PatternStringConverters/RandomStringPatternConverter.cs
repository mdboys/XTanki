using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029F3 RID: 10739
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RandomStringPatternConverter : PatternConverter, IOptionHandler
	{
		// Token: 0x060091C1 RID: 37313 RVA: 0x0013DEA8 File Offset: 0x0013C0A8
		public void ActivateOptions()
		{
			string option = this.Option;
			if (option != null && option.Length > 0)
			{
				int num;
				if (SystemInfo.TryParse(option, out num))
				{
					this.m_length = num;
					return;
				}
				LogLog.Error(RandomStringPatternConverter.declaringType, "RandomStringPatternConverter: Could not convert Option [" + option + "] to Length Int32");
			}
		}

		// Token: 0x060091C2 RID: 37314 RVA: 0x0013DEF4 File Offset: 0x0013C0F4
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				Random random = RandomStringPatternConverter.s_random;
				lock (random)
				{
					for (int i = 0; i < this.m_length; i++)
					{
						int num = RandomStringPatternConverter.s_random.Next(36);
						if (num < 26)
						{
							char c = (char)(65 + num);
							writer.Write(c);
						}
						else if (num < 36)
						{
							char c2 = (char)(48 + (num - 26));
							writer.Write(c2);
						}
						else
						{
							writer.Write('X');
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogLog.Error(RandomStringPatternConverter.declaringType, "Error occurred while converting.", ex);
			}
		}

		// Token: 0x04006140 RID: 24896
		private static readonly Random s_random = new Random();

		// Token: 0x04006141 RID: 24897
		private static readonly Type declaringType = typeof(RandomStringPatternConverter);

		// Token: 0x04006142 RID: 24898
		private int m_length = 4;
	}
}
