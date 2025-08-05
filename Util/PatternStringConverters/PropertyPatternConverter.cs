using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020029F2 RID: 10738
	internal sealed class PropertyPatternConverter : PatternConverter
	{
		// Token: 0x060091BF RID: 37311 RVA: 0x0013DE44 File Offset: 0x0013C044
		[NullableContext(1)]
		protected override void Convert(TextWriter writer, object state)
		{
			CompositeProperties compositeProperties = new CompositeProperties();
			PropertiesDictionary properties = ThreadContext.Properties.GetProperties(false);
			if (properties != null)
			{
				compositeProperties.Add(properties);
			}
			compositeProperties.Add(GlobalContext.Properties.GetReadOnlyProperties());
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, null, compositeProperties[this.Option]);
				return;
			}
			PatternConverter.WriteDictionary(writer, null, compositeProperties.Flatten());
		}
	}
}
