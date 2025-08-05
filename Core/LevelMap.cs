using System;
using System.Collections;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A5F RID: 10847
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LevelMap
	{
		// Token: 0x1700172C RID: 5932
		public Level this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				Level level;
				lock (this)
				{
					level = (Level)this.m_mapName2Level[name];
				}
				return level;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06009442 RID: 37954 RVA: 0x00143114 File Offset: 0x00141314
		public LevelCollection AllLevels
		{
			get
			{
				LevelCollection levelCollection;
				lock (this)
				{
					levelCollection = new LevelCollection(this.m_mapName2Level.Values);
				}
				return levelCollection;
			}
		}

		// Token: 0x06009443 RID: 37955 RVA: 0x00058886 File Offset: 0x00056A86
		public void Clear()
		{
			this.m_mapName2Level.Clear();
		}

		// Token: 0x06009444 RID: 37956 RVA: 0x00058893 File Offset: 0x00056A93
		public void Add(string name, int value)
		{
			this.Add(name, value, null);
		}

		// Token: 0x06009445 RID: 37957 RVA: 0x00143154 File Offset: 0x00141354
		public void Add(string name, int value, string displayName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("name", name, "Parameter: name, Value: [" + name + "] out of range. Level name must not be empty");
			}
			if (displayName == null || displayName.Length == 0)
			{
				displayName = name;
			}
			this.Add(new Level(value, name, displayName));
		}

		// Token: 0x06009446 RID: 37958 RVA: 0x001431B0 File Offset: 0x001413B0
		public void Add(Level level)
		{
			if (level == null)
			{
				throw new ArgumentNullException("level");
			}
			lock (this)
			{
				this.m_mapName2Level[level.Name] = level;
			}
		}

		// Token: 0x06009447 RID: 37959 RVA: 0x00143204 File Offset: 0x00141404
		public Level LookupWithDefault(Level defaultLevel)
		{
			if (defaultLevel == null)
			{
				throw new ArgumentNullException("defaultLevel");
			}
			Level level2;
			lock (this)
			{
				Level level = (Level)this.m_mapName2Level[defaultLevel.Name];
				if (level == null)
				{
					this.m_mapName2Level[defaultLevel.Name] = defaultLevel;
					level2 = defaultLevel;
				}
				else
				{
					level2 = level;
				}
			}
			return level2;
		}

		// Token: 0x04006224 RID: 25124
		private readonly Hashtable m_mapName2Level = SystemInfo.CreateCaseInsensitiveHashtable();
	}
}
