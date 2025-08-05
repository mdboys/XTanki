using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020029C0 RID: 10688
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LevelMapping : IOptionHandler
	{
		// Token: 0x06009062 RID: 36962 RVA: 0x0013BBB4 File Offset: 0x00139DB4
		public void ActivateOptions()
		{
			Level[] array = new Level[this.m_entriesMap.Count];
			LevelMappingEntry[] array2 = new LevelMappingEntry[this.m_entriesMap.Count];
			this.m_entriesMap.Keys.CopyTo(array, 0);
			this.m_entriesMap.Values.CopyTo(array2, 0);
			Array.Sort<Level, LevelMappingEntry>(array, array2, 0, array.Length, null);
			Array.Reverse(array2, 0, array2.Length);
			LevelMappingEntry[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].ActivateOptions();
			}
			this.m_entries = array2;
		}

		// Token: 0x06009063 RID: 36963 RVA: 0x000565B5 File Offset: 0x000547B5
		public void Add(LevelMappingEntry entry)
		{
			if (this.m_entriesMap.ContainsKey(entry.Level))
			{
				this.m_entriesMap.Remove(entry.Level);
			}
			this.m_entriesMap.Add(entry.Level, entry);
		}

		// Token: 0x06009064 RID: 36964 RVA: 0x0013BC3C File Offset: 0x00139E3C
		public LevelMappingEntry Lookup(Level level)
		{
			if (this.m_entries != null)
			{
				foreach (LevelMappingEntry levelMappingEntry in this.m_entries)
				{
					if (level >= levelMappingEntry.Level)
					{
						return levelMappingEntry;
					}
				}
			}
			return null;
		}

		// Token: 0x040060E0 RID: 24800
		private LevelMappingEntry[] m_entries;

		// Token: 0x040060E1 RID: 24801
		private readonly Hashtable m_entriesMap = new Hashtable();
	}
}
