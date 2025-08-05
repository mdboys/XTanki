using System;
using System.Runtime.CompilerServices;

namespace log4net.Util
{
	// Token: 0x020029D9 RID: 10713
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThreadContextProperties : ContextPropertiesBase
	{
		// Token: 0x0600915D RID: 37213 RVA: 0x00057033 File Offset: 0x00055233
		internal ThreadContextProperties()
		{
		}

		// Token: 0x170016AD RID: 5805
		public override object this[string key]
		{
			get
			{
				PropertiesDictionary dictionary = ThreadContextProperties._dictionary;
				if (dictionary == null)
				{
					return null;
				}
				return dictionary[key];
			}
			set
			{
				this.GetProperties(true)[key] = value;
			}
		}

		// Token: 0x06009160 RID: 37216 RVA: 0x0005705E File Offset: 0x0005525E
		public void Remove(string key)
		{
			PropertiesDictionary dictionary = ThreadContextProperties._dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Remove(key);
		}

		// Token: 0x06009161 RID: 37217 RVA: 0x00057070 File Offset: 0x00055270
		public string[] GetKeys()
		{
			PropertiesDictionary dictionary = ThreadContextProperties._dictionary;
			if (dictionary == null)
			{
				return null;
			}
			return dictionary.GetKeys();
		}

		// Token: 0x06009162 RID: 37218 RVA: 0x00057082 File Offset: 0x00055282
		public void Clear()
		{
			PropertiesDictionary dictionary = ThreadContextProperties._dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Clear();
		}

		// Token: 0x06009163 RID: 37219 RVA: 0x00057093 File Offset: 0x00055293
		internal PropertiesDictionary GetProperties(bool create)
		{
			if (ThreadContextProperties._dictionary == null && create)
			{
				ThreadContextProperties._dictionary = new PropertiesDictionary();
			}
			return ThreadContextProperties._dictionary;
		}

		// Token: 0x0400612A RID: 24874
		[ThreadStatic]
		private static PropertiesDictionary _dictionary;
	}
}
