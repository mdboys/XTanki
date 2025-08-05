using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E0 RID: 10720
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class ConversionNotSupportedException : ApplicationException
	{
		// Token: 0x0600917E RID: 37246 RVA: 0x000571EA File Offset: 0x000553EA
		public ConversionNotSupportedException()
		{
		}

		// Token: 0x0600917F RID: 37247 RVA: 0x000571F2 File Offset: 0x000553F2
		public ConversionNotSupportedException(string message)
			: base(message)
		{
		}

		// Token: 0x06009180 RID: 37248 RVA: 0x000571FB File Offset: 0x000553FB
		public ConversionNotSupportedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06009181 RID: 37249 RVA: 0x00057205 File Offset: 0x00055405
		protected ConversionNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06009182 RID: 37250 RVA: 0x0005720F File Offset: 0x0005540F
		public static ConversionNotSupportedException Create(Type destinationType, object sourceValue)
		{
			return ConversionNotSupportedException.Create(destinationType, sourceValue, null);
		}

		// Token: 0x06009183 RID: 37251 RVA: 0x00057219 File Offset: 0x00055419
		public static ConversionNotSupportedException Create(Type destinationType, object sourceValue, Exception innerException)
		{
			if (sourceValue == null)
			{
				return new ConversionNotSupportedException(string.Format("Cannot convert value [null] to type [{0}]", destinationType), innerException);
			}
			return new ConversionNotSupportedException(string.Format("Cannot convert from type [{0}] value [{1}] to type [{2}]", sourceValue.GetType(), sourceValue, destinationType), innerException);
		}
	}
}
