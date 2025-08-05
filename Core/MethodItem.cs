using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A6A RID: 10858
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class MethodItem
	{
		// Token: 0x060094CF RID: 38095 RVA: 0x00059195 File Offset: 0x00057395
		public MethodItem()
		{
			this.Name = "?";
			this.Parameters = new string[0];
		}

		// Token: 0x060094D0 RID: 38096 RVA: 0x000591B4 File Offset: 0x000573B4
		public MethodItem(string name)
			: this()
		{
			this.Name = name;
		}

		// Token: 0x060094D1 RID: 38097 RVA: 0x000591C3 File Offset: 0x000573C3
		public MethodItem(string name, string[] parameters)
			: this(name)
		{
			this.Parameters = parameters;
		}

		// Token: 0x060094D2 RID: 38098 RVA: 0x000591D3 File Offset: 0x000573D3
		public MethodItem(MethodBase methodBase)
			: this(methodBase.Name, MethodItem.GetMethodParameterNames(methodBase))
		{
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x060094D3 RID: 38099 RVA: 0x000591E7 File Offset: 0x000573E7
		public string Name { get; }

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x060094D4 RID: 38100 RVA: 0x000591EF File Offset: 0x000573EF
		public string[] Parameters { get; }

		// Token: 0x060094D5 RID: 38101 RVA: 0x001442F0 File Offset: 0x001424F0
		private static string[] GetMethodParameterNames(MethodBase methodBase)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				ParameterInfo[] parameters = methodBase.GetParameters();
				int upperBound = parameters.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					arrayList.Add(string.Format("{0} {1}", parameters[i].ParameterType, parameters[i].Name));
				}
			}
			catch (Exception ex)
			{
				LogLog.Error(MethodItem.declaringType, "An exception ocurred while retreiving method parameters.", ex);
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x04006251 RID: 25169
		private const string NA = "?";

		// Token: 0x04006252 RID: 25170
		private static readonly Type declaringType = typeof(MethodItem);
	}
}
