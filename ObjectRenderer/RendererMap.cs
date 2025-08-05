using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.ObjectRenderer
{
	// Token: 0x02002A16 RID: 10774
	[NullableContext(1)]
	[Nullable(0)]
	public class RendererMap
	{
		// Token: 0x060092E9 RID: 37609 RVA: 0x00057CB3 File Offset: 0x00055EB3
		public RendererMap()
		{
			this.m_map = Hashtable.Synchronized(new Hashtable());
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x060092EA RID: 37610 RVA: 0x00057CD6 File Offset: 0x00055ED6
		public IObjectRenderer DefaultRenderer
		{
			get
			{
				return RendererMap.s_defaultRenderer;
			}
		}

		// Token: 0x060092EB RID: 37611 RVA: 0x00140B7C File Offset: 0x0013ED7C
		public string FindAndRender(object obj)
		{
			string text = obj as string;
			if (text != null)
			{
				return text;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.FindAndRender(obj, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x060092EC RID: 37612 RVA: 0x00140BB0 File Offset: 0x0013EDB0
		public void FindAndRender(object obj, TextWriter writer)
		{
			if (obj == null)
			{
				writer.Write(SystemInfo.NullText);
				return;
			}
			string text = obj as string;
			if (text != null)
			{
				writer.Write(text);
				return;
			}
			try
			{
				this.Get(obj.GetType()).RenderObject(this, obj, writer);
			}
			catch (Exception ex)
			{
				LogLog.Error(RendererMap.declaringType, "Exception while rendering object of type [" + obj.GetType().FullName + "]", ex);
				string text2 = string.Empty;
				if (obj != null && obj.GetType() != null)
				{
					text2 = obj.GetType().FullName;
				}
				writer.Write("<log4net.Error>Exception rendering object type [" + text2 + "]");
				if (ex != null)
				{
					string text3 = null;
					try
					{
						text3 = ex.ToString();
					}
					catch
					{
					}
					writer.Write("<stackTrace>" + text3 + "</stackTrace>");
				}
				writer.Write("</log4net.Error>");
			}
		}

		// Token: 0x060092ED RID: 37613 RVA: 0x00057CDD File Offset: 0x00055EDD
		public IObjectRenderer Get(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return this.Get(obj.GetType());
		}

		// Token: 0x060092EE RID: 37614 RVA: 0x00140CA4 File Offset: 0x0013EEA4
		public IObjectRenderer Get(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IObjectRenderer objectRenderer = (IObjectRenderer)this.m_cache[type];
			if (objectRenderer == null)
			{
				for (Type type2 = type; type2 != null; type2 = type2.BaseType)
				{
					objectRenderer = this.SearchTypeAndInterfaces(type2);
					if (objectRenderer != null)
					{
						break;
					}
				}
				if (objectRenderer == null)
				{
					objectRenderer = RendererMap.s_defaultRenderer;
				}
				this.m_cache[type] = objectRenderer;
			}
			return objectRenderer;
		}

		// Token: 0x060092EF RID: 37615 RVA: 0x00140D08 File Offset: 0x0013EF08
		private IObjectRenderer SearchTypeAndInterfaces(Type type)
		{
			IObjectRenderer objectRenderer = (IObjectRenderer)this.m_map[type];
			if (objectRenderer != null)
			{
				return objectRenderer;
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				objectRenderer = this.SearchTypeAndInterfaces(type2);
				if (objectRenderer != null)
				{
					return objectRenderer;
				}
			}
			return null;
		}

		// Token: 0x060092F0 RID: 37616 RVA: 0x00057CF0 File Offset: 0x00055EF0
		public void Clear()
		{
			this.m_map.Clear();
			this.m_cache.Clear();
		}

		// Token: 0x060092F1 RID: 37617 RVA: 0x00057D08 File Offset: 0x00055F08
		public void Put(Type typeToRender, IObjectRenderer renderer)
		{
			this.m_cache.Clear();
			if (typeToRender == null)
			{
				throw new ArgumentNullException("typeToRender");
			}
			if (renderer == null)
			{
				throw new ArgumentNullException("renderer");
			}
			this.m_map[typeToRender] = renderer;
		}

		// Token: 0x04006192 RID: 24978
		private static readonly Type declaringType = typeof(RendererMap);

		// Token: 0x04006193 RID: 24979
		private static readonly IObjectRenderer s_defaultRenderer = new DefaultRenderer();

		// Token: 0x04006194 RID: 24980
		private readonly Hashtable m_cache = new Hashtable();

		// Token: 0x04006195 RID: 24981
		private readonly Hashtable m_map;
	}
}
