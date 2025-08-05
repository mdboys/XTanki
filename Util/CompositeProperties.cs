using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029B7 RID: 10679
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CompositeProperties
	{
		// Token: 0x0600901D RID: 36893 RVA: 0x00056418 File Offset: 0x00054618
		internal CompositeProperties()
		{
		}

		// Token: 0x1700164F RID: 5711
		public object this[string key]
		{
			get
			{
				if (this.m_flattened != null)
				{
					return this.m_flattened[key];
				}
				foreach (object obj in this.m_nestedProperties)
				{
					ReadOnlyPropertiesDictionary readOnlyPropertiesDictionary = (ReadOnlyPropertiesDictionary)obj;
					if (readOnlyPropertiesDictionary.Contains(key))
					{
						return readOnlyPropertiesDictionary[key];
					}
				}
				return null;
			}
		}

		// Token: 0x0600901F RID: 36895 RVA: 0x0005642B File Offset: 0x0005462B
		public void Add(ReadOnlyPropertiesDictionary properties)
		{
			this.m_flattened = null;
			this.m_nestedProperties.Add(properties);
		}

		// Token: 0x06009020 RID: 36896 RVA: 0x0013B540 File Offset: 0x00139740
		public PropertiesDictionary Flatten()
		{
			if (this.m_flattened == null)
			{
				this.m_flattened = new PropertiesDictionary();
				int num = this.m_nestedProperties.Count;
				while (--num >= 0)
				{
					foreach (object obj in ((IEnumerable)((ReadOnlyPropertiesDictionary)this.m_nestedProperties[num])))
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						this.m_flattened[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			return this.m_flattened;
		}

		// Token: 0x040060CE RID: 24782
		private PropertiesDictionary m_flattened;

		// Token: 0x040060CF RID: 24783
		private readonly ArrayList m_nestedProperties = new ArrayList();
	}
}
