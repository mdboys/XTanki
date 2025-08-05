using System;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029DD RID: 10717
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThreadContextStacks
	{
		// Token: 0x06009173 RID: 37235 RVA: 0x0005719B File Offset: 0x0005539B
		internal ThreadContextStacks(ContextPropertiesBase properties)
		{
			this.m_properties = properties;
		}

		// Token: 0x170016B2 RID: 5810
		public ThreadContextStack this[string key]
		{
			get
			{
				object obj = this.m_properties[key];
				ThreadContextStack threadContextStack;
				if (obj == null)
				{
					threadContextStack = new ThreadContextStack();
					this.m_properties[key] = threadContextStack;
				}
				else
				{
					threadContextStack = obj as ThreadContextStack;
					if (threadContextStack == null)
					{
						string text = SystemInfo.NullText;
						try
						{
							text = obj.ToString();
						}
						catch
						{
						}
						LogLog.Error(ThreadContextStacks.declaringType, string.Concat(new string[]
						{
							"ThreadContextStacks: Request for stack named [",
							key,
							"] failed because a property with the same name exists which is a [",
							obj.GetType().Name,
							"] with value [",
							text,
							"]"
						}));
						threadContextStack = new ThreadContextStack();
					}
				}
				return threadContextStack;
			}
		}

		// Token: 0x04006131 RID: 24881
		private static readonly Type declaringType = typeof(ThreadContextStacks);

		// Token: 0x04006132 RID: 24882
		private readonly ContextPropertiesBase m_properties;
	}
}
