using System;
using System.Collections;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using log4net.Layout;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E1 RID: 10721
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ConverterRegistry
	{
		// Token: 0x06009184 RID: 37252 RVA: 0x0013D6D8 File Offset: 0x0013B8D8
		static ConverterRegistry()
		{
			ConverterRegistry.AddConverter(typeof(bool), typeof(BooleanConverter));
			ConverterRegistry.AddConverter(typeof(Encoding), typeof(EncodingConverter));
			ConverterRegistry.AddConverter(typeof(Type), typeof(TypeConverter));
			ConverterRegistry.AddConverter(typeof(PatternLayout), typeof(PatternLayoutConverter));
			ConverterRegistry.AddConverter(typeof(PatternString), typeof(PatternStringConverter));
			ConverterRegistry.AddConverter(typeof(IPAddress), typeof(IPAddressConverter));
		}

		// Token: 0x06009185 RID: 37253 RVA: 0x00005698 File Offset: 0x00003898
		private ConverterRegistry()
		{
		}

		// Token: 0x06009186 RID: 37254 RVA: 0x0013D794 File Offset: 0x0013B994
		public static void AddConverter(Type destinationType, object converter)
		{
			if (destinationType != null && converter != null)
			{
				Hashtable hashtable = ConverterRegistry.s_type2converter;
				lock (hashtable)
				{
					ConverterRegistry.s_type2converter[destinationType] = converter;
				}
			}
		}

		// Token: 0x06009187 RID: 37255 RVA: 0x00057248 File Offset: 0x00055448
		public static void AddConverter(Type destinationType, Type converterType)
		{
			ConverterRegistry.AddConverter(destinationType, ConverterRegistry.CreateConverterInstance(converterType));
		}

		// Token: 0x06009188 RID: 37256 RVA: 0x0013D7D8 File Offset: 0x0013B9D8
		public static IConvertTo GetConvertTo(Type sourceType, Type destinationType)
		{
			Hashtable hashtable = ConverterRegistry.s_type2converter;
			IConvertTo convertTo2;
			lock (hashtable)
			{
				IConvertTo convertTo = ConverterRegistry.s_type2converter[sourceType] as IConvertTo;
				if (convertTo == null)
				{
					convertTo = ConverterRegistry.GetConverterFromAttribute(sourceType) as IConvertTo;
					if (convertTo != null)
					{
						ConverterRegistry.s_type2converter[sourceType] = convertTo;
					}
				}
				convertTo2 = convertTo;
			}
			return convertTo2;
		}

		// Token: 0x06009189 RID: 37257 RVA: 0x0013D83C File Offset: 0x0013BA3C
		public static IConvertFrom GetConvertFrom(Type destinationType)
		{
			Hashtable hashtable = ConverterRegistry.s_type2converter;
			IConvertFrom convertFrom2;
			lock (hashtable)
			{
				IConvertFrom convertFrom = ConverterRegistry.s_type2converter[destinationType] as IConvertFrom;
				if (convertFrom == null)
				{
					convertFrom = ConverterRegistry.GetConverterFromAttribute(destinationType) as IConvertFrom;
					if (convertFrom != null)
					{
						ConverterRegistry.s_type2converter[destinationType] = convertFrom;
					}
				}
				convertFrom2 = convertFrom;
			}
			return convertFrom2;
		}

		// Token: 0x0600918A RID: 37258 RVA: 0x0013D8A0 File Offset: 0x0013BAA0
		private static object GetConverterFromAttribute(Type destinationType)
		{
			object[] customAttributes = destinationType.GetCustomAttributes(typeof(TypeConverterAttribute), true);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				TypeConverterAttribute typeConverterAttribute = customAttributes[0] as TypeConverterAttribute;
				if (typeConverterAttribute != null)
				{
					return ConverterRegistry.CreateConverterInstance(SystemInfo.GetTypeFromString(destinationType, typeConverterAttribute.ConverterTypeName, false, true));
				}
			}
			return null;
		}

		// Token: 0x0600918B RID: 37259 RVA: 0x0013D8EC File Offset: 0x0013BAEC
		private static object CreateConverterInstance(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType", "CreateConverterInstance cannot create instance, converterType is null");
			}
			if (typeof(IConvertFrom).IsAssignableFrom(converterType) || typeof(IConvertTo).IsAssignableFrom(converterType))
			{
				try
				{
					return Activator.CreateInstance(converterType);
				}
				catch (Exception ex)
				{
					LogLog.Error(ConverterRegistry.declaringType, "Cannot CreateConverterInstance of type [" + converterType.FullName + "], Exception in call to Activator.CreateInstance", ex);
					goto IL_0083;
				}
			}
			LogLog.Error(ConverterRegistry.declaringType, "Cannot CreateConverterInstance of type [" + converterType.FullName + "], type does not implement IConvertFrom or IConvertTo");
			IL_0083:
			return null;
		}

		// Token: 0x04006136 RID: 24886
		private static readonly Type declaringType = typeof(ConverterRegistry);

		// Token: 0x04006137 RID: 24887
		private static readonly Hashtable s_type2converter = new Hashtable();
	}
}
