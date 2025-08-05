using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029C7 RID: 10695
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NullEnumerator : IEnumerator
	{
		// Token: 0x06009099 RID: 37017 RVA: 0x00005698 File Offset: 0x00003898
		private NullEnumerator()
		{
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x0600909A RID: 37018 RVA: 0x000568B8 File Offset: 0x00054AB8
		public static NullEnumerator Instance { get; } = new NullEnumerator();

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x0600909B RID: 37019 RVA: 0x000564E5 File Offset: 0x000546E5
		public object Current
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600909C RID: 37020 RVA: 0x00007F86 File Offset: 0x00006186
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x0600909D RID: 37021 RVA: 0x0000568E File Offset: 0x0000388E
		public void Reset()
		{
		}
	}
}
