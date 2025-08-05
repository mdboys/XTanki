using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net
{
	// Token: 0x020029B4 RID: 10676
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NDC
	{
		// Token: 0x06009005 RID: 36869 RVA: 0x00005698 File Offset: 0x00003898
		private NDC()
		{
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06009006 RID: 36870 RVA: 0x000562F9 File Offset: 0x000544F9
		public static int Depth
		{
			get
			{
				return ThreadContext.Stacks["NDC"].Count;
			}
		}

		// Token: 0x06009007 RID: 36871 RVA: 0x0005630F File Offset: 0x0005450F
		public static void Clear()
		{
			ThreadContext.Stacks["NDC"].Clear();
		}

		// Token: 0x06009008 RID: 36872 RVA: 0x00056325 File Offset: 0x00054525
		public static Stack CloneStack()
		{
			return ThreadContext.Stacks["NDC"].InternalStack;
		}

		// Token: 0x06009009 RID: 36873 RVA: 0x0005633B File Offset: 0x0005453B
		public static void Inherit(Stack stack)
		{
			ThreadContext.Stacks["NDC"].InternalStack = stack;
		}

		// Token: 0x0600900A RID: 36874 RVA: 0x00056352 File Offset: 0x00054552
		public static string Pop()
		{
			return ThreadContext.Stacks["NDC"].Pop();
		}

		// Token: 0x0600900B RID: 36875 RVA: 0x00056368 File Offset: 0x00054568
		public static IDisposable Push(string message)
		{
			return ThreadContext.Stacks["NDC"].Push(message);
		}

		// Token: 0x0600900C RID: 36876 RVA: 0x0000568E File Offset: 0x0000388E
		public static void Remove()
		{
		}

		// Token: 0x0600900D RID: 36877 RVA: 0x0013B184 File Offset: 0x00139384
		public static void SetMaxDepth(int maxDepth)
		{
			if (maxDepth < 0)
			{
				return;
			}
			ThreadContextStack threadContextStack = ThreadContext.Stacks["NDC"];
			if (maxDepth == 0)
			{
				threadContextStack.Clear();
				return;
			}
			while (threadContextStack.Count > maxDepth)
			{
				threadContextStack.Pop();
			}
		}
	}
}
