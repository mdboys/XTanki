using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Platform.Kernel.ECS.ClientEntitySystem.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002880 RID: 10368
	[NullableContext(1)]
	[Nullable(0)]
	public static class EcsToStringUtil
	{
		// Token: 0x06008A84 RID: 35460 RVA: 0x00131BF0 File Offset: 0x0012FDF0
		public static string ToString(ICollection<Type> components)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			bool flag = true;
			foreach (Type type in components)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(type.Name);
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06008A85 RID: 35461 RVA: 0x00131C74 File Offset: 0x0012FE74
		public static string ToStringWithProperties(object obj, string separator = ", ")
		{
			string text = obj.GetType().Name;
			PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (properties.Length == 0)
			{
				return text;
			}
			text += " [";
			PropertyInfo propertyInfo = null;
			try
			{
				int num = 0;
				foreach (PropertyInfo propertyInfo2 in properties)
				{
					num++;
					if (EcsToStringUtil.NeedShow(propertyInfo2))
					{
						propertyInfo = propertyInfo2;
						string text2 = text;
						text = string.Format("{0}{1}={2}", text2, propertyInfo2.Name, EcsToStringUtil.PropertyToString(obj, propertyInfo2));
						if (num < properties.Length)
						{
							text += separator;
						}
					}
				}
			}
			catch (Exception ex)
			{
				string text3 = text;
				text = string.Format("{0}{1} property={2}", text3, ex.Message, propertyInfo);
			}
			return text + "]";
		}

		// Token: 0x06008A86 RID: 35462 RVA: 0x00131D44 File Offset: 0x0012FF44
		public static object PropertyToString(object obj, PropertyInfo property)
		{
			object value = property.GetValue(obj, BindingFlags.Default, null, null, null);
			if (value == null)
			{
				return "null";
			}
			if (value is string)
			{
				return value;
			}
			if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && !EcsToStringUtil.HasToStringMethod(property.PropertyType))
			{
				return EcsToStringUtil.EnumerableToString((IEnumerable)value, ", ");
			}
			return value;
		}

		// Token: 0x06008A87 RID: 35463 RVA: 0x00052F35 File Offset: 0x00051135
		private static bool HasToStringMethod(Type type)
		{
			MethodInfo method = type.GetMethod("ToString", new Type[0]);
			return ((method != null) ? method.DeclaringType : null) != typeof(object);
		}

		// Token: 0x06008A88 RID: 35464 RVA: 0x00052F63 File Offset: 0x00051163
		public static string EnumerableToString(IEnumerable enumerable)
		{
			return EcsToStringUtil.EnumerableToString(enumerable, ", ");
		}

		// Token: 0x06008A89 RID: 35465 RVA: 0x00052F70 File Offset: 0x00051170
		public static string EnumerableToString(IEnumerable enumerable, string separator)
		{
			return enumerable.GetType().Name + EcsToStringUtil.EnumerableWithoutTypeToString(enumerable, separator);
		}

		// Token: 0x06008A8A RID: 35466 RVA: 0x00131DA8 File Offset: 0x0012FFA8
		public static string EnumerableWithoutTypeToString(IEnumerable enumerable, string separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			bool flag = true;
			foreach (object obj in enumerable)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(separator);
				}
				stringBuilder.Append(obj);
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06008A8B RID: 35467 RVA: 0x00052F89 File Offset: 0x00051189
		private static bool NeedShow(PropertyInfo property)
		{
			return typeof(Component).IsAssignableFrom(property.DeclaringType) && property.GetCustomAttributes(typeof(ObsoleteAttribute), false).Length == 0;
		}

		// Token: 0x06008A8C RID: 35468 RVA: 0x00052FB9 File Offset: 0x000511B9
		public static string ToString(Entity entity)
		{
			if (!(entity is EntityStub))
			{
				return string.Format("[Name={0},\tid={1}]", entity.Name, entity.Id);
			}
			return "[EntityStub]";
		}

		// Token: 0x06008A8D RID: 35469 RVA: 0x00052FE4 File Offset: 0x000511E4
		public static string ToStringWithComponents(EntityInternal entity)
		{
			if (!(entity is EntityStub))
			{
				return string.Format("[{0},\t{1},\t{2}]", entity.Name, entity.Id, EcsToStringUtil.ToString(entity.ComponentClasses));
			}
			return "[EntityStub]";
		}

		// Token: 0x06008A8E RID: 35470 RVA: 0x00131E2C File Offset: 0x0013002C
		public static string ToString(Handler handler)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MethodInfo method = handler.Method;
			string text = EcsToStringUtil.AttributesToString(method.GetCustomAttributes(true));
			stringBuilder.Append(text);
			stringBuilder.Append(" ");
			stringBuilder.Append(EcsToStringUtil.ToString(method));
			stringBuilder.Append(" ");
			stringBuilder.Append("\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06008A8F RID: 35471 RVA: 0x00131E90 File Offset: 0x00130090
		public static string ToString(MethodInfo method)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(method.DeclaringType.Name).Append("::").Append(method.Name)
				.Append("(");
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				object[] customAttributes = parameterInfo.GetCustomAttributes(true);
				if (customAttributes.Length != 0)
				{
					stringBuilder.Append(EcsToStringUtil.AttributesToString(customAttributes));
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(parameterInfo.ParameterType.IsSubclassOf(typeof(ICollection)) ? ("Collection<" + parameterInfo.ParameterType.GetGenericArguments()[0].Name + ">") : parameterInfo.ParameterType.Name);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06008A90 RID: 35472 RVA: 0x00131F8C File Offset: 0x0013018C
		public static string AttributesToString(object[] annotations)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			bool flag = true;
			foreach (object obj in annotations)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(obj.GetType().Name);
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06008A91 RID: 35473 RVA: 0x00131FF4 File Offset: 0x001301F4
		public static string ToString(ICollection<Handler> handlers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			bool flag = true;
			foreach (Handler handler in handlers)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(handler.Method.Name);
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06008A92 RID: 35474 RVA: 0x00132078 File Offset: 0x00130278
		public static object ToString(object[] objects)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			bool flag = true;
			foreach (object obj in objects)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(obj);
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
