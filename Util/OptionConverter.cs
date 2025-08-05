using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using log4net.Core;
using log4net.Util.TypeConverters;

namespace log4net.Util
{
	// Token: 0x020029CA RID: 10698
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OptionConverter
	{
		// Token: 0x060090B4 RID: 37044 RVA: 0x00005698 File Offset: 0x00003898
		private OptionConverter()
		{
		}

		// Token: 0x060090B5 RID: 37045 RVA: 0x0013BDBC File Offset: 0x00139FBC
		public static bool ToBoolean(string argValue, bool defaultValue)
		{
			if (argValue != null && argValue.Length > 0)
			{
				try
				{
					return bool.Parse(argValue);
				}
				catch (Exception ex)
				{
					LogLog.Error(OptionConverter.declaringType, "[" + argValue + "] is not in proper bool form.", ex);
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x060090B6 RID: 37046 RVA: 0x0013BE10 File Offset: 0x0013A010
		public static long ToFileSize(string argValue, long defaultValue)
		{
			if (argValue == null)
			{
				return defaultValue;
			}
			string text = argValue.Trim().ToUpper(CultureInfo.InvariantCulture);
			long num = 1L;
			int num2;
			if ((num2 = text.IndexOf("KB")) != -1)
			{
				num = 1024L;
				text = text.Substring(0, num2);
			}
			else if ((num2 = text.IndexOf("MB")) != -1)
			{
				num = 1048576L;
				text = text.Substring(0, num2);
			}
			else if ((num2 = text.IndexOf("GB")) != -1)
			{
				num = 1073741824L;
				text = text.Substring(0, num2);
			}
			if (text != null)
			{
				text = text.Trim();
				long num3;
				if (SystemInfo.TryParse(text, out num3))
				{
					return num3 * num;
				}
				LogLog.Error(OptionConverter.declaringType, "OptionConverter: [" + text + "] is not in the correct file size syntax.");
			}
			return defaultValue;
		}

		// Token: 0x060090B7 RID: 37047 RVA: 0x0013BED0 File Offset: 0x0013A0D0
		public static object ConvertStringTo(Type target, string txt)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (typeof(string) == target || typeof(object) == target)
			{
				return txt;
			}
			IConvertFrom convertFrom = ConverterRegistry.GetConvertFrom(target);
			if (convertFrom != null && convertFrom.CanConvertFrom(typeof(string)))
			{
				return convertFrom.ConvertFrom(txt);
			}
			if (target.IsEnum)
			{
				return OptionConverter.ParseEnum(target, txt, true);
			}
			MethodInfo method = target.GetMethod("Parse", new Type[] { typeof(string) });
			if (method == null)
			{
				return null;
			}
			return method.Invoke(null, BindingFlags.InvokeMethod, null, new object[] { txt }, CultureInfo.InvariantCulture);
		}

		// Token: 0x060090B8 RID: 37048 RVA: 0x0013BF7C File Offset: 0x0013A17C
		public static bool CanConvertTypeTo(Type sourceType, Type targetType)
		{
			if (sourceType == null || targetType == null)
			{
				return false;
			}
			if (targetType.IsAssignableFrom(sourceType))
			{
				return true;
			}
			IConvertTo convertTo = ConverterRegistry.GetConvertTo(sourceType, targetType);
			if (convertTo != null && convertTo.CanConvertTo(targetType))
			{
				return true;
			}
			IConvertFrom convertFrom = ConverterRegistry.GetConvertFrom(targetType);
			return convertFrom != null && convertFrom.CanConvertFrom(sourceType);
		}

		// Token: 0x060090B9 RID: 37049 RVA: 0x0013BFC8 File Offset: 0x0013A1C8
		public static object ConvertTypeTo(object sourceInstance, Type targetType)
		{
			Type type = sourceInstance.GetType();
			if (targetType.IsAssignableFrom(type))
			{
				return sourceInstance;
			}
			IConvertTo convertTo = ConverterRegistry.GetConvertTo(type, targetType);
			if (convertTo != null && convertTo.CanConvertTo(targetType))
			{
				return convertTo.ConvertTo(sourceInstance, targetType);
			}
			IConvertFrom convertFrom = ConverterRegistry.GetConvertFrom(targetType);
			if (convertFrom != null && convertFrom.CanConvertFrom(type))
			{
				return convertFrom.ConvertFrom(sourceInstance);
			}
			throw new ArgumentException(string.Format("Cannot convert source object [{0}] to target type [{1}]", sourceInstance, targetType.Name), "sourceInstance");
		}

		// Token: 0x060090BA RID: 37050 RVA: 0x0013C03C File Offset: 0x0013A23C
		public static object InstantiateByClassName(string className, Type superClass, object defaultValue)
		{
			if (className != null)
			{
				try
				{
					Type typeFromString = SystemInfo.GetTypeFromString(className, true, true);
					if (!superClass.IsAssignableFrom(typeFromString))
					{
						LogLog.Error(OptionConverter.declaringType, string.Concat(new string[] { "OptionConverter: A [", className, "] object is not assignable to a [", superClass.FullName, "] variable." }));
						return defaultValue;
					}
					return Activator.CreateInstance(typeFromString);
				}
				catch (Exception ex)
				{
					LogLog.Error(OptionConverter.declaringType, "Could not instantiate class [" + className + "].", ex);
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x060090BB RID: 37051 RVA: 0x0013C0D8 File Offset: 0x0013A2D8
		public static string SubstituteVariables(string value, IDictionary props)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2;
			for (;;)
			{
				num2 = value.IndexOf("${", num);
				if (num2 == -1)
				{
					break;
				}
				stringBuilder.Append(value.Substring(num, num2 - num));
				int num3 = value.IndexOf('}', num2);
				if (num3 == -1)
				{
					goto IL_008F;
				}
				num2 += 2;
				string text = value.Substring(num2, num3 - num2);
				string text2 = props[text] as string;
				if (text2 != null)
				{
					stringBuilder.Append(text2);
				}
				num = num3 + 1;
			}
			if (num == 0)
			{
				return value;
			}
			stringBuilder.Append(value.Substring(num, value.Length - num));
			return stringBuilder.ToString();
			IL_008F:
			throw new LogException(string.Format("[{0}] has no closing brace. Opening brace at position [{1}]", value, num2));
		}

		// Token: 0x060090BC RID: 37052 RVA: 0x000569C7 File Offset: 0x00054BC7
		private static object ParseEnum(Type enumType, string value, bool ignoreCase)
		{
			return Enum.Parse(enumType, value, ignoreCase);
		}

		// Token: 0x040060FC RID: 24828
		private const string DELIM_START = "${";

		// Token: 0x040060FD RID: 24829
		private const char DELIM_STOP = '}';

		// Token: 0x040060FE RID: 24830
		private const int DELIM_START_LEN = 2;

		// Token: 0x040060FF RID: 24831
		private const int DELIM_STOP_LEN = 1;

		// Token: 0x04006100 RID: 24832
		private static readonly Type declaringType = typeof(OptionConverter);
	}
}
