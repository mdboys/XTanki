using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029DA RID: 10714
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThreadContextStack : IFixingRequired
	{
		// Token: 0x06009164 RID: 37220 RVA: 0x000570B0 File Offset: 0x000552B0
		internal ThreadContextStack()
		{
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06009165 RID: 37221 RVA: 0x000570C3 File Offset: 0x000552C3
		public int Count
		{
			get
			{
				return this.InternalStack.Count;
			}
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06009166 RID: 37222 RVA: 0x000570D0 File Offset: 0x000552D0
		// (set) Token: 0x06009167 RID: 37223 RVA: 0x000570D8 File Offset: 0x000552D8
		internal Stack InternalStack { get; set; } = new Stack();

		// Token: 0x06009168 RID: 37224 RVA: 0x000570E1 File Offset: 0x000552E1
		object IFixingRequired.GetFixedObject()
		{
			return this.GetFullMessage();
		}

		// Token: 0x06009169 RID: 37225 RVA: 0x000570E9 File Offset: 0x000552E9
		public void Clear()
		{
			this.InternalStack.Clear();
		}

		// Token: 0x0600916A RID: 37226 RVA: 0x0013D41C File Offset: 0x0013B61C
		public string Pop()
		{
			Stack internalStack = this.InternalStack;
			if (internalStack.Count > 0)
			{
				return ((ThreadContextStack.StackFrame)internalStack.Pop()).Message;
			}
			return string.Empty;
		}

		// Token: 0x0600916B RID: 37227 RVA: 0x0013D450 File Offset: 0x0013B650
		public IDisposable Push(string message)
		{
			Stack internalStack = this.InternalStack;
			internalStack.Push(new ThreadContextStack.StackFrame(message, (internalStack.Count <= 0) ? null : ((ThreadContextStack.StackFrame)internalStack.Peek())));
			return new ThreadContextStack.AutoPopStackFrame(internalStack, internalStack.Count - 1);
		}

		// Token: 0x0600916C RID: 37228 RVA: 0x0013D49C File Offset: 0x0013B69C
		internal string GetFullMessage()
		{
			Stack internalStack = this.InternalStack;
			if (internalStack.Count > 0)
			{
				return ((ThreadContextStack.StackFrame)internalStack.Peek()).FullMessage;
			}
			return null;
		}

		// Token: 0x0600916D RID: 37229 RVA: 0x000570E1 File Offset: 0x000552E1
		public override string ToString()
		{
			return this.GetFullMessage();
		}

		// Token: 0x020029DB RID: 10715
		[Nullable(0)]
		private sealed class StackFrame
		{
			// Token: 0x0600916E RID: 37230 RVA: 0x000570F6 File Offset: 0x000552F6
			internal StackFrame(string message, ThreadContextStack.StackFrame parent)
			{
				this.Message = message;
				this.m_parent = parent;
				if (parent == null)
				{
					this.m_fullMessage = message;
				}
			}

			// Token: 0x170016B0 RID: 5808
			// (get) Token: 0x0600916F RID: 37231 RVA: 0x00057116 File Offset: 0x00055316
			internal string Message { get; }

			// Token: 0x170016B1 RID: 5809
			// (get) Token: 0x06009170 RID: 37232 RVA: 0x0005711E File Offset: 0x0005531E
			internal string FullMessage
			{
				get
				{
					if (this.m_fullMessage == null && this.m_parent != null)
					{
						this.m_fullMessage = this.m_parent.FullMessage + " " + this.Message;
					}
					return this.m_fullMessage;
				}
			}

			// Token: 0x0400612C RID: 24876
			private readonly ThreadContextStack.StackFrame m_parent;

			// Token: 0x0400612D RID: 24877
			private string m_fullMessage;
		}

		// Token: 0x020029DC RID: 10716
		[NullableContext(0)]
		private struct AutoPopStackFrame : IDisposable
		{
			// Token: 0x06009171 RID: 37233 RVA: 0x00057157 File Offset: 0x00055357
			[NullableContext(1)]
			internal AutoPopStackFrame(Stack frameStack, int frameDepth)
			{
				this.m_frameStack = frameStack;
				this.m_frameDepth = frameDepth;
			}

			// Token: 0x06009172 RID: 37234 RVA: 0x00057167 File Offset: 0x00055367
			public void Dispose()
			{
				if (this.m_frameDepth >= 0 && this.m_frameStack != null)
				{
					while (this.m_frameStack.Count > this.m_frameDepth)
					{
						this.m_frameStack.Pop();
					}
				}
			}

			// Token: 0x0400612F RID: 24879
			[Nullable(1)]
			private readonly Stack m_frameStack;

			// Token: 0x04006130 RID: 24880
			private readonly int m_frameDepth;
		}
	}
}
