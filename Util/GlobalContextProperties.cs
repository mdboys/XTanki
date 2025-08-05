using System;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029BF RID: 10687
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GlobalContextProperties : ContextPropertiesBase
	{
		// Token: 0x0600905C RID: 36956 RVA: 0x0005657B File Offset: 0x0005477B
		internal GlobalContextProperties()
		{
		}

		// Token: 0x17001668 RID: 5736
		public override object this[string key]
		{
			get
			{
				return this.m_readOnlyProperties[key];
			}
			set
			{
				object syncRoot = this.m_syncRoot;
				lock (syncRoot)
				{
					PropertiesDictionary propertiesDictionary = new PropertiesDictionary(this.m_readOnlyProperties);
					propertiesDictionary[key] = value;
					PropertiesDictionary propertiesDictionary2 = propertiesDictionary;
					this.m_readOnlyProperties = new ReadOnlyPropertiesDictionary(propertiesDictionary2);
				}
			}
		}

		// Token: 0x0600905F RID: 36959 RVA: 0x0013BB0C File Offset: 0x00139D0C
		public void Remove(string key)
		{
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				if (this.m_readOnlyProperties.Contains(key))
				{
					PropertiesDictionary propertiesDictionary = new PropertiesDictionary(this.m_readOnlyProperties);
					propertiesDictionary.Remove(key);
					this.m_readOnlyProperties = new ReadOnlyPropertiesDictionary(propertiesDictionary);
				}
			}
		}

		// Token: 0x06009060 RID: 36960 RVA: 0x0013BB74 File Offset: 0x00139D74
		public void Clear()
		{
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				this.m_readOnlyProperties = new ReadOnlyPropertiesDictionary();
			}
		}

		// Token: 0x06009061 RID: 36961 RVA: 0x000565AB File Offset: 0x000547AB
		internal ReadOnlyPropertiesDictionary GetReadOnlyProperties()
		{
			return this.m_readOnlyProperties;
		}

		// Token: 0x040060DE RID: 24798
		private readonly object m_syncRoot = new object();

		// Token: 0x040060DF RID: 24799
		private volatile ReadOnlyPropertiesDictionary m_readOnlyProperties = new ReadOnlyPropertiesDictionary();
	}
}
