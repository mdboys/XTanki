using System;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Plugin;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x02002A76 RID: 10870
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class PluginAttribute : Attribute, IPluginFactory
	{
		// Token: 0x06009506 RID: 38150 RVA: 0x0005936D File Offset: 0x0005756D
		public PluginAttribute(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x06009507 RID: 38151 RVA: 0x0005937C File Offset: 0x0005757C
		public PluginAttribute(Type type)
		{
			this.m_type = type;
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x06009508 RID: 38152 RVA: 0x0005938B File Offset: 0x0005758B
		// (set) Token: 0x06009509 RID: 38153 RVA: 0x00059393 File Offset: 0x00057593
		public Type Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x0600950A RID: 38154 RVA: 0x0005939C File Offset: 0x0005759C
		// (set) Token: 0x0600950B RID: 38155 RVA: 0x000593A4 File Offset: 0x000575A4
		public string TypeName
		{
			get
			{
				return this.m_typeName;
			}
			set
			{
				this.m_typeName = value;
			}
		}

		// Token: 0x0600950C RID: 38156 RVA: 0x00144798 File Offset: 0x00142998
		public IPlugin CreatePlugin()
		{
			Type type = this.m_type;
			if (this.m_type == null)
			{
				type = SystemInfo.GetTypeFromString(this.m_typeName, true, true);
			}
			if (!typeof(IPlugin).IsAssignableFrom(type))
			{
				throw new LogException("Plugin type [" + type.FullName + "] does not implement the log4net.IPlugin interface");
			}
			return (IPlugin)Activator.CreateInstance(type);
		}

		// Token: 0x0600950D RID: 38157 RVA: 0x000593AD File Offset: 0x000575AD
		public override string ToString()
		{
			if (this.m_type != null)
			{
				return "PluginAttribute[Type=" + this.m_type.FullName + "]";
			}
			return "PluginAttribute[Type=" + this.m_typeName + "]";
		}

		// Token: 0x04006266 RID: 25190
		private Type m_type;

		// Token: 0x04006267 RID: 25191
		private string m_typeName;
	}
}
