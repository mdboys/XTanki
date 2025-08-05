using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200285F RID: 10335
	public static class ClassUtils
	{
		// Token: 0x06008A30 RID: 35376 RVA: 0x001315B8 File Offset: 0x0012F7B8
		[NullableContext(1)]
		public static IList<Type> GetClasses(Type cls, Type to, IList<Type> classes)
		{
			if (cls == to)
			{
				return classes;
			}
			classes.Add(cls);
			Type type = cls.BaseType;
			while (type != to)
			{
				classes.Add(type);
				type = ((type != null) ? type.BaseType : null);
				if (type == null)
				{
					throw new ArgumentException(string.Format("cls = {0}, to = {1}", cls, to));
				}
			}
			return classes;
		}
	}
}
